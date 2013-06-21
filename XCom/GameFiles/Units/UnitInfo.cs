using System;
using System.Collections;
using System.IO;
//using SDLDotNet;
using XCom.Interfaces;

namespace XCom
{
	public enum Direction{North=0,NorthEast,East,SouthEast,South,SouthWest,West,NorthWest};

	public class UnitInfo
	{
		public static readonly int[] BounceFactor = new int[]{2,1,0,1,2,1,0,1};

		private Hashtable units;
		private string groundImages;

		public UnitInfo()
		{
			units = new Hashtable();
		}

		public Hashtable Units
		{
			get{return units;}
		}

		public IUnitDescriptor this[object o]
		{
			get{return (IUnitDescriptor)units[o];}
			set{units[o]=value;}
		}

		public UnitInfo(Stream file,VarCollection v):this() 
		{
			StreamReader sr = new StreamReader(file);
			VarCollection vars = new VarCollection(sr,v);
			KeyVal line=vars.ReadLine();

			if(line==null)
				return;

			groundImages = line.Rest;

			while((line=vars.ReadLine())!=null)
			{				
				if(line.Keyword=="unit")
				{
					string name = line.Rest;
					line = vars.ReadLine();

					if(line.Keyword=="type")
					{
						int type = int.Parse(line.Rest);
						switch(type)
						{
							case 0:
								units[name] = new Type0Descriptor(name,sr,vars);
								break;
							case 1: 
								units[name] = new Type1Descriptor(name,sr,vars);
								break;
							case 2:
								units[name] = new Type2Descriptor(name,sr,vars);
								break;
							case 3:
								units[name] = new Type3Descriptor(name,sr,vars);
								break;
							case 4:
								units[name] = new Type4Descriptor(name,sr,vars);
								break;
							case 5:
								units[name] = new Type5Descriptor(name,sr,vars);
								break;
							case 6:
								units[name] = new Type6Descriptor(name,sr,vars);
								break;
							case 7:
								units[name] = new Type7Descriptor(name,sr,vars);
								break;
						}
					}
				}
				else
				{
					xConsole.AddLine("Unknown keyword parsing unit file(1): "+line.Keyword+"->"+line.Rest);
				}
			}
			sr.Close();
		}
	}
}
