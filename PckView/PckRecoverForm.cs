/*using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace PckView
{
	public class PckRecoverForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblFile;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.TextBox txtWidth;
		private System.Windows.Forms.TextBox txtHeight;
		private System.Windows.Forms.Button btnCancel;
	
		public PckRecoverForm(string filename)
		{
			InitializeComponent();

			lblFile.Text="The file "+filename+" could not be opened. If you think the file uses pck encoding, the problem is probably due to an image size that is not known. You can attempt to figure this out yourself if you wish. Enter a width and height value that is larger than the images' and the program wont crash. You can then use the Bytes->Width form to figure out the correct width in realtime.";
			DialogResult=DialogResult.Cancel;
		}

		public int EnteredWidth
		{
			get{return int.Parse(txtWidth.Text);}
		}

		public int EnteredHeight
		{
			get{return int.Parse(txtHeight.Text);}
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblFile = new System.Windows.Forms.Label();
			this.txtWidth = new System.Windows.Forms.TextBox();
			this.txtHeight = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblFile
			// 
			this.lblFile.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.lblFile.Name = "lblFile";
			this.lblFile.Size = new System.Drawing.Size(312, 152);
			this.lblFile.TabIndex = 0;
			// 
			// txtWidth
			// 
			this.txtWidth.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			this.txtWidth.Location = new System.Drawing.Point(48, 160);
			this.txtWidth.Name = "txtWidth";
			this.txtWidth.Size = new System.Drawing.Size(64, 20);
			this.txtWidth.TabIndex = 1;
			this.txtWidth.Text = "100";
			// 
			// txtHeight
			// 
			this.txtHeight.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			this.txtHeight.Location = new System.Drawing.Point(48, 184);
			this.txtHeight.Name = "txtHeight";
			this.txtHeight.Size = new System.Drawing.Size(64, 20);
			this.txtHeight.TabIndex = 2;
			this.txtHeight.Text = "200";
			// 
			// label1
			// 
			this.label1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			this.label1.Location = new System.Drawing.Point(0, 160);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 16);
			this.label1.TabIndex = 3;
			this.label1.Text = "Width";
			// 
			// label2
			// 
			this.label2.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			this.label2.Location = new System.Drawing.Point(0, 184);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 16);
			this.label2.TabIndex = 4;
			this.label2.Text = "Height";
			// 
			// btnOk
			// 
			this.btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnOk.Location = new System.Drawing.Point(79, 216);
			this.btnOk.Name = "btnOk";
			this.btnOk.TabIndex = 5;
			this.btnOk.Text = "Ok";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnCancel.Location = new System.Drawing.Point(159, 216);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// PckRecoverForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(312, 245);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.btnCancel,
																		  this.btnOk,
																		  this.label2,
																		  this.label1,
																		  this.txtHeight,
																		  this.txtWidth,
																		  this.lblFile});
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PckRecoverForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "PckRecoverForm";
			this.TopMost = true;
			this.ResumeLayout(false);

		}
		#endregion

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			DialogResult=DialogResult.OK;
			Close();
		}
	}
}
*/