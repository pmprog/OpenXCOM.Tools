using System;

namespace XCom
{
	/// <summary>
	/// Struct for defining map locations
	/// </summary>
	public struct MapLocation
	{
		public int Row, Col, Height;

		public MapLocation(int row, int col, int height)
		{
			this.Row=row;
			this.Col=col;
			this.Height=height;
		}
	}
}