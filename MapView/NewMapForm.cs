using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace MapView
{
	public class NewMapForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox txtRows;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtCols;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtHeight;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtMapName;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnOk;
		private System.ComponentModel.Container components = null;

		private string name;
		private byte r,c,h;
		private System.Windows.Forms.Label label5;

		public NewMapForm()
		{
			InitializeComponent();
			name=null;
		}

		public string MapName
		{
			get{return name;}
		}

		public byte MapRows
		{
			get{return r;}
		}

		public byte MapCols
		{
			get{return c;}
		}

		public byte MapHeight
		{
			get{return h;}
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
			this.txtRows = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtCols = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtHeight = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtMapName = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnOk = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtRows
			// 
			this.txtRows.Location = new System.Drawing.Point(8, 32);
			this.txtRows.Name = "txtRows";
			this.txtRows.TabIndex = 0;
			this.txtRows.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Rows";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "Cols";
			// 
			// txtCols
			// 
			this.txtCols.Location = new System.Drawing.Point(8, 80);
			this.txtCols.Name = "txtCols";
			this.txtCols.TabIndex = 2;
			this.txtCols.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(112, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(72, 16);
			this.label3.TabIndex = 5;
			this.label3.Text = "Height";
			// 
			// txtHeight
			// 
			this.txtHeight.Location = new System.Drawing.Point(112, 32);
			this.txtHeight.Name = "txtHeight";
			this.txtHeight.TabIndex = 4;
			this.txtHeight.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(0, 64);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(72, 16);
			this.label4.TabIndex = 7;
			this.label4.Text = "Name";
			// 
			// txtMapName
			// 
			this.txtMapName.Location = new System.Drawing.Point(0, 80);
			this.txtMapName.Name = "txtMapName";
			this.txtMapName.Size = new System.Drawing.Size(216, 20);
			this.txtMapName.TabIndex = 6;
			this.txtMapName.Text = "";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.txtRows,
																					this.label1,
																					this.txtCols,
																					this.label2,
																					this.label3,
																					this.txtHeight});
			this.groupBox1.Location = new System.Drawing.Point(0, 104);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(224, 112);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Dimensions";
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(72, 224);
			this.btnOk.Name = "btnOk";
			this.btnOk.TabIndex = 9;
			this.btnOk.Text = "Ok";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// label5
			// 
			this.label5.ForeColor = System.Drawing.Color.Red;
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(224, 56);
			this.label5.TabIndex = 10;
			this.label5.Text = "For best results, make maps in 10x10x4 or 20x20x4 pieces. Maps with other dimensi" +
				"ons may or may not be accepted by the game engine";
			// 
			// NewMapForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(224, 247);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.label5,
																		  this.btnOk,
																		  this.groupBox1,
																		  this.label4,
																		  this.txtMapName});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NewMapForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "New Map";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			try
			{
				r = byte.Parse(txtRows.Text);
				c = byte.Parse(txtCols.Text);
				h = byte.Parse(txtHeight.Text);
				name = txtMapName.Text;
				Close();
			}
			catch{}
		}
	}
}
