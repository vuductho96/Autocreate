using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
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

        private List<CompanyHoliday> _companyHolidays;
        private DateTime _currentCalendarMonth;
        private Button[] _calendarButtons = new Button[42];
        private bool _isInitializing = true;
        
        // Đường dẫn file lưu lịch
        private static readonly string ScheduleSaveFile = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, "schedule_data.json");

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
            DateTime today = DateTime.Today;
            int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
            _weekStart = today.AddDays(-1 * diff).Date;
            _weekEnd = _weekStart.AddDays(6).Date;

            dtpFrom.Value = _weekStart;
            dtpTo.Value = _weekEnd;

            // Khởi tạo weekday headers cho TableLayoutPanel Lịch
            string[] headers = { "CN", "T2", "T3", "T4", "T5", "T6", "T7" };
            tblCalendar.SuspendLayout();
            for (int i = 0; i < 7; i++)
            {
                var lbl = new Label
                {
                    Text = headers[i],
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    ForeColor = (i == 0 || i == 6) ? Color.Red : Color.Black,
                    BackColor = Color.FromArgb(230, 235, 240)
                };
                tblCalendar.Controls.Add(lbl, i, 0);
            }

            // Khởi tạo sẵn 42 buttons cho lịch để tái sử dụng (Tránh lag do tạo mới control liên tục)
            for (int i = 0; i < 42; i++)
            {
                int col = i % 7;
                int row = (i / 7) + 1;

                var btn = new Button
                {
                    Dock = DockStyle.Fill,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    Margin = new Padding(2)
                };
                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
                btn.Click += CalendarButton_Click;

                _calendarButtons[i] = btn;
                tblCalendar.Controls.Add(btn, col, row);
            }
            tblCalendar.ResumeLayout();

            // Tự động khởi tạo toàn bộ ngày nghỉ lễ & nghỉ định kỳ 2026 của công ty IRISO VN (màu hồng trên lịch)
            InitializeIrisoHolidays2026();
            
            _currentCalendarMonth = new DateTime(_weekStart.Year, _weekStart.Month, 1);
            RefreshHolidaysUI();

            // Load lịch đã lưu trước đó (nếu có)
            LoadSchedule();

            InitDateComboBox();
            
            // Chỉ chạy AutoSchedule nếu chưa có lịch được lưu
            if (_schedule.Count == 0)
            {
                RunAutoSchedule();
            }
            else
            {
                RedrawActiveDay();
            }

            _isInitializing = false;
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
                            isOff = false;
                        }
                        else
                        {
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
            // Lưu lịch khi đóng app
            SaveSchedule();
        }

        // === Lưu & Load lịch xuống file JSON ===
        private void SaveSchedule()
        {
            try
            {
                var data = _schedule.Select(s => new
                {
                    Date = s.Date.ToString("yyyy-MM-dd"),
                    s.ShiftName,
                    s.EmployeeIds
                }).ToList();
                string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(ScheduleSaveFile, json);
            }
            catch { /* Không crash app nếu lỗi ghi file */ }
        }

        private void LoadSchedule()
        {
            try
            {
                if (!File.Exists(ScheduleSaveFile)) return;
                string json = File.ReadAllText(ScheduleSaveFile);
                var raw = JsonSerializer.Deserialize<List<JsonElement>>(json);
                if (raw == null) return;

                _schedule.Clear();
                foreach (var item in raw)
                {
                    if (DateTime.TryParse(item.GetProperty("Date").GetString(), out DateTime dt))
                    {
                        string shiftName = item.GetProperty("ShiftName").GetString() ?? "";
                        var entry = new ScheduleEntry(dt, shiftName);
                        var ids = item.GetProperty("EmployeeIds");
                        foreach (var idEl in ids.EnumerateArray())
                        {
                            string? id = idEl.GetString();
                            if (!string.IsNullOrEmpty(id)) entry.EmployeeIds.Add(id);
                        }
                        _schedule.Add(entry);
                    }
                }
                Log("System", $"Đã nạp lịch đã lưu: {_schedule.Count} ca");
            }
            catch { /* Nếu file hỏng thì bỏ qua, tạo mới */ }
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
            return SchedulerEngine.GetScheduleKey(date, false, holidayDates);
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

            // 2. Thứ 7: Phụ thuộc vào _companyHolidays
            // Nếu có trong danh sách _companyHolidays (màu đỏ) -> là ngày tăng ca
            // Nếu không có (màu trắng) -> là ngày làm thường, không xếp tăng ca
            DateTime saturday = monday.AddDays(5);
            if (holidayDates.Contains(saturday))
            {
                var h = _companyHolidays.First(x => x.Date == saturday);
                cbActiveDay.Items.Add(new DateItem(saturday, $"Thứ Bảy (Tăng ca: {saturday:dd/MM})"));
            }
            else
            {
                // Thứ 7 không có trong lịch cty -> tính là ngày làm thường, không có trong danh sách xếp ca
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

            _schedule = SchedulerEngine.AutoSchedule(_employees, start, end, false, maxPerShift, holidayDates);
            SaveSchedule();
            RedrawActiveDay();
            RefreshHolidaysUI(); // Cập nhật lịch ngày nghỉ để hiện mã NV nghỉ phép
            Log("System", "Đã tự động xếp lịch tuần mới cho 4 ca.");
        }

        private void RedrawActiveDay()
        {
            if (cbActiveDay.SelectedItem == null) return;
            DateTime activeDate = ((DateItem)cbActiveDay.SelectedItem).Date;

            // Suspend layout để tối ưu hóa render hàng loạt control
            flowEmployeePool.SuspendLayout();
            flowDayShift.SuspendLayout();
            flowCa2Shift.SuspendLayout();
            flowNightShift.SuspendLayout();
            flowAdminShift.SuspendLayout();

            flowEmployeePool.Controls.Clear();
            flowDayShift.Controls.Clear();
            flowCa2Shift.Controls.Clear();
            flowNightShift.Controls.Clear();
            flowAdminShift.Controls.Clear();

            var dayShiftIds  = GetAssignedIds(activeDate, "Ngày");
            var ca2ShiftIds  = GetAssignedIds(activeDate, "Ca2");
            var nightShiftIds = GetAssignedIds(activeDate, "Đêm");
            var adminShiftIds = GetAssignedIds(activeDate, "Hành chính");

            foreach (var empId in dayShiftIds)
            {
                var emp = _employees.FirstOrDefault(e => e.Id == empId);
                if (emp != null) flowDayShift.Controls.Add(CreateEmployeeCard(emp, "Ngày"));
            }

            foreach (var empId in ca2ShiftIds)
            {
                var emp = _employees.FirstOrDefault(e => e.Id == empId);
                if (emp != null) flowCa2Shift.Controls.Add(CreateEmployeeCard(emp, "Ca2"));
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
            var assignedAllToday = dayShiftIds.Concat(ca2ShiftIds).Concat(nightShiftIds).Concat(adminShiftIds).ToHashSet();

            // Hiển thị nhân viên nghỉ phép với card vàng + note
            foreach (var emp in _employees)
            {
                if (assignedAllToday.Contains(emp.Id)) continue;
                var leaveToday = emp.LeavePeriods.FirstOrDefault(lp => activeDate >= lp.StartDate && activeDate <= lp.EndDate);
                if (leaveToday == null) continue;

                bool matchesSearch = string.IsNullOrEmpty(keyword) ||
                                    emp.Name.ToLower().Contains(keyword) ||
                                    emp.Id.ToLower().Contains(keyword);
                if (!matchesSearch) continue;

                flowEmployeePool.Controls.Add(CreateEmployeeCard(emp, "Pool", leaveToday));
            }

            int poolCount = 0;
            foreach (var emp in _employees)
            {
                bool isOnLeave = emp.LeavePeriods.Any(lp => activeDate >= lp.StartDate && activeDate <= lp.EndDate);
                bool matchesSearch = string.IsNullOrEmpty(keyword) || 
                                    emp.Name.ToLower().Contains(keyword) || 
                                    emp.Id.ToLower().Contains(keyword);

                if (!assignedAllToday.Contains(emp.Id) && matchesSearch && !isOnLeave)
                {


                    flowEmployeePool.Controls.Add(CreateEmployeeCard(emp, "Pool", null));
                    poolCount++;
                    if (poolCount >= 50) break;
                }
            }

            // Append manual add buttons
            AddManualAddButton(flowDayShift, "Ngày");
            AddManualAddButton(flowCa2Shift, "Ca2");
            AddManualAddButton(flowNightShift, "Đêm");
            AddManualAddButton(flowAdminShift, "Hành chính");

            flowEmployeePool.ResumeLayout();
            flowDayShift.ResumeLayout();
            flowCa2Shift.ResumeLayout();
            flowNightShift.ResumeLayout();
            flowAdminShift.ResumeLayout();

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

        private void AddManualAddButton(FlowLayoutPanel flowPanel, string shiftName)
        {
            var btnAdd = new Button
            {
                Text = "+",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.Red,
                Size = new Size(245, 30),
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(3, 3, 3, 5),
                Cursor = Cursors.Hand,
                Tag = shiftName,
                BackColor = flowPanel.BackColor
            };
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.Click += BtnAddEmployee_Click;

            // Draw dashed border
            btnAdd.Paint += (s, pe) =>
            {
                pe.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (Pen pen = new Pen(Color.FromArgb(180, 180, 180), 1.5F))
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    pe.Graphics.DrawRectangle(pen, 0, 0, btnAdd.Width - 1, btnAdd.Height - 1);
                }
            };

            flowPanel.Controls.Add(btnAdd);
        }

        private void BtnAddEmployee_Click(object sender, EventArgs e)
        {
            if (!(sender is Button btn) || !(btn.Tag is string shiftName)) return;

            if (cbActiveDay.SelectedItem == null) return;
            DateTime activeDate = ((DateItem)cbActiveDay.SelectedItem).Date;
            DateTime keyDate = GetScheduleKey(activeDate);
            if (keyDate == DateTime.MinValue) return;

            var assignedIds = new HashSet<string>();
            var entryDay = _schedule.FirstOrDefault(s => s.Date == keyDate && s.ShiftName == "Ngày");
            var entryCa2 = _schedule.FirstOrDefault(s => s.Date == keyDate && s.ShiftName == "Ca2");
            var entryNight = _schedule.FirstOrDefault(s => s.Date == keyDate && s.ShiftName == "Đêm");
            var entryAdmin = _schedule.FirstOrDefault(s => s.Date == keyDate && s.ShiftName == "Hành chính");

            if (entryDay != null) foreach (var id in entryDay.EmployeeIds) assignedIds.Add(id);
            if (entryCa2 != null) foreach (var id in entryCa2.EmployeeIds) assignedIds.Add(id);
            if (entryNight != null) foreach (var id in entryNight.EmployeeIds) assignedIds.Add(id);
            if (entryAdmin != null) foreach (var id in entryAdmin.EmployeeIds) assignedIds.Add(id);

            var availableEmployees = _employees
                .Where(emp => !assignedIds.Contains(emp.Id) &&
                              (!emp.LeavePeriods.Any(lp => activeDate >= lp.StartDate && activeDate <= lp.EndDate)))
                .Where(emp =>
                {
                    if (emp.Role == EmployeeRole.Leader || emp.Role == EmployeeRole.Technician) return true;
                    int rot = SchedulerEngine.GetShiftRotationForWeek(emp.Id, activeDate);
                    if (shiftName == "Ngày") return rot == 0;
                    if (shiftName == "Ca2") return rot == 2;
                    if (shiftName == "Đêm") return rot == 1;
                    if (shiftName == "Hành chính") return rot != 2;
                    return true;
                })
                .ToList();

            using (var form = new OvertimeScheduler.Forms.SelectEmployeeForm(availableEmployees))
            {
                if (form.ShowDialog() == DialogResult.OK && form.SelectedEmployee != null)
                {
                    var entry = _schedule.FirstOrDefault(s => s.Date == keyDate && s.ShiftName == shiftName);
                    if (entry == null)
                    {
                        entry = new ScheduleEntry { Date = keyDate, ShiftName = shiftName, EmployeeIds = new List<string>() };
                        _schedule.Add(entry);
                    }
                    entry.EmployeeIds.Add(form.SelectedEmployee.Id);
                    SaveSchedule();
                    RedrawActiveDay();
                }
            }
        }

        private Panel CreateEmployeeCard(Employee emp, string location, LeavePeriod? leave = null)
        {
            bool isOnLeave = leave != null;

            var cardPanel = new Panel
            {
                Size = new Size(245, isOnLeave ? 45 : 30),
                BorderStyle = BorderStyle.None,
                Margin = new Padding(3, 3, 3, 5),
                Tag = emp.Id
            };

            Color backColor;
            if (isOnLeave)
            {
                backColor = Color.FromArgb(255, 243, 205);
            }
            else
            {
                switch (emp.Role)
                {
                    case EmployeeRole.Leader: backColor = Color.LightSkyBlue; break;
                    case EmployeeRole.Technician: backColor = Color.FromArgb(40, 40, 40); break;
                    case EmployeeRole.NewWorker: backColor = Color.FromArgb(230, 70, 70); break;
                    case EmployeeRole.Worker:
                    default: backColor = Color.White; break;
                }
            }
            cardPanel.BackColor = backColor;

            Color textColor = isOnLeave ? Color.FromArgb(130, 80, 0)
                : (emp.Role == EmployeeRole.Technician || emp.Role == EmployeeRole.NewWorker) ? Color.White : Color.Black;

            string cardText = isOnLeave ? $"{emp.Id} - {emp.Name} (NGHỈ PHÉP)" : $"{emp.Id} - {emp.Name}";
            
            var lblName = new Label
            {
                Text = cardText,
                Font = new Font("Segoe UI", 8.5F, isOnLeave ? FontStyle.Bold : FontStyle.Regular),
                ForeColor = textColor,
                Location = new Point(8, 6),
                BackColor = Color.Transparent,
                AutoSize = true
            };
            cardPanel.Controls.Add(lblName);

            if (isOnLeave && !string.IsNullOrEmpty(leave!.Note))
            {
                var lblNote = new Label
                {
                    Text = $"📋 {leave.Note}",
                    Font = new Font("Segoe UI", 7.5F, FontStyle.Regular),
                    ForeColor = Color.FromArgb(100, 60, 0),
                    Location = new Point(8, 24),
                    BackColor = Color.Transparent,
                    AutoSize = true,
                    Padding = new Padding(0)
                };
                cardPanel.Controls.Add(lblNote);
            }

            // Custom paint rounded border
            cardPanel.Paint += (s, pe) =>
            {
                pe.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                int radius = 8;
                using (var path = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    path.AddArc(0, 0, radius, radius, 180, 90);
                    path.AddArc(cardPanel.Width - radius - 1, 0, radius, radius, 270, 90);
                    path.AddArc(cardPanel.Width - radius - 1, cardPanel.Height - radius - 1, radius, radius, 0, 90);
                    path.AddArc(0, cardPanel.Height - radius - 1, radius, radius, 90, 90);
                    path.CloseAllFigures();

                    Color borderColor = (emp.Role == EmployeeRole.Technician && !isOnLeave) 
                        ? Color.FromArgb(60, 60, 60) 
                        : Color.FromArgb(200, 200, 200);
                    using (var pen = new Pen(borderColor, 1F))
                    {
                        pe.Graphics.DrawPath(pen, path);
                    }
                }
            };

            // Set region to clip background to round path
            cardPanel.SizeChanged += (s, ev) => UpdatePanelRegion(cardPanel);
            UpdatePanelRegion(cardPanel);

            if (!isOnLeave)
            {
                // Nút X chỉ có khi không phải nghỉ phép
                var btnDelete = new Button
                {
                    Text = location == "Pool" ? "X" : "x",
                    Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                    Size = new Size(24, 20),
                    Location = new Point(215, 5),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.Transparent,
                    ForeColor = textColor
                };
                btnDelete.FlatAppearance.BorderSize = 0;

                if (location == "Pool")
                {
                    btnDelete.Click += (s, ev) =>
                    {
                        DateTime activeDate = ((DateItem)cbActiveDay.SelectedItem).Date;
                        emp.LeavePeriods.Add(new LeavePeriod(activeDate, activeDate, "Báo nghỉ nhanh"));
                        Log("System", $"Nhân viên {emp.Name} báo nghỉ vào ngày {activeDate:dd/MM/yyyy}.");
                        RunAutoSchedule();
                    };
                }
                else
                {
                    btnDelete.Click += (s, ev) =>
                    {
                        DateTime activeDate = ((DateItem)cbActiveDay.SelectedItem).Date;
                        DateTime keyDate = GetScheduleKey(activeDate);
                        var entry = _schedule.FirstOrDefault(se => se.Date == keyDate && se.ShiftName == location);
                        if (entry != null) entry.EmployeeIds.Remove(emp.Id);
                        SaveSchedule();
                        RedrawActiveDay();
                    };
                }
                cardPanel.Controls.Add(btnDelete);

                cardPanel.MouseDown += (s, ev) =>
                {
                    if (ev.Button == MouseButtons.Left)
                        cardPanel.DoDragDrop(cardPanel, DragDropEffects.Move);
                };
            }

            cardPanel.DoubleClick += CardPanel_DoubleClick;
            lblName.DoubleClick += (s, ev) => CardPanel_DoubleClick(cardPanel, ev);

            return cardPanel;
        }

        private void UpdatePanelRegion(Panel p)
        {
            int radius = 8;
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(p.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(p.Width - radius, p.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, p.Height - radius, radius, radius, 90, 90);
            path.CloseAllFigures();
            p.Region = new Region(path);
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
            if (_isInitializing) return;
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
                string filePath = ExcelService.ExportToExcel(_employees, _schedule, start, end, false, holidayDates);
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
            if (targetPanel == flowDayShift)   targetShift = "Ngày";
            else if (targetPanel == flowCa2Shift)  targetShift = "Ca2";
            else if (targetPanel == flowNightShift) targetShift = "Đêm";
            else if (targetPanel == flowAdminShift) targetShift = "Hành chính";

            if (string.IsNullOrEmpty(targetShift)) return;

            // Kiểm tra phân ca xoay ca 2 tuần ngày, 2 tuần đêm
            var emp = _employees.FirstOrDefault(x => x.Id == employeeId);
            if (emp != null && (emp.Role == EmployeeRole.Worker || emp.Role == EmployeeRole.NewWorker))
            {
                int rot = SchedulerEngine.GetShiftRotationForWeek(emp.Id, activeDate);
                if (targetShift == "Ca2" && rot != 2)
                {
                    MessageBox.Show($"Nhân viên {emp.Name} không trong tuần 'về ca' ở ca 2, không xếp vào ca 2 được!", "Lưu ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (targetShift == "Ngày" && rot != 0)
                {
                    MessageBox.Show($"Nhân viên {emp.Name} đang trong tuần làm ca đêm (ca 3) hoặc ca 2, không xếp vào ca ngày được!", "Lưu ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (targetShift == "Đêm" && rot != 1)
                {
                    MessageBox.Show($"Nhân viên {emp.Name} đang trong tuần làm ca ngày (ca 1) hoặc ca 2, không xếp vào ca đêm được!", "Lưu ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (targetShift == "Hành chính" && rot == 2)
                {
                    MessageBox.Show($"Nhân viên {emp.Name} đang trong tuần 'về ca' ở ca 2, không xếp vào ca hành chính được!", "Lưu ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Xóa khỏi tất cả ca hiện tại
            foreach (var shiftName in new[] { "Ngày", "Ca2", "Đêm", "Hành chính" })
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

            SaveSchedule(); // Lưu ngay sau khi kéo thả
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
                    emp.LeavePeriods.Add(new LeavePeriod(request.date, request.date, "Báo nghỉ qua Zalo"));
                    Log("Zalo Bot", $"Tự động cập nhật nghỉ phép cho {emp.Name} vào ngày {request.date:dd/MM/yyyy}.");

                    RunAutoSchedule();

                    DateTime start = dtpFrom.Value.Date;
                    DateTime end = dtpTo.Value.Date;
                    var holidayDates = _companyHolidays.Select(h => h.Date).ToList();
                    string filePath = ExcelService.ExportToExcel(_employees, _schedule, start, end, false, holidayDates);
                    _zaloBot.SendExcelFile(filePath);
                }
            }
        }

        private void Log(string senderName, string message)
        {
            System.Diagnostics.Debug.WriteLine($"[{DateTime.Now:HH:mm:ss}] [{senderName}] {message}");
        }
        #endregion

        #region Quản lý Ngày nghỉ Công ty (Live Calendar)
        private void btnPrevMonth_Click(object sender, EventArgs e)
        {
            _currentCalendarMonth = _currentCalendarMonth.AddMonths(-1);
            RefreshHolidaysUI();
        }

        private void btnNextMonth_Click(object sender, EventArgs e)
        {
            _currentCalendarMonth = _currentCalendarMonth.AddMonths(1);
            RefreshHolidaysUI();
        }

        private void CalendarButton_Click(object sender, EventArgs e)
        {
            // Lịch ngày nghỉ công ty là cố định, chỉ hiển thị, không cho click đổi
            // (Muốn chỉnh sửa lịch nghỉ thì liên hệ quản lý)
        }

        private void RefreshHolidaysUI()
        {
            lblMonthYear.Text = $"Tháng {_currentCalendarMonth:MM - yyyy}";

            DateTime firstDay = new DateTime(_currentCalendarMonth.Year, _currentCalendarMonth.Month, 1);
            int startOffset = (int)firstDay.DayOfWeek;

            tblCalendar.SuspendLayout();
            for (int i = 0; i < 42; i++)
            {
                DateTime cellDate = firstDay.AddDays(i - startOffset);
                var btn = _calendarButtons[i];
                btn.Tag = cellDate.Date;

                // Tìm nhân viên nghỉ phép ngày này
                var onLeaveIds = _employees
                    .Where(emp => emp != null && emp.LeavePeriods != null && emp.LeavePeriods.Any(lp => lp != null && cellDate.Date >= lp.StartDate && cellDate.Date <= lp.EndDate))
                    .Select(emp => emp.Id)
                    .ToList();

                // Kiểm tra ngày nghỉ công ty
                bool isHoliday = _companyHolidays.Any(h => h.Date == cellDate.Date);

                // Dựng text cho ô lịch: số ngày + mã nhân viên nghỉ (nếu có)
                if (onLeaveIds.Count > 0)
                {
                    string ids = string.Join(",", onLeaveIds);
                    btn.Text = $"{cellDate.Day}\n{ids}";
                    btn.Font = new Font("Segoe UI", 7.5F, FontStyle.Regular);
                }
                else
                {
                    btn.Text = cellDate.Day.ToString();
                    btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                }

                // Màu sắc ưu tiên: đỏ (nghỉ công ty) > vàng (có NV nghỉ phép) > trắng
                if (isHoliday)
                {
                    btn.BackColor = Color.FromArgb(239, 83, 80);
                    btn.ForeColor = Color.White;
                }
                else if (onLeaveIds.Count > 0)
                {
                    btn.BackColor = Color.FromArgb(255, 249, 196); // Vàng nhạt
                    btn.ForeColor = cellDate.Month != _currentCalendarMonth.Month
                        ? Color.FromArgb(180, 140, 0)
                        : Color.FromArgb(130, 80, 0);  // Nâu vàng
                }
                else
                {
                    btn.BackColor = Color.White;
                    btn.ForeColor = cellDate.Month != _currentCalendarMonth.Month
                        ? Color.DarkGray : Color.Black;
                }
            }
            tblCalendar.ResumeLayout();
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
