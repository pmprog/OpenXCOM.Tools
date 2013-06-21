/*using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace MapView
{
	public class CopyForm : System.Windows.Forms.Form
	{
		private CopyGroup copyGroup;
		private Settings settings;
	
		public CopyForm()
		{
			InitializeComponent();
			loadDefaults();
		}

		public CopyGroup CopyGroup
		{
			get{return copyGroup;}
		}

		private void loadDefaults()
		{
			settings = new Settings();
		}

		public Settings Settings
		{
			get{return settings;}
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.copyGroup = new MapView.CopyGroup();
			this.SuspendLayout();
			// 
			// copyGroup
			// 
			this.copyGroup.Dock = System.Windows.Forms.DockStyle.Fill;
			this.copyGroup.Name = "copyGroup";
			this.copyGroup.Size = new System.Drawing.Size(232, 237);
			this.copyGroup.TabIndex = 0;
			this.copyGroup.TabStop = false;
			this.copyGroup.Text = "Copy Options";
			// 
			// CopyForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(232, 237);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.copyGroup});
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CopyForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "CopyForm";
			this.ResumeLayout(false);

		}
		#endregion
	}
}
*/