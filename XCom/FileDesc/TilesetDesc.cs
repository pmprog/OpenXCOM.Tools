using System;
//using System.Collections;
using System.Collections.Generic;
using System.IO;
using XCom.Interfaces;
using XCom.Interfaces.Base;

namespace XCom
{
	public class TilesetDesc:FileDesc
	{
		private Dictionary<string, ITileset> tilesets;
		private double version;

		public TilesetDesc():base("")
		{
			tilesets = new Dictionary<string, ITileset>();
		}

		public TilesetDesc(string inFile,VarCollection v):base(inFile)
		{
			tilesets = new Dictionary<string, ITileset>();
			StreamReader sr = new StreamReader(File.OpenRead(inFile));
			string line="",keyword="",name="";
			VarCollection vars = new VarCollection(sr,v);

			while((line = vars.ReadLine(sr))!=null)
			{
				int idx = line.IndexOf(':');
				keyword = line.Substring(0,idx);
				string keywordLow = keyword.ToLower();
				name = line.Substring(idx+1);
				switch(keywordLow)
				{
					case "tileset":
						line = VarCollection.ReadLine(sr,vars);
						idx = line.IndexOf(':');
						keyword = line.Substring(0,idx).ToLower();
						string rest = line.Substring(idx+1);
						switch(keyword)
						{
							case "type":
								int type = int.Parse(rest);
								switch(type)
								{
									//case 0:
									//	tilesets[name] = new Type0Tileset(name,sr,new VarCollection(vars));
									//	break;
									case 1:
										tilesets[name] = new XCTileset(name,sr,new VarCollection(vars));
										break;
								}
								break;
							default:
								Console.WriteLine("Type line not found: "+line);
								break;
						}
						
						break;
					case "version":
						version = double.Parse(name);
						break;
					default:
							Console.WriteLine("Unknown line: "+line);
						break;
				}
			}
			sr.Close();
		}

		public ITileset AddTileset(string name, string mapPath, string rmpPath, string blankPath)
		{
			IXCTileset tSet = new XCTileset(name);
			tSet.MapPath=mapPath;
			tSet.RmpPath=rmpPath;
			tSet.BlankPath=blankPath;
			tilesets[name] = tSet;
			return tSet;
		}

		public Dictionary<string, ITileset> Tilesets
		{
			get{return tilesets;}
		}

		public override void Save(string outFile)
		{
			//iterate thru each tileset, call save on them
			VarCollection vc = new VarCollection("Path");
			StreamWriter sw = new StreamWriter(outFile);

			foreach(string s in tilesets.Keys)
			{
				IXCTileset ts = (IXCTileset)tilesets[s];
				if(ts==null)
					continue;
				vc.AddVar("rootPath",ts.MapPath);
				vc.AddVar("rmpPath",ts.RmpPath);
				vc.AddVar("blankPath",ts.BlankPath);
			}

			foreach(string v in vc.Variables)
			{
				Variable var = (Variable)vc.Vars[v];
				sw.WriteLine(var.Name+":"+var.Value);
			}

			foreach(string s in tilesets.Keys)
			{
				if(tilesets[s]==null)
					continue;

				((IXCTileset)tilesets[s]).Save(sw,vc);
			}

      sw.Close();
		}
	}
}
