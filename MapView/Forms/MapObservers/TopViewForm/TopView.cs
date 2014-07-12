using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using MapView.Forms.MainWindow;
using XCom;
using XCom.Interfaces;
using System.Drawing.Drawing2D;
using Microsoft.Win32;
using XCom.Interfaces.Base;
using System.Reflection;

namespace MapView.TopViewForm
{
	public partial class TopView : Map_Observer_Form
	{
		private Dictionary<MenuItem, int> visibleHash;
		private Dictionary<string, SolidBrush> brushes;
		private Dictionary<string, Pen> pens;

		private TopViewPanel topViewPanel;

		public event EventHandler VisibleTileChanged;

		#region Singleton access
		private static TopView myInstance = null;
		public static TopView Instance
		{
			get
			{
				if (myInstance == null)
					myInstance = new TopView();

				return myInstance;
			}
		}
		#endregion

		private TopView()
		{
			//LogFile.Instance.WriteLine("Start TopView window creation");		

			InitializeComponent();
         
            var mainToolStripButtonsFactory = new MainToolStripButtonsFactory();
            mainToolStripButtonsFactory.MakeToolstrip(toolStrip);

			SuspendLayout();
			topViewPanel = new TopViewPanel();
			topViewPanel.Width = 100;
			topViewPanel.Height = 100;

			center.AutoScroll = true;
			center.Controls.Add(topViewPanel);

			center.Resize += delegate(object sender, EventArgs e)
			{
				topViewPanel.ParentSize(center.Width, center.Height);
			};

			//bottom.PanelClicked += new EventHandler(bottomClick);

			this.Menu = new MainMenu();
			MenuItem vis = Menu.MenuItems.Add("Visible");

			visibleHash = new Dictionary<MenuItem, int>();

			topViewPanel.Ground = vis.MenuItems.Add("Ground", new EventHandler(visibleClick));
			visibleHash[topViewPanel.Ground] = 0;
			topViewPanel.Ground.Shortcut = Shortcut.F1;

			topViewPanel.West = vis.MenuItems.Add("West", new EventHandler(visibleClick));
			visibleHash[topViewPanel.West] = 1;
			topViewPanel.West.Shortcut = Shortcut.F2;

			topViewPanel.North = vis.MenuItems.Add("North", new EventHandler(visibleClick));
			visibleHash[topViewPanel.North] = 2;
			topViewPanel.North.Shortcut = Shortcut.F3;

			topViewPanel.Content = vis.MenuItems.Add("Content", new EventHandler(visibleClick));
			visibleHash[topViewPanel.Content] = 3;
			topViewPanel.Content.Shortcut = Shortcut.F4;

			MenuItem edit = Menu.MenuItems.Add("Edit");
			edit.MenuItems.Add("Options", new EventHandler(options_click));
			edit.MenuItems.Add("Fill", new EventHandler(fill_click));

			//mapView.BlankChanged += new BoolDelegate(blankMode);

			//Controls.Add(bottom);

			topViewPanel.BottomPanel = bottom;

			MoreObservers.Add("BottomPanel", bottom);
			MoreObservers.Add("TopViewPanel", topViewPanel);

			ResumeLayout();
		}

		public BottomPanel BottomPanel
		{
			get { return bottom; }
		}

		private void visibleClick(object sender, EventArgs e)
		{
			((MenuItem)sender).Checked = !((MenuItem)sender).Checked;

			if (VisibleTileChanged != null)
				VisibleTileChanged(this, new EventArgs());

			MapViewPanel.Instance.Refresh();

			Refresh();
		}

		private void diamondHeight(object sender, string keyword, object val)
		{
			topViewPanel.MinHeight = (int)val;
		}

		protected override void OnRISettingsLoad(DSShared.Windows.RegistrySaveLoadEventArgs e)
		{
			bottom.Height = 74;
			RegistryKey riKey = e.OpenKey;

			foreach (MenuItem mi in visibleHash.Keys)
				mi.Checked = bool.Parse((string)riKey.GetValue("vis" + visibleHash[mi].ToString(), "true"));
		}

