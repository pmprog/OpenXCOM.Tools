using System;
using System.Windows.Forms;
using System.Drawing;
using XCom;

namespace PckView
{
	public delegate void MouseStuff(int num1, int num2);

	public class ViewPck : Panel
	{
		private PckFile pckFile;

		private int space=2;
		private int width=32,height=40;
		private Color goodColor = Color.FromArgb(204,204,255);
		private int clickX, clickY,moveX, moveY;

		public event MouseStuff MouseThing;

		public ViewPck()
		{
			pckFile=null;
			Width = 8*(width+2*space);
			this.Paint+=new PaintEventHandler(paint);	
			this.SetStyle(ControlStyles.DoubleBuffer|ControlStyles.UserPaint|ControlStyles.AllPaintingInWmPaint,true);
			this.MouseDown+=new MouseEventHandler(click);
			this.MouseMove+=new MouseEventHandler(moving);
			clickX = clickY = -1;
		}

		public PckFile Pck
		{
			get{return pckFile;}
			set{pckFile = value;if(pckFile!=null)Height=((pckFile.Size/8)+1)*(height+2*space);}
		}

		public PckImage Selected
		{
			get{if(pckFile!=null)return pckFile[clickY*8+clickX];return null;}
		}

		private void moving(object sender, MouseEventArgs e)
		{
			int x = e.X/(width+2*space);
			int y = e.Y/(height+2*space);

			if(x!=moveX || y != moveY)
			{
				moveX = x;
				moveY = y;
				if(MouseThing != null)
					MouseThing(clickY*8+clickX,moveY*8+moveX);
			}
		}

		private void click(object sender, MouseEventArgs e)
		{
			clickX = e.X/(width+2*space);
			clickY = e.Y/(height+2*space);

			Refresh();

			if(MouseThing != null)
				MouseThing(clickY*8+clickX,moveY*8+moveX);
		}

		private void paint(object sender, PaintEventArgs e)
		{
			if(pckFile!=null)
			{
				Graphics g = e.Graphics;

				g.FillRectangle(new SolidBrush(goodColor),clickX*(width+2*space)-space,clickY*(height+2*space)-space,width+2*space,height+2*space);

				for(int i=0;i<9;i++)
					g.DrawLine(Pens.Black,new Point(i*(width+2*space)-space,0),new Point(i*(width+2*space)-space,Height));
				for(int i=0;i<pckFile.Size/8+1;i++)
					g.DrawLine(Pens.Black,new Point(0,i*(height+2*space)-space),new Point(Width,i*(height+2*space)-space));

				for(int i=0;i<pckFile.Size;i++)
				{
					int x = i%8;
					int y = i/8;
					try
					{
						g.DrawImage(pckFile[i].Image,x*(width+2*space),y*(height+2*space));
					}
					catch(Exception)
					{}
				}
			}
		}
	}
}
