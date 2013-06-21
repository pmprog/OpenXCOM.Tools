using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using XCom;
using XCom.Interfaces;
using XCom.Interfaces.Base;


namespace MapView
{
	public delegate void BoolDelegate(bool val);
	public class MapViewPanel : Panel
	{
		private View view;
		private HScrollBar horiz;
		private VScrollBar vert;
		private Button saveBlank, runCalc;

		private CheckBox blankCheck, l0, l1, l2, l3;
		private CheckBox s0, s1, s2, s3;

		private GroupBox blankGroup;
		private GroupBox allBlank;

		public event BoolDelegate BlankChanged;

		private static MapViewPanel myInstance;
		private static Form blankForm;

		private MapViewPanel()
		{
			ImageUpdate += new EventHandler(update);

			horiz = new HScrollBar();
			vert = new VScrollBar();

			horiz.Scroll += new System.Windows.Forms.ScrollEventHandler(this.horiz_Scroll);
			horiz.Dock = DockStyle.Bottom;

			vert.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vert_Scroll);
			vert.Dock = DockStyle.Right;

			Controls.AddRange(new System.Windows.Forms.Control[] {
																	 this.vert,
																	 this.horiz});
			fillBlank();

			SetView(new View());

			allBlank = new GroupBox();
			allBlank.Text = "Blank info";

			blankCheck.Location = new Point(0, 0);
			blankGroup.Location = new Point(0, blankCheck.Bottom);
			blankGroup.Size = new Size(140, 65);
			saveBlank.Location = new Point(0, blankGroup.Bottom);
			runCalc.Location = new Point(0, saveBlank.Bottom);

			blankForm = new Form();
			blankForm.Text = "Blank Controls";
			blankForm.Size = new Size(150, 170);
			blankForm.MaximizeBox = false;
			blankForm.FormBorderStyle = FormBorderStyle.FixedSingle;
			blankForm.Controls.AddRange(new Control[] { blankCheck, blankGroup, saveBlank, runCalc });
			allBlank.Dock = DockStyle.Fill;

			blankCheck.Checked = false;
			OnResize(null);
		}

		public void SetView(View v)
		{
			if (view != null)
			{
				v.Map = view.Map;
				this.Controls.Remove(view);
			}

			view = v;

			view.Location = new Point(0, 0);
			view.BorderStyle = BorderStyle.Fixed3D;

			vert.Minimum = 0;
			vert.Value = vert.Minimum;

			view.Width = ClientSize.Width - vert.Width - 1;

			this.Controls.Add(view);
		}

		public void Cut_click(object sender, EventArgs e)
		{
			view.Copy();
			view.ClearSelection();
		}

		public void Copy_click(object sender, EventArgs e)
		{
			view.Copy();
		}

		public void Paste_click(object sender, EventArgs e)
		{
			view.Paste();
		}

		public Form BlankForm
		{
			get { return blankForm; }
		}

		public static MapViewPanel Instance
		{
			get
			{
				if (myInstance == null)
				{
					myInstance = new MapViewPanel();
					LogFile.Instance.WriteLine("Main view panel created");
				}
				return myInstance;
			}
		}

		private void fillBlank()
		{
			blankGroup = new GroupBox();
			blankGroup.Text = "Visible levels";
			blankGroup.Enabled = false;

			blankCheck = new CheckBox();
			blankCheck.Text = "Show blank";
			blankCheck.CheckedChanged += new EventHandler(checkChange);

			//int wid=30;
			int y = 12;

			l0 = makeCheck("0", new Point(5, y), new EventHandler(setDraw));
			l1 = makeCheck("1", new Point(l0.Right, y), new EventHandler(setDraw));
			l2 = makeCheck("2", new Point(l1.Right, y), new EventHandler(setDraw));
			l3 = makeCheck("3", new Point(l2.Right, y), new EventHandler(setDraw));

			s0 = makeCheck("0", new Point(5, l0.Bottom), new EventHandler(setVisible));
			s1 = makeCheck("1", new Point(s0.Right, l1.Bottom), new EventHandler(setVisible));
			s2 = makeCheck("2", new Point(s1.Right, l2.Bottom), new EventHandler(setVisible));
			s3 = makeCheck("3", new Point(s2.Right, l3.Bottom), new EventHandler(setVisible));

			saveBlank = new Button();
			saveBlank.Text = "Save Blank";
			saveBlank.Click += new EventHandler(saveBlankClick);

			runCalc = new Button();
			runCalc.Text = "Run Calc";
			runCalc.Click += new EventHandler(runCalcClick);

			blankGroup.Controls.AddRange(new Control[] { l0, l1, l2, l3, s0, s1, s2, s3 });
		}

