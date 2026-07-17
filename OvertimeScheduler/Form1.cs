using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using OvertimeScheduler.Models;
using OvertimeScheduler.Services;
using OvertimeScheduler.Forms;

namespace OvertimeScheduler
{
    public partial class Form1 : Form
    {
        private List<Employee> _employees;
        private List<ScheduleEntry> _schedule;
        private ZaloBotService _zaloBot;
        private DateTime _weekStart;
        private DateTime _weekEnd;
        private bool _saturdayWorking = false;
        private List<CompanyHoliday> _companyHolidays;

        public Form1()
        {
            InitializeComponent();
            _employees = Employee.LoadEmployeesFromExcel();
            _schedule = new List<ScheduleEntry>();
            _zaloBot = new ZaloBotService();
            _companyHolidays = new List<CompanyHoliday>();
            
            _zaloBot.OnLogMessage += ZaloBot_OnLogMessage;
            _zaloBot.OnLeaveRequestReceived += ZaloBot_OnLeaveRequestReceived;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Tự động khởi tạo toàn bộ ngày nghỉ lễ & nghỉ định kỳ 2026 của công ty IRISO VN (màu hồng trên lịch)
            InitializeIrisoHolidays2026();
            RefreshHolidaysUI();

            DateTime today = DateTime.Today;
            int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
            _weekStart = today.AddDays(-1 * diff).Date;
            _weekEnd = _weekStart.AddDays(6).Date;

            dtpFrom.Value = _weekStart;
            dtpTo.Value = _weekEnd;

            InitDateComboBox();
            RunAutoSchedule();
        }

