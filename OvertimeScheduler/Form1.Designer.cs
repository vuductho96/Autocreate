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
            this.lblActiveDay = new System.Windows.Forms.Label();
            this.cbActiveDay = new System.Windows.Forms.ComboBox();
            this.btnAutoSchedule = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.lblBudgetStatus = new System.Windows.Forms.Label();
            
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.groupSidebar = new System.Windows.Forms.GroupBox();
            this.flowEmployeePool = new System.Windows.Forms.FlowLayoutPanel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            
            this.panelMain = new System.Windows.Forms.Panel();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageSchedule = new System.Windows.Forms.TabPage();
            this.tabPageChart = new System.Windows.Forms.TabPage();
            this.tabPageHolidays = new System.Windows.Forms.TabPage();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            
            this.tableLayoutPanelShifts = new System.Windows.Forms.TableLayoutPanel();
            this.groupDayShift = new System.Windows.Forms.GroupBox();
            this.flowDayShift = new System.Windows.Forms.FlowLayoutPanel();
            this.groupCa2Shift = new System.Windows.Forms.GroupBox();
            this.flowCa2Shift = new System.Windows.Forms.FlowLayoutPanel();
            this.groupNightShift = new System.Windows.Forms.GroupBox();
            this.flowNightShift = new System.Windows.Forms.FlowLayoutPanel();
            this.groupAdminShift = new System.Windows.Forms.GroupBox();
            this.flowAdminShift = new System.Windows.Forms.FlowLayoutPanel();
            this.overtimeChart = new OvertimeScheduler.Forms.OvertimeChart();

            this.panelCalendarContainer = new System.Windows.Forms.Panel();
            this.panelHolidayHeader = new System.Windows.Forms.Panel();
            this.btnPrevMonth = new System.Windows.Forms.Button();
            this.lblMonthYear = new System.Windows.Forms.Label();
            this.btnNextMonth = new System.Windows.Forms.Button();
            this.tblCalendar = new System.Windows.Forms.TableLayoutPanel();

            this.grpScheduleConfig = new System.Windows.Forms.GroupBox();
            this.lblMaxPerShift = new System.Windows.Forms.Label();
            this.numMaxPerShift = new System.Windows.Forms.NumericUpDown();
            this.lblMonthlyBudget = new System.Windows.Forms.Label();
            this.numMonthlyBudget = new System.Windows.Forms.NumericUpDown();

            this.grpZaloConfig = new System.Windows.Forms.GroupBox();
            this.lblZalo = new System.Windows.Forms.Label();
            this.txtZaloGroupName = new System.Windows.Forms.TextBox();
            this.btnToggleZalo = new System.Windows.Forms.Button();
            this.btnAddEmployee = new System.Windows.Forms.Button();

            this.panelTop.SuspendLayout();
            this.panelSidebar.SuspendLayout();
            this.groupSidebar.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageSchedule.SuspendLayout();
            this.tabPageChart.SuspendLayout();
            this.tabPageHolidays.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            this.tableLayoutPanelShifts.SuspendLayout();
            this.groupCa2Shift.SuspendLayout();
            this.groupDayShift.SuspendLayout();
            this.groupNightShift.SuspendLayout();
            this.groupAdminShift.SuspendLayout();
            this.panelCalendarContainer.SuspendLayout();
            this.panelHolidayHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxPerShift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMonthlyBudget)).BeginInit();
            this.grpScheduleConfig.SuspendLayout();
            this.grpZaloConfig.SuspendLayout();
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
            this.panelTop.Controls.Add(this.lblActiveDay);
            this.panelTop.Controls.Add(this.cbActiveDay);
            this.panelTop.Controls.Add(this.btnAutoSchedule);
            this.panelTop.Controls.Add(this.btnExportExcel);
            this.panelTop.Controls.Add(this.lblBudgetStatus);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1008, 45);
            this.panelTop.TabIndex = 0;

            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblFrom.Location = new System.Drawing.Point(10, 14);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(26, 15);
            this.lblFrom.TabIndex = 0;
            this.lblFrom.Text = "Từ:";

            // 
            // dtpFrom
            // 
            this.dtpFrom.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(40, 10);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(90, 23);
            this.dtpFrom.TabIndex = 1;
            this.dtpFrom.ValueChanged += new System.EventHandler(this.dtpDateRange_ValueChanged);

            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTo.Location = new System.Drawing.Point(140, 14);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(32, 15);
            this.lblTo.TabIndex = 2;
            this.lblTo.Text = "Đến:";

            // 
            // dtpTo
            // 
            this.dtpTo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(175, 10);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(90, 23);
            this.dtpTo.TabIndex = 3;
            this.dtpTo.ValueChanged += new System.EventHandler(this.dtpDateRange_ValueChanged);

            // 
            // lblActiveDay
            // 
            this.lblActiveDay.AutoSize = true;
            this.lblActiveDay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblActiveDay.Location = new System.Drawing.Point(275, 14);
            this.lblActiveDay.Name = "lblActiveDay";
            this.lblActiveDay.Size = new System.Drawing.Size(61, 15);
            this.lblActiveDay.TabIndex = 4;
            this.lblActiveDay.Text = "Ngày xếp:";

            // 
            // cbActiveDay
            // 
            this.cbActiveDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbActiveDay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbActiveDay.FormattingEnabled = true;
            this.cbActiveDay.Location = new System.Drawing.Point(340, 10);
            this.cbActiveDay.Name = "cbActiveDay";
            this.cbActiveDay.Size = new System.Drawing.Size(150, 23);
            this.cbActiveDay.TabIndex = 5;
            this.cbActiveDay.SelectedIndexChanged += new System.EventHandler(this.cbActiveDay_SelectedIndexChanged);

            // 
            // btnAutoSchedule
            // 
            this.btnAutoSchedule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(115)))), ((int)(((byte)(232)))));
            this.btnAutoSchedule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAutoSchedule.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnAutoSchedule.ForeColor = System.Drawing.Color.White;
            this.btnAutoSchedule.Location = new System.Drawing.Point(500, 8);
            this.btnAutoSchedule.Name = "btnAutoSchedule";
            this.btnAutoSchedule.Size = new System.Drawing.Size(125, 27);
            this.btnAutoSchedule.TabIndex = 6;
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
            this.btnExportExcel.Location = new System.Drawing.Point(635, 8);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(155, 27);
            this.btnExportExcel.TabIndex = 7;
            this.btnExportExcel.Text = "Xuất Excel & Gửi Zalo";
            this.btnExportExcel.UseVisualStyleBackColor = false;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);

            // 
            // lblBudgetStatus
            // 
            this.lblBudgetStatus.AutoSize = true;
            this.lblBudgetStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblBudgetStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.lblBudgetStatus.Location = new System.Drawing.Point(800, 14);
            this.lblBudgetStatus.Name = "lblBudgetStatus";
            this.lblBudgetStatus.Size = new System.Drawing.Size(155, 15);
            this.lblBudgetStatus.TabIndex = 8;
            this.lblBudgetStatus.Text = "Đã xếp: 0 giờ | Còn lại: 0 giờ";

            // 
            // panelSidebar
            // 
            this.panelSidebar.Controls.Add(this.groupSidebar);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 45);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Padding = new System.Windows.Forms.Padding(10);
            this.panelSidebar.Size = new System.Drawing.Size(300, 605);
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
            this.groupSidebar.Size = new System.Drawing.Size(280, 585);
            this.groupSidebar.TabIndex = 0;
            this.groupSidebar.TabStop = false;
            this.groupSidebar.Text = "DANH SÁCH NHÂN SỰ";

            // 
            // btnAddEmployee
            // 
            this.btnAddEmployee.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAddEmployee.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddEmployee.FlatAppearance.BorderSize = 0;
            this.btnAddEmployee.BackColor = System.Drawing.Color.FromArgb(220, 230, 245);
            this.btnAddEmployee.ForeColor = System.Drawing.Color.FromArgb(10, 50, 110);
            this.btnAddEmployee.Location = new System.Drawing.Point(8, 47);
            this.btnAddEmployee.Name = "btnAddEmployee";
            this.btnAddEmployee.Size = new System.Drawing.Size(264, 25);
            this.btnAddEmployee.TabIndex = 2;
            this.btnAddEmployee.Text = "+ Thêm Nhân Viên Mới";
            this.btnAddEmployee.UseVisualStyleBackColor = false;
            this.btnAddEmployee.Click += new System.EventHandler(this.btnAddEmployee_Click);

            // 
            // flowEmployeePool
            // 
            this.flowEmployeePool.AutoScroll = true;
            this.flowEmployeePool.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowEmployeePool.Location = new System.Drawing.Point(8, 51);
            this.flowEmployeePool.Name = "flowEmployeePool";
            this.flowEmployeePool.Size = new System.Drawing.Size(264, 526);
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
            this.panelMain.Location = new System.Drawing.Point(300, 45);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(5, 10, 10, 10);
            this.panelMain.Size = new System.Drawing.Size(708, 605);
            this.panelMain.TabIndex = 2;

            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageSchedule);
            this.tabControlMain.Controls.Add(this.tabPageChart);
            this.tabControlMain.Controls.Add(this.tabPageHolidays);
            this.tabControlMain.Controls.Add(this.tabPageSettings);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.tabControlMain.Location = new System.Drawing.Point(5, 10);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(693, 585);
            this.tabControlMain.TabIndex = 0;

            // 
            // tabPageSchedule
            // 
            this.tabPageSchedule.Controls.Add(this.tableLayoutPanelShifts);
            this.tabPageSchedule.Location = new System.Drawing.Point(4, 24);
            this.tabPageSchedule.Name = "tabPageSchedule";
            this.tabPageSchedule.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSchedule.Size = new System.Drawing.Size(685, 557);
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
            this.tabPageChart.Size = new System.Drawing.Size(685, 557);
            this.tabPageChart.TabIndex = 1;
            this.tabPageChart.Text = "BIỂU ĐỒ THỐNG KÊ";
            this.tabPageChart.UseVisualStyleBackColor = true;

            // 
            // tabPageHolidays
            // 
            this.tabPageHolidays.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.tabPageHolidays.Controls.Add(this.panelCalendarContainer);
            this.tabPageHolidays.Location = new System.Drawing.Point(4, 24);
            this.tabPageHolidays.Name = "tabPageHolidays";
            this.tabPageHolidays.Padding = new System.Windows.Forms.Padding(5);
            this.tabPageHolidays.Size = new System.Drawing.Size(685, 557);
            this.tabPageHolidays.TabIndex = 2;
            this.tabPageHolidays.Text = "NGÀY NGHỈ CÔNG TY";

            // 
            // tabPageSettings
            // 
            this.tabPageSettings.Controls.Add(this.grpScheduleConfig);
            this.tabPageSettings.Controls.Add(this.grpZaloConfig);
            this.tabPageSettings.Location = new System.Drawing.Point(4, 24);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Padding = new System.Windows.Forms.Padding(10);
            this.tabPageSettings.Size = new System.Drawing.Size(685, 557);
            this.tabPageSettings.TabIndex = 3;
            this.tabPageSettings.Text = "CÀI ĐẶT CẤU HÌNH";
            this.tabPageSettings.UseVisualStyleBackColor = true;

            // 
            // tableLayoutPanelShifts - 4 CA
            // 
            this.tableLayoutPanelShifts.ColumnCount = 4;
            this.tableLayoutPanelShifts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelShifts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelShifts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelShifts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelShifts.Controls.Add(this.groupDayShift, 0, 0);
            this.tableLayoutPanelShifts.Controls.Add(this.groupCa2Shift, 1, 0);
            this.tableLayoutPanelShifts.Controls.Add(this.groupNightShift, 2, 0);
            this.tableLayoutPanelShifts.Controls.Add(this.groupAdminShift, 3, 0);
            this.tableLayoutPanelShifts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelShifts.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelShifts.Name = "tableLayoutPanelShifts";
            this.tableLayoutPanelShifts.RowCount = 1;
            this.tableLayoutPanelShifts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelShifts.Size = new System.Drawing.Size(679, 551);
            this.tableLayoutPanelShifts.TabIndex = 0;

            // 
            // groupAdminShift - HÀNH CHÍNH (Xanh lá)
            // 
            this.groupAdminShift.Controls.Add(this.flowAdminShift);
            this.groupAdminShift.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupAdminShift.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupAdminShift.ForeColor = System.Drawing.Color.FromArgb(27, 94, 32);
            this.groupAdminShift.Location = new System.Drawing.Point(510, 3);
            this.groupAdminShift.Name = "groupAdminShift";
            this.groupAdminShift.Padding = new System.Windows.Forms.Padding(4, 6, 4, 4);
            this.groupAdminShift.Size = new System.Drawing.Size(163, 545);
            this.groupAdminShift.TabIndex = 3;
            this.groupAdminShift.TabStop = false;
            this.groupAdminShift.Text = "HÀNH CHÍNH  (08:00 ~ 17:00)";

            // 
            // flowAdminShift
            // 
            this.flowAdminShift.AllowDrop = true;
            this.flowAdminShift.AutoScroll = true;
            this.flowAdminShift.BackColor = System.Drawing.Color.FromArgb(232, 245, 233);
            this.flowAdminShift.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowAdminShift.Location = new System.Drawing.Point(4, 22);
            this.flowAdminShift.Name = "flowAdminShift";
            this.flowAdminShift.Size = new System.Drawing.Size(155, 519);
            this.flowAdminShift.TabIndex = 0;
            this.flowAdminShift.DragDrop += new System.Windows.Forms.DragEventHandler(this.flowShift_DragDrop);
            this.flowAdminShift.DragEnter += new System.Windows.Forms.DragEventHandler(this.flowShift_DragEnter);

            // 
            // groupDayShift - CA 1 (Xanh dương)
            // 
            this.groupDayShift.Controls.Add(this.flowDayShift);
            this.groupDayShift.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupDayShift.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupDayShift.ForeColor = System.Drawing.Color.FromArgb(13, 71, 161);
            this.groupDayShift.Location = new System.Drawing.Point(3, 3);
            this.groupDayShift.Name = "groupDayShift";
            this.groupDayShift.Padding = new System.Windows.Forms.Padding(4, 6, 4, 4);
            this.groupDayShift.Size = new System.Drawing.Size(163, 545);
            this.groupDayShift.TabIndex = 0;
            this.groupDayShift.TabStop = false;
            this.groupDayShift.Text = "CA 1  (06:00 ~ 14:00)";

            // 
            // flowDayShift
            // 
            this.flowDayShift.AllowDrop = true;
            this.flowDayShift.AutoScroll = true;
            this.flowDayShift.BackColor = System.Drawing.Color.FromArgb(227, 242, 253);
            this.flowDayShift.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowDayShift.Location = new System.Drawing.Point(4, 22);
            this.flowDayShift.Name = "flowDayShift";
            this.flowDayShift.Size = new System.Drawing.Size(155, 519);
            this.flowDayShift.TabIndex = 0;
            this.flowDayShift.DragDrop += new System.Windows.Forms.DragEventHandler(this.flowShift_DragDrop);
            this.flowDayShift.DragEnter += new System.Windows.Forms.DragEventHandler(this.flowShift_DragEnter);

            // 
            // groupCa2Shift - CA 2 CHIỀU (Cam nhạt)
            // 
            this.groupCa2Shift.Controls.Add(this.flowCa2Shift);
            this.groupCa2Shift.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupCa2Shift.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupCa2Shift.ForeColor = System.Drawing.Color.FromArgb(230, 81, 0);
            this.groupCa2Shift.Location = new System.Drawing.Point(172, 3);
            this.groupCa2Shift.Name = "groupCa2Shift";
            this.groupCa2Shift.Padding = new System.Windows.Forms.Padding(4, 6, 4, 4);
            this.groupCa2Shift.Size = new System.Drawing.Size(163, 545);
            this.groupCa2Shift.TabIndex = 1;
            this.groupCa2Shift.TabStop = false;
            this.groupCa2Shift.Text = "CA 2  (14:00 ~ 22:00)";

            // 
            // flowCa2Shift
            // 
            this.flowCa2Shift.AllowDrop = true;
            this.flowCa2Shift.AutoScroll = true;
            this.flowCa2Shift.BackColor = System.Drawing.Color.FromArgb(255, 243, 224);
            this.flowCa2Shift.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowCa2Shift.Location = new System.Drawing.Point(4, 22);
            this.flowCa2Shift.Name = "flowCa2Shift";
            this.flowCa2Shift.Size = new System.Drawing.Size(155, 519);
            this.flowCa2Shift.TabIndex = 0;
            this.flowCa2Shift.DragDrop += new System.Windows.Forms.DragEventHandler(this.flowShift_DragDrop);
            this.flowCa2Shift.DragEnter += new System.Windows.Forms.DragEventHandler(this.flowShift_DragEnter);

            // 
            // groupNightShift - CA 3 ĐÊM (Tím)
            // 
            this.groupNightShift.Controls.Add(this.flowNightShift);
            this.groupNightShift.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupNightShift.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupNightShift.ForeColor = System.Drawing.Color.FromArgb(74, 20, 140);
            this.groupNightShift.Location = new System.Drawing.Point(341, 3);
            this.groupNightShift.Name = "groupNightShift";
            this.groupNightShift.Padding = new System.Windows.Forms.Padding(4, 6, 4, 4);
            this.groupNightShift.Size = new System.Drawing.Size(163, 545);
            this.groupNightShift.TabIndex = 2;
            this.groupNightShift.TabStop = false;
            this.groupNightShift.Text = "CA 3  (22:00 ~ 06:00)";

            // 
            // flowNightShift
            // 
            this.flowNightShift.AllowDrop = true;
            this.flowNightShift.AutoScroll = true;
            this.flowNightShift.BackColor = System.Drawing.Color.FromArgb(237, 231, 246);
            this.flowNightShift.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowNightShift.Location = new System.Drawing.Point(4, 22);
            this.flowNightShift.Name = "flowNightShift";
            this.flowNightShift.Size = new System.Drawing.Size(155, 519);
            this.flowNightShift.TabIndex = 0;
            this.flowNightShift.DragDrop += new System.Windows.Forms.DragEventHandler(this.flowShift_DragDrop);
            this.flowNightShift.DragEnter += new System.Windows.Forms.DragEventHandler(this.flowShift_DragEnter);
            // 
            // overtimeChart
            // 
            this.overtimeChart.BackColor = System.Drawing.Color.White;
            this.overtimeChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.overtimeChart.Location = new System.Drawing.Point(3, 3);
            this.overtimeChart.Name = "overtimeChart";
            this.overtimeChart.Size = new System.Drawing.Size(679, 551);
            this.overtimeChart.TabIndex = 0;

            // 
            // panelCalendarContainer
            // 
            this.panelCalendarContainer.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelCalendarContainer.BackColor = System.Drawing.Color.White;
            this.panelCalendarContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCalendarContainer.Controls.Add(this.tblCalendar);
            this.panelCalendarContainer.Controls.Add(this.panelHolidayHeader);
            this.panelCalendarContainer.Location = new System.Drawing.Point(67, 38);
            this.panelCalendarContainer.Name = "panelCalendarContainer";
            this.panelCalendarContainer.Padding = new System.Windows.Forms.Padding(5);
            this.panelCalendarContainer.Size = new System.Drawing.Size(550, 480);
            this.panelCalendarContainer.TabIndex = 0;

            // 
            // panelHolidayHeader
            // 
            this.panelHolidayHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.panelHolidayHeader.Controls.Add(this.btnPrevMonth);
            this.panelHolidayHeader.Controls.Add(this.lblMonthYear);
            this.panelHolidayHeader.Controls.Add(this.btnNextMonth);
            this.panelHolidayHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHolidayHeader.Location = new System.Drawing.Point(5, 5);
            this.panelHolidayHeader.Name = "panelHolidayHeader";
            this.panelHolidayHeader.Size = new System.Drawing.Size(538, 45);
            this.panelHolidayHeader.TabIndex = 0;

            // 
            // btnPrevMonth
            // 
            this.btnPrevMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevMonth.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnPrevMonth.Location = new System.Drawing.Point(10, 8);
            this.btnPrevMonth.Name = "btnPrevMonth";
            this.btnPrevMonth.Size = new System.Drawing.Size(40, 28);
            this.btnPrevMonth.TabIndex = 0;
            this.btnPrevMonth.Text = "<";
            this.btnPrevMonth.UseVisualStyleBackColor = true;
            this.btnPrevMonth.Click += new System.EventHandler(this.btnPrevMonth_Click);

            // 
            // lblMonthYear
            // 
            this.lblMonthYear.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMonthYear.Location = new System.Drawing.Point(56, 8);
            this.lblMonthYear.Name = "lblMonthYear";
            this.lblMonthYear.Size = new System.Drawing.Size(200, 28);
            this.lblMonthYear.TabIndex = 1;
            this.lblMonthYear.Text = "Tháng 12 - 2026";
            this.lblMonthYear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // btnNextMonth
            // 
            this.btnNextMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextMonth.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnNextMonth.Location = new System.Drawing.Point(262, 8);
            this.btnNextMonth.Name = "btnNextMonth";
            this.btnNextMonth.Size = new System.Drawing.Size(40, 28);
            this.btnNextMonth.TabIndex = 2;
            this.btnNextMonth.Text = ">";
            this.btnNextMonth.UseVisualStyleBackColor = true;
            this.btnNextMonth.Click += new System.EventHandler(this.btnNextMonth_Click);

            // 
            // tblCalendar
            // 
            this.tblCalendar.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tblCalendar.ColumnCount = 7;
            this.tblCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblCalendar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblCalendar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblCalendar.Location = new System.Drawing.Point(5, 50);
            this.tblCalendar.Name = "tblCalendar";
            this.tblCalendar.RowCount = 7;
            this.tblCalendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblCalendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblCalendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblCalendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblCalendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblCalendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblCalendar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblCalendar.Size = new System.Drawing.Size(538, 423);
            this.tblCalendar.TabIndex = 1;

            // 
            // grpScheduleConfig
            // 
            this.grpScheduleConfig.Controls.Add(this.lblMaxPerShift);
            this.grpScheduleConfig.Controls.Add(this.numMaxPerShift);
            this.grpScheduleConfig.Controls.Add(this.lblMonthlyBudget);
            this.grpScheduleConfig.Controls.Add(this.numMonthlyBudget);
            this.grpScheduleConfig.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpScheduleConfig.Location = new System.Drawing.Point(20, 20);
            this.grpScheduleConfig.Name = "grpScheduleConfig";
            this.grpScheduleConfig.Size = new System.Drawing.Size(300, 120);
            this.grpScheduleConfig.TabIndex = 0;
            this.grpScheduleConfig.TabStop = false;
            this.grpScheduleConfig.Text = "Cấu hình thuật toán xếp ca";

            // 
            // lblMaxPerShift
            // 
            this.lblMaxPerShift.AutoSize = true;
            this.lblMaxPerShift.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblMaxPerShift.Location = new System.Drawing.Point(12, 30);
            this.lblMaxPerShift.Name = "lblMaxPerShift";
            this.lblMaxPerShift.Size = new System.Drawing.Size(107, 17);
            this.lblMaxPerShift.TabIndex = 1;
            this.lblMaxPerShift.Text = "Số người tối đa/ca:";

            // 
            // numMaxPerShift
            // 
            this.numMaxPerShift.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numMaxPerShift.Location = new System.Drawing.Point(160, 26);
            this.numMaxPerShift.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numMaxPerShift.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            this.numMaxPerShift.Name = "numMaxPerShift";
            this.numMaxPerShift.Size = new System.Drawing.Size(60, 25);
            this.numMaxPerShift.TabIndex = 2;
            this.numMaxPerShift.Value = new decimal(new int[] { 2, 0, 0, 0 });
            this.numMaxPerShift.ValueChanged += new System.EventHandler(this.numMaxPerShift_ValueChanged);

            // 
            // lblMonthlyBudget
            // 
            this.lblMonthlyBudget.AutoSize = true;
            this.lblMonthlyBudget.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblMonthlyBudget.Location = new System.Drawing.Point(12, 68);
            this.lblMonthlyBudget.Name = "lblMonthlyBudget";
            this.lblMonthlyBudget.Size = new System.Drawing.Size(115, 17);
            this.lblMonthlyBudget.TabIndex = 3;
            this.lblMonthlyBudget.Text = "Quỹ 1 tháng (giờ):";

            // 
            // numMonthlyBudget
            // 
            this.numMonthlyBudget.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numMonthlyBudget.Location = new System.Drawing.Point(160, 64);
            this.numMonthlyBudget.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            this.numMonthlyBudget.Name = "numMonthlyBudget";
            this.numMonthlyBudget.Size = new System.Drawing.Size(60, 25);
            this.numMonthlyBudget.TabIndex = 4;
            this.numMonthlyBudget.Value = new decimal(new int[] { 200, 0, 0, 0 });
            this.numMonthlyBudget.ValueChanged += new System.EventHandler(this.numMonthlyBudget_ValueChanged);

            // 
            // grpZaloConfig
            // 
            this.grpZaloConfig.Controls.Add(this.lblZalo);
            this.grpZaloConfig.Controls.Add(this.txtZaloGroupName);
            this.grpZaloConfig.Controls.Add(this.btnToggleZalo);
            this.grpZaloConfig.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpZaloConfig.Location = new System.Drawing.Point(340, 20);
            this.grpZaloConfig.Name = "grpZaloConfig";
            this.grpZaloConfig.Size = new System.Drawing.Size(300, 160);
            this.grpZaloConfig.TabIndex = 1;
            this.grpZaloConfig.TabStop = false;
            this.grpZaloConfig.Text = "Cài đặt kết nối Zalo";

            // 
            // lblZalo
            // 
            this.lblZalo.AutoSize = true;
            this.lblZalo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblZalo.Location = new System.Drawing.Point(15, 35);
            this.lblZalo.Name = "lblZalo";
            this.lblZalo.Size = new System.Drawing.Size(135, 17);
            this.lblZalo.TabIndex = 0;
            this.lblZalo.Text = "Nhóm Zalo nhận lịch:";

            // 
            // txtZaloGroupName
            // 
            this.txtZaloGroupName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtZaloGroupName.Location = new System.Drawing.Point(15, 55);
            this.txtZaloGroupName.Name = "txtZaloGroupName";
            this.txtZaloGroupName.Size = new System.Drawing.Size(260, 25);
            this.txtZaloGroupName.TabIndex = 1;
            this.txtZaloGroupName.Text = "Test Group";

            // 
            // btnToggleZalo
            // 
            this.btnToggleZalo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(156)))));
            this.btnToggleZalo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleZalo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnToggleZalo.ForeColor = System.Drawing.Color.White;
            this.btnToggleZalo.Location = new System.Drawing.Point(15, 95);
            this.btnToggleZalo.Name = "btnToggleZalo";
            this.btnToggleZalo.Size = new System.Drawing.Size(150, 30);
            this.btnToggleZalo.TabIndex = 2;
            this.btnToggleZalo.Text = "Bật Zalo Bot";
            this.btnToggleZalo.UseVisualStyleBackColor = false;
            this.btnToggleZalo.Click += new System.EventHandler(this.btnToggleZalo_Click);

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
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelSidebar.ResumeLayout(false);
            this.groupSidebar.ResumeLayout(false);
            this.groupSidebar.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageSchedule.ResumeLayout(false);
            this.tabPageChart.ResumeLayout(false);
            this.tabPageHolidays.ResumeLayout(false);
            this.tabPageSettings.ResumeLayout(false);
            this.tableLayoutPanelShifts.ResumeLayout(false);
            this.groupAdminShift.ResumeLayout(false);
            this.groupNightShift.ResumeLayout(false);
            this.groupCa2Shift.ResumeLayout(false);
            this.groupDayShift.ResumeLayout(false);
            this.panelCalendarContainer.ResumeLayout(false);
            this.panelHolidayHeader.ResumeLayout(false);
            this.grpScheduleConfig.ResumeLayout(false);
            this.grpScheduleConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxPerShift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMonthlyBudget)).EndInit();
            this.grpZaloConfig.ResumeLayout(false);
            this.grpZaloConfig.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label lblActiveDay;
        private System.Windows.Forms.ComboBox cbActiveDay;
        private System.Windows.Forms.Button btnAutoSchedule;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Label lblBudgetStatus;

        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.GroupBox groupSidebar;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.FlowLayoutPanel flowEmployeePool;

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageSchedule;
        private System.Windows.Forms.TabPage tabPageChart;
        private System.Windows.Forms.TabPage tabPageHolidays;
        private System.Windows.Forms.TabPage tabPageSettings;

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelShifts;
        private System.Windows.Forms.GroupBox groupDayShift;
        private System.Windows.Forms.FlowLayoutPanel flowDayShift;
        private System.Windows.Forms.GroupBox groupCa2Shift;
        private System.Windows.Forms.FlowLayoutPanel flowCa2Shift;
        private System.Windows.Forms.GroupBox groupNightShift;
        private System.Windows.Forms.FlowLayoutPanel flowNightShift;
        private System.Windows.Forms.GroupBox groupAdminShift;
        private System.Windows.Forms.FlowLayoutPanel flowAdminShift;
        private OvertimeScheduler.Forms.OvertimeChart overtimeChart;

        private System.Windows.Forms.Panel panelCalendarContainer;
        private System.Windows.Forms.Panel panelHolidayHeader;
        private System.Windows.Forms.Button btnPrevMonth;
        private System.Windows.Forms.Label lblMonthYear;
        private System.Windows.Forms.Button btnNextMonth;
        private System.Windows.Forms.TableLayoutPanel tblCalendar;

        private System.Windows.Forms.GroupBox grpScheduleConfig;
        private System.Windows.Forms.Label lblMaxPerShift;
        private System.Windows.Forms.NumericUpDown numMaxPerShift;
        private System.Windows.Forms.Label lblMonthlyBudget;
        private System.Windows.Forms.NumericUpDown numMonthlyBudget;

        private System.Windows.Forms.GroupBox grpZaloConfig;
        private System.Windows.Forms.Label lblZalo;
        private System.Windows.Forms.TextBox txtZaloGroupName;
        private System.Windows.Forms.Button btnToggleZalo;
        private System.Windows.Forms.Button btnAddEmployee;
    }
}
