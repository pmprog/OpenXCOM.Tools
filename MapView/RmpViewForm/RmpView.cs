using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using XCom;
using XCom.Interfaces;
using System.Drawing.Drawing2D;
using XCom.Interfaces.Base;

namespace MapView.RmpViewForm
{

	/*
		TFTD ---- UFO
		Commander ---- Commander
		Navigator ---- Leader
		Medic ---- Engineer
		Technition ---- Medic
		SquadLeader ---- Navigator 
		Soldier ---- Soldier
	*/

	//public delegate void RmpClick(int row,int col,MouseButtons button);

	public partial class RmpView : Map_Observer_Form
	{
		private XCMapFile map;
		private RmpPanel rmpPanel;
		private Label locInfo;
		private Label links;
		private int clickRow, clickCol;

		private RmpEntry currEntry;
		//private Settings settings;
		private Panel contentPane;

		private static RmpView instance;

		private RmpView()
		{
			clickRow = clickCol = 0;
			InitializeComponent();

			rmpPanel = new RmpPanel();
			contentPane.Controls.Add(rmpPanel);
			rmpPanel.MapPanelClicked += new MapPanelClickDelegate(panelClick);
			rmpPanel.MouseMove += new MouseEventHandler(mouseMove);
			rmpPanel.Dock = DockStyle.Fill;

			locInfo = new Label();
			links = new Label();

			locInfo.Width = 100;
			locInfo.Height = ClientSize.Height - rmpPanel.Height;

			links.Height = locInfo.Height;
			links.Width = ClientSize.Width - locInfo.Width;

			locInfo.Top = rmpPanel.Bottom;
			locInfo.Left = 0;

			links.Top = locInfo.Top;
			links.Left = locInfo.Right;

			links.BorderStyle = BorderStyle.Fixed3D;

			//this.Menu = new MainMenu();
			//MenuItem f = new MenuItem("Edit");
			//Menu.MenuItems.Add(f);
			//f.MenuItems.Add("Options",new EventHandler(options_click));

			object[] uTypeItms = new object[] { UnitType.Any, UnitType.Flying, UnitType.FlyingLarge, UnitType.Large, UnitType.Small };
			cbType.Items.AddRange(uTypeItms);

			cbUse1.Items.AddRange(uTypeItms);
			cbUse2.Items.AddRange(uTypeItms);
			cbUse3.Items.AddRange(uTypeItms);
			cbUse4.Items.AddRange(uTypeItms);
			cbUse5.Items.AddRange(uTypeItms);

			cbType.DropDownStyle = ComboBoxStyle.DropDownList;

			cbUse1.DropDownStyle = ComboBoxStyle.DropDownList;
			cbUse2.DropDownStyle = ComboBoxStyle.DropDownList;
			cbUse3.DropDownStyle = ComboBoxStyle.DropDownList;
			cbUse4.DropDownStyle = ComboBoxStyle.DropDownList;
			cbUse5.DropDownStyle = ComboBoxStyle.DropDownList;

			//object[] itms = {UnitRank.Civilian0,UnitRank.XCom1,UnitRank.Soldier2,UnitRank.Navigator3,UnitRank.LeaderCommander4,UnitRank.Engineer5,UnitRank.Misc1,UnitRank.Medic7,UnitRank.Misc2};
			object[] itms2 = { UnitRankNum.Zero, UnitRankNum.One, UnitRankNum.Two, UnitRankNum.Three, UnitRankNum.Four, UnitRankNum.Five, UnitRankNum.Six, UnitRankNum.Seven, UnitRankNum.Eight };

			cbRank1.Items.AddRange(RmpFile.UnitRankUFO);
			cbRank1.DropDownStyle = ComboBoxStyle.DropDownList;

			cbRank2.Items.AddRange(itms2);
			cbRank2.DropDownStyle = ComboBoxStyle.DropDownList;

			cbLink1.DropDownStyle = ComboBoxStyle.DropDownList;
			cbLink2.DropDownStyle = ComboBoxStyle.DropDownList;
			cbLink3.DropDownStyle = ComboBoxStyle.DropDownList;
			cbLink4.DropDownStyle = ComboBoxStyle.DropDownList;
			cbLink5.DropDownStyle = ComboBoxStyle.DropDownList;

			cbUsage.DropDownStyle = ComboBoxStyle.DropDownList;
			cbUsage.Items.AddRange(RmpFile.SpawnUsage);

			currEntry = null;

			Text = "RmpView";
		}

