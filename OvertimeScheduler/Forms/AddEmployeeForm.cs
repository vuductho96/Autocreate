using System;
using System.Drawing;
using System.Windows.Forms;
using OvertimeScheduler.Models;

namespace OvertimeScheduler.Forms
{
    public class AddEmployeeForm : Form
    {
        private TextBox txtId;
        private TextBox txtName;
        private ComboBox cbRole;
        private Button btnSave;
        private Button btnCancel;

        public Employee NewEmployee { get; private set; }

        public AddEmployeeForm()
        {
            this.Text = "Thêm nhân viên mới";
            this.Size = new Size(320, 220);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var lblId = new Label { Text = "Mã NV:", Location = new Point(20, 20), AutoSize = true };
            txtId = new TextBox { Location = new Point(100, 18), Size = new Size(180, 23) };

            var lblName = new Label { Text = "Tên NV:", Location = new Point(20, 55), AutoSize = true };
            txtName = new TextBox { Location = new Point(100, 53), Size = new Size(180, 23) };

            var lblRole = new Label { Text = "Chức vụ:", Location = new Point(20, 90), AutoSize = true };
            cbRole = new ComboBox 
            { 
                Location = new Point(100, 88), 
                Size = new Size(180, 23),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cbRole.Items.Add("Công nhân");
            cbRole.Items.Add("Trưởng nhóm");
            cbRole.Items.Add("Kỹ thuật");
            cbRole.Items.Add("Công nhân mới");
            cbRole.SelectedIndex = 0;

            this.Controls.AddRange(new Control[] { lblId, txtId, lblName, txtName, lblRole, cbRole });

            btnSave = new Button
            {
                Text = "Thêm",
                Location = new Point(110, 135),
                Size = new Size(80, 28),
                BackColor = Color.FromArgb(26, 115, 232),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button
            {
                Text = "Hủy",
                Location = new Point(200, 135),
                Size = new Size(80, 28),
                FlatStyle = FlatStyle.Flat
            };
            btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };

            this.Controls.AddRange(new Control[] { btnSave, btnCancel });
            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string id = txtId.Text.Trim();
            string name = txtName.Text.Trim();

            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Vui lòng điền đầy đủ Mã và Tên nhân viên!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            EmployeeRole role = EmployeeRole.Worker;
            if (cbRole.SelectedIndex == 1) role = EmployeeRole.Leader;
            else if (cbRole.SelectedIndex == 2) role = EmployeeRole.Technician;
            else if (cbRole.SelectedIndex == 3) role = EmployeeRole.NewWorker;

            NewEmployee = new Employee(id, name, role);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
