using System;
using System.Collections;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace PckView
{
	public enum PckImageType{Bitmap,Surface};

	public delegate void LoadingDelegate(int curr, int total);

	public class PckFile:xCollection
	{
		private ArrayList images;
		private string name,path;
		private Palette pal=Palette.TFTDBattle;

		public event LoadingDelegate LoadingEvent; 
		public readonly static int imgSpace=1;

		public PckFile(Stream pckFile, Stream tabFile,int bpp)
		{
			byte[] info = new byte[pckFile.Length];
			pckFile.Read(info,0,info.Length);
			pckFile.Close();

			this.pal=DefaultPalette;

			uint[] offsets = new uint[(tabFile.Length/bpp)+1];
			BinaryReader br = new BinaryReader(tabFile);

			if(bpp==2)
				for(int i=0;i<tabFile.Length/bpp;i++)
					offsets[i] = br.ReadUInt16();
			else
				for(int i=0;i<tabFile.Length/bpp;i++)
					offsets[i] = br.ReadUInt32();

			offsets[offsets.Length-1] = (uint)info.Length;

			images = new ArrayList(offsets.Length-1);

			for(int i=0;i<offsets.Length-1;i++)
			{
				byte[] imgDat = new byte[offsets[i+1]-offsets[i]];
				for(int j=0;j<imgDat.Length;j++)			
					imgDat[j] = info[offsets[i]+j];

				images.Add(new PckImage(i,imgDat,pal));

				if(LoadingEvent!=null)
					LoadingEvent(i,offsets.Length-1);
			}	
			br.Close();
		}

		public int ImgWidth{get{return PckImage.Width;}}
		public int ImgHeight{get{return PckImage.Height;}}

		public string Name
		{
			get{return name;}
			set{name=value;}
		}

		public string Path
		{
			get{return path;}
			set{path=value;}
		}

		public string Extension
		{
			get{return ".PCK";}
		}

		public PckFile(ArrayList images)
		{
			this.images=images;
		}

		public Palette DefaultPalette
		{
			get{return Palette.TFTDBattle;}
		}

		public void Save(string path,int bpp)
		{
			path = path.ToLower();
			if(path.EndsWith(".pck"))
				path = path.Substring(0,path.IndexOf(".pck"));

			BinaryWriter pck = new BinaryWriter(File.Create(path+".pck"));
			BinaryWriter tab = new BinaryWriter(File.Create(path+".tab"));

			if(bpp==2)
			{
				ushort count=0;
				foreach(PckImage img in images)
				{
					tab.Write(count);
					pck.Write(img.Bytes);
					count+=(ushort)img.Bytes.Length;
				}
			}
			else
			{
				int count=0;
				foreach(PckImage img in images)
				{
					tab.Write(count);
					pck.Write(img.Bytes);
					count+=img.Bytes.Length;
				}
			}

			pck.Flush();
			pck.Close();

			tab.Flush();
			tab.Close();
		}

		public Palette Pal{
			get{return pal;}
			set{
				foreach(PckImage p in images)
					p.Image.Palette = value.Colors;
				pal=value;
			}
		}

		public IEnumerator GetEnumerator() 
		{
			return images.GetEnumerator();
		}

		public int Size
		{
			get{return images.Count;}
		}

		public ITile this[int i]
		{
			get{return (i<images.Count && i>=0 ? (PckImage)images[i]:null);}
			set
			{
				if(i<images.Count && i>=0)
					images[i]=value;
				else 
				{
					value.FileNum = images.Count;
					images.Add(value);					
				}
			}
		}

		public void Remove(int i)
		{
			Console.WriteLine("Removing: "+i);
			images.RemoveAt(i);
		}

		public static xCollection FromBmp(Bitmap b)
		{
			PckImage.Scale=1;
			switch(b.PixelFormat)
			{
				case PixelFormat.Format8bppIndexed:
					break;
				case PixelFormat.Format24bppRgb:
					PckImage.Scale=2;
					break;
			}

			int space=PckFile.imgSpace*PckImage.Scale;

			ArrayList list = new ArrayList();

			int cols = (b.Width+space)/(PckImage.Width+space);
			int rows = (b.Height+space)/(PckImage.Height+space);

			int num=0;
			
			for(int y=0;y<b.Height;y+=PckImage.Height+space)
				for(int x=0;x<b.Width;x+=PckImage.Width+space)
					try
					{
						switch(b.PixelFormat)
						{
							case PixelFormat.Format8bppIndexed:
								list.Add(PckImage.FromBMP(b,num++,Palette.TFTDBattle,x,y));
								break;
							case PixelFormat.Format24bppRgb:
								list.Add(PckImage.FromBMP24(b,num++,Palette.TFTDBattle,x,y));
								break;
						}
														
						Bmp.FireLoadingEvent(num,rows*cols);
					}
					catch{}

			return new PckFile(list);			
		}

		public static ITile FromBmpSingle(Bitmap b, int num, Palette p)
		{
			return PckImage.FromBMP(b,num,p,0,0);
		}

		public void SaveBMP(string file,Palette pal,int bpp,int across)
		{
			int mod=1;
			if(Size%across==0)
				mod=0;

			int space=imgSpace;

			Bitmap b = new Bitmap(across*(ImgWidth+space)-space,(Size/across+mod)*(ImgHeight+space)-space);
			Graphics g = Graphics.FromImage(b);
			g.FillRectangle(new SolidBrush(pal.Colors.Entries[PckImage.TransparentIndex]),0,0,b.Width,b.Height);

			for(int i=0;i<Size;i++)
			{
				int x = i%across*(ImgWidth+space);
				int y = i/across*(ImgHeight+space);
				
				copy(this[i].Image,b,x,y);
			}
			
			b.Palette=pal.Colors;
			if(bpp==8)
				Bmp.Save(file,b);
			else
				Bmp.Save24(file,b);			
		}

		public unsafe void Hq2x(int across)
		{
			foreach(PckImage pf in images)
				pf.Hq2x();
			/*	
			int mod=1;
			if(Size%across==0)
				mod=0;

			int space=imgSpace;

			pal.Transparent=false;

			CImage in24 = new CImage();
			in24.Init(across*(ImgWidth+space)-space,(Size/across+mod)*(ImgHeight+space)-space,24);
			for(int i=0;i<Size;i++)
			{
				int x = i%across*(ImgWidth+space);
				int y = i/across*(ImgHeight+space);
				copy24(this[i].Image,in24,x,y);
			}		

			in24.ConvertTo16();
			
			CImage out32 = new CImage();
			out32.Init(in24.m_Xres*2,in24.m_Yres*2,32);

			CImage.InitLUTs();
			CImage.hq2x_32(in24.m_pBitmap,out32.m_pBitmap,in24.m_Xres,in24.m_Yres,out32.m_Xres*4);

			out32.ConvertTo24();	
			
			writeCImage(out32,"Out24.bmp");*/

			Console.WriteLine("Done");
		}

		public static unsafe void writeCImage(CImage img, string outFile)
		{
			BinaryWriter bw = new BinaryWriter(File.OpenWrite(outFile));

			int more = 0;
			while((img.m_Xres*3+more)%4!=0)
				more++;

			int len = (img.m_Xres*3+more)*img.m_Yres;
			bw.Write('B');//must always be set to 'BM' to declare that this is a .bmp-file.
			bw.Write('M');
			bw.Write(14+40+len);//specifies the size of the file in bytes.
			bw.Write((int)0); //zero
			bw.Write((int)14+40); //specifies the offset from the beginning of the file to the bitmap data.

			bw.Write((int)40);//specifies the size of the BITMAPINFOHEADER structure, in bytes
			bw.Write((int)img.m_Xres);
			bw.Write((int)img.m_Yres);
			bw.Write((short)1);//specifies the number of planes of the target device
			bw.Write((short)24);//specifies the number of bits per pixel
			bw.Write((int)0);
			bw.Write((int)0);
			bw.Write((int)0);
			bw.Write((int)0);
			bw.Write((int)0);
			bw.Write((int)0);

			byte* pix=img.m_pBitmap;
			pix+=img.m_NumPixel*3;
	
			for (int i=0; i<img.m_Yres; i++)
			{
				pix-=img.m_Xres*3;

				for(int j=0;j<img.m_Xres*3;j++)
					bw.Write((byte)*(pix+j));

				for(int j=0;j<more;j++)
					bw.Write((byte)0x00);
			}

			bw.Flush();
			bw.Close();	
		}

		private unsafe void copy24(Bitmap b,CImage img,int x,int y)
		{
			for(int row=0;row<b.Height;row++)
				for(int col=0;col<b.Width;col++)
				{
					Color c = b.GetPixel(col,row);
					*(img.m_pBitmap+((y+row)*img.m_Xres*3)+((x+col)*3))=c.B;
					*(img.m_pBitmap+((y+row)*img.m_Xres*3)+((x+col)*3+1))=c.G;
					*(img.m_pBitmap+((y+row)*img.m_Xres*3)+((x+col)*3+2))=c.R;
				}
		}

		private void copy(Bitmap src, Bitmap dest, int startX, int startY)
		{
			for(int x=startX;x<startX+src.Width;x++)
				for(int y=startY;y<startY+src.Height;y++)
					dest.SetPixel(x,y,src.GetPixel(x-startX,y-startY));
		}
	}								 
}