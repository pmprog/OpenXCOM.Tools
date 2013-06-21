/*
using System;
using XCom.Interfaces;

namespace XCom.GameFiles.Images.xcFiles
{
	public class xcPckBigob:IXCFile
	{
		private const string TAB_EXT = ".tab";

		public xcPckBigob():base(32,48)
		{
			FileOptions.SaveDialog=false;
			FileOptions.OpenDialog=false;
			FileOptions.CustomDialog=false;

			ext = ".pck";
			author = "Ben Ratzlaff";
			desc ="Opens bigobs.pck. Save as a generic pck file";

			expDesc = "Inventory Images";
		}

		public override bool RegisterFile() { return true; }

		public override string SingleFileName{get{return "bigobs*.pck";}}

		protected override XCImageCollection LoadFileOverride(string directory,string file,int imgWid,int imgHei)
		{
			//single files get file+ext sent to this method
			string fName = file.Substring(0,file.LastIndexOf("."));
			try	
			{
				return new PckFile(System.IO.File.OpenRead(directory+"\\"+file),
					System.IO.File.OpenRead(directory+"\\"+fName+TAB_EXT),2,DefaultPalette,ImageSize.Height,ImageSize.Width);
			}
			catch
			{
				return new PckFile(System.IO.File.OpenRead(directory+"\\"+file),
					System.IO.File.OpenRead(directory+"\\"+fName+TAB_EXT),4,DefaultPalette,ImageSize.Height,ImageSize.Width);
			}
		}
	}
}
*/