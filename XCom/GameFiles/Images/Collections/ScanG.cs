/*using System;
using System.IO;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using XCom.Interfaces;

namespace XCom
{
	public class ScanG:IxCollection
	{
		public ScanG(Stream s):this(s,Palette.TFTDBattle){}

		public ScanG(Stream s, Palette p)
		{
			BinaryReader sr = new BinaryReader(s);
			images = new ArrayList();
			int i=0;
			while(sr.BaseStream.Position<sr.BaseStream.Length)
				images.Add(new ScanEntry(sr.ReadBytes(ScanEntry.Width*ScanEntry.Height),p,i++));
		}

		public void Save(string directory, string file, IxCollection images)
		{
			System.IO.BinaryWriter scanG = new System.IO.BinaryWriter(System.IO.File.Create(directory+"\\"+file+".dat"));
			foreach(ITile image in images)
				ScanEntry.Save(scanG,image);
			scanG.Close();
		}

		private ScanG(ArrayList images)
		{
			this.images = images;
			name="newTerrain";
			path="C:\\";
		}

		//		public override int ImgWidth{get{return ScanEntry.Width*scale;}}
		//		public override int ImgHeight{get{return ScanEntry.Height*scale;}}

		public static IxCollection FromBmp(Bitmap b)
		{
			//			ArrayList list = new ArrayList();
			//
			//			int cols = (b.Width+space)/(ScanEntry.Width+space);
			//			int rows = (b.Height+space)/(ScanEntry.Height+space);
			//
			//			int num=0;
			//
			//			for(int y=0;y<b.Height;y+=ScanEntry.Height+space)
			//				for(int x=0;x<b.Width;x+=ScanEntry.Width+space)
			//				{
			//					list.Add(new ScanEntry(b,num++,Palette.TFTDGeo,x,y));
			//					Bmp.FireLoadingEvent(num,rows*cols);
			//				}
			//
			//			return new ScanG(list);
			return null;
		}

		public static ITile FromBmpSingle(Bitmap src,int num,Palette pal)
		{
			return new ScanEntry(src,num,pal,0,0);
		}
	}
}
*/