		protected override void OnRISettingsSave(DSShared.Windows.RegistrySaveLoadEventArgs e)
		{
			RegistryKey riKey = e.OpenKey;
			foreach (MenuItem mi in visibleHash.Keys)
				riKey.SetValue("vis" + visibleHash[mi].ToString(), mi.Checked);
		}

		protected override void LoadDefaultSettings(Settings settings)
		{
			//RegistryKey swKey = Registry.CurrentUser.CreateSubKey("Software");
			//RegistryKey mvKey = swKey.CreateSubKey("MapView");
			//RegistryKey riKey = mvKey.CreateSubKey("TopView");

			//foreach (MenuItem mi in visibleHash.Keys)
			//    mi.Checked = bool.Parse((string)riKey.GetValue("vis" + visibleHash[mi].ToString(), "true"));

			//riKey.Close();
			//mvKey.Close();
			//swKey.Close();

			brushes = new Dictionary<string, SolidBrush>();
			pens = new Dictionary<string, Pen>();

			brushes.Add("GroundColor", new SolidBrush(Color.Orange));
			brushes.Add("ContentColor", new SolidBrush(Color.Green));
			brushes.Add("SelectTileColor", bottom.SelectColor);

			Pen northPen = new Pen(new SolidBrush(Color.Red), 4);
			pens.Add("NorthColor", northPen);
			pens.Add("NorthWidth", northPen);

			Pen westPen = new Pen(new SolidBrush(Color.Red), 4);
			pens.Add("WestColor", westPen);
			pens.Add("WestWidth", westPen);

			Pen selPen = new Pen(new SolidBrush(Color.Black), 2);
			pens.Add("SelectColor", selPen);
			pens.Add("SelectWidth", selPen);

			Pen gridPen = new Pen(new SolidBrush(Color.Black), 1);
			pens.Add("GridColor", gridPen);
			pens.Add("GridWidth", gridPen);

			Pen mousePen = new Pen(new SolidBrush(Color.Blue), 2);
			pens.Add("MouseColor", mousePen);
			pens.Add("MouseWidth", mousePen);

			ValueChangedDelegate bc = new ValueChangedDelegate(brushChanged);
			ValueChangedDelegate pc = new ValueChangedDelegate(penColorChanged);
			ValueChangedDelegate pw = new ValueChangedDelegate(penWidthChanged);
			ValueChangedDelegate dh = new ValueChangedDelegate(diamondHeight);

			settings.AddSetting("GroundColor", Color.Orange, "Color of the ground tile indicator", "Tile", bc, false, null);
			settings.AddSetting("NorthColor", Color.Red, "Color of the north tile indicator", "Tile", pc, false, null);
			settings.AddSetting("WestColor", Color.Red, "Color of the west tile indicator", "Tile", pc, false, null);
			settings.AddSetting("ContentColor", Color.Green, "Color of the content tile indicator", "Tile", bc, false, null);
			settings.AddSetting("NorthWidth", 4, "Width of the north tile indicator in pixels", "Tile", pw, false, null);
			settings.AddSetting("WestWidth", 4, "Width of the west tile indicator in pixels", "Tile", pw, false, null);
			settings.AddSetting("SelectColor", Color.Black, "Color of the selection line", "Select", pc, false, null);
			settings.AddSetting("SelectWidth", 2, "Width of the selection line in pixels", "Select", pw, false, null);
			settings.AddSetting("GridColor", Color.Black, "Color of the grid lines", "Grid", pc, false, null);
			settings.AddSetting("GridWidth", 1, "Width of the grid lines", "Grid", pw, false, null);
			settings.AddSetting("MouseWidth", 2, "Width of the mouse-over indicatior", "Grid", pw, false, null);
			settings.AddSetting("MouseColor", Color.Blue, "Color of the mouse-over indicator", "Grid", pc, false, null);
			settings.AddSetting("SelectTileColor", Color.Lavender, "Background color of the selected tile piece", "Other", bc, false, null);
			settings.AddSetting("DiamondMinHeight", topViewPanel.MinHeight, "Minimum height of the grid tiles", "Tile", dh, false, null);

			topViewPanel.Brushes = brushes;
			topViewPanel.Pens = pens;

			bottom.Brushes = brushes;
			bottom.Pens = pens;
		}

