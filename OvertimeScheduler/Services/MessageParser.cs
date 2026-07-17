using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using OvertimeScheduler.Models;

namespace OvertimeScheduler.Services
{
    public class MessageParser
    {
        public static (Employee employee, DateTime date)? ParseLeaveMessage(string messageText, List<Employee> employees, DateTime weekStart)
        {
            if (string.IsNullOrEmpty(messageText)) return null;

            string normalized = RemoveSign4VietnameseString(messageText).ToLower();

            // Kiểm tra các từ khóa báo nghỉ hoặc bận
            bool isLeaveMsg = normalized.Contains("nghi") || normalized.Contains("ban") || normalized.Contains("xin phep");
            if (!isLeaveMsg) return null;

            // 1. Tìm nhân viên
            Employee matchedEmployee = null;
            foreach (var emp in employees)
            {
                string empId = emp.Id.ToLower();
                string empName = RemoveSign4VietnameseString(emp.Name).ToLower();
                
                // Trích xuất tên viết tắt hoặc tên ngắn (ví dụ: "Nguyen Van An" -> "an")
                string[] nameParts = empName.Split(' ');
                string shortName = nameParts[nameParts.Length - 1]; // "an"

                if (normalized.Contains(empId) || normalized.Contains(empName) || 
                    (normalized.Contains(shortName) && shortName.Length > 1))
                {
                    matchedEmployee = emp;
                    break;
                }
            }

            if (matchedEmployee == null) return null;

            // 2. Tìm thứ/ngày nghỉ
            DateTime? leaveDate = null;
            
            // Tìm các thứ trong tuần
            if (normalized.Contains("thu hai") || normalized.Contains("thu 2") || normalized.Contains("t2"))
                leaveDate = GetDateOfDayOfWeek(weekStart, DayOfWeek.Monday);
            else if (normalized.Contains("thu ba") || normalized.Contains("thu 3") || normalized.Contains("t3"))
                leaveDate = GetDateOfDayOfWeek(weekStart, DayOfWeek.Tuesday);
            else if (normalized.Contains("thu tu") || normalized.Contains("thu 4") || normalized.Contains("t4"))
                leaveDate = GetDateOfDayOfWeek(weekStart, DayOfWeek.Wednesday);
            else if (normalized.Contains("thu nam") || normalized.Contains("thu 5") || normalized.Contains("t5"))
                leaveDate = GetDateOfDayOfWeek(weekStart, DayOfWeek.Thursday);
            else if (normalized.Contains("thu sau") || normalized.Contains("thu 6") || normalized.Contains("t6"))
                leaveDate = GetDateOfDayOfWeek(weekStart, DayOfWeek.Friday);
            else if (normalized.Contains("thu bay") || normalized.Contains("thu 7") || normalized.Contains("t7"))
                leaveDate = GetDateOfDayOfWeek(weekStart, DayOfWeek.Saturday);
            else if (normalized.Contains("chu nhat") || normalized.Contains("cn"))
                leaveDate = GetDateOfDayOfWeek(weekStart, DayOfWeek.Sunday);

            if (leaveDate.HasValue)
            {
                return (matchedEmployee, leaveDate.Value);
            }

            return null;
        }

        private static DateTime GetDateOfDayOfWeek(DateTime startOfWeek, DayOfWeek day)
        {
            int current = (int)startOfWeek.DayOfWeek;
            if (current == 0) current = 7; // coi CN là ngày 7
            
            int target = (int)day;
            if (target == 0) target = 7;

            return startOfWeek.AddDays(target - current);
        }

        public static string RemoveSign4VietnameseString(string str)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
                "đ",
                "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
                "í","ì","ỉ","ĩ","ị",
                "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
                "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
                "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
                "d",
                "e","e","e","e","e","e","e","e","e","e","e",
                "i","i","i","i","i",
                "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
                "u","u","u","u","u","u","u","u","u","u","u",
                "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                str = str.Replace(arr1[i], arr2[i]);
                str = str.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return str;
        }
    }
}
