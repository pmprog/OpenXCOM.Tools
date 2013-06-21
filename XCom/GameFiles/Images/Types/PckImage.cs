using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using XCom.Interfaces;

namespace XCom
{
//	[xImage(32,40)]
	public class PckImage:XCImage
	{
		private PckFile pckFile;
		private int mapID;		
		private byte[] expanded;
		private int moveIdx=-1;
		private byte moveVal=0;

		private int staticID;
		private static int globalStaticID=0;

		public static int Width=32;
		public static int Height=40;

		private static readonly byte TransIdx=0xFE;

		internal PckImage(int imageNum,byte[] idx,Palette pal,PckFile pFile):this(imageNum,idx,pal,pFile,40,32){}

		internal PckImage(int imageNum,byte[] idx,Palette pal,PckFile pFile,int height,int width)
		{
			Palette=pal;
			pckFile = pFile;
			this.fileNum=imageNum;
			//this.imageNum=imageNum;
			//this.idx=idx;
			staticID=globalStaticID++;

			Height=height;
			Width=width;

			//image = new Bitmap(Width,Height,PixelFormat.Format8bppIndexed);
			expanded = new byte[Width*Height];

			for(int i=0;i<expanded.Length;i++)
				expanded[i] = TransparentIndex;

			int ex=0;
			int startIdx=0;

			if(idx[0]!=254)
				ex = idx[startIdx++]*Width;		

			for(int i=startIdx;i<idx.Length;i++)
			{
				switch(idx[i])
				{
					case 254: //skip required pixels
					{
						if(moveIdx==-1)
						{
							moveIdx=i+1;
							moveVal=idx[i+1];
						}
						ex+=idx[i+1];
						i++;
					}
						break;
					case 255: //end of image
						break;
					default:
						expanded[ex++]=idx[i];
						break;
				}					
			}
			this.idx=expanded;
		
			image = Bmp.MakeBitmap8(Width,Height,expanded,pal.Colors);
			gray = Bmp.MakeBitmap8(Width,Height,expanded,pal.Grayscale.Colors);
		}

		public static int EncodePck(System.IO.BinaryWriter output, XCImage tile)
		{
			int count = 0;
			bool flag = true;
			byte[] input = tile.Bytes;
			List<byte> bytes = new List<byte>();
			//Color trans = pal.Transparent;
			//pal.SetTransparent(false);

			int totalCount = 0;
			for (int i = 0; i < input.Length; i++)
			{
				byte idx = input[i];
				totalCount++;

				if (idx == TransIdx)
					count++;
				else
				{
					if (count != 0)
					{
						if (flag)
						{
							bytes.Add((byte)(count / tile.Image.Width)); //# of initial rows to skip
							count = (byte)(count % tile.Image.Width);//where we currently are in the transparent row
							flag = false;
							//Console.WriteLine("count, lines: {0}, cells {1}",count/PckImage.IMAGE_WIDTH,count%PckImage.IMAGE_WIDTH);
						}

						while (count >= 255)
						{
							bytes.Add(TransIdx);
							bytes.Add(255);
							count -= 255;
						}

						if (count != 0)
						{
							bytes.Add(TransIdx);
							bytes.Add((byte)count);
						}
						count = 0;
					}
					bytes.Add(idx);
				}
			}

			bool throughLoop = false;
			while (count >= 255)
			{
				bytes.Add(254);
				bytes.Add(255);
				count -= 255;
				throughLoop = true;
			}

			if ((byte)bytes[bytes.Count - 1] != 255 || throughLoop)
				bytes.Add(255);

			//if (bytes.Count % 2 == 1 || throughLoop)
			//    bytes.Add(255);

			output.Write(bytes.ToArray());

			return bytes.Count;
		}

		//public override void Hq2x()
		//{
		//    if(Width==32)//hasnt been done yet
		//        base.Hq2x();
		//}

		public int StaticID
		{
			get{return staticID;}
		}

		public static Type GetCollectionType()
		{
			return typeof(PckFile);
		}

		public void ReImage()
		{
			image = Bmp.MakeBitmap8(Width,Height,expanded,Palette.Colors);
			gray = Bmp.MakeBitmap8(Width,Height,expanded,Palette.Grayscale.Colors);			
		}

		public void MoveImage(byte offset)
		{
			idx[moveIdx]=(byte)(moveVal-offset);
			int ex=0;
			int startIdx=0;
			for(int i=0;i<expanded.Length;i++)
				expanded[i] = TransparentIndex;

			if(idx[0]!=254)
				ex = idx[startIdx++]*Width;	

			for(int i=startIdx;i<idx.Length;i++)
			{
				switch(idx[i])
				{
					case 254: //skip required pixels
					{
						ex+=idx[i+1];
						i++;
					}
						break;
					case 255: //end of image
						break;
					default:
						expanded[ex++]=idx[i];
						break;
				}					
			}
		
			image = Bmp.MakeBitmap8(Width,Height,expanded,Palette.Colors);
			gray = Bmp.MakeBitmap8(Width,Height,expanded,Palette.Grayscale.Colors);
		}

		public int MapID
		{
			get{return mapID;}
			set{mapID=value;}
		}

		public override bool Equals(object other)
		{
			if(other is PckImage)
				return ToString().Equals(other.ToString());			
			return false;
		}

		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}

		public override string ToString()
		{
			string ret="";

			if(pckFile!=null)
				ret+=pckFile.ToString();
			ret+=fileNum+"\n";

			for(int i=0;i<expanded.Length;i++)
			{
				ret+=expanded[i];
				if(expanded[i]==255)
					ret+="\n";
				else
					ret+=" ";
			}
			return ret;
		}
	}
}
