using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Collections;

namespace XCom
{
	/// <summary>
	/// A class defining a color array of 256 values
	/// </summary>
	/// 
	//see http://support.microsoft.com/default.aspx?scid=kb%3Ben-us%3B319061
	public class Palette
	{
		private string name;

		private ColorPalette cp;

		private static Hashtable palHash=new Hashtable();

		private static readonly char COMMENT='#';

		private static readonly string embedPath="XCom._Embedded.";

		/// <summary>
		/// The UFO Palette embedded in this assembly
		/// </summary>
		public static Palette UFOBattle
		{
			get
			{
				if(palHash["ufo-battle"]==null)
				{
					Assembly thisAssembly = Assembly.GetExecutingAssembly();
					palHash["ufo-battle"] = new Palette(thisAssembly.GetManifestResourceStream(embedPath+"ufo-battle.pal"));
				}
				return (Palette)palHash["ufo-battle"];
			}	
		}

		public static Palette UFOGeo
		{
			get
			{
				if(palHash["ufo-geo"] == null)
				{
					Assembly thisAssembly = Assembly.GetExecutingAssembly();
					palHash["ufo-geo"] = new Palette(thisAssembly.GetManifestResourceStream(embedPath+"ufo-geo.pal"));
				}
				return (Palette)palHash["ufo-geo"];
			}	
		}

		public static Palette UFOGraph
		{
			get
			{
				if(palHash["ufo-graph"] == null)
				{
					Assembly thisAssembly = Assembly.GetExecutingAssembly();
					palHash["ufo-graph"] = new Palette(thisAssembly.GetManifestResourceStream(embedPath+"ufo-graph.pal"));
				}
				return (Palette)palHash["ufo-graph"];
			}	
		}

		public static Palette UFOResearch
		{
			get
			{
				if(palHash["ufo-research"] == null)
				{
					Assembly thisAssembly = Assembly.GetExecutingAssembly();
					palHash["ufo-research"] = new Palette(thisAssembly.GetManifestResourceStream(embedPath+"ufo-research.pal"));
				}
				return (Palette)palHash["ufo-research"];
			}	
		}

		/// <summary>
		/// The TFTD Palette embedded in this assembly
		/// </summary>
		public static Palette TFTDBattle
		{
			get
			{
				if(palHash["tftd-battle"] == null)
				{
					Assembly thisAssembly = Assembly.GetExecutingAssembly();
					palHash["tftd-battle"] = new Palette(thisAssembly.GetManifestResourceStream(embedPath+"tftd-battle.pal"));
				}
				return (Palette)palHash["tftd-battle"];
			}	
		}

		public static Palette TFTDGeo
		{
			get
			{
				if(palHash["tftd-geo"] == null)
				{
					Assembly thisAssembly = Assembly.GetExecutingAssembly();
					palHash["tftd-geo"] = new Palette(thisAssembly.GetManifestResourceStream(embedPath+"tftd-geo.pal"));
				}
				return (Palette)palHash["tftd-geo"];
			}	
		}

		public static Palette TFTDGraph
		{
			get
			{
				if(palHash["tftd-graph"] == null)
				{
					Assembly thisAssembly = Assembly.GetExecutingAssembly();
					palHash["tftd-graph"] = new Palette(thisAssembly.GetManifestResourceStream(embedPath+"tftd-graph.pal"));
				}
				return (Palette)palHash["tftd-graph"];
			}	
		}

		public static Palette TFTDResearch
		{
			get
			{
				if(palHash["tftd-research"] == null)
				{
					Assembly thisAssembly = Assembly.GetExecutingAssembly();
					palHash["tftd-research"] = new Palette(thisAssembly.GetManifestResourceStream(embedPath+"tftd-research.pal"));
				}
				return (Palette)palHash["tftd-research"];
			}	
		}

		public Palette(string name)
		{
			Bitmap b = new Bitmap(1,1,PixelFormat.Format8bppIndexed);
			cp = b.Palette;
			b.Dispose();
			this.name=name;
		}

