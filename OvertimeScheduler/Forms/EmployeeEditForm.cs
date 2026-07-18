using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using OvertimeScheduler.Models;

namespace OvertimeScheduler.Forms
{
    public partial class EmployeeEditForm : Form
    {
        private Employee _employee;
        private List<LeavePeriod> _tempLeavePeriods;
        private Dictionary<DateTime, double> _tempOverrides;

        public EmployeeEditForm(Employee employee)
        {
            InitializeComponent();
            _employee = employee;

            // Sao chép dữ liệu tạm để hỗ trợ rollback khi nhấn Cancel
            _tempLeavePeriods = employee.LeavePeriods.Select(lp => new LeavePeriod(lp.StartDate, lp.EndDate, lp.Note)).ToList();
            _tempOverrides = employee.FixedOvertimeHours.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            lblEmpInfo.Text = $"Nhân viên: [{employee.Id}] - {employee.Name}";

            RefreshLeaveList();
            RefreshOverrideList();
        }

        private void RefreshLeaveList()
        {
            lstLeavePeriods.Items.Clear();
            foreach (var lp in _tempLeavePeriods)
            {
                lstLeavePeriods.Items.Add(lp);
            }
        }

        private void RefreshOverrideList()
        {
            lstOverrides.Items.Clear();
            foreach (var kvp in _tempOverrides.OrderBy(k => k.Key))
            {
                lstOverrides.Items.Add(new OverrideItem(kvp.Key, kvp.Value));
            }
        }

        private void btnAddLeave_Click(object sender, EventArgs e)
        {
            DateTime start = dtpLeaveFrom.Value.Date;
            DateTime end = dtpLeaveTo.Value.Date;
            string note = txtLeaveNote.Text.Trim();

            if (start > end)
            {
                MessageBox.Show("Ngày bắt đầu không thể lớn hơn ngày kết thúc!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _tempLeavePeriods.Add(new LeavePeriod(start, end, note));
            txtLeaveNote.Clear();
            RefreshLeaveList();
        }

        private void btnRemoveLeave_Click(object sender, EventArgs e)
        {
            if (lstLeavePeriods.SelectedItem == null) return;
            _tempLeavePeriods.Remove((LeavePeriod)lstLeavePeriods.SelectedItem);
            RefreshLeaveList();
        }

        private void btnAddOverride_Click(object sender, EventArgs e)
        {
            DateTime date = dtpOverrideDate.Value.Date;
            double hours = (double)numOverrideHours.Value;

            if (hours <= 0)
            {
                MessageBox.Show("Số giờ tăng ca phải lớn hơn 0!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _tempOverrides[date] = hours;
            RefreshOverrideList();
        }

        private void btnRemoveOverride_Click(object sender, EventArgs e)
        {
            if (lstOverrides.SelectedItem == null) return;
            var item = (OverrideItem)lstOverrides.SelectedItem;
            _tempOverrides.Remove(item.Date);
            RefreshOverrideList();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Gán dữ liệu tạm lại cho nhân viên chính thức
            _employee.LeavePeriods = _tempLeavePeriods;
            _employee.FixedOvertimeHours = _tempOverrides;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private class OverrideItem
        {
            public DateTime Date { get; }
            public double Hours { get; }

            public OverrideItem(DateTime date, double hours)
            {
                Date = date;
                Hours = hours;
            }

            public override string ToString()
            {
                return $"{Date:dd/MM/yyyy}: {Hours} giờ";
            }
        }
    }
}
