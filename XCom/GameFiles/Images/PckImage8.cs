using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.IO;

namespace XCom
{
	public class PckImage8
	{
		public const int IMAGE_WIDTH=32;
		public const int IMAGE_HEIGHT=40;
		public const byte TRANSPARENT_COLOR_INDEX=0xFE;

		private Bitmap image;
		private byte[] idx;
		private int imageNum;
		private Palette pal;

		internal PckImage8(int imageNum,byte[] bytes,Palette pal)
		{
			this.pal=pal;
			this.imageNum=imageNum;
			idx=bytes;
			image = new Bitmap(IMAGE_WIDTH,IMAGE_HEIGHT,PixelFormat.Format8bppIndexed);
			byte[] expanded = new byte[IMAGE_WIDTH*IMAGE_HEIGHT];

			for(int i=0;i<expanded.Length;i++)
				expanded[i] = TRANSPARENT_COLOR_INDEX;

			int ex = idx[0]*IMAGE_WIDTH;
			for(int i=1;i<idx.Length;i++)
			{
				switch(idx[i])
				{
					case 254: //skip required pixels
						ex+=idx[i+1];
						i++;
						break;
					case 255: //end of image
						break;
					default:
						expanded[ex++]=idx[i];
						break;
				}					
			}
			
			Rectangle   rect = new Rectangle(0, 0, IMAGE_WIDTH, IMAGE_HEIGHT);
			BitmapData bitmapData = image.LockBits(rect,ImageLockMode.WriteOnly,PixelFormat.Format8bppIndexed);

			// Write to the temporary buffer that is provided by LockBits.
			// Copy the pixels from the source image in this loop.
			// Because you want an index, convert RGB to the appropriate
			// palette index here.
			IntPtr pixels = bitmapData.Scan0;

			unsafe 
			{ 
				// Get the pointer to the image bits.
				// This is the unsafe operation.
				byte *  pBits;
				if (bitmapData.Stride > 0)
					pBits = (byte *)pixels.ToPointer();
				else
					// If the Stide is negative, Scan0 points to the last 
					// scanline in the buffer. To normalize the loop, obtain
					// a pointer to the front of the buffer that is located 
					// (Height-1) scanlines previous.
					pBits = (byte *)pixels.ToPointer() + bitmapData.Stride*(IMAGE_HEIGHT-1);
				uint stride = (uint)Math.Abs(bitmapData.Stride);

				ex=0;
				for ( uint row = 0; row < IMAGE_HEIGHT; ++row )
					for ( uint col = 0; col < IMAGE_WIDTH; ++col )
					{
						// The destination pixel.
						// The pointer to the color index byte of the
						// destination; this real pointer causes this
						// code to be considered unsafe.
						byte * p8bppPixel = pBits + row*stride + col;
						*p8bppPixel = expanded[ex++];
					}
			}
			image.UnlockBits(bitmapData);
			image.Palette = pal.Colors;
		}

		public Palette Pal
		{
			get{return pal;}
		}

		public PckImage8 Clone(Palette pal)
		{
			byte[] b = new byte[idx.Length];
			for(int i=0;i<b.Length;i++)
				b[i]=idx[i];
			return new PckImage8(imageNum,b,pal);
		}

		public byte[] Bytes{get{return idx;}}

		public override string ToString()
		{
			string res = "";
			foreach(byte b in idx)
				res+= b+" ";
			return res;
		}

		public Bitmap Image
		{
			get{return image;}
		}

		public int FileNum
		{
			get{return imageNum;}
			set{imageNum=value;}
		}

		public static PckImage8 FromBMP(Bitmap b,int num,Palette pal,int startX,int startY)
		{
			int count=0;
			bool flag=true;
			ArrayList bytes = new ArrayList();
			ArrayList entries = new ArrayList(b.Palette.Entries);
			//bool trans = pal.Transparent;
			//pal.Transparent=false;

			for(int r=startY;r<startY+PckImage.IMAGE_HEIGHT;r++)
				for(int c=startX;c<startX+PckImage.IMAGE_WIDTH;c++)
				{
					byte idx = (byte)entries.IndexOf(b.GetPixel(c,r));

					if(idx==PckImage.TRANSPARENT_COLOR_INDEX)
						count++;
					else
					{
						if(count!=0)
						{
							if(flag)
							{
								bytes.Add((byte)(count/PckImage.IMAGE_WIDTH));
								count = (byte)(count%PckImage.IMAGE_WIDTH);
								flag=false;
								//Console.WriteLine("count, lines: {0}, cells {1}",count/PckImage.IMAGE_WIDTH,count%PckImage.IMAGE_WIDTH);
							}

							bytes.Add((byte)PckImage.TRANSPARENT_COLOR_INDEX);
							bytes.Add((byte)count);
							count=0;
						}
						bytes.Add((byte)idx);
					}
				}

			while(count>=255)
			{
				bytes.Add((byte)254);
				bytes.Add((byte)255);
				count-=255;
			}

			bytes.Add((byte)255);
			//pal.Transparent=trans;
			return new PckImage8(num,(byte[])bytes.ToArray(typeof(byte)),pal);
		}
	}
}