		public Palette(Stream s)
		{
			StreamReader input = new StreamReader(s);
			string[] line=new string[0];
			name = input.ReadLine();

			Bitmap b = new Bitmap(1,1,PixelFormat.Format8bppIndexed);
			cp = b.Palette;

			for(byte i=0;i<0xFF;i++)
			{
				string allLine = input.ReadLine().Trim();
				if(allLine[0]==COMMENT)
				{
					i--;
					continue;
				}
				line = allLine.Split(',');
				cp.Entries[i] = Color.FromArgb(int.Parse(line[0]),int.Parse(line[1]),int.Parse(line[2]));
			}
			b.Dispose();

			//checkPalette();
		}
/*
		private void checkPalette()
		{
			Bitmap b = new Bitmap(1,1,PixelFormat.Format8bppIndexed);
			ColorPalette colors = b.Palette;
			b.Dispose();

			ArrayList cpList = new ArrayList(cp.Entries);
			ArrayList colorList = new ArrayList();

			for(int i=0;i<cpList.Count;i++)
			{
				if(!colorList.Contains(cpList[i]))
				{
					colorList.Add(cpList[i]);
					colors.Entries[i]=(Color)cpList[i];
				}
				else
				{
					Color c = (Color)cpList[i];
					int rc=c.R;
					int gc=c.G;
					int bc=c.B;

					if(rc==0)
						rc++;
					else
						rc--;

					if(gc==0)
						gc++;
					else
						gc--;

					if(bc==0)
						bc++;
					else
						bc--;

					colorList.Add(Color.FromArgb(rc,gc,bc));
                    colors.Entries[i]=Color.FromArgb(rc,gc,bc);
				}				
			}
		}*/

		public Color Transparent
		{
			get{return cp.Entries[Bmp.DefaultTransparentIndex];}
		}
		
		public Palette Grayscale
		{
			get
			{
				if(palHash[name+"#gray"]==null)
				{
					Palette p = new Palette(name+"#gray");
					palHash[p.name]=p;
					for(int i=0;i<cp.Entries.Length;i++)
					{
						int s = (int)(this[i].R*.10+this[i].G*.50+this[i].B*.25);
						p[i]=Color.FromArgb(s,s,s);
					}
				}
				return (Palette)palHash[name+"#gray"];
			}
		}
		
		public void SetTransparent(bool val,int index)
		{
			Color old = cp.Entries[index];
			if(val)
				cp.Entries[index]=Color.FromArgb(0,old);
			else
				cp.Entries[index]=Color.FromArgb(255,old);
		}

		public void SetTransparent(bool val)
		{
			SetTransparent(val,Bmp.DefaultTransparentIndex);
		}

		public ColorPalette Colors
		{
			get{return cp;}
		}

		/// <summary>
		/// This palette's name
		/// </summary>
		public string Name
		{
			get{return name;}
		}

		/// <summary>
		/// Indexes colors on number
		/// </summary>
		public Color this[int i]
		{
			get{return cp.Entries[i];}
			set{cp.Entries[i]=value;}
		}

		/// <summary>
		/// tests for palette equality
		/// </summary>
		/// <param name="other">another palette</param>
		/// <returns>true if the palette names are the same</returns>
		public override bool Equals(Object other)
		{
			if(!(other is Palette))
				return false;
			return this.cp.Equals(((Palette)other).cp);
		}

		public override int GetHashCode()
		{
			return cp.GetHashCode();
		}

		public static Palette GetPalette(string name)
		{
			if(palHash[name]==null)
			{
				Assembly thisAssembly = Assembly.GetExecutingAssembly();
				try
				{
					palHash[name] = new Palette(thisAssembly.GetManifestResourceStream(embedPath+name+".pal"));
				}
				catch{palHash[name]=null;}
			}
			return (Palette)palHash[name];
		}

		public override string ToString()
		{
			return name;
		}
	}
}
