/*
 * 由SharpDevelop创建。
 * 用户： XiaoSanya
 * 日期: 2015/4/11
 * 时间: 19:06
 */
namespace InHinhAnh
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_deleteSettings = new System.Windows.Forms.Button();
            this.btn_loadSettings = new System.Windows.Forms.Button();
            this.btn_saveSettings = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.selected_parper = new System.Windows.Forms.ComboBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxInterval = new System.Windows.Forms.TextBox();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.labelFolder = new System.Windows.Forms.Label();
            this.textBoxInfo = new System.Windows.Forms.TextBox();
            this.checkBox_StartAtWindows = new System.Windows.Forms.CheckBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.status_countPrinted = new System.Windows.Forms.ToolStripStatusLabel();
            this.text_user = new System.Windows.Forms.Label();
            this.checkedList_mayin = new System.Windows.Forms.CheckedListBox();
            this.checkBox_minimizeStartup = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_deleteSettings);
            this.groupBox1.Controls.Add(this.btn_loadSettings);
            this.groupBox1.Controls.Add(this.btn_saveSettings);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.selected_parper);
            this.groupBox1.Controls.Add(this.buttonStart);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxInterval);
            this.groupBox1.Controls.Add(this.buttonSelect);
            this.groupBox1.Controls.Add(this.labelFolder);
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(786, 96);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // btn_deleteSettings
            // 
            this.btn_deleteSettings.Location = new System.Drawing.Point(519, 61);
            this.btn_deleteSettings.Name = "btn_deleteSettings";
            this.btn_deleteSettings.Size = new System.Drawing.Size(103, 23);
            this.btn_deleteSettings.TabIndex = 12;
            this.btn_deleteSettings.Text = "Delete Settings";
            this.btn_deleteSettings.UseVisualStyleBackColor = true;
            this.btn_deleteSettings.Click += new System.EventHandler(this.btn_deleteSettings_Click);
            // 
            // btn_loadSettings
            // 
            this.btn_loadSettings.Location = new System.Drawing.Point(519, 28);
            this.btn_loadSettings.Name = "btn_loadSettings";
            this.btn_loadSettings.Size = new System.Drawing.Size(103, 23);
            this.btn_loadSettings.TabIndex = 12;
            this.btn_loadSettings.Text = "Load Settings";
            this.btn_loadSettings.UseVisualStyleBackColor = true;
            this.btn_loadSettings.Click += new System.EventHandler(this.btn_loadSettings_Click);
            // 
            // btn_saveSettings
            // 
            this.btn_saveSettings.Location = new System.Drawing.Point(405, 61);
            this.btn_saveSettings.Name = "btn_saveSettings";
            this.btn_saveSettings.Size = new System.Drawing.Size(107, 23);
            this.btn_saveSettings.TabIndex = 12;
            this.btn_saveSettings.Text = "Save Settings";
            this.btn_saveSettings.UseVisualStyleBackColor = true;
            this.btn_saveSettings.Click += new System.EventHandler(this.btn_saveSettings_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(243, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Pager Size:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Chọn thư mục chứa file PDF:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Chu kỳ";
            // 
            // selected_parper
            // 
            this.selected_parper.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selected_parper.FormattingEnabled = true;
            this.selected_parper.Items.AddRange(new object[] {
            "A3",
            "A4",
            "A5",
            "A6"});
            this.selected_parper.Location = new System.Drawing.Point(310, 62);
            this.selected_parper.Name = "selected_parper";
            this.selected_parper.Size = new System.Drawing.Size(87, 21);
            this.selected_parper.TabIndex = 8;
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(635, 29);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(136, 55);
            this.buttonStart.TabIndex = 7;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.ButtonStartClick);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(95, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 22);
            this.label2.TabIndex = 6;
            this.label2.Text = "s";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxInterval
            // 
            this.textBoxInterval.Location = new System.Drawing.Point(55, 63);
            this.textBoxInterval.Name = "textBoxInterval";
            this.textBoxInterval.Size = new System.Drawing.Size(40, 20);
            this.textBoxInterval.TabIndex = 5;
            this.textBoxInterval.Text = "30";
            // 
            // buttonSelect
            // 
            this.buttonSelect.Location = new System.Drawing.Point(405, 28);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(108, 23);
            this.buttonSelect.TabIndex = 2;
            this.buttonSelect.Text = "Select";
            this.buttonSelect.UseVisualStyleBackColor = true;
            this.buttonSelect.Click += new System.EventHandler(this.Button1Click);
            // 
            // labelFolder
            // 
            this.labelFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelFolder.Location = new System.Drawing.Point(6, 29);
            this.labelFolder.Name = "labelFolder";
            this.labelFolder.Size = new System.Drawing.Size(391, 22);
            this.labelFolder.TabIndex = 1;
            this.labelFolder.Text = "Chưa chọn";
            this.labelFolder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxInfo
            // 
            this.textBoxInfo.Location = new System.Drawing.Point(12, 115);
            this.textBoxInfo.Multiline = true;
            this.textBoxInfo.Name = "textBoxInfo";
            this.textBoxInfo.ReadOnly = true;
            this.textBoxInfo.Size = new System.Drawing.Size(513, 259);
            this.textBoxInfo.TabIndex = 2;
            // 
            // checkBox_StartAtWindows
            // 
            this.checkBox_StartAtWindows.Location = new System.Drawing.Point(593, 376);
            this.checkBox_StartAtWindows.Name = "checkBox_StartAtWindows";
            this.checkBox_StartAtWindows.Size = new System.Drawing.Size(205, 28);
            this.checkBox_StartAtWindows.TabIndex = 3;
            this.checkBox_StartAtWindows.Text = "Start and Autorun at Windows Startup";
            this.checkBox_StartAtWindows.UseVisualStyleBackColor = true;
            this.checkBox_StartAtWindows.CheckedChanged += new System.EventHandler(this.checkBox_StartAtWindows_CheckedChanged);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.status_countPrinted});
            this.statusStrip1.Location = new System.Drawing.Point(0, 400);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(824, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(59, 17);
            this.toolStripStatusLabel1.Text = "Số bản in:";
            // 
            // status_countPrinted
            // 
            this.status_countPrinted.Name = "status_countPrinted";
            this.status_countPrinted.Size = new System.Drawing.Size(13, 17);
            this.status_countPrinted.Text = "0";
            // 
            // text_user
            // 
            this.text_user.AutoSize = true;
            this.text_user.Location = new System.Drawing.Point(9, 383);
            this.text_user.Name = "text_user";
            this.text_user.Size = new System.Drawing.Size(32, 13);
            this.text_user.TabIndex = 5;
            this.text_user.Text = "User:";
            // 
            // checkedList_mayin
            // 
            this.checkedList_mayin.FormattingEnabled = true;
            this.checkedList_mayin.Location = new System.Drawing.Point(554, 115);
            this.checkedList_mayin.Name = "checkedList_mayin";
            this.checkedList_mayin.Size = new System.Drawing.Size(244, 259);
            this.checkedList_mayin.TabIndex = 6;
            // 
            // checkBox_minimizeStartup
            // 
            this.checkBox_minimizeStartup.Location = new System.Drawing.Point(468, 376);
            this.checkBox_minimizeStartup.Name = "checkBox_minimizeStartup";
            this.checkBox_minimizeStartup.Size = new System.Drawing.Size(119, 28);
            this.checkBox_minimizeStartup.TabIndex = 3;
            this.checkBox_minimizeStartup.Text = "Minimize at Startup";
            this.checkBox_minimizeStartup.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 422);
            this.Controls.Add(this.checkedList_mayin);
            this.Controls.Add(this.text_user);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.textBoxInfo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkBox_minimizeStartup);
            this.Controls.Add(this.checkBox_StartAtWindows);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "In Hình Ảnh Tự Động";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.Button buttonStart;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxInterval;
		private System.Windows.Forms.Button buttonSelect;
		private System.Windows.Forms.TextBox textBoxInfo;
		private System.Windows.Forms.Label labelFolder;
		private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox_StartAtWindows;
        private System.Windows.Forms.ComboBox selected_parper;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label text_user;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripStatusLabel status_countPrinted;
        private System.Windows.Forms.CheckedListBox checkedList_mayin;
        private System.Windows.Forms.Button btn_loadSettings;
        private System.Windows.Forms.Button btn_saveSettings;
        private System.Windows.Forms.CheckBox checkBox_minimizeStartup;
        private System.Windows.Forms.Button btn_deleteSettings;
    }
}
