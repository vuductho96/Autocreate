using System;
using System.Collections.Generic;

namespace OvertimeScheduler.Models
{
    public class ScheduleEntry
    {
        public DateTime Date { get; set; }
        public string ShiftName { get; set; } // "Ngày", "Đêm", "Hành chính"
        public List<string> EmployeeIds { get; set; } = new List<string>();

        public ScheduleEntry() { }

        public ScheduleEntry(DateTime date, string shiftName)
        {
            Date = date.Date;
            ShiftName = shiftName;
        }
    }
}
