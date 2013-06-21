using System;
using XCom;

namespace MapView
{
	public enum ArgType{NewMap,MapClicked};

	public class Args:EventArgs
	{
		private ArgType type;
		private MapLocation location;

		public Args(ArgType type)
		{
			this.type=type;
		}

		public ArgType Type
		{
			get{return type;}
		}
		
		public MapLocation Location
		{
			get{return location;}
			set{location=value;}
		}
	}
}