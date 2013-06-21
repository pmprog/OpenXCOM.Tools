using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using XCom;
using XCom.Interfaces;

namespace PckView
{
	/// <summary>
	/// Summary description for EditorPane.
	/// </summary>
	public class EditorPane:Panel
	{
		private XCImage img;
		private Palette pal;
		private bool lines;
		private int square=1;
		private int imgWidth,imgHeight;
		private double scale=1.0;

		public EditorPane(XCImage img)
		{
			this.SetStyle(ControlStyles.DoubleBuffer|ControlStyles.UserPaint|ControlStyles.AllPaintingInWmPaint,true);
			this.img = img;
			pal=null;
			lines=false;

			imgWidth = PckImage.Width*square;
			imgHeight = PckImage.Height*square;
		}

		public int PreferredWidth
		{
			get{return PckImage.Width*10;}
		}

		public int PreferredHeight
		{
			get{return PckImage.Height*10;}
		}

		public double ScaleVal
		{
			get{return scale;}
			set{scale=value;Refresh();}
		}

		public bool Lines
		{
			get{return lines;}
			set{lines=value;Refresh();}
		}

		public XCImage Image
		{
			get{return img;}
			set{img=value;Refresh();}
		}

		public Palette Palette
		{
			get{return pal;}
			set{pal=value;if(img!=null){img.Image.Palette=pal.Colors;Refresh();}}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			int wid = img.Image.Width;
			int hei = img.Image.Height;

			for(int y=0;y<hei;y++)
				for(int x=0;x<wid;x++)
					g.FillRectangle(new SolidBrush(img.Image.GetPixel(x,y)),x*(int)(square*scale),y*(int)(square*scale),(int)(square*scale),(int)(square*scale));	

			if(lines)
			{
				for(int x=0;x<wid+1;x++)
					g.DrawLine(Pens.Black,x*(int)(square*scale),0,x*(int)(square*scale),hei*(int)(square*scale));
				for(int y=0;y<hei+1;y++)
					g.DrawLine(Pens.Black,0,y*(int)(square*scale),wid*(int)(square*scale),y*(int)(square*scale));
			}
		}

		public void SelectColor(int index)
		{

		}
	}
}
