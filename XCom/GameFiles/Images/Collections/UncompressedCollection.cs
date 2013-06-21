using System;
using XCom.Interfaces;

namespace XCom
{
	public class UncompressedCollection:XCImageCollection
	{
		public UncompressedCollection(int imgWid, int imgHei, System.IO.Stream inFile, Palette pal)
		{
			System.IO.BinaryReader sr = new System.IO.BinaryReader(inFile);
			int i=0;
			while(sr.BaseStream.Position<sr.BaseStream.Length)
				Add(new XCImage(sr.ReadBytes(imgWid*imgHei),imgWid,imgHei,pal,i++));
		}

		public static void Save(string directory, string file,string ext, XCImageCollection images)
		{
			System.IO.BinaryWriter bw = new System.IO.BinaryWriter(System.IO.File.Create(directory+"\\"+file+ext));
			foreach(XCImage tile in images)
				bw.Write(tile.Bytes);
			bw.Flush();
			bw.Close();
		}
	}
}
