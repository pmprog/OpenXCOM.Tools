using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace MapView
{
	public class LoadingForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ProgressBar progress;
		private System.ComponentModel.Container components = null;

		public LoadingForm()
		{
			InitializeComponent();
		}

		public void Update(int curr, int total)
		{
			progress.Maximum=total;
			progress.Value=curr;
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.progress = new System.Windows.Forms.ProgressBar();
			this.SuspendLayout();
			// 
			// progress
			// 
			this.progress.Dock = System.Windows.Forms.DockStyle.Fill;
			this.progress.Name = "progress";
			this.progress.Size = new System.Drawing.Size(292, 29);
			this.progress.TabIndex = 0;
			// 
			// LoadingForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 29);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.progress});
			this.Name = "LoadingForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.ResumeLayout(false);

		}
		#endregion
	}
}
