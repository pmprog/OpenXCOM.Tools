using System;
using System.Drawing;
using System.Windows.Forms;
using XCom;
using XCom.Interfaces;

namespace PckView
{
	/// <summary>
	/// Summary description for ButtonPanel.
	/// </summary>
	public class ButtonPanel:Panel
	{
		private XCImage img;
		private SinglePanel top;

		public ButtonPanel()
		{
			top = new SinglePanel();
			Controls.Add(top);
			top.Location=new Point(0,0);
		}

		public XCImage Image
		{
			get{return img;}
			set{img=value;top.Image = value;Width=top.Width;}
		}

		public int PreferredWidth
		{
			get{return top.Width;}
		}

		public int PreferredHeight
		{
			get
			{
				if(Parent!=null)
					return Parent.Height;
				return top.Height;
			}
		}

		public Palette Palette
		{
			set{top.Palette=value;}
		}
	}
}
