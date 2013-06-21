namespace PckView
{
	partial class SaveProfileForm
	{
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
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
			this.lblHelp = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtOutDir = new System.Windows.Forms.TextBox();
			this.btnFindDir = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.txtInfo = new System.Windows.Forms.RichTextBox();
			this.saveFile = new System.Windows.Forms.SaveFileDialog();
			this.panelBottom = new System.Windows.Forms.Panel();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.cbPalette = new System.Windows.Forms.ComboBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.txtDesc = new System.Windows.Forms.TextBox();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.radioSingle = new System.Windows.Forms.RadioButton();
			this.radioAll = new System.Windows.Forms.RadioButton();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.panelBottom.SuspendLayout();
			this.panel1.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblHelp
			// 
			this.lblHelp.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblHelp.Location = new System.Drawing.Point(0, 0);
			this.lblHelp.Name = "lblHelp";
			this.lblHelp.Size = new System.Drawing.Size(474, 40);
			this.lblHelp.TabIndex = 0;
			this.lblHelp.Text = "Profiles saved in the \'custom\' directory will be automatically loaded upon progra" +
				"m startup";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtOutDir);
			this.groupBox1.Controls.Add(this.btnFindDir);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox1.Location = new System.Drawing.Point(0, 40);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(474, 42);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Save To";
			// 
			// txtOutDir
			// 
			this.txtOutDir.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtOutDir.Location = new System.Drawing.Point(3, 16);
			this.txtOutDir.Name = "txtOutDir";
			this.txtOutDir.Size = new System.Drawing.Size(393, 20);
			this.txtOutDir.TabIndex = 1;
			this.txtOutDir.Text = "custom/";
			// 
			// btnFindDir
			// 
			this.btnFindDir.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnFindDir.Location = new System.Drawing.Point(396, 16);
			this.btnFindDir.Name = "btnFindDir";
			this.btnFindDir.Size = new System.Drawing.Size(75, 23);
			this.btnFindDir.TabIndex = 0;
			this.btnFindDir.Text = "Locate";
			this.btnFindDir.Click += new System.EventHandler(this.btnFindDir_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.txtInfo);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new System.Drawing.Point(0, 235);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(474, 179);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Profile Information";
			// 
			// txtInfo
			// 
			this.txtInfo.BackColor = System.Drawing.SystemColors.Control;
			this.txtInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtInfo.Location = new System.Drawing.Point(3, 16);
			this.txtInfo.Name = "txtInfo";
			this.txtInfo.ReadOnly = true;
			this.txtInfo.Size = new System.Drawing.Size(468, 160);
			this.txtInfo.TabIndex = 0;
			this.txtInfo.Text = "";
			// 
			// saveFile
			// 
			this.saveFile.FileName = "doc1";
			this.saveFile.Filter = "Image Profiles|*.pvp";
			this.saveFile.InitialDirectory = ".\\custom\\";
			// 
			// panelBottom
			// 
			this.panelBottom.Controls.Add(this.btnCancel);
			this.panelBottom.Controls.Add(this.btnSave);
			this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelBottom.Location = new System.Drawing.Point(0, 414);
			this.panelBottom.Name = "panelBottom";
			this.panelBottom.Size = new System.Drawing.Size(474, 40);
			this.panelBottom.TabIndex = 1;
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(153, 9);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(65, 9);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 0;
			this.btnSave.Text = "Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.groupBox4);
			this.panel1.Controls.Add(this.groupBox3);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 82);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(474, 87);
			this.panel1.TabIndex = 1;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.cbPalette);
			this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox4.Location = new System.Drawing.Point(0, 44);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(474, 43);
			this.groupBox4.TabIndex = 4;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Default Palette";
			// 
			// cbPalette
			// 
			this.cbPalette.Dock = System.Windows.Forms.DockStyle.Left;
			this.cbPalette.FormattingEnabled = true;
			this.cbPalette.Location = new System.Drawing.Point(3, 16);
			this.cbPalette.Name = "cbPalette";
			this.cbPalette.Size = new System.Drawing.Size(165, 21);
			this.cbPalette.Sorted = true;
			this.cbPalette.TabIndex = 0;
			this.cbPalette.SelectedIndexChanged += new System.EventHandler(this.cbPalette_SelectedIndexChanged);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.txtDesc);
			this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox3.Location = new System.Drawing.Point(0, 0);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(474, 44);
			this.groupBox3.TabIndex = 3;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Explorer Description";
			// 
			// txtDesc
			// 
			this.txtDesc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtDesc.Location = new System.Drawing.Point(3, 16);
			this.txtDesc.Name = "txtDesc";
			this.txtDesc.Size = new System.Drawing.Size(468, 20);
			this.txtDesc.TabIndex = 2;
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.radioAll);
			this.groupBox5.Controls.Add(this.radioSingle);
			this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox5.Location = new System.Drawing.Point(0, 169);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(474, 66);
			this.groupBox5.TabIndex = 3;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Open what?";
			// 
			// radioSingle
			// 
			this.radioSingle.AutoSize = true;
			this.radioSingle.Location = new System.Drawing.Point(3, 19);
			this.radioSingle.Name = "radioSingle";
			this.radioSingle.Size = new System.Drawing.Size(124, 17);
			this.radioSingle.TabIndex = 0;
			this.radioSingle.Text = "Just this file ( file.ext )";
			this.radioSingle.UseVisualStyleBackColor = true;
			// 
			// radioAll
			// 
			this.radioAll.AutoSize = true;
			this.radioAll.Checked = true;
			this.radioAll.Location = new System.Drawing.Point(3, 42);
			this.radioAll.Name = "radioAll";
			this.radioAll.Size = new System.Drawing.Size(93, 17);
			this.radioAll.TabIndex = 1;
			this.radioAll.TabStop = true;
			this.radioAll.Text = "All files ( *.ext )";
			this.radioAll.UseVisualStyleBackColor = true;
			// 
			// SaveProfileForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(474, 454);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox5);
			this.Controls.Add(this.panelBottom);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.lblHelp);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "SaveProfileForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Save Profile";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.panelBottom.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

		private System.Windows.Forms.Label lblHelp;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnFindDir;
		private System.Windows.Forms.TextBox txtOutDir;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RichTextBox txtInfo;
		private System.Windows.Forms.SaveFileDialog saveFile;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Panel panelBottom;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.TextBox txtDesc;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.ComboBox cbPalette;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.RadioButton radioAll;
		private System.Windows.Forms.RadioButton radioSingle;
	}
}