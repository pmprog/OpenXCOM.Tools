//using System;
//using System.Drawing;
//using System.Collections.Generic;
//using XCom.Interfaces.Base;

//namespace XCom
//{
//    public enum TileLocation{North,West,Content,Ground};	

//    public class IMap:IMap_Base
//    {
//        protected int spots,spotsFull;
//        protected Rectangle boundingRectangle;
//        protected ITileset mySet;
//        protected bool baseStyle=false;

//        //protected IUnit[] units;		

//        protected List<string> dependencies;
//        protected IMap_Base xComShip;
//        protected IMap_Base alienShip;

//        protected IMap(ITileset map,MapSize mapSize):base("",null)
//        {
//            xConsole.AddLine("Loading tileset: "+map.Name);
//            mySet=map;
//            this.mapSize = mapSize;
//            int rows = mapSize.Rows;
//            int cols = mapSize.Cols;
//            int height = mapSize.Height;
//            spots = rows*cols;

//            dependencies= new List<string>();

//            boundingRectangle = new Rectangle(0,0,16*(rows+cols+2),8*(rows+cols+1)+(24*height));

//            mapData = new XCMapTile[rows*cols*height];
//            //indexArray = new IndexArray(rows,cols,height);

//            for(int i=0;i<mapData.Length;i++)
//                mapData[i] = XCMapTile.BlankTile;

//            currentHeight=0;
//        }

//        public IMap_Base XComShip
//        {
//            get{return xComShip;}
//        }

//        public IMap_Base AlienShip
//        {
//            get{return alienShip;}
//        }

//        /*public IUnit[] Units
//        {
//            get{return units;}
//            set{units=value;}
//        }*/

//        public int ScreenHeight{get{return boundingRectangle.Height;}}
//        public int ScreenWidth{get{return boundingRectangle.Width;}}

//        public ITileset Tileset{get{return mySet;}}

//        protected void addShip(IMap_Base ship)
//        {
//            /*foreach(string dep in ship.Dependencies)
//                usedImages.Add(dep);

//            Random gen = new Random();
//            for(int r=0;r<ship.Rows;r+=10)
//                for(int c=0;c<ship.Cols;c+=10)
//                {
//                    MapFile g = mySet[mySet.Ground[gen.Next()%mySet.Ground.Length]].GetMapFile();
//                    for(int row=0;row<10;row++)
//                        for(int col=0;col<10;col++)
//                            if(ship[r+row,c+col,ship.Height-1].Ground==null)
//                                ship[r+row,c+col,ship.Height-1].Ground = g[row,col,g.Height-1].Ground;
//                }
			
//            bool f=true;
//            while(f)
//            {
//                int r = ((gen.Next()%rows)/10)*10;
//                int c = ((gen.Next()%cols)/10)*10;
//                if(spaceFree(r,c,ship.Rows,ship.Cols))
//                {
//                    AddMap(ship,new Point3D(r,c,height-1));
//                    f=false;
//                }
//            }*/
//        }

//        /// <summary>
//        /// simple (and slow) visibility algorithm to compute some hidden tiles
//        /// </summary>
//        protected void calcDrawAbove()
//        {
//            for (int h = mapSize.Height - 1; h >= 0; h--)
//                for (int row = 0; row < mapSize.Rows - 1; row++)
//                {
//                    for (int col = 0; col < mapSize.Cols - 1; col++)
//                    {
//                        if(this[row,col,h]==null)
//                            continue;
						
//                        try
//                        {
//                            if(((XCMapTile)this[row,col,h-1]).Ground!=null && //top
//                                ((XCMapTile)this[row+1,col,h-1]).Ground!=null && //south
//                                ((XCMapTile)this[row+2,col,h-1]).Ground!=null &&
//                                ((XCMapTile)this[row+1,col+1,h-1]).Ground!=null && //southeast
//                                ((XCMapTile)this[row+2,col+1,h-1]).Ground!=null &&
//                                ((XCMapTile)this[row+2,col+2,h-1]).Ground!=null &&
//                                ((XCMapTile)this[row,col+1,h-1]).Ground!=null && //east
//                                ((XCMapTile)this[row,col+2,h-1]).Ground!=null &&
//                                ((XCMapTile)this[row+1,col+2,h-1]).Ground!=null)
//                                ((XCMapTile)this[row,col,h]).DrawAbove=false;
//                        }
//                        catch{}
//                    }
//                }
//        }


//        //method to determine if the area of tiles specified is empty
//        //it does this by checking every 10th tile
//        protected bool spaceFree(int row, int col, int numRows, int numCols)
//        {
//            int h = mapSize.Height - 1;
//            bool result=true;
//            int locR = row;
//            int locC = col;

//            if (row + numRows > mapSize.Rows || col + numCols > mapSize.Cols)
//                return false;

//            for(int cr = 0;cr<numRows&&result;cr+=10)
//                for(int cc=0;cc<numCols&&result;cc+=10)
//                    if(!((XCMapTile)this[locR+cr,locC+cc,h]).Blank)
//                        result=false;
//            return result;
//        }


//        //finds a blank area of the map that is the size of numR x numC
//        //the upper left tile will be 10x10 aligned
//        protected MapLocation findSpot(int numR, int numC)
//        {
//            int h = mapSize.Height - 1;
//            for (int r = 0; r < mapSize.Rows; r += 10)
//                for (int c = 0; c < mapSize.Cols; c += 10)
//                {
//                    if(((XCMapTile)this[r,c,h]).Blank)
//                    {
//                        if (r + numR - 1 < mapSize.Rows && c + numC - 1 < mapSize.Cols)
//                        {
//                            bool result=true;
//                            int locR = r;
//                            int locC = c;

//                            for(int cr = 0;cr<numR&&result;cr+=10)
//                                for(int cc=0;cc<numC&&result;cc+=10)
//                                    if(!((XCMapTile)this[locR+cr,locC+cc,h]).Blank)
//                                        result=false;
//                            if(result)
//                                return new MapLocation(r,c,h);
//                        }
//                    }
//                }
//            return new MapLocation(-1,-1,-1);
//        }

//        public List<string> Dependencies
//        {
//            get{return dependencies;}
//        }

//        //adds a mapfile to the specified location, will overwrite anything thats there
//        protected void AddMap(XCMapFile m,MapLocation location)
//        {
//            int locR=location.Row;
//            int locC=location.Col;

//            for (int h = this.mapSize.Height - 1, hm = m.MapSize.Height - 1; hm >= 0; hm--, h--)
//            {
//                for (int r = 0; r < m.MapSize.Rows; r++)
//                    for (int c = 0; c < m.MapSize.Cols; c++)
//                        this[locR+r,locC+c,h]=m[r,c,hm];
//            }
//            spotsFull += (m.MapSize.Rows * m.MapSize.Cols);

//            foreach(string s in m.Dependencies)
//                if(!dependencies.Contains(s))
//                    dependencies.Add(s);

//            /*
//            if(mySet.BaseStyle)
//            {
//                if (locR + m.Rows < mySet.Size.Rows) //clear out bottom row
//                {
//                    for (int h = this.mySet.Size.Height - 1; h >= 0; h--)
//                        for(int c=0;c<m.Cols;c+=10)
//                        {
//                            if(this[locR+m.Rows-1,locC+c+3,h].West!=null)
//                            {
//                                this[locR+m.Rows-1,locC+c+3,h].Content=null;
//                                this[locR+m.Rows-1,locC+c+4,h].Content=null;
//                                this[locR+m.Rows-1,locC+c+5,h].Content=null;
//                            }
//                        }
//                }

//                if (locC + m.Cols < this.mySet.Size.Cols)
//                {
//                    for (int h = this.mySet.Size.Height - 1; h >= 0; h--)
//                        for(int r=0;r<m.Rows;r+=10)
//                        {
//                            if(this[locR+r+3,locC+m.Cols-1,h].North!=null)
//                            {
//                                this[locR+r+3,locC+m.Cols-1,h].Content=null;
//                                this[locR+r+4,locC+m.Cols-1,h].Content=null;
//                                this[locR+r+5,locC+m.Cols-1,h].Content=null;
//                            }
//                        }
//                }

//                if(locR>0) 
//                {
//                    for (int h = this.mySet.Size.Height - 1; h >= 0; h--)
//                        for(int c=0;c<m.Cols;c+=10)
//                        {
//                            if(this[locR-1,locC+c+3,h].Content==null)
//                            {
//                                this[locR,locC+c+3,h].North=null;
//                                this[locR,locC+c+4,h].North=null;
//                                this[locR,locC+c+5,h].North=null;
//                            }
//                        }
//                }

//                if(locC>0)
//                {
//                    for (int h = this.mySet.Size.Height - 1; h >= 0; h--)
//                        for(int r=0;r<m.Rows;r+=10)
//                        {
//                            if(this[locR+r+3,locC-1,h].Content==null)
//                            {
//                                this[locR+r+3,locC,h].West=null;
//                                this[locR+r+4,locC,h].West=null;
//                                this[locR+r+5,locC,h].West=null;
//                            }
//                        }
//                }
//            }*/
//        }
//    }

//    //public class IndexArray
//    //{
//    //    private int[] idx;
//    //    private int size;

//    //    private int cols;

//    //    public IndexArray(int rows, int cols, int height)
//    //    {
//    //        idx = new int[rows*cols*height*4];
//    //        this.cols=cols*4;
//    //        size = rows*cols*4;
//    //    }

//    //    public int this[int row, int col, int height,int tile]
//    //    {
//    //        get{return idx[(size*height)+(row*cols)+col*4+tile];}
//    //        set{idx[(size*height)+(row*cols)+col*4+tile]=value;}
//    //    }
//    //}
//}
