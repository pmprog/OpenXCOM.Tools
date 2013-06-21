using System;
using System.IO;
using System.Drawing;
using System.Collections;
using XCom.Interfaces;

namespace XCom
{
	public class SPKImage:XCImage
	{
		public SPKImage(Palette p, Stream s, int width, int height)
		{
			//int transparent=254;	
			idx = new byte[width*height];
			for(int i=0;i<idx.Length;i++)
				idx[i]=254;

			long pix=0;

			BinaryReader data = new BinaryReader(s);

			try
			{
				while(data.BaseStream.Position < data.BaseStream.Length)
				{
					int cas = data.ReadUInt16();
					switch(cas)
					{
						case 0xFFFF:
						{
							long val = data.ReadUInt16()*2;
							pix+=val;
							break;
						}
						case 0xFFFE:
						{
							long val = data.ReadUInt16()*2;
							while((val--)>0)
							{							
								idx[pix++] = data.ReadByte();							
							}
							break;
						}
						case 0xFFFD:
						{
							image = Bmp.MakeBitmap8(width,height,idx,p.Colors);
							Palette=p;
							data.Close();
							return;
						}
					}
				}
			}
			catch{}

			image = Bmp.MakeBitmap8(width,height,idx,p.Colors);
			Palette=p;
			data.Close();
		}

		public static void Save(byte[] img, Stream file)
		{
			byte transparent=254;
			//Console.WriteLine("length: "+img.Length);
			BinaryWriter data = new BinaryWriter(file);

			ArrayList toWrite = new ArrayList();
			int count=0;

			for(int i=0;i<img.Length;i++)
			{
				if(img[i]==transparent)
				{
					if(toWrite.Count!=0)
					{
						if(toWrite.Count%2 ==1) //odd number of items in the list
						{
							toWrite.Add(img[i]); //add transparent index to make it even
							count--; //dont want this index to count
						}

						data.Write((ushort)0xFFFE);
						data.Write((ushort)(toWrite.Count/2));

						foreach(byte b in toWrite)
							data.Write((byte)b);
						toWrite = new ArrayList();
					}
					count++;
				}
				else
				{
					if(count!=0)
					{
						if(count%2!=0)
						{
							toWrite.Add(transparent); //add to list to make the count even
							count--; //dont need to do this, but owell
						}
						data.Write((ushort)0xFFFF);
						data.Write((ushort)(count/2));
						count=0;
					}
					toWrite.Add(img[i]);
				}
			}

			data.Write((ushort)0xFFFE);
			data.Write((ushort)(toWrite.Count/2));
			foreach(byte b in toWrite)
				data.Write((byte)b);

			data.Write((ushort)0xFFFD);
			data.Flush();
			data.Close();
		}
	}
}
