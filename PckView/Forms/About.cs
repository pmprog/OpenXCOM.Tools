using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;

namespace PckView
{
	public partial class About : System.Windows.Forms.Form
	{
		public About()
		{
			InitializeComponent();

			FileVersionInfo info = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
#if DEBUG
			lblVer.Text = string.Format("Debug version {0},{1}", info.FileMajorPart, info.FileMinorPart);
#else
			lblVer.Text = string.Format("Release version {0},{1}", info.FileMajorPart, info.FileMinorPart);
#endif
		}
	}
}
