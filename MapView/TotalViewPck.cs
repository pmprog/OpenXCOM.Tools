using System;
using System.Windows.Forms;
using System.Drawing;
using XCom;
using System.Collections;

namespace PckView
{
	public class TotalViewPck:Panel
	{
		private ViewPck view;
		//private GameInfo images;
		private ByteView byteView;

		private System.Windows.Forms.ComboBox tileChooser;
		private System.Windows.Forms.VScrollBar scroll;
		private System.Windows.Forms.Label status;
		private System.Windows.Forms.CheckBox showBytes;

		private string[] showList;

		public TotalViewPck()
		{
			tileChooser = new ComboBox();
			scroll = new VScrollBar();
			status = new Label();
			showBytes = new CheckBox();

			tileChooser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			tileChooser.Size = new System.Drawing.Size(121, 21);
			tileChooser.SelectedIndexChanged += new System.EventHandler(this.tileChooser_SelectedIndexChanged);
			tileChooser.MaxDropDownItems=20;

			this.status.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.status.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.status.Location = new System.Drawing.Point(0, 250);
			this.status.Size = new System.Drawing.Size(293, 23);

			this.showBytes.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.showBytes.Location = new System.Drawing.Point(120, 0);
			this.showBytes.Size = new System.Drawing.Size(100, 21);
			this.showBytes.Text = "Show Bytes";
			this.showBytes.CheckedChanged += new System.EventHandler(this.showBytes_CheckedChanged);

			scroll.Dock = System.Windows.Forms.DockStyle.Right;
			scroll.Location = new System.Drawing.Point(294, 0);
			scroll.Size = new System.Drawing.Size(18, 273);
			scroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scroll_Scroll);

			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  showBytes,
																		  status,
																		  scroll,
																		  tileChooser});

			//this.images = images;
		
			view = new ViewPck();
			view.Location = new Point(0,tileChooser.Height);
			view.MouseThing+=new MouseStuff(viewClicked);
			scroll.Minimum = -tileChooser.Height;

			this.Controls.Add(view);
			OnResize(null);
		}

		public string[] List
		{
			set{showList=value;}
		}

		public bool ShowAll
		{
			set
			{/*
				tileChooser.Items.Clear();

				if(value)
				{
					foreach(string s in images.FileNames)
						tileChooser.Items.Add(s);
					tileChooser.SelectedIndex=0;
				}
				else
				{
					if(showList!=null)
					{
						ArrayList names = new ArrayList(showList);
						foreach(string s in images.FileNames)
						{
							if(names.Contains(s))
								tileChooser.Items.Add(s);
						}	
						tileChooser.SelectedIndex=0;
					}
				}*/
			}
		}


		protected override void OnResize(EventArgs e)
		{
			showBytes.Width = Width-tileChooser.Width-scroll.Width;
			status.Width = Width-scroll.Width;
			status.Top = Height-status.Height;
		}

		private void tileChooser_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			view.Pck = GameInfo.GetPckFile(tileChooser.SelectedItem.ToString());
			view.Refresh();
			scroll.Maximum = Math.Max((view.Height-Height+tileChooser.Height+50),scroll.Minimum);
			scroll.Value=scroll.Minimum;
			scroll_Scroll(null,null);
		}

		private void scroll_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
		{
			view.Location = new Point(0,-scroll.Value);
			view.Refresh();
		}

		private void viewClicked(int click, int move)
		{
			status.Text = "Selected: "+click+" Over: "+move;
			if(showBytes.Checked)
			{
				byteView.Image = view.Selected;
			}
		}

		private void showBytes_CheckedChanged(object sender, System.EventArgs e)
		{
			if(showBytes.Checked)
			{
				byteView = new ByteView(view.Selected);
				byteView.Show();
				byteView.Location = new Point(this.Parent.Right,this.Parent.Top);
				byteView.Disposed+=new EventHandler(bvDisposed);
			}
			else
			{
				byteView.Dispose();
			}
		}

		private void bvDisposed(object sender, EventArgs e)
		{
			showBytes.Checked=false;
		}
	}
}
