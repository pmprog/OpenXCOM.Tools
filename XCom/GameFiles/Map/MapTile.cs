using System;
using System.Drawing;
using System.Collections;
using XCom.Interfaces;
using XCom.Interfaces.Base;

namespace XCom
{
	public class XCMapTile:IMapTile
	{
		public enum MapQuadrant { Ground, West, North, Content };
		private RmpEntry rmpInfo;
		private ITile ground, north, west, content;		
        private bool blank;

		//private IUnit unit;

		private int maxSub = 0;		
		private int standOffset = 0;

		internal XCMapTile(ITile ground, ITile west, ITile north, ITile content)
		{
			this.ground = ground;
			this.north = north;
			this.west = west;
			this.content = content;

			calcTiles();
			drawAbove = true;
			blank = false;
			//unit = null;
		}

		public static XCMapTile BlankTile
		{
			get
			{
				XCMapTile mt = new XCMapTile(null, null, null, null);
				mt.blank = true;
				return mt;
			}
		}

		/// <summary>
		/// If drawing a unit 
		/// </summary>
		public int StandOffset
		{
			get { return standOffset; }
		}

		private void calcTiles()
		{
			int notNull = 0;
			maxSub = -255;
			if (ground != null)
			{
				notNull++;
				maxSub = ground.Info.TileOffset;
				standOffset = ground.Info.StandOffset;
			}
			if (north != null)
			{
				notNull++;
				maxSub = Math.Max(maxSub, north.Info.TileOffset);
				standOffset = Math.Max(standOffset, north.Info.TileOffset);
			}
			if (west != null)
			{
				notNull++;
				maxSub = Math.Max(maxSub, west.Info.TileOffset);
				standOffset = Math.Max(standOffset, west.Info.TileOffset);
			}
			if (content != null)
			{
				notNull++;
				maxSub = Math.Max(maxSub, content.Info.TileOffset);
				standOffset = Math.Max(standOffset, content.Info.TileOffset);
			}

			usedTiles = new XCTile[notNull];
			int space = 0;

			if (ground != null)
				usedTiles[space++] = ground;
			if (north != null)
				usedTiles[space++] = north;
			if (west != null)
				usedTiles[space++] = west;
			if (content != null)
				usedTiles[space++] = content;
		}

		public bool Blank
		{
			get { return blank; }
			set { blank = value; }
		}

		//public MapLocation MapCoords
		//{
		//    get{return mapCoords;}
		//    set{mapCoords=value;}
		//}

		public ITile this[MapQuadrant quad]
		{
			get
			{
				switch (quad)
				{
					case MapQuadrant.Ground:
						return Ground;
					case MapQuadrant.Content:
						return Content;
					case MapQuadrant.North:
						return North;
					case MapQuadrant.West:
						return West;
					default:
						return null;
				}
			}
			set
			{
				switch (quad)
				{
					case MapQuadrant.Ground:
						Ground = value;
						break;
					case MapQuadrant.Content:
						Content = value;
						break;
					case MapQuadrant.North:
						North = value;
						break;
					case MapQuadrant.West:
						West = value;
						break;
				}
			}
		}

		//public IUnit Unit
		//{
		//	get { return unit; }
		//	set { unit = value; }
		//}

		public ITile North
		{
			get { return north; }
			set { north = value; calcTiles(); }
		}

		public ITile Content
		{
			get { return content; }
			set { content = value; calcTiles(); }
		}

		public ITile Ground
		{
			get { return ground; }
			set { ground = value; calcTiles(); }
		}

		public ITile West
		{
			get { return west; }
			set { west = value; calcTiles(); }
		}

		public RmpEntry Rmp
		{
			get { return rmpInfo; }
			set { rmpInfo = value; }
		}
	}
}
