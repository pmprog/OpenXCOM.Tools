using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using XCom;
using XCom.Interfaces;
using System.Collections;
using XCom.Interfaces.Base;
using MapView.TopViewForm;

namespace MapView
{
	public class View :Panel
	{
		private IMap_Base map;
		private Point origin = new Point(100,0);
		private int currentImage;
		private Point clickPoint,clickPointx;

		private MapView.Cursor cursor;
		private int offX=0,offY=0;
//		private int currentLevel;

		private Size viewable;
		private bool newLeft;
		private Point topLeft;

		private static int hWid=16, hHeight = 8;

		//public event EventHandler ViewClicked;

		private bool drawAll=true;
		private bool[] draw={false,false,false,false};
		private bool[] vis = {false,false,false,false};
		private bool flipLock=false,flipLock2=false;
		
		private bool mDown;
		private Point startDrag,endDrag;
		private Pen dashPen;
		private bool selectGrayscale=true;

		private GraphicsPath underGrid;
		private Brush transBrush;
		private Color gridColor;
		private bool useGrid=true;
		private IMapTile[,] copied;

		public event EventHandler DragChanged;
		//public event HeightChangedDelegate HeightChanged;

		public View()
		{
			map=null;
			currentImage=0;
			clickPoint = startDrag=endDrag=clickPointx=new Point(-1,-1);
			topLeft=new Point(0,0);

			this.SetStyle(ControlStyles.DoubleBuffer|ControlStyles.UserPaint|ControlStyles.AllPaintingInWmPaint,true);
			newLeft=true;

			gridColor = Color.FromArgb(175,69,100,129);
			transBrush = new SolidBrush(gridColor);

			//dashPen = new Pen(new HatchBrush(HatchStyle.DarkHorizontal,Color.Black,transGray),1);
			dashPen = new Pen(Brushes.Black,1);
		}
		
		public void Paste()
		{
			if(copied!=null)
			{
				//row  col
				//y    x

				for (int r = startDrag.Y; r < map.MapSize.Rows && (r - startDrag.Y) < copied.GetLength(0); r++)
					for (int c = startDrag.X; c < map.MapSize.Cols && (c - startDrag.X) < copied.GetLength(1); c++)
					{
                        XCMapTile tile = map[r, c] as XCMapTile;
                        XCMapTile copyTile = copied[r - startDrag.Y, c - startDrag.X] as XCMapTile;
                        tile.Ground = copyTile.Ground;
                        tile.Content = copyTile.Content;
                        tile.West = copyTile.West;
                        tile.North = copyTile.North;
					}

				Globals.MapChanged = true;
				Refresh();
			}
		}

		public bool SelectGrayscale
		{
			get{return selectGrayscale;}
			set{selectGrayscale=value;Refresh();}
		}

		public void ClearSelection()
		{
			Point s=new Point(0,0);
			Point e=new Point(0,0);

			s.X=Math.Min(startDrag.X,endDrag.X);
			s.Y=Math.Min(startDrag.Y,endDrag.Y);

			e.X=Math.Max(startDrag.X,endDrag.X);
			e.Y=Math.Max(startDrag.Y,endDrag.Y);

			for(int c=s.X;c<=e.X;c++)
				for(int r=s.Y;r<=e.Y;r++)
					map[r,c]=XCMapTile.BlankTile;
			Globals.MapChanged = true;
			Refresh();
		}

		public void Copy()
		{
			Point s=new Point(0,0);
			Point e=new Point(0,0);

			s.X=Math.Min(startDrag.X,endDrag.X);
			s.Y=Math.Min(startDrag.Y,endDrag.Y);

			e.X=Math.Max(startDrag.X,endDrag.X);
			e.Y=Math.Max(startDrag.Y,endDrag.Y);

			//row  col
			//y    x

			copied = new XCMapTile[e.Y-s.Y+1,e.X-s.X+1];

			for(int c=s.X;c<=e.X;c++)
				for(int r=s.Y;r<=e.Y;r++)
					copied[r-s.Y,c-s.X]=map[r,c];

//			Console.WriteLine("Copied block of size {0},{1},{2}",endR-startR+1,endC-startC+1,map.Height-map.CurrentLevel);
		}

