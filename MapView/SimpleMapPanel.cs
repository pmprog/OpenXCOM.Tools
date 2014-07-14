using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using XCom.Interfaces.Base;
using System.Drawing.Drawing2D;
using System.Drawing;
using XCom;
using System.ComponentModel;

namespace MapView.TopViewForm
{
	public class SimpleMapPanel : Map_Observer_Control
	{
		//private DSShared.Windows.RegistryInfo registryInfo;

		private int offX = 0, offY = 0;
		protected int hWidth = 8, hHeight = 4;
		protected int minHeight = 4;

		private GraphicsPath upper;
		private GraphicsPath lower;
		private GraphicsPath cell;
		private GraphicsPath copyArea;
		private GraphicsPath selected;

		private Dictionary<string, SolidBrush> brushes;
		private Dictionary<string, Pen> pens;

		private Point sel1, sel2,sel3,sel4;

		private int mR, mC;

		public SimpleMapPanel()
		{
			//this.Resize += new EventHandler(SimpleMapPanel_Resize);

			upper = new GraphicsPath();
			lower = new GraphicsPath();
			cell = new GraphicsPath();
			selected = new GraphicsPath();
			copyArea = new GraphicsPath();

			sel1 = new Point(0, 0);
			sel2 = new Point(0, 0);
			sel3 = new Point(0, 0);
			sel4 = new Point(0, 0);
		}

		[Browsable(false)]
		[DefaultValue(4)]
		public int HalfHeight
		{
			get { return hHeight; }
			set { hHeight = value; hWidth = 2 * value; }
		}

		public void ParentSize(int width, int height)
		{
			if (map != null)
			{
				int oldWid = hWidth;

			    if (map.MapSize.Rows > 0 || map.MapSize.Cols > 0)
			    {
			        if (height > width / 2)
			        {
			            //use width
			            hWidth = width / (map.MapSize.Rows + map.MapSize.Cols);

			            if (hWidth % 2 != 0)
			                hWidth--;

			            hHeight = hWidth / 2;
			        }
			        else
			        {
			            //use height
			            hHeight = height / (map.MapSize.Rows + map.MapSize.Cols);
			            hWidth = hHeight * 2;
			        }
			    }

			    if (hHeight < minHeight)
				{
					hWidth = minHeight * 2;
					hHeight = minHeight;
				}

				offX = 4 + map.MapSize.Rows * hWidth;
				offY = 4;

				if (oldWid != hWidth)
				{
					Width = 8 + (map.MapSize.Rows + map.MapSize.Cols) * hWidth;
					Height = 8 + (map.MapSize.Rows + map.MapSize.Cols) * hHeight;
					Refresh();
				}
			}
		}

		[Browsable(false)]
		[DefaultValue(null)]
		public override XCom.Interfaces.Base.IMap_Base Map
		{
			set
			{
				map = value;
				hWidth = 7;
				ParentSize(Parent.Width, Parent.Height);
				Refresh();
			}
		}

		protected void viewDrag(object sender, EventArgs ex)
		{
			Point s = new Point(0, 0);
			Point e = new Point(0, 0);

			s.X = Math.Min(MapViewPanel.Instance.View.StartDrag.X, MapViewPanel.Instance.View.EndDrag.X);
			s.Y = Math.Min(MapViewPanel.Instance.View.StartDrag.Y, MapViewPanel.Instance.View.EndDrag.Y);

			e.X = Math.Max(MapViewPanel.Instance.View.StartDrag.X, MapViewPanel.Instance.View.EndDrag.X);
			e.Y = Math.Max(MapViewPanel.Instance.View.StartDrag.Y, MapViewPanel.Instance.View.EndDrag.Y);

			//                 col hei
			sel1.X = offX + (s.X - s.Y) * hWidth;
			sel1.Y = offY + (s.X + s.Y) * hHeight;

			sel2.X = offX + (e.X - s.Y) * hWidth + hWidth;
			sel2.Y = offY + (e.X + s.Y) * hHeight + hHeight;

			sel3.X = offX + (e.X - e.Y) * hWidth;
			sel3.Y = offY + (e.X + e.Y) * hHeight + hHeight + hHeight;

			sel4.X = offX + (s.X - e.Y) * hWidth - hWidth;
			sel4.Y = offY + (s.X + e.Y) * hHeight + hHeight;

			copyArea.Reset();
			copyArea.AddLine(sel1, sel2);
			copyArea.AddLine(sel2, sel3);
			copyArea.AddLine(sel3, sel4);
			copyArea.CloseFigure();

			Refresh();
		}

		[Browsable(false)]
		[DefaultValue(null)]
		public Dictionary<string, SolidBrush> Brushes
		{
			get { return brushes; }
			set { brushes = value; }
		}

		[Browsable(false)]
		[DefaultValue(null)]
		public Dictionary<string, Pen> Pens
		{
			get { return pens; }
			set { pens = value; }
		}