        private void InitializeIrisoHolidays2026()
        {
            _companyHolidays.Clear();
            
            DateTime start = new DateTime(2026, 1, 1);
            DateTime end = new DateTime(2026, 12, 31);
            
            for (DateTime curr = start; curr <= end; curr = curr.AddDays(1))
            {
                bool isSunday = (curr.DayOfWeek == DayOfWeek.Sunday);
                bool isSaturday = (curr.DayOfWeek == DayOfWeek.Saturday);
                bool isOff = false;
                string holidayName = "";

                if (isSunday)
                {
                    isOff = true;
                    holidayName = "Chủ Nhật";
                }
                else if (isSaturday)
                {
                    int day = curr.Day;
                    if (curr.Month == 1)
                    {
                        if (day == 3)
                        {
                            isOff = true;
                            holidayName = "Nghỉ Thứ Bảy (Lịch IRISO)";
                        }
                    }
                    else
                    {
                        if (day <= 7)
                        {
                            // Thứ 7 đầu tiên của tháng 2-12 là ngày đi làm (màu trắng)
                            isOff = false;
                        }
                        else
                        {
                            // Các Thứ 7 còn lại là ngày nghỉ (màu hồng)
                            isOff = true;
                            holidayName = "Nghỉ Thứ Bảy (Lịch IRISO)";
                        }
                    }
                }

                // Lễ đặc biệt và lịch nghỉ công ty bổ sung
                int m = curr.Month;
                int d = curr.Day;

                if (m == 1 && d == 2)
                {
                    isOff = true;
                    holidayName = "Nghỉ bù / Nghỉ thêm IRISO";
                }
                else if (m == 2 && (d >= 16 && d <= 20))
                {
                    isOff = true;
                    holidayName = $"Tết Nguyên Đán (Mùng {d-15} Tết)";
                }
                else if (m == 4 && d == 29)
                {
                    isOff = true;
                    holidayName = "Giỗ tổ Hùng Vương (Nghỉ lễ)";
                }
                else if (m == 4 && d == 30)
                {
                    isOff = true;
                    holidayName = "Giải phóng Miền Nam";
                }
                else if (m == 5 && d == 1)
                {
                    isOff = true;
                    holidayName = "Quốc tế Lao động";
                }
                else if (m == 9 && d == 1)
                {
                    isOff = true;
                    holidayName = "Nghỉ Quốc khánh";
                }
                else if (m == 9 && d == 2)
                {
                    isOff = true;
                    holidayName = "Quốc khánh";
                }
                else if (m == 12 && d == 30)
                {
                    isOff = true;
                    holidayName = "Nghỉ tết sớm IRISO";
                }
                else if (m == 12 && d == 31)
                {
                    isOff = true;
                    holidayName = "Nghỉ Tết Dương Lịch";
                }

                if (isOff)
                {
                    if (!_companyHolidays.Any(h => h.Date == curr.Date))
                    {
                        _companyHolidays.Add(new CompanyHoliday(curr, holidayName));
                    }
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_zaloBot != null)
            {
                _zaloBot.StopBot();
            }
        }

        #region Khởi tạo & Cập nhật Dữ liệu
        private DateTime GetMonday(DateTime date)
        {
            int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return date.AddDays(-1 * diff).Date;
        }

        private DateTime GetScheduleKey(DateTime date)
        {
            var holidayDates = _companyHolidays.Select(h => h.Date).ToList();
            return SchedulerEngine.GetScheduleKey(date, _saturdayWorking, holidayDates);
        }

        private void InitDateComboBox()
        {
            cbActiveDay.SelectedIndexChanged -= cbActiveDay_SelectedIndexChanged;
            cbActiveDay.Items.Clear();

            DateTime monday = GetMonday(dtpFrom.Value);
            var holidayDates = _companyHolidays.Select(h => h.Date).ToList();

            // 1. Ngày thường: T2 - T6 (loại trừ các ngày lễ ra riêng)
            var weekdayList = new List<DateTime>();
            for (int i = 0; i < 5; i++)
            {
                DateTime d = monday.AddDays(i);
                if (holidayDates.Contains(d)) continue;
                weekdayList.Add(d);
            }

            if (weekdayList.Count > 0)
            {
                cbActiveDay.Items.Add(new DateItem(monday, $"Ngày Thường (T2 - T6: {weekdayList.First():dd/MM} - {weekdayList.Last():dd/MM})"));
            }

            // 2. Thứ 7 (nếu không làm thường và không phải ngày lễ)
            DateTime saturday = monday.AddDays(5);
            if (!_saturdayWorking)
            {
                if (holidayDates.Contains(saturday))
                {
                    var h = _companyHolidays.First(x => x.Date == saturday);
                    cbActiveDay.Items.Add(new DateItem(saturday, $"Ngày Lễ ({h.Name}: {saturday:dd/MM})"));
                }
                else
                {
                    cbActiveDay.Items.Add(new DateItem(saturday, $"Thứ Bảy (Tăng ca: {saturday:dd/MM})"));
                }
            }

            // 3. Chủ Nhật
            DateTime sunday = monday.AddDays(6);
            if (holidayDates.Contains(sunday))
            {
                var h = _companyHolidays.First(x => x.Date == sunday);
                cbActiveDay.Items.Add(new DateItem(sunday, $"Ngày Lễ ({h.Name}: {sunday:dd/MM})"));
            }
            else
            {
                cbActiveDay.Items.Add(new DateItem(sunday, $"Chủ Nhật (Tăng ca: {sunday:dd/MM})"));
            }

            // 4. Các ngày lễ khác trong tuần
            for (int i = 0; i < 5; i++)
            {
                DateTime d = monday.AddDays(i);
                if (holidayDates.Contains(d))
                {
                    var h = _companyHolidays.First(x => x.Date == d);
                    cbActiveDay.Items.Add(new DateItem(d, $"Ngày Lễ ({h.Name}: {d:dd/MM})"));
                }
            }

            if (cbActiveDay.Items.Count > 0)
            {
                cbActiveDay.SelectedIndex = 0;
            }
            cbActiveDay.SelectedIndexChanged += cbActiveDay_SelectedIndexChanged;
        }

        private void RunAutoSchedule()
        {
            DateTime start = dtpFrom.Value.Date;
            DateTime end = dtpTo.Value.Date;

            if (start > end) return;

            int maxPerShift = (int)numMaxPerShift.Value;
            var holidayDates = _companyHolidays.Select(h => h.Date).ToList();

            _schedule = SchedulerEngine.AutoSchedule(_employees, start, end, _saturdayWorking, maxPerShift, holidayDates);
            RedrawActiveDay();
            Log("System", "Đã tự động xếp lịch tuần mới cho 3 ca.");
        }

        private void RedrawActiveDay()
        {
            if (cbActiveDay.SelectedItem == null) return;
            DateTime activeDate = ((DateItem)cbActiveDay.SelectedItem).Date;

            flowEmployeePool.Controls.Clear();
            flowDayShift.Controls.Clear();
            flowNightShift.Controls.Clear();
            flowAdminShift.Controls.Clear();

            var dayShiftIds = GetAssignedIds(activeDate, "Ngày");
            var nightShiftIds = GetAssignedIds(activeDate, "Đêm");
            var adminShiftIds = GetAssignedIds(activeDate, "Hành chính");

            foreach (var empId in dayShiftIds)
            {
                var emp = _employees.FirstOrDefault(e => e.Id == empId);
                if (emp != null) flowDayShift.Controls.Add(CreateEmployeeCard(emp, "Ngày"));
            }

            foreach (var empId in nightShiftIds)
            {
                var emp = _employees.FirstOrDefault(e => e.Id == empId);
                if (emp != null) flowNightShift.Controls.Add(CreateEmployeeCard(emp, "Đêm"));
            }

            foreach (var empId in adminShiftIds)
            {
                var emp = _employees.FirstOrDefault(e => e.Id == empId);
                if (emp != null) flowAdminShift.Controls.Add(CreateEmployeeCard(emp, "Hành chính"));
            }

            string keyword = txtSearch.Text.Trim().ToLower();
            var assignedAllToday = dayShiftIds.Concat(nightShiftIds).Concat(adminShiftIds).ToHashSet();

            int poolCount = 0;
            foreach (var emp in _employees)
            {
                bool isOnLeave = emp.LeavePeriods.Any(lp => activeDate >= lp.StartDate && activeDate <= lp.EndDate);
                bool matchesSearch = string.IsNullOrEmpty(keyword) || 
                                    emp.Name.ToLower().Contains(keyword) || 
                                    emp.Id.ToLower().Contains(keyword);

                if (!assignedAllToday.Contains(emp.Id) && matchesSearch && !isOnLeave)
                {
                    flowEmployeePool.Controls.Add(CreateEmployeeCard(emp, "Pool"));
                    poolCount++;
                    if (poolCount >= 50) break;
                }
            }

            UpdateBudgetStatusAndChart();
        }

        private List<string> GetAssignedIds(DateTime date, string shiftName)
        {
            DateTime keyDate = GetScheduleKey(date);
            if (keyDate == DateTime.MinValue) return new List<string>();
            var entry = _schedule.FirstOrDefault(s => s.Date == keyDate && s.ShiftName == shiftName);
            return entry != null ? entry.EmployeeIds : new List<string>();
        }

        private void UpdateBudgetStatusAndChart()
        {
            DateTime start = dtpFrom.Value.Date;
            DateTime end = dtpTo.Value.Date;

            double totalScheduled = 0;
            var empHours = _employees.ToDictionary(e => e.Id, e => 0.0);

            foreach (var emp in _employees)
            {
                foreach (var kvp in emp.FixedOvertimeHours)
                {
                    if (kvp.Key >= start && kvp.Key <= end)
                    {
                        empHours[emp.Id] += kvp.Value;
                    }
                }
            }

            for (var date = start; date <= end; date = date.AddDays(1))
            {
                DateTime keyDate = GetScheduleKey(date);
                if (keyDate == DateTime.MinValue) continue;
                
                foreach (var shiftName in new[] { "Ngày", "Đêm", "Hành chính" })
                {
                    var entry = _schedule.FirstOrDefault(s => s.Date == keyDate && s.ShiftName == shiftName);
                    if (entry != null)
                    {
                        foreach (var empId in entry.EmployeeIds)
                        {
                            var emp = _employees.FirstOrDefault(e => e.Id == empId);
                            if (emp != null)
                            {
                                if (!emp.FixedOvertimeHours.ContainsKey(date))
                                {
                                    empHours[empId] += 4.0;
                                }
                            }
                        }
                    }
                }
            }

            totalScheduled = empHours.Values.Sum();

            double budget = (double)numMonthlyBudget.Value;
            double remaining = budget - totalScheduled;

            lblBudgetStatus.Text = $"Đã xếp: {totalScheduled:0} giờ | Còn lại: {remaining:0} giờ";
            if (remaining < 0)
            {
                lblBudgetStatus.ForeColor = Color.Red;
            }
            else
            {
                lblBudgetStatus.ForeColor = Color.FromArgb(46, 125, 50);
            }

            overtimeChart.UpdateData(_employees, _schedule, start, end);
        }

        private Panel CreateEmployeeCard(Employee emp, string location)
        {
            var cardPanel = new Panel
            {
                Size = new Size(245, 55),
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(3, 3, 3, 5),
                Tag = emp.Id
            };

            switch (emp.Role)
            {
                case EmployeeRole.Leader:
                    cardPanel.BackColor = Color.LightSkyBlue;
                    break;
                case EmployeeRole.Technician:
                    cardPanel.BackColor = Color.FromArgb(40, 40, 40);
                    break;
                case EmployeeRole.NewWorker:
                    cardPanel.BackColor = Color.FromArgb(230, 70, 70);
                    break;
                case EmployeeRole.Worker:
                default:
                    cardPanel.BackColor = Color.White;
                    break;
            }

            Color textColor = (emp.Role == EmployeeRole.Technician || emp.Role == EmployeeRole.NewWorker) ? Color.White : Color.Black;

            var lblName = new Label
            {
                Text = $"{emp.Id} - {emp.Name}",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = textColor,
                Location = new Point(5, 8),
                AutoSize = true
            };
            cardPanel.Controls.Add(lblName);

            var lblRole = new Label
            {
                Text = GetRoleNameVietnamese(emp.Role),
                Font = new Font("Segoe UI", 8F, FontStyle.Italic),
                ForeColor = textColor,
                Location = new Point(5, 28),
                AutoSize = true
            };
            cardPanel.Controls.Add(lblRole);

            var btnDelete = new Button
            {
                Text = "X",
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                Size = new Size(24, 20),
                Location = new Point(215, 8),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = textColor
            };
            btnDelete.FlatAppearance.BorderSize = 0;
            
            if (location == "Pool")
            {
                btnDelete.Text = "X";
                btnDelete.Click += (s, ev) =>
                {
                    DateTime activeDate = ((DateItem)cbActiveDay.SelectedItem).Date;
                    emp.LeavePeriods.Add(new LeavePeriod(activeDate, activeDate));
                    Log("System", $"Nhân viên {emp.Name} báo nghỉ vào ngày {activeDate:dd/MM/yyyy}.");
                    
                    RunAutoSchedule();
                };
            }
            else
            {
                btnDelete.Text = "x";
                btnDelete.Click += (s, ev) =>
                {
                    DateTime activeDate = ((DateItem)cbActiveDay.SelectedItem).Date;
                    DateTime keyDate = GetScheduleKey(activeDate);
                    var entry = _schedule.FirstOrDefault(se => se.Date == keyDate && se.ShiftName == location);
                    if (entry != null)
                    {
                        entry.EmployeeIds.Remove(emp.Id);
                    }
                    RedrawActiveDay();
                };
            }
            cardPanel.Controls.Add(btnDelete);

            cardPanel.MouseDown += (s, ev) =>
            {
                if (ev.Button == MouseButtons.Left)
                {
                    cardPanel.DoDragDrop(cardPanel, DragDropEffects.Move);
                }
            };

            cardPanel.DoubleClick += CardPanel_DoubleClick;
            lblName.DoubleClick += (s, ev) => CardPanel_DoubleClick(cardPanel, ev);
            lblRole.DoubleClick += (s, ev) => CardPanel_DoubleClick(cardPanel, ev);

            return cardPanel;
        }

        private void CardPanel_DoubleClick(object sender, EventArgs e)
        {
            Panel card = (Panel)sender;
            string empId = card.Tag as string;
            if (string.IsNullOrEmpty(empId)) return;

            var emp = _employees.FirstOrDefault(e => e.Id == empId);
            if (emp != null)
            {
                using (var editForm = new EmployeeEditForm(emp))
                {
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        Log("System", $"Đã cập nhật cấu hình cho {emp.Name}. Tự động tính toán lại...");
                        RunAutoSchedule();
                    }
                }
            }
        }

