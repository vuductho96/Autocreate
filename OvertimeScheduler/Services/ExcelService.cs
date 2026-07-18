using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using OvertimeScheduler.Models;

namespace OvertimeScheduler.Services
{
    public class ExcelService
    {
        public static string ExportToExcel(
            List<Employee> employees, 
            List<ScheduleEntry> schedule, 
            DateTime fromDate, 
            DateTime toDate, 
            bool saturdayWorking = false,
            List<DateTime> holidays = null)
        {
            string outputDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Outputs");
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            string filename = $"Lich_Tuan_{fromDate:ddMMyyyy}_{toDate:ddMMyyyy}.xlsx";
            string filePath = Path.Combine(outputDir, filename);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Lich_Tuan");
                worksheet.ShowGridLines = true;

                DateTime monday = GetMonday(fromDate);
                int currentRow = 1;

                // 1. Tạo khối Ngày thường
                var weekdayList = new List<DateTime>();
                for (int i = 0; i < 5; i++)
                {
                    DateTime d = monday.AddDays(i);
                    if (d >= fromDate.Date && d <= toDate.Date && !(holidays != null && holidays.Contains(d)))
                    {
                        weekdayList.Add(d);
                    }
                }

                if (weekdayList.Count > 0)
                {
                    DateTime startW = weekdayList.First();
                    DateTime endW = weekdayList.Last();
                    string weekdayTitle = startW.Date == endW.Date 
                        ? $"LỊCH LÀM VIỆC  NGÀY {startW:dd/M/yyyy}"
                        : $"LỊCH LÀM VIỆC  NGÀY {startW:dd} ~ {endW:dd/M/yyyy}";
                    WriteDayBlock(worksheet, ref currentRow, weekdayTitle, monday, employees, schedule, fromDate, toDate, saturdayWorking, holidays);
                }

                // 2. Tạo các khối Ngày nghỉ đỏ (Thứ Bảy, Chủ Nhật, Ngày Lễ...) trong khoảng [fromDate, toDate] theo trình tự thời gian
                for (DateTime d = fromDate.Date; d <= toDate.Date; d = d.AddDays(1))
                {
                    if (holidays != null && holidays.Contains(d))
                    {
                        if (d.DayOfWeek == DayOfWeek.Saturday && saturdayWorking) continue;

                        currentRow += 2;
                        string holidayTitle = $"LỊCH TĂNG CA  NGÀY {d:dd/M/yyyy}";
                        WriteDayBlock(worksheet, ref currentRow, holidayTitle, d, employees, schedule, fromDate, toDate, saturdayWorking, holidays);
                    }
                }

                // Auto-fit columns
                worksheet.Columns().AdjustToContents();
                workbook.SaveAs(filePath);
            }

            return filePath;
        }

