/*using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using XCom;
using XCom.Interfaces;

namespace MapView
{
	public class ExportForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.RadioButton radio4;
		private System.Windows.Forms.RadioButton radio2;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.CheckedListBox mapList;
		private System.Windows.Forms.CheckBox checkExportImages;
		private System.Windows.Forms.GroupBox groupImages;
		private System.Windows.Forms.CheckBox checkPalette;
		private System.Windows.Forms.CheckBox checkMinimal;
		private System.Windows.Forms.CheckBox checkMaxImages;
		private List<string> maps;
		private MpkFile mpkFile;
		private System.Windows.Forms.TextBox txtNumImages;
		private System.Windows.Forms.Button btnInfo;

		private Dictionary<CheckBox,MpkOption> mpkOptionsBool;

		public ExportForm()
		{
			InitializeComponent();
			mpkFile = new MpkFile();

			mpkOptionsBool = new Dictionary<CheckBox, MpkOption>();
			mpkOptionsBool[checkPalette]=MpkOption.SavePalette;
			mpkOptionsBool[checkMinimal]=MpkOption.SaveMinimal;
			mpkOptionsBool[checkExportImages]=MpkOption.SaveImages;
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
			this.components = new System.ComponentModel.Container();
			this.checkPalette = new System.Windows.Forms.CheckBox();
			this.checkMinimal = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.mapList = new System.Windows.Forms.CheckedListBox();
			this.groupImages = new System.Windows.Forms.GroupBox();
			this.checkMaxImages = new System.Windows.Forms.CheckBox();
			this.txtNumImages = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.radio2 = new System.Windows.Forms.RadioButton();
			this.radio4 = new System.Windows.Forms.RadioButton();
			this.checkExportImages = new System.Windows.Forms.CheckBox();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnInfo = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupImages.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// checkPalette
			// 
			this.checkPalette.Enabled = false;
			this.checkPalette.Location = new System.Drawing.Point(8, 16);
			this.checkPalette.Name = "checkPalette";
			this.checkPalette.Size = new System.Drawing.Size(88, 16);
			this.checkPalette.TabIndex = 0;
			this.checkPalette.Text = "Save Palette";
			this.toolTip.SetToolTip(this.checkPalette, "If using a nonstandard palette, this needs to be checked");
			this.checkPalette.CheckedChanged += new System.EventHandler(this.checkChanged);
			// 
			// checkMinimal
			// 
			this.checkMinimal.Checked = true;
			this.checkMinimal.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkMinimal.Location = new System.Drawing.Point(8, 32);
			this.checkMinimal.Name = "checkMinimal";
			this.checkMinimal.Size = new System.Drawing.Size(120, 16);
			this.checkMinimal.TabIndex = 1;
			this.checkMinimal.Text = "Save Minimal Info";
			this.toolTip.SetToolTip(this.checkMinimal, "If checked, any images/mcd entries that are not used will not be exported");
			this.checkMinimal.CheckedChanged += new System.EventHandler(this.checkChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.mapList});
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(112, 273);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Maps";
			this.toolTip.SetToolTip(this.groupBox1, "Only maps that are checked will be exported");
			// 
			// mapList
			// 
			this.mapList.CheckOnClick = true;
			this.mapList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mapList.Location = new System.Drawing.Point(3, 16);
			this.mapList.Name = "mapList";
			this.mapList.Size = new System.Drawing.Size(106, 244);
			this.mapList.TabIndex = 0;
			this.toolTip.SetToolTip(this.mapList, "Only maps that are checked will be exported");
			// 
			// groupImages
			// 
			this.groupImages.Controls.AddRange(new System.Windows.Forms.Control[] {
																					  this.checkMaxImages,
																					  this.txtNumImages,
																					  this.checkMinimal,
																					  this.checkPalette,
																					  this.groupBox2});
			this.groupImages.Location = new System.Drawing.Point(112, 0);
			this.groupImages.Name = "groupImages";
			this.groupImages.Size = new System.Drawing.Size(200, 112);
			this.groupImages.TabIndex = 4;
			this.groupImages.TabStop = false;
			// 
			// checkMaxImages
			// 
			this.checkMaxImages.Location = new System.Drawing.Point(8, 48);
			this.checkMaxImages.Name = "checkMaxImages";
			this.checkMaxImages.Size = new System.Drawing.Size(104, 16);
			this.checkMaxImages.TabIndex = 4;
			this.checkMaxImages.Text = "Images per file";
			this.toolTip.SetToolTip(this.checkMaxImages, "If checked, there will be a hard limit to the number of images in a PCK file");
			this.checkMaxImages.CheckedChanged += new System.EventHandler(this.checkMaxImages_CheckedChanged);
			// 
			// txtNumImages
			// 
			this.txtNumImages.Location = new System.Drawing.Point(112, 48);
			this.txtNumImages.Name = "txtNumImages";
			this.txtNumImages.Size = new System.Drawing.Size(56, 20);
			this.txtNumImages.TabIndex = 3;
			this.txtNumImages.Text = "100";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.radio2,
																					this.radio4});
			this.groupBox2.Location = new System.Drawing.Point(8, 64);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(80, 40);
			this.groupBox2.TabIndex = 8;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "TAB bpp";
			this.toolTip.SetToolTip(this.groupBox2, "TFTD and all unit files use a bpp of 4, UFO terrain uses a bpp of 2");
			// 
			// radio2
			// 
			this.radio2.Location = new System.Drawing.Point(8, 16);
			this.radio2.Name = "radio2";
			this.radio2.Size = new System.Drawing.Size(32, 16);
			this.radio2.TabIndex = 7;
			this.radio2.Text = "2";
			this.toolTip.SetToolTip(this.radio2, "TFTD and all unit files use a bpp of 4, UFO terrain uses a bpp of 2");
			this.radio2.CheckedChanged += new System.EventHandler(this.radio2_CheckedChanged);
			// 
			// radio4
			// 
			this.radio4.Checked = true;
			this.radio4.Location = new System.Drawing.Point(40, 16);
			this.radio4.Name = "radio4";
			this.radio4.Size = new System.Drawing.Size(32, 16);
			this.radio4.TabIndex = 6;
			this.radio4.TabStop = true;
			this.radio4.Text = "4";
			this.toolTip.SetToolTip(this.radio4, "TFTD and all unit files use a bpp of 4, UFO terrain uses a bpp of 2");
			this.radio4.CheckedChanged += new System.EventHandler(this.radio2_CheckedChanged);
			// 
			// checkExportImages
			// 
			this.checkExportImages.Location = new System.Drawing.Point(120, 0);
			this.checkExportImages.Name = "checkExportImages";
			this.checkExportImages.Size = new System.Drawing.Size(96, 16);
			this.checkExportImages.TabIndex = 5;
			this.checkExportImages.Text = "Export Images";
			this.toolTip.SetToolTip(this.checkExportImages, "If you dont export images, any destination computer needs to have EXACTLY the sam" +
				"e image files. ");
			this.checkExportImages.CheckedChanged += new System.EventHandler(this.checkChanged);
			// 
			// panel1
			// 
			this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.btnCancel,
																				 this.btnSave});
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(112, 249);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(200, 24);
			this.panel1.TabIndex = 5;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnCancel.Location = new System.Drawing.Point(103, 0);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnSave.Location = new System.Drawing.Point(23, 0);
			this.btnSave.Name = "btnSave";
			this.btnSave.TabIndex = 0;
			this.btnSave.Text = "Save";
			// 
			// btnInfo
			// 
			this.btnInfo.Location = new System.Drawing.Point(120, 120);
			this.btnInfo.Name = "btnInfo";
			this.btnInfo.TabIndex = 6;
			this.btnInfo.Text = "Map Info";
			this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
			// 
			// ExportForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(312, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.btnInfo,
																		  this.checkExportImages,
																		  this.panel1,
																		  this.groupImages,
																		  this.groupBox1});
			this.Name = "ExportForm";
			this.Text = "Export";
			this.groupBox1.ResumeLayout(false);
			this.groupImages.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void checkChanged(object sender, EventArgs e)
		{
			mpkFile.SetOption(mpkOptionsBool[(CheckBox)sender],((CheckBox)sender).Checked);
			if(sender==checkExportImages)
				groupImages.Enabled=checkExportImages.Checked;
		}

		private void checkMaxImages_CheckedChanged(object sender, System.EventArgs e)
		{
			int num=0;
			try
			{
				num=int.Parse(txtNumImages.Text);
			}
			catch
			{
				txtNumImages.Text="100";
				num=100;
			}
			if(checkMaxImages.Checked)
				mpkFile.SetOption(MpkOption.NumImages,num);
			else
				mpkFile.SetOption(MpkOption.NumImages,0);
		}

		private void radio2_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radio2.Checked)
				mpkFile.SetOption(MpkOption.Bpp,2);
			else
				mpkFile.SetOption(MpkOption.Bpp,4);
		}

		private void btnInfo_Click(object sender, System.EventArgs e)
		{
		
		}

		public List<string> Maps
		{
			set{maps=value;mapList.Items.Clear();foreach(string s in maps)mapList.Items.Add(s,true);}
		}
	}
}
*/