using System;
using System.Collections.Generic;
using System.Text;
using XCom.Interfaces;
using System.IO;

namespace XCom.GameFiles.Images.xcFiles
{
	public class xcSpk : IXCImageFile
	{
		public xcSpk() : this(320, 200) { }

		public xcSpk(int wid, int hei)
			: base(wid, hei)
		{
			ext = ".spk";
			author = "Ben Ratzlaff";
			desc = "Spk file codec";

			defPal = Palette.UFOResearch;

			expDesc = "SPK Image";

			fileOptions.Init(true, false, true, true);
		}

		protected override XCImageCollection LoadFileOverride(string directory, string file, int imgWid, int imgHei, Palette pal)
		{
			XCImageCollection collect = new XCImageCollection();
			XCImage img = new SPKImage(pal, File.OpenRead(directory + "\\" + file),imgWid,imgHei);
			collect.Add(img);

			return collect;
		}

		public override void SaveCollection(string directory, string file, XCImageCollection images)
		{
			if (images.Count == 1)
				SPKImage.Save(images[0].Bytes, File.OpenWrite(directory + "\\" + file + ext));
			else
				for (int i = 0; i < images.Count; i++)
					SPKImage.Save(images[i].Bytes, File.OpenWrite(directory + "\\" + file + i.ToString() + ext));
		}
	}
}
