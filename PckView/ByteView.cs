using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace PckView
{
	/// <summary>
	/// Summary description for ByteView.
	/// </summary>
	public class ByteView : System.Windows.Forms.Form
	{
		private PckImage image;

		private System.Windows.Forms.RichTextBox output;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ByteView(PckImage img)
		{
			InitializeComponent();
			image = img;
			printData();
		}

		public PckImage Image
		{
			set{if(value!=image){image = value;printData();}}
		}
/*
		private void printData()
		{
			if(image != null)
			{
				output.Text="";
				foreach(int i in image)
				{
					if(i==254)
					{
						output.SelectionColor = Color.Gray;
						output.AppendText("254\n");
						output.SelectionColor = Color.Black;
					}
					else if(i==255)
					{
						output.SelectionColor = Color.Red;
						output.AppendText("\n255\n");
						output.SelectionColor = Color.Black;
					}
					else
						output.AppendText(i+" ");
				}
			}
		}*/

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.output = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// output
			// 
			this.output.Dock = System.Windows.Forms.DockStyle.Fill;
			this.output.Name = "output";
			this.output.ReadOnly = true;
			this.output.Size = new System.Drawing.Size(292, 273);
			this.output.TabIndex = 0;
			this.output.Text = "";
			// 
			// ByteView
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.output});
			this.Name = "ByteView";
			this.Text = "ByteView";
			this.ResumeLayout(false);

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
		#endregion
	}
}
