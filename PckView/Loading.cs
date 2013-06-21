/*using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace PckView
{
	/// <summary>
	/// Summary description for Loading.
	/// </summary>
	public class Loading : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ProgressBar progress;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Loading()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}


		public void update(int curr, int total)
		{
			progress.Maximum=total;
			progress.Value=curr;
		}
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

		#region Windows Form Designer generated code
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
			// Loading
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 29);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.progress});
			this.Name = "Loading";
			this.Text = "Loading BMP file";
			this.ResumeLayout(false);

		}
		#endregion
	}
}
*/