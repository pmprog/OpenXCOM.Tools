using System;

namespace XCom
{
	public abstract class ItemDescriptor
	{
		protected int numHands;
		protected int handIndex=0,groundIndex=0;
		protected string name;

		//public static string debug = "";

		public ItemDescriptor(string name, VarCollection vars)
		{
			this.name=name;

			KeyVal line=null;

			while((line=vars.ReadLine()).Keyword.ToLower()!="end")
			{
				switch(line.Keyword.ToLower())
				{
					case "hand":
						handIndex = int.Parse(line.Rest);
						break;
					case "ground":
						groundIndex = int.Parse(line.Rest);
						break;
					case "numhands":
						numHands = int.Parse(line.Rest);
						break;
					default:
						ParseLine(line,vars);
						break;
				}
			}		
		}

		public string Name
		{
			get{return name;}
		}

		public int GroundIndex
		{
			get{return groundIndex;}
		}

		public int HandIndex
		{
			get{return handIndex;}
		}

		public int NumHands
		{
			get{return numHands;}
		}

		public abstract void ParseLine(KeyVal line, VarCollection vars);
	}
}
