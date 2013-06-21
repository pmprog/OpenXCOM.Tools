using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

//http://www.vbdotnetheaven.com/Code/May2004/MultiColListViewMCB.asp

namespace PckView
{
	public partial class ModForm : System.Windows.Forms.Form
	{
		private XCom.SharedSpace space;

		public ModForm()
		{
			InitializeComponent();

			DSShared.Windows.RegistryInfo ri = new DSShared.Windows.RegistryInfo(this);
		}

		public XCom.SharedSpace SharedSpace
		{
			set
			{
				space = value;
				foreach (XCom.Interfaces.IXCImageFile xcf in space.GetImageModList())
				{
					if (xcf.FileExtension == ".bad" && xcf.Author == "Author" && xcf.Description == "Description")
						modList.Items.Add(new ListViewItem(new string[] { xcf.FileExtension, xcf.Author, xcf.GetType().ToString() }));
					else
						modList.Items.Add(new ListViewItem(new string[] { xcf.FileExtension, xcf.Author, xcf.Description }));
				}
			}
		}
	}
}
