using System;
using System.Windows.Forms;
using System.Drawing;

namespace XCom
{
	public delegate void TileChangedDelegate(XCTile newTile);

	public class McdView:Panel
	{/*
		public event TileChangedDelegate TileChanged;

		private McdFile openFile;
		private int startY,space;
		private ScrollBar scrollBar;
		private const int width = PckImage.IMAGE_WIDTH;
		private const int height = PckImage.IMAGE_HEIGHT;
		private const int tilesAcross=8;
		private int curr;
		private Timer time;
		private Point clickPoint;
		private SolidBrush brush = new SolidBrush(Color.FromArgb(204,204,255));
		private Label selTile;

		private CheckBox animating;

		public McdView()
		{
			openFile=null;
			this.SetStyle(ControlStyles.DoubleBuffer|ControlStyles.AllPaintingInWmPaint|ControlStyles.UserPaint,true);
			scrollBar = new VScrollBar();

			animating = new CheckBox();
			animating.Text = "Animating";
			animating.Location = new Point(0,0);
			animating.CheckedChanged+=new EventHandler(check);

			selTile = new Label();
			selTile.Text = "Selected tile: ";
			selTile.Location = new Point(animating.Right,0);
			selTile.Width = Width-animating.Width;
			selTile.Height = animating.Height;

			scrollBar.Minimum = -animating.Height;
			scrollBar.Scroll+=new ScrollEventHandler(scroll);
			scrollBar.Location = new Point(Width-scrollBar.Width,selTile.Bottom);
			scrollBar.Height = this.Height-selTile.Height;


			Width = tilesAcross*(width+2*space)+scrollBar.Width;
			startY=0;
			space=2;
			curr=0;
			time = new Timer();
			time.Tick+=new EventHandler(tick);
			time.Interval=150;

			Controls.Add(scrollBar);
			Controls.Add(animating);
			Controls.Add(selTile);
			clickPoint = new Point(0,0);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			clickPoint.X=e.X/(width+2*space);
			clickPoint.Y=(e.Y-startY)/(height+2*space);

			if(TileChanged!=null)
				TileChanged((Tile)openFile[clickPoint.Y*tilesAcross+clickPoint.X]);
			selTile.Text = "Selected tile: "+(clickPoint.Y*tilesAcross+clickPoint.X);
			Refresh();
		}

		public Tile SelectedTile
		{
			get
			{
				try
				{
					return (Tile)openFile[clickPoint.Y*tilesAcross+clickPoint.X];
				}
				catch{return null;}
			}
		}

		private void check(object sender, EventArgs e)
		{
			if(animating.Checked)
				time.Start();
			else
				time.Stop();
		}

		private void tick(object sender, EventArgs e)
		{
			curr = (curr+1)%8;
			Refresh();
		}

		public int CurrentImage
		{
			get{return curr;}
			set{curr=value;}
		}

		protected override void OnResize(EventArgs e)
		{
			selTile.Width = Width-animating.Width;
			scrollBar.Location = new Point(Width-scrollBar.Width,selTile.Bottom);
			scrollBar.Height = this.Height-selTile.Height;
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
			scrollBar.Maximum = ((openFile.Length/8)+1)*(PckImage.IMAGE_HEIGHT+space);
			Refresh();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if(openFile==null)
				return;

			Graphics g = e.Graphics;

			g.FillRectangle(brush,clickPoint.X*(width+2*space)-space,startY+clickPoint.Y*(height+2*space),width+2*space,height+space);

			for(int i=0;i<9;i++)
				g.DrawLine(Pens.Black,new Point(i*(width+2*space)-space,0),new Point(i*(width+2*space)-space,Height));
			for(int i=0;i<openFile.Length/8+1;i++)
				g.DrawLine(Pens.Black,new Point(0,startY+i*(height+2*space)-space),new Point(Width,startY+i*(height+2*space)-space));

			for(int i=0;i<openFile.Length;i++)
			{
				int x = i%tilesAcross;
				int y = i/tilesAcross;
				try
				{
					g.DrawImage(openFile[i][curr].Image,x*(width+2*space),-openFile[i].Info.TileOffset+startY+y*(height+2*space));
				}
				catch(Exception)
				{}
			}
		}

		public McdFile OpenFile
		{
			get{return openFile;}
			set{openFile=value;reload();}
		}*/
	}
}
