using System;
using System.Collections.Generic;
using System.Text;
using XCom.Interfaces;
using System.IO;

namespace XCom.GameFiles.Images.xcFiles
{
	public class xcBdy : IXCImageFile
	{
		public xcBdy() : this(320, 200) { }

		public xcBdy(int wid, int hei)
			: base(wid, hei)
		{
			ext = ".bdy";
			author = "Ben Ratzlaff";
			desc = "Bdy file codec";

			defPal = Palette.TFTDResearch;


			expDesc = "BDY Image";

			fileOptions.Init(true, false, true, true);
		}

		protected override XCImageCollection LoadFileOverride(string directory, string file, int imgWid, int imgHei, Palette pal)
		{
			XCImageCollection collect = new XCImageCollection();
			XCImage img = new BDYImage(pal, File.OpenRead(directory + "\\" + file),imgWid,imgHei);
			collect.Add(img);

			return collect;
		}

		public override void SaveCollection(string directory, string file, XCImageCollection images)
		{
			if (images.Count == 1)
				BDYImage.Save(images[0].Bytes, File.OpenWrite(directory + "\\" + file + ext));
			else
				for (int i = 0; i < images.Count; i++)
					BDYImage.Save(images[i].Bytes, File.OpenWrite(directory + "\\" + file + i.ToString() + ext));
		}
	}
}