		public Color GridColor
		{
			get{return gridColor;}
			set{gridColor=value;transBrush=new SolidBrush(value);Refresh();}
		}

		public Color GridLineColor
		{
			get{return dashPen.Color;}
			set{dashPen.Color=value;Refresh();}
		}

		public int GridLineWidth
		{
			get{return (int)dashPen.Width;}
			set{dashPen.Width=value;Refresh();}
		}

		public bool UseGrid
		{
			get{return useGrid;}
			set{useGrid=value;Refresh();}
		}

		public new MapView.Cursor Cursor
		{
			get{return cursor;}
			set{cursor=value;Refresh();}
		}

		public bool DrawAll
		{
			get{return drawAll;}
			set{drawAll=value;Refresh();}
		}

		public bool[] Vis
		{
			get{return vis;}
		}

		public bool[] Draw
		{
			get{return draw;}
		}

		public new void Resize()
		{
			OnResize(null);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if(map!=null)
			{
				mDown=true;
				clickPointx = convertCoordsDiamond(e.X,e.Y,map.CurrentHeight);
				StartDrag=EndDrag=clickPointx;
				flipLock2=true;
				if(!drawAll && !flipLock)
					map[clickPoint.Y,clickPoint.X].DrawAbove = !map[clickPoint.Y,clickPoint.X].DrawAbove;

				if(DragChanged!=null)
					DragChanged(null,null);

				map.SelectedTile = new MapLocation(clickPoint.Y, clickPoint.X, map.CurrentHeight);

				//if(ViewClicked!=null)
				//{
				//    Args a = new Args(ArgType.MapClicked);
				//    a.Location = new MapLocation(clickPoint.Y,clickPoint.X,map.CurrentHeight);
				//    map.SelectedTile = new MapLocation(clickPoint.Y,clickPoint.X,map.CurrentHeight);
				//    ViewClicked(this,a);
				//}

				Focus();
				Refresh();
				flipLock2=false;
			}
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			if(e.Delta>0)
				map.Up();
			else if(e.Delta<0)
				map.Down();
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			mDown=false;
			Refresh();
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if(map!=null)
			{
				Point temp = convertCoordsDiamond(e.X,e.Y,map.CurrentHeight);

				if(temp.X!=clickPoint.X || temp.Y != clickPoint.Y)
				{
					clickPoint=temp;

					if(mDown)
					{
						EndDrag=temp;
						if(DragChanged!=null)
							DragChanged(null,null);
					}

					Refresh();
				}
			}
		}

		public Point StartDrag
		{
			get{return startDrag;}
			set
			{
				startDrag=value;
				if(startDrag.Y<0)
					startDrag.Y=0;
				else if (startDrag.Y >= map.MapSize.Rows)
					startDrag.Y = map.MapSize.Rows - 1;

				if(startDrag.X<0)
					startDrag.X=0;
				else if (startDrag.X >= map.MapSize.Cols)
					startDrag.X = map.MapSize.Cols - 1;
			}
		}

		public Point EndDrag
		{
			get{return endDrag;}
			set
			{
				endDrag=value;
				if(endDrag.Y<0)
					endDrag.Y=0;
				else if (endDrag.Y >= map.MapSize.Rows)
					endDrag.Y = map.MapSize.Rows - 1;

				if(endDrag.X<0)
					endDrag.X=0;
				else if (endDrag.X >= map.MapSize.Cols)
					endDrag.X = map.MapSize.Cols - 1;
			}
		}

		public IMap_Base Map
		{
			get{return map;}
			set
			{
				if (map != null)
				{
					map.HeightChanged -= new HeightChangedDelegate(mapHeight);
					map.SelectedTileChanged -= new SelectedTileChangedDelegate(tileChange);
				}

				map=value;
				if(map!=null)
				{
					origin = new Point((map.MapSize.Rows - 1) * hWid * Globals.PckImageScale, 0);
					map.HeightChanged+=new HeightChangedDelegate(mapHeight);
					map.SelectedTileChanged+=new SelectedTileChangedDelegate(tileChange);
					Width = (map.MapSize.Rows + map.MapSize.Cols) * hWid * Globals.PckImageScale;
					Height = map.MapSize.Height * 25 * Globals.PckImageScale + (map.MapSize.Rows + map.MapSize.Cols) * hHeight * Globals.PckImageScale;
				}
			}
		}

