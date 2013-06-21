using System;
using System.Collections.Generic;
using System.Text;

namespace PckView
{
	partial class HelpForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(336, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "Right click an image to save/replace/delete individual images";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(0, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(336, 64);
			this.label2.TabIndex = 1;
			this.label2.Text = "When editing BMP files, DO NOT add any colors that are not part of the palette. T" +
				"he game works on palettes, which means for any given image, only 256 colors can " +
				"be used. The colors have been encoded into each BMP that gets saved, use only th" +
				"ose colors";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(0, 112);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(336, 48);
			this.label3.TabIndex = 2;
			this.label3.Text = "If you find a bug in the program, and can reproduce it reliably, you may email me" +
				" about it. Any other questions (use of program, etc) will be answered only on th" +
				"e XTC messageboards";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(0, 24);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(336, 24);
			this.label4.TabIndex = 3;
			this.label4.Text = "To add new images, add them to the blank space at the bottom";
			// 
			// HelpForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(346, 159);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.label4,
																		  this.label3,
																		  this.label2,
																		  this.label1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "HelpForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Basic help";
			this.ResumeLayout(false);

		}
		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
	}
}
