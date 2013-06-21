using System;
using XCom;
using XCom.Interfaces;
using System.Collections.Generic;

namespace PckView
{
	public class xcProfile : IXCImageFile
	{
		private IXCImageFile codec;
		public static readonly string PROFILE_EXT=".pvp";

		public xcProfile():base(0,0)
		{
			fileOptions.Init(false, false, false, false);
			//fileOptions.BmpDialog = false;
			//fileOptions.OpenDialog = false;
			//fileOptions.SaveDialog = false;
			//fileOptions.CustomDialog = false;

			ext = PROFILE_EXT;
			author = "Ben Ratzlaff";
			desc = "Provides profile support";
		}

		public xcProfile(ImgProfile profile):base(0,0)
		{			
			imageSize = new System.Drawing.Size(profile.ImgWid, profile.ImgHei);
			codec = profile.ImgType;
			expDesc = profile.Description;
			ext = profile.Extension;
			author = "Profile";
			desc = profile.Description;

			if (profile.OpenSingle != "")
				singleFile = profile.OpenSingle;

			//fileOptions.BmpDialog = true;
			//fileOptions.OpenDialog = true;

			//since we are loading off of an already generic implementation
			//we should let that implementation determine how this format be saved
			//fileOptions.SaveDialog = false;
			//fileOptions.CustomDialog = false;

			fileOptions.Init(false, true, true, false);

			xConsole.AddLine("Profile created: " + desc);

			try
			{
				defPal = XCom.SharedSpace.Instance.GetPaletteTable()[profile.Palette];
			}
			catch
			{
				defPal = XCom.Palette.TFTDBattle;
			}
		}

		public IXCImageFile Codec { get { return codec; } set { codec = value; } }

		protected override XCom.XCImageCollection LoadFileOverride(string directory,string file,int imgWid,int imgHei,Palette pal)
		{
			return codec.LoadFile(directory,file,imgWid,imgHei,pal);
		}

		public override void SaveCollection(string directory, string file,XCom.XCImageCollection images)
		{
			codec.SaveCollection(directory,file,images);
		}
	}
}
