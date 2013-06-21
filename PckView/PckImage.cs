using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.IO;
using hq2x;

namespace PckView
{
	public class PckImage:ITile
	{
		#region static stuff
		private static int IMAGE_WIDTH=32;
		private static int IMAGE_HEIGHT=40;
		private static int scale=1;
		private static byte TRANSPARENT_COLOR_INDEX=0xFE;

		public static byte TransparentIndex
		{
			get{return TRANSPARENT_COLOR_INDEX;}
		}

		public static int Width
		{
			get{return IMAGE_WIDTH*scale;}
		}

		public static int Height
		{
			get{return IMAGE_HEIGHT*scale;}
		}

		public static int Scale
		{
			get{return scale;}
			set{scale=value;}
		}
		#endregion

		private Bitmap image;
		private byte[] idx;
		private int imageNum;

		private Color[] types=new Color[]{Color.Red,Color.Gainsboro,Color.Green,Color.Gray,Color.Yellow,Color.Blue,Color.Orange,Color.Aqua,Color.Beige,Color.White,Color.DarkGray,Color.Gold,Color.Purple};

		internal PckImage(int imageNum,byte[] bytes,Palette pal)
		{
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

		public PckImage(Bitmap img, int num)
		{
			this.image=img;
			this.imageNum=num;
		}

		public ITile Clone(Palette pal)
		{
			byte[] b = new byte[idx.Length];
			for(int i=0;i<b.Length;i++)
				b[i]=idx[i];
			return new PckImage(imageNum,b,pal);
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

		public static PckImage FromBMP(Bitmap b,int num,Palette pal,int startX,int startY)
		{
			int count=0;
			bool flag=true;
			ArrayList bytes = new ArrayList();
			ArrayList entries = new ArrayList(b.Palette.Entries);
			bool trans = pal.Transparent;
			pal.Transparent=false;

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
			pal.Transparent=trans;
			return new PckImage(num,(byte[])bytes.ToArray(typeof(byte)),pal);
		}

		public static PckImage FromBMP24(Bitmap b,int num,Palette pal,int startX,int startY)
		{
			Bitmap img = new Bitmap(Width,Height,PixelFormat.Format24bppRgb);
			for(int r=0;r<Height;r++)
				for(int c=0;c<Width;c++)
					img.SetPixel(c,r,b.GetPixel(startX+c,startY+r));

			return new PckImage(img,num);
		}

		public unsafe void Hq2x()
		{
			CImage in24 = new CImage();
			in24.Init(image.Width,image.Height,24);

			for(int row=0;row<image.Height;row++)
				for(int col=0;col<image.Width;col++)
				{
					Color c = image.GetPixel(col,row);
					*(in24.m_pBitmap+((row)*in24.m_Xres*3)+((col)*3))=c.B;
					*(in24.m_pBitmap+((row)*in24.m_Xres*3)+((col)*3+1))=c.G;
					*(in24.m_pBitmap+((row)*in24.m_Xres*3)+((col)*3+2))=c.R;
				}					

			in24.ConvertTo16();
			
			CImage out32 = new CImage();
			out32.Init(in24.m_Xres*2,in24.m_Yres*2,32);

			CImage.InitLUTs();
			CImage.hq2x_32(in24.m_pBitmap,out32.m_pBitmap,in24.m_Xres,in24.m_Yres,out32.m_Xres*4);

			out32.ConvertTo24();

			PckImage.scale=2;

			Bitmap b = new Bitmap(out32.m_Xres,out32.m_Yres,PixelFormat.Format24bppRgb);
			Rectangle   rect = new Rectangle(0, 0, b.Width,b.Height);
			BitmapData bitmapData = b.LockBits(rect,ImageLockMode.WriteOnly,b.PixelFormat);

			IntPtr pixels = bitmapData.Scan0;

			byte *  pBits;
			if (bitmapData.Stride > 0)
				pBits = (byte *)pixels.ToPointer();
			else
				pBits = (byte *)pixels.ToPointer() + bitmapData.Stride*(b.Height-1);

			byte* srcBits = out32.m_pBitmap;
			for (int i=0;i<b.Width*b.Height;i++)
			{
				*(pBits++)=*(srcBits++);
				*(pBits++)=*(srcBits++);
				*(pBits++)=*(srcBits++);
			}
			
			b.UnlockBits(bitmapData);
			
			image.Dispose();
			in24.__dtor();
			out32.__dtor();

			b.MakeTransparent(b.GetPixel(0,0));

			image=b;
		}

//		private void copy(Bitmap src, Bitmap dest, int startX, int startY)
//		{
//			for(int x=startX;x<startX+src.Width;x++)
//				for(int y=startY;y<startY+src.Height;y++)
//					dest.SetPixel(x,y,src.GetPixel(x-startX,y-startY));
//		}
	}
}
