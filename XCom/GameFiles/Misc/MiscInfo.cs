using System;
using System.Drawing;
using System.Collections;
using System.IO;
using XCom.Interfaces;

namespace XCom
{
	public class MiscInfo
	{
		private Hashtable stuff;

		public MiscInfo()
		{
			stuff = new Hashtable();
		}

		public string UfoGraph
		{
			get{return (string)stuff["ufograph"];}
		}

		public string GeoGraph
		{
			get{return (string)stuff["geograph"];}
		}

		public string CursorFile
		{
			get
			{
				return (string)stuff["cursor"];
			}
			set{stuff["cursor"]=value;}
		}

		//public SCRImage GetSCR(string basename)
		//{
		//    return new SCRImage(GameInfo.DefaultPalette,File.OpenRead(((string)stuff["geograph"])+basename+".scr"),320,200);
		//}

#if !MAPEDIT
		public ConsoleArgs GetConsoleArgs()
		{
			return (ConsoleArgs)stuff["console"];
		}

		public string FontPath
		{
			get{return (string)stuff["font"];}
		}

		public string this[string keyword]
		{
			get{return(string)stuff[keyword];}		
		}

//		public IFont GetFont(string name,Color c)
//		{
//			if(stuff["font:"+name+c.GetHashCode()]==null)
//			{
//				stuff["font:"+name+c.GetHashCode()] = new xFont(name,(string)stuff["font"],c);
//			}
//			return (IFont)stuff["font:"+name+c.GetHashCode()];
//		}
#else
		public void SetCursor(string path)
		{
			stuff["cursor"] = path;
		}
#endif

		public MiscInfo(Stream s,VarCollection v):this()
		{
			StreamReader sr = new StreamReader(s);
			VarCollection vars = new VarCollection(sr,v);
			KeyVal line;

			while((line=vars.ReadLine())!=null)
			{
				switch(line.Keyword)
				{
					case "cursor":
						stuff["cursor"] = @line.Rest;
						break;
					case "font":
						stuff["font"] = @line.Rest;
						break;
					case "geograph":
						stuff["geograph"]=@line.Rest;
						break;
					case "ufograph":
						stuff["ufograph"]=@line.Rest;
						break;
#if !MAPEDIT
					case "console":
						stuff["console"] = new ConsoleArgs(vars,(string)stuff["font"],(string)stuff["geograph"]);
						break;
#endif
				}
			}
		}
	}
}
