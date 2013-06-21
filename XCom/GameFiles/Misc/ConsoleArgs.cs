#if !MAPEDIT
using System;
using XCom.Interfaces;
using System.Drawing;

namespace XCom
{
	public class ConsoleArgs
	{
		private string logPath=null;
		private string fontPath;
		private string backPath;
		private string myFont=null;
		//private XCImage background=null;
		private Color fontColor;

		public string LogPath
		{
			get{return logPath;}
		}

//		public IFont Font
//		{
//			get
//			{
//				if(myFont==null)
//					myFont = new xFont("",fontPath,fontColor);
//				return myFont;
//			}
//		}

		//public XCImage Background
		//{
		//    get
		//    {
		//        if(background==null)
		//            background=new SCRImage(GameInfo.DefaultPalette,backPath);
		//        return background;
		//    }
		//}

		public ConsoleArgs(VarCollection vars,string fontPath,string backPath)
		{
			KeyVal line;

			fontColor = Color.Lavender;
			while((line=vars.ReadLine())!=null)
			{
				switch(line.Keyword)
				{
					case "end":
						return;
					case "logfile":
						logPath=@line.Rest;
						break;
					case "font":
						myFont=fontPath+line.Rest;
						break;
					case "color":
						string[] val = line.Rest.Split(',');
						fontColor = Color.FromArgb(int.Parse(val[0]),int.Parse(val[1]),int.Parse(val[2]));
						break;
					case "back":
						this.backPath=backPath+line.Rest;
						break;
				}
			}
		}
	}
}
#endif
