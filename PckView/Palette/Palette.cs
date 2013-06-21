using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Collections;

namespace PckView
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

		private static Palette ufoBattle,ufoGeo,ufoGraph,ufoResearch;
		private static Palette tftdBattle,tftdGeo,tftdGraph,tftdResearch;

		private static readonly char COMMENT='#';

		private static readonly string embedPath="PckView._Embedded.";

		/// <summary>
		/// The UFO Palette embedded in this assembly
		/// </summary>
		public static Palette UFOBattle
		{
			get
			{
				if(ufoBattle == null)
				{
					Assembly thisAssembly = Assembly.GetExecutingAssembly();
					ufoBattle = new Palette(thisAssembly.GetManifestResourceStream(embedPath+"ufo-battle.pal"));
				}
				return ufoBattle;
			}	
		}

		public static Palette UFOGeo
		{
			get
			{
				if(ufoGeo == null)
				{
					Assembly thisAssembly = Assembly.GetExecutingAssembly();
					ufoGeo = new Palette(thisAssembly.GetManifestResourceStream(embedPath+"ufo-geo.pal"));
				}
				return ufoGeo;
			}	
		}

		public static Palette UFOGraph
		{
			get
			{
				if(ufoGraph == null)
				{
					Assembly thisAssembly = Assembly.GetExecutingAssembly();
					ufoGraph = new Palette(thisAssembly.GetManifestResourceStream(embedPath+"ufo-graph.pal"));
				}
				return ufoGraph;
			}	
		}

		public static Palette UFOResearch
		{
			get
			{
				if(ufoResearch == null)
				{
					Assembly thisAssembly = Assembly.GetExecutingAssembly();
					ufoResearch = new Palette(thisAssembly.GetManifestResourceStream(embedPath+"ufo-research.pal"));
				}
				return ufoResearch;
			}	
		}

		public static Palette TFTDBattle
		{
			get
			{
				if(tftdBattle == null)
				{
					Assembly thisAssembly = Assembly.GetExecutingAssembly();
					tftdBattle = new Palette(thisAssembly.GetManifestResourceStream(embedPath+"tftd-battle.pal"));
				}
				return tftdBattle;
			}	
		}

		public static Palette TFTDGeo
		{
			get
			{
				if(tftdGeo == null)
				{
					Assembly thisAssembly = Assembly.GetExecutingAssembly();
					tftdGeo = new Palette(thisAssembly.GetManifestResourceStream(embedPath+"tftd-geo.pal"));
				}
				return tftdGeo;
			}	
		}

		public static Palette TFTDGraph
		{
			get
			{
				if(tftdGraph == null)
				{
					Assembly thisAssembly = Assembly.GetExecutingAssembly();
					tftdGraph = new Palette(thisAssembly.GetManifestResourceStream(embedPath+"tftd-graph.pal"));
				}
				return tftdGraph;
			}	
		}

		/// <summary>
		/// The TFTD Palette embedded in this assembly
		/// </summary>
		public static Palette TFTDResearch
		{
			get
			{
				if(tftdResearch == null)
				{
					Assembly thisAssembly = Assembly.GetExecutingAssembly();
					tftdResearch = new Palette(thisAssembly.GetManifestResourceStream(embedPath+"tftd-research.pal"));
					//tftdPal = new Palette(File.OpenRead(@"D:\Users\daishiva\xPck\tftd-battle.gif"));
				}
				return tftdResearch;
			}	
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

#if !DIRECTX
			Color old = cp.Entries[PckImage.TransparentIndex];
			cp.Entries[PckImage.TransparentIndex]=Color.FromArgb(0,old);
#endif
			b.Dispose();
		}

#if DIRECTX
		public Color Transparent
		{
			get{return cp.Entries[PckImage.TransparentIndex];}
		}
#else
			
		public bool Transparent
		{
			get
			{
				return cp.Entries[PckImage.TransparentIndex].A==0;
			}
			set
			{
				Color old = cp.Entries[PckImage.TransparentIndex];
				if(value)
					cp.Entries[PckImage.TransparentIndex]=Color.FromArgb(0,old);
				else
					cp.Entries[PckImage.TransparentIndex]=Color.FromArgb(255,old);
			}
		}
#endif

		public ColorPalette Colors
		{
			get{return cp;}
		}

		/// <summary>
		/// This palette's name
		/// </summary>
		public string Name
		{
			get
			{
				return name;
			}
		}

		/// <summary>
		/// Indexes colors on number
		/// </summary>
		public Color this[int i]
		{
			get{return cp.Entries[i];}
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
	}
}
