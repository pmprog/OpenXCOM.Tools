using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using XCom.Interfaces;
using System.Reflection;
using XCom.Interfaces.Base;

namespace XCom 
{
	public delegate void ParseLineDelegate(KeyVal kv,VarCollection vars);

	public class GameInfo
	{
		private static Palette currentPalette = Palette.TFTDBattle;

		private static ImageInfo imageInfo;
		private static TilesetDesc tileInfo;

		private static Dictionary<Palette,Dictionary<string,PckFile>> pckHash;

		public static event ParseLineDelegate ParseLine;

		public static void Init(Palette p, DSShared.PathInfo paths)
		{
			currentPalette = p;
			pckHash = new Dictionary<Palette, Dictionary<string, PckFile>>();		

			VarCollection vars = new VarCollection(new StreamReader(File.OpenRead(paths.ToString())));

			Directory.SetCurrentDirectory(paths.Path);

			xConsole.Init(20);
			KeyVal kv = null;

			while((kv=vars.ReadLine())!=null)
			{
				switch (kv.Keyword)
				{					
		/* mapedit */case "mapdata":
						tileInfo = new TilesetDesc(kv.Rest, vars);
						break;
		/* mapedit */case "images": 
						imageInfo = new ImageInfo(kv.Rest, vars); 
						break;
					case "useBlanks":
						Globals.UseBlanks = bool.Parse(kv.Rest);
						break;
					default:
						if (ParseLine != null)
							ParseLine(kv, vars);
						else
							xConsole.AddLine("Error in paths file: " + kv);
						break;
				}
			}

			vars.BaseStream.Close();
		}

		public static ImageInfo ImageInfo
		{
			get{return imageInfo;}
		}

		public static TilesetDesc TilesetInfo
		{
			get { return tileInfo; }
		}

		public static Palette DefaultPalette
		{
			get{return currentPalette;}
			set{currentPalette=value;}
		}

		public static PckFile GetPckFile(string imageSet, Palette p)
		{
			return imageInfo.Images[imageSet].GetPckFile(p);
		}

		public static PckFile GetPckFile(string imageSet)
		{
			return GetPckFile(imageSet,currentPalette);
		}		

		public static McdFile GetMcdFile(string imageSet)
		{
			return imageInfo.Images[imageSet].GetMcdFile(currentPalette);
		}

		public static McdFile GetMcdFile(string imageSet,Palette p)
		{
			return imageInfo.Images[imageSet].GetMcdFile(p);
		}

		public static XCMapFile GetMap(string tileset, string file)
		{
			return (XCMapFile)tileInfo.Tilesets[tileset][file].GetMapFile();
		}

		public static PckFile CachePck(string basePath,string basename,int bpp, Palette p)		
		{
			if (pckHash == null)
				pckHash = new Dictionary<Palette, Dictionary<string, PckFile>>();

			if (!pckHash.ContainsKey(p))
				pckHash.Add(p, new Dictionary<string, PckFile>());

			//if(pckHash[p][basePath+basename]==null)
			if(!pckHash[p].ContainsKey(basePath+basename))
				pckHash[p].Add(basePath+basename,new PckFile(File.OpenRead(basePath+basename+".PCK"),File.OpenRead(basePath+basename+".TAB"),bpp,p));

			return pckHash[p][basePath+basename];
		}
	}
}