		public int CurrentImage
		{
			set{currentImage=value;}
		}

		public Size Viewable
		{
			get{return viewable;}
			set{viewable=value;}
		}

		public bool NewLeft
		{
			set{newLeft=value;}
		}

		private void tileChange(IMap_Base mapFile,SelectedTileChangedEventArgs e)// MapLocation newCoords)
		{
			MapLocation newCoords = e.MapLocation;
			clickPointx = new Point(newCoords.Col,newCoords.Row);

			flipLock=true;
			if(!drawAll && !flipLock2)
				map[newCoords.Row, newCoords.Col, map.CurrentHeight].DrawAbove = !map[newCoords.Row, newCoords.Col, map.CurrentHeight].DrawAbove;

			//if(ViewClicked!=null)
			//{
			//    Args a = new Args(ArgType.MapClicked);
			//    a.Location = new MapLocation(newCoords.Col, newCoords.Row, map.CurrentHeight);
			//    ViewClicked(this,a);
			//}
			flipLock=false;
		}

		private void mapHeight(IMap_Base mapFile, HeightChangedEventArgs e)
		{
			Refresh();
			//if(HeightChanged!=null)
			//	HeightChanged(mapFile,e);
		}

		private int topx,topy,wid,hei,bottomx,bottomy;

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if(map!=null)
			{
				Graphics g = e.Graphics;

				if(newLeft)
				{
					topx = -(Location.X+PckImage.Width);
					topy = -(Location.Y+PckImage.Height);
					wid = (-Location.X)+viewable.Width;
					hei = (-Location.Y)+viewable.Height+PckImage.Height;
					bottomx = topx+wid+PckImage.Width;
					bottomy = topy+hei+PckImage.Height;

					newLeft=false;
				}

				/*
				for(int h=bv.map.Height-1;h>=0;h--)
				if(h>=bv.map.CurrentLevel || h==bv.map.Height-1)
				{						
					Point ur = bv.convertCoordsDiamond(new Point(bv.width,0),h);
					if(ur.Y<0)
						ur.Y=0;
					ur.X=0;
					Point start = bv.convertCoordsRect(ur,h);
					//at this point, we are going to start drawing on the row that crosses through the top right corner
					for(int row=ur.Y,startX = start.X,startY=start.Y;row<bv.map.Rows && startY < bv.height;row++,startX-=bv.hWid,startY+=bv.hHeight)
					{
						//hWid is half the width of a diamond (16)
						//hHeight is half the height of a diamond (8)
						for(int col=0,x=startX,y=startY;col<bv.map.Cols;col++,x+=16,y+=8)
						{
							if(x>=bv.width || y>=bv.height)
								break;

							if(x<=-PckImage.Width || y <= -PckImage.Height || bv.map[row,col,h]==null)
								continue;
								*/
				for (int h = map.MapSize.Height - 1; h >= 0; h--)
				{
					bool val=true;

					if(!drawAll)
						val=true;
					else
					{
						if (h >= map.CurrentHeight)
							val=true;
						else
							val=false;
					}

					if((!drawAll && vis[h]) || !val)
						continue;

					if (h == map.CurrentHeight && useGrid)
					{
						underGrid=new GraphicsPath();
						Point pt0 = new Point(origin.X + hWid, origin.Y + (map.CurrentHeight + 1) * 24);
						Point pt1 = new Point(origin.X + map.MapSize.Cols * hWid + hWid, origin.Y + map.MapSize.Cols * hHeight + (map.CurrentHeight + 1) * 24);
						Point pt2 = new Point(origin.X + hWid + (map.MapSize.Cols - map.MapSize.Rows) * hWid, origin.Y + (map.MapSize.Rows + map.MapSize.Cols) * hHeight + (map.CurrentHeight + 1) * 24);
						Point pt3 = new Point(origin.X - map.MapSize.Rows * hWid + hWid, origin.Y + map.MapSize.Rows * hHeight + (map.CurrentHeight + 1) * 24);
						underGrid.AddLine(pt0,pt1);
						underGrid.AddLine(pt1,pt2);
						underGrid.AddLine(pt2,pt3);
						underGrid.CloseFigure();

						g.FillPath(transBrush,underGrid);

						for (int i = 0; i <= map.MapSize.Rows; i++)
							g.DrawLine(dashPen, origin.X - i * hWid + hWid, origin.Y + i * hHeight + (map.CurrentHeight + 1) * 24,
								origin.X + ((map.MapSize.Cols - i) * hWid) + hWid, origin.Y + (map.CurrentHeight + 1) * 24 + ((i + map.MapSize.Cols) * hHeight));
						for (int i = 0; i <= map.MapSize.Cols; i++)
							g.DrawLine(dashPen, origin.X + i * hWid + hWid, origin.Y + i * hHeight + (map.CurrentHeight + 1) * 24,
								(origin.X + i * hWid + hWid) - map.MapSize.Rows * hWid, (origin.Y + i * hHeight) + map.MapSize.Rows * hHeight + (map.CurrentHeight + 1) * 24);
					}

					for (int row = 0, startX = origin.X, startY = origin.Y + (24 * h * Globals.PckImageScale); row < map.MapSize.Rows; row++, startX -= hWid * Globals.PckImageScale, startY += hHeight * Globals.PckImageScale)
					{
						for (int col = 0, x = startX, y = startY; col < map.MapSize.Cols; col++, x += hWid * Globals.PckImageScale, y += hHeight * Globals.PckImageScale)
						{
							if(x>bottomx || y>bottomy)
								break;

							bool here = false;
							if(row==clickPoint.Y && col == clickPoint.X || row==clickPointx.Y && col == clickPointx.X)
							{
								if(cursor!=null)
									cursor.DrawHigh(g, x, y, MapViewPanel.Current, false, map.CurrentHeight == h);	
								here=true;
							}

							if(x>topx && x < wid && y > topy && y < hei)
							{
								if(!drawAll && draw[h])	
								{
									if(map[row,col,h].DrawAbove)
									{
										if(!selectGrayscale)
                                            drawTile(g, (XCMapTile)map[row, col, h], x, y);	
										else if((here && Globals.UseGray) || (/*mDown && */((row>=startDrag.Y && row<=endDrag.Y) || (row>=startDrag.Y && row<=endDrag.Y))&& 
											((col>=startDrag.X && col<=endDrag.X) || (col>=startDrag.X && col<=endDrag.X))))
                                            drawTileGray(g, (XCMapTile)map[row, col, h], x, y);
										else
                                            drawTile(g, (XCMapTile)map[row, col, h], x, y);								
									}
								}
								else if (h == map.CurrentHeight || map[row, col, h].DrawAbove)
								{
									if(!selectGrayscale)
                                        drawTile(g, (XCMapTile)map[row, col, h], x, y);	
									else if((here && Globals.UseGray))
                                        drawTileGray(g, (XCMapTile)map[row, col, h], x, y);
									else if(startDrag.X>=endDrag.X && col<=startDrag.X&&col>=endDrag.X)
									{
										if(startDrag.Y>=endDrag.Y && row<=startDrag.Y && row>=endDrag.Y)
                                            drawTileGray(g, (XCMapTile)map[row, col, h], x, y);
										else if(startDrag.Y<=endDrag.Y && row>=startDrag.Y && row<=endDrag.Y)
                                            drawTileGray(g, (XCMapTile)map[row, col, h], x, y);
										else
                                            drawTile(g, (XCMapTile)map[row, col, h], x, y);	
											
									}
									else if(startDrag.X<=endDrag.X && col>=startDrag.X && col<=endDrag.X)
									{
										if(startDrag.Y>=endDrag.Y && row<=startDrag.Y && row>=endDrag.Y)
                                            drawTileGray(g, (XCMapTile)map[row, col, h], x, y);
										else if(startDrag.Y<=endDrag.Y && row>=startDrag.Y && row<=endDrag.Y)
                                            drawTileGray(g, (XCMapTile)map[row, col, h], x, y);
										else
                                            drawTile(g, (XCMapTile)map[row, col, h], x, y);	
									}
									else
										drawTile(g,(XCMapTile)map[row,col,h],x,y);	
								}
							}

							if(here && cursor!=null)
								cursor.DrawLow(g, x, y, MapViewPanel.Current, false, map.CurrentHeight == h);
						}
					}
					
				}
			}
		}

