using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using XCom;
using XCom.Interfaces;
using XCom.Interfaces.Base;

namespace MapView
{
	public delegate void SelectedTileChanged(TilePanel sender,ITile newTile);
	public class TileView : Map_Observer_Form
	{
		private IContainer components;
		private RichTextBox rtb;
		private System.Windows.Forms.MainMenu menu;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem mcdInfoTab;

		private TilePanel all,ground,wWalls,nWalls,objects;
		private TilePanel[] panels;
		private Form MCDInfo;
		private System.Windows.Forms.TabControl tabs;
		private System.Windows.Forms.TabPage allTab;
		private System.Windows.Forms.TabPage groundTab;
		private System.Windows.Forms.TabPage objectsTab;
		private System.Windows.Forms.TabPage nWallsTab;
		private System.Windows.Forms.TabPage wWallsTab;
		private Settings settings;
		private Hashtable brushes;

		private static TileView myInstance;
		private TileView()
		{
			InitializeComponent();

			all = new TilePanel(TileType.All);
			ground=new TilePanel(TileType.Ground);
			wWalls = new TilePanel(TileType.WestWall);
			nWalls = new TilePanel(TileType.NorthWall);
			objects = new TilePanel(TileType.Object);

			panels = new TilePanel[]{all,ground,wWalls,nWalls,objects};

			addPanel(all,allTab);
			addPanel(ground,groundTab);
			addPanel(wWalls,wWallsTab);
			addPanel(nWalls,nWallsTab);
			addPanel(objects,objectsTab);

//
//			tp = new TilePanel();
//			this.Controls.Add(tp);
//			tp.TileChanged+=new SelectedTileChanged(tileChanged);

			OnResize(null);

			MenuItem edit = new MenuItem("Edit");
			edit.MenuItems.Add("Options",new EventHandler(options_click));
			menu.MenuItems.Add(edit);
		}

		private void options_click(object sender,EventArgs e)
		{
            PropertyForm pf = new PropertyForm("tileViewOptions",settings);
			pf.Text="Tile View Settings";
			pf.Show();
		}

		private void brushChanged(object sender,string key, object val)
		{
			((SolidBrush)brushes[key]).Color=(Color)val;
			Refresh();
		}

		protected override void LoadDefaultSettings(Settings settings)
		{
			brushes = new Hashtable();

			ValueChangedDelegate bc = new ValueChangedDelegate(brushChanged);

			foreach(string s in Enum.GetNames(typeof(SpecialType)))
			{
				brushes[s]=new SolidBrush(TilePanel.tileTypes[(int)Enum.Parse(typeof(SpecialType),s)]);
				settings.AddSetting(s,((SolidBrush)brushes[s]).Color,"Color of specified tile type","TileView",bc,false,null);
			}
			TilePanel.Colors=brushes;
		}

		//public Settings Settings
		//{
		//    get{return settings;}
		//}

		private void addPanel(TilePanel panel, TabPage page)
		{
			panel.Dock = DockStyle.Fill;
			page.Controls.Add(panel);
			panel.TileChanged+=new SelectedTileChanged(tileChanged);
		}

		public static TileView Instance
		{
			get
			{
				if(myInstance==null)
					myInstance = new TileView();
				return myInstance;
			}
		}

//		protected override void OnResize(EventArgs e)
//		{
//			if(tp!=null)
//			{
//				tp.Height=ClientSize.Height;
//				tp.Width=ClientSize.Width;
//			}
//		}

