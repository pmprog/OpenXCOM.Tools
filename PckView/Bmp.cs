using System;
using System.Drawing;
using System.IO;
using System.Collections;
using System.Drawing.Imaging;
using System.Reflection;

namespace PckView
{
	/// <summary>
	/// Summary description for Bmp.
	/// </summary>
	public class Bmp
	{
		public static event LoadingDelegate LoadingEvent;

		public static void Save(string path, Bitmap image)
		{
			Save(new FileStream(path,FileMode.Create),image);
		}

		public static void Save(Stream s, Bitmap image)
		{
			BinaryWriter bw = new BinaryWriter(s);

			int more = 0;
			while((image.Width+more)%4!=0)
				more++;

			int len = (image.Width+more)*image.Height;
			bw.Write('B');//must always be set to 'BM' to declare that this is a .bmp-file.
			bw.Write('M');
			bw.Write(1078+len);//specifies the size of the file in bytes.
			bw.Write((int)0); //zero
			bw.Write((int)1078); //14+40+(4*256) specifies the offset from the beginning of the file to the bitmap data.

			bw.Write((int)40);//specifies the size of the BITMAPINFOHEADER structure, in bytes
			bw.Write((int)image.Width);
			bw.Write((int)image.Height);
			bw.Write((short)1);//specifies the number of planes of the target device
			bw.Write((short)8);//specifies the number of bits per pixel
			bw.Write((int)0);
			bw.Write((int)0);
			bw.Write((int)0);
			bw.Write((int)0);
			bw.Write((int)0);
			bw.Write((int)0);
			bw.Write((int)0);

			//Console.WriteLine("File size should be: "+(1078+len+more));

			for(int i=1;i<256;i++)
			{						
				bw.Write((byte)image.Palette.Entries[i].B);
				bw.Write((byte)image.Palette.Entries[i].G);
				bw.Write((byte)image.Palette.Entries[i].R);		
				bw.Write((byte)0);		
			}

			Hashtable table = new Hashtable();
			int idx=0;
			foreach(Color c in image.Palette.Entries)
				table[c]=(byte)idx++;
			//			Console.WriteLine("Colors: "+idx);

			//the blank color between each individual image
			table[Color.FromArgb(0,0,0,0)]=(byte)255;

			for(int i=image.Height-1;i>=0;i--)
			{
				for(int j=0;j<image.Width;j++)
					bw.Write((byte)table[image.GetPixel(j,i)]);	
				for(int j=0;j<more;j++)
					bw.Write((byte)0x00);
			}

			bw.Flush();
			bw.Close();
		}

		public static void Save24(string path, Bitmap image)
		{
			Save24(new FileStream(path,FileMode.Create),image);
		}

		public static void Save24(Stream s, Bitmap image)
		{
			BinaryWriter bw = new BinaryWriter(s);

			int more = 0;
			while((image.Width*3+more)%4!=0)
				more++;

			int len = (image.Width*3+more)*image.Height;
			bw.Write('B');//must always be set to 'BM' to declare that this is a .bmp-file.
			bw.Write('M');
			bw.Write(14+40+len);//specifies the size of the file in bytes.
			bw.Write((int)0); //zero
			bw.Write((int)14+40); //specifies the offset from the beginning of the file to the bitmap data.

			bw.Write((int)40);//specifies the size of the BITMAPINFOHEADER structure, in bytes
			bw.Write((int)image.Width);
			bw.Write((int)image.Height);
			bw.Write((short)1);//specifies the number of planes of the target device
			bw.Write((short)24);//specifies the number of bits per pixel
			bw.Write((int)0);
			bw.Write((int)0);
			bw.Write((int)0);
			bw.Write((int)0);
			bw.Write((int)0);
			bw.Write((int)0);

			for(int i=image.Height-1;i>=0;i--)
			{
				for(int j=0;j<image.Width;j++)
				{
					Color c = image.GetPixel(j,i);
					bw.Write((byte)c.B);
					bw.Write((byte)c.G);
					bw.Write((byte)c.R);
				}

				for(int j=0;j<more;j++)
					bw.Write((byte)0x00);
			}

			bw.Flush();
			bw.Close();
		}

