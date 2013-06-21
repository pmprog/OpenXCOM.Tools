using System;
using System.Windows.Forms;
using System.Collections;
using XCom;
using XCom.Interfaces;

namespace PckView
{
	/// <summary>
	/// Summary description for SinglePanel.
	/// </summary>
	public class SinglePanel : Panel
	{
		private XCImage img;

		public SinglePanel()
		{
			Width = PckImage.Width;
			Height = PckImage.Height;
		}

		public XCImage Image
		{
			get{return img;}
			set{img=value;Width=img.Image.Width;Height=img.Image.Height;Refresh();}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if(img!=null)
				e.Graphics.DrawImage(img.Image,0,0);
		}

		public Palette Palette
		{
			set{if(img!=null){img.Image.Palette = value.Colors;Refresh();}}
		}

	}
}
