using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace XCom
{
	public class MapView :Panel
	{/*
		private MapFile map;
		private Point origin = new Point(100,0);
		private Point offsetPt = new Point(0,0);
		private Point clickPoint,clickPointx;

		private int currentLevel,curr;

		private const int hWid=16, hHeight = 8;

		private Button up,down;
		private HScrollBar horiz;
		private VScrollBar vert;
		private CheckBox animated;

		private Timer time;

		public MapView()
		{
			map=null;
			clickPoint = clickPointx=new Point(-1,-1);
			currentLevel=0;
			curr=0;

			this.SetStyle(ControlStyles.Opaque|ControlStyles.DoubleBuffer|ControlStyles.UserPaint|ControlStyles.AllPaintingInWmPaint,true);

			up = new Button();
			up.Text = "Up";
			up.Click+=new EventHandler(Up);
			up.Location = new Point(0,0);
			Controls.Add(up);

			down = new Button();
			down.Text = "Down";
			down.Click+=new EventHandler(Down);
			down.Location = new Point(up.Right,0);
			down.Height = up.Height;
			Controls.Add(down);

			animated = new CheckBox();
			animated.Text = "Animated";
			animated.Location = new Point(down.Right,0);
			animated.Height = down.Height;
			animated.CheckedChanged+=new EventHandler(check);
			animated.Width = Width-down.Width-up.Width;
			Controls.Add(animated);

			vert = new VScrollBar();
			vert.Location = new Point(Width-vert.Width,animated.Bottom);
			vert.Height = Height-animated.Height;
			vert.Scroll+=new ScrollEventHandler(vScroll);
			Controls.Add(vert);

			horiz = new HScrollBar();
			horiz.Location = new Point(0,Height-horiz.Height);
			horiz.Width = Width-vert.Width;
			horiz.Scroll+=new ScrollEventHandler(hScroll);
			Controls.Add(horiz);

			time = new Timer();
			time.Interval=150;
			time.Tick+=new EventHandler(tick);
			clickPointx = new Point(0,0);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			clickPointx = convertCoordsDiamond(new Point(e.X,e.Y),currentLevel);
			if(clickPointx.Y>map.Rows || clickPointx.Y<0)
				clickPointx.Y=0;
			if(clickPointx.X>map.Cols || clickPointx.X<0)
				clickPointx.X=0;
			Refresh();
		}

		private void hScroll(object sender, ScrollEventArgs e)
		{
			offsetPt.X= origin.X-horiz.Value;
			Refresh();
		}

		private void vScroll(object sender,ScrollEventArgs e)
		{
			offsetPt.Y=origin.Y-vert.Value;
			Refresh();
		}

		protected override void OnResize(EventArgs e)
		{
			animated.Width = Width-down.Width-up.Width;

			vert.Location = new Point(Width-vert.Width,animated.Bottom);
			vert.Height = Height-animated.Height;

			horiz.Location = new Point(0,Height-horiz.Height);
			horiz.Width = Width-vert.Width;
		}

		private void check(object sender, EventArgs e)
		{
			if(animated.Checked)
				time.Start();
			else
				time.Stop();
		}

		private void tick(object sender,EventArgs e)
		{
			curr = (curr+1)%8;
			Refresh();
		}

		public void Down(object sender, EventArgs e)
		{
			if(map==null)
				return;

			map.Down();
			this.currentLevel = map.CurrentLevel;
			Refresh();
		}

		public void Up(object sender, EventArgs e)
		{
			if(map==null)
				return;

			map.Up();
			this.currentLevel = map.CurrentLevel;
			Refresh();
		}

		public int CurrentLevel
		{
			get{return currentLevel;}
			set{currentLevel=value;}
		}

		public MapFile Map
		{
			get{return map;}
			set
			{
				map=value;
				if(map!=null)
				{
					origin = new Point((map.Rows-1)*16,down.Height);
					horiz.Minimum=map.Rows*16-map.Rows*8;
					horiz.Maximum=map.Rows*16+map.Rows*8;
					horiz.Value=map.Rows*16;

					vert.Minimum=0;
					this.currentLevel=map.CurrentLevel;
					Refresh();
				}
			}
		}

		private Font mapFont = new Font("Arial",8);

		protected override void OnPaint(PaintEventArgs e)
		{
			if(map!=null)
			{
				Graphics g = e.Graphics;
				g.DrawString(map.BaseName,mapFont,Brushes.Red,0,25);
				if(map[clickPointx.Y,clickPointx.X,map.CurrentLevel].Ground!=null)
					g.DrawString("Ground id: "+map[clickPointx.Y,clickPointx.X,map.CurrentLevel].Ground.MapID,mapFont,Brushes.Red,0,35);
				if(map[clickPointx.Y,clickPointx.X,map.CurrentLevel].North!=null)
					g.DrawString("North id: "+map[clickPointx.Y,clickPointx.X,map.CurrentLevel].North.MapID,mapFont,Brushes.Red,0,45);
				if(map[clickPointx.Y,clickPointx.X,map.CurrentLevel].West!=null)
					g.DrawString("West id: "+map[clickPointx.Y,clickPointx.X,map.CurrentLevel].West.MapID,mapFont,Brushes.Red,0,55);
				if(map[clickPointx.Y,clickPointx.X,map.CurrentLevel].Content!=null)
					g.DrawString("Content id: "+map[clickPointx.Y,clickPointx.X,map.CurrentLevel].Content.MapID,mapFont,Brushes.Red,0,65);

				for(int h=map.Height-1;h>=0;h--)
					if(h>=currentLevel)
					{
						for(int row=0,startX = origin.X+offsetPt.X,startY=origin.Y+offsetPt.Y+(24*h);row<map.Rows;row++,startX-=hWid,startY+=hHeight)
						{
							for(int col=0,x=startX,y=startY;col<map.Cols;col++,x+=hWid,y+=hHeight)
							{
								if(x>Width || y>Height)
									break;

								if(x>-PckImage.IMAGE_WIDTH && y > -PckImage.IMAGE_HEIGHT)
									map[row,col,h].Drawgfx(x,y,g,curr);

								if(row==clickPointx.Y && col == clickPointx.X)
								{
									Pen p = new Pen(Brushes.Red,2);
									//g.DrawLine(Pens.Red,x+16,y,x+16,y+24);
									g.DrawLine(p,x,y+8,x,y+32);
									g.DrawLine(p,x+32,y+8,x+32,y+32);
									g.DrawLine(p,x,y+30,x+16,y+38);
									g.DrawLine(p,x+16,y+38,x+32,y+30);

									g.DrawLine(p,x,y+8,x+16,y+16);
									//g.DrawLine(p,x+16,y+16,x,y+32);
									//g.DrawLine(p,x+16,y+16,x+32,y+32);
									g.DrawLine(p,x+16,y+16,x+32,y+8);
									g.DrawLine(p,x+16,y+16,x+16,y+40);

									g.DrawLine(p,x,y+8,x+16,y);
									g.DrawLine(p,x+16,y,x+32,y+8);
								}
							}
						}
					}
			}
		}

		private Point convertCoordsDiamond(Point p,int level)
		{
			int x = p.X-origin.X-16-offsetPt.X; //16 is half the width of the diamond
			int y = p.Y-origin.Y-24*(level+1)-+offsetPt.Y; //24 is the distance from the top of the diamond to the very top of the image

			double x1 = (x*1.0/32)+(y*1.0/16);
			double x2 = -(x*1.0-2*y*1.0)/32;

			return new Point((int)Math.Floor(x1),(int)Math.Floor(x2));
		}*/
	}
}