		/// <summary>
		/// Creates a TRUE 8-bit indexed bitmap from the specified byte array
		/// </summary>
		/// <param name="width">width of final bitmap</param>
		/// <param name="height">height of final bitmap</param>
		/// <param name="idx">image data</param>
		/// <param name="pal">Palette to color the image with</param>
		/// <returns></returns>
		public static Bitmap MakeBitmap(int width,int height,byte[] idx,ColorPalette pal)
		{
			Bitmap image = new Bitmap(width,height,PixelFormat.Format8bppIndexed);
			Rectangle   rect = new Rectangle(0, 0, width,height);
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
					pBits = (byte *)pixels.ToPointer() + bitmapData.Stride*(height-1);
				uint stride = (uint)Math.Abs(bitmapData.Stride);

				int ex=0;
				for ( uint row = 0; row < height; ++row )
					for ( uint col = 0; col < width; ++col )
					{
						// The destination pixel.
						// The pointer to the color index byte of the
						// destination; this real pointer causes this
						// code to be considered unsafe.
						byte * p8bppPixel = pBits + row*stride + col;
						*p8bppPixel = idx[ex++];
					}
			}
			image.UnlockBits(bitmapData);
			image.Palette=pal;
			return image;
		}

		public static void FireLoadingEvent(int curr,int total)
		{
			if(LoadingEvent!=null)
				LoadingEvent(curr,total);
		}		

		public static xCollection Load(string file, Type collectionType)
		{
			Bitmap b = new Bitmap(file);

			Console.WriteLine("Loading bitmap, format: "+b.PixelFormat);

			MethodInfo mi = collectionType.GetMethod("FromBmp");
			if(mi==null)
				return null;
			else
				return (xCollection)mi.Invoke(null,new object[]{b});
		}

		public static ITile LoadSingle(Bitmap src,int num,Palette pal,Type collectionType)
		{			
			MethodInfo mi = collectionType.GetMethod("FromBmpSingle");
			if(mi==null)
				return null;
			else
				return (ITile)mi.Invoke(null,new object[]{src,num,pal});
		}

		public static void Draw(Bitmap src, Bitmap dest, int x, int y)
		{
			Rectangle srcRect = new Rectangle(0, 0, src.Width,src.Height);
			BitmapData srcData = src.LockBits(srcRect,ImageLockMode.ReadOnly,PixelFormat.Format8bppIndexed);

			Rectangle destRect = new Rectangle(0, 0, dest.Width,dest.Height);
			BitmapData destData = dest.LockBits(destRect,ImageLockMode.WriteOnly,PixelFormat.Format8bppIndexed);

			IntPtr srcPixels = srcData.Scan0;
			IntPtr destPixels = destData.Scan0;

			unsafe 
			{ 
				byte * sBits;
				if (srcData.Stride > 0)
					sBits = (byte *)srcPixels.ToPointer();
				else
					sBits = (byte *)srcPixels.ToPointer() + srcData.Stride*(src.Height-1);
				uint sStride = (uint)Math.Abs(srcData.Stride);

				byte *  dBits;
				if (destData.Stride > 0)
					dBits = (byte *)destPixels.ToPointer();
				else
					dBits = (byte *)destPixels.ToPointer() + destData.Stride*(dest.Height-1);
				uint dStride = (uint)Math.Abs(destData.Stride);

				for ( uint row = 0; row < src.Height; row++ )
					for ( uint col = 0; col < src.Width; col++ )
					{
						byte * d8bppPixel = dBits + (row+y)*dStride + (col+x);
						byte * s8bppPixel = sBits + row*sStride + col;

						if(*s8bppPixel!=PckImage.TransparentIndex && row+y<dest.Height)
							*d8bppPixel=*s8bppPixel;
					}
			}
			src.UnlockBits(srcData);
			dest.UnlockBits(destData);
		}

