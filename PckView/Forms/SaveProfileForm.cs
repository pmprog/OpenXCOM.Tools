using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using XCom.Interfaces;
using XCom;
using System.IO;

namespace PckView
{
	public partial class SaveProfileForm : System.Windows.Forms.Form
	{
		private ImgProfile profileInfo;
		public SaveProfileForm()
		{
			InitializeComponent();
			profileInfo = new ImgProfile();
			DialogResult = DialogResult.Cancel;
			DSShared.Windows.RegistryInfo ri = new DSShared.Windows.RegistryInfo(this);

			if (!Directory.Exists(XCom.SharedSpace.Instance["CustomDir"].ToString()))
				Directory.CreateDirectory(XCom.SharedSpace.Instance["CustomDir"].ToString());

			saveFile.InitialDirectory = XCom.SharedSpace.Instance["CustomDir"].ToString();
			saveFile.FileName = "profile.pvp";

			txtOutDir.Text = saveFile.InitialDirectory + "\\" + saveFile.FileName;

			saveFile.Filter = "Image Profiles|*" + xcProfile.PROFILE_EXT;			

			foreach (string key in ((Dictionary<string, Palette>)SharedSpace.Instance["Palettes"]).Keys)
				cbPalette.Items.Add(key);

			if (cbPalette.Items.Count > 0)
				cbPalette.SelectedIndex = 0;

			restring();

			DialogResult = DialogResult.Cancel;
		}

		private void restring()
		{
			txtInfo.Text = "";
			
			if(ImgType!= null)
				txtInfo.Text+="Type: " + ImgType.ExplorerDescription + "\n";
			
			txtInfo.Text+="Width: " + ImgWid + "\nHeight: " + ImgHei;
		}

		public int ImgWid
		{
			get { return profileInfo.ImgWid; }
			set { profileInfo.ImgWid = value; restring(); }
		}

		public int ImgHei
		{
			get { return profileInfo.ImgHei; }
			set { profileInfo.ImgHei = value; restring(); }
		}

		public IXCImageFile ImgType
		{
			get { return profileInfo.ImgType; }
			set { profileInfo.ImgType = value; restring(); }
		}

		public string FileString
		{
			get { return profileInfo.FileString; }
			set 
			{
				profileInfo.FileString = value;

				saveFile.FileName = value.Substring(0,value.LastIndexOf(".")) + xcProfile.PROFILE_EXT;
				txtOutDir.Text = saveFile.InitialDirectory + "\\" + saveFile.FileName;
			}
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{			
			Close();
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.OK;

			profileInfo.Description = txtDesc.Text;
			if (radioSingle.Checked)
			{
				string file = txtOutDir.Text.Substring(txtOutDir.Text.LastIndexOf("\\")+1);
				file = file.Substring(0,file.LastIndexOf("."));
				profileInfo.OpenSingle = file;
			}
			profileInfo.Palette = cbPalette.SelectedItem.ToString();
			profileInfo.SaveProfile(txtOutDir.Text);

			((PckViewForm)SharedSpace.Instance["PckView"]).LoadProfile(txtOutDir.Text);

			DialogResult = DialogResult.OK;

			Close();
		}

		private void btnFindDir_Click(object sender, EventArgs e)
		{
			if (saveFile.ShowDialog() == DialogResult.OK)
				txtOutDir.Text = saveFile.FileName;
		}

		private void cbPalette_SelectedIndexChanged(object sender, EventArgs e)
		{
			profileInfo.Palette = cbPalette.SelectedText;
		}
	}
}
