using System;
using System.Collections.Generic;
using System.Linq;
using OvertimeScheduler.Models;

namespace OvertimeScheduler.Services
{
    public class SchedulerEngine
    {
        public static int GetIso8601WeekOfYear(DateTime time)
        {
            DayOfWeek day = System.Globalization.CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }
            return System.Globalization.CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public static int GetEmployeeShiftGroup(string empId)
        {
            int idVal = 0;
            string numStr = new string(empId.Where(char.IsDigit).ToArray());
            if (int.TryParse(numStr, out idVal))
            {
                return idVal % 3;
            }
            return Math.Abs(empId.GetHashCode()) % 3;
        }

        // Trả về: 0 = Ca 1 (Ngày), 1 = Ca 3 (Đêm), 2 = Ca 2 (Về ca)
        public static int GetShiftRotationForWeek(string empId, DateTime date)
        {
            int w = GetIso8601WeekOfYear(date);
            int group = GetEmployeeShiftGroup(empId);
            return ((w / 2) + group) % 3;
        }

        public static List<ScheduleEntry> AutoSchedule(
            List<Employee> employees, 
            DateTime fromDate, 
            DateTime toDate, 
            bool saturdayWorking = false, 
            int maxPerShift = 2,
            List<DateTime> holidays = null)
        {
            var schedule = new List<ScheduleEntry>();
            var activeEmployees = employees.ToList();
            
            // Map để theo dõi tổng số tiếng tăng ca của mỗi nhân sự
            var shiftHours = activeEmployees.ToDictionary(e => e.Id, e => 0.0);
            
            // 1. Đăng ký trước các giờ làm thêm cố định
            foreach (var emp in activeEmployees)
            {
                foreach (var kvp in emp.FixedOvertimeHours)
                {
                    if (kvp.Key >= fromDate.Date && kvp.Key <= toDate.Date)
                    {
                        shiftHours[emp.Id] += kvp.Value;
                    }
                }
            }

            var rand = new Random();

            // 2. Xác định các ngày khóa đại diện duy nhất (Key Dates) trong khoảng thời gian
            var keyDates = new List<DateTime>();
            for (var d = fromDate.Date; d <= toDate.Date; d = d.AddDays(1))
            {
                DateTime key = GetScheduleKey(d, saturdayWorking, holidays);
                if (key != DateTime.MinValue && !keyDates.Contains(key))
                {
                    keyDates.Add(key);
                }
            }

            // 3. Xếp ca cho từng Key Date
            foreach (var date in keyDates)
            {
                var dayEntry = new ScheduleEntry(date, "Ngày");
                var nightEntry = new ScheduleEntry(date, "Đêm");
                var adminEntry = new ScheduleEntry(date, "Hành chính");

                // Lấy tất cả ngày thực tế trong khoảng mà Key Date này đại diện
                var representedDays = GetRepresentedDays(date, saturdayWorking, fromDate, toDate, holidays);
                if (representedDays.Count == 0) continue;

                // Lọc ra các nhân viên đi làm hôm nay (không xin nghỉ phép vào bất kỳ ngày nào trong block)
                var availableToday = activeEmployees
                    .Where(e => !e.LeavePeriods.Any(lp => representedDays.Any(day => day >= lp.StartDate && day <= lp.EndDate)))
                    .ToList();

                var pickedToday = new HashSet<string>();

                // Ưu tiên điền các nhân sự có gán giờ làm việc cố định
                var fixedToday = availableToday.Where(e => representedDays.Any(day => e.FixedOvertimeHours.ContainsKey(day) && e.FixedOvertimeHours[day] > 0)).ToList();
                foreach (var emp in fixedToday)
                {
                    if (emp.Role == EmployeeRole.Leader || emp.Role == EmployeeRole.Technician)
                    {
                        if (!dayEntry.EmployeeIds.Contains(emp.Id)) dayEntry.EmployeeIds.Add(emp.Id);
                    }
                    else
                    {
                        if (!adminEntry.EmployeeIds.Contains(emp.Id)) adminEntry.EmployeeIds.Add(emp.Id);
                    }
                    pickedToday.Add(emp.Id);
                }

                // Hàm chọn người dựa trên tổng giờ tăng ca ít nhất
                string PickEmployee(List<Employee> pool, HashSet<string> alreadyPickedToday, Func<Employee, bool> criteria = null)
                {
                    var candidates = pool
                        .Where(e => !alreadyPickedToday.Contains(e.Id))
                        .Where(criteria ?? (e => true))
                        .OrderBy(e => shiftHours[e.Id])
                        .ThenBy(e => rand.Next())
                        .ToList();

                    if (candidates.Count > 0)
                    {
                        var picked = candidates[0];
                        alreadyPickedToday.Add(picked.Id);
                        
                        double hoursToAdd = 0;
                        foreach (var day in representedDays)
                        {
                            if (!picked.FixedOvertimeHours.ContainsKey(day))
                            {
                                hoursToAdd += 4.0;
                            }
                        }
                        shiftHours[picked.Id] += hoursToAdd;

                        return picked.Id;
                    }
                    return null;
                }

                // Ca Ngày: lọc chỉ lấy Leader, Tech, hoặc Operator xoay ca == 0 (Ca 1/Ngày)
                var poolDay = availableToday.Where(e => 
                    e.Role == EmployeeRole.Leader || 
                    e.Role == EmployeeRole.Technician || 
                    ((e.Role == EmployeeRole.Worker || e.Role == EmployeeRole.NewWorker) && GetShiftRotationForWeek(e.Id, date) == 0)
                ).ToList();

                while (dayEntry.EmployeeIds.Count < maxPerShift)
                {
                    var emp = PickEmployee(poolDay, pickedToday);
                    if (emp == null) break;
                    dayEntry.EmployeeIds.Add(emp);
                }

                // Ca Đêm: lọc lấy Leader, Tech (không lấy NewWorker), hoặc Operator xoay ca == 1 (Ca 3/Đêm)
                var poolNight = availableToday.Where(e => 
                    e.Role == EmployeeRole.Leader || 
                    e.Role == EmployeeRole.Technician || 
                    ((e.Role == EmployeeRole.Worker || e.Role == EmployeeRole.NewWorker) && GetShiftRotationForWeek(e.Id, date) == 1)
                ).ToList();

                while (nightEntry.EmployeeIds.Count < maxPerShift)
                {
                    var emp = PickEmployee(poolNight, pickedToday, e => e.Role != EmployeeRole.NewWorker && e.Role != EmployeeRole.Leader);
                    if (emp == null) emp = PickEmployee(poolNight, pickedToday);
                    if (emp == null) break;
                    nightEntry.EmployeeIds.Add(emp);
                }

                // Hành Chính: lọc lấy Leader, Tech, hoặc Operator xoay ca == 0 hoặc 1 (loại trừ ca 2)
                var poolAdmin = availableToday.Where(e => 
                    e.Role == EmployeeRole.Leader || 
                    e.Role == EmployeeRole.Technician || 
                    ((e.Role == EmployeeRole.Worker || e.Role == EmployeeRole.NewWorker) && GetShiftRotationForWeek(e.Id, date) != 2)
                ).ToList();

                while (adminEntry.EmployeeIds.Count < 1)
                {
                    var emp = PickEmployee(poolAdmin, pickedToday);
                    if (emp == null) break;
                    adminEntry.EmployeeIds.Add(emp);
                }

                schedule.Add(dayEntry);
                schedule.Add(nightEntry);
                schedule.Add(adminEntry);
            }

            return schedule;
        }