		public static Bitmap MakeBitmap(int width,int height,ColorPalette pal)
		{
			Bitmap image = new Bitmap(width,height,PixelFormat.Format8bppIndexed);
			Rectangle   rect = new Rectangle(0, 0, width,height);
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
					pBits = (byte *)pixels.ToPointer() + bitmapData.Stride*(height-1);
				uint stride = (uint)Math.Abs(bitmapData.Stride);

				for ( uint row = 0; row < height; ++row )
					for ( uint col = 0; col < width; ++col )
					{
						// The destination pixel.
						// The pointer to the color index byte of the
						// destination; this real pointer causes this
						// code to be considered unsafe.
						byte * p8bppPixel = pBits + row*stride + col;
						//*p8bppPixel = idx[ex++];
						*p8bppPixel=PckImage.TransparentIndex;

					}
			}
			image.UnlockBits(bitmapData);
			image.Palette=pal;
			return image;
		}

		public static Bitmap To24Bit(Bitmap old)
		{
			Bitmap b=null;
			if(old.PixelFormat==PixelFormat.Format32bppArgb || old.PixelFormat==PixelFormat.Format32bppRgb)
			{
				b = new Bitmap(old.Width,old.Height,PixelFormat.Format24bppRgb);
				Rectangle   rect = new Rectangle(0, 0, old.Width,old.Height);
				BitmapData destData = b.LockBits(rect,ImageLockMode.WriteOnly,PixelFormat.Format24bppRgb);

				Rectangle rect1 = new Rectangle(0,0,old.Width,old.Height);
				BitmapData srcData = old.LockBits(rect1,ImageLockMode.ReadOnly,PixelFormat.Format32bppArgb);

				IntPtr dPixels = destData.Scan0;
				IntPtr sPixels = srcData.Scan0;

				unsafe 
				{ 
					byte * dBits;
					if (destData.Stride > 0)
						dBits = (byte *)dPixels.ToPointer();
					else
						dBits = (byte *)dPixels.ToPointer() + destData.Stride*(b.Height-1);
					uint dStride = (uint)Math.Abs(destData.Stride);

					byte *  sBits;
					if (srcData.Stride > 0)
						sBits = (byte *)sPixels.ToPointer();
					else
						sBits = (byte *)sPixels.ToPointer() +srcData.Stride*(old.Height-1);
					uint sStride = (uint)Math.Abs(srcData.Stride);

					//bool flag=true;
					for(int row=0;row<old.Height;row++)
					{
						for(int col=0;col<old.Width;col++)
						{
							Color c = Color.FromArgb(*((int*)sBits));
							*(dBits++) = (byte)c.B;//*(sBits++);
							*(dBits++) = (byte)c.G;//*(sBits++);
							*(dBits++) = (byte)c.R;//*(sBits++);
							sBits+=4;
						}
						dBits++;
						dBits++;
						
						//if(row%2==0)

						//else
						//	dBits--;
						
					}
					
//					if (destData.Stride > 0)
//						dBits = (byte *)dPixels.ToPointer();
//					else
//						dBits = (byte *)dPixels.ToPointer() + destData.Stride*(b.Height-1);
//
//					for(int row=4;row<old.Height;row++)
//					{
//						int amt = row-2;
//						for(int col=amt;col<old.Width;col++)
//						{
//							*(dBits+row*(old.Width)+col-amt) = *(dBits+row*(old.Width)+col);
//							*(dBits+(row+1)*(old.Width)+col-amt) = *(dBits+(row+1)*(old.Width)+col);
//						}
//					}
				}
				old.UnlockBits(srcData);
				b.UnlockBits(destData);
			}
			else if(old.PixelFormat==PixelFormat.Format8bppIndexed)
			{
				b = new Bitmap(old.Width,old.Height,PixelFormat.Format24bppRgb);
				Rectangle   rect = new Rectangle(0, 0, old.Width,old.Height);
				BitmapData destData = b.LockBits(rect,ImageLockMode.WriteOnly,PixelFormat.Format24bppRgb);

				Rectangle rect1 = new Rectangle(0,0,old.Width,old.Height);
				BitmapData srcData = old.LockBits(rect1,ImageLockMode.ReadOnly,PixelFormat.Format8bppIndexed);

				IntPtr dPixels = destData.Scan0;
				IntPtr sPixels = srcData.Scan0;

				unsafe 
				{ 
					byte * dBits;
					if (destData.Stride > 0)
						dBits = (byte *)dPixels.ToPointer();
					else
						dBits = (byte *)dPixels.ToPointer() + destData.Stride*(b.Height-1);
					//uint dStride = (uint)Math.Abs(destData.Stride);

					byte *  sBits;
					if (srcData.Stride > 0)
						sBits = (byte *)sPixels.ToPointer();
					else
						sBits = (byte *)sPixels.ToPointer() +srcData.Stride*(old.Height-1);
					//uint sStride = (uint)Math.Abs(srcData.Stride);

					for(int i=0;i<old.Width*old.Height;i++)
					{
						Color c = old.Palette.Entries[*(sBits++)];
						*(dBits++) = (byte)c.B;
						*(dBits++) = (byte)c.G;
						*(dBits++) = (byte)c.R;
					}
				}
				old.UnlockBits(srcData);
				b.UnlockBits(destData);
			}

			return b;
		}

		public static Bitmap To16Bit(Bitmap old)
		{
			Bitmap b=null;
			if(old.PixelFormat==PixelFormat.Format24bppRgb)
			{
				b = new Bitmap(old.Width,old.Height,PixelFormat.Format16bppRgb565);
				Rectangle   rect = new Rectangle(0, 0, old.Width,old.Height);
				BitmapData destData = b.LockBits(rect,ImageLockMode.WriteOnly,PixelFormat.Format16bppRgb565);

				Rectangle rect1 = new Rectangle(0,0,old.Width,old.Height);
				BitmapData srcData = old.LockBits(rect1,ImageLockMode.ReadOnly,PixelFormat.Format24bppRgb);

				IntPtr dPixels = destData.Scan0;
				IntPtr sPixels = srcData.Scan0;

				unsafe 
				{ 
					byte * dBits;
					if (destData.Stride > 0)
						dBits = (byte *)dPixels.ToPointer();
					else
						dBits = (byte *)dPixels.ToPointer() + destData.Stride*(b.Height-1);
					//uint dStride = (uint)Math.Abs(destData.Stride);

					byte *  sBits;
					if (srcData.Stride > 0)
						sBits = (byte *)sPixels.ToPointer();
					else
						sBits = (byte *)sPixels.ToPointer() +srcData.Stride*(old.Height-1);
					//uint sStride = (uint)Math.Abs(srcData.Stride);

					for(int i=0;i<old.Width*old.Height;i++)
					{
						byte pxb = (byte)(*(sBits++) >> 3);
						byte pxg = (byte)(*(sBits++) >> 2);
						byte pxr = (byte)(*(sBits++) >> 3);

						*((ushort *)dBits)=(ushort)((pxr<<11) + (pxg<<5)+pxb);
						dBits++;
						dBits++;
					}
				}
				old.UnlockBits(srcData);
				b.UnlockBits(destData);
			}
			else if(old.PixelFormat==PixelFormat.Format8bppIndexed)
			{
				b = new Bitmap(old.Width,old.Height,PixelFormat.Format16bppRgb565);
				Rectangle   rect = new Rectangle(0, 0, old.Width,old.Height);
				BitmapData destData = b.LockBits(rect,ImageLockMode.WriteOnly,PixelFormat.Format16bppRgb565);

				Rectangle rect1 = new Rectangle(0,0,old.Width,old.Height);
				BitmapData srcData = old.LockBits(rect1,ImageLockMode.ReadOnly,PixelFormat.Format8bppIndexed);

				IntPtr dPixels = destData.Scan0;
				IntPtr sPixels = srcData.Scan0;

				unsafe 
				{ 
					byte * dBits;
					if (destData.Stride > 0)
						dBits = (byte *)dPixels.ToPointer();
					else
						dBits = (byte *)dPixels.ToPointer() + destData.Stride*(b.Height-1);
					//uint dStride = (uint)Math.Abs(destData.Stride);

					byte *  sBits;
					if (srcData.Stride > 0)
						sBits = (byte *)sPixels.ToPointer();
					else
						sBits = (byte *)sPixels.ToPointer() +srcData.Stride*(old.Height-1);
					//uint sStride = (uint)Math.Abs(srcData.Stride);

					for(int i=0;i<old.Width*old.Height;i++)
					{
						int idx = *(sBits++);

						byte pxb = (byte)old.Palette.Entries[idx].B;
						byte pxg = (byte)old.Palette.Entries[idx].G;
						byte pxr = (byte)old.Palette.Entries[idx].R;

						*((ushort *)dBits)=(ushort)((pxr<<11) + (pxg<<5)+pxb);
						dBits++;
						dBits++;
					}
				}
				old.UnlockBits(srcData);
				b.UnlockBits(destData);
			}
			return b;
		}
	}
}