		private void options_click(object sender, EventArgs e)
		{
			PropertyForm pf = new PropertyForm("rmpViewOptions", Settings);
			pf.Text = "RmpView Settings";
			pf.Show();
		}

		private void brushChanged(object sender, string key, object val)
		{
			rmpPanel.Brushes[key].Color = (Color)val;
			Refresh();
		}

		private void penColorChanged(object sender, string key, object val)
		{
			rmpPanel.Pens[key].Color = (Color)val;
			Refresh();
		}

		private void penWidthChanged(object sender, string key, object val)
		{
			rmpPanel.Pens[key].Width = (int)val;
			Refresh();
		}

		//public Settings Settings
		//{
		//    get { return settings; }
		//}

		public static RmpView Instance
		{
			get
			{
				if (instance == null)
					instance = new RmpView();
				return instance;
			}
		}

		private void mouseMove(object sender, MouseEventArgs e)
		{
			XCMapTile t = rmpPanel.GetTile(e.X, e.Y);
			if (t != null && t.Rmp != null)
				lblMouseOver.Text = "Mouse Over: " + t.Rmp.Index;
			else
				lblMouseOver.Text = "";
		}

		private void panelClick(object sender, MapPanelClickEventArgs e)
		{
			idxLabel.Text = this.Text;
			try
			{
				currEntry = ((XCMapTile)e.ClickTile).Rmp;

				if (currEntry == null && e.MouseEventArgs.Button == MouseButtons.Right)
				{
					if (map is XCMapFile)
						currEntry = ((XCMapFile)map).AddRmp(e.ClickLocation);
				}
			}
			catch { return; }

			fillGUI();
		}

		private List<object> byteList = new List<object>();
		private object[] itms2 = { LinkTypes.ExitEast, LinkTypes.ExitNorth, LinkTypes.ExitSouth, LinkTypes.ExitWest, LinkTypes.NotUsed };

		private void fillGUI()
		{
			if (currEntry == null)
			{
				gbNodeInfo.Enabled = false;
				return;
			}
			gbNodeInfo.Enabled = true;
			gbNodeInfo.SuspendLayout();

			byteList.Clear();

			cbLink1.Items.Clear();
			cbLink2.Items.Clear();
			cbLink3.Items.Clear();
			cbLink4.Items.Clear();
			cbLink5.Items.Clear();

			for (byte i = 0; i < ((XCMapFile)map).Rmp.Length; i++)
			{
				if (i == currEntry.Index)
					continue;

				byteList.Add(i);
			}

			byteList.AddRange(itms2);

			object[] bArr = byteList.ToArray();

			cbLink1.Items.AddRange(bArr);
			cbLink2.Items.AddRange(bArr);
			cbLink3.Items.AddRange(bArr);
			cbLink4.Items.AddRange(bArr);
			cbLink5.Items.AddRange(bArr);

			cbType.SelectedItem = currEntry.UType;

			if (map.Tiles[0][0].Palette == Palette.UFOBattle)
				cbRank1.SelectedItem = RmpFile.UnitRankUFO[(byte)currEntry.URank1];
			else
				cbRank1.SelectedItem = RmpFile.UnitRankTFTD[(byte)currEntry.URank1];


			cbRank2.SelectedItem = currEntry.URank2;
			tbZero.Text = currEntry.Zero1 + "";
			cbUsage.SelectedItem = RmpFile.SpawnUsage[(byte)currEntry.Usage];

			idxLabel2.Text = "Index: " + currEntry.Index;

			if (currEntry[0].Index < 0xFB)
				cbLink1.SelectedItem = currEntry[0].Index;
			else
				cbLink1.SelectedItem = (LinkTypes)currEntry[0].Index;

			if (currEntry[1].Index < 0xFB)
				cbLink2.SelectedItem = currEntry[1].Index;
			else
				cbLink2.SelectedItem = (LinkTypes)currEntry[1].Index;

			if (currEntry[2].Index < 0xFB)
				cbLink3.SelectedItem = currEntry[2].Index;
			else
				cbLink3.SelectedItem = (LinkTypes)currEntry[2].Index;

			if (currEntry[3].Index < 0xFB)
				cbLink4.SelectedItem = currEntry[3].Index;
			else
				cbLink4.SelectedItem = (LinkTypes)currEntry[3].Index;

			if (currEntry[4].Index < 0xFB)
				cbLink5.SelectedItem = currEntry[4].Index;
			else
				cbLink5.SelectedItem = (LinkTypes)currEntry[4].Index;

			cbUse1.SelectedItem = currEntry[0].UType;
			cbUse2.SelectedItem = currEntry[1].UType;
			cbUse3.SelectedItem = currEntry[2].UType;
			cbUse4.SelectedItem = currEntry[3].UType;
			cbUse5.SelectedItem = currEntry[4].UType;

			txtDist1.Text = currEntry[0].Distance + "";
			txtDist2.Text = currEntry[1].Distance + "";
			txtDist3.Text = currEntry[2].Distance + "";
			txtDist4.Text = currEntry[3].Distance + "";
			txtDist5.Text = currEntry[4].Distance + "";

			gbNodeInfo.ResumeLayout();
		}

