/*using System;
using System.Drawing;
using System.Collections;
using XCom.Interfaces;

namespace XCom
{
	[xImage(32,32)]
	public class TerrainEntry:ITile
	{
		public static int Width{get{return 32;}}
		public static int Height{get{return 32;}}

		public TerrainEntry(byte[] entries,Palette pal,int idx):base(entries,pal,idx)
		{
			image = Bmp.MakeBitmap8(Width,Height,this.idx,palette.Colors);	
		}	

		public static Type GetCollectionType()
		{
			return typeof(TerrainFile);
		}
	
		public TerrainEntry(Bitmap b,int num,Palette pal,int startX,int startY)
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

			return new TerrainEntry(stuff,p,-1);
		}
	}
}
*/