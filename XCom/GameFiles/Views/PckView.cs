/*using System;
using System.Windows.Forms;
using System.Drawing;

namespace XCom
{
	public class PckView:Panel
	{/*
		private PckFile openFile;
		private int startY,space;
		private ScrollBar scrollBar;
		private const int width = PckImage.IMAGE_WIDTH;
		private const int height = PckImage.IMAGE_HEIGHT;

		public PckView()
		{
			openFile=null;
			this.SetStyle(ControlStyles.DoubleBuffer|ControlStyles.AllPaintingInWmPaint|ControlStyles.UserPaint,true);
			scrollBar = new VScrollBar();
			Width = 8*(width+2*space)+scrollBar.Width;
			scrollBar.Minimum = 0;
			scrollBar.Scroll+=new ScrollEventHandler(scroll);
			scrollBar.Location = new Point(Width-scrollBar.Width,0);
			scrollBar.Height = this.Height;
			startY=0;
			space=2;

			Controls.Add(scrollBar);
		}

		protected override void OnResize(EventArgs e)
		{
			scrollBar.Location = new Point(Width-scrollBar.Width,0);
			scrollBar.Height = this.Height;
		}

		private void scroll(object sender, ScrollEventArgs e)
		{
			startY = -scrollBar.Value;
			Refresh();
		}

		private void reload()
		{
			if(openFile==null)
				return;
			scrollBar.Value=scrollBar.Minimum;
			startY=-scrollBar.Value;
			scrollBar.Maximum = ((openFile.Size/8)+1)*(PckImage.IMAGE_HEIGHT+space);
			Refresh();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if(openFile==null)
				return;

			Graphics g = e.Graphics;

			for(int i=0;i<9;i++)
				g.DrawLine(Pens.Black,new Point(i*(width+2*space)-space,0),new Point(i*(width+2*space)-space,Height));
			for(int i=0;i<openFile.Size/8+1;i++)
				g.DrawLine(Pens.Black,new Point(0,startY+i*(height+2*space)-space),new Point(Width,startY+i*(height+2*space)-space));

			for(int i=0;i<openFile.Size;i++)
			{
				int x = i%8;
				int y = i/8;
				try
				{
					g.DrawImage(openFile[i].Image,x*(width+2*space),startY+y*(height+2*space));
				}
				catch(Exception)
				{}
			}
		}

		public PckFile OpenFile
		{
			get{return openFile;}
			set{openFile=value;reload();}
		}*/
//	}
//}
