using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DSShared.Windows;

namespace XCom
{
	public partial class ConsoleForm : Form
	{
		public ConsoleForm()
		{
			InitializeComponent();

			XCom.xConsole.Init(100);
			XCom.xConsole.BufferChanged += new BufferChangedDelegate(xConsole_BufferChanged);

			new RegistryInfo(this);

			SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
		}

		void xConsole_BufferChanged(Node current)
		{			
			
			string buffer = current.str+"\n";
			Node curr = current.next;

			while (current != curr)
			{
				buffer = buffer + curr.str + "\n";
				curr = curr.next;
			}

			consoleText.Text = buffer;
			Refresh();
			
			//consoleText.Text += current.str+"\n";
		}

		private void miClose_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}