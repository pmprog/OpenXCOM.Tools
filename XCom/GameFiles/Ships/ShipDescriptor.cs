using System;
using System.IO;
using System.Collections.Generic;
using XCom.Interfaces;
using XCom.Interfaces.Base;

namespace XCom
{
	public class ShipDescriptor:IXCMapData
	{
		private bool alienShip,useTanks;
		private MapLocation startLocUL;
		private IUnit[] units;

		public bool AlienShip{get{return alienShip;}}
		public bool UseTanks{get{return useTanks;}}
		public MapLocation StartLocUL{get{return startLocUL;}}

		public XCMapFile GetShip()
		{
			ImageInfo images = GameInfo.ImageInfo;

			List<ITile> a = new List<ITile>();
			foreach(string s in dependencies)
			{
				McdFile mcd = images[s].GetMcdFile();
				foreach(XCTile t in mcd)
					a.Add(t);
			}

			XCMapFile ship = new XCMapFile(basename,basePath,blankPath,a,dependencies);

			return ship;
		}

		public IUnit[] Units
		{
			get{return units;}
			set{units=value;}
		}

		public ShipDescriptor(string name, StreamReader sr,VarCollection vars):base(null,null,null,null,null,null,null)
		{
			string line="",keyword="",rest="";
			alienShip=true;
			useTanks=true;
			startLocUL=new MapLocation(0,0,0);

			while((line=vars.ReadLine(sr))!=null)
			{
				if(line=="end" || line=="END")
					return;

				int idx = line.IndexOf(':');
				keyword = line.Substring(0,idx).ToLower();
				rest = line.Substring(idx+1);

				switch(keyword)
				{
					case "path":
						basePath=@rest;
						break;
					case "blankpath":
						blankPath=@rest;
						break;
					case "rmppath":
						rmpPath=@rest;
						break;
					case "file":
						basename = rest;
						break;
					case "dependencies":
						dependencies = rest.Split(' ');
						break;
					case "xcomship":
						alienShip=false;
						break;
					case "notanks":
						useTanks=false;
						break;
					default:
						Console.WriteLine("Unknown line in ship: {0} -> {1}",name,line);
						break;
				}
			}
		}
	}
}
