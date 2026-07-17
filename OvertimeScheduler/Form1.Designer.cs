namespace OvertimeScheduler
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.chkSaturdayWorking = new System.Windows.Forms.CheckBox();
            this.lblMaxPerShift = new System.Windows.Forms.Label();
            this.numMaxPerShift = new System.Windows.Forms.NumericUpDown();
            this.lblMonthlyBudget = new System.Windows.Forms.Label();
            this.numMonthlyBudget = new System.Windows.Forms.NumericUpDown();
            this.lblActiveDay = new System.Windows.Forms.Label();
            this.cbActiveDay = new System.Windows.Forms.ComboBox();
            this.btnAutoSchedule = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.lblBudgetStatus = new System.Windows.Forms.Label();
            this.lblZalo = new System.Windows.Forms.Label();
            this.txtZaloGroupName = new System.Windows.Forms.TextBox();
            this.btnToggleZalo = new System.Windows.Forms.Button();
            
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.groupSidebar = new System.Windows.Forms.GroupBox();
            this.flowEmployeePool = new System.Windows.Forms.FlowLayoutPanel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            
            this.panelMain = new System.Windows.Forms.Panel();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageSchedule = new System.Windows.Forms.TabPage();
            this.tabPageChart = new System.Windows.Forms.TabPage();
            this.tabPageHolidays = new System.Windows.Forms.TabPage();
            
            this.tableLayoutPanelShifts = new System.Windows.Forms.TableLayoutPanel();
            this.groupDayShift = new System.Windows.Forms.GroupBox();
            this.flowDayShift = new System.Windows.Forms.FlowLayoutPanel();
            this.groupNightShift = new System.Windows.Forms.GroupBox();
            this.flowNightShift = new System.Windows.Forms.FlowLayoutPanel();
            this.groupAdminShift = new System.Windows.Forms.GroupBox();
            this.flowAdminShift = new System.Windows.Forms.FlowLayoutPanel();
            this.overtimeChart = new OvertimeScheduler.Forms.OvertimeChart();

            this.tableLayoutPanelHolidays = new System.Windows.Forms.TableLayoutPanel();
            this.panelHolidayLeft = new System.Windows.Forms.Panel();
            this.mcHolidays = new System.Windows.Forms.MonthCalendar();
            this.lblHolidayName = new System.Windows.Forms.Label();
            this.txtHolidayName = new System.Windows.Forms.TextBox();
            this.btnAddHoliday = new System.Windows.Forms.Button();
            this.btnDeleteHoliday = new System.Windows.Forms.Button();
            this.groupHolidaysList = new System.Windows.Forms.GroupBox();
            this.lstHolidays = new System.Windows.Forms.ListBox();

            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxPerShift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMonthlyBudget)).BeginInit();
            this.panelSidebar.SuspendLayout();
            this.groupSidebar.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageSchedule.SuspendLayout();
            this.tabPageChart.SuspendLayout();
            this.tabPageHolidays.SuspendLayout();
            this.tableLayoutPanelShifts.SuspendLayout();
            this.groupDayShift.SuspendLayout();
            this.groupNightShift.SuspendLayout();
            this.groupAdminShift.SuspendLayout();
            this.tableLayoutPanelHolidays.SuspendLayout();
            this.panelHolidayLeft.SuspendLayout();
            this.groupHolidaysList.SuspendLayout();
            this.SuspendLayout();

            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.panelTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTop.Controls.Add(this.lblFrom);
            this.panelTop.Controls.Add(this.dtpFrom);
            this.panelTop.Controls.Add(this.lblTo);
            this.panelTop.Controls.Add(this.dtpTo);
            this.panelTop.Controls.Add(this.chkSaturdayWorking);
            this.panelTop.Controls.Add(this.lblMaxPerShift);
            this.panelTop.Controls.Add(this.numMaxPerShift);
            this.panelTop.Controls.Add(this.lblMonthlyBudget);
            this.panelTop.Controls.Add(this.numMonthlyBudget);
            this.panelTop.Controls.Add(this.lblActiveDay);
            this.panelTop.Controls.Add(this.cbActiveDay);
            this.panelTop.Controls.Add(this.btnAutoSchedule);
            this.panelTop.Controls.Add(this.btnExportExcel);
            this.panelTop.Controls.Add(this.lblBudgetStatus);
            this.panelTop.Controls.Add(this.lblZalo);
            this.panelTop.Controls.Add(this.txtZaloGroupName);
            this.panelTop.Controls.Add(this.btnToggleZalo);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1008, 68);
            this.panelTop.TabIndex = 0;

            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblFrom.Location = new System.Drawing.Point(10, 12);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(26, 15);
            this.lblFrom.TabIndex = 0;
            this.lblFrom.Text = "Từ:";

            // 
            // dtpFrom
            // 
            this.dtpFrom.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(45, 8);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(95, 23);
            this.dtpFrom.TabIndex = 1;
            this.dtpFrom.ValueChanged += new System.EventHandler(this.dtpDateRange_ValueChanged);

            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTo.Location = new System.Drawing.Point(150, 12);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(32, 15);
            this.lblTo.TabIndex = 2;
            this.lblTo.Text = "Đến:";

            // 
            // dtpTo
            // 
            this.dtpTo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(185, 8);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(95, 23);
            this.dtpTo.TabIndex = 3;
            this.dtpTo.ValueChanged += new System.EventHandler(this.dtpDateRange_ValueChanged);

            // 
            // chkSaturdayWorking
            // 
            this.chkSaturdayWorking.AutoSize = true;
            this.chkSaturdayWorking.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.chkSaturdayWorking.Location = new System.Drawing.Point(290, 10);
            this.chkSaturdayWorking.Name = "chkSaturdayWorking";
            this.chkSaturdayWorking.Size = new System.Drawing.Size(125, 19);
            this.chkSaturdayWorking.TabIndex = 4;
            this.chkSaturdayWorking.Text = "Thứ 7 làm thường";
            this.chkSaturdayWorking.UseVisualStyleBackColor = true;
            this.chkSaturdayWorking.CheckedChanged += new System.EventHandler(this.chkSaturdayWorking_CheckedChanged);

            // 
            // lblMaxPerShift
            // 
            this.lblMaxPerShift.AutoSize = true;
            this.lblMaxPerShift.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMaxPerShift.Location = new System.Drawing.Point(425, 12);
            this.lblMaxPerShift.Name = "lblMaxPerShift";
            this.lblMaxPerShift.Size = new System.Drawing.Size(60, 15);
            this.lblMaxPerShift.TabIndex = 5;
            this.lblMaxPerShift.Text = "Người/ca:";

            // 
            // numMaxPerShift
            // 
            this.numMaxPerShift.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numMaxPerShift.Location = new System.Drawing.Point(485, 8);
            this.numMaxPerShift.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numMaxPerShift.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            this.numMaxPerShift.Name = "numMaxPerShift";
            this.numMaxPerShift.Size = new System.Drawing.Size(40, 23);
            this.numMaxPerShift.TabIndex = 6;
            this.numMaxPerShift.Value = new decimal(new int[] { 2, 0, 0, 0 });
            this.numMaxPerShift.ValueChanged += new System.EventHandler(this.numMaxPerShift_ValueChanged);

            // 
            // lblMonthlyBudget
            // 
            this.lblMonthlyBudget.AutoSize = true;
            this.lblMonthlyBudget.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMonthlyBudget.Location = new System.Drawing.Point(535, 12);
            this.lblMonthlyBudget.Name = "lblMonthlyBudget";
            this.lblMonthlyBudget.Size = new System.Drawing.Size(44, 15);
            this.lblMonthlyBudget.TabIndex = 7;
            this.lblMonthlyBudget.Text = "Quỹ 1T:";

            // 
            // numMonthlyBudget
            // 
            this.numMonthlyBudget.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numMonthlyBudget.Location = new System.Drawing.Point(580, 8);
            this.numMonthlyBudget.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            this.numMonthlyBudget.Name = "numMonthlyBudget";
            this.numMonthlyBudget.Size = new System.Drawing.Size(50, 23);
            this.numMonthlyBudget.TabIndex = 8;
            this.numMonthlyBudget.Value = new decimal(new int[] { 200, 0, 0, 0 });
            this.numMonthlyBudget.ValueChanged += new System.EventHandler(this.numMonthlyBudget_ValueChanged);

            // 
            // lblActiveDay
            // 
            this.lblActiveDay.AutoSize = true;
            this.lblActiveDay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblActiveDay.Location = new System.Drawing.Point(640, 12);
            this.lblActiveDay.Name = "lblActiveDay";
            this.lblActiveDay.Size = new System.Drawing.Size(61, 15);
            this.lblActiveDay.TabIndex = 9;
            this.lblActiveDay.Text = "Ngày xếp:";

            // 
            // cbActiveDay
            // 
            this.cbActiveDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbActiveDay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbActiveDay.FormattingEnabled = true;
            this.cbActiveDay.Location = new System.Drawing.Point(705, 8);
            this.cbActiveDay.Name = "cbActiveDay";
            this.cbActiveDay.Size = new System.Drawing.Size(135, 23);
            this.cbActiveDay.TabIndex = 10;
            this.cbActiveDay.SelectedIndexChanged += new System.EventHandler(this.cbActiveDay_SelectedIndexChanged);

            // 
            // lblBudgetStatus
            // 
            this.lblBudgetStatus.AutoSize = true;
            this.lblBudgetStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblBudgetStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.lblBudgetStatus.Location = new System.Drawing.Point(850, 12);
            this.lblBudgetStatus.Name = "lblBudgetStatus";
            this.lblBudgetStatus.Size = new System.Drawing.Size(155, 15);
            this.lblBudgetStatus.TabIndex = 11;
            this.lblBudgetStatus.Text = "Đã xếp: 0 giờ | Còn lại: 0 giờ";

            // 
            // btnAutoSchedule
            // 
            this.btnAutoSchedule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(115)))), ((int)(((byte)(232)))));
            this.btnAutoSchedule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAutoSchedule.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnAutoSchedule.ForeColor = System.Drawing.Color.White;
            this.btnAutoSchedule.Location = new System.Drawing.Point(10, 36);
            this.btnAutoSchedule.Name = "btnAutoSchedule";
            this.btnAutoSchedule.Size = new System.Drawing.Size(130, 25);
            this.btnAutoSchedule.TabIndex = 12;
            this.btnAutoSchedule.Text = "Tự Động Xếp Lịch";
            this.btnAutoSchedule.UseVisualStyleBackColor = false;
            this.btnAutoSchedule.Click += new System.EventHandler(this.btnAutoSchedule_Click);

            // 
            // btnExportExcel
            // 
            this.btnExportExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.btnExportExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportExcel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnExportExcel.ForeColor = System.Drawing.Color.White;
            this.btnExportExcel.Location = new System.Drawing.Point(150, 36);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(130, 25);
            this.btnExportExcel.TabIndex = 13;
            this.btnExportExcel.Text = "Xuất Excel & Gửi Zalo";
            this.btnExportExcel.UseVisualStyleBackColor = false;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);

            // 
            // lblZalo
            // 
            this.lblZalo.AutoSize = true;
            this.lblZalo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblZalo.Location = new System.Drawing.Point(290, 41);
            this.lblZalo.Name = "lblZalo";
            this.lblZalo.Size = new System.Drawing.Size(73, 15);
            this.lblZalo.TabIndex = 14;
            this.lblZalo.Text = "Nhóm Zalo:";

            // 
            // txtZaloGroupName
            // 
            this.txtZaloGroupName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtZaloGroupName.Location = new System.Drawing.Point(365, 37);
            this.txtZaloGroupName.Name = "txtZaloGroupName";
            this.txtZaloGroupName.Size = new System.Drawing.Size(150, 23);
            this.txtZaloGroupName.TabIndex = 15;
            this.txtZaloGroupName.Text = "Test Group";

            // 
            // btnToggleZalo
            // 
            this.btnToggleZalo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(156)))));
            this.btnToggleZalo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleZalo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnToggleZalo.ForeColor = System.Drawing.Color.White;
            this.btnToggleZalo.Location = new System.Drawing.Point(525, 36);
            this.btnToggleZalo.Name = "btnToggleZalo";
            this.btnToggleZalo.Size = new System.Drawing.Size(100, 25);
            this.btnToggleZalo.TabIndex = 16;
            this.btnToggleZalo.Text = "Bật Zalo Bot";
            this.btnToggleZalo.UseVisualStyleBackColor = false;
            this.btnToggleZalo.Click += new System.EventHandler(this.btnToggleZalo_Click);

            // 
            // panelSidebar
            // 
            this.panelSidebar.Controls.Add(this.groupSidebar);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 68);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Padding = new System.Windows.Forms.Padding(10);
            this.panelSidebar.Size = new System.Drawing.Size(300, 582);
            this.panelSidebar.TabIndex = 1;

            // 
            // groupSidebar
            // 
            this.groupSidebar.Controls.Add(this.flowEmployeePool);
            this.groupSidebar.Controls.Add(this.txtSearch);
            this.groupSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupSidebar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupSidebar.Location = new System.Drawing.Point(10, 10);
            this.groupSidebar.Name = "groupSidebar";
            this.groupSidebar.Padding = new System.Windows.Forms.Padding(8);
            this.groupSidebar.Size = new System.Drawing.Size(280, 562);
            this.groupSidebar.TabIndex = 0;
            this.groupSidebar.TabStop = false;
            this.groupSidebar.Text = "DANH SÁCH NHÂN SỰ";

            // 
            // flowEmployeePool
            // 
            this.flowEmployeePool.AutoScroll = true;
            this.flowEmployeePool.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowEmployeePool.Location = new System.Drawing.Point(8, 51);
            this.flowEmployeePool.Name = "flowEmployeePool";
            this.flowEmployeePool.Size = new System.Drawing.Size(264, 503);
            this.flowEmployeePool.TabIndex = 1;

            // 
            // txtSearch
            // 
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtSearch.Location = new System.Drawing.Point(8, 24);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderText = "Tìm kiếm theo tên hoặc mã...";
            this.txtSearch.Size = new System.Drawing.Size(264, 23);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);

            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.tabControlMain);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(300, 68);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(5, 10, 10, 10);
            this.panelMain.Size = new System.Drawing.Size(708, 582);
            this.panelMain.TabIndex = 2;

            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageSchedule);
            this.tabControlMain.Controls.Add(this.tabPageChart);
            this.tabControlMain.Controls.Add(this.tabPageHolidays);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.tabControlMain.Location = new System.Drawing.Point(5, 10);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(693, 562);
            this.tabControlMain.TabIndex = 0;

            // 
            // tabPageSchedule
            // 
            this.tabPageSchedule.Controls.Add(this.tableLayoutPanelShifts);
            this.tabPageSchedule.Location = new System.Drawing.Point(4, 24);
            this.tabPageSchedule.Name = "tabPageSchedule";
            this.tabPageSchedule.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSchedule.Size = new System.Drawing.Size(685, 534);
            this.tabPageSchedule.TabIndex = 0;
            this.tabPageSchedule.Text = "BẢNG XẾP LỊCH";
            this.tabPageSchedule.UseVisualStyleBackColor = true;

            // 
            // tabPageChart
            // 
            this.tabPageChart.Controls.Add(this.overtimeChart);
            this.tabPageChart.Location = new System.Drawing.Point(4, 24);
            this.tabPageChart.Name = "tabPageChart";
            this.tabPageChart.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageChart.Size = new System.Drawing.Size(685, 534);
            this.tabPageChart.TabIndex = 1;
            this.tabPageChart.Text = "BIỂU ĐỒ THỐNG KÊ";
            this.tabPageChart.UseVisualStyleBackColor = true;

            // 
            // tabPageHolidays
            // 
            this.tabPageHolidays.Controls.Add(this.tableLayoutPanelHolidays);
            this.tabPageHolidays.Location = new System.Drawing.Point(4, 24);
            this.tabPageHolidays.Name = "tabPageHolidays";
            this.tabPageHolidays.Padding = new System.Windows.Forms.Padding(5);
            this.tabPageHolidays.Size = new System.Drawing.Size(685, 534);
            this.tabPageHolidays.TabIndex = 2;
            this.tabPageHolidays.Text = "NGÀY NGHỈ CÔNG TY";
            this.tabPageHolidays.UseVisualStyleBackColor = true;

            // 
            // tableLayoutPanelShifts
            // 
            this.tableLayoutPanelShifts.ColumnCount = 3;
            this.tableLayoutPanelShifts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelShifts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelShifts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelShifts.Controls.Add(this.groupAdminShift, 2, 0);
            this.tableLayoutPanelShifts.Controls.Add(this.groupNightShift, 1, 0);
            this.tableLayoutPanelShifts.Controls.Add(this.groupDayShift, 0, 0);
            this.tableLayoutPanelShifts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelShifts.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelShifts.Name = "tableLayoutPanelShifts";
            this.tableLayoutPanelShifts.RowCount = 1;
            this.tableLayoutPanelShifts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelShifts.Size = new System.Drawing.Size(679, 528);
            this.tableLayoutPanelShifts.TabIndex = 0;

            // 
            // groupAdminShift
            // 
            this.groupAdminShift.Controls.Add(this.flowAdminShift);
            this.groupAdminShift.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupAdminShift.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupAdminShift.Location = new System.Drawing.Point(456, 3);
            this.groupAdminShift.Name = "groupAdminShift";
            this.groupAdminShift.Padding = new System.Windows.Forms.Padding(6);
            this.groupAdminShift.Size = new System.Drawing.Size(220, 522);
            this.groupAdminShift.TabIndex = 2;
            this.groupAdminShift.TabStop = false;
            this.groupAdminShift.Text = "HÀNH CHÍNH";

            // 
            // flowAdminShift
            // 
            this.flowAdminShift.AllowDrop = true;
            this.flowAdminShift.AutoScroll = true;
            this.flowAdminShift.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.flowAdminShift.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowAdminShift.Location = new System.Drawing.Point(6, 22);
            this.flowAdminShift.Name = "flowAdminShift";
            this.flowAdminShift.Size = new System.Drawing.Size(208, 494);
            this.flowAdminShift.TabIndex = 1;
            this.flowAdminShift.DragDrop += new System.Windows.Forms.DragEventHandler(this.flowShift_DragDrop);
            this.flowAdminShift.DragEnter += new System.Windows.Forms.DragEventHandler(this.flowShift_DragEnter);

            // 
            // groupNightShift
            // 
            this.groupNightShift.Controls.Add(this.flowNightShift);
            this.groupNightShift.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupNightShift.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupNightShift.Location = new System.Drawing.Point(229, 3);
            this.groupNightShift.Name = "groupNightShift";
            this.groupNightShift.Padding = new System.Windows.Forms.Padding(6);
            this.groupNightShift.Size = new System.Drawing.Size(221, 522);
            this.groupNightShift.TabIndex = 1;
            this.groupNightShift.TabStop = false;
            this.groupNightShift.Text = "CA ĐÊM";

            // 
            // flowNightShift
            // 
            this.flowNightShift.AllowDrop = true;
            this.flowNightShift.AutoScroll = true;
            this.flowNightShift.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.flowNightShift.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowNightShift.Location = new System.Drawing.Point(6, 22);
            this.flowNightShift.Name = "flowNightShift";
            this.flowNightShift.Size = new System.Drawing.Size(209, 494);
            this.flowNightShift.TabIndex = 1;
            this.flowNightShift.DragDrop += new System.Windows.Forms.DragEventHandler(this.flowShift_DragDrop);
            this.flowNightShift.DragEnter += new System.Windows.Forms.DragEventHandler(this.flowShift_DragEnter);

            // 
            // groupDayShift
            // 
            this.groupDayShift.Controls.Add(this.flowDayShift);
            this.groupDayShift.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupDayShift.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupDayShift.Location = new System.Drawing.Point(3, 3);
            this.groupDayShift.Name = "groupDayShift";
            this.groupDayShift.Padding = new System.Windows.Forms.Padding(6);
            this.groupDayShift.Size = new System.Drawing.Size(220, 522);
            this.groupDayShift.TabIndex = 0;
            this.groupDayShift.TabStop = false;
            this.groupDayShift.Text = "CA NGÀY";

            // 
            // flowDayShift
            // 
            this.flowDayShift.AllowDrop = true;
            this.flowDayShift.AutoScroll = true;
            this.flowDayShift.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.flowDayShift.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowDayShift.Location = new System.Drawing.Point(6, 22);
            this.flowDayShift.Name = "flowDayShift";
            this.flowDayShift.Size = new System.Drawing.Size(208, 494);
            this.flowDayShift.TabIndex = 0;
            this.flowDayShift.DragDrop += new System.Windows.Forms.DragEventHandler(this.flowShift_DragDrop);
            this.flowDayShift.DragEnter += new System.Windows.Forms.DragEventHandler(this.flowShift_DragEnter);

            // 
            // overtimeChart
            // 
            this.overtimeChart.BackColor = System.Drawing.Color.White;
            this.overtimeChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.overtimeChart.Location = new System.Drawing.Point(3, 3);
            this.overtimeChart.Name = "overtimeChart";
            this.overtimeChart.Size = new System.Drawing.Size(679, 528);
            this.overtimeChart.TabIndex = 0;

            // 
            // tableLayoutPanelHolidays
            // 
            this.tableLayoutPanelHolidays.ColumnCount = 2;
            this.tableLayoutPanelHolidays.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42F));
            this.tableLayoutPanelHolidays.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58F));
            this.tableLayoutPanelHolidays.Controls.Add(this.panelHolidayLeft, 0, 0);
            this.tableLayoutPanelHolidays.Controls.Add(this.groupHolidaysList, 1, 0);
            this.tableLayoutPanelHolidays.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelHolidays.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanelHolidays.Name = "tableLayoutPanelHolidays";
            this.tableLayoutPanelHolidays.RowCount = 1;
            this.tableLayoutPanelHolidays.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelHolidays.Size = new System.Drawing.Size(675, 524);
            this.tableLayoutPanelHolidays.TabIndex = 0;

            // 
            // panelHolidayLeft
            // 
            this.panelHolidayLeft.Controls.Add(this.mcHolidays);
            this.panelHolidayLeft.Controls.Add(this.lblHolidayName);
            this.panelHolidayLeft.Controls.Add(this.txtHolidayName);
            this.panelHolidayLeft.Controls.Add(this.btnAddHoliday);
            this.panelHolidayLeft.Controls.Add(this.btnDeleteHoliday);
            this.panelHolidayLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHolidayLeft.Location = new System.Drawing.Point(3, 3);
            this.panelHolidayLeft.Name = "panelHolidayLeft";
            this.panelHolidayLeft.Size = new System.Drawing.Size(277, 518);
            this.panelHolidayLeft.TabIndex = 0;

            // 
            // mcHolidays
            // 
            this.mcHolidays.Location = new System.Drawing.Point(10, 10);
            this.mcHolidays.MaxSelectionCount = 1;
            this.mcHolidays.Name = "mcHolidays";
            this.mcHolidays.TabIndex = 0;

            // 
            // lblHolidayName
            // 
            this.lblHolidayName.AutoSize = true;
            this.lblHolidayName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblHolidayName.Location = new System.Drawing.Point(10, 185);
            this.lblHolidayName.Name = "lblHolidayName";
            this.lblHolidayName.Size = new System.Drawing.Size(107, 15);
            this.lblHolidayName.TabIndex = 1;
            this.lblHolidayName.Text = "Tên ngày nghỉ/lễ:";

            // 
            // txtHolidayName
            // 
            this.txtHolidayName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtHolidayName.Location = new System.Drawing.Point(10, 205);
            this.txtHolidayName.Name = "txtHolidayName";
            this.txtHolidayName.Size = new System.Drawing.Size(227, 23);
            this.txtHolidayName.TabIndex = 2;

            // 
            // btnAddHoliday
            // 
            this.btnAddHoliday.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.btnAddHoliday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddHoliday.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnAddHoliday.ForeColor = System.Drawing.Color.White;
            this.btnAddHoliday.Location = new System.Drawing.Point(10, 240);
            this.btnAddHoliday.Name = "btnAddHoliday";
            this.btnAddHoliday.Size = new System.Drawing.Size(108, 25);
            this.btnAddHoliday.TabIndex = 3;
            this.btnAddHoliday.Text = "Thêm Ngày Nghỉ";
            this.btnAddHoliday.UseVisualStyleBackColor = false;
            this.btnAddHoliday.Click += new System.EventHandler(this.btnAddHoliday_Click);

            // 
            // btnDeleteHoliday
            // 
            this.btnDeleteHoliday.BackColor = System.Drawing.Color.DarkRed;
            this.btnDeleteHoliday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteHoliday.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDeleteHoliday.ForeColor = System.Drawing.Color.White;
            this.btnDeleteHoliday.Location = new System.Drawing.Point(129, 240);
            this.btnDeleteHoliday.Name = "btnDeleteHoliday";
            this.btnDeleteHoliday.Size = new System.Drawing.Size(108, 25);
            this.btnDeleteHoliday.TabIndex = 4;
            this.btnDeleteHoliday.Text = "Xóa Ngày Nghỉ";
            this.btnDeleteHoliday.UseVisualStyleBackColor = false;
            this.btnDeleteHoliday.Click += new System.EventHandler(this.btnDeleteHoliday_Click);

            // 
            // groupHolidaysList
            // 
            this.groupHolidaysList.Controls.Add(this.lstHolidays);
            this.groupHolidaysList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupHolidaysList.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupHolidaysList.Location = new System.Drawing.Point(286, 3);
            this.groupHolidaysList.Name = "groupHolidaysList";
            this.groupHolidaysList.Padding = new System.Windows.Forms.Padding(8);
            this.groupHolidaysList.Size = new System.Drawing.Size(386, 518);
            this.groupHolidaysList.TabIndex = 1;
            this.groupHolidaysList.TabStop = false;
            this.groupHolidaysList.Text = "DANH SÁCH CÁC NGÀY NGHỈ";

            // 
            // lstHolidays
            // 
            this.lstHolidays.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstHolidays.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lstHolidays.FormattingEnabled = true;
            this.lstHolidays.ItemHeight = 17;
            this.lstHolidays.Location = new System.Drawing.Point(8, 24);
            this.lstHolidays.Name = "lstHolidays";
            this.lstHolidays.Size = new System.Drawing.Size(370, 486);
            this.lstHolidays.TabIndex = 0;

            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1008, 650);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelSidebar);
            this.Controls.Add(this.panelTop);
            this.MinimumSize = new System.Drawing.Size(1024, 690);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Auto Overtime Scheduler - Xếp lịch tăng ca tự động";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxPerShift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMonthlyBudget)).EndInit();
            this.panelSidebar.ResumeLayout(false);
            this.groupSidebar.ResumeLayout(false);
            this.groupSidebar.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageSchedule.ResumeLayout(false);
            this.tabPageChart.ResumeLayout(false);
            this.tabPageHolidays.ResumeLayout(false);
            this.tableLayoutPanelShifts.ResumeLayout(false);
            this.groupAdminShift.ResumeLayout(false);
            this.groupNightShift.ResumeLayout(false);
            this.groupDayShift.ResumeLayout(false);
            this.tableLayoutPanelHolidays.ResumeLayout(false);
            this.panelHolidayLeft.ResumeLayout(false);
            this.panelHolidayLeft.PerformLayout();
            this.groupHolidaysList.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.CheckBox chkSaturdayWorking;
        private System.Windows.Forms.Label lblMaxPerShift;
        private System.Windows.Forms.NumericUpDown numMaxPerShift;
        private System.Windows.Forms.Label lblMonthlyBudget;
        private System.Windows.Forms.NumericUpDown numMonthlyBudget;
        private System.Windows.Forms.Label lblActiveDay;
        private System.Windows.Forms.ComboBox cbActiveDay;
        private System.Windows.Forms.Button btnAutoSchedule;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Label lblBudgetStatus;
        private System.Windows.Forms.Label lblZalo;
        private System.Windows.Forms.TextBox txtZaloGroupName;
        private System.Windows.Forms.Button btnToggleZalo;
        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.GroupBox groupSidebar;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.FlowLayoutPanel flowEmployeePool;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageSchedule;
        private System.Windows.Forms.TabPage tabPageChart;
        private System.Windows.Forms.TabPage tabPageHolidays;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelShifts;
        private System.Windows.Forms.GroupBox groupDayShift;
        private System.Windows.Forms.FlowLayoutPanel flowDayShift;
        private System.Windows.Forms.GroupBox groupNightShift;
        private System.Windows.Forms.FlowLayoutPanel flowNightShift;
        private System.Windows.Forms.GroupBox groupAdminShift;
        private System.Windows.Forms.FlowLayoutPanel flowAdminShift;
        private OvertimeScheduler.Forms.OvertimeChart overtimeChart;

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelHolidays;
        private System.Windows.Forms.Panel panelHolidayLeft;
        private System.Windows.Forms.MonthCalendar mcHolidays;
        private System.Windows.Forms.Label lblHolidayName;
        private System.Windows.Forms.TextBox txtHolidayName;
        private System.Windows.Forms.Button btnAddHoliday;
        private System.Windows.Forms.Button btnDeleteHoliday;
        private System.Windows.Forms.GroupBox groupHolidaysList;
        private System.Windows.Forms.ListBox lstHolidays;
    }
}