		private void tileChanged(TilePanel sender, ITile t)
		{
			if(t!=null && t.Info is McdEntry)
			{
				McdEntry info = (McdEntry)t.Info;
				Text = "TileView: mapID:"+t.MapID+" mcdID: "+t.ID;
				if(MCDInfo==null)
					return;
				rtb.Text = "";
				rtb.SelectionColor = Color.Gray;
				for(int i=0;i<30;i++)
					rtb.AppendText(info[i]+" ");
				rtb.AppendText("\n");
				rtb.SelectionColor = Color.Gray;
				for(int i=30;i<62;i++)
					rtb.AppendText(info[i]+" ");
				rtb.AppendText("\n\n");
				rtb.SelectionColor = Color.Black;

				rtb.AppendText(string.Format("Images: {0} {1} {2} {3} {4} {5} {6} {7}\n",info.Image1,info.Image2,info.Image3,info.Image4,info.Image5,info.Image6,info.Image7,info.Image8));// unsigned char Frame[8];      //Each frame is an index into the ____.TAB file; it rotates between the frames constantly.
				rtb.AppendText(string.Format("loft references: {0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10} {11}\n",info[8],info[9],info[10],info[11],info[12],info[13],info[14],info[15],info[16],info[17],info[18],info[19]));// unsigned char LOFT[12];      //The 12 levels of references into GEODATA\LOFTEMPS.DAT
				rtb.AppendText(string.Format("scang reference: {0} {1} -> {2}\n",info[20],info[21],info[20]*256+info[21]));// short int ScanG;      //A reference into the GEODATA\SCANG.DAT
				//rtb.AppendText(string.Format("Unknown data: {0}\n",info[22]));// unsigned char u23;
				//rtb.AppendText(string.Format("Unknown data: {0}\n",info[23]));// unsigned char u24;
				//rtb.AppendText(string.Format("Unknown data: {0}\n",info[24]));// unsigned char u25;
				//rtb.AppendText(string.Format("Unknown data: {0}\n",info[25]));// unsigned char u26;
				//rtb.AppendText(string.Format("Unknown data: {0}\n",info[26]));// unsigned char u27;
				//rtb.AppendText(string.Format("Unknown data: {0}\n",info[27]));// unsigned char u28;
				//rtb.AppendText(string.Format("Unknown data: {0}\n",info[28]));// unsigned char u29;
				//rtb.AppendText(string.Format("Unknown data: {0}\n",info[29]));// unsigned char u30;
				rtb.AppendText(string.Format("UFO Door?: {0}\n",info.UFODoor));// unsigned char UFO_Door;      //If it's a UFO door it uses only Frame[0] until it is walked through, then it animates once and becomes Alt_MCD.  It changes back at the end of the turn
				rtb.AppendText(string.Format("SeeThru?: {0}\n",info.StopLOS));// unsigned char Stop_LOS;      //You cannot see through this tile.
				rtb.AppendText(string.Format("No Floor?: {0}\n",info.NoGround));// unsigned char No_Floor;      //If 1, then a non-flying unit can't stand here
				rtb.AppendText(string.Format("Big Wall?: {0}\n",info.BigWall));// unsigned char Big_Wall;      //It's an object (tile type 3), but it acts like a wall
				rtb.AppendText(string.Format("Grav Lift?: {0}\n",info.GravLift));// unsigned char Gravlift;
				rtb.AppendText(string.Format("People Door?: {0}\n",info.HumanDoor));// unsigned char Door;      //It's a human style door--you walk through it and it changes to Alt_MCD
				rtb.AppendText(string.Format("Block fire?: {0}\n",info.BlockFire));// unsigned char Block_Fire;       //If 1, fire won't go through the tile
				rtb.AppendText(string.Format("Block Smoke?: {0}\n",info.BlockSmoke));// unsigned char Block_Smoke;      //If 1, smoke won't go through the tile
				//rtb.AppendText(string.Format("Unknown data: {0}\n",info[38]));// unsigned char u39;
				rtb.AppendText(string.Format("TU Walk: {0}\n",info.TU_Walk));// unsigned char TU_Walk;       //The number of TUs require to pass the tile while walking.  An 0xFF (255) means it's unpassable.
				rtb.AppendText(string.Format("TU Fly: {0}\n",info.TU_Fly));// unsigned char TU_Fly;        // remember, 0xFF means it's impassable!
				rtb.AppendText(string.Format("TU Slide: {0}\n",info.TU_Slide));// unsigned char TU_Slide;      // sliding things include snakemen and silacoids
				rtb.AppendText(string.Format("Armor: {0}\n",info.Armor));// unsigned char Armour;        //The higher this is the less likely it is that a weapon will destroy this tile when it's hit.
				rtb.AppendText(string.Format("HE Block: {0}\n",info.HE_Block));// unsigned char HE_Block;      //How much of an explosion this tile will block
				rtb.SelectionColor=Color.Red;
				rtb.AppendText(string.Format("Death tile: {0}\n",info.DieTile));// unsigned char Die_MCD;       //If the terrain is destroyed, it is set to 0 and a tile of type Die_MCD is added
				rtb.AppendText(string.Format("Flammable: {0}\n",info.Flammable));// unsigned char Flammable;     //How flammable it is (the higher the harder it is to set aflame)
				rtb.SelectionColor=Color.Red;
				rtb.AppendText(string.Format("Door open tile: {0}\n",info.Alt_MCD));// unsigned char Alt_MCD;       //If "Door" or "UFO_Door" is on, then when a unit walks through it the door is set to 0 and a tile type Alt_MCD is added.
				//rtb.AppendText(string.Format("Unknown data: {0}\n",info[47]));// unsigned char u48;
				rtb.AppendText(string.Format("Unit y offset: {0}\n",info.StandOffset));// signed char T_Level;      //When a unit or object is standing on the tile, the unit is shifted by this amount
				rtb.AppendText(string.Format("tile y offset: {0}\n",info.TileOffset));// unsigned char P_Level;      //When the tile is drawn, this amount is subtracted from its y (so y position-P_Level is where it's drawn)
				//rtb.AppendText(string.Format("Unknown data: {0}\n",info[50]));// unsigned char u51;
				rtb.AppendText(string.Format("block light[0-10]: {0}\n",info.LightBlock));// unsigned char Light_Block;     //The amount of light it blocks, from 0 to 10
				rtb.AppendText(string.Format("footstep sound effect: {0}\n",info.Footstep));// unsigned char Footstep;         //The Sound Effect set to choose from when footsteps are on the tile
				rtb.AppendText(string.Format("tile type: {0}:{1}\n",(sbyte)info.TileType,info.TileType+""));//info.TileType==0?"floor":info.TileType==1?"west wall":info.TileType==2?"north wall":info.TileType==3?"object":"Unknown"));// unsigned char Tile_Type;       //This is the type of tile it is meant to be -- 0=floor, 1=west wall, 2=north wall, 3=object .  When this type of tile is in the Die_As or Open_As flags, this value is added to the tile coordinate to determine the byte in which the tile type should be written.
				rtb.AppendText(string.Format("high explosive type: {0}:{1}\n",info.HE_Type,info.HE_Type==0?"HE":info.HE_Type==1?"smoke":"unknown"));// unsigned char HE_Type;         //0=HE  1=Smoke
				rtb.AppendText(string.Format("HE Strength: {0}\n",info.HE_Strength));// unsigned char HE_Strength;     //The strength of the explosion caused when it's destroyed.  0 means no explosion.
				rtb.AppendText(string.Format("smoke blockage: {0}\n",info.SmokeBlockage));// unsigned char Smoke_Blockage;      //? Not sure about this ...
				rtb.AppendText(string.Format("# turns to burn: {0}\n",info.Fuel));// unsigned char Fuel;      //The number of turns the tile will burn when set aflame
				rtb.AppendText(string.Format("amount of light produced: {0}\n",info.LightSource));// unsigned char Light_Source;      //The amount of light this tile produces
				rtb.AppendText(string.Format("special properties: {0}\n",info.TargetType));// unsigned char Target_Type;       //The special properties of the tile
				//rtb.AppendText(string.Format("Unknown data: {0}\n",info[60]));// unsigned char u61;
				//rtb.AppendText(string.Format("Unknown data: {0}\n",info[61]));// unsigned char u62;
			}
		}

