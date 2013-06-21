using System;
using System.IO;
using XCom.Interfaces.Base;

namespace XCom.Interfaces
{
	public class IXCTileset : ITileset
	{
		protected Palette myPal;
		protected string rootPath, rmpPath, blankPath;
		protected string[] groundMaps;
		protected bool underwater, baseStyle;
		protected int mapDepth;
		protected string scanFile, loftFile;
		protected MapSize mapSize;

		protected IXCTileset(string name)
			: base(name)
		{
			myPal = GameInfo.DefaultPalette;
			mapSize = new MapSize(60, 60, 4);
			mapDepth = 0;
			underwater = true;
			baseStyle = false;
		}

		protected IXCTileset(string name, StreamReader sr, VarCollection vars)
			: this(name)
		{
			while (sr.Peek() != -1)
			{
				string line = VarCollection.ReadLine(sr, vars);

				if (line == "end" || line == "END")
					return;

				int idx = line.IndexOf(':');

				string keyword = line.Substring(0, idx);
				string keywordLow = keyword.ToLower();

				string rest = line.Substring(idx + 1);

				switch (keywordLow)
				{
					case "palette":
						if (rest.ToLower() == "ufo")
							myPal = Palette.UFOBattle;
						else if (rest.ToLower() == "tftd")
							myPal = Palette.TFTDBattle;
						else
							myPal = Palette.GetPalette(rest);
						break;
					case "dll":
						string dllName = rest.Substring(rest.LastIndexOf(@"\") + 1);
						Console.WriteLine(name + " is in dll " + dllName);
						break;
					case "rootpath":
						rootPath = @rest;
						break;
					case "rmppath":
						rmpPath = @rest;
						break;
					case "basestyle":
						baseStyle = true;
						break;
					case "ground":
						groundMaps = rest.Split(' ');
						break;
					case "size":
						string[] dim = rest.Split(',');
						int rows = int.Parse(dim[0]);
						int cols = int.Parse(dim[1]);
						int height = int.Parse(dim[2]);

						mapSize = new MapSize(rows, cols, height);
						break;
					case "landmap":
						underwater = false;
						break;
					case "depth":
						mapDepth = int.Parse(rest);
						break;
					case "blankpath":
						blankPath = @rest;
						break;
					case "scang":
						scanFile = @rest;
						break;
					case "loftemp":
						loftFile = @rest;
						break;
					default:
						{
							//user-defined keyword
							ParseLine(keywordLow, rest, sr, vars);
							break;
						}
				}
			}
		}

		public MapSize Size { get { return mapSize; } }
		public virtual void Save(StreamWriter sw, VarCollection vars) { }

		public bool Underwater { get { return underwater; } }
		public string MapPath { get { return rootPath; } set { rootPath = value; } }
		public string RmpPath { get { return rmpPath; } set { rmpPath = value; } }
		public string BlankPath { get { return blankPath; } set { blankPath = value; } }
		public Palette Palette { get { return myPal; } set { myPal = value; } }
		public int Depth { get { return mapDepth; } }
		public string[] Ground { get { return groundMaps; } }
		public bool BaseStyle { get { return baseStyle; } }

		public virtual void ParseLine(string keyword, string line, StreamReader sr, VarCollection vars) { }
		public virtual void AddMap(string name, string subset) { }
		public virtual void AddMap(XCMapDesc imd, string subset) { }
		public virtual XCMapDesc RemoveMap(string name, string subset) { return null; }
	}
}
