using System;
using System.Collections.Generic;
using System.Text;
using XCom.Interfaces;
using DSShared;
using System.IO;
using XCom;
using System.Windows.Forms;

namespace PckView
{
	public class ImgProfile
	{
		public ImgProfile() { }

		private int imgWid = 0, imgHei = 0;
		private IXCImageFile imgType = null;		
		private string desc = "";
		private string defPal = "";
		private string single = "";
		private string ext = "";

		public static List<xcProfile> LoadFile(string inFile)
		{
			StreamReader sr = new StreamReader(inFile);
			VarCollection_Structure vs = new VarCollection_Structure(sr);
			sr.Close();

			List<xcProfile> profileList = new List<xcProfile>();

			foreach (string s in vs.KeyValList.Keys)
			{
				ImgProfile profile = new ImgProfile();
				Dictionary<string, DSShared.KeyVal> info = vs.KeyValList[s].SubHash;
				profile.ext = info["open"].Rest;
				profile.imgHei = int.Parse(info["height"].Rest);
				profile.imgWid = int.Parse(info["width"].Rest);
				profile.desc = s;
				profile.defPal = info["palette"].Rest;
				profile.ext = info["open"].Rest;
				
				if(info.ContainsKey("openSingle") && info["openSingle"]!=null)
					profile.single = info["openSingle"].Rest+info["open"].Rest;

				foreach (IXCImageFile ixc in SharedSpace.Instance.GetImageModList())
					if (ixc.ExplorerDescription == info["codec"].Rest)
					{
						profile.imgType = ixc;
						break;
					}

				profileList.Add(new xcProfile(profile));
			}

			return profileList;
		}

		public string OpenSingle
		{
			get { return single; }
			set { single = value; }
		}

		public string Palette
		{
			get { return defPal; }
			set { defPal = value; }
		}

		public string Description
		{
			get { return desc; }
			set { desc = value; }
		}

		public int ImgWid
		{
			get { return imgWid; }
			set { imgWid = value; }
		}

		public int ImgHei
		{
			get { return imgHei; }
			set { imgHei = value; }
		}

		public IXCImageFile ImgType
		{
			get { return imgType; }
			set { imgType = value; }
		}

		public string Extension
		{
			get { return ext; }
		}

		public string FileString
		{
			get { return ext; }
			set { ext = value; }
		}

		public void SaveProfile(string outFile)
		{
			bool append = false;
			if (File.Exists(outFile))
				append = MessageBox.Show("File exists, append new profile? Clicking 'No' will overwrite the file", "File exists", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
				
			StreamWriter sw = new StreamWriter(outFile, append);
			sw.WriteLine(Description);
			sw.WriteLine("{");

			sw.WriteLine("\tcodec:" + ImgType.ExplorerDescription);
			sw.WriteLine("\topen:." + ext.Substring(ext.LastIndexOf(".") + 1));
			sw.WriteLine("\twidth:" + ImgWid);
			sw.WriteLine("\theight:" + ImgHei);
			sw.WriteLine("\tpalette:" + Palette);
			if (single != "")
				sw.WriteLine("\topenSingle:" + single);

			//here would be the place to put decoder-specific settings

			sw.WriteLine("}\n");
			sw.Close();
		}
	}
}
