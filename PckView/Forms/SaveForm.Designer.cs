/*namespace PckView
{
	partial class SaveForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.pckSave = new System.Windows.Forms.TabControl();
			this.tab1 = new System.Windows.Forms.TabPage();
			this.btnPckSave = new System.Windows.Forms.Button();
			this.btnPckSelect = new System.Windows.Forms.Button();
			this.txtPckPath = new System.Windows.Forms.TextBox();
			this.radio4bpp = new System.Windows.Forms.RadioButton();
			this.radio2bpp = new System.Windows.Forms.RadioButton();
			this.bmpTab = new System.Windows.Forms.TabPage();
			this.radio24 = new System.Windows.Forms.RadioButton();
			this.radio8 = new System.Windows.Forms.RadioButton();
			this.btnBmpSave = new System.Windows.Forms.Button();
			this.btnBmpSelect = new System.Windows.Forms.Button();
			this.txtBmpPath = new System.Windows.Forms.TextBox();
			this.savePck = new System.Windows.Forms.SaveFileDialog();
			this.saveBmp = new System.Windows.Forms.SaveFileDialog();
			this.saveTabs = new System.Windows.Forms.TabControl();
			this.tabSingle = new System.Windows.Forms.TabPage();
			this.tabsSingle = new System.Windows.Forms.TabControl();
			this.tabMulti = new System.Windows.Forms.TabPage();
			this.tabsMulti = new System.Windows.Forms.TabControl();
			this.tabOld = new System.Windows.Forms.TabPage();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.topPanel = new System.Windows.Forms.Panel();
			this.txtOut = new System.Windows.Forms.TextBox();
			this.btnFind = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.pckSave.SuspendLayout();
			this.tab1.SuspendLayout();
			this.bmpTab.SuspendLayout();
			this.saveTabs.SuspendLayout();
			this.tabSingle.SuspendLayout();
			this.tabMulti.SuspendLayout();
			this.tabOld.SuspendLayout();
			this.panel1.SuspendLayout();
			this.topPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// pckSave
			// 
			this.pckSave.Controls.Add(this.tab1);
			this.pckSave.Controls.Add(this.bmpTab);
			this.pckSave.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pckSave.Location = new System.Drawing.Point(0, 0);
			this.pckSave.Name = "pckSave";
			this.pckSave.SelectedIndex = 0;
			this.pckSave.Size = new System.Drawing.Size(522, 189);
			this.pckSave.TabIndex = 0;
			// 
			// tab1
			// 
			this.tab1.Controls.Add(this.btnPckSave);
			this.tab1.Controls.Add(this.btnPckSelect);
			this.tab1.Controls.Add(this.txtPckPath);
			this.tab1.Controls.Add(this.radio4bpp);
			this.tab1.Controls.Add(this.radio2bpp);
			this.tab1.Location = new System.Drawing.Point(4, 22);
			this.tab1.Name = "tab1";
			this.tab1.Size = new System.Drawing.Size(514, 163);
			this.tab1.TabIndex = 0;
			this.tab1.Text = "Pck";
			// 
			// btnPckSave
			// 
			this.btnPckSave.Location = new System.Drawing.Point(155, 72);
			this.btnPckSave.Name = "btnPckSave";
			this.btnPckSave.Size = new System.Drawing.Size(75, 23);
			this.btnPckSave.TabIndex = 4;
			this.btnPckSave.Text = "Save";
			this.btnPckSave.Click += new System.EventHandler(this.btnPckSave_Click);
			// 
			// btnPckSelect
			// 
			this.btnPckSelect.Location = new System.Drawing.Point(8, 8);
			this.btnPckSelect.Name = "btnPckSelect";
			this.btnPckSelect.Size = new System.Drawing.Size(48, 23);
			this.btnPckSelect.TabIndex = 3;
			this.btnPckSelect.Text = "Select";
			this.btnPckSelect.Click += new System.EventHandler(this.btnPckSelect_Click);
			// 
			// txtPckPath
			// 
			this.txtPckPath.Location = new System.Drawing.Point(64, 8);
			this.txtPckPath.Name = "txtPckPath";
			this.txtPckPath.Size = new System.Drawing.Size(312, 20);
			this.txtPckPath.TabIndex = 2;
			// 
			// radio4bpp
			// 
			this.radio4bpp.Location = new System.Drawing.Point(216, 40);
			this.radio4bpp.Name = "radio4bpp";
			this.radio4bpp.Size = new System.Drawing.Size(84, 32);
			this.radio4bpp.TabIndex = 1;
			this.radio4bpp.Text = "TFTD Units Bpp:4";
			// 
			// radio2bpp
			// 
			this.radio2bpp.Checked = true;
			this.radio2bpp.Location = new System.Drawing.Point(84, 40);
			this.radio2bpp.Name = "radio2bpp";
			this.radio2bpp.Size = new System.Drawing.Size(140, 32);
			this.radio2bpp.TabIndex = 0;
			this.radio2bpp.TabStop = true;
			this.radio2bpp.Text = "Terrain and UFO units Bpp:2";
			// 
			// bmpTab
			// 
			this.bmpTab.Controls.Add(this.radio24);
			this.bmpTab.Controls.Add(this.radio8);
			this.bmpTab.Controls.Add(this.btnBmpSave);
			this.bmpTab.Controls.Add(this.btnBmpSelect);
			this.bmpTab.Controls.Add(this.txtBmpPath);
			this.bmpTab.Location = new System.Drawing.Point(4, 22);
			this.bmpTab.Name = "bmpTab";
			this.bmpTab.Size = new System.Drawing.Size(514, 163);
			this.bmpTab.TabIndex = 1;
			this.bmpTab.Text = "Bmp";
			// 
			// radio24
			// 
			this.radio24.Location = new System.Drawing.Point(192, 40);
			this.radio24.Name = "radio24";
			this.radio24.Size = new System.Drawing.Size(56, 16);
			this.radio24.TabIndex = 4;
			this.radio24.Text = "24-bit";
			// 
			// radio8
			// 
			this.radio8.Checked = true;
			this.radio8.Location = new System.Drawing.Point(136, 40);
			this.radio8.Name = "radio8";
			this.radio8.Size = new System.Drawing.Size(56, 16);
			this.radio8.TabIndex = 3;
			this.radio8.TabStop = true;
			this.radio8.Text = "8-bit";
			// 
			// btnBmpSave
			// 
			this.btnBmpSave.Location = new System.Drawing.Point(155, 64);
			this.btnBmpSave.Name = "btnBmpSave";
			this.btnBmpSave.Size = new System.Drawing.Size(75, 23);
			this.btnBmpSave.TabIndex = 2;
			this.btnBmpSave.Text = "Save";
			this.btnBmpSave.Click += new System.EventHandler(this.btnBmpSave_Click);
			// 
			// btnBmpSelect
			// 
			this.btnBmpSelect.Location = new System.Drawing.Point(8, 8);
			this.btnBmpSelect.Name = "btnBmpSelect";
			this.btnBmpSelect.Size = new System.Drawing.Size(48, 23);
			this.btnBmpSelect.TabIndex = 1;
			this.btnBmpSelect.Text = "Select";
			this.btnBmpSelect.Click += new System.EventHandler(this.btnBmpSelect_Click);
			// 
			// txtBmpPath
			// 
			this.txtBmpPath.Location = new System.Drawing.Point(64, 8);
			this.txtBmpPath.Name = "txtBmpPath";
			this.txtBmpPath.Size = new System.Drawing.Size(312, 20);
			this.txtBmpPath.TabIndex = 0;
			// 
			// savePck
			// 
			this.savePck.Filter = "Pck files|*.pck";
			// 
			// saveBmp
			// 
			this.saveBmp.Filter = "Bmp files|*.bmp";
			// 
			// saveTabs
			// 
			this.saveTabs.Controls.Add(this.tabSingle);
			this.saveTabs.Controls.Add(this.tabMulti);
			this.saveTabs.Controls.Add(this.tabOld);
			this.saveTabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.saveTabs.Location = new System.Drawing.Point(0, 24);
			this.saveTabs.Name = "saveTabs";
			this.saveTabs.SelectedIndex = 0;
			this.saveTabs.Size = new System.Drawing.Size(530, 215);
			this.saveTabs.TabIndex = 1;
			// 
			// tabSingle
			// 
			this.tabSingle.Controls.Add(this.tabsSingle);
			this.tabSingle.Location = new System.Drawing.Point(4, 22);
			this.tabSingle.Name = "tabSingle";
			this.tabSingle.Size = new System.Drawing.Size(522, 189);
			this.tabSingle.TabIndex = 0;
			this.tabSingle.Text = "Specific Files";
			// 
			// tabsSingle
			// 
			this.tabsSingle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabsSingle.Location = new System.Drawing.Point(0, 0);
			this.tabsSingle.Name = "tabsSingle";
			this.tabsSingle.SelectedIndex = 0;
			this.tabsSingle.Size = new System.Drawing.Size(522, 189);
			this.tabsSingle.TabIndex = 0;
			// 
			// tabMulti
			// 
			this.tabMulti.Controls.Add(this.tabsMulti);
			this.tabMulti.Location = new System.Drawing.Point(4, 22);
			this.tabMulti.Name = "tabMulti";
			this.tabMulti.Size = new System.Drawing.Size(522, 189);
			this.tabMulti.TabIndex = 1;
			this.tabMulti.Text = "Generic Files";
			// 
			// tabsMulti
			// 
			this.tabsMulti.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabsMulti.Location = new System.Drawing.Point(0, 0);
			this.tabsMulti.Name = "tabsMulti";
			this.tabsMulti.SelectedIndex = 0;
			this.tabsMulti.Size = new System.Drawing.Size(522, 189);
			this.tabsMulti.TabIndex = 0;
			// 
			// tabOld
			// 
			this.tabOld.Controls.Add(this.pckSave);
			this.tabOld.Location = new System.Drawing.Point(4, 22);
			this.tabOld.Name = "tabOld";
			this.tabOld.Size = new System.Drawing.Size(522, 189);
			this.tabOld.TabIndex = 2;
			this.tabOld.Text = "Old";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnCancel);
			this.panel1.Controls.Add(this.btnSave);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 239);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(530, 24);
			this.panel1.TabIndex = 2;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnCancel.Location = new System.Drawing.Point(266, 0);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnSave.Location = new System.Drawing.Point(190, 0);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 0;
			this.btnSave.Text = "Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// topPanel
			// 
			this.topPanel.Controls.Add(this.txtOut);
			this.topPanel.Controls.Add(this.btnFind);
			this.topPanel.Controls.Add(this.label1);
			this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.topPanel.Location = new System.Drawing.Point(0, 0);
			this.topPanel.Name = "topPanel";
			this.topPanel.Size = new System.Drawing.Size(530, 24);
			this.topPanel.TabIndex = 5;
			// 
			// txtOut
			// 
			this.txtOut.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtOut.Location = new System.Drawing.Point(40, 0);
			this.txtOut.Name = "txtOut";
			this.txtOut.Size = new System.Drawing.Size(415, 20);
			this.txtOut.TabIndex = 1;
			// 
			// btnFind
			// 
			this.btnFind.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnFind.Location = new System.Drawing.Point(455, 0);
			this.btnFind.Name = "btnFind";
			this.btnFind.Size = new System.Drawing.Size(75, 24);
			this.btnFind.TabIndex = 2;
			this.btnFind.Text = "Select";
			this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Left;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "Output";
			// 
			// SaveForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(530, 263);
			this.Controls.Add(this.saveTabs);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.topPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SaveForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Save file";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.SaveForm_Closing);
			this.pckSave.ResumeLayout(false);
			this.tab1.ResumeLayout(false);
			this.tab1.PerformLayout();
			this.bmpTab.ResumeLayout(false);
			this.bmpTab.PerformLayout();
			this.saveTabs.ResumeLayout(false);
			this.tabSingle.ResumeLayout(false);
			this.tabMulti.ResumeLayout(false);
			this.tabOld.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.topPanel.ResumeLayout(false);
			this.topPanel.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion


		private System.Windows.Forms.TabControl pckSave;
		private System.Windows.Forms.TextBox txtBmpPath;
		private System.Windows.Forms.Button btnBmpSelect;
		private System.Windows.Forms.Button btnBmpSave;
		private System.Windows.Forms.RadioButton radio2bpp;
		private System.Windows.Forms.RadioButton radio4bpp;
		private System.Windows.Forms.Button btnPckSelect;
		private System.Windows.Forms.TextBox txtPckPath;
		private System.Windows.Forms.Button btnPckSave;
		private System.Windows.Forms.SaveFileDialog savePck;
		private System.Windows.Forms.SaveFileDialog saveBmp;
		private System.Windows.Forms.TabPage tab1;
		private System.Windows.Forms.TabPage bmpTab;
		private System.Windows.Forms.RadioButton radio8;
		private System.Windows.Forms.RadioButton radio24;
		private System.Windows.Forms.TabControl saveTabs;
		private System.Windows.Forms.TabPage tabSingle;
		private System.Windows.Forms.TabPage tabMulti;
		private System.Windows.Forms.TabPage tabOld;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TabControl tabsSingle;
		private System.Windows.Forms.TabControl tabsMulti;
		private System.Windows.Forms.Panel topPanel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtOut;
		private System.Windows.Forms.Button btnFind;
	}
}*/