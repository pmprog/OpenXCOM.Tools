using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using XCom.Interfaces.Base;
using XCom;
using System.Windows.Forms;

namespace MapView.TopViewForm
{
	public class TopViewPanel : SimpleMapPanel
	{
		private bool blank = false;
		private MenuItem g, n, w, c;
		private BottomPanel bottomPanel;

		public TopViewPanel()
		{
			MapViewPanel.Instance.View.DragChanged+=new EventHandler(viewDrag);
		}

		public MenuItem Ground
		{
			get { return g; }
			set { g = value; }
		}

		public MenuItem North
		{
			get { return n; }
			set { n = value; }
		}

		public MenuItem West
		{
			get { return w; }
			set { w = value; }
		}

		public MenuItem Content
		{
			get { return c; }
			set { c = value; }
		}

		public BottomPanel BottomPanel
		{
			get { return bottomPanel; }
			set { bottomPanel = value; }
		}

		public int MinHeight
		{
			get { return minHeight; }
			set { minHeight = value; ParentSize(Width, Height); }
		}

		protected override void RenderCell(IMapTile tile, System.Drawing.Graphics g, int x, int y)
		{
			XCMapTile mapTile = (XCMapTile)tile;
			if (!blank)
			{
				if (mapTile.Ground != null && this.g.Checked)
					g.FillPath(Brushes["GroundColor"], UpperPath(x,y));

				if (mapTile.North != null && n.Checked)
					g.DrawLine(Pens["NorthColor"], x, y, x + hWidth, y + hHeight);

				if (mapTile.West != null && w.Checked)
					g.DrawLine(Pens["WestColor"], x, y, x - hWidth, y + hHeight);

				if (mapTile.Content != null && c.Checked)
					g.FillPath(Brushes["ContentColor"], LowerPath(x,y));
			}
			else
			{
				if (!mapTile.DrawAbove)
				{
					g.FillPath(System.Drawing.Brushes.DarkGray, UpperPath(x, y));
					g.FillPath(System.Drawing.Brushes.DarkGray, LowerPath(x, y));
				}
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			ControlPaint.DrawBorder3D(e.Graphics, ClientRectangle,Border3DStyle.Etched);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			if (e.Button == MouseButtons.Right)
				bottomPanel.SetSelected(e.Button, 1);
			else if (e.Button == MouseButtons.Left)
			    viewDrag(null, null);
		}
	}
}
