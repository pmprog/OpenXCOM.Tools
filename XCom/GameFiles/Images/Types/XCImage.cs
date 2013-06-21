using System;
using System.Drawing;

namespace XCom.Interfaces
{
	public class XCImage:ICloneable
	{
		protected byte[] idx;
		protected int fileNum;
		protected Bitmap image;
		protected Bitmap gray;
		private Palette palette;
		private byte transparent=0xFE;

		//entries must not be compressed
		public XCImage(byte[] entries,int width, int height, Palette pal,int idx)
		{
			fileNum=idx;
			this.idx=entries;
			palette=pal;

			if(pal!=null)
				image = Bmp.MakeBitmap8(width,height,entries,pal.Colors);
		}

		public XCImage():this(new byte[]{},0,0,null,-1)
		{ }

		public XCImage(Bitmap b,int idx)
		{
			fileNum = idx;
			image = b;
			this.idx = null;
			palette = null;
		}

		public byte[] Bytes{get{return idx;}}
		public int FileNum{get{return fileNum;}set{fileNum=value;}}
		public Bitmap Image{get{return image;}}
		public Palette Palette{get{return palette;}
			set
			{
				palette=value;

				if(image!=null)
					image.Palette=palette.Colors;				
			}
		}
		public virtual byte TransparentIndex{get{return transparent;}}
		public Bitmap Gray { get { return gray; } }

		public object Clone()
		{
			if (idx != null)
			{
				byte[] b = new byte[idx.Length];
				for (int i = 0; i < b.Length; i++)
					b[i] = idx[i];

				return new XCImage(b, image.Width, image.Height, palette, fileNum);
			}
			else if (image != null)
				return new XCImage((Bitmap)image.Clone(), fileNum);
			else
				return null;
		}

		public virtual void Hq2x()
		{
			image = Bmp.Hq2x(image);
		}
	}
}