		private void fill_click(object sender, EventArgs evt)
		{
            var map = MapViewPanel.Instance.View.Map;
		    if (map == null) return;
            Point s = new Point(0, 0);
			Point e = new Point(0, 0);

			s.X = Math.Min(MapViewPanel.Instance.View.StartDrag.X, MapViewPanel.Instance.View.EndDrag.X);
			s.Y = Math.Min(MapViewPanel.Instance.View.StartDrag.Y, MapViewPanel.Instance.View.EndDrag.Y);

			e.X = Math.Max(MapViewPanel.Instance.View.StartDrag.X, MapViewPanel.Instance.View.EndDrag.X);
			e.Y = Math.Max(MapViewPanel.Instance.View.StartDrag.Y, MapViewPanel.Instance.View.EndDrag.Y);

			//row   col
			//y     x
			for (int c = s.X; c <= e.X; c++)
				for (int r = s.Y; r <= e.Y; r++)
                    ((XCMapTile)map[r, c])[bottom.SelectedQuadrant] = TileView.Instance.SelectedTile;
			Globals.MapChanged = true;
			MapViewPanel.Instance.Refresh();
			Refresh();
		}

		private void options_click(object sender, EventArgs e)
		{
			PropertyForm pf = new PropertyForm("TopViewType", Settings);
			pf.Text = "TopView Options";
			pf.Show();
		}

		private void brushChanged(object sender, string key, object val)
		{
			((SolidBrush)brushes[key]).Color = (Color)val;
			if (key == "SelectTileColor")
				bottom.SelectColor = (SolidBrush)brushes[key];
			Refresh();
		}

		private void penColorChanged(object sender, string key, object val)
		{
			((Pen)pens[key]).Color = (Color)val;
			Refresh();
		}

		private void penWidthChanged(object sender, string key, object val)
		{
			((Pen)pens[key]).Width = (int)val;
			Refresh();
		}

		public bool GroundVisible
		{
			get { return topViewPanel.Ground.Checked; }
		}

		public bool NorthVisible
		{
			get { return topViewPanel.North.Checked; }
		}

		public bool WestVisible
		{
			get { return topViewPanel.West.Checked; }
		}

		public bool ContentVisible
		{
			get { return topViewPanel.Content.Checked; }
		}

		private void btnUp_Click(object sender, EventArgs e)
		{
			map.Up();
		}

		private void btnDown_Click(object sender, EventArgs e)
		{
			map.Down();
		}
	}
}


