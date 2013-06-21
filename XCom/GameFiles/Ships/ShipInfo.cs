using System;
using System.Collections;
using System.IO;

namespace XCom
{
	/// <summary>
	/// Summary description for ShipInfo.
	/// </summary>
	public class ShipInfo
	{
		private Hashtable ships;
		public ShipInfo()
		{
			ships = new Hashtable();
		}

		public Hashtable Ships
		{
			get{return ships;}
		}

		public ShipDescriptor this[string name]
		{
			get{return (ShipDescriptor)ships[name];}
		}

		public ShipInfo(Stream file,VarCollection v):this()
		{
			StreamReader sr = new StreamReader(file);
			//string line="",keyword="",rest="";
			VarCollection vars = new VarCollection(sr,v);

			KeyVal kv = null;
			while((kv = vars.ReadLine())!=null)
			{
				if(kv.Keyword=="end")
					continue;

//				int idx = line.IndexOf(':');
//				keyword = line.Substring(0,idx);
//				rest = line.Substring(idx+1);
				switch(kv.Keyword)
				{
					case "ship":
						ships[kv.Rest] = new ShipDescriptor(kv.Rest,sr,vars);
						break;
					default:
						xConsole.AddLine("Unknown line in ship: "+kv);
						break;
				}
			}

			sr.Close();
		}
	}
}
