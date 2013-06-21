/*using System;
using System.IO;
using System.Drawing;

namespace XCom.Interfaces
{
	public abstract class IUnitDescriptor
	{		
		protected string basename,basePath,unitName;
		protected int ground;
		protected int[] multiGround;
		protected IUnitFile myFile;
		protected Palette defPal=Palette.TFTDBattle;

		protected IUnitDescriptor(string name, StreamReader sr,VarCollection vars)
		{
			unitName=name;

			string line;
			while((line=vars.ReadLine(sr))!=null)
			{				
				if(line=="end")
					return;
				int idx = line.IndexOf(':');
				string keyword = line.Substring(0,idx).ToLower();
				string rest = line.Substring(idx+1);

				switch(keyword.ToLower())
				{
					case "path":
						basePath = @rest;
						break;
					case "file":
						basename = rest;
						break;
					case "ground":
						ground = int.Parse(rest);
						break;
					case "multiground":
						int g = int.Parse(rest);
						multiGround = new int[]{g,g+1,g+2,g+3};
						break;
					case "palette":
						if(rest.ToLower()=="ufo")
							defPal = Palette.UFOBattle;
						else
							defPal=Palette.TFTDBattle;
						break;
					default:
						ParseLine(keyword,rest,sr,vars);
						break;
				}
			}
		}

		public PckFile GetPckFile()
		{
			return GetPckFile(defPal);
		}

		public PckFile GetPckFile(Palette p)
		{
			try
			{
				return GameInfo.CachePck(basePath,basename,4,p);
			}
			catch
			{
				return GameInfo.CachePck(basePath,basename,2,p);
			}
		}

		public string Name
		{
			get{return unitName;}
		}

		public string Basename
		{
			get{return basename;}
		}

		public string BasePath
		{
			get{return basePath;}
		}

		public int Ground
		{
			get{return ground;}
			set{ground=value;}
		}

		public int[] MultiGround
		{
			get{return multiGround;}
			set{multiGround=value;}
		}

		protected abstract void ParseLine(string keyword, string rest, StreamReader sr, VarCollection vars);
		public abstract IUnit GetNewUnit(Palette p);

		public IUnit GetNewUnit()
		{
			return GetNewUnit(GameInfo.DefaultPalette);
		}
	}
}
*/