        private static void WriteDayBlock(
            IXLWorksheet ws, 
            ref int startRow, 
            string title, 
            DateTime queryDate, 
            List<Employee> employees, 
            List<ScheduleEntry> schedule,
            DateTime fromDate,
            DateTime toDate,
            bool saturdayWorking,
            List<DateTime> holidays)
        {
            int rTitle = startRow;
            int rHeader = startRow + 1;
            int rDataStart = startRow + 2;

            // 1. Ghi Title (Màu vàng)
            ws.Cell(rTitle, 1).Value = title;
            ws.Cell(rTitle, 1).Style.Font.Bold = true;
            ws.Cell(rTitle, 1).Style.Font.FontSize = 12;
            ws.Cell(rTitle, 1).Style.Font.FontColor = XLColor.Black;
            ws.Cell(rTitle, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell(rTitle, 1).Style.Fill.BackgroundColor = XLColor.FromHtml("#ffff00"); // Yellow
            ws.Range(rTitle, 1, rTitle, 8).Merge();

            // 2. Ghi Headers ca trực (Toàn bộ chữ ĐEN)
            // CA 1 (Ngày) - Columns A-B (1-2)
            ws.Cell(rHeader, 1).Value = "CA 1 (6:00~14:00)";
            ws.Range(rHeader, 1, rHeader, 2).Merge();
            ws.Range(rHeader, 1, rHeader, 2).Style.Fill.BackgroundColor = XLColor.FromHtml("#00b0f0"); // Blue
            ws.Range(rHeader, 1, rHeader, 2).Style.Font.Bold = true;
            ws.Range(rHeader, 1, rHeader, 2).Style.Font.FontColor = XLColor.Black;

            // CA 2 (Về ca) - Columns C-D (3-4)
            ws.Cell(rHeader, 3).Value = "CA 2 (14:00~22:00)";
            ws.Range(rHeader, 3, rHeader, 4).Merge();
            ws.Range(rHeader, 3, rHeader, 4).Style.Fill.BackgroundColor = XLColor.FromHtml("#f2f2f2"); // Light Gray
            ws.Range(rHeader, 3, rHeader, 4).Style.Font.Bold = true;
            ws.Range(rHeader, 3, rHeader, 4).Style.Font.FontColor = XLColor.Black;

            // CA 3 (Đêm) - Columns E-F (5-6)
            ws.Cell(rHeader, 5).Value = "CA 3 (22:00~6:00)";
            ws.Range(rHeader, 5, rHeader, 6).Merge();
            ws.Range(rHeader, 5, rHeader, 6).Style.Fill.BackgroundColor = XLColor.FromHtml("#ffc000"); // Orange
            ws.Range(rHeader, 5, rHeader, 6).Style.Font.Bold = true;
            ws.Range(rHeader, 5, rHeader, 6).Style.Font.FontColor = XLColor.Black;

            // HÀNH CHÍNH - Columns G-H (7-8)
            ws.Cell(rHeader, 7).Value = "HÀNH CHÍNH";
            ws.Range(rHeader, 7, rHeader, 8).Merge();
            ws.Range(rHeader, 7, rHeader, 8).Style.Fill.BackgroundColor = XLColor.FromHtml("#92d050"); // Light Green
            ws.Range(rHeader, 7, rHeader, 8).Style.Font.Bold = true;
            ws.Range(rHeader, 7, rHeader, 8).Style.Font.FontColor = XLColor.Black;

            // Định dạng headers
            var headerRange = ws.Range(rHeader, 1, rHeader, 8);
            headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            headerRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            // 3. Lấy dữ liệu nhân sự xếp vào các ca
            DateTime keyDate = GetScheduleKey(queryDate, saturdayWorking, holidays);
            var dayIds = schedule.FirstOrDefault(s => s.Date == keyDate && s.ShiftName == "Ngày")?.EmployeeIds ?? new List<string>();
            var ca2Ids = schedule.FirstOrDefault(s => s.Date == keyDate && s.ShiftName == "Ca2")?.EmployeeIds ?? new List<string>();
            var nightIds = schedule.FirstOrDefault(s => s.Date == keyDate && s.ShiftName == "Đêm")?.EmployeeIds ?? new List<string>();
            var adminIds = schedule.FirstOrDefault(s => s.Date == keyDate && s.ShiftName == "Hành chính")?.EmployeeIds ?? new List<string>();

            int maxRows = Math.Max(dayIds.Count, Math.Max(nightIds.Count, Math.Max(adminIds.Count, ca2Ids.Count)));
            if (maxRows == 0) maxRows = 1; // Tạo ít nhất 1 dòng trống để kéo lưới

            for (int i = 0; i < maxRows; i++)
            {
                int r = rDataStart + i;

                // CA 1 (Ngày)
                if (i < dayIds.Count)
                {
                    var emp = employees.FirstOrDefault(e => e.Id == dayIds[i]);
                    if (emp != null)
                    {
                        ws.Cell(r, 1).Value = emp.Id;
                        ws.Cell(r, 2).Value = FormatEmployeeNameExcel(emp, queryDate, "Ngày", holidays);
                    }
                }

                // CA 2 (Về ca)
                if (i < ca2Ids.Count)
                {
                    var emp = employees.FirstOrDefault(e => e.Id == ca2Ids[i]);
                    if (emp != null)
                    {
                        ws.Cell(r, 3).Value = emp.Id;
                        ws.Cell(r, 4).Value = FormatEmployeeNameExcel(emp, queryDate, "Ca2", holidays);
                    }
                }

                // CA 3 (Đêm)
                if (i < nightIds.Count)
                {
                    var emp = employees.FirstOrDefault(e => e.Id == nightIds[i]);
                    if (emp != null)
                    {
                        ws.Cell(r, 5).Value = emp.Id;
                        ws.Cell(r, 6).Value = FormatEmployeeNameExcel(emp, queryDate, "Đêm", holidays);
                    }
                }

                // HÀNH CHÍNH
                if (i < adminIds.Count)
                {
                    var emp = employees.FirstOrDefault(e => e.Id == adminIds[i]);
                    if (emp != null)
                    {
                        ws.Cell(r, 7).Value = emp.Id;
                        ws.Cell(r, 8).Value = FormatEmployeeNameExcel(emp, queryDate, "Hành chính", holidays);
                    }
                }

                // Định dạng borders và màu chữ đen cho dòng data
                var dataRowRange = ws.Range(r, 1, r, 8);
                dataRowRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                dataRowRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                dataRowRange.Style.Font.FontColor = XLColor.Black;
                
                // Align IDs to center
                ws.Cell(r, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell(r, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell(r, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell(r, 7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }

            startRow = rDataStart + maxRows - 1;
        }

        private static string FormatEmployeeNameExcel(Employee emp, DateTime date, string shiftName, List<DateTime> holidays)
        {
            DateTime blockStart = date.Date;
            DateTime blockEnd = date.Date;

            if (date.DayOfWeek == DayOfWeek.Monday)
            {
                blockEnd = date.AddDays(4).Date;
            }

            var overlappingLeaves = emp.LeavePeriods
                .Where(lp => lp.StartDate <= blockEnd && lp.EndDate >= blockStart)
                .ToList();

            string noteStr = "";
            if (overlappingLeaves.Count > 0)
            {
                var notes = overlappingLeaves
                    .Select(lp => string.IsNullOrEmpty(lp.Note) ? $"{lp.StartDate:dd/MM}-{lp.EndDate:dd/MM} nghỉ" : lp.Note)
                    .Distinct();
                noteStr = " (" + string.Join(", ", notes) + ")";
            }

            string otNote = GetEmployeeOvertimeNote(emp, date, shiftName, holidays);
            return $"{emp.Name.ToUpper()}{otNote}{noteStr}";
        }

        private static string GetEmployeeOvertimeNote(Employee emp, DateTime activeDate, string location, List<DateTime> holidays)
        {
            if (location != "Ngày" && location != "Đêm") return "";

            bool isWeekendOrHoliday = activeDate.DayOfWeek == DayOfWeek.Saturday || 
                                     activeDate.DayOfWeek == DayOfWeek.Sunday || 
                                     (holidays != null && holidays.Any(h => h.Date == activeDate.Date));

            double otHours = 0;
            if (emp.FixedOvertimeHours.ContainsKey(activeDate))
            {
                otHours = emp.FixedOvertimeHours[activeDate];
            }
            else
            {
                otHours = isWeekendOrHoliday ? 12.0 : 4.0; // default
            }

            if (otHours <= 0) return "";

            if (!isWeekendOrHoliday)
            {
                if (location == "Ngày")
                {
                    int endTime = 14 + (int)Math.Round(otHours);
                    return $" ({endTime}h)";
                }
                else if (location == "Đêm")
                {
                    int startTime = 22 - (int)Math.Round(otHours);
                    if (startTime < 0) startTime += 24;
                    return $" ({startTime}h-6h)";
                }
            }
            else
            {
                if (location == "Ngày")
                {
                    int endTime = 6 + (int)Math.Round(otHours);
                    if (endTime > 24) endTime -= 24;
                    return $" ({endTime}h)";
                }
                else if (location == "Đêm")
                {
                    int startTime = 6 - (int)Math.Round(otHours);
                    while (startTime <= 0) startTime += 24;
                    return $" ({startTime}h-6h)";
                }
            }

            return "";
        }

        private static DateTime GetMonday(DateTime date)
        {
            int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return date.AddDays(-1 * diff).Date;
        }

        private static DateTime GetScheduleKey(DateTime date, bool saturdayWorking, List<DateTime> holidays)
        {
            if (holidays != null && holidays.Contains(date.Date))
            {
                return date.Date;
            }
            if (date.DayOfWeek == DayOfWeek.Sunday)
            {
                return date.Date;
            }
            if (date.DayOfWeek == DayOfWeek.Saturday)
            {
                return saturdayWorking ? DateTime.MinValue : date.Date;
            }
            return GetMonday(date);
        }
    }
}
