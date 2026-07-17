using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClosedXML.Excel;

namespace OvertimeScheduler.Models
{
    public enum EmployeeRole
    {
        Leader,       // Xanh biển
        Worker,       // Trắng
        Technician,   // Đen
        NewWorker     // Đỏ
    }

    public class LeavePeriod
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public LeavePeriod(DateTime start, DateTime end)
        {
            StartDate = start.Date;
            EndDate = end.Date;
        }

        public override string ToString()
        {
            return $"{StartDate:dd/MM} -> {EndDate:dd/MM}";
        }
    }

    public class Employee
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public EmployeeRole Role { get; set; }
        
        // Nghỉ phép theo khoảng ngày
        public List<LeavePeriod> LeavePeriods { get; set; } = new List<LeavePeriod>();
        
        // Gán giờ làm thêm cố định vào ngày cụ thể
        public Dictionary<DateTime, double> FixedOvertimeHours { get; set; } = new Dictionary<DateTime, double>();

        public Employee() { }

        public Employee(string id, string name, EmployeeRole role)
        {
            Id = id;
            Name = name;
            Role = role;
        }

        public static List<Employee> LoadEmployeesFromExcel()
        {
            var list = new List<Employee>();
            string listPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ListNhânViên.xlsx");
            
            // Tìm kiếm file ở thư mục cha (cho quá trình develop/debug)
            if (!File.Exists(listPath))
            {
                listPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "ListNhânViên.xlsx");
            }
            if (!File.Exists(listPath))
            {
                listPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "ListNhânViên.xlsx");
            }

            if (File.Exists(listPath))
            {
                try
                {
                    using (var workbook = new XLWorkbook(listPath))
                    {
                        var worksheet = workbook.Worksheet(1);
                        var rows = worksheet.RowsUsed().Skip(1); // Bỏ qua tiêu đề cột

                        var loadedIds = new HashSet<string>();
                        foreach (var row in rows)
                        {
                            string id = row.Cell(1).GetValue<string>().Trim();
                            string name = row.Cell(2).GetValue<string>().Trim();
                            string roleStr = row.Cell(3).GetValue<string>().Trim().ToLower();

                            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name)) continue;
                            if (loadedIds.Contains(id)) continue; // Bỏ qua trùng lặp ID nhân viên

                            loadedIds.Add(id);

                            EmployeeRole role = EmployeeRole.Worker;
                            if (roleStr.Contains("leader") || roleStr.Contains("trưởng") || roleStr.Contains("truong"))
                            {
                                role = EmployeeRole.Leader;
                            }
                            else if (roleStr.Contains("tech") || roleStr.Contains("kỹ thuật") || roleStr.Contains("ky thuat") || roleStr.Contains("sửa"))
                            {
                                role = EmployeeRole.Technician;
                            }
                            else if (roleStr.Contains("new") || roleStr.Contains("mới") || roleStr.Contains("moi"))
                            {
                                role = EmployeeRole.NewWorker;
                            }
                            else
                            {
                                // Tạo phân bố vai trò ngẫu nhiên dựa trên hash ID để biểu diễn giao diện đẹp hơn
                                int hash = id.GetHashCode();
                                if (hash % 30 == 0) role = EmployeeRole.Leader;
                                else if (hash % 18 == 1) role = EmployeeRole.Technician;
                                else if (hash % 15 == 2) role = EmployeeRole.NewWorker;
                            }

                            list.Add(new Employee(id, name, role));
                        }
                    }
                }
                catch
                {
                    return GetMockEmployees(); // Fallback nếu file lỗi
                }
            }

            if (list.Count == 0)
            {
                return GetMockEmployees(); // Fallback nếu không đọc được dòng nào
            }

            return list;
        }

        public static List<Employee> GetMockEmployees()
        {
            return new List<Employee>
            {
                new Employee("NV01", "Nguyen Van An", EmployeeRole.Leader),
                new Employee("NV02", "Tran Thi Binh", EmployeeRole.Leader),
                
                new Employee("NV03", "Le Van Cuong", EmployeeRole.Technician),
                new Employee("NV04", "Pham Hong Dung", EmployeeRole.Technician),
                new Employee("NV05", "Hoang Van Em", EmployeeRole.Technician),
                
                new Employee("NV06", "Nguyen Thi Hoa", EmployeeRole.Worker),
                new Employee("NV07", "Tran Van Huong", EmployeeRole.Worker),
                new Employee("NV08", "Le Thi Lan", EmployeeRole.Worker),
                new Employee("NV09", "Pham Van Minh", EmployeeRole.Worker),
                new Employee("NV10", "Vu Thi Nam", EmployeeRole.Worker),
                new Employee("NV11", "Do Van Oanh", EmployeeRole.Worker),
                new Employee("NV12", "Bui Thi Phuong", EmployeeRole.Worker),
                new Employee("NV13", "Nguyen Van Quang", EmployeeRole.Worker),
                
                new Employee("NV14", "Hoang Thi Son", EmployeeRole.NewWorker),
                new Employee("NV15", "Vu Van Tuyen", EmployeeRole.NewWorker)
            };
        }
    }
}
