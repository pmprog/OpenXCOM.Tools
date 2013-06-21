#define win98

using System;
using System.Windows.Forms;

namespace MapView
{
	public class DirectoryChooser
	{
#if(!win98)
		private DirPicker dp;
#else
		private System.Windows.Forms.OpenFileDialog openFile;
#endif
		public DirectoryChooser()
		{
#if(win98)
			openFile = new System.Windows.Forms.OpenFileDialog();
			openFile.RestoreDirectory = true;
#else
			dp = new DirPicker();
#endif			
		}

		public string InitialDirectory
		{
			set
			{
#if(win98)
				openFile.InitialDirectory=value;
#else
				dp.CurrentPath=value;
#endif	
			}
		}

		public string ShowDialog(IWin32Window parent,string type)
		{
#if(win98)
			openFile.Title="Select any file in the "+type+" directory";
			if(openFile.ShowDialog(parent)==DialogResult.OK)
				return openFile.FileName.Substring(0,openFile.FileName.LastIndexOf(@"\")+1);
#else
			dp.Text="Select the "+type+" directory";
			dp.ShowDialog(parent);
			if(!dp.CancelClicked)
				return dp.CurrentPath;
#endif			
			return null;
		}
	}
}