		public Point Origin
		{
			get{return origin;}
		}

		private void drawTile(Graphics g, XCMapTile mt,int x, int y)
		{
			if(mt.Ground != null && TopView.Instance.GroundVisible)
				g.DrawImage(mt.Ground[MapViewPanel.Current].Image,x,y-mt.Ground.Info.TileOffset);

			if(mt.North != null && TopView.Instance.NorthVisible)
				g.DrawImage(mt.North[MapViewPanel.Current].Image,x,y-mt.North.Info.TileOffset);

			if(mt.West != null && TopView.Instance.WestVisible)
				g.DrawImage(mt.West[MapViewPanel.Current].Image,x,y-mt.West.Info.TileOffset);

			if(mt.Content != null && TopView.Instance.ContentVisible)
				g.DrawImage(mt.Content[MapViewPanel.Current].Image,x,y-mt.Content.Info.TileOffset);
		}

		private void drawTileGray(Graphics g, XCMapTile mt,int x, int y)
		{
			if(mt.Ground != null && TopView.Instance.GroundVisible)
				g.DrawImage(mt.Ground[MapViewPanel.Current].Gray,x,y-mt.Ground.Info.TileOffset);

			if(mt.North != null && TopView.Instance.NorthVisible)
				g.DrawImage(mt.North[MapViewPanel.Current].Gray,x,y-mt.North.Info.TileOffset);

			if(mt.West != null && TopView.Instance.WestVisible)
				g.DrawImage(mt.West[MapViewPanel.Current].Gray,x,y-mt.West.Info.TileOffset);

			if(mt.Content != null && TopView.Instance.ContentVisible)
				g.DrawImage(mt.Content[MapViewPanel.Current].Gray,x,y-mt.Content.Info.TileOffset);
		}

