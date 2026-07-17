using System;

namespace OvertimeScheduler.Models
{
    public class CompanyHoliday
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }

        public CompanyHoliday(DateTime date, string name)
        {
            Date = date.Date;
            Name = name;
        }

        public override string ToString()
        {
            return $"{Date:dd/MM/yyyy} - {Name}";
        }
    }
}
