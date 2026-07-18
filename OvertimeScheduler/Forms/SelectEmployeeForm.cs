using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using OvertimeScheduler.Models;

namespace OvertimeScheduler.Forms
{
    public class SelectEmployeeForm : Form
    {
        private ComboBox cbEmployees;
        private Button btnOk;
        private Button btnCancel;
        public Employee SelectedEmployee { get; private set; }

        public SelectEmployeeForm(List<Employee> availableEmployees)
        {
            this.Text = "Thêm nhân viên vào ca";
            this.Size = new Size(350, 150);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var lbl = new Label
            {
                Text = "Chọn nhân viên (có thể gõ để tìm):",
                Location = new Point(20, 15),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular)
            };
            this.Controls.Add(lbl);

            cbEmployees = new ComboBox
            {
                Location = new Point(20, 40),
                Size = new Size(300, 25),
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                DropDownStyle = ComboBoxStyle.DropDown,
                AutoCompleteMode = AutoCompleteMode.SuggestAppend,
                AutoCompleteSource = AutoCompleteSource.ListItems
            };

            foreach (var emp in availableEmployees)
            {
                cbEmployees.Items.Add(new EmployeeItem(emp));
            }

            if (cbEmployees.Items.Count > 0)
                cbEmployees.SelectedIndex = 0;

            this.Controls.Add(cbEmployees);

            btnOk = new Button
            {
                Text = "Thêm",
                Location = new Point(140, 75),
                Size = new Size(80, 25),
                BackColor = Color.FromArgb(26, 115, 232),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnOk.Click += (s, e) =>
            {
                if (cbEmployees.SelectedItem is EmployeeItem item)
                {
                    SelectedEmployee = item.Employee;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một nhân viên hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };
            this.Controls.Add(btnOk);

            btnCancel = new Button
            {
                Text = "Hủy",
                Location = new Point(230, 75),
                Size = new Size(80, 25),
                FlatStyle = FlatStyle.Flat
            };
            btnCancel.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnOk;
            this.CancelButton = btnCancel;
        }

        private class EmployeeItem
        {
            public Employee Employee { get; }
            public EmployeeItem(Employee emp)
            {
                Employee = emp;
            }
            public override string ToString()
            {
                return $"{Employee.Id} - {Employee.Name}";
            }
        }
    }
}
