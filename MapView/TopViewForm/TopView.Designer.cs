namespace MapView.TopViewForm
{
	partial class TopView
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
			this.toolStripContainer2 = new System.Windows.Forms.ToolStripContainer();
			this.center = new System.Windows.Forms.Panel();
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.bottom = new MapView.TopViewForm.BottomPanel();
			this.toolStripContainer2.ContentPanel.SuspendLayout();
			this.toolStripContainer2.LeftToolStripPanel.SuspendLayout();
			this.toolStripContainer2.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripContainer2
			// 
			// 
			// toolStripContainer2.BottomToolStripPanel
			// 
			this.toolStripContainer2.BottomToolStripPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			// 
			// toolStripContainer2.ContentPanel
			// 
			this.toolStripContainer2.ContentPanel.Controls.Add(this.center);
			this.toolStripContainer2.ContentPanel.Size = new System.Drawing.Size(442, 201);
			this.toolStripContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			// 
			// toolStripContainer2.LeftToolStripPanel
			// 
			this.toolStripContainer2.LeftToolStripPanel.Controls.Add(this.toolStrip);
			this.toolStripContainer2.LeftToolStripPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStripContainer2.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer2.Name = "toolStripContainer2";
			// 
			// toolStripContainer2.RightToolStripPanel
			// 
			this.toolStripContainer2.RightToolStripPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStripContainer2.Size = new System.Drawing.Size(492, 251);
			this.toolStripContainer2.TabIndex = 4;
			this.toolStripContainer2.Text = "toolStripContainer2";
			// 
			// toolStripContainer2.TopToolStripPanel
			// 
			this.toolStripContainer2.TopToolStripPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			// 
			// center
			// 
			this.center.Dock = System.Windows.Forms.DockStyle.Fill;
			this.center.Location = new System.Drawing.Point(0, 0);
			this.center.Name = "center";
			this.center.Size = new System.Drawing.Size(442, 201);
			this.center.TabIndex = 2;
			// 
			// toolStrip
			// 
			this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip.Location = new System.Drawing.Point(0, 3);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Padding = new System.Windows.Forms.Padding(0);
			this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip.Size = new System.Drawing.Size(25, 109);
			this.toolStrip.TabIndex = 1;
			this.toolStrip.Text = "toolStrip1";
			// 
			// bottom
			// 
			this.bottom.Brushes = null;
			this.bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bottom.Location = new System.Drawing.Point(0, 251);
			this.bottom.Name = "bottom";
			this.bottom.Pens = null;
			this.bottom.Size = new System.Drawing.Size(492, 74);
			this.bottom.TabIndex = 0;
			this.bottom.Text = "bottom";
			this.bottom.Tile = null;
			// 
			// TopView
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(492, 325);
			this.Controls.Add(this.toolStripContainer2);
			this.Controls.Add(this.bottom);
			this.Name = "TopView";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "TopView";
			this.toolStripContainer2.ContentPanel.ResumeLayout(false);
			this.toolStripContainer2.LeftToolStripPanel.ResumeLayout(false);
			this.toolStripContainer2.LeftToolStripPanel.PerformLayout();
			this.toolStripContainer2.ResumeLayout(false);
			this.toolStripContainer2.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

		private BottomPanel bottom;
		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.Panel center;
		private System.Windows.Forms.ToolStripContainer toolStripContainer2;
	}
}