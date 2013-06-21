using System;
using System.IO;
using System.Collections;
//using SDLDotNet;

namespace XCom
{
	public class McdFile//:IEnumerable
	{
		private XCTile[] tiles;

//		internal McdFile(string basename, string directory)
//		{
//			BufferedStream file = new BufferedStream(File.OpenRead(directory+basename+".MCD"));
//			int diff = 0;
//			if(basename == "XBASES05")
//				diff=3;
//			tiles = new Tile[(file.Length/62)-diff];
//			PckFile f = GameInfo.GetPckFile(basename,directory,2);
//			for(int i=0;i<tiles.Length;i++)
//			{
//				byte[] info = new byte[62];
//				file.Read(info,0,62);
//				tiles[i] = new Tile(i,f,new McdEntry(info),this);
//			}
//
//			foreach(Tile t in tiles)
//				t.Tiles = tiles;
//			file.Close();
//		}

		internal McdFile(string basename, string directory, PckFile f)
		{
			BufferedStream file = new BufferedStream(File.OpenRead(directory+basename+".MCD"));
			int diff = 0;
			if(basename == "XBASES05")
				diff=3;
			tiles = new XCTile[(((int)file.Length)/62)-diff];
	
			for(int i=0;i<tiles.Length;i++)
			{
				byte[] info = new byte[62];
				file.Read(info,0,62); 
				tiles[i] = new XCTile(i,f,new McdEntry(info),this);
			}

			foreach(XCTile t in tiles)
				t.Tiles = tiles;
			file.Close();
		}

		public IEnumerator GetEnumerator()
		{
			return tiles.GetEnumerator();
		}

		public XCTile this[int i]
		{
			get{return tiles[i];}
		}

		public int Length
		{
			get{return tiles.Length;}
		}
	}
}
