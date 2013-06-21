using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using XCom;
using XCom.Interfaces;

namespace PckView
{
	public delegate void XCImageCollectionHandler(object sender, XCImageCollectionSetEventArgs e);

	public class TotalViewPck : Panel
	{
		private ViewPck view;

		private System.Windows.Forms.VScrollBar scroll;
		private StatusBar statusBar;
		private StatusBarPanel statusOverTile, statusBPP;
		private int click, move;

		public event PckViewMouseClicked ViewClicked;
		public event XCImageCollectionHandler XCImageCollectionSet;

		private static TotalViewPck myInstance;

		public TotalViewPck()
		{
			scroll = new VScrollBar();

			statusBar = new StatusBar();
			statusOverTile = new StatusBarPanel();
			statusOverTile.AutoSize = StatusBarPanelAutoSize.Spring;
			statusBar.Panels.Add(statusOverTile);
			statusBar.ShowPanels = true;
			statusBar.Dock = DockStyle.Bottom;

			statusBPP = new StatusBarPanel();
			statusBPP.AutoSize = StatusBarPanelAutoSize.Contents;
			statusBPP.Width = 50;
			statusBPP.Alignment = HorizontalAlignment.Right;
			statusBar.Panels.Add(statusBPP);

			scroll.Dock = System.Windows.Forms.DockStyle.Right;
			scroll.Maximum = 5000;
			scroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scroll_Scroll);

			view = new ViewPck();
			view.Location = new Point(0, 0);
			view.ViewClicked += new PckViewMouseClicked(viewClicked);
			view.ViewMoved += new PckViewMouseMoved(viewMoved);
			view.Dock = DockStyle.Fill;
			view.ViewClicked += new PckViewMouseClicked(viewClik);
			scroll.Minimum = 0;

			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  statusBar,
																		  scroll,view});

			view.SizeChanged += new EventHandler(viewSizeChange);
			OnResize(null);
		}

		public ViewPck View
		{
			get { return view; }
		}

		public static TotalViewPck Instance
		{
			get
			{
				if (myInstance == null)
					myInstance = new TotalViewPck();
				return myInstance;
			}
		}

		private void viewClik(int idx)
		{
			if (ViewClicked != null)
				ViewClicked(idx);
		}

		public Palette Pal
		{
			get
			{
				return view != null ? view.Pal : null;
			}
			set
			{
				if (view != null)
				{
					view.Pal = value;
					Console.WriteLine("Pal set: " + value.ToString());
				}
			}
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);

			if (view.PreferredHeight >= Height)
			{
				scroll.Visible = true;
				scroll.Maximum = view.PreferredHeight - Height + 50;
			}
			else
				scroll.Visible = false;
		}

		public XCImage Selected
		{
			get { return view.Selected; }
			set { view.Selected = value; }
		}

		public XCImageCollection Collection
		{
			get { return view.Collection; }
			set
			{
				try
				{
					view.Collection = value;
					if (value is PckFile)
						statusBPP.Text = "Bpp: " + ((PckFile)view.Collection).Bpp + "  ";
					else
						statusBPP.Text = "";
					if (XCImageCollectionSet != null)
						XCImageCollectionSet(this, new XCImageCollectionSetEventArgs(value));
				}
				catch(Exception e)
				{
					if (XCImageCollectionSet != null)
						XCImageCollectionSet(this, new XCImageCollectionSetEventArgs(null));

					throw e;
				}
			}
		}

		private void viewSizeChange(object sender, EventArgs e)
		{
			scroll.Value = scroll.Minimum;
			view.StartY = -scroll.Value;
			if (view.PreferredHeight >= Height)
			{
				scroll.Visible = true;
				scroll.Maximum = view.PreferredHeight - Height;
			}
			else
				scroll.Visible = false;
		}

		private void tileChooser_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//view.Pck = ImageCollection.GetPckFile(tileChooser.SelectedItem.ToString());
			view.Refresh();
			//scroll.Maximum = Math.Max((view.Height-Height+tileChooser.Height+50),scroll.Minimum);
			scroll.Value = scroll.Minimum;
			scroll_Scroll(null, null);
		}

		private void scroll_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
		{
			view.StartY = -scroll.Value;
			view.Refresh();
		}

		private void viewClicked(int x)
		{
			click = x;
			statusOverTile.Text = "Selected: " + click + " Over: " + move;
		}

		private void viewMoved(int x)
		{
			move = x;
			statusOverTile.Text = "Selected: " + click + " Over: " + move;
		}

		public void Hq2x()
		{
			view.Hq2x();
		}
	}

	public class XCImageCollectionSetEventArgs
	{
		private XCImageCollection collection;

		public XCImageCollectionSetEventArgs(XCImageCollection collection)
		{
			this.collection = collection;
		}

		public XCImageCollection Collection
		{
			get { return collection; }
		}
	}
}
