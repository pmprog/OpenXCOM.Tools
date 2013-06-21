/*using System;

namespace XCom
{
	public abstract class ImageFileDescriptor
	{
		protected Palette pal;
		protected int width,height;
		protected bool multiImage;

		public ImageFileDescriptor()
		{
			pal=null;
			width=height=100;
			multiImage=false;
		}

		public int DefaultWidth
		{
			get{return width;}
		}

		public int DefaultHeight
		{
			get{return height;}
		}		

		public Palette DefaultPalette
		{
			get{return pal;}
		}
	}

	public abstract class ScreenDesc:ImageFileDescriptor
	{
		public ScreenDesc()
		{
			this.width=320;
			this.height=200;
		}
	}
}
*/