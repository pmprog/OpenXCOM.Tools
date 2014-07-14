using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;
using XCom;
using XCom.Interfaces.Base;

namespace MapView.TopViewForm
{
	public class BottomPanel : Map_Observer_Control
	{
		private XCMapTile mapTile;
		private MapLocation lastLoc;
		private Font font;

		private const int tileWidth = 32;
		private const int tileHeight = 40;
		private const int space = 2;
		private const int startX = 5, startY = 0;
		private readonly Color goodColor = Color.FromArgb(204, 204, 255);
		private SolidBrush brush = new SolidBrush(Color.FromArgb(204, 204, 255));

		private Dictionary<string, SolidBrush> brushes;
		private Dictionary<string, Pen> pens;

		private XCMapTile.MapQuadrant selected;
		//private TileView tileView;
		//private Button btnCopy, btnPaste, btnCut, btnUp, btnDown;
		public event EventHandler PanelClicked;

		public BottomPanel()
		{
			mapTile = null;
			font = new Font("Arial", 8);
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.UserPaint, true);
			selected = XCMapTile.MapQuadrant.Ground;

			//tileView = TileView.Instance;

			/*
			btnCut = new Button();
			btnPaste = new Button();
			btnCopy = new Button();
			btnUp = new Button();
			btnDown = new Button();

			Assembly a = Assembly.GetExecutingAssembly();
			btnCut.Image = Bitmap.FromStream(a.GetManifestResourceStream("MapView._Embedded.cut.gif"));
			btnPaste.Image = Bitmap.FromStream(a.GetManifestResourceStream("MapView._Embedded.paste.gif"));
			btnCopy.Image = Bitmap.FromStream(a.GetManifestResourceStream("MapView._Embedded.copy.gif"));
			btnUp.Image = Bitmap.FromStream(a.GetManifestResourceStream("MapView._Embedded.up.gif"));
			btnDown.Image = Bitmap.FromStream(a.GetManifestResourceStream("MapView._Embedded.down.gif"));

			btnCut.Left = 200;
			btnCut.Anchor = AnchorStyles.Top | AnchorStyles.Left;
			btnCut.Size = new Size(25, 25);
			btnCut.Click += new EventHandler(MapViewPanel.Instance.Cut_click);

			btnCopy.Left = btnCut.Right;
			btnCopy.Anchor = btnCut.Anchor;
			btnCopy.Size = btnCut.Size;
			btnCopy.Click += new EventHandler(MapViewPanel.Instance.Copy_click);

			btnPaste.Left = btnCopy.Right;
			btnPaste.Anchor = btnCopy.Anchor;
			btnPaste.Size = btnCopy.Size;
			btnPaste.Click += new EventHandler(MapViewPanel.Instance.Paste_click);

			btnUp.Left = btnPaste.Right;
			btnUp.Anchor = btnPaste.Anchor;
			btnUp.Size = btnPaste.Size;
			btnUp.Click += new EventHandler(Down_click);

			btnDown.Left = btnUp.Right;
			btnDown.Anchor = btnUp.Anchor;
			btnDown.Size = btnUp.Size;
			btnDown.Click += new EventHandler(Up_click);*/

			Globals.LoadExtras();

			//Controls.AddRange(new Control[] { btnCut, btnCopy, btnPaste, btnUp, btnDown });
		}

		[Browsable(false)]
		public Dictionary<string, SolidBrush> Brushes
		{
			get { return brushes; }
			set { brushes = value; }
		}

		[Browsable(false)]
		public Dictionary<string, Pen> Pens
		{
			get { return pens; }
			set { pens = value; }
		}

		//public void Down_click(object sender, EventArgs e)
		//{
		//    MapViewPanel.Instance.View.Map.Up();
		//}

		//public void Up_click(object sender, EventArgs e)
		//{
		//    MapViewPanel.Instance.View.Map.Down();
		//}

