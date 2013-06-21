/*using System;
using System.Windows.Forms;
using System.Drawing;
using XCom.Interfaces;

namespace XCom
{
	public class SCRImage:XCImage
	{
		public SCRImage(Palette p,System.IO.Stream s,int w, int h)
		{
			Palette=p;
			idx = new byte[w*h];
			s.Read(idx,0,idx.Length);

			image = Bmp.MakeBitmap8(w,h,idx,p.Colors);
		}

		public SCRImage(Palette p, string file):this(p,System.IO.File.OpenRead(file),320,200){}
	}
}
*/