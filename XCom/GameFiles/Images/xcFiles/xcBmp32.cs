using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using XCom.Interfaces;
using System.IO;

namespace XCom.GameFiles.Images.xcFiles
{
	public class xcDotNet:IXCImageFile
	{
		public xcDotNet() : this(32, 40) { }

		public xcDotNet(int wid, int hei)
			: base(wid, hei)
		{
			ext = ".*";
			author = "Ben Ratzlaff";
			desc = "Interface for any file type the .net framework can load";

			expDesc = ".net image loader";

			fileOptions.Init(false, true, false, true);
			fileOptions.BitDepth = 32;
		}

		public override bool RegisterFile()
		{
			return false;
		}

		protected override XCImageCollection LoadFileOverride(string directory, string file, int imgWid, int imgHei, Palette pal)
		{
			System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(directory + "\\" + file);
			BmpForm bmf = new BmpForm();
			bmf.Bitmap = bmp;

			if (bmf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				imageSize = bmf.SelectedSize;

				return new DotNetCollection(directory + file, imgWid, imgHei, bmf.SelectedSpace);
			}

			return null;			
		}

		public override void SaveCollection(string directory, string file, XCImageCollection images)
		{
			DotNetCollection.Save(directory + "\\" + file, images);
		}
	}
}
