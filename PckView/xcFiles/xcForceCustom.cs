using System;

namespace PckView
{
	public class xcForceCustom:xcCustom
	{
		public xcForceCustom():base(0,0)
		{
			fileOptions.Init(false, false, true, false);
			//fileOptions[Filter.Bmp]=false;
			//fileOptions[Filter.Save]=false;
			//fileOptions[Filter.Open]=true;
			//fileOptions[Filter.Custom]=false;

			ext = ".*";
			author = "Ben Ratzlaff";
			desc = "Forces the Custom File Dialog box to be shown";

			expDesc = "Custom File";
		}

		public override string SingleFileName{get{return "*.*";}}
	}
}