		[Browsable(false)]
		public SolidBrush SelectColor
		{
			get { return brush; }
			set { brush = value; Refresh(); }
		}

		[Browsable(false)]
		public XCMapTile Tile
		{
			get { return mapTile; }
			set { mapTile = value; Refresh(); }
		}

		public XCMapTile.MapQuadrant SelectedQuadrant
		{
			get { return selected; }
		}

		public void SetSelected(MouseButtons btn, int clicks)
		{
			if (btn == MouseButtons.Right && mapTile != null)
			{
				if (clicks == 1)
					mapTile[selected] = TileView.Instance.SelectedTile;
				else if (clicks == 2)
					mapTile[selected] = null;
				Globals.MapChanged = true;
			}
			else if (btn == MouseButtons.Left && mapTile != null)
			{
				if (clicks == 2)
					TileView.Instance.SelectedTile = mapTile[selected];
			}
		}

		public override void HeightChanged(IMap_Base sender, HeightChangedEventArgs e)
		{
			lastLoc.Height = e.NewHeight;
			mapTile = (XCMapTile)map[lastLoc.Row, lastLoc.Col];
			Refresh();
		}

		public override void SelectedTileChanged(IMap_Base sender, SelectedTileChangedEventArgs e)
		{
			mapTile = (XCMapTile)e.SelectedTile;
			lastLoc = e.MapLocation;
			Refresh();
		}

		private void visChanged(object sender, EventArgs e)
		{
			Refresh();
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			selected = (XCMapTile.MapQuadrant)((e.X - startX) / (tileWidth + 2 * space));

			SetSelected(e.Button, e.Clicks);

			if (PanelClicked != null)
				PanelClicked(this, new EventArgs());

			Refresh();
		}