        private static DateTime GetMonday(DateTime date)
        {
            int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return date.AddDays(-1 * diff).Date;
        }

        public static DateTime GetScheduleKey(DateTime date, bool saturdayWorking, List<DateTime> holidays = null)
        {
            if (holidays != null && holidays.Contains(date.Date))
            {
                return date.Date; // Ngày lễ hoặc ngày nghỉ IRISO xếp riêng
            }
            if (date.DayOfWeek == DayOfWeek.Sunday)
            {
                return date.Date;
            }
            if (date.DayOfWeek == DayOfWeek.Saturday)
            {
                // Nếu Thứ 7 là ngày làm việc thường (không nằm trong danh sách nghỉ lễ/nghỉ IRISO)
                // Hoặc nếu cấu hình luôn coi Thứ 7 làm thường (saturdayWorking = true)
                return DateTime.MinValue; // Bỏ qua không tính tăng ca
            }
            return GetMonday(date);
        }

        private static List<DateTime> GetRepresentedDays(DateTime keyDate, bool saturdayWorking, DateTime fromDate, DateTime toDate, List<DateTime> holidays)
        {
            var days = new List<DateTime>();
            if (keyDate == DateTime.MinValue) return days;

            if (holidays != null && holidays.Contains(keyDate))
            {
                if (keyDate >= fromDate && keyDate <= toDate) days.Add(keyDate);
            }
            else if (keyDate.DayOfWeek == DayOfWeek.Sunday)
            {
                if (keyDate >= fromDate && keyDate <= toDate) days.Add(keyDate);
            }
            else if (keyDate.DayOfWeek == DayOfWeek.Saturday)
            {
                // Thứ 7 không phải ngày lễ (ngày nghỉ) thì không đại diện cho ngày nào (đã trả về MinValue ở GetScheduleKey)
            }
            else
            {
                // Ngày thường: Thứ 2 đến Thứ 6 loại trừ ngày lễ
                DateTime monday = keyDate;
                for (int i = 0; i < 5; i++)
                {
                    DateTime d = monday.AddDays(i);
                    if (holidays != null && holidays.Contains(d)) continue;
                    if (d >= fromDate && d <= toDate)
                    {
                        days.Add(d);
                    }
                }
            }
            return days;
        }
    }
}