		public void SetMap(object sender, SetMapEventArgs e)
		{
			Map = (XCMapFile)e.Map;
		}

		public override IMap_Base Map
		{
			set
			{
				base.Map = value;
				this.map = (XCMapFile)value;

				rmpPanel.Map = map;
				if (rmpPanel.Map != null)
				{
					currEntry = ((XCMapTile)map[clickRow, clickCol]).Rmp;
					fillGUI();
					cbRank1.Items.Clear();

					if (map.Tiles[0][0].Palette == Palette.UFOBattle)
						cbRank1.Items.AddRange(RmpFile.UnitRankUFO);
					else
						cbRank1.Items.AddRange(RmpFile.UnitRankTFTD);

					//Text = string.Format("RmpView: r:{0} c:{1} h:{2}", rmpPanel.Map.MapSize.Rows, rmpPanel.Map.MapSize.Cols, rmpPanel.Map.MapSize.Height);
					//rmpPanel.Map.HeightChanged += new HeightChangedDelegate(heightChanged);
					//rmpPanel.Map.SelectedTileChanged += new SelectedTileChangedDelegate(tileChanged);
				}
			}
		}

		/*public IMapFile Map
		{
			set
			{
				if (map != null)
				{
					map.HeightChanged -= new HeightChangedDelegate(heightChanged);
					map.SelectedTileChanged -= new SelectedTileChangedDelegate(tileChanged);
				}

				if (value is XCMapFile)
					map = (XCMapFile)value;
				else
					return;

				rmpPanel.Map = map;
				if (rmpPanel.Map != null)
				{
					currEntry = ((XCMapTile)map[clickRow, clickCol]).Rmp;
					fillGUI();
					cbRank1.Items.Clear();

					if (map.Tiles[0][0].Palette == Palette.UFOBattle)
						cbRank1.Items.AddRange(RmpFile.UnitRankUFO);
					else
						cbRank1.Items.AddRange(RmpFile.UnitRankTFTD);

					Text = string.Format("RmpView: r:{0} c:{1} h:{2}", rmpPanel.Map.MapSize.Rows, rmpPanel.Map.MapSize.Cols, rmpPanel.Map.MapSize.Height);
					rmpPanel.Map.HeightChanged += new HeightChangedDelegate(heightChanged);
					rmpPanel.Map.SelectedTileChanged += new SelectedTileChangedDelegate(tileChanged);
				}
				OnResize(null);
			}
		}*/

