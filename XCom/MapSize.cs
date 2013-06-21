using System;
using System.Collections.Generic;
using System.Text;

namespace XCom
{
	public struct MapSize
	{
		public int Rows, Cols, Height;

		public MapSize(int rows, int cols, int height)
		{
			Rows = rows;
			Cols = cols;
			Height = height;
		}

		public override string ToString()
		{
			return Rows + "," + Cols + "," + Height;
		}
	}
}