/*using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using XCom;
using XCom.Interfaces;
using System.Drawing.Drawing2D;
using Microsoft.Win32;
using XCom.Interfaces.Base;

namespace MapView.TopViewForm
{
	public partial class TopView : Map_Observer_Form
	{
		private int hWidth = 8, hHeight = 4;
		private int minHeight = 5, minWidth = 10;

		private Point origin;
		private BottomPanel bottom;

		private int offX = 0, offY = 0;
		private int selR, selC, mR, mC;
		private bool blank = false;

		private MapViewPanel mapView;

		private Dictionary<MenuItem, int> visibleHash;
		private Dictionary<string, SolidBrush> brushes;
		private Dictionary<string, Pen> pens;

		private MenuItem g, n, w, c;
		private Point sel1, sel2, sel3, sel4;

		private GraphicsPath copyArea, selected;
		private Settings settings;

		private HScrollBar horiz;
		private VScrollBar vert;
		//private Label corner;

		public event EventHandler VisibleTileChanged;

		private TopView()
		{
			LogFile.Instance.WriteLine("Start TopView window creation");
			this.bottom = new BottomPanel();

			InitializeComponent();

			bottom.SuspendLayout();
			bottom.Height = 74;
			bottom.Dock = DockStyle.Bottom;
			bottom.ResumeLayout();

			bottom.PanelClicked += new EventHandler(bottomClick);

			selR = selC = -2;

			sel1 = sel2 = sel3 = sel4 = new Point(-1, -1);

			this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.UserPaint, true);

			myInstance = this;

			this.Menu = new MainMenu();
			MenuItem vis = Menu.MenuItems.Add("Visible");

			visibleHash = new Dictionary<MenuItem, int>();

			g = vis.MenuItems.Add("Ground", new EventHandler(visibleClick));
			visibleHash[g] = 0;
			g.Shortcut = Shortcut.F1;

			w = vis.MenuItems.Add("West", new EventHandler(visibleClick));
			visibleHash[w] = 1;
			w.Shortcut = Shortcut.F2;

			n = vis.MenuItems.Add("North", new EventHandler(visibleClick));
			visibleHash[n] = 2;
			n.Shortcut = Shortcut.F3;

			c = vis.MenuItems.Add("Content", new EventHandler(visibleClick));
			visibleHash[c] = 3;
			c.Shortcut = Shortcut.F4;

			MenuItem edit = Menu.MenuItems.Add("Edit");
			edit.MenuItems.Add("Options", new EventHandler(options_click));
			edit.MenuItems.Add("Fill", new EventHandler(fill_click));

			mapView = MapViewPanel.Instance;
			//mapView.ViewClicked += new EventHandler(viewClicked);
			mapView.BlankChanged += new BoolDelegate(blankMode);
			this.Closing += new CancelEventHandler(closing);

			mapView.View.DragChanged += new EventHandler(viewDrag);

			brushes = new Dictionary<string, SolidBrush>();
			pens = new Dictionary<string, Pen>();

			vert = new VScrollBar();
			horiz = new HScrollBar();

			vert.Dock = DockStyle.Right;
			vert.Minimum = 0;
			Controls.Add(vert);

			horiz.Dock = DockStyle.Bottom;
			horiz.Minimum = 0;
			Controls.Add(horiz);

			Controls.Add(bottom);

			loadDefaults();
		}

		private static TopView myInstance = null;
		public static TopView Instance
		{
			get
			{
				if (myInstance == null)
					myInstance = new TopView();

				return myInstance;
			}
		}

		protected override void SelectedTileChanged(object sender, SelectedTileChangedEventArgs e)
		{
			bottom.Tile = (XCMapTile)e.SelectedTile;
			MapLocation pt = e.MapLocation;

			Text = "r: " + pt.Row + " c: " + pt.Col;
			selR = pt.Row;
			selC = pt.Col;

			selected = new GraphicsPath();

			int xc = (selC - selR) * hWidth;
			int yc = (selC + selR) * hHeight;

			selected.AddLine(xc, yc, xc + hWidth, yc + hHeight);
			selected.AddLine(xc + hWidth, yc + hHeight, xc, yc + 2 * hHeight);
			selected.AddLine(xc, yc + 2 * hHeight, xc - hWidth, yc + hHeight);
			selected.CloseFigure();

			Refresh();
		}

		//private void tileChangedx(IMapFile mapFile, MapLocation pt)
		//{
		//    Text = "r: " + pt.Row + " c: " + pt.Col;
		//    selR = pt.Row;
		//    selC = pt.Col;

		//    selected = new GraphicsPath();

		//    int xc = origin.X + (selC - selR) * hWidth;
		//    int yc = origin.Y + (selC + selR) * hHeight;

		//    selected.AddLine(xc, yc, xc + hWidth, yc + hHeight);
		//    selected.AddLine(xc + hWidth, yc + hHeight, xc, yc + 2 * hHeight);
		//    selected.AddLine(xc, yc + 2 * hHeight, xc - hWidth, yc + hHeight);
		//    selected.CloseFigure();

		//    Refresh();
		//}

		//public override bool AcceptsMapType(Type t)
		//{
		//    return typeof(XCMapFile).IsAssignableFrom(t);
		//}

		private void fill_click(object sender, EventArgs evt)
		{
			Point s = new Point(0, 0);
			Point e = new Point(0, 0);

			s.X = Math.Min(mapView.View.StartDrag.X, mapView.View.EndDrag.X);
			s.Y = Math.Min(mapView.View.StartDrag.Y, mapView.View.EndDrag.Y);

			e.X = Math.Max(mapView.View.StartDrag.X, mapView.View.EndDrag.X);
			e.Y = Math.Max(mapView.View.StartDrag.Y, mapView.View.EndDrag.Y);

			//row   col
			//y     x

			for (int c = s.X; c <= e.X; c++)
				for (int r = s.Y; r <= e.Y; r++)
					((XCMapTile)mapView.View.Map[r, c])[bottom.SelectedQuadrant] = TileView.Instance.SelectedTile;
			Globals.MapChanged = true;
			mapView.Refresh();
			Refresh();
		}

		private void options_click(object sender, EventArgs e)
		{
			PropertyForm pf = new PropertyForm("TopViewType", settings);
			pf.Text = "TopView Options";
			pf.Show();
		}

		private void brushChanged(object sender, string key, object val)
		{
			((SolidBrush)brushes[key]).Color = (Color)val;
			if (key == "SelectTileColor")
				bottom.SelectColor = (SolidBrush)brushes[key];
			Refresh();
		}

		private void penColorChanged(object sender, string key, object val)
		{
			((Pen)pens[key]).Color = (Color)val;
			Refresh();
		}

		private void penWidthChanged(object sender, string key, object val)
		{
			((Pen)pens[key]).Width = (int)val;
			Refresh();
		}

		public Settings Settings
		{
			get { return settings; }
		}

		protected override void HeightChanged(object sender, HeightChangedEventArgs e)
		{
			Refresh();
		}

		protected override void OnSetMap(IMap_Base map)
		{
			OnResize(null);
		}

		private void viewDrag(object sender, EventArgs ex)
		{
			Point s = new Point(0, 0);
			Point e = new Point(0, 0);

			s.X = Math.Min(mapView.View.StartDrag.X, mapView.View.EndDrag.X);
			s.Y = Math.Min(mapView.View.StartDrag.Y, mapView.View.EndDrag.Y);

			e.X = Math.Max(mapView.View.StartDrag.X, mapView.View.EndDrag.X);
			e.Y = Math.Max(mapView.View.StartDrag.Y, mapView.View.EndDrag.Y);

			//                 col hei
			sel1.X = offX + origin.X + (s.X - s.Y) * hWidth;
			sel1.Y = offY + origin.Y + (s.X + s.Y) * hHeight;

			sel2.X = offX + origin.X + (e.X - s.Y) * hWidth + hWidth;
			sel2.Y = offY + origin.Y + (e.X + s.Y) * hHeight + hHeight;

			sel3.X = offX + origin.X + (e.X - e.Y) * hWidth;
			sel3.Y = offY + origin.Y + (e.X + e.Y) * hHeight + hHeight + hHeight;

			sel4.X = offX + origin.X + (s.X - e.Y) * hWidth - hWidth;
			sel4.Y = offY + origin.Y + (s.X + e.Y) * hHeight + hHeight;

			copyArea = new GraphicsPath();
			copyArea.AddLine(sel1, sel2);
			copyArea.AddLine(sel2, sel3);
			copyArea.AddLine(sel3, sel4);
			copyArea.CloseFigure();

			Refresh();
		}

		public bool GroundVisible
		{
			get { return g.Checked; }
		}

		public bool NorthVisible
		{
			get { return n.Checked; }
		}

		public bool WestVisible
		{
			get { return w.Checked; }
		}

		public bool ContentVisible
		{
			get { return c.Checked; }
		}

		private void loadDefaults()
		{
			RegistryKey swKey = Registry.CurrentUser.CreateSubKey("Software");
			RegistryKey mvKey = swKey.CreateSubKey("MapView");
			RegistryKey riKey = mvKey.CreateSubKey("TopView");

			foreach (MenuItem mi in visibleHash.Keys)
				mi.Checked = bool.Parse((string)riKey.GetValue("vis" + visibleHash[mi].ToString(), "true"));

			riKey.Close();
			mvKey.Close();
			swKey.Close();

			brushes["GroundColor"] = new SolidBrush(Color.Orange);
			brushes["ContentColor"] = new SolidBrush(Color.Green);
			brushes["SelectTileColor"] = bottom.SelectColor;

			Pen northPen = new Pen(new SolidBrush(Color.Red), 4);
			pens["NorthColor"] = northPen;
			pens["NorthWidth"] = northPen;

			Pen westPen = new Pen(new SolidBrush(Color.Red), 4);
			pens["WestColor"] = westPen;
			pens["WestWidth"] = westPen;

			Pen selPen = new Pen(new SolidBrush(Color.Black), 2);
			pens["SelectColor"] = selPen;
			pens["SelectWidth"] = selPen;

			Pen gridPen = new Pen(new SolidBrush(Color.Black), 1);
			pens["GridColor"] = gridPen;
			pens["GridWidth"] = gridPen;

			Pen mousePen = new Pen(new SolidBrush(Color.Blue), 2);
			pens["MouseColor"] = mousePen;
			pens["MouseWidth"] = mousePen;

			settings = new Settings();

			ValueChangedDelegate bc = new ValueChangedDelegate(brushChanged);
			ValueChangedDelegate pc = new ValueChangedDelegate(penColorChanged);
			ValueChangedDelegate pw = new ValueChangedDelegate(penWidthChanged);
			ValueChangedDelegate dh = new ValueChangedDelegate(diamondHeight);

			settings.AddSetting("GroundColor", Color.Orange, "Color of the ground tile indicator", "Tile", bc, false, null);
			settings.AddSetting("NorthColor", Color.Red, "Color of the north tile indicator", "Tile", pc, false, null);
			settings.AddSetting("WestColor", Color.Red, "Color of the west tile indicator", "Tile", pc, false, null);
			settings.AddSetting("ContentColor", Color.Green, "Color of the content tile indicator", "Tile", bc, false, null);
			settings.AddSetting("NorthWidth", 4, "Width of the north tile indicator in pixels", "Tile", pw, false, null);
			settings.AddSetting("WestWidth", 4, "Width of the west tile indicator in pixels", "Tile", pw, false, null);
			settings.AddSetting("SelectColor", Color.Black, "Color of the selection line", "Select", pc, false, null);
			settings.AddSetting("SelectWidth", 2, "Width of the selection line in pixels", "Select", pw, false, null);
			settings.AddSetting("GridColor", Color.Black, "Color of the grid lines", "Grid", pc, false, null);
			settings.AddSetting("GridWidth", 1, "Width of the grid lines", "Grid", pw, false, null);
			settings.AddSetting("MouseWidth", 2, "Width of the mouse-over indicatior", "Grid", pw, false, null);
			settings.AddSetting("MouseColor", Color.Blue, "Color of the mouse-over indicator", "Grid", pc, false, null);
			settings.AddSetting("SelectTileColor", Color.Lavender, "Background color of the selected tile piece", "Other", bc, false, null);
			settings.AddSetting("DiamondMinHeight", minHeight, "Minimum height of the grid tiles", "Tile", dh, false, null);
		}

		private void diamondHeight(object sender, string keyword, object val)
		{
			minHeight = (int)val;
			minWidth = 2 * minHeight;

			if (hWidth < minWidth)
			{
				hWidth = minWidth;
				hHeight = minHeight;
				OnResize(null);
			}
		}

		private void visibleClick(object sender, EventArgs e)
		{
			((MenuItem)sender).Checked = !((MenuItem)sender).Checked;

			if (VisibleTileChanged != null)
				VisibleTileChanged(this, new EventArgs());

			MapViewPanel.Instance.Refresh();

			Refresh();
		}

		private void closing(object sender, CancelEventArgs e)
		{
			RegistryKey swKey = Registry.CurrentUser.CreateSubKey("Software");
			RegistryKey mvKey = swKey.CreateSubKey("MapView");
			RegistryKey riKey = mvKey.CreateSubKey("TopView");

			foreach (MenuItem mi in visibleHash.Keys)
				riKey.SetValue("vis" + visibleHash[mi].ToString(), mi.Checked);

			riKey.Close();
			mvKey.Close();
			swKey.Close();
		}

		//private void viewClicked(object sender, EventArgs e)
		//{
		//    MapLocation c = mapView.Map.SelectedTile;
		//    bottom.Tile = (XCMapTile)mapView.Map[c.Row, c.Col, mapView.Map.CurrentHeight];
		//}

		public BottomPanel BottomPanel
		{
			get { return bottom; }
		}

		private void blankMode(bool val)
		{
			blank = val;
			Refresh();
		}

		private void bottomClick(object sender, EventArgs e)
		{
			Refresh();
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			int vWid = vert.Left;
			int vHei = horiz.Top;

			if (map != null)
			{
				if (vHei > vWid / 2)
				{
					//use width
					hWidth = vWid / (map.MapSize.Rows + map.MapSize.Cols + 1);

					if (hWidth % 2 != 0)
						hWidth--;

					hHeight = hWidth / 2;
				}
				else
				{ //use height
					hHeight = vHei / (map.MapSize.Rows + map.MapSize.Cols);
					hWidth = hHeight * 2;
				}

				if (hWidth < minWidth)
				{
					hWidth = minWidth;
					hHeight = minHeight;
				}

				//vert.Value = vert.Minimum;
				//horiz.Value = horiz.Minimum;
				//vert_Scroll(null, null);
				//horiz_Scroll(null, null);

				//if (vert.Visible = (ClientSize.Height - bottom.Height < hHeight * (map.MapSize.Rows + map.MapSize.Cols)))
				//{
				//    vert.Maximum = hHeight * (map.MapSize.Rows + map.MapSize.Cols) - (ClientSize.Height - bottom.Height - corner.Height);
				//    horiz.Width = ClientSize.Width - corner.Width;
				//}
				//else
				//{
				//    //corner.Visible = false;
				//    horiz.Width = ClientSize.Width;
				//}

				//if (horiz.Visible = (ClientSize.Width < hWidth * (map.MapSize.Rows + map.MapSize.Cols + 1)))
				//{
				//    horiz.Maximum = Math.Max(hWidth * (map.MapSize.Rows + map.MapSize.Cols + 1) - ClientSize.Width, horiz.Minimum);
				//    vert.Height = ClientSize.Height - bottom.Height + 3;
				//}
				//else
				//{
				//    //corner.Visible = false;
				//    vert.Height = ClientSize.Height - bottom.Height + corner.Height + 3;
				//}

				//if (vert.Visible && horiz.Visible)
				//    corner.Visible = true;

				origin = new Point((map.MapSize.Rows) * hWidth, 0);
				viewDrag(null, null);
				Refresh();
			}
		}

		//private void horiz_Scroll(object sender, ScrollEventArgs e)
		//{
		//    offX = -horiz.Value;
		//    viewDrag(null, null);
		//    Refresh();
		//}

		//private void vert_Scroll(object sender, ScrollEventArgs e)
		//{
		//    offY = -vert.Value;
		//    viewDrag(null, null);
		//    Refresh();
		//}

		//based on hWidth
		private int mapWidth()
		{
			if (map != null)
				return (map.MapSize.Rows + map.MapSize.Cols) * hWidth;
			return 0;
		}

		private int mapHeight()
		{
			if (map != null)
				return (map.MapSize.Rows + map.MapSize.Cols) * hHeight;
			return 0;
		}

		private void heightChanged(int x)
		{
			Refresh();
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			Point p = convertCoordsDiamond(e.X - offX, e.Y - offY);
			map.SelectedTile = new MapLocation(p.Y, p.X, map.CurrentHeight);
			mDown = true;

			MapViewPanel.Instance.View.StartDrag = p;
			MapViewPanel.Instance.View.EndDrag = p;

			if (e.Button == MouseButtons.Right)
			{
				bottom.SetSelected(e.Button, 1);
				Refresh();
			}
			else if (e.Button == MouseButtons.Left)
			{
				MapViewPanel.Instance.View.Refresh();
				viewDrag(null, null);
			}
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
			Point p = convertCoordsDiamond(e.X - offX, e.Y - offY);
			if (p.Y != mR || p.X != mC)
			{
				mR = p.Y;
				mC = p.X;

				if (mDown)
				{
					mapView.View.EndDrag = p;

					mapView.View.Refresh();
					viewDrag(null, null);
				}
				Refresh();
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if (map != null)
			{
				Graphics g = e.Graphics;
				GraphicsPath upper = new GraphicsPath();
				GraphicsPath lower = new GraphicsPath();

				for (int row = 0, startX = origin.X + offX, startY = origin.Y + offY; row < map.MapSize.Rows; row++, startX -= hWidth, startY += hHeight)
				{
					for (int col = 0, x = startX, y = startY; col < map.MapSize.Cols; col++, x += hWidth, y += hHeight)
					{
						if (map[row, col] != null)
						{
							upper.Reset();
							upper.AddLine(x, y, x + hWidth, y + hHeight);
							upper.AddLine(x + hWidth, y + hHeight, x - hWidth, y + hHeight);
							upper.CloseFigure();

							lower.Reset();
							lower.AddLine(x, y + 2 * hHeight, x + hWidth, y + hHeight);
							lower.AddLine(x + hWidth, y + hHeight, x - hWidth, y + hHeight);
							lower.CloseFigure();

							XCMapTile mapTile = map[row, col] as XCMapTile;

							if (!blank)
							{
								if (mapTile.Ground != null && this.g.Checked)
									g.FillPath(brushes["GroundColor"], upper);

								if (mapTile.North != null && n.Checked)
									g.DrawLine(pens["NorthColor"], x, y, x + hWidth, y + hHeight);

								if (mapTile.West != null && w.Checked)
									g.DrawLine(pens["WestColor"], x, y, x - hWidth, y + hHeight);

								if (mapTile.Content != null && c.Checked)
									g.FillPath(brushes["ContentColor"], lower);
							}
							else
							{
								if (!mapTile.DrawAbove)
								{
									g.FillPath(Brushes.DarkGray, upper);
									g.FillPath(Brushes.DarkGray, lower);
								}
							}
						}
					}
				}

				for (int i = 0; i <= map.MapSize.Rows; i++)
					g.DrawLine(pens["GridColor"], origin.X - i * hWidth + offX, origin.Y + i * hHeight + offY, origin.X + ((map.MapSize.Cols - i) * hWidth) + offX, origin.Y + ((i + map.MapSize.Cols) * hHeight) + offY);
				for (int i = 0; i <= map.MapSize.Cols; i++)
					g.DrawLine(pens["GridColor"], origin.X + i * hWidth + offX, origin.Y + i * hHeight + offY, (origin.X + i * hWidth) - map.MapSize.Rows * hWidth + offX, (origin.Y + i * hHeight) + map.MapSize.Rows * hHeight + offY);

				if (copyArea != null)
					g.DrawPath(pens["SelectColor"], copyArea);

				//				if(selected!=null) //clicked on
				//					g.DrawPath(new Pen(Brushes.Blue,2),selected);

				if (mR < map.MapSize.Rows && mC < map.MapSize.Cols && mR >= 0 && mC >= 0)
				{
					int xc = origin.X + (mC - mR) * hWidth + offX;
					int yc = origin.Y + (mC + mR) * hHeight + offY;

					GraphicsPath selPath = new GraphicsPath();
					selPath.AddLine(xc, yc, xc + hWidth, yc + hHeight);
					selPath.AddLine(xc + hWidth, yc + hHeight, xc, yc + 2 * hHeight);
					selPath.AddLine(xc, yc + 2 * hHeight, xc - hWidth, yc + hHeight);
					selPath.CloseFigure();
					g.DrawPath(pens["MouseColor"], selPath);
				}
			}
		}

		private Point convertCoordsDiamond(int xp, int yp)
		{
			int x = xp - origin.X; //16 is half the width of the diamond
			int y = yp - origin.Y; //24 is the distance from the top of the diamond to the very top of the image

			double x1 = (x * 1.0 / (2 * hWidth)) + (y * 1.0 / (2 * hHeight));
			double x2 = -(x * 1.0 - 2 * y * 1.0) / (2 * hWidth);

			return new Point((int)Math.Floor(x1), (int)Math.Floor(x2));
		}
	}
}
*/