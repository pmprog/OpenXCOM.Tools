/*using System;
using XCom.Interfaces;

namespace XCom.GameFiles.Images.xcFiles
{
	public class xcPckX1:IXCFile
	{
		public xcPckX1():base(128,64)
		{
			desc[0]=FileExtension;
			desc[1]="Ben Ratzlaff";
			desc[2]="Opens x1.pck";		

			fileOptions.CustomDialog=false;
			fileOptions.OpenDialog = false;
		}

		public override string FileExtension{get{return ".pck";}}
		public override bool RegisterFile() { return true; }

		public override string ExplorerDescription{get{return "Explosion Images";}}

		public override string SingleFileName{get{return "x1.pck";}}

		protected override XCImageCollection LoadFileOverride(string directory,string file,int imgWid,int imgHei)
		{
			PckImage.Width=128;
			PckImage.Height=64;

			string tabBase = file.Substring(0,file.LastIndexOf("."));

			return new PckFile(System.IO.File.OpenRead(directory+"\\"+file),
				System.IO.File.OpenRead(directory+"\\"+tabBase+PckFile.TAB_EXT),2,DefaultPalette,ImageSize.Height,ImageSize.Width);
		}

		public override void SaveCollection(string directory, string file,XCImageCollection images)
		{
			PckFile.Save(directory,file,images,2);
		}
	}
}
*/