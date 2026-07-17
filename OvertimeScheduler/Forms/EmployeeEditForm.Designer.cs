namespace OvertimeScheduler.Forms
{
    partial class EmployeeEditForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblEmpInfo = new System.Windows.Forms.Label();
            this.groupLeave = new System.Windows.Forms.GroupBox();
            this.lblLeaveNote = new System.Windows.Forms.Label();
            this.txtLeaveNote = new System.Windows.Forms.TextBox();
            this.btnRemoveLeave = new System.Windows.Forms.Button();
            this.btnAddLeave = new System.Windows.Forms.Button();
            this.lstLeavePeriods = new System.Windows.Forms.ListBox();
            this.dtpLeaveTo = new System.Windows.Forms.DateTimePicker();
            this.lblLeaveTo = new System.Windows.Forms.Label();
            this.dtpLeaveFrom = new System.Windows.Forms.DateTimePicker();
            this.lblLeaveFrom = new System.Windows.Forms.Label();
            this.groupOverride = new System.Windows.Forms.GroupBox();
            this.btnRemoveOverride = new System.Windows.Forms.Button();
            this.btnAddOverride = new System.Windows.Forms.Button();
            this.lstOverrides = new System.Windows.Forms.ListBox();
            this.numOverrideHours = new System.Windows.Forms.NumericUpDown();
            this.lblOverrideHours = new System.Windows.Forms.Label();
            this.dtpOverrideDate = new System.Windows.Forms.DateTimePicker();
            this.lblOverrideDate = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupLeave.SuspendLayout();
            this.groupOverride.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOverrideHours)).BeginInit();
            this.SuspendLayout();
            // 
            // lblEmpInfo
            // 
            this.lblEmpInfo.AutoSize = true;
            this.lblEmpInfo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblEmpInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(115)))), ((int)(((byte)(232)))));
            this.lblEmpInfo.Location = new System.Drawing.Point(15, 15);
            this.lblEmpInfo.Name = "lblEmpInfo";
            this.lblEmpInfo.Size = new System.Drawing.Size(262, 21);
            this.lblEmpInfo.TabIndex = 0;
            this.lblEmpInfo.Text = "Nhân viên: [NV01] Nguyễn Văn A";
            // 
            // groupLeave
            // 
            this.groupLeave.Controls.Add(this.lblLeaveNote);
            this.groupLeave.Controls.Add(this.txtLeaveNote);
            this.groupLeave.Controls.Add(this.btnRemoveLeave);
            this.groupLeave.Controls.Add(this.btnAddLeave);
            this.groupLeave.Controls.Add(this.lstLeavePeriods);
            this.groupLeave.Controls.Add(this.dtpLeaveTo);
            this.groupLeave.Controls.Add(this.lblLeaveTo);
            this.groupLeave.Controls.Add(this.dtpLeaveFrom);
            this.groupLeave.Controls.Add(this.lblLeaveFrom);
            this.groupLeave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupLeave.Location = new System.Drawing.Point(15, 55);
            this.groupLeave.Name = "groupLeave";
            this.groupLeave.Size = new System.Drawing.Size(260, 290);
            this.groupLeave.TabIndex = 1;
            this.groupLeave.TabStop = false;
            this.groupLeave.Text = "ĐĂNG KÝ NGHỈ PHÉP";
            // 
            // lblLeaveNote
            // 
            this.lblLeaveNote.AutoSize = true;
            this.lblLeaveNote.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblLeaveNote.Location = new System.Drawing.Point(10, 88);
            this.lblLeaveNote.Name = "lblLeaveNote";
            this.lblLeaveNote.Size = new System.Drawing.Size(51, 15);
            this.lblLeaveNote.TabIndex = 7;
            this.lblLeaveNote.Text = "Ghi chú:";
            // 
            // txtLeaveNote
            // 
            this.txtLeaveNote.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtLeaveNote.Location = new System.Drawing.Point(80, 84);
            this.txtLeaveNote.Name = "txtLeaveNote";
            this.txtLeaveNote.Size = new System.Drawing.Size(170, 23);
            this.txtLeaveNote.TabIndex = 8;
            // 
            // btnRemoveLeave
            // 
            this.btnRemoveLeave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnRemoveLeave.Location = new System.Drawing.Point(135, 250);
            this.btnRemoveLeave.Name = "btnRemoveLeave";
            this.btnRemoveLeave.Size = new System.Drawing.Size(115, 25);
            this.btnRemoveLeave.TabIndex = 6;
            this.btnRemoveLeave.Text = "Xóa Chọn";
            this.btnRemoveLeave.UseVisualStyleBackColor = true;
            this.btnRemoveLeave.Click += new System.EventHandler(this.btnRemoveLeave_Click);
            // 
            // btnAddLeave
            // 
            this.btnAddLeave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnAddLeave.Location = new System.Drawing.Point(10, 250);
            this.btnAddLeave.Name = "btnAddLeave";
            this.btnAddLeave.Size = new System.Drawing.Size(115, 25);
            this.btnAddLeave.TabIndex = 5;
            this.btnAddLeave.Text = "Thêm Nghỉ";
            this.btnAddLeave.UseVisualStyleBackColor = true;
            this.btnAddLeave.Click += new System.EventHandler(this.btnAddLeave_Click);
            // 
            // lstLeavePeriods
            // 
            this.lstLeavePeriods.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lstLeavePeriods.FormattingEnabled = true;
            this.lstLeavePeriods.ItemHeight = 15;
            this.lstLeavePeriods.Location = new System.Drawing.Point(10, 115);
            this.lstLeavePeriods.Name = "lstLeavePeriods";
            this.lstLeavePeriods.Size = new System.Drawing.Size(240, 124);
            this.lstLeavePeriods.TabIndex = 4;
            // 
            // dtpLeaveTo
            // 
            this.dtpLeaveTo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dtpLeaveTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpLeaveTo.Location = new System.Drawing.Point(80, 55);
            this.dtpLeaveTo.Name = "dtpLeaveTo";
            this.dtpLeaveTo.Size = new System.Drawing.Size(170, 23);
            this.dtpLeaveTo.TabIndex = 3;
            // 
            // lblLeaveTo
            // 
            this.lblLeaveTo.AutoSize = true;
            this.lblLeaveTo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblLeaveTo.Location = new System.Drawing.Point(10, 59);
            this.lblLeaveTo.Name = "lblLeaveTo";
            this.lblLeaveTo.Size = new System.Drawing.Size(60, 15);
            this.lblLeaveTo.TabIndex = 2;
            this.lblLeaveTo.Text = "Đến ngày:";
            // 
            // dtpLeaveFrom
            // 
            this.dtpLeaveFrom.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dtpLeaveFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpLeaveFrom.Location = new System.Drawing.Point(80, 22);
            this.dtpLeaveFrom.Name = "dtpLeaveFrom";
            this.dtpLeaveFrom.Size = new System.Drawing.Size(170, 23);
            this.dtpLeaveFrom.TabIndex = 1;
            // 
            // lblLeaveFrom
            // 
            this.lblLeaveFrom.AutoSize = true;
            this.lblLeaveFrom.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblLeaveFrom.Location = new System.Drawing.Point(10, 26);
            this.lblLeaveFrom.Name = "lblLeaveFrom";
            this.lblLeaveFrom.Size = new System.Drawing.Size(52, 15);
            this.lblLeaveFrom.TabIndex = 0;
            this.lblLeaveFrom.Text = "Từ ngày:";
            // 
            // groupOverride
            // 
            this.groupOverride.Controls.Add(this.btnRemoveOverride);
            this.groupOverride.Controls.Add(this.btnAddOverride);
            this.groupOverride.Controls.Add(this.lstOverrides);
            this.groupOverride.Controls.Add(this.numOverrideHours);
            this.groupOverride.Controls.Add(this.lblOverrideHours);
            this.groupOverride.Controls.Add(this.dtpOverrideDate);
            this.groupOverride.Controls.Add(this.lblOverrideDate);
            this.groupOverride.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupOverride.Location = new System.Drawing.Point(290, 55);
            this.groupOverride.Name = "groupOverride";
            this.groupOverride.Size = new System.Drawing.Size(260, 290);
            this.groupOverride.TabIndex = 2;
            this.groupOverride.TabStop = false;
            this.groupOverride.Text = "GÁN GIỜ TĂNG CA CỐ ĐỊNH";
            // 
            // btnRemoveOverride
            // 
            this.btnRemoveOverride.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnRemoveOverride.Location = new System.Drawing.Point(135, 250);
            this.btnRemoveOverride.Name = "btnRemoveOverride";
            this.btnRemoveOverride.Size = new System.Drawing.Size(115, 25);
            this.btnRemoveOverride.TabIndex = 7;
            this.btnRemoveOverride.Text = "Xóa Chọn";
            this.btnRemoveOverride.UseVisualStyleBackColor = true;
            this.btnRemoveOverride.Click += new System.EventHandler(this.btnRemoveOverride_Click);
            // 
            // btnAddOverride
            // 
            this.btnAddOverride.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnAddOverride.Location = new System.Drawing.Point(10, 250);
            this.btnAddOverride.Name = "btnAddOverride";
            this.btnAddOverride.Size = new System.Drawing.Size(115, 25);
            this.btnAddOverride.TabIndex = 6;
            this.btnAddOverride.Text = "Thêm/Sửa";
            this.btnAddOverride.UseVisualStyleBackColor = true;
            this.btnAddOverride.Click += new System.EventHandler(this.btnAddOverride_Click);
            // 
            // lstOverrides
            // 
            this.lstOverrides.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lstOverrides.FormattingEnabled = true;
            this.lstOverrides.ItemHeight = 15;
            this.lstOverrides.Location = new System.Drawing.Point(10, 90);
            this.lstOverrides.Name = "lstOverrides";
            this.lstOverrides.Size = new System.Drawing.Size(240, 154);
            this.lstOverrides.TabIndex = 5;
            // 
            // numOverrideHours
            // 
            this.numOverrideHours.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numOverrideHours.Location = new System.Drawing.Point(80, 55);
            this.numOverrideHours.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numOverrideHours.Name = "numOverrideHours";
            this.numOverrideHours.Size = new System.Drawing.Size(170, 23);
            this.numOverrideHours.TabIndex = 3;
            this.numOverrideHours.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            // 
            // lblOverrideHours
            // 
            this.lblOverrideHours.AutoSize = true;
            this.lblOverrideHours.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblOverrideHours.Location = new System.Drawing.Point(10, 59);
            this.lblOverrideHours.Name = "lblOverrideHours";
            this.lblOverrideHours.Size = new System.Drawing.Size(57, 15);
            this.lblOverrideHours.TabIndex = 2;
            this.lblOverrideHours.Text = "Số tiếng: ";
            // 
            // dtpOverrideDate
            // 
            this.dtpOverrideDate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dtpOverrideDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpOverrideDate.Location = new System.Drawing.Point(80, 22);
            this.dtpOverrideDate.Name = "dtpOverrideDate";
            this.dtpOverrideDate.Size = new System.Drawing.Size(170, 23);
            this.dtpOverrideDate.TabIndex = 1;
            // 
            // lblOverrideDate
            // 
            this.lblOverrideDate.AutoSize = true;
            this.lblOverrideDate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblOverrideDate.Location = new System.Drawing.Point(10, 26);
            this.lblOverrideDate.Name = "lblOverrideDate";
            this.lblOverrideDate.Size = new System.Drawing.Size(38, 15);
            this.lblOverrideDate.TabIndex = 0;
            this.lblOverrideDate.Text = "Ngày:";
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(115)))), ((int)(((byte)(232)))));
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(340, 355);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(100, 32);
            this.btnUpdate.TabIndex = 3;
            this.btnUpdate.Text = "Cập Nhật";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCancel.Location = new System.Drawing.Point(450, 355);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 32);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Hủy bỏ";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // EmployeeEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(569, 401);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.groupOverride);
            this.Controls.Add(this.groupLeave);
            this.Controls.Add(this.lblEmpInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EmployeeEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chỉnh sửa thông tin nhân viên";
            this.groupLeave.ResumeLayout(false);
            this.groupLeave.PerformLayout();
            this.groupOverride.ResumeLayout(false);
            this.groupOverride.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOverrideHours)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblEmpInfo;
        private System.Windows.Forms.GroupBox groupLeave;
        private System.Windows.Forms.Label lblLeaveFrom;
        private System.Windows.Forms.DateTimePicker dtpLeaveFrom;
        private System.Windows.Forms.Label lblLeaveTo;
        private System.Windows.Forms.DateTimePicker dtpLeaveTo;
        private System.Windows.Forms.ListBox lstLeavePeriods;
        private System.Windows.Forms.Button btnAddLeave;
        private System.Windows.Forms.Button btnRemoveLeave;
        private System.Windows.Forms.GroupBox groupOverride;
        private System.Windows.Forms.Label lblOverrideDate;
        private System.Windows.Forms.DateTimePicker dtpOverrideDate;
        private System.Windows.Forms.Label lblOverrideHours;
        private System.Windows.Forms.NumericUpDown numOverrideHours;
        private System.Windows.Forms.ListBox lstOverrides;
        private System.Windows.Forms.Button btnAddOverride;
        private System.Windows.Forms.Button btnRemoveOverride;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblLeaveNote;
        private System.Windows.Forms.TextBox txtLeaveNote;
    }
}
