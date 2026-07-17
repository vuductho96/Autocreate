using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using OvertimeScheduler.Models;

namespace OvertimeScheduler.Forms
{
    public class OvertimeChart : UserControl
    {
        private Dictionary<EmployeeRole, double> _totalHours = new Dictionary<EmployeeRole, double>();
        private Dictionary<EmployeeRole, double> _avgHours = new Dictionary<EmployeeRole, double>();

        public OvertimeChart()
        {
            this.DoubleBuffered = true;
            this.BackColor = Color.White;
        }

        public void UpdateData(List<Employee> employees, List<ScheduleEntry> schedule, DateTime start, DateTime end)
        {
            _totalHours.Clear();
            _avgHours.Clear();

            var roles = Enum.GetValues(typeof(EmployeeRole)).Cast<EmployeeRole>().ToList();
            
            // Map lưu trữ tổng giờ làm của mỗi nhân sự trong khoảng ngày đã chọn
            var empHours = employees.ToDictionary(e => e.Id, e => 0.0);

            // Cộng trước các giờ làm gán cố định
            foreach (var emp in employees)
            {
                foreach (var kvp in emp.FixedOvertimeHours)
                {
                    if (kvp.Key >= start.Date && kvp.Key <= end.Date)
                    {
                        empHours[emp.Id] += kvp.Value;
                    }
                }
            }

            // Cộng dồn giờ làm từ ca trực (mỗi ca trực được tính là 4 tiếng, trừ khi ngày đó có giờ cố định)
            foreach (var entry in schedule)
            {
                if (entry.Date >= start.Date && entry.Date <= end.Date)
                {
                    foreach (var empId in entry.EmployeeIds)
                    {
                        var emp = employees.FirstOrDefault(e => e.Id == empId);
                        if (emp != null)
                        {
                            if (!emp.FixedOvertimeHours.ContainsKey(entry.Date))
                            {
                                empHours[empId] += 4.0;
                            }
                        }
                    }
                }
            }

            // Tính toán tổng số và trung bình theo vai trò
            foreach (var role in roles)
            {
                var roleEmployees = employees.Where(e => e.Role == role).ToList();
                double total = roleEmployees.Sum(e => empHours[e.Id]);
                double avg = roleEmployees.Count > 0 ? total / roleEmployees.Count : 0.0;

                _totalHours[role] = total;
                _avgHours[role] = avg;
            }

            this.Invalidate(); // Vẽ lại biểu đồ
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int width = this.Width;
            int height = this.Height;

            // Vẽ nền và khung viền nhẹ nhàng, sang trọng
            g.Clear(Color.FromArgb(248, 250, 252));
            using (var borderPen = new Pen(Color.FromArgb(226, 232, 240), 1))
            {
                g.DrawRectangle(borderPen, 0, 0, width - 1, height - 1);
            }

            // Tựa đề biểu đồ
            using (var titleFont = new Font("Segoe UI", 9.5F, FontStyle.Bold))
            using (var brush = new SolidBrush(Color.FromArgb(15, 23, 42)))
            {
                g.DrawString("BIỂU ĐỒ THỐNG KÊ TIẾNG TĂNG CA TUẦN THEO VAI TRÒ", titleFont, brush, 15, 10);
            }

            // Căn lề vẽ biểu đồ
            int marginLeft = 55;
            int marginRight = 25;
            int marginTop = 38;
            int marginBottom = 30;
            int chartWidth = width - marginLeft - marginRight;
            int chartHeight = height - marginTop - marginBottom;

            if (chartWidth <= 0 || chartHeight <= 0) return;

            // Xác định giá trị cực đại để chia tỉ lệ trục Y
            double maxVal = 20.0; 
            if (_totalHours.Values.Count > 0)
            {
                double maxTotal = _totalHours.Values.Max();
                double maxAvg = _avgHours.Values.Max();
                maxVal = Math.Max(maxVal, Math.Max(maxTotal, maxAvg * 5)); // Đảm bảo line cũng hiển thị đẹp
            }
            maxVal = Math.Ceiling(maxVal / 10.0) * 10.0; // Tròn chục

            // Vẽ lưới kẻ ngang (Gridlines) và nhãn trục Y
            using (var gridPen = new Pen(Color.FromArgb(226, 232, 240), 1))
            using (var labelFont = new Font("Segoe UI", 8F))
            using (var labelBrush = new SolidBrush(Color.FromArgb(100, 116, 139)))
            {
                for (int i = 0; i <= 4; i++)
                {
                    double val = (maxVal / 4) * i;
                    int y = marginTop + chartHeight - (int)((val / maxVal) * chartHeight);

                    g.DrawLine(gridPen, marginLeft, y, marginLeft + chartWidth, y);
                    g.DrawString(val.ToString("0"), labelFont, labelBrush, marginLeft - 30, y - 6);
                }
            }

            var roles = Enum.GetValues(typeof(EmployeeRole)).Cast<EmployeeRole>().ToList();
            int barWidth = 32;
            int groupSpacing = chartWidth / roles.Count;

            using (var linePen = new Pen(Color.FromArgb(239, 68, 68), 2.5F)) // Dây màu đỏ cho Average
            using (var font = new Font("Segoe UI", 8F))
            using (var boldFont = new Font("Segoe UI", 8F, FontStyle.Bold))
            using (var textBrush = new SolidBrush(Color.FromArgb(15, 23, 42)))
            {
                Point[] linePoints = new Point[roles.Count];

                for (int i = 0; i < roles.Count; i++)
                {
                    var role = roles[i];
                    int groupCenterX = marginLeft + (i * groupSpacing) + (groupSpacing / 2);

                    // 1. Vẽ Cột hiển thị Tổng số giờ tăng ca của vai trò
                    double total = _totalHours.ContainsKey(role) ? _totalHours[role] : 0.0;
                    int barHeight = (int)((total / maxVal) * chartHeight);
                    int barX = groupCenterX - (barWidth / 2);
                    int barY = marginTop + chartHeight - barHeight;

                    Color barColor = Color.FromArgb(14, 165, 233); // Xanh biển (Leader)
                    if (role == EmployeeRole.Technician) barColor = Color.FromArgb(30, 41, 59); // Đen xám (Kỹ thuật)
                    else if (role == EmployeeRole.NewWorker) barColor = Color.FromArgb(244, 63, 94); // Đỏ hồng (Mới)
                    else if (role == EmployeeRole.Worker) barColor = Color.FromArgb(226, 232, 240); // Trắng/Bạc (Công nhân)

                    using (var barBrush = new SolidBrush(barColor))
                    {
                        g.FillRectangle(barBrush, barX, barY, barWidth, barHeight);
                        using (var borderPen = new Pen(Color.FromArgb(148, 163, 184), 1))
                        {
                            g.DrawRectangle(borderPen, barX, barY, barWidth, barHeight);
                        }
                    }

                    // Ghi trị số cột
                    if (total > 0)
                    {
                        string valText = total.ToString("0");
                        SizeF sz = g.MeasureString(valText, boldFont);
                        g.DrawString(valText, boldFont, textBrush, barX + (barWidth - sz.Width) / 2, barY - 14);
                    }

                    // Ghi nhãn danh mục trục X
                    string roleLabel = GetRoleLabel(role);
                    SizeF labelSize = g.MeasureString(roleLabel, boldFont);
                    g.DrawString(roleLabel, boldFont, textBrush, groupCenterX - (labelSize.Width / 2), marginTop + chartHeight + 6);

                    // 2. Lưu tọa độ vẽ dây giá trị Trung bình (Avg)
                    double avg = _avgHours.ContainsKey(role) ? _avgHours[role] : 0.0;
                    int lineY = marginTop + chartHeight - (int)((avg / maxVal) * chartHeight);
                    linePoints[i] = new Point(groupCenterX, lineY);
                }

                // Vẽ dây kết nối
                if (roles.Count > 1)
                {
                    g.DrawLines(linePen, linePoints);
                }

                // Vẽ các nút điểm dây và ghi số liệu trung bình
                using (var dotBrush = new SolidBrush(Color.FromArgb(239, 68, 68)))
                {
                    for (int i = 0; i < roles.Count; i++)
                    {
                        double avg = _avgHours.ContainsKey(roles[i]) ? _avgHours[roles[i]] : 0.0;
                        g.FillEllipse(dotBrush, linePoints[i].X - 4, linePoints[i].Y - 4, 8, 8);

                        string avgText = $"{avg:0.0}h (Trung bình)";
                        SizeF sz = g.MeasureString(avgText, boldFont);
                        g.DrawString(avgText, boldFont, dotBrush, linePoints[i].X - (sz.Width / 2), linePoints[i].Y - 16);
                    }
                }
            }

            // Vẽ Chú Giải (Legend)
            using (var font = new Font("Segoe UI", 8F))
            using (var textBrush = new SolidBrush(Color.FromArgb(71, 85, 105)))
            {
                int legX = width - 230;
                int legY = 12;

                // Cột tổng giờ
                using (var colBrush = new SolidBrush(Color.FromArgb(14, 165, 233)))
                {
                    g.FillRectangle(colBrush, legX, legY + 2, 12, 10);
                    g.DrawString("Tổng giờ tăng ca", font, textBrush, legX + 16, legY);
                }

                // Đường trung bình
                using (var linePen = new Pen(Color.FromArgb(239, 68, 68), 2))
                using (var dotBrush = new SolidBrush(Color.FromArgb(239, 68, 68)))
                {
                    g.DrawLine(linePen, legX + 115, legY + 7, legX + 130, legY + 7);
                    g.FillEllipse(dotBrush, legX + 120, legY + 5, 5, 5);
                    g.DrawString("Trung bình/người", font, textBrush, legX + 135, legY);
                }
            }
        }

        private string GetRoleLabel(EmployeeRole role)
        {
            switch (role)
            {
                case EmployeeRole.Leader: return "Leader";
                case EmployeeRole.Technician: return "Kỹ thuật";
                case EmployeeRole.NewWorker: return "Mới";
                case EmployeeRole.Worker: return "Công nhân";
                default: return "";
            }
        }
    }
}
