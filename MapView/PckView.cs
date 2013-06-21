using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using XCom;

namespace PckView
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class PckView : System.Windows.Forms.Form
	{
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.MainMenu main;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem mapOnlyItem;
		private System.Windows.Forms.MenuItem allItem;
		private TotalViewPck v;

		public PckView()
		{
			InitializeComponent();
			v = new TotalViewPck();
			v.Dock = DockStyle.Fill;
			this.Controls.Add(v);
		}

		public string[] List
		{
			set{v.List = value;v.ShowAll=false;}
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			this.main = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.mapOnlyItem = new System.Windows.Forms.MenuItem();
			this.allItem = new System.Windows.Forms.MenuItem();
			// 
			// main
			// 
			this.main.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																				 this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.mapOnlyItem,
																					  this.allItem});
			this.menuItem1.Text = "&Images";
			// 
			// mapOnlyItem
			// 
			this.mapOnlyItem.Checked = true;
			this.mapOnlyItem.Index = 0;
			this.mapOnlyItem.Text = "&Map Only";
			this.mapOnlyItem.Click += new System.EventHandler(this.mapOnlyItem_Click);
			// 
			// allItem
			// 
			this.allItem.Index = 1;
			this.allItem.Text = "&All";
			this.allItem.Click += new System.EventHandler(this.allItem_Click);
			// 
			// PckView
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(312, 273);
			this.Menu = this.main;
			this.Name = "PckView";
			this.Text = "PckView";

		}
		#endregion

		private void mapOnlyItem_Click(object sender, System.EventArgs e)
		{
			mapOnlyItem.Checked=true;
			allItem.Checked=false;

			v.ShowAll=allItem.Checked;
		}

		private void allItem_Click(object sender, System.EventArgs e)
		{
			mapOnlyItem.Checked=false;
			allItem.Checked=true;

			v.ShowAll=allItem.Checked;
		}
	}
}
