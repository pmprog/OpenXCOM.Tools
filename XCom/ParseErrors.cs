using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace XCom
{
	public class ParseErrors : System.Windows.Forms.Form
	{
		private bool error=false;
		private System.Windows.Forms.RichTextBox txt;

		private System.ComponentModel.Container components = null;

		public ParseErrors()
		{
			InitializeComponent();
		}

		public void AddError(string error)
		{
			txt.AppendText(error+"\n");
			this.error=true;
		}

		public bool Error
		{
			get{return error;}
			set{error=value;}
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
			this.txt = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// txt
			// 
			this.txt.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txt.Name = "txt";
			this.txt.Size = new System.Drawing.Size(504, 213);
			this.txt.TabIndex = 0;
			this.txt.Text = "";
			// 
			// ParseErrors
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(504, 213);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.txt});
			this.Name = "ParseErrors";
			this.Text = "There were errors";
			this.ResumeLayout(false);

		}
		#endregion
	}
}
