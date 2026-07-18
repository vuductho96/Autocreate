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
            List<DateTime> holidays = null,
            double weekdayBudget = 300,
            double weekendBudget = 200)
        {
            var schedule = new List<ScheduleEntry>();
            var activeEmployees = employees.ToList();
            var holidayDates = holidays ?? new List<DateTime>();
            
            // Map để theo dõi tổng số tiếng tăng ca của mỗi nhân sự
            var shiftHours = activeEmployees.ToDictionary(e => e.Id, e => 0.0);
            
            // 1. Đăng ký trước các giờ làm thêm cố định và đếm số ngày thường / ngày nghỉ trong khoảng thời gian
            double weekdayFixedHours = 0;
            double weekendFixedHours = 0;
            int numWeekdays = 0;
            int numWeekendDays = 0;

            for (var d = fromDate.Date; d <= toDate.Date; d = d.AddDays(1))
            {
                bool isWeekendOrHoliday = d.DayOfWeek == DayOfWeek.Saturday || 
                                         d.DayOfWeek == DayOfWeek.Sunday || 
                                         holidayDates.Contains(d);
                if (d.DayOfWeek == DayOfWeek.Saturday && saturdayWorking)
                {
                    isWeekendOrHoliday = false;
                }

                if (isWeekendOrHoliday)
                    numWeekendDays++;
                else
                    numWeekdays++;
            }

            foreach (var emp in activeEmployees)
            {
                foreach (var kvp in emp.FixedOvertimeHours)
                {
                    if (kvp.Key >= fromDate.Date && kvp.Key <= toDate.Date)
                    {
                        shiftHours[emp.Id] += kvp.Value;

                        bool isWeekendOrHoliday = kvp.Key.DayOfWeek == DayOfWeek.Saturday || 
                                                 kvp.Key.DayOfWeek == DayOfWeek.Sunday || 
                                                 holidayDates.Contains(kvp.Key.Date);
                        if (kvp.Key.DayOfWeek == DayOfWeek.Saturday && saturdayWorking)
                        {
                            isWeekendOrHoliday = false;
                        }

                        if (isWeekendOrHoliday)
                            weekendFixedHours += kvp.Value;
                        else
                            weekdayFixedHours += kvp.Value;
                    }
                }
            }

            // Tính toán động giới hạn số người tối đa/ca dựa trên ngân sách quỹ giờ
            int maxPerShiftWeekday = maxPerShift;
            if (numWeekdays > 0)
            {
                double weekdayBudgetAvailable = weekdayBudget - weekdayFixedHours - (4.0 * numWeekdays); // Trừ đi ca hành chính mặc định
                if (weekdayBudgetAvailable < 0) weekdayBudgetAvailable = 0;
                double weekdaySlotsAvailable = weekdayBudgetAvailable / (4.0 * numWeekdays);
                double weekdaySlotsPerShift = weekdaySlotsAvailable / 2.0; // chia cho CA 1 và CA 3
                maxPerShiftWeekday = (int)Math.Floor(weekdaySlotsPerShift);
                maxPerShiftWeekday = Math.Max(0, Math.Min(maxPerShift, maxPerShiftWeekday));
            }
            else
            {
                maxPerShiftWeekday = 0;
            }

            int maxPerShiftWeekend = maxPerShift;
            if (numWeekendDays > 0)
            {
                double weekendBudgetAvailable = weekendBudget - weekendFixedHours - (12.0 * numWeekendDays); // Trừ đi ca hành chính mặc định
                if (weekendBudgetAvailable < 0) weekendBudgetAvailable = 0;
                double weekendSlotsAvailable = weekendBudgetAvailable / (12.0 * numWeekendDays);
                double weekendSlotsPerShift = weekendSlotsAvailable / 2.0; // chia cho CA 1 và CA 3
                maxPerShiftWeekend = (int)Math.Floor(weekendSlotsPerShift);
                maxPerShiftWeekend = Math.Max(0, Math.Min(maxPerShift, maxPerShiftWeekend));
            }
            else
            {
                maxPerShiftWeekend = 0;
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
                bool isWeekendKey = date.DayOfWeek == DayOfWeek.Saturday || 
                                  date.DayOfWeek == DayOfWeek.Sunday || 
                                  holidayDates.Contains(date.Date);
                int targetMax = isWeekendKey ? maxPerShiftWeekend : maxPerShiftWeekday;

                var dayEntry = new ScheduleEntry(date, "Ngày");
                var ca2Entry = new ScheduleEntry(date, "Ca2");
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
                                bool isWeekend = day.DayOfWeek == DayOfWeek.Saturday || 
                                                 day.DayOfWeek == DayOfWeek.Sunday || 
                                                 holidayDates.Contains(day.Date);
                                if (day.DayOfWeek == DayOfWeek.Saturday && saturdayWorking)
                                {
                                    isWeekend = false;
                                }
                                hoursToAdd += isWeekend ? 12.0 : 4.0;
                            }
                        }
                        shiftHours[picked.Id] += hoursToAdd;

                        return picked.Id;
                    }
                    return null;
                }

                // Ca 2: Điền tất cả nhân viên xoay ca về Ca 2
                var ca2Employees = availableToday
                    .Where(e => (e.Role == EmployeeRole.Worker || e.Role == EmployeeRole.NewWorker) 
                                && GetShiftRotationForWeek(e.Id, date) == 2)
                    .Select(e => e.Id)
                    .ToList();
                ca2Entry.EmployeeIds.AddRange(ca2Employees);
                foreach (var empId in ca2Employees)
                {
                    pickedToday.Add(empId);
                }

                // Ca Ngày: lọc chỉ lấy Leader, Tech, hoặc Operator xoay ca == 0 (Ca 1/Ngày)
                var poolDay = availableToday.Where(e => 
                    e.Role == EmployeeRole.Leader || 
                    e.Role == EmployeeRole.Technician || 
                    ((e.Role == EmployeeRole.Worker || e.Role == EmployeeRole.NewWorker) && GetShiftRotationForWeek(e.Id, date) == 0)
                ).ToList();

                while (dayEntry.EmployeeIds.Count < targetMax)
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

                while (nightEntry.EmployeeIds.Count < targetMax)
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
                schedule.Add(ca2Entry);
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
