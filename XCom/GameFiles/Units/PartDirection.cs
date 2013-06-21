using System;

namespace XCom
{
	public class PartDirection
	{
		private int[] images;
		public PartDirection(int[] images)
		{
			this.images=images;
		}

		public int this[Direction d]
		{
			get{return images[(int)d];}
		}
	}
}