		private CheckBox makeCheck(string text, Point location, EventHandler evt)
		{
			CheckBox c = new CheckBox();
			c.Text = text;
			c.Location = location;
			c.Width = 30;
			c.Checked = true;
			c.CheckedChanged += evt;
			return c;
		}

		private void setVisible(object sender, EventArgs e)
		{
			if (sender == s0)
			{
				view.Vis[0] = !s0.Checked;
			}
			else if (sender == s1)
			{
				view.Vis[1] = !s1.Checked;
			}
			else if (sender == s2)
			{
				view.Vis[2] = !s2.Checked;
			}
			else //l3
			{
				view.Vis[3] = !s3.Checked;
			}
		}

		private void runCalcClick(object sender, EventArgs e)
		{
			if (view.Map is XCMapFile)
				((XCMapFile)view.Map).CalcDrawAbove();
		}

		private void saveBlankClick(object sender, EventArgs e)
		{
			if (view.Map is XCMapFile)
				((XCMapFile)view.Map).SaveBlanks();
		}

		private void setDraw(object sender, EventArgs e)
		{
			if (sender == l0)
			{
				view.Draw[0] = !l0.Checked;
			}
			else if (sender == l1)
			{
				view.Draw[1] = !l1.Checked;
			}
			else if (sender == l2)
			{
				view.Draw[2] = !l2.Checked;
			}
			else //l3
			{
				view.Draw[3] = !l3.Checked;
			}
		}

		private void checkChange(object sender, EventArgs e)
		{
			blankGroup.Enabled = blankCheck.Checked;
			view.DrawAll = !blankCheck.Checked;

			if (BlankChanged != null)
				BlankChanged(blankCheck.Checked);
		}

		public IMap_Base Map
		{
			get { return view.Map; }
		}

		private void update(object sender, EventArgs e)
		{
			view.Refresh();
		}

		private void up_click(object sender, EventArgs e)
		{
			view.Map.Up();
			view.Focus();
		}

		private void down_click(object sender, EventArgs e)
		{
			view.Map.Down();
			view.Focus();
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			vert.Value = vert.Minimum;
			horiz.Value = horiz.Minimum;
			vert_Scroll(null, null);
			horiz_Scroll(null, null);

			int h = 0, w = 0;

			if (vert.Visible = (view.Height > ClientSize.Height))
			{
				vert.Maximum = view.Height - ClientSize.Height + horiz.Height;
				w = vert.Width;
			}
			else
				horiz.Width = ClientSize.Width;

			if (horiz.Visible = (view.Width > ClientSize.Width))
			{
				horiz.Maximum = Math.Max((view.Width - ClientSize.Width + vert.Width), horiz.Minimum);
				h = horiz.Height;
			}
			else
				vert.Height = ClientSize.Height;

			view.Viewable = new Size(Width - w, Height - h);
			view.Refresh();
		}

		private void vert_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
		{
			view.Location = new Point(view.Left, -(vert.Value) + 1);
			view.NewLeft = true;
			view.Refresh();
		}

		private void horiz_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
		{
			view.Location = new Point(-(horiz.Value), view.Top);
			view.NewLeft = true;
			view.Refresh();
		}

		//public void SetMap(XCMapDesc map)
		//{
		//    view.Map = map.GetMapFile();
		//    view.Focus();
		//    OnResize(null);
		//}

		public void SetMap(IMap_Base map)
		{
			view.Map = map;
			view.Focus();
			OnResize(null);
		}

		public void ForceResize()
		{
			OnResize(null);
		}

		public View View
		{
			get { return view; }
		}

		/*** Timer stuff ***/
		private static int current;
		private static Timer timer;
		private static bool started;
		public static event EventHandler ImageUpdate;

		public static void Start()
		{
			if (timer == null)
			{
				timer = new Timer();
				timer.Interval = 100;
				timer.Tick += new EventHandler(tick);
				timer.Start();
				started = true;
			}

			if (!started)
			{
				timer.Start();
				started = true;
			}
		}

		public static void Stop()
		{
			if (timer == null)
			{
				timer = new Timer();
				timer.Interval = 100;
				timer.Tick += new EventHandler(tick);
				started = false;
			}

			if (started)
			{
				timer.Stop();
				started = false;
			}
		}

		public static bool Updating
		{
			get { return started; }
		}

		public static int Interval
		{
			get { return timer.Interval; }
			set { timer.Interval = value; }
		}

		private static void tick(object sender, EventArgs e)
		{
			current = (current + 1) % 8;

			if (ImageUpdate != null)
				ImageUpdate(null, null);
		}

		public static int Current
		{
			get { return current; }
			set { current = value; }
		}
	}
}
