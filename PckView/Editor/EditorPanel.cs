using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using XCom;
using XCom.Interfaces;

namespace PckView
{
	public class EditorPanel :Panel
	{
		private EditorPane editor;

		public EditorPanel(XCImage img)
		{
			editor = new EditorPane(img);
			editor.Location=new Point(0,0);
			Controls.Add(editor);
		}

		public EditorPane Editor
		{
			get{return editor;}
		}

		protected override void OnResize(EventArgs e)
		{
			editor.Size=ClientSize;
		}
	}
}