		public override IMap_Base Map
		{
			set 
			{ 
				base.Map = value;
                if (value == null)
                {
                    Tiles = null;
                }
                else
                {
                    Tiles = value.Tiles;
                }
			}
		}

		//public void SetMap(object sender, SetMapEventArgs e)
		//{
		//    Tiles = ((XCMapFile)e.Map).Tiles;
		//}

		public System.Collections.Generic.List<ITile> Tiles
		{
			set
			{
				for(int i=0;i<panels.Length;i++)
					panels[i].Tiles = value;
				OnResize(null);
			}
		}

//		private void vert_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
//		{
//			tp.StartY = -vert.Value;
//		}

		public ITile SelectedTile
		{
			get
			{
				return panels[tabs.SelectedIndex].SelectedTile;
			}
			set
			{
				tabs.SelectedIndex=0;
				all.SelectedTile=value;
				Refresh();
			}
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.menu = new System.Windows.Forms.MainMenu(this.components);
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.mcdInfoTab = new System.Windows.Forms.MenuItem();
			this.tabs = new System.Windows.Forms.TabControl();
			this.allTab = new System.Windows.Forms.TabPage();
			this.groundTab = new System.Windows.Forms.TabPage();
			this.wWallsTab = new System.Windows.Forms.TabPage();
			this.nWallsTab = new System.Windows.Forms.TabPage();
			this.objectsTab = new System.Windows.Forms.TabPage();
			this.tabs.SuspendLayout();
			this.SuspendLayout();
			// 
			// menu
			// 
			this.menu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mcdInfoTab});
			this.menuItem1.Text = "MCD";
			// 
			// mcdInfoTab
			// 
			this.mcdInfoTab.Index = 0;
			this.mcdInfoTab.Text = "MCD Info";
			this.mcdInfoTab.Click += new System.EventHandler(this.mcdInfoTab_Click);
			// 
			// tabs
			// 
			this.tabs.Controls.Add(this.allTab);
			this.tabs.Controls.Add(this.groundTab);
			this.tabs.Controls.Add(this.wWallsTab);
			this.tabs.Controls.Add(this.nWallsTab);
			this.tabs.Controls.Add(this.objectsTab);
			this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabs.Location = new System.Drawing.Point(0, 0);
			this.tabs.Name = "tabs";
			this.tabs.SelectedIndex = 0;
			this.tabs.Size = new System.Drawing.Size(320, 273);
			this.tabs.TabIndex = 0;
			// 
			// allTab
			// 
			this.allTab.Location = new System.Drawing.Point(4, 22);
			this.allTab.Name = "allTab";
			this.allTab.Size = new System.Drawing.Size(312, 247);
			this.allTab.TabIndex = 0;
			this.allTab.Text = "All";
			// 
			// groundTab
			// 
			this.groundTab.Location = new System.Drawing.Point(4, 22);
			this.groundTab.Name = "groundTab";
			this.groundTab.Size = new System.Drawing.Size(312, 247);
			this.groundTab.TabIndex = 1;
			this.groundTab.Text = "Ground";
			// 
			// wWallsTab
			// 
			this.wWallsTab.Location = new System.Drawing.Point(4, 22);
			this.wWallsTab.Name = "wWallsTab";
			this.wWallsTab.Size = new System.Drawing.Size(312, 247);
			this.wWallsTab.TabIndex = 2;
			this.wWallsTab.Text = "West Walls";
			// 
			// nWallsTab
			// 
			this.nWallsTab.Location = new System.Drawing.Point(4, 22);
			this.nWallsTab.Name = "nWallsTab";
			this.nWallsTab.Size = new System.Drawing.Size(312, 247);
			this.nWallsTab.TabIndex = 4;
			this.nWallsTab.Text = "North Walls";
			// 
			// objectsTab
			// 
			this.objectsTab.Location = new System.Drawing.Point(4, 22);
			this.objectsTab.Name = "objectsTab";
			this.objectsTab.Size = new System.Drawing.Size(312, 247);
			this.objectsTab.TabIndex = 3;
			this.objectsTab.Text = "Objects";
			// 
			// TileView
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(320, 273);
			this.Controls.Add(this.tabs);
			this.Menu = this.menu;
			this.Name = "TileView";
			this.ShowInTaskbar = false;
			this.Text = "TileView";
			this.tabs.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion

		private void mcdInfoTab_Click(object sender, System.EventArgs e)
		{
			if(!mcdInfoTab.Checked)
			{
				if(MCDInfo==null)
				{
					MCDInfo = new Form();
					MCDInfo.Text = "MCD Info";
					rtb = new RichTextBox();
					rtb.WordWrap=false;
					rtb.ReadOnly=true;
					MCDInfo.Controls.Add(rtb);
					rtb.Dock=DockStyle.Fill;
					MCDInfo.Size = new Size(200,470);
					MCDInfo.Closing+=new CancelEventHandler(infoTabClosing);
				}
				MCDInfo.Visible=true;
				MCDInfo.Location = new Point(this.Location.X-MCDInfo.Width,this.Location.Y);
				MCDInfo.Show();
				mcdInfoTab.Checked=true;
			}
		}

		private void infoTabClosing(object sender, CancelEventArgs e)
		{
			e.Cancel=true;
			MCDInfo.Visible=false;
			mcdInfoTab.Checked=false;
		}
	}

	public class TilePanel : Panel
	{
		private ITile[] tiles;

		private int space=2;
		private int height = 40,width=32;
		private SolidBrush brush = new SolidBrush(Color.FromArgb(204,204,255));
		private Pen pen = new Pen(Brushes.Red,3);
		private int startY=0;
		private int selectedNum;
		private VScrollBar vert;
		private int numAcross=1;

		public static readonly Color[] tileTypes={Color.Cornsilk,Color.Lavender,Color.DarkRed,Color.Fuchsia,Color.Aqua,
									Color.DarkOrange,Color.DeepPink,Color.LightBlue,Color.Lime,
									  Color.LightGreen,Color.MediumPurple,Color.LightCoral,Color.LightCyan,
									  Color.Yellow,Color.Blue};
		private TileType type;

		public event SelectedTileChanged TileChanged;
		//private static PckFile extraFile;
		private static Hashtable brushes;

		//public static PckFile ExtraFile
		//{
		//    get{return extraFile;}
		//    set{extraFile=value;}
		//}

		public static Hashtable Colors
		{
			get{return brushes;}
			set{brushes=value;}
		}

		public TilePanel(TileType type)
		{
			this.type=type;
			vert = new VScrollBar();
			vert.ValueChanged+=new EventHandler(valChange);
			vert.Location = new Point(Width-vert.Width,0);
			this.Controls.Add(vert);
			MapViewPanel.ImageUpdate+=new EventHandler(tick);
			SetStyle(ControlStyles.AllPaintingInWmPaint|ControlStyles.DoubleBuffer|ControlStyles.UserPaint,true);			
			selectedNum=0;

			Globals.LoadExtras();
		}

		private void valChange(object sender, EventArgs e)
		{
			startY = -vert.Value;
			Refresh();
		}

		protected override void OnResize(EventArgs e)
		{
			numAcross = (Width-(vert.Visible?vert.Width:0))/(width+2*space);
			vert.Location = new Point(Width-vert.Width,0);
			vert.Height=Height;
			vert.Maximum = Math.Max((PreferredHeight-Height)+10,vert.Minimum);	
		
			if(vert.Maximum==vert.Minimum)
				vert.Visible=false;
			else
				vert.Visible=true;
			Refresh();
		}

		public int StartY
		{
			get{return startY;}
			set{startY=value;Refresh();}
		}

		public int PreferredHeight
		{
			get
			{
				if(tiles!=null && numAcross>0)
				{
					if(tiles.Length%numAcross==0)
						return (tiles.Length/numAcross)*(height+2*space);

					return (1+tiles.Length/numAcross)*(height+2*space);
				}
				return 0;
			}
		}

		public System.Collections.Generic.List<ITile> Tiles
		{
			set
			{
			    if (value != null)
			    {
			        if (type == TileType.All)
			        {
			            //tiles=value;
			            tiles = new ITile[value.Count + 1];
			            tiles[0] = null;
			            for (int i = 0; i < value.Count; i++)
			                tiles[i + 1] = value[i];
			        }
			        else
			        {
			            var list = new List<ITile>();
			            for (int i = 0; i < value.Count; i++)
			                if (value[i].Info.TileType == type)
			                    list.Add(value[i]);
			            tiles = list.ToArray();
			        }
			    }
			    else
			    {
			        tiles = null;
			    }
			    OnResize(null);
			}
		}

		private int scrollAmount=20;

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			if(e.Delta<0)
				if(vert.Value+scrollAmount<vert.Maximum)
					vert.Value+=scrollAmount;
				else
					vert.Value = vert.Maximum;
			else if(e.Delta>0)
				if(vert.Value-scrollAmount>vert.Minimum)
					vert.Value-=scrollAmount;
				else
					vert.Value = vert.Minimum;
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			this.Focus();
		    if (tiles == null) return;
			int x = e.X/(width+2*space);
			int y = (e.Y-startY)/(height+2*space);

			if(x>=numAcross)
				x=numAcross-1;

			selectedNum = y*numAcross+x;

			selectedNum = (selectedNum<tiles.Length)?selectedNum:tiles.Length-1;
			if(TileChanged!=null)
				TileChanged(this,SelectedTile);
			Refresh();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if(tiles!=null)
			{
				Graphics g = e.Graphics;				

				int i=0,j=0;
				foreach(ITile t in tiles)
				{
					if(t!=null && (type==TileType.All || t.Info.TileType==type))
					{
						g.FillRectangle((SolidBrush)brushes[t.Info.TargetType.ToString()]/*new SolidBrush(tileTypes[(int)t.Info.TargetType])*/,i*(width+2*space),startY+j*(height+2*space),width+2*space,height+2*space);
						g.DrawImage(t[MapViewPanel.Current].Image,i*(width+2*space),startY+j*(height+2*space)-t.Info.TileOffset);

						if(t.Info.HumanDoor || t.Info.UFODoor)
							g.DrawString("Door",this.Font,Brushes.Black,i*(width+2*space),startY+j*(height+2*space)+PckImage.Height-Font.Height);

						i=(i+1)%numAcross;
						if(i==0)
							j++;
					}
					else if(t==null)
					{
						if(Globals.ExtraTiles!=null)
							g.DrawImage(Globals.ExtraTiles[0].Image, i * (width + 2 * space), startY + j * (height + 2 * space));
						i=(i+1)%numAcross;
						if(i==0)
							j++;
					}
				}

				//g.DrawRectangle(brush,(selectedNum%numAcross)*(width+2*space),startY+(selectedNum/numAcross)*(height+2*space),width+2*space,height+2*space)				

				for(int k=0;k<=numAcross+1;k++)
					g.DrawLine(Pens.Black,k*(width+2*space),startY,k*(width+2*space),startY+PreferredHeight);
				for(int k=0;k<=PreferredHeight;k+=(height+2*space))
					g.DrawLine(Pens.Black,0,startY+k,numAcross*(width+2*space),startY+k);

				g.DrawRectangle(pen,(selectedNum%numAcross)*(width+2*space),startY+(selectedNum/numAcross)*(height+2*space),width+2*space,height+2*space);
			}
		}

		public ITile SelectedTile
		{
			get
			{
				return tiles[selectedNum];
			}
			set
			{
				if(value==null)
					selectedNum=0;
				else
				{
					selectedNum = value.MapID+1;

					if(TileChanged!=null)
						TileChanged(this,SelectedTile);

					int y = startY+(selectedNum/numAcross)*(height+2*space);
					int val = -(startY-y);

					if(val > vert.Minimum)
					{
						if(val < vert.Maximum)
							vert.Value = val;
						else
							vert.Value = vert.Maximum;
					}
					else
						vert.Value = vert.Minimum;
				}
			}
		}

		private void tick(object sender, EventArgs e)
		{
			Refresh();
		}
	}
}
