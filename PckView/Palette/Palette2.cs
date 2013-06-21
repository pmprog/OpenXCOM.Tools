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

		private static Palette ufoPal;
		private static Palette tftdPal;

		private static readonly char COMMENT='#';

		/// <summary>
		/// The UFO Palette embedded in this assembly
		/// </summary>
		public static Palette UFOPalette
		{
			get
			{
				if(ufoPal == null)
				{
					Assembly thisAssembly = Assembly.GetExecutingAssembly();
					ufoPal = new Palette(thisAssembly.GetManifestResourceStream("PckView.ufo-battle.pal"));
				}
				return ufoPal;
			}	
		}

		/// <summary>
		/// The TFTD Palette embedded in this assembly
		/// </summary>
		public static Palette TFTDPalette
		{
			get
			{
				if(tftdPal == null)
				{
					Assembly thisAssembly = Assembly.GetExecutingAssembly();
					tftdPal = new Palette(thisAssembly.GetManifestResourceStream("PckView.tftd-battle.pal"));
				}
				return tftdPal;
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
			Color old = cp.Entries[PckImage.TRANSPARENT_COLOR_INDEX];
			cp.Entries[PckImage.TRANSPARENT_COLOR_INDEX]=Color.FromArgb(0,old);
			b.Dispose();
		}

		public bool Transparent
		{
			get
			{
				return cp.Entries[PckImage.TRANSPARENT_COLOR_INDEX].A==0;
			}
			set
			{
				Color old = cp.Entries[PckImage.TRANSPARENT_COLOR_INDEX];
				if(value)
					cp.Entries[PckImage.TRANSPARENT_COLOR_INDEX]=Color.FromArgb(0,old);
				else
					cp.Entries[PckImage.TRANSPARENT_COLOR_INDEX]=Color.FromArgb(255,old);
			}
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
