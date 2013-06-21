/*using System;
using System.Drawing;

namespace XCom.Interfaces
{
	public abstract class IXCImage:ITile
	{
		private bool hq=false;
		private int width=320,height=200;

		public virtual byte TransparentIndex
		{
			get{return 254;}
		}

		public virtual string Extension
		{
			get{return "";}
		}

		public override void Hq2x()
		{
			image = Bmp.Hq2x(image);
			//scale*=2;
		}

		public int Width
		{
			get{return width;}
			set{width=value;}
		}

		public int Height
		{
			get{return height;}
			set{height=value;}
		}
	}
}*/