/*using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using XCom.Interfaces;
using XCom;

namespace PckView
{
	public class WidthForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnSet;
		private System.Windows.Forms.TextBox txtWidth;
		private System.Windows.Forms.TrackBar trackBar1;
		private System.Windows.Forms.TrackBar trackOff;
		private System.Windows.Forms.Button btnOff;
		private System.Windows.Forms.TextBox txtOff;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private ViewPck view;
	
		public WidthForm(int maxWidth)
		{
			InitializeComponent();
			trackBar1.Maximum=maxWidth;
			trackBar1.Value=maxWidth;
		}

		public ViewPck Images
		{
			get{return view;}
			set{view=value;txtWidth.Text=view.Collection.IXCFile.ImageSize.Width.ToString();}
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.txtWidth = new System.Windows.Forms.TextBox();
			this.btnSet = new System.Windows.Forms.Button();
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this.trackOff = new System.Windows.Forms.TrackBar();
			this.btnOff = new System.Windows.Forms.Button();
			this.txtOff = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackOff)).BeginInit();
			this.SuspendLayout();
			// 
			// txtWidth
			// 
			this.txtWidth.Location = new System.Drawing.Point(107, 0);
			this.txtWidth.Name = "txtWidth";
			this.txtWidth.Size = new System.Drawing.Size(56, 20);
			this.txtWidth.TabIndex = 0;
			this.txtWidth.Text = "";
			// 
			// btnSet
			// 
			this.btnSet.Location = new System.Drawing.Point(171, 0);
			this.btnSet.Name = "btnSet";
			this.btnSet.TabIndex = 1;
			this.btnSet.Text = "Set";
			this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
			// 
			// trackBar1
			// 
			this.trackBar1.Location = new System.Drawing.Point(0, 24);
			this.trackBar1.Maximum = 100;
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Size = new System.Drawing.Size(344, 42);
			this.trackBar1.TabIndex = 2;
			this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
			// 
			// trackOff
			// 
			this.trackOff.Location = new System.Drawing.Point(0, 88);
			this.trackOff.Maximum = 100;
			this.trackOff.Name = "trackOff";
			this.trackOff.Size = new System.Drawing.Size(344, 42);
			this.trackOff.TabIndex = 5;
			this.trackOff.Scroll += new System.EventHandler(this.trackOff_Scroll);
			// 
			// btnOff
			// 
			this.btnOff.Location = new System.Drawing.Point(176, 64);
			this.btnOff.Name = "btnOff";
			this.btnOff.TabIndex = 4;
			this.btnOff.Text = "Set";
			this.btnOff.Click += new System.EventHandler(this.btnOff_Click);
			// 
			// txtOff
			// 
			this.txtOff.Location = new System.Drawing.Point(112, 64);
			this.txtOff.Name = "txtOff";
			this.txtOff.Size = new System.Drawing.Size(56, 20);
			this.txtOff.TabIndex = 3;
			this.txtOff.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 16);
			this.label1.TabIndex = 6;
			this.label1.Text = "Width";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 16);
			this.label2.TabIndex = 7;
			this.label2.Text = "Width Offset";
			// 
			// WidthForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(352, 133);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.label2,
																		  this.label1,
																		  this.trackOff,
																		  this.btnOff,
																		  this.txtOff,
																		  this.trackBar1,
																		  this.btnSet,
																		  this.txtWidth});
			this.Name = "WidthForm";
			this.Text = "WidthForm";
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackOff)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void btnSet_Click(object sender, System.EventArgs e)
		{
			try
			{
				int w = int.Parse(txtWidth.Text);
				//view.Collection.ImgWidth=w;

				foreach(PckImage pi in view.Collection)
					pi.ReImage();
				view.Refresh();
			}
			catch{}
		}

		private void trackBar1_Scroll(object sender, System.EventArgs e)
		{
			txtWidth.Text=trackBar1.Value.ToString();
			btnSet_Click(sender,e);
		}

		private void trackOff_Scroll(object sender, System.EventArgs e)
		{
			txtOff.Text=trackOff.Value.ToString();
			btnOff_Click(sender,e);
		}

		private void btnOff_Click(object sender, System.EventArgs e)
		{
			try
			{
				byte w = byte.Parse(txtOff.Text);
				foreach(PckImage pi in view.Collection)
					pi.MoveImage(w);
				view.Refresh();
			}
			catch(Exception ex){Console.WriteLine("Err: "+ex.Message);}
		}
	}
}
*/