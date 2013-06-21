/*using System;
using System.Drawing;
using System.Collections;
using XCom.Interfaces;

namespace XCom
{
	[xImage(4,4)]
	public class ScanEntry:ITile
	{
		public static int Width{get{return 4;}}
		public static int Height{get{return 4;}}

		public ScanEntry(byte[] entries,Palette pal,int idx):base(entries,pal,idx)
		{
			image = Bmp.MakeBitmap8(Width,Height,this.idx,palette.Colors);	
		}	

		public static Type GetCollectionType()
		{
			return typeof(ScanG);
		}

		public static void Save(System.IO.BinaryWriter output, ITile tile)
		{
			output.Write(tile.Bytes);
		}
	
		public ScanEntry(Bitmap b,int num,Palette pal,int startX,int startY)
		{
			ArrayList entries = new ArrayList(b.Palette.Entries);
			idx = new byte[Width*Height];
			fileNum=num;
			palette=pal;

			for(int r=startY,i=0;r<startY+Height;r++)
				for(int c=startX;c<startX+Width;c++,i++)
					idx[i] = (byte)entries.IndexOf(b.GetPixel(c,r));

			image = Bmp.MakeBitmap8(Width,Height,idx,palette.Colors);	
		}	

		public override ITile Clone(Palette p)
		{
			byte[] stuff = new byte[idx.Length];
			for(int i=0;i<stuff.Length;i++)
				stuff[i]=idx[i];

			return new ScanEntry(stuff,p,-1);
		}
	}
}
*/