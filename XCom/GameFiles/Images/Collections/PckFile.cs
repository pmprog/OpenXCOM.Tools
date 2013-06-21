using System;
using System.Collections;
using System.IO;
using System.Drawing;
using XCom.Interfaces;

namespace XCom
{
	public delegate void LoadingDelegate(int curr, int total);

	public class PckFile:XCImageCollection
	{
		private int bpp;
		public static readonly string TAB_EXT = ".tab";

		public PckFile(Stream pckFile, Stream tabFile,int bpp,Palette pal,int imgHeight,int imgWidth)
		{
			if(tabFile!=null)
				tabFile.Position=0;

			pckFile.Position=0;

			byte[] info = new byte[pckFile.Length];
			pckFile.Read(info,0,info.Length);
			pckFile.Close();

			this.bpp=bpp;

			Pal=pal;

			uint[] offsets;
			
			if(tabFile!=null)
			{
				offsets= new uint[(tabFile.Length/bpp)+1];
				BinaryReader br = new BinaryReader(tabFile);

				if(bpp==2)
					for(int i=0;i<tabFile.Length/bpp;i++)
						offsets[i] = br.ReadUInt16();
				else
					for(int i=0;i<tabFile.Length/bpp;i++)
						offsets[i] = br.ReadUInt32();
				br.Close();
			}
			else
			{
				offsets = new uint[2];
				offsets[0]=0;
			}

			offsets[offsets.Length-1] = (uint)info.Length;

			for(int i=0;i<offsets.Length-1;i++)
			{
				byte[] imgDat = new byte[offsets[i+1]-offsets[i]];
				for(int j=0;j<imgDat.Length;j++)			
					imgDat[j] = info[offsets[i]+j];

				Add(new PckImage(i,imgDat,pal,this,imgHeight,imgWidth));
			}

			pckFile.Close();
			if(tabFile!=null)
				tabFile.Close();
		}

		public PckFile(Stream pckFile, Stream tabFile,int bpp,Palette pal):this(pckFile,tabFile,bpp,pal,40,32)
		{}

		public int Bpp
		{
			get{return bpp;}
		}

		public static void Save(string directory, string file, XCImageCollection images, int bpp)
		{
			System.IO.BinaryWriter pck = new System.IO.BinaryWriter(System.IO.File.Create(directory+"\\"+file+".pck"));
			System.IO.BinaryWriter tab = new System.IO.BinaryWriter(System.IO.File.Create(directory+"\\"+file+TAB_EXT));

			if(bpp==2)
			{
				ushort count=0;
				foreach(XCImage img in images)
				{
					tab.Write((ushort)count);
					ushort encLen = (ushort)PckImage.EncodePck(pck,img);
					count+=encLen;
				}
			}
			else
			{
				uint count=0;
				foreach(XCImage img in images)
				{
					tab.Write((uint)count);
					uint encLen = (uint)PckImage.EncodePck(pck,img);
					count+=encLen;
				}
			}

			pck.Close();
			tab.Close();
		}
	}								 
}