		public override void SelectedTileChanged(IMap_Base sender, SelectedTileChangedEventArgs e)
		{
			//bottom.Tile = (XCMapTile)e.SelectedTile;

			MapLocation pt = e.MapLocation;

			Text = "r: " + pt.Row + " c: " + pt.Col;

			int xc = (pt.Col - pt.Row) * hWidth;
			int yc = (pt.Col + pt.Row) * hHeight;

			selected.Reset();
			selected.AddLine(xc, yc, xc + hWidth, yc + hHeight);
			selected.AddLine(xc + hWidth, yc + hHeight, xc, yc + 2 * hHeight);
			selected.AddLine(xc, yc + 2 * hHeight, xc - hWidth, yc + hHeight);
			selected.CloseFigure();

			viewDrag(null, null);
			Refresh();
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);
			if (e.Delta > 0)
				map.Up();
			else
				map.Down();
		}

		private void convertCoordsDiamond(int x, int y, out int row, out int col)
		{
			//int x = xp - offX; //16 is half the width of the diamond
			//int y = yp - offY; //24 is the distance from the top of the diamond to the very top of the image

			double x1 = (x * 1.0 / (2 * hWidth)) + (y * 1.0 / (2 * hHeight));
			double x2 = -(x * 1.0 - 2 * y * 1.0) / (2 * hWidth);

			row = (int)Math.Floor(x2);
			col = (int)Math.Floor(x1);
			//return new Point((int)Math.Floor(x1), (int)Math.Floor(x2));
		}

		protected virtual void RenderCell(IMapTile tile, System.Drawing.Graphics g, int x, int y) { }

		protected GraphicsPath UpperPath(int x, int y)
		{
			upper.Reset();
			upper.AddLine(x, y, x + hWidth, y + hHeight);
			upper.AddLine(x + hWidth, y + hHeight, x - hWidth, y + hHeight);
			upper.CloseFigure();
			return upper;
		}

		protected GraphicsPath LowerPath(int x, int y)
		{
			lower.Reset();
			lower.AddLine(x, y + 2 * hHeight, x + hWidth, y + hHeight);
			lower.AddLine(x + hWidth, y + hHeight, x - hWidth, y + hHeight);
			lower.CloseFigure();
			return lower;
		}

		protected GraphicsPath CellPath(int xc, int yc)
		{
			cell.Reset();
			cell.AddLine(xc, yc, xc + hWidth, yc + hHeight);
			cell.AddLine(xc + hWidth, yc + hHeight, xc, yc + 2 * hHeight);
			cell.AddLine(xc, yc + 2 * hHeight, xc - hWidth, yc + hHeight);
			cell.CloseFigure();
			return cell;
		}

		protected override void Render(System.Drawing.Graphics g)
		{
			g.FillRectangle(System.Drawing.SystemBrushes.Control, ClientRectangle);

			if (map != null)
			{
				for (int row = 0, startX = offX, startY = offY; row < map.MapSize.Rows; row++, startX -= hWidth, startY += hHeight)
				{
					for (int col = 0, x = startX, y = startY; col < map.MapSize.Cols; col++, x += hWidth, y += hHeight)
					{
						IMapTile mapTile = map[row, col];

						if (mapTile != null)
							RenderCell(mapTile, g, x, y);
					}
				}

				for (int i = 0; i <= map.MapSize.Rows; i++)
					g.DrawLine(pens["GridColor"], offX - i * hWidth, offY + i * hHeight, ((map.MapSize.Cols - i) * hWidth) + offX, ((i + map.MapSize.Cols) * hHeight) + offY);
				for (int i = 0; i <= map.MapSize.Cols; i++)
					g.DrawLine(pens["GridColor"], offX + i * hWidth, offY + i * hHeight, (i * hWidth) - map.MapSize.Rows * hWidth + offX, (i * hHeight) + map.MapSize.Rows * hHeight + offY);

				if (copyArea != null)
					g.DrawPath(pens["SelectColor"], copyArea);

				//				if(selected!=null) //clicked on
				//					g.DrawPath(new Pen(Brushes.Blue,2),selected);

				if (mR < map.MapSize.Rows && mC < map.MapSize.Cols && mR >= 0 && mC >= 0)
				{
					int xc = (mC - mR) * hWidth + offX;
					int yc = (mC + mR) * hHeight + offY;

					GraphicsPath selPath = CellPath(xc, yc);
					g.DrawPath(pens["MouseColor"], selPath);
				}
			}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			int row, col;
		    if (map == null) return;
			convertCoordsDiamond(e.X - offX, e.Y - offY,out row, out col);
			map.SelectedTile = new MapLocation(row,col, map.CurrentHeight);
			mDown = true;

			Point p = new Point(col, row);

			MapViewPanel.Instance.View.StartDrag = p;
			MapViewPanel.Instance.View.EndDrag = p;
		}

		private bool mDown = false;
		protected override void OnMouseUp(MouseEventArgs e)
		{
			mDown = false;
			MapViewPanel.Instance.View.Refresh();
			Refresh();
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			int row, col;
			convertCoordsDiamond(e.X - offX, e.Y - offY,out row, out col);
			if (row != mR || col != mC)
			{
				mR = row;
				mC = col;

				if (mDown)
				{
					MapViewPanel.Instance.View.EndDrag = new Point(col,row);
					MapViewPanel.Instance.View.Refresh();
					viewDrag(null, null);
				}
				Refresh();
			}
		}
	}
}
