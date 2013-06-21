using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace MapView
{
	public class TilesetForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtTileset;
		private System.Windows.Forms.Button btnOk;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;

		private string txt,mapPath,rmpPath,blanksPath;
		private System.Windows.Forms.TextBox txtRmp;
		private System.Windows.Forms.TextBox txtMap;
		private System.Windows.Forms.Button btnFindMap;
		private System.Windows.Forms.Button btnFindBlank;
		private System.Windows.Forms.TextBox txtBlank;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnFindRmp;

		public TilesetForm()
		{
			InitializeComponent();
			txt=mapPath=rmpPath=null;
		}

		public string TilesetText
		{
			get{return txt;}
		}

		public string MapPath
		{
			get{return mapPath;}
		}

		public string RmpPath
		{
			get{return rmpPath;}
		}

		public string BlankPath
		{
			get{return blanksPath;}
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
			this.label1 = new System.Windows.Forms.Label();
			this.txtTileset = new System.Windows.Forms.TextBox();
			this.btnOk = new System.Windows.Forms.Button();
			this.txtRmp = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtMap = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.btnFindMap = new System.Windows.Forms.Button();
			this.btnFindRmp = new System.Windows.Forms.Button();
			this.btnFindBlank = new System.Windows.Forms.Button();
			this.txtBlank = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Tileset name";
			// 
			// txtTileset
			// 
			this.txtTileset.Location = new System.Drawing.Point(0, 16);
			this.txtTileset.Name = "txtTileset";
			this.txtTileset.Size = new System.Drawing.Size(160, 20);
			this.txtTileset.TabIndex = 1;
			this.txtTileset.Text = "";
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(112, 160);
			this.btnOk.Name = "btnOk";
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "Ok";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// txtRmp
			// 
			this.txtRmp.Location = new System.Drawing.Point(0, 96);
			this.txtRmp.Name = "txtRmp";
			this.txtRmp.Size = new System.Drawing.Size(272, 20);
			this.txtRmp.TabIndex = 4;
			this.txtRmp.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(0, 80);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "RMP directory";
			// 
			// txtMap
			// 
			this.txtMap.Location = new System.Drawing.Point(0, 56);
			this.txtMap.Name = "txtMap";
			this.txtMap.Size = new System.Drawing.Size(272, 20);
			this.txtMap.TabIndex = 6;
			this.txtMap.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(0, 40);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 16);
			this.label3.TabIndex = 5;
			this.label3.Text = "MAP directory";
			// 
			// btnFindMap
			// 
			this.btnFindMap.Location = new System.Drawing.Point(272, 56);
			this.btnFindMap.Name = "btnFindMap";
			this.btnFindMap.Size = new System.Drawing.Size(32, 23);
			this.btnFindMap.TabIndex = 7;
			this.btnFindMap.Text = "find";
			this.btnFindMap.Click += new System.EventHandler(this.btnFindMap_Click);
			// 
			// btnFindRmp
			// 
			this.btnFindRmp.Location = new System.Drawing.Point(272, 96);
			this.btnFindRmp.Name = "btnFindRmp";
			this.btnFindRmp.Size = new System.Drawing.Size(32, 23);
			this.btnFindRmp.TabIndex = 8;
			this.btnFindRmp.Text = "find";
			this.btnFindRmp.Click += new System.EventHandler(this.btnFindRmp_Click);
			// 
			// btnFindBlank
			// 
			this.btnFindBlank.Location = new System.Drawing.Point(272, 136);
			this.btnFindBlank.Name = "btnFindBlank";
			this.btnFindBlank.Size = new System.Drawing.Size(32, 23);
			this.btnFindBlank.TabIndex = 11;
			this.btnFindBlank.Text = "find";
			this.btnFindBlank.Click += new System.EventHandler(this.btnFindBlank_Click);
			// 
			// txtBlank
			// 
			this.txtBlank.Location = new System.Drawing.Point(0, 136);
			this.txtBlank.Name = "txtBlank";
			this.txtBlank.Size = new System.Drawing.Size(272, 20);
			this.txtBlank.TabIndex = 10;
			this.txtBlank.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(0, 120);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100, 16);
			this.label4.TabIndex = 9;
			this.label4.Text = "Blanks directory";
			// 
			// TilesetForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(306, 191);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.btnFindBlank,
																		  this.txtBlank,
																		  this.label4,
																		  this.btnFindRmp,
																		  this.btnFindMap,
																		  this.txtMap,
																		  this.label3,
																		  this.txtRmp,
																		  this.label2,
																		  this.btnOk,
																		  this.txtTileset,
																		  this.label1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TilesetForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "TilesetForm";
			this.ResumeLayout(false);

		}
		#endregion

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			if(txtTileset.Text=="")
			{
//				Dialog.ShowDialog(this,"You must specify a map name");
				MessageBox.Show(this,"You must specify a map name","Err..",MessageBoxButtons.OK);
				return;
			}

			if(txtMap.Text=="")
			{
//				Dialog.ShowDialog(this,"You must have a map path");
				MessageBox.Show(this,"You must have a map path","Err..",MessageBoxButtons.OK);
				return;
			}

			if(txtRmp.Text=="")
			{
				MessageBox.Show(this,"You must have a rmp path","Err..",MessageBoxButtons.OK);
				return;
			}

			if(txtBlank.Text=="")
			{
				MessageBox.Show(this,"You must have a blanks path","Err..",MessageBoxButtons.OK);
				return;
			}

			if(!System.IO.Directory.Exists(txtMap.Text))
			{
				MessageBox.Show(this,"map directory "+txtMap.Text+" does not exist","Err..",MessageBoxButtons.OK);
				return;
			}

			if(!System.IO.Directory.Exists(txtRmp.Text))
			{
				MessageBox.Show(this,"rmp directory "+txtRmp.Text+" does not exist","Err..",MessageBoxButtons.OK);
				return;
			}

			if(!System.IO.Directory.Exists(txtBlank.Text))
			{
				MessageBox.Show(this,"blanks directory "+txtBlank.Text+" does not exist","Err..",MessageBoxButtons.OK);
				return;
			}

			txt = txtTileset.Text;
			mapPath = txtMap.Text;
			rmpPath = txtRmp.Text;
			blanksPath=txtBlank.Text;

			Close();
		}

		private void btnFindMap_Click(object sender, System.EventArgs e)
		{
			FolderBrowserDialog fs = new FolderBrowserDialog();
			fs.Description="Find map directory";
			if(fs.ShowDialog(this)==DialogResult.OK)
				txtMap.Text=fs.SelectedPath;
		}

		private void btnFindRmp_Click(object sender, System.EventArgs e)
		{
			FolderBrowserDialog fs = new FolderBrowserDialog();
			fs.Description = "Find rmp directory";
			if (fs.ShowDialog(this) == DialogResult.OK)
				txtRmp.Text=fs.SelectedPath;		
		}

		private void btnFindBlank_Click(object sender, System.EventArgs e)
		{
			FolderBrowserDialog fs = new FolderBrowserDialog();
			fs.Description = "Find blank directory";
			if (fs.ShowDialog(this) == DialogResult.OK)
				txtBlank.Text=fs.SelectedPath;	
		}
	}
}
