using System;
using System.Collections.Generic;
using System.Text;

namespace PckView
{
	partial class BmpForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

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
			this.components = new System.ComponentModel.Container();
			this.drawPanel = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.scrollSpace = new System.Windows.Forms.HScrollBar();
			this.txtSpace = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.panelHeight = new System.Windows.Forms.Panel();
			this.scrollHeight = new System.Windows.Forms.HScrollBar();
			this.label2 = new System.Windows.Forms.Label();
			this.txtHeight = new System.Windows.Forms.TextBox();
			this.panelWidth = new System.Windows.Forms.Panel();
			this.scrollWidth = new System.Windows.Forms.HScrollBar();
			this.label1 = new System.Windows.Forms.Label();
			this.txtWidth = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnOk = new System.Windows.Forms.Button();
			this.cbTypes = new System.Windows.Forms.ComboBox();
			this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.miClose = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.miLineColor = new System.Windows.Forms.MenuItem();
			this.colors = new System.Windows.Forms.ColorDialog();
			this.openFile = new System.Windows.Forms.OpenFileDialog();
			this.panel3.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panelHeight.SuspendLayout();
			this.panelWidth.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// drawPanel
			// 
			this.drawPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.drawPanel.Location = new System.Drawing.Point(0, 76);
			this.drawPanel.Name = "drawPanel";
			this.drawPanel.Size = new System.Drawing.Size(542, 337);
			this.drawPanel.TabIndex = 6;
			this.drawPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.drawPanel_Paint);
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.panel2);
			this.panel3.Controls.Add(this.panel1);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(542, 76);
			this.panel3.TabIndex = 0;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.panel4);
			this.panel2.Controls.Add(this.panelHeight);
			this.panel2.Controls.Add(this.panelWidth);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(152, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(390, 76);
			this.panel2.TabIndex = 6;
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.scrollSpace);
			this.panel4.Controls.Add(this.txtSpace);
			this.panel4.Controls.Add(this.label3);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel4.Location = new System.Drawing.Point(0, 48);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(390, 24);
			this.panel4.TabIndex = 4;
			// 
			// scrollSpace
			// 
			this.scrollSpace.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scrollSpace.LargeChange = 2;
			this.scrollSpace.Location = new System.Drawing.Point(48, 0);
			this.scrollSpace.Maximum = 20;
			this.scrollSpace.Minimum = 1;
			this.scrollSpace.Name = "scrollSpace";
			this.scrollSpace.Size = new System.Drawing.Size(294, 24);
			this.scrollSpace.TabIndex = 2;
			this.scrollSpace.Value = 1;
			this.scrollSpace.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollSpace_Scroll);
			// 
			// txtSpace
			// 
			this.txtSpace.Dock = System.Windows.Forms.DockStyle.Right;
			this.txtSpace.Location = new System.Drawing.Point(342, 0);
			this.txtSpace.Name = "txtSpace";
			this.txtSpace.Size = new System.Drawing.Size(48, 20);
			this.txtSpace.TabIndex = 1;
			this.txtSpace.TextChanged += new System.EventHandler(this.txtSpace_TextChanged);
			// 
			// label3
			// 
			this.label3.Dock = System.Windows.Forms.DockStyle.Left;
			this.label3.Location = new System.Drawing.Point(0, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 24);
			this.label3.TabIndex = 0;
			this.label3.Text = "Space";
			// 
			// panelHeight
			// 
			this.panelHeight.Controls.Add(this.scrollHeight);
			this.panelHeight.Controls.Add(this.label2);
			this.panelHeight.Controls.Add(this.txtHeight);
			this.panelHeight.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelHeight.Location = new System.Drawing.Point(0, 24);
			this.panelHeight.Name = "panelHeight";
			this.panelHeight.Size = new System.Drawing.Size(390, 24);
			this.panelHeight.TabIndex = 3;
			// 
			// scrollHeight
			// 
			this.scrollHeight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scrollHeight.Location = new System.Drawing.Point(48, 0);
			this.scrollHeight.Maximum = 500;
			this.scrollHeight.Minimum = 1;
			this.scrollHeight.Name = "scrollHeight";
			this.scrollHeight.Size = new System.Drawing.Size(294, 24);
			this.scrollHeight.SmallChange = 10;
			this.scrollHeight.TabIndex = 3;
			this.scrollHeight.Value = 1;
			this.scrollHeight.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollHeight_Scroll);
			// 
			// label2
			// 
			this.label2.Dock = System.Windows.Forms.DockStyle.Left;
			this.label2.Location = new System.Drawing.Point(0, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 24);
			this.label2.TabIndex = 1;
			this.label2.Text = "Height";
			// 
			// txtHeight
			// 
			this.txtHeight.Dock = System.Windows.Forms.DockStyle.Right;
			this.txtHeight.Location = new System.Drawing.Point(342, 0);
			this.txtHeight.Name = "txtHeight";
			this.txtHeight.Size = new System.Drawing.Size(48, 20);
			this.txtHeight.TabIndex = 4;
			this.txtHeight.TextChanged += new System.EventHandler(this.txtHeight_TextChanged);
			// 
			// panelWidth
			// 
			this.panelWidth.Controls.Add(this.scrollWidth);
			this.panelWidth.Controls.Add(this.label1);
			this.panelWidth.Controls.Add(this.txtWidth);
			this.panelWidth.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelWidth.Location = new System.Drawing.Point(0, 0);
			this.panelWidth.Name = "panelWidth";
			this.panelWidth.Size = new System.Drawing.Size(390, 24);
			this.panelWidth.TabIndex = 2;
			// 
			// scrollWidth
			// 
			this.scrollWidth.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scrollWidth.Location = new System.Drawing.Point(48, 0);
			this.scrollWidth.Maximum = 500;
			this.scrollWidth.Minimum = 1;
			this.scrollWidth.Name = "scrollWidth";
			this.scrollWidth.Size = new System.Drawing.Size(294, 24);
			this.scrollWidth.SmallChange = 10;
			this.scrollWidth.TabIndex = 2;
			this.scrollWidth.Value = 1;
			this.scrollWidth.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollWidth_Scroll);
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Left;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "Width";
			// 
			// txtWidth
			// 
			this.txtWidth.Dock = System.Windows.Forms.DockStyle.Right;
			this.txtWidth.Location = new System.Drawing.Point(342, 0);
			this.txtWidth.Name = "txtWidth";
			this.txtWidth.Size = new System.Drawing.Size(48, 20);
			this.txtWidth.TabIndex = 4;
			this.txtWidth.TextChanged += new System.EventHandler(this.txtWidth_TextChanged);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnOk);
			this.panel1.Controls.Add(this.cbTypes);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(152, 76);
			this.panel1.TabIndex = 7;
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(39, 24);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 5;
			this.btnOk.Text = "Ok";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// cbTypes
			// 
			this.cbTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbTypes.Location = new System.Drawing.Point(0, 0);
			this.cbTypes.Name = "cbTypes";
			this.cbTypes.Size = new System.Drawing.Size(144, 21);
			this.cbTypes.TabIndex = 4;
			this.cbTypes.SelectedIndexChanged += new System.EventHandler(this.cbTypes_SelectedIndexChanged);
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem2});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miClose});
			this.menuItem1.Text = "File";
			// 
			// miClose
			// 
			this.miClose.Index = 0;
			this.miClose.Text = "Close";
			this.miClose.Click += new System.EventHandler(this.miClose_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.miLineColor});
			this.menuItem2.Text = "Edit";
			// 
			// miLineColor
			// 
			this.miLineColor.Index = 0;
			this.miLineColor.Text = "Line Color";
			this.miLineColor.Click += new System.EventHandler(this.miLineColor_Click);
			// 
			// openFile
			// 
			this.openFile.Filter = "BMP Files|*.bmp";
			this.openFile.Multiselect = true;
			// 
			// BmpForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(542, 413);
			this.Controls.Add(this.drawPanel);
			this.Controls.Add(this.panel3);
			this.Menu = this.mainMenu1;
			this.Name = "BmpForm";
			this.Text = "BmpForm";
			this.panel3.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.panelHeight.ResumeLayout(false);
			this.panelHeight.PerformLayout();
			this.panelWidth.ResumeLayout(false);
			this.panelWidth.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private System.Windows.Forms.Panel drawPanel;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem miClose;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem miLineColor;
		private System.Windows.Forms.ColorDialog colors;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panelHeight;
		private System.Windows.Forms.HScrollBar scrollHeight;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel panelWidth;
		private System.Windows.Forms.HScrollBar scrollWidth;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ComboBox cbTypes;
		private System.Windows.Forms.TextBox txtWidth;
		private System.Windows.Forms.TextBox txtHeight;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.OpenFileDialog openFile;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.HScrollBar scrollSpace;
		private System.Windows.Forms.TextBox txtSpace;
	}
}
