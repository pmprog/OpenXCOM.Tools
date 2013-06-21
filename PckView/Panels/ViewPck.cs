using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using XCom.Interfaces;
using XCom;

namespace PckView
{
	public delegate void PckViewMouseClicked(int num);
	public delegate void PckViewMouseMoved(int moveNum);

	public class ViewPck : Panel
	{
		private XCImageCollection myFile;

		private int space=2;
		private Color goodColor = Color.FromArgb(204,204,255);
		private SolidBrush goodBrush = new SolidBrush(Color.FromArgb(204,204,255));
		private int clickX, clickY,moveX, moveY;
		private int startY;

		public event PckViewMouseClicked ViewClicked;
		public event PckViewMouseMoved ViewMoved;

		public ViewPck()
		{
			//pckFile=null;
			this.Paint+=new PaintEventHandler(paint);	
			this.SetStyle(ControlStyles.DoubleBuffer|ControlStyles.UserPaint|ControlStyles.AllPaintingInWmPaint,true);
			this.MouseDown+=new MouseEventHandler(click);
			this.MouseMove+=new MouseEventHandler(moving);
			clickX = clickY = -1;
			startY=0;	
		}

		//saves a bitmap as a 8-bit image
		public void SaveBMP(string file,Palette pal)
		{
			Bmp.SaveBMP(file,myFile,pal,numAcross(),1);
		}

		public void Hq2x()
		{
			myFile.Hq2x();
		}

		public Palette Pal
		{
			get{return myFile.Pal;}
			set{if(myFile!=null)myFile.Pal=value;}
		}

		public int StartY
		{
			set{startY=value;Refresh();}
		}

		public int PreferredHeight
		{
			get
			{
				if(myFile!=null)
					return calcHeight();
				else
					return 0;
			}
		}

		public XCImageCollection Collection
		{
			get{return myFile;}
			set
			{
				myFile=value;
				if(myFile!=null)
					Height = calcHeight();

				click(null,new MouseEventArgs(MouseButtons.Left,1,0,0,0));
				moving(null,new MouseEventArgs(MouseButtons.Left,1,0,0,0));
				Refresh();
			}	
		}

		public XCImage Selected
		{
			get{if(myFile!=null)return myFile[clickY*numAcross()+clickX];return null;}
			set{if(myFile!=null)myFile[clickY*numAcross()+clickX]=value;}
		}

		private void moving(object sender, MouseEventArgs e)
		{
			if(myFile!=null)
			{
				int x = e.X/(myFile.IXCFile.ImageSize.Width+2*space);
				int y = (e.Y-startY)/(myFile.IXCFile.ImageSize.Height+2*space);

				if(x!=moveX || y != moveY)
				{
					moveX = x;
					moveY = y;

					if(moveX>=numAcross())moveX=numAcross()-1;

					if(ViewMoved != null)
						ViewMoved(moveY*numAcross()+moveX);
				}
			}
		}

		private void click(object sender, MouseEventArgs e)
		{
			if(myFile!=null)
			{
				clickX = e.X/(myFile.IXCFile.ImageSize.Width+2*space);
				clickY = (e.Y-startY)/(myFile.IXCFile.ImageSize.Height+2*space);

				if(clickX>=numAcross())clickX=numAcross()-1;

				Refresh();

				if(ViewClicked != null)
					ViewClicked(clickY*numAcross()+clickX);
			}
		}

		private void paint(object sender, PaintEventArgs e)
		{
			if(myFile!=null && myFile.Count>0)
			{
				Graphics g = e.Graphics;
				if(myFile.IXCFile.FileOptions.BitDepth==8 && myFile[0].Palette.Transparent.A==0)
					g.FillRectangle(goodBrush,clickX*(myFile.IXCFile.ImageSize.Width+2*space)-space,startY+clickY*(myFile.IXCFile.ImageSize.Height+2*space)-space,myFile.IXCFile.ImageSize.Width+2*space,myFile.IXCFile.ImageSize.Height+2*space);
				else
					g.FillRectangle(Brushes.Red,clickX*(myFile.IXCFile.ImageSize.Width+2*space)-space,startY+clickY*(myFile.IXCFile.ImageSize.Height+2*space)-space,myFile.IXCFile.ImageSize.Width+2*space,myFile.IXCFile.ImageSize.Height+2*space);

				for(int i=0;i<numAcross()+1;i++)
					g.DrawLine(Pens.Black,new Point(i*(myFile.IXCFile.ImageSize.Width+2*space)-space,startY),new Point(i*(myFile.IXCFile.ImageSize.Width+2*space)-space,Height-startY));
				for(int i=0;i<myFile.Count/numAcross()+1;i++)
					g.DrawLine(Pens.Black,new Point(0,startY+i*(myFile.IXCFile.ImageSize.Height+2*space)-space),new Point(Width,startY+i*(myFile.IXCFile.ImageSize.Height+2*space)-space));

				for(int i=0;i<myFile.Count;i++)
				{
					int x = i%numAcross();
					int y = i/numAcross();
					try
					{
						g.DrawImage(myFile[i].Image,x*(myFile.IXCFile.ImageSize.Width+2*space),startY+y*(myFile.IXCFile.ImageSize.Height+2*space));
					}
					catch(Exception)
					{}
				}
			}
		}

		private int numAcross()
		{
			return Math.Max(1,(Width-8)/(myFile.IXCFile.ImageSize.Width+2*space));
		}

		private int calcHeight()
		{
			return (((myFile.Count)/numAcross()))*(myFile.IXCFile.ImageSize.Height+2*space)+60;
		}
	}
}