		public override void SelectedTileChanged(IMap_Base sender, SelectedTileChangedEventArgs e)
		{
			this.Text = string.Format("RmpView: r:{0} c:{1} ", e.MapLocation.Row, e.MapLocation.Col);
		}

		public override void HeightChanged(IMap_Base sender, HeightChangedEventArgs e)
		{
			currEntry = ((XCMapTile)map[clickRow, clickCol]).Rmp;
			fillGUI();
			Refresh();
		}

		private void cbType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			currEntry.UType = (UnitType)cbType.SelectedItem;
		}

		private void cbRank1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			currEntry.URank1 = (byte)((StrEnum)cbRank1.SelectedItem).Enum;
		}

		private void cbRank2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			currEntry.URank2 = (UnitRankNum)cbRank2.SelectedItem;
		}

		private void tbZero_Leave(object sender, System.EventArgs e)
		{
			try
			{
				currEntry.Zero1 = byte.Parse(tbZero.Text);
			}
			catch
			{
				tbZero.Text = currEntry.Zero1 + "";
			}
		}

		private void tbZero_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Enter:
					try
					{
						currEntry.Zero1 = byte.Parse(tbZero.Text);
					}
					catch
					{
						tbZero.Text = currEntry.Zero1 + "";
					}
					break;
			}
		}

		private void calcLinkDistance(RmpEntry from, RmpEntry to, TextBox result)
		{
			result.Text = ((int)Math.Sqrt(Math.Pow(from.Row - to.Row, 2) + Math.Pow(from.Col - to.Col, 2) + Math.Pow(from.Height - to.Height, 2))).ToString();
		}

		private void cbLink1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				currEntry[0].Index = (byte)cbLink1.SelectedItem;
				if (currEntry[0].Index < 0xFB)
					calcLinkDistance(currEntry, map.Rmp[currEntry[0].Index], txtDist1);
			}
			catch
			{
				currEntry[0].Index = (byte)(LinkTypes)cbLink1.SelectedItem;
				if (currEntry[0].Index < 0xFB)
					calcLinkDistance(currEntry, map.Rmp[currEntry[0].Index], txtDist1);
			}
			Refresh();
		}

		private void cbLink2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				currEntry[1].Index = (byte)cbLink2.SelectedItem;
				if (currEntry[1].Index < 0xFB)
					calcLinkDistance(currEntry, map.Rmp[currEntry[1].Index], txtDist2);
			}
			catch
			{
				currEntry[1].Index = (byte)(LinkTypes)cbLink2.SelectedItem;
				if (currEntry[1].Index < 0xFB)
					calcLinkDistance(currEntry, map.Rmp[currEntry[1].Index], txtDist2);
			}
			Refresh();
		}

		private void cbLink3_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				currEntry[2].Index = (byte)cbLink3.SelectedItem;
				if (currEntry[2].Index < 0xFB)
					calcLinkDistance(currEntry, map.Rmp[currEntry[2].Index], txtDist3);
			}
			catch
			{
				currEntry[2].Index = (byte)(LinkTypes)cbLink3.SelectedItem;
				if (currEntry[2].Index < 0xFB)
					calcLinkDistance(currEntry, map.Rmp[currEntry[2].Index], txtDist3);
			}
			Refresh();
		}

		private void cbLink4_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				currEntry[3].Index = (byte)cbLink4.SelectedItem;
				if (currEntry[3].Index < 0xFB)
					calcLinkDistance(currEntry, map.Rmp[currEntry[3].Index], txtDist4);
			}
			catch
			{
				currEntry[3].Index = (byte)(LinkTypes)cbLink4.SelectedItem;
				if (currEntry[3].Index < 0xFB)
					calcLinkDistance(currEntry, map.Rmp[currEntry[3].Index], txtDist4);
			}
			Refresh();
		}

		private void cbLink5_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				currEntry[4].Index = (byte)cbLink5.SelectedItem;
				if (currEntry[4].Index < 0xFB)
					calcLinkDistance(currEntry, map.Rmp[currEntry[4].Index], txtDist5);
			}
			catch
			{
				currEntry[4].Index = (byte)(LinkTypes)cbLink5.SelectedItem;
				if (currEntry[4].Index < 0xFB)
					calcLinkDistance(currEntry, map.Rmp[currEntry[4].Index], txtDist5);
			}
			Refresh();
		}

		private void btnRemove_Click(object sender, System.EventArgs e)
		{
			map.Rmp.RemoveEntry(currEntry);
			((XCMapTile)map[currEntry.Row, currEntry.Col, currEntry.Height]).Rmp = null;
			currEntry = null;
			gbNodeInfo.Enabled = false;
			Refresh();
		}

		private void cbUse1_SelectedIndexChanged(object sender, System.EventArgs e)
		{

			currEntry[0].UType = (UnitType)cbUse1.SelectedItem;
			Refresh();
		}

		private void cbUse2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			currEntry[1].UType = (UnitType)cbUse2.SelectedItem;
			Refresh();
		}

		private void cbUse3_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			currEntry[2].UType = (UnitType)cbUse3.SelectedItem;
			Refresh();
		}

		private void cbUse4_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			currEntry[3].UType = (UnitType)cbUse4.SelectedItem;
			Refresh();
		}

		private void cbUse5_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			currEntry[4].UType = (UnitType)cbUse5.SelectedItem;
			Refresh();
		}

		private void txtDist1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Enter:
					try
					{
						currEntry[0].Distance = byte.Parse(txtDist1.Text);
					}
					catch
					{
						txtDist1.Text = currEntry[0].Distance + "";
					}
					break;
			}
		}

		private void txtDist1_Leave(object sender, System.EventArgs e)
		{
			try
			{
				currEntry[0].Distance = byte.Parse(txtDist1.Text);
			}
			catch
			{
				txtDist1.Text = currEntry[0].Distance + "";
			}
		}

		private void txtDist2_Leave(object sender, System.EventArgs e)
		{
			try
			{
				currEntry[1].Distance = byte.Parse(txtDist2.Text);
			}
			catch
			{
				txtDist2.Text = currEntry[1].Distance + "";
			}
		}

		private void txtDist2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Enter:
					try
					{
						currEntry[1].Distance = byte.Parse(txtDist2.Text);
					}
					catch
					{
						txtDist2.Text = currEntry[1].Distance + "";
					}
					break;
			}
		}

		private void txtDist3_Leave(object sender, System.EventArgs e)
		{
			try
			{
				currEntry[2].Distance = byte.Parse(txtDist3.Text);
			}
			catch
			{
				txtDist3.Text = currEntry[2].Distance + "";
			}
		}

		private void txtDist3_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Enter:
					try
					{
						currEntry[2].Distance = byte.Parse(txtDist3.Text);
					}
					catch
					{
						txtDist3.Text = currEntry[2].Distance + "";
					}
					break;
			}
		}

		private void txtDist4_Leave(object sender, System.EventArgs e)
		{
			try
			{
				currEntry[3].Distance = byte.Parse(txtDist4.Text);
			}
			catch
			{
				txtDist4.Text = currEntry[3].Distance + "";
			}
		}

		private void txtDist4_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Enter:
					try
					{
						currEntry[3].Distance = byte.Parse(txtDist4.Text);
					}
					catch
					{
						txtDist4.Text = currEntry[3].Distance + "";
					}
					break;
			}
		}

		private void txtDist5_Leave(object sender, System.EventArgs e)
		{
			try
			{
				currEntry[4].Distance = byte.Parse(txtDist5.Text);
			}
			catch
			{
				txtDist5.Text = currEntry[4].Distance + "";
			}
		}

		private void txtDist5_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Enter:
					try
					{
						currEntry[4].Distance = byte.Parse(txtDist5.Text);
					}
					catch
					{
						txtDist5.Text = currEntry[4].Distance + "";
					}
					break;
			}
		}

		private void cbUsage_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			currEntry.Usage = (SpawnUsage)((StrEnum)cbUsage.SelectedItem).Enum;
			Refresh();
		}

		//private void loadDefaults()
		protected override void LoadDefaultSettings(Settings settings)
		{
			Dictionary<string, SolidBrush> brushes = rmpPanel.Brushes;
			Dictionary<string, Pen> pens = rmpPanel.Pens;

			ValueChangedDelegate bc = new ValueChangedDelegate(brushChanged);
			ValueChangedDelegate pc = new ValueChangedDelegate(penColorChanged);
			ValueChangedDelegate pw = new ValueChangedDelegate(penWidthChanged);

			Pen redPen = new Pen(new SolidBrush(Color.Red), 2);
			pens["UnselectedLinkColor"] = redPen;
			pens["UnselectedLinkWidth"] = redPen;
			settings.AddSetting("UnselectedLinkColor", redPen.Color, "Color of unselected link lines", "Links", pc, false, null);
			settings.AddSetting("UnselectedLinkWidth", 2, "Width of unselected link lines", "Links", pw, false, null);

			Pen bluePen = new Pen(new SolidBrush(Color.Blue), 2);
			pens["SelectedLinkColor"] = bluePen;
			pens["SelectedLinkWidth"] = bluePen;
			settings.AddSetting("SelectedLinkColor", bluePen.Color, "Color of selected link lines", "Links", pc, false, null);
			settings.AddSetting("SelectedLinkWidth", 2, "Width of selected link lines", "Links", pw, false, null);

			Pen wallPen = new Pen(new SolidBrush(Color.Black), 4);
			pens["WallColor"] = wallPen;
			pens["WallWidth"] = wallPen;
			settings.AddSetting("WallColor", wallPen.Color, "Color of wall indicators", "View", pc, false, null);
			settings.AddSetting("WallWidth", 4, "Width of wall indicators", "View", pw, false, null);

			Pen gridPen = new Pen(new SolidBrush(Color.Black), 1);
			pens["GridLineColor"] = gridPen;
			pens["GridLineWidth"] = gridPen;
			settings.AddSetting("GridLineColor", gridPen.Color, "Color of grid lines", "View", pc, false, null);
			settings.AddSetting("GridLineWidth", 1, "Width of grid lines", "View", pw, false, null);

			SolidBrush selBrush = new SolidBrush(Color.Blue);
			brushes["SelectedNodeColor"] = selBrush;
			settings.AddSetting("SelectedNodeColor", selBrush.Color, "Color of selected nodes", "Nodes", bc, false, null);

			SolidBrush spawnBrush = new SolidBrush(Color.GreenYellow);
			brushes["SpawnNodeColor"] = spawnBrush;
			settings.AddSetting("SpawnNodeColor", spawnBrush.Color, "Color of spawn nodes", "Nodes", bc, false, null);

			SolidBrush nodeBrush = new SolidBrush(Color.Green);
			brushes["UnselectedNodeColor"] = nodeBrush;
			settings.AddSetting("UnselectedNodeColor", nodeBrush.Color, "Color of unselected nodes", "Nodes", bc, false, null);

			SolidBrush contentBrush = new SolidBrush(Color.DarkGray);
			brushes["ContentTiles"] = contentBrush;
			settings.AddSetting("ContentTiles", contentBrush.Color, "Color of map tiles with a content tile", "Other", bc, false, null);
		}
	}

	public class RmpPanel : MapPanel
	{
		//private static Dictionary<string,Pen> pens;
		//private static Dictionary<string, SolidBrush> brushes;

		private Font myFont = new Font("Arial", 12, FontStyle.Bold);

		//public event RmpClick RmpSquareClicked;

		public RmpPanel() { }

		public Dictionary<string, SolidBrush> Brushes
		{
			get { return brushes; }
			set { brushes = value; }
		}

		public Dictionary<string, Pen> Pens
		{
			get { return pens; }
			set { pens = value; }
		}

		public void Calc()
		{
			OnResize(null);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if (map != null)
			{
				Graphics g = e.Graphics;
				GraphicsPath lower = new GraphicsPath();
				GraphicsPath upper = new GraphicsPath();

				for (int row = 0, startX = origin.X, startY = origin.Y; row < map.MapSize.Rows; row++, startX -= hWidth, startY += hHeight)
				{
					for (int col = 0, x = startX, y = startY; col < map.MapSize.Cols; col++, x += hWidth, y += hHeight)
					{
						if (map[row, col] != null)
						{
							lower.Reset();
							lower.AddLine(x, y + 2 * hHeight, x + hWidth, y + hHeight);
							lower.AddLine(x + hWidth, y + hHeight, x - hWidth, y + hHeight);
							lower.CloseFigure();
							XCMapTile tile = (XCMapTile)map[row, col];

							if (tile.North != null)
								g.DrawLine(pens["WallColor"], x, y, x + hWidth, y + hHeight);

							if (tile.West != null)
								g.DrawLine(pens["WallColor"], x, y, x - hWidth, y + hHeight);

							if (tile.Content != null)
								g.FillPath(brushes["ContentTiles"], lower);
						}
					}
				}

				for (int row = 0, startX = origin.X, startY = origin.Y; row < map.MapSize.Rows; row++, startX -= hWidth, startY += hHeight)
				{
					for (int col = 0, x = startX, y = startY; col < map.MapSize.Cols; col++, x += hWidth, y += hHeight)
					{
						if (map[row, col] != null && ((XCMapTile)map[row, col]).Rmp != null)
						{
							RmpEntry f = ((XCMapTile)map[row, col]).Rmp;
							upper.Reset();
							upper.AddLine(x, y, x + hWidth, y + hHeight);
							upper.AddLine(x + hWidth, y + hHeight, x, y + 2 * hHeight);
							upper.AddLine(x, y + 2 * hHeight, x - hWidth, y + hHeight);
							upper.CloseFigure();

							for (int rr = 0; rr < f.NumLinks; rr++)
							{
								Link l = f[rr];
								switch (l.Index)
								{
									case Link.NotUsed:
										break;
									case Link.ExitEast:
										g.DrawLine(pens["UnselectedLinkColor"], x, y + hHeight, Width, Height);
										break;
									case Link.ExitNorth:
										g.DrawLine(pens["UnselectedLinkColor"], x, y + hHeight, Width, 0);
										break;
									case Link.ExitSouth:
										g.DrawLine(pens["UnselectedLinkColor"], x, y + hHeight, 0, Height);
										break;
									case Link.ExitWest:
										g.DrawLine(pens["UnselectedLinkColor"], x, y + hHeight, 0, 0);
										break;
									default:
										if (map.Rmp[l.Index] != null)
										{
											if (map.Rmp[l.Index].Height == map.CurrentHeight)
											{
												int toRow = map.Rmp[l.Index].Row;
												int toCol = map.Rmp[l.Index].Col;
												g.DrawLine(pens["UnselectedLinkColor"], x, y + hHeight, origin.X + (toCol - toRow) * hWidth, origin.Y + (toCol + toRow + 1) * hHeight);
											}
										}
										break;
								}
							}
						}
					}
				}

				if (((XCMapTile)map[clickPoint.Y, clickPoint.X]).Rmp != null)
				{
					int r = clickPoint.Y;
					int c = clickPoint.X;
					RmpEntry f = ((XCMapTile)map[r, c]).Rmp;

					for (int rr = 0; rr < f.NumLinks; rr++)
					{
						Link l = f[rr];
						switch (l.Index)
						{
							case Link.NotUsed:
								break;
							case Link.ExitEast:
								g.DrawLine(pens["SelectedLinkColor"], origin.X + (c - r) * hWidth, origin.Y + (c + r + 1) * hHeight, Width, Height);
								break;
							case Link.ExitNorth:
								g.DrawLine(pens["SelectedLinkColor"], origin.X + (c - r) * hWidth, origin.Y + (c + r + 1) * hHeight, Width, 0);
								break;
							case Link.ExitSouth:
								g.DrawLine(pens["SelectedLinkColor"], origin.X + (c - r) * hWidth, origin.Y + (c + r + 1) * hHeight, 0, Height);
								break;
							case Link.ExitWest:
								g.DrawLine(pens["SelectedLinkColor"], origin.X + (c - r) * hWidth, origin.Y + (c + r + 1) * hHeight, 0, 0);
								break;
							default:
								if (map.Rmp[l.Index] != null && map.Rmp[l.Index].Height == map.CurrentHeight)
								{
									int toRow = map.Rmp[l.Index].Row;
									int toCol = map.Rmp[l.Index].Col;
									g.DrawLine(pens["SelectedLinkColor"], origin.X + (c - r) * hWidth, origin.Y + (c + r + 1) * hHeight, origin.X + (toCol - toRow) * hWidth, origin.Y + (toCol + toRow + 1) * hHeight);
								}
								break;
						}
					}
				}

				for (int row = 0, startX = origin.X, startY = origin.Y; row < map.MapSize.Rows; row++, startX -= hWidth, startY += hHeight)
				{
					for (int col = 0, x = startX, y = startY; col < map.MapSize.Cols; col++, x += hWidth, y += hHeight)
					{
						XCMapTile tile = (XCMapTile)map[row, col];
						if (map[row, col] != null && tile.Rmp != null)
						{
							upper.Reset();
							upper.AddLine(x, y, x + hWidth, y + hHeight);
							upper.AddLine(x + hWidth, y + hHeight, x, y + 2 * hHeight);
							upper.AddLine(x, y + 2 * hHeight, x - hWidth, y + hHeight);
							upper.CloseFigure();

							//if clicked on, draw Blue
							if (row == clickPoint.Y && col == clickPoint.X)
								g.FillPath(brushes["SelectedNodeColor"], upper);
							else if (tile.Rmp.Usage != SpawnUsage.NoSpawn)
								g.FillPath(brushes["SpawnNodeColor"], upper);
							else
								g.FillPath(brushes["UnselectedNodeColor"], upper);

							for (int rr = 0; rr < tile.Rmp.NumLinks; rr++)
							{
								Link l = tile.Rmp[rr];
								switch (l.Index)
								{
									case Link.NotUsed:
										break;
									case Link.ExitEast:
										break;
									case Link.ExitNorth:
										break;
									case Link.ExitSouth:
										break;
									case Link.ExitWest:
										break;
									default:
										if (map.Rmp[l.Index] != null && map.Rmp[l.Index].Height < map.CurrentHeight)
										{
											g.DrawLine(pens["UnselectedLinkColor"], x, y, x, y + hHeight * 2);
										}
										else if (map.Rmp[l.Index] != null && map.Rmp[l.Index].Height > map.CurrentHeight)
										{
											g.DrawLine(pens["UnselectedLinkColor"], x - hWidth, y + hHeight, x + hWidth, y + hHeight);
										}
										break;
								}
							}
						}
					}
				}

				for (int i = 0; i <= map.MapSize.Rows; i++)
					g.DrawLine(pens["GridLineColor"], origin.X - i * hWidth, origin.Y + i * hHeight, origin.X + ((map.MapSize.Cols - i) * hWidth), origin.Y + ((i + map.MapSize.Cols) * hHeight));
				for (int i = 0; i <= map.MapSize.Cols; i++)
					g.DrawLine(pens["GridLineColor"], origin.X + i * hWidth, origin.Y + i * hHeight, (origin.X + i * hWidth) - map.MapSize.Rows * hWidth, (origin.Y + i * hHeight) + map.MapSize.Rows * hHeight);

				g.DrawString("W", myFont, System.Drawing.Brushes.Black, 0, 0);
				g.DrawString("N", myFont, System.Drawing.Brushes.Black, Width - 30, 0);
				g.DrawString("S", myFont, System.Drawing.Brushes.Black, 0, Height - myFont.Height);
				g.DrawString("E", myFont, System.Drawing.Brushes.Black, Width - 30, Height - myFont.Height);
			}
		}
	}
}