		/// <summary>
		/// convert from rectangular coordinates to tile coordinates
		/// </summary>
		/// <param name="p">x,y coordinate from the mouse</param>
		/// <returns>(column,row) coordinates corresponding to the map</returns>
//		private Point convertCoordsDiamond(Point p,int level)
//		{
//			int x = p.X-origin.X-PckImage.Width/2; //16 is half the width of the diamond
//			int y = p.Y-origin.Y-(24*PckImage.Scale)*(level+1); //24 is the distance from the top of the diamond to the very top of the image
//
//			double x1 = (x*1.0/32)+(y*1.0/16);
//			double x2 = -(x*1.0-2*y*1.0)/32;
//
//			return new Point((int)Math.Floor(x1),(int)Math.Floor(x2));
//		}


		/// <summary>
		/// convert from screen coordinates to tile coordinates
		/// </summary>
		/// <param name="xp"></param>
		/// <param name="yp"></param>
		/// <param name="level"></param>
		/// <returns></returns>
		private Point convertCoordsDiamond(int xp, int yp,int level)
		{
			int x = xp-(origin.X+offX)-(hWid*Globals.PckImageScale); //16 is half the width of the diamond
			int y = yp-(origin.Y+offY)-(24*Globals.PckImageScale)*(level+1); //24 is the distance from the top of the diamond to the very top of the image

			double x1 = (x*1.0/(2*(hWid*Globals.PckImageScale)))+(y*1.0/(2*(hHeight*Globals.PckImageScale)));
			double x2 = -(x*1.0-2*y*1.0)/(2*(hWid*Globals.PckImageScale));

			return new Point((int)Math.Floor(x1),(int)Math.Floor(x2));
		}

		/// <summary>
		/// convert from map coordinates to rectangular coordinates
		/// </summary>
		/// <param name="p">the map coordinates in (column,row) form</param>
		/// <returns>(x,y) screen coordinates relative to this panel</returns>
		private Point ConvertCoordsRect(Point p)
		{
			int x = p.X;
			int y = p.Y;
			
			return new Point(origin.X+offX+((PckImage.Width/2)*(x-y)),origin.Y+offY+(x+y));
		}
	}
}
