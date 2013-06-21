using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using XCom;
using XCom.Interfaces;
using XCom.Interfaces.Base;

namespace MapView
{
	public class ChangeMapSizeForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtR;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtC;
		private System.Windows.Forms.TextBox txtH;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox oldC;
		private System.Windows.Forms.TextBox oldR;
		private System.Windows.Forms.TextBox oldH;

		private IMap_Base map;

		public ChangeMapSizeForm()
		{
			InitializeComponent();

			DialogResult=DialogResult.Cancel;
		}

		public IMap_Base Map
		{
			get{return map;}
			set { map = value; txtR.Text = oldR.Text = map.MapSize.Rows.ToString(); txtC.Text = oldC.Text = map.MapSize.Cols.ToString(); txtH.Text = oldH.Text = map.MapSize.Height.ToString(); }
		}

		public int NewRows
		{
			get{return int.Parse(txtR.Text);}
		}

		public int NewCols
		{
			get{return int.Parse(txtC.Text);}
		}

		public int NewHeight
		{
			get{return int.Parse(txtH.Text);}
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
		this.oldC = new System.Windows.Forms.TextBox();
		this.oldR = new System.Windows.Forms.TextBox();
		this.oldH = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.txtR = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.txtC = new System.Windows.Forms.TextBox();
		this.txtH = new System.Windows.Forms.TextBox();
		this.btnOk = new System.Windows.Forms.Button();
		this.btnCancel = new System.Windows.Forms.Button();
		this.SuspendLayout();
		// 
		// oldC
		// 
		this.oldC.Location = new System.Drawing.Point(48, 32);
		this.oldC.Name = "oldC";
		this.oldC.ReadOnly = true;
		this.oldC.Size = new System.Drawing.Size(40, 20);
		this.oldC.TabIndex = 7;
		this.oldC.Text = "";
		// 
		// oldR
		// 
		this.oldR.Location = new System.Drawing.Point(0, 32);
		this.oldR.Name = "oldR";
		this.oldR.ReadOnly = true;
		this.oldR.Size = new System.Drawing.Size(40, 20);
		this.oldR.TabIndex = 6;
		this.oldR.Text = "";
		// 
		// oldH
		// 
		this.oldH.Location = new System.Drawing.Point(96, 32);
		this.oldH.Name = "oldH";
		this.oldH.ReadOnly = true;
		this.oldH.Size = new System.Drawing.Size(40, 20);
		this.oldH.TabIndex = 8;
		this.oldH.Text = "";
		// 
		// label1
		// 
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(56, 16);
		this.label1.TabIndex = 0;
		this.label1.Text = "Old Size";
		// 
		// txtR
		// 
		this.txtR.Location = new System.Drawing.Point(0, 80);
		this.txtR.Name = "txtR";
		this.txtR.Size = new System.Drawing.Size(40, 20);
		this.txtR.TabIndex = 1;
		this.txtR.Text = "";
		// 
		// label2
		// 
		this.label2.Location = new System.Drawing.Point(0, 64);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(56, 16);
		this.label2.TabIndex = 5;
		this.label2.Text = "New Size";
		// 
		// label3
		// 
		this.label3.Location = new System.Drawing.Point(56, 16);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(24, 16);
		this.label3.TabIndex = 6;
		this.label3.Text = "c";
		this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		// 
		// label4
		// 
		this.label4.Location = new System.Drawing.Point(8, 16);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(24, 16);
		this.label4.TabIndex = 7;
		this.label4.Text = "r";
		this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		// 
		// label5
		// 
		this.label5.Location = new System.Drawing.Point(104, 16);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(24, 16);
		this.label5.TabIndex = 8;
		this.label5.Text = "h";
		this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		// 
		// txtC
		// 
		this.txtC.Location = new System.Drawing.Point(48, 80);
		this.txtC.Name = "txtC";
		this.txtC.Size = new System.Drawing.Size(40, 20);
		this.txtC.TabIndex = 2;
		this.txtC.Text = "";
		// 
		// txtH
		// 
		this.txtH.Location = new System.Drawing.Point(96, 80);
		this.txtH.Name = "txtH";
		this.txtH.Size = new System.Drawing.Size(40, 20);
		this.txtH.TabIndex = 3;
		this.txtH.Text = "";
		// 
		// btnOk
		// 
		this.btnOk.Location = new System.Drawing.Point(16, 112);
		this.btnOk.Name = "btnOk";
		this.btnOk.Size = new System.Drawing.Size(48, 23);
		this.btnOk.TabIndex = 4;
		this.btnOk.Text = "OK";
		this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
		// 
		// btnCancel
		// 
		this.btnCancel.Location = new System.Drawing.Point(64, 112);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(48, 23);
		this.btnCancel.TabIndex = 5;
		this.btnCancel.Text = "Cancel";
		this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
		// 
		// ChangeMapSizeForm
		// 
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
		this.ClientSize = new System.Drawing.Size(136, 141);
		this.ControlBox = false;
		this.Controls.AddRange(new System.Windows.Forms.Control[] {
																	  this.btnCancel,
																	  this.btnOk,
																	  this.txtH,
																	  this.txtC,
																	  this.label5,
																	  this.label4,
																	  this.label3,
																	  this.label2,
																	  this.txtR,
																	  this.oldC,
																	  this.oldH,
																	  this.oldR,
																	  this.label1});
		this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		this.Name = "ChangeMapSizeForm";
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Change map";
		this.ResumeLayout(false);

	}
		#endregion

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			try
			{
				int.Parse(txtR.Text);
				int.Parse(txtC.Text);
				int.Parse(txtH.Text);

				DialogResult=DialogResult.OK;
				Close();
			}
			catch{MessageBox.Show(this,"Input must be whole numbers","Error",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);}
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			Close();
		}
	}
}
