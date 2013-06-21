using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using XCom;
using XCom.Interfaces;

namespace PckView
{
	public class Editor : System.Windows.Forms.Form
	{
		public event EventHandler PalViewClosing;

		private System.ComponentModel.Container components = null;
		private EditorPanel edit;
		private PalView palView;
		private System.Windows.Forms.MainMenu menu;
		private System.Windows.Forms.MenuItem paletteMain;
		private System.Windows.Forms.MenuItem showPalette;
		private ButtonPanel buttons;
		private System.Windows.Forms.MenuItem linesItem;
		private System.Windows.Forms.MenuItem showLines;
		private TrackBar size;

		public Editor(PckImage curr)
		{
			edit = new EditorPanel(curr);
			buttons = new ButtonPanel();
			size = new TrackBar();

			size.Minimum=1;
			size.Maximum=10;

			InitializeComponent();

			Controls.Add(edit);
			Controls.Add(buttons);
			Controls.Add(size);
            
			buttons.Location = new Point(0,0);
			buttons.Width = buttons.PreferredWidth;

			size.Left=buttons.Right;
			size.Top=buttons.Top;
			edit.Top=size.Bottom;
			edit.Left=buttons.Right;

			ClientSize=new Size(buttons.PreferredWidth+edit.Editor.PreferredWidth,
								edit.Editor.PreferredHeight+size.Height);

			palView = new PalView();
			palView.Closing+=new CancelEventHandler(palClose);

			palView.PaletteIndexChanged+=new PaletteClickDelegate(edit.Editor.SelectColor);
			size.Scroll+=new EventHandler(sizeScroll);
		}

		private void sizeScroll(object sender, EventArgs e)
		{
			edit.Editor.ScaleVal=size.Value;
		}

		public Palette Palette
		{
			get{return edit.Editor.Palette;}
			set{edit.Editor.Palette=value;palView.Palette = value;buttons.Palette=value;}
		}

		public void ShowPalView()
		{
			if(palView.Visible)
				palView.BringToFront();
			else
			{
				palView.Left = Right;
				palView.Top = Top;
				palView.Show();
			}
			showPalette.Checked=true;
		}

		private void palClose(object sender, CancelEventArgs e)
		{
			e.Cancel=true;
			palView.Hide();
			showPalette.Checked=false;

			if(PalViewClosing!=null)
				PalViewClosing(this,new EventArgs());
		}

		protected override void OnResize(EventArgs e)
		{
			edit.Width = ClientSize.Width-buttons.PreferredWidth;
			edit.Height=ClientSize.Height-size.Height;
			buttons.Height = ClientSize.Height;
			size.Width=edit.Width;			

			edit.Left=buttons.Right;
			size.Left=edit.Left;
		}

		public XCImage CurrImage
		{
			get{return edit.Editor.Image;}
			set{edit.Editor.Image=value;buttons.Image = value;OnResize(null);}
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.menu = new System.Windows.Forms.MainMenu();
			this.paletteMain = new System.Windows.Forms.MenuItem();
			this.showPalette = new System.Windows.Forms.MenuItem();
			this.linesItem = new System.Windows.Forms.MenuItem();
			this.showLines = new System.Windows.Forms.MenuItem();
			// 
			// menu
			// 
			this.menu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																				 this.paletteMain,
																				 this.linesItem});
			// 
			// paletteMain
			// 
			this.paletteMain.Index = 0;
			this.paletteMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						this.showPalette});
			this.paletteMain.Text = "Palette";
			// 
			// showPalette
			// 
			this.showPalette.Index = 0;
			this.showPalette.Text = "Show";
			this.showPalette.Click += new System.EventHandler(this.showPalette_Click);
			// 
			// linesItem
			// 
			this.linesItem.Index = 1;
			this.linesItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.showLines});
			this.linesItem.Text = "Lines";
			// 
			// showLines
			// 
			this.showLines.Index = 0;
			this.showLines.Text = "Show";
			this.showLines.Click += new System.EventHandler(this.showLines_Click);
			// 
			// Editor
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Menu = this.menu;
			this.Name = "Editor";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Editor";

		}
		#endregion

		private void showPalette_Click(object sender, System.EventArgs e)
		{
			if(showPalette.Checked)
				palView.Close();
			else
				ShowPalView();
		}

		private void showLines_Click(object sender, System.EventArgs e)
		{
			showLines.Checked=!showLines.Checked;
			edit.Editor.Lines=showLines.Checked;
		}
	}
}
