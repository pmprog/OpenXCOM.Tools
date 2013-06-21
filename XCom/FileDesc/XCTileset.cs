using System;
using System.Collections.Generic;
using System.IO;
using XCom.Interfaces;
using System.Reflection;
using XCom.Interfaces.Base;

namespace XCom
{
	public class XCTileset:IXCTileset
	{
		private string[] mapOrder;
		private MapLocation[] startLoc;
		private int startTile=-1;
		private int endTile=-1;

		public XCTileset(string name):base(name)
		{
		}

		public XCTileset(string name, StreamReader sr,VarCollection vars):base(name,sr,vars)
		{
		}

		public MapLocation[] StartLocations{get{return startLoc;}}
		public int EndTile{get{return endTile;}}
		public int StartTile{get{return startTile;}}
		public string[] MapOrder{get{return mapOrder;}}
		public string[] Order{get{return mapOrder;}}

		//public override IMap GetMap(ShipDescriptor xCom,ShipDescriptor alien){return new Type1Map(this,xCom,alien);}	

		public override void Save(StreamWriter sw, VarCollection vars)
		{
			sw.WriteLine("Tileset:" + name);
			sw.WriteLine("\ttype:1");

			if (vars.Vars[rootPath] == null)
				sw.WriteLine("\trootpath:" + rootPath);
			else
				sw.WriteLine("\trootpath:" + ((Variable)vars.Vars[rootPath]).Name);

			if (vars.Vars[rmpPath] == null)
				sw.WriteLine("\trmpPath:" + rootPath);
			else
				sw.WriteLine("\trmpPath:" + ((Variable)vars.Vars[rmpPath]).Name);

			if (vars.Vars[blankPath] == null)
				sw.WriteLine("\tblankPath:" + blankPath);
			else
				sw.WriteLine("\tblankPath:" + ((Variable)vars.Vars[blankPath]).Name);

			sw.WriteLine("\tpalette:" + myPal.Name);

			foreach (string str in subsets.Keys)
			{
				Dictionary<string, IMapDesc> h = subsets[str];
				if (h == null)
					continue;

				VarCollection vc = new VarCollection("Deps");
				foreach (string s in h.Keys)
				{
					XCMapDesc id = (XCMapDesc)maps[s];
					if (id == null)
						continue;

					string depList = "";
					if (id.Dependencies.Length > 0)
					{
						int i = 0;
						for (; i < id.Dependencies.Length - 1; i++)
							depList += id.Dependencies[i] + " ";
						depList += id.Dependencies[i];
					}

					vc.AddVar(id.Name, depList);
				}

				sw.WriteLine("\tfiles:" + str);

				foreach (string vKey in vc.Variables)
					((Variable)vc.Vars[vKey]).Write(sw, "\t\t");

				sw.WriteLine("\tend");
			}

			sw.WriteLine("end\n");

			sw.Flush();
		}

		public override void AddMap(string fName,string subset)
		{
			XCMapDesc imd = new XCMapDesc(fName,rootPath,blankPath,rmpPath,new string[0],myPal); 
			maps[imd.Name] =imd;
			subsets[subset][imd.Name]=imd;
		}

		public override void AddMap(XCMapDesc imd, string subset)
		{
			maps[imd.Name] =imd;
			subsets[subset][imd.Name]=imd;
		}

		public override XCMapDesc RemoveMap(string fName,string subset)
		{
			XCMapDesc imd = (XCMapDesc)subsets[subset][fName];
			subsets[subset].Remove(fName);
			return imd;
		}

		public override void ParseLine(string keyword, string rest,StreamReader sr,VarCollection vars)
		{
			switch(keyword)
			{
				case "files":
				{
					Dictionary<string, IMapDesc> subset = new Dictionary<string, IMapDesc>();
					subsets[rest]=subset;
					string line = VarCollection.ReadLine(sr,vars);
					while(line!="end" && line!="END")
					{
						int idx = line.IndexOf(':');
						string fName = line.Substring(0,idx);
						string[] deps = line.Substring(idx+1).Split(' ');
						XCMapDesc imd = new XCMapDesc(fName, rootPath, blankPath, rmpPath, deps, myPal);
						maps[fName] =imd;
						subset[fName]=imd;
						line = VarCollection.ReadLine(sr,vars);
					}
				}
					break;
				case "order":
					mapOrder = rest.Split(' ');
					break;
				case "starttile":
					startTile = int.Parse(rest);
					break;
				case "startloc":
					string[] locs = rest.Split(' ');
					startLoc = new MapLocation[locs.Length];
					for(int i=0;i<locs.Length;i++)
					{
						string[] loc = locs[i].Split(',');
						int r = int.Parse(loc[0]);
						int c = int.Parse(loc[1]);
						int h = int.Parse(loc[2]);
						startLoc[i] = new MapLocation(r,c,h);
					}
					break;
				case "endtile":
					endTile = int.Parse(rest);
					break;
				default:
					xConsole.AddLine(string.Format("Unknown line in tileset {0}-> {1}:{2}",name,keyword,rest));
					break;
			}
		}
	}
}
