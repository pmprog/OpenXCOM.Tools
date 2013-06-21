using System;
using System.Collections.Generic;
using System.Text;

namespace PckView
{
	partial class OpenCustomForm
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
			this.cbTypes = new System.Windows.Forms.ComboBox();
			this.scrollWid = new System.Windows.Forms.HScrollBar();
			this.scrollHei = new System.Windows.Forms.HScrollBar();
			this.btnTry = new System.Windows.Forms.Button();
			this.btnProfile = new System.Windows.Forms.Button();
			this.gbErrors = new System.Windows.Forms.GroupBox();
			this.txtErr = new System.Windows.Forms.RichTextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.txtWid = new System.Windows.Forms.TextBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.txtHei = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.gbErrors.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// cbTypes
			// 
			this.cbTypes.Dock = System.Windows.Forms.DockStyle.Left;
			this.cbTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbTypes.Location = new System.Drawing.Point(3, 16);
			this.cbTypes.Name = "cbTypes";
			this.cbTypes.Size = new System.Drawing.Size(179, 21);
			this.cbTypes.TabIndex = 0;
			// 
			// scrollWid
			// 
			this.scrollWid.Dock = System.Windows.Forms.DockStyle.Top;
			this.scrollWid.LargeChange = 1;
			this.scrollWid.Location = new System.Drawing.Point(50, 16);
			this.scrollWid.Maximum = 1000;
			this.scrollWid.Minimum = 1;
			this.scrollWid.Name = "scrollWid";
			this.scrollWid.Size = new System.Drawing.Size(256, 16);
			this.scrollWid.TabIndex = 3;
			this.scrollWid.Value = 1;
			this.scrollWid.Scroll += new System.Windows.Forms.ScrollEventHandler(this.wid_Scroll);
			// 
			// scrollHei
			// 
			this.scrollHei.Dock = System.Windows.Forms.DockStyle.Top;
			this.scrollHei.LargeChange = 1;
			this.scrollHei.Location = new System.Drawing.Point(50, 16);
			this.scrollHei.Maximum = 1000;
			this.scrollHei.Minimum = 1;
			this.scrollHei.Name = "scrollHei";
			this.scrollHei.Size = new System.Drawing.Size(256, 16);
			this.scrollHei.TabIndex = 5;
			this.scrollHei.Value = 1;
			this.scrollHei.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hei_Scroll);
			// 
			// btnTry
			// 
			this.btnTry.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnTry.Location = new System.Drawing.Point(78, 3);
			this.btnTry.Name = "btnTry";
			this.btnTry.Size = new System.Drawing.Size(75, 23);
			this.btnTry.TabIndex = 9;
			this.btnTry.Text = "Try";
			this.btnTry.Click += new System.EventHandler(this.btnTry_Click);
			// 
			// btnProfile
			// 
			this.btnProfile.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnProfile.Location = new System.Drawing.Point(155, 3);
			this.btnProfile.Name = "btnProfile";
			this.btnProfile.Size = new System.Drawing.Size(75, 23);
			this.btnProfile.TabIndex = 10;
			this.btnProfile.Text = "Save Profile";
			this.btnProfile.Click += new System.EventHandler(this.btnProfile_Click);
			// 
			// gbErrors
			// 
			this.gbErrors.Controls.Add(this.txtErr);
			this.gbErrors.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gbErrors.Location = new System.Drawing.Point(0, 0);
			this.gbErrors.Name = "gbErrors";
			this.gbErrors.Size = new System.Drawing.Size(309, 0);
			this.gbErrors.TabIndex = 11;
			this.gbErrors.TabStop = false;
			this.gbErrors.Text = "Error message";
			// 
			// txtErr
			// 
			this.txtErr.BackColor = System.Drawing.SystemColors.Control;
			this.txtErr.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtErr.Location = new System.Drawing.Point(3, 16);
			this.txtErr.Name = "txtErr";
			this.txtErr.Size = new System.Drawing.Size(303, 0);
			this.txtErr.TabIndex = 0;
			this.txtErr.Text = "";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.cbTypes);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(309, 43);
			this.groupBox1.TabIndex = 12;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "File Type";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.scrollWid);
			this.groupBox2.Controls.Add(this.txtWid);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox2.Location = new System.Drawing.Point(0, 43);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(309, 43);
			this.groupBox2.TabIndex = 13;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Width";
			// 
			// txtWid
			// 
			this.txtWid.Dock = System.Windows.Forms.DockStyle.Left;
			this.txtWid.Location = new System.Drawing.Point(3, 16);
			this.txtWid.Name = "txtWid";
			this.txtWid.Size = new System.Drawing.Size(47, 20);
			this.txtWid.TabIndex = 0;
			this.txtWid.TextChanged += new System.EventHandler(this.txtWid_TextChanged);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.scrollHei);
			this.groupBox3.Controls.Add(this.txtHei);
			this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox3.Location = new System.Drawing.Point(0, 86);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(309, 43);
			this.groupBox3.TabIndex = 14;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Height";
			// 
			// txtHei
			// 
			this.txtHei.Dock = System.Windows.Forms.DockStyle.Left;
			this.txtHei.Location = new System.Drawing.Point(3, 16);
			this.txtHei.Name = "txtHei";
			this.txtHei.Size = new System.Drawing.Size(47, 20);
			this.txtHei.TabIndex = 0;
			this.txtHei.TextChanged += new System.EventHandler(this.txtHei_TextChanged);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnProfile);
			this.panel1.Controls.Add(this.btnTry);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 129);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(309, 30);
			this.panel1.TabIndex = 15;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.gbErrors);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 159);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(309, 0);
			this.panel2.TabIndex = 16;
			// 
			// OpenCustomForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(309, 158);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "OpenCustomForm";
			this.Text = "Open Unknown File";
			this.gbErrors.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private System.Windows.Forms.ComboBox cbTypes;
		private System.Windows.Forms.Button btnTry;
		private System.Windows.Forms.HScrollBar scrollWid;
		private System.Windows.Forms.HScrollBar scrollHei;
		private System.Windows.Forms.GroupBox gbErrors;
		private System.Windows.Forms.Button btnProfile;
		private System.Windows.Forms.RichTextBox txtErr;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox txtWid;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.TextBox txtHei;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
	}
}
