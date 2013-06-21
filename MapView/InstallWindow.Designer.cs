namespace MapView
{
	partial class InstallWindow
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
			this.txtUFO = new System.Windows.Forms.TextBox();
			this.txtTFTD = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.btnFindUFO = new System.Windows.Forms.Button();
			this.btnFindTFTD = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.folderSelector = new System.Windows.Forms.FolderBrowserDialog();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtUFO
			// 
			this.txtUFO.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtUFO.Location = new System.Drawing.Point(88, 0);
			this.txtUFO.Name = "txtUFO";
			this.txtUFO.Size = new System.Drawing.Size(324, 20);
			this.txtUFO.TabIndex = 0;
			// 
			// txtTFTD
			// 
			this.txtTFTD.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtTFTD.Location = new System.Drawing.Point(88, 0);
			this.txtTFTD.Name = "txtTFTD";
			this.txtTFTD.Size = new System.Drawing.Size(324, 20);
			this.txtTFTD.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Left;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 25);
			this.label1.TabIndex = 2;
			this.label1.Text = "UFO Directory";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Dock = System.Windows.Forms.DockStyle.Left;
			this.label2.Location = new System.Drawing.Point(0, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(88, 25);
			this.label2.TabIndex = 3;
			this.label2.Text = "TFTD Directory";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnFindUFO
			// 
			this.btnFindUFO.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnFindUFO.Location = new System.Drawing.Point(412, 0);
			this.btnFindUFO.Name = "btnFindUFO";
			this.btnFindUFO.Size = new System.Drawing.Size(40, 25);
			this.btnFindUFO.TabIndex = 4;
			this.btnFindUFO.Text = "Find";
			this.btnFindUFO.Click += new System.EventHandler(this.btnFindUFO_Click);
			// 
			// btnFindTFTD
			// 
			this.btnFindTFTD.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnFindTFTD.Location = new System.Drawing.Point(412, 0);
			this.btnFindTFTD.Name = "btnFindTFTD";
			this.btnFindTFTD.Size = new System.Drawing.Size(40, 25);
			this.btnFindTFTD.TabIndex = 5;
			this.btnFindTFTD.Text = "Find";
			this.btnFindTFTD.Click += new System.EventHandler(this.btnFindTFTD_Click);
			// 
			// okButton
			// 
			this.okButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.okButton.Location = new System.Drawing.Point(191, 56);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(120, 23);
			this.okButton.TabIndex = 6;
			this.okButton.Text = "The paths are correct";
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.txtUFO);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.btnFindUFO);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(452, 25);
			this.panel1.TabIndex = 7;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.txtTFTD);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Controls.Add(this.btnFindTFTD);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 25);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(452, 25);
			this.panel2.TabIndex = 8;
			// 
			// InstallWindow
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(452, 84);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.okButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "InstallWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Set up";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnFindUFO;
		private System.Windows.Forms.Button btnFindTFTD;
		private System.Windows.Forms.TextBox txtUFO;
		private System.Windows.Forms.TextBox txtTFTD;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.FolderBrowserDialog folderSelector;
	}
}
