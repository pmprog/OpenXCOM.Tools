using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using XCom;

namespace MapView.RmpViewForm
{
	public delegate void MapPanelClickDelegate(object sender, MapPanelClickEventArgs e);

    public class MapPanel : Panel
    {
        protected XCMapFile map;
        protected Point origin;
        protected Point clickPoint;
        protected Dictionary<string, Pen> pens;
        protected Dictionary<string, SolidBrush> brushes;

        protected int hWidth = 8, hHeight = 4;

        public event MapPanelClickDelegate MapPanelClicked;

        public MapPanel()
        {
            pens = new Dictionary<string, Pen>();
            brushes = new Dictionary<string, SolidBrush>();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.UserPaint, true);
        }

        public XCMapFile Map
        {
            get { return map; }
            set
            {
                map = value;
                OnResize(null);
            }
        }

        /// <summary>
        /// Get the tile contained at (x,y) in local screen coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>null if (x,y) is an invalid location for a tile</returns>
        public XCMapTile GetTile(int x, int y)
        {
            Point p = convertCoordsDiamond(x, y);
            if (p.Y >= 0 && p.Y < map.MapSize.Rows &&
                p.X >= 0 && p.X < map.MapSize.Cols)
                return (XCMapTile) map[p.Y, p.X];
            return null;
        }

        public Point GetTileCoordinates(int x, int y)
        {
            Point p = convertCoordsDiamond(x, y);
            if (p.Y >= 0 && p.Y < map.MapSize.Rows && p.X >= 0 && p.X < map.MapSize.Cols)
                return p;
            return new Point(-1, -1);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            //Point pt = convertCoordsDiamond(e.X, e.Y);

            if (MapPanelClicked != null)
            {
                XCom.Interfaces.Base.IMapTile tile = null;

                Point p = convertCoordsDiamond(e.X, e.Y);
                if (p.Y >= 0 && p.Y < map.MapSize.Rows &&
                    p.X >= 0 && p.X < map.MapSize.Cols)
                    tile = map[p.Y, p.X];

                if (tile != null)
                {
                    clickPoint.X = p.X;
                    clickPoint.Y = p.Y;

                    map.SelectedTile = new MapLocation(clickPoint.Y, clickPoint.X, map.CurrentHeight);
                    MapPanelClickEventArgs mpe = new MapPanelClickEventArgs();
                    mpe.ClickTile = tile;
                    mpe.MouseEventArgs = e;
                    mpe.ClickLocation = new MapLocation(clickPoint.Y, clickPoint.X, map.CurrentHeight);
                    MapPanelClicked(this, mpe);

                    //RmpSquareClicked(clickPoint.Y, clickPoint.X, e.Button);
                }
            }

            Refresh();
        }

        protected override void OnResize(EventArgs e)
        {
            if (map != null)
            {
                if (Height > Width / 2)
                {
                    //use width
                    hWidth = (Width) / (map.MapSize.Rows + map.MapSize.Cols + 1);

                    if (hWidth % 2 != 0)
                        hWidth--;

                    hHeight = hWidth / 2;
                }
                else
                {
                    //use height
                    hHeight = (Height) / (map.MapSize.Rows + map.MapSize.Cols);
                    hWidth = hHeight * 2;
                }

                origin = new Point((map.MapSize.Rows) * hWidth, 0);
                Refresh();
            }
        }

        private Point convertCoordsDiamond(int xp, int yp)
        {
            int x = xp - origin.X;
            int y = yp - origin.Y;

            double x1 = (x * 1.0 / (hWidth * 2)) + (y * 1.0 / (hHeight * 2));
            double x2 = -(x * 1.0 - 2 * y * 1.0) / (hWidth * 2);

            return new Point((int) Math.Floor(x1), (int) Math.Floor(x2));
        }
    }

    public class MapPanelClickEventArgs:EventArgs
	{
		private XCom.Interfaces.Base.IMapTile clickTile;
		private MapLocation clickLocation;

		public MapLocation ClickLocation
		{
			get { return clickLocation; }
			set { clickLocation = value; }
		}

		public XCom.Interfaces.Base.IMapTile ClickTile
		{
		  get { return clickTile; }
		  set { clickTile = value; }
		}

		private MouseEventArgs me;

		public MouseEventArgs MouseEventArgs
		{
		  get { return me; }
		  set { me = value; }
		}

		public MapPanelClickEventArgs(){}
	}
}
