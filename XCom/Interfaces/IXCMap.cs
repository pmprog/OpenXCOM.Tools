//using System;
//using XCom.Interfaces.Base;

//namespace XCom.Interfaces
//{
//    public abstract class IXCMap : XCom.Interfaces.Base.IMap
//    {
//        public IXCMap(ITileset map) : base(map) { }

		//public virtual void Die(int row, int col, int height, TileLocation loc)
		//{
		//    XCMapTile sel = ((XCMapTile)this[row, col, height]);

		//    switch (loc)
		//    {
		//        case TileLocation.North:
		//            if (sel.North != null)
		//                sel.North = ((XCTile)sel.North).Dead;
		//            break;
		//        case TileLocation.West:
		//            if (sel.West != null)
		//                sel.West = ((XCTile)sel.West).Dead;
		//            break;
		//        case TileLocation.Content:
		//            if (sel.Content != null)
		//                sel.Content = ((XCTile)sel.Content).Dead;
		//            break;
		//        case TileLocation.Ground:
		//            if (sel.Ground != null)
		//                sel.Ground = ((XCTile)sel.Ground).Dead;
		//            break;
		//    }
		//}
//    }
//}