        private string GetRoleNameVietnamese(EmployeeRole role)
        {
            switch (role)
            {
                case EmployeeRole.Leader: return "Trưởng nhóm";
                case EmployeeRole.Technician: return "Kỹ thuật";
                case EmployeeRole.NewWorker: return "Công nhân mới";
                case EmployeeRole.Worker: return "Công nhân";
                default: return "";
            }
        }
        #endregion

        #region Điều khiển Giao diện (Event Handlers)
        private void dtpDateRange_ValueChanged(object sender, EventArgs e)
        {
            InitDateComboBox();
            RunAutoSchedule();
        }

        private void cbActiveDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            RedrawActiveDay();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            RedrawActiveDay();
        }

        private void btnAutoSchedule_Click(object sender, EventArgs e)
        {
            RunAutoSchedule();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            DateTime start = dtpFrom.Value.Date;
            DateTime end = dtpTo.Value.Date;

            if (start > end) return;

            try
            {
                var holidayDates = _companyHolidays.Select(h => h.Date).ToList();
                string filePath = ExcelService.ExportToExcel(_employees, _schedule, start, end, _saturdayWorking, holidayDates);
                Log("System", $"Đã xuất file Excel tại: {filePath}");

                if (_zaloBot.IsRunning)
                {
                    _zaloBot.SendExcelFile(filePath);
                }
                else
                {
                    MessageBox.Show($"Đã xuất Excel thành công tại:\n{filePath}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xuất Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnToggleZalo_Click(object sender, EventArgs e)
        {
            if (_zaloBot.IsRunning)
            {
                _zaloBot.StopBot();
                btnToggleZalo.Text = "Bật Zalo Bot";
                btnToggleZalo.BackColor = Color.FromArgb(0, 104, 156);
            }
            else
            {
                string groupName = txtZaloGroupName.Text.Trim();
                if (string.IsNullOrEmpty(groupName))
                {
                    MessageBox.Show("Vui lòng nhập tên nhóm Zalo nhận lịch!", "Lưu ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _zaloBot.StartBot(groupName);
                btnToggleZalo.Text = "Tắt Zalo Bot";
                btnToggleZalo.BackColor = Color.DarkRed;
            }
        }

        private void numMonthlyBudget_ValueChanged(object sender, EventArgs e)
        {
            UpdateBudgetStatusAndChart();
        }

        private void chkSaturdayWorking_CheckedChanged(object sender, EventArgs e)
        {
            _saturdayWorking = chkSaturdayWorking.Checked;
            InitDateComboBox();
            RunAutoSchedule();
        }

        private void numMaxPerShift_ValueChanged(object sender, EventArgs e)
        {
            RunAutoSchedule();
        }
        #endregion

        #region Kéo thả Drag and Drop
        private void flowShift_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Panel)))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void flowShift_DragDrop(object sender, DragEventArgs e)
        {
            if (cbActiveDay.SelectedItem == null) return;
            DateTime activeDate = ((DateItem)cbActiveDay.SelectedItem).Date;
            DateTime keyDate = GetScheduleKey(activeDate);
            if (keyDate == DateTime.MinValue) return;

            Panel targetPanel = (FlowLayoutPanel)sender;
            Panel draggedCard = (Panel)e.Data.GetData(typeof(Panel));
            string employeeId = draggedCard.Tag as string;

            if (string.IsNullOrEmpty(employeeId)) return;

            string targetShift = "";
            if (targetPanel == flowDayShift) targetShift = "Ngày";
            else if (targetPanel == flowNightShift) targetShift = "Đêm";
            else if (targetPanel == flowAdminShift) targetShift = "Hành chính";

            if (string.IsNullOrEmpty(targetShift)) return;

            foreach (var shiftName in new[] { "Ngày", "Đêm", "Hành chính" })
            {
                var entry = _schedule.FirstOrDefault(se => se.Date == keyDate && se.ShiftName == shiftName);
                if (entry != null)
                {
                    entry.EmployeeIds.Remove(employeeId);
                }
            }

            var targetEntry = _schedule.FirstOrDefault(se => se.Date == keyDate && se.ShiftName == targetShift);
            if (targetEntry == null)
            {
                targetEntry = new ScheduleEntry(keyDate, targetShift);
                _schedule.Add(targetEntry);
            }

            if (!targetEntry.EmployeeIds.Contains(employeeId))
            {
                targetEntry.EmployeeIds.Add(employeeId);
            }

            RedrawActiveDay();
        }
        #endregion

        #region Zalo Bot Callbacks
        private void ZaloBot_OnLogMessage(string time, string msg)
        {
            Log("Zalo Bot", msg);
        }

        private void ZaloBot_OnLeaveRequestReceived((Employee employee, DateTime date) request)
        {
            var emp = _employees.FirstOrDefault(e => e.Id == request.employee.Id);
            if (emp != null)
            {
                if (!emp.LeavePeriods.Any(lp => request.date >= lp.StartDate && request.date <= lp.EndDate))
                {
                    emp.LeavePeriods.Add(new LeavePeriod(request.date, request.date));
                    Log("Zalo Bot", $"Tự động cập nhật nghỉ phép cho {emp.Name} vào ngày {request.date:dd/MM/yyyy}.");

                    RunAutoSchedule();

                    DateTime start = dtpFrom.Value.Date;
                    DateTime end = dtpTo.Value.Date;
                    var holidayDates = _companyHolidays.Select(h => h.Date).ToList();
                    string filePath = ExcelService.ExportToExcel(_employees, _schedule, start, end, _saturdayWorking, holidayDates);
                    _zaloBot.SendExcelFile(filePath);
                }
            }
        }

        private void Log(string senderName, string message)
        {
            System.Diagnostics.Debug.WriteLine($"[{DateTime.Now:HH:mm:ss}] [{senderName}] {message}");
        }
        #endregion

        #region Quản lý Ngày lễ Công ty (Live Calendar)
        private void btnAddHoliday_Click(object sender, EventArgs e)
        {
            string name = txtHolidayName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Vui lòng nhập tên ngày nghỉ!", "Lưu ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime selectedDate = mcHolidays.SelectionStart.Date;
            if (_companyHolidays.Any(h => h.Date == selectedDate))
            {
                MessageBox.Show("Ngày này đã đăng ký nghỉ lễ rồi!", "Lưu ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _companyHolidays.Add(new CompanyHoliday(selectedDate, name));
            txtHolidayName.Clear();
            RefreshHolidaysUI();
            
            InitDateComboBox();
            RunAutoSchedule();
        }

        private void btnDeleteHoliday_Click(object sender, EventArgs e)
        {
            if (lstHolidays.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn một ngày nghỉ trong danh sách để xóa!", "Lưu ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CompanyHoliday holiday = (CompanyHoliday)lstHolidays.SelectedItem;
            _companyHolidays.Remove(holiday);
            RefreshHolidaysUI();

            InitDateComboBox();
            RunAutoSchedule();
        }

        private void RefreshHolidaysUI()
        {
            lstHolidays.Items.Clear();
            var sorted = _companyHolidays.OrderBy(h => h.Date).ToList();
            foreach (var h in sorted)
            {
                lstHolidays.Items.Add(h);
            }

            mcHolidays.BoldedDates = _companyHolidays.Select(h => h.Date).ToArray();
            mcHolidays.UpdateBoldedDates();
        }
        #endregion

        private class DateItem
        {
            public DateTime Date { get; }
            public string Label { get; }
            
            public DateItem(DateTime date, string label) 
            { 
                Date = date.Date; 
                Label = label;
            }

            public override string ToString()
            {
                return Label;
            }
        }
    }
}
