using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using XCom.Interfaces;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace XCom
{
	public class DotNetCollection : XCImageCollection
	{
		public DotNetCollection(Bitmap src, int width, int height, int space)
		{
			Form pvf = (Form)SharedSpace.Instance["PckView"];

			//xConsole.AddLine("File: " + file);
			//Image img = Image.FromFile(file);
			//Bitmap src = new Bitmap(img);

			int across = (src.Width + space) / (width + space);
			int down = (src.Height + space) / (height + space);

			DSShared.Windows.ProgressWindow pw = new DSShared.Windows.ProgressWindow(pvf);
			pw.Minimum = 0;
			pw.Maximum = across * down;
			pw.Width = 300;
			pw.Height = 50;

			pw.Show();

			BitmapData srcData = src.LockBits(new Rectangle(0, 0, src.Width, src.Height), ImageLockMode.ReadOnly, src.PixelFormat);

			int bpp = 4;
			PixelFormat pf = src.PixelFormat;
			xConsole.AddLine("Pixelformat is: " + pf.ToString());
			switch (src.PixelFormat)
			{
				case (PixelFormat.Format24bppRgb):
					bpp = 3;
					break;
				case (PixelFormat.Format32bppArgb):
				case (PixelFormat.Format32bppPArgb):
				case (PixelFormat.Format32bppRgb):
					bpp = 4;
					break;
				default:
					throw new Exception("Image is not 24 or 32 bit, a different collection is needed");
			}

			for (int i = 0, idx = 0; i < src.Height; i += (height + space))
			{
				for (int j = 0; j < src.Width; j += (width + space), idx++)
				{
					Bitmap dest = new Bitmap(width, height, pf);
					BitmapData destData = dest.LockBits(new Rectangle(0, 0, dest.Width, dest.Height), ImageLockMode.WriteOnly, dest.PixelFormat);

					copyData(srcData, j, i, destData, 0, 0, destData.Width, destData.Height, bpp);

					dest.UnlockBits(destData);

					Add(new XCImage(dest, idx));
					try
					{
						pw.Value = idx;
					}
					catch { }
				}
				//srcPtr += srcData.Stride - srcData.Width * bpp;
			}
			src.UnlockBits(srcData);

			pw.Hide();
		}

		private unsafe void copyData(BitmapData srcData, int srcX, int srcY, BitmapData destData, int destX, int destY, int width, int height,int bpp)
		{
			for (int y = 0; y < height && y + srcY < srcData.Height; y++)
			{
				int srcRow = (srcY + y) * srcData.Stride;
				int destRow = y * destData.Stride;
				for (int x = 0; x < width && srcX + x < srcData.Width; x++)
				{
					byte* srcPixel = ((byte*)(srcData.Scan0)) + srcRow + ((srcX + x) * bpp);
					byte* destPixel = ((byte*)(destData.Scan0)) + destRow + (x * bpp);

					for (int k = 0; k < bpp; k++)
						*destPixel++ = *srcPixel++;
				}
			}
		}


		public static void Save(string outFile, XCImageCollection images)
		{

		}

		/// <summary>
		/// 32-bit images dont have palettes
		/// </summary>
		public override Palette Pal
		{
			get
			{
				return null;
			}
			set
			{

			}
		}
	}
}
