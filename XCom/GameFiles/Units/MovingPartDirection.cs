using System;

namespace XCom
{
	public class MovingPartDirection
	{
		private int[,] images;
		public MovingPartDirection(int[,] images)
		{
			this.images=images;
		}

		public int this[Direction d,int frame]
		{
			get{return images[(int)d,frame];}
		}
	}
}
