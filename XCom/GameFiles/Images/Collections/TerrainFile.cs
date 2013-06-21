/*using System;
using System.IO;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using XCom.Interfaces;

namespace XCom
{
	public class TerrainFile:IxCollection
	{
		public TerrainFile(Stream s,Palette pal)
		{
			BinaryReader sr = new BinaryReader(s);
			images = new ArrayList();
			int i=0;
			while(sr.BaseStream.Position<sr.BaseStream.Length)
				images.Add(new TerrainEntry(sr.ReadBytes(1024),pal,i++));
		}

		private TerrainFile(ArrayList images)
		{
			this.images = images;
			name="newTerrain";
			path="C:\\";
		}

		public static void Save(string directory, string file, IxCollection images)
		{
			BinaryWriter bw = new BinaryWriter(File.Create(directory+"\\"+file+".dat"));
			foreach(ITile te in images)
				bw.Write(te.Bytes);
			bw.Flush();
			bw.Close();			
		}

		public static ITile FromBmpSingle(Bitmap src,int num,Palette pal)
		{
			return new TerrainEntry(src,num,pal,0,0);
		}
	}
}
*/