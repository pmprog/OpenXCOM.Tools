using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace MapView
{
	public class Dialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label txt;
		private System.Windows.Forms.Button btnOk;
		private System.ComponentModel.Container components = null;

		public Dialog(string text)
		{
			InitializeComponent();
			txt.Text=text;
		}

		public static void ShowDialog(IWin32Window parent, string text)
		{
			Dialog d = new Dialog(text);
			d.ShowDialog(parent);
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
			this.txt = new System.Windows.Forms.Label();
			this.btnOk = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txt
			// 
			this.txt.Dock = System.Windows.Forms.DockStyle.Top;
			this.txt.Name = "txt";
			this.txt.Size = new System.Drawing.Size(292, 72);
			this.txt.TabIndex = 0;
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(104, 72);
			this.btnOk.Name = "btnOk";
			this.btnOk.TabIndex = 1;
			this.btnOk.Text = "Ok";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// Dialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 103);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.btnOk,
																		  this.txt});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "Dialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Dialog";
			this.ResumeLayout(false);

		}
		#endregion

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			Close();
		}
	}
}