		protected override void Render(Graphics g)
		{
			if (DesignMode)
			{
				g.DrawLine(System.Drawing.Pens.Black, 0, 0, Width, Height);
				g.DrawLine(System.Drawing.Pens.Black, 0, Height, Width, 0);

				ControlPaint.DrawBorder3D(g, ClientRectangle, Border3DStyle.Flat);
				return;
			}

			if (mapTile != null)
			{
				if (selected == XCMapTile.MapQuadrant.Ground) g.FillRectangle(brush, startX, startY, tileWidth + 1, tileHeight + 2);
				else if (selected == XCMapTile.MapQuadrant.West) g.FillRectangle(brush, startX + ((tileWidth + 2 * space)), startY, tileWidth + 1, tileHeight + 2);
				else if (selected == XCMapTile.MapQuadrant.North) g.FillRectangle(brush, startX + (2 * (tileWidth + 2 * space)), startY, tileWidth + 1, tileHeight + 2);
				else if (selected == XCMapTile.MapQuadrant.Content) g.FillRectangle(brush, startX + (3 * (tileWidth + 2 * space)), startY, tileWidth + 1, tileHeight + 2);

				if (!TopView.Instance.GroundVisible)
					g.FillRectangle(System.Drawing.Brushes.DarkGray, startX, startY, tileWidth + 1, tileHeight + 2);

				if (mapTile.Ground != null)
				{
					g.DrawImage(mapTile.Ground[MapViewPanel.Current].Image, startX, startY - mapTile.Ground.Info.TileOffset);
					if (mapTile.Ground.Info.HumanDoor || mapTile.Ground.Info.UFODoor)
						g.DrawString("Door", Font, System.Drawing.Brushes.Black, startX, startY + PckImage.Height - Font.Height);
				}
				else
					g.DrawImage(Globals.ExtraTiles[3].Image, startX, startY);

				if (!TopView.Instance.WestVisible)
					g.FillRectangle(System.Drawing.Brushes.DarkGray, startX + ((tileWidth + 2 * space)), startY, tileWidth + 1, tileHeight + 2);

				if (mapTile.West != null)
				{
					g.DrawImage(mapTile.West[MapViewPanel.Current].Image, startX + ((tileWidth + 2 * space)), startY - mapTile.West.Info.TileOffset);
					if (mapTile.West.Info.HumanDoor || mapTile.West.Info.UFODoor)
						g.DrawString("Door", Font, System.Drawing.Brushes.Black, startX + ((tileWidth + 2 * space)), startY + PckImage.Height - Font.Height);
				}
				else
					g.DrawImage(Globals.ExtraTiles[1].Image, startX + ((tileWidth + 2 * space)), startY);

				if (!TopView.Instance.NorthVisible)
					g.FillRectangle(System.Drawing.Brushes.DarkGray, startX + (2 * (tileWidth + 2 * space)), startY, tileWidth + 1, tileHeight + 2);

				if (mapTile.North != null)
				{
					g.DrawImage(mapTile.North[MapViewPanel.Current].Image, startX + (2 * (tileWidth + 2 * space)), startY - mapTile.North.Info.TileOffset);
					if (mapTile.North.Info.HumanDoor || mapTile.North.Info.UFODoor)
						g.DrawString("Door", Font, System.Drawing.Brushes.Black, startX + (2 * (tileWidth + 2 * space)), startY + PckImage.Height - Font.Height);
				}
				else
					g.DrawImage(Globals.ExtraTiles[2].Image, startX + (2 * (tileWidth + 2 * space)), startY);

				if (!TopView.Instance.ContentVisible)
					g.FillRectangle(System.Drawing.Brushes.DarkGray, startX + (3 * (tileWidth + 2 * space)), startY, tileWidth + 1, tileHeight + 2);

				if (mapTile.Content != null)
				{
					g.DrawImage(mapTile.Content[MapViewPanel.Current].Image, startX + (3 * (tileWidth + 2 * space)), startY - mapTile.Content.Info.TileOffset);
					if (mapTile.Content.Info.HumanDoor || mapTile.Content.Info.UFODoor)
						g.DrawString("Door", Font, System.Drawing.Brushes.Black, startX + (3 * (tileWidth + 2 * space)), startY + PckImage.Height - Font.Height);
				}
				else
					g.DrawImage(Globals.ExtraTiles[4].Image, startX + (3 * (tileWidth + 2 * space)), startY);

				g.FillRectangle(Brushes["GroundColor"], new RectangleF(startX, startY + tileHeight + space + font.Height, tileWidth, 3));
				g.FillRectangle(new SolidBrush(Pens["NorthColor"].Color), new RectangleF(startX + ((tileWidth + 2 * space)), startY + tileHeight + space + font.Height, tileWidth, 3));
				g.FillRectangle(new SolidBrush(Pens["WestColor"].Color), new RectangleF(startX + (2 * (tileWidth + 2 * space)), startY + tileHeight + space + font.Height, tileWidth, 3));
				g.FillRectangle(Brushes["ContentColor"], new RectangleF(startX + (3 * (tileWidth + 2 * space)), startY + tileHeight + space + font.Height, tileWidth, 3));

				g.DrawString("Gnd", font, System.Drawing.Brushes.Black, new RectangleF(startX, startY + tileHeight + space, tileWidth, 50));
				g.DrawString("West", font, System.Drawing.Brushes.Black, new RectangleF(startX + ((tileWidth + 2 * space)), startY + tileHeight + space, tileWidth, 50));
				g.DrawString("North", font, System.Drawing.Brushes.Black, new RectangleF(startX + (2 * (tileWidth + 2 * space)), startY + tileHeight + space, tileWidth, 50));
				g.DrawString("Content", font, System.Drawing.Brushes.Black, new RectangleF(startX + (3 * (tileWidth + 2 * space)), startY + tileHeight + space, tileWidth + 50, 50));

				for (int i = 0; i < 4; i++)
					g.DrawRectangle(System.Drawing.Pens.Black, startX - 1 + (i * (tileWidth + 2 * space)), startY, tileWidth + 2, tileHeight + 2);
			}
		}
	}
}
