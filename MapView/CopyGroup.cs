/*using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using XCom;

namespace MapView
{
	/// <summary>
	/// Summary description for CopyForm.
	/// </summary>
	public class CopyGroup : GroupBox
	{
		private System.Windows.Forms.Button btnPaste;
		private System.Windows.Forms.GroupBox level4Group;
		private System.Windows.Forms.CheckBox checkBox1; //ground
		private System.Windows.Forms.CheckBox checkBox2; //west
		private System.Windows.Forms.CheckBox checkBox3; //content
		private System.Windows.Forms.CheckBox checkBox4; //north

		private System.Windows.Forms.GroupBox level3Group;
		private System.Windows.Forms.CheckBox checkBox5; //ground
		private System.Windows.Forms.CheckBox checkBox6; //content
		private System.Windows.Forms.CheckBox checkBox7; //west
		private System.Windows.Forms.CheckBox checkBox8; //north

		private System.Windows.Forms.GroupBox level2Group;
		private System.Windows.Forms.CheckBox checkBox9; //ground
		private System.Windows.Forms.CheckBox checkBox10; //content
		private System.Windows.Forms.CheckBox checkBox11; //west
		private System.Windows.Forms.CheckBox checkBox12; //north

		private System.Windows.Forms.GroupBox level1Group;
		private System.Windows.Forms.CheckBox checkBox13; //ground
		private System.Windows.Forms.CheckBox checkBox14; //content
		private System.Windows.Forms.CheckBox checkBox15; //west
		private System.Windows.Forms.CheckBox checkBox16; //north

		private System.Windows.Forms.Button btnCopy;
		private Hashtable groupCheckHash;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

//		private static CopyGroup instance;

		public CopyGroup()
		{
			InitializeComponent();
			groupCheckHash = new Hashtable();
			for(int i=1;i<5;i++)
				groupCheckHash[i]=new Hashtable();

			Hashtable h = groupCheckHash[4] as Hashtable;
			h["group"]=level4Group;
			h[XCMapTile.MapQuadrant.Ground] = checkBox1;
            h[XCMapTile.MapQuadrant.West] = checkBox2;
            h[XCMapTile.MapQuadrant.Content] = checkBox3;
            h[XCMapTile.MapQuadrant.North] = checkBox4;

			h = groupCheckHash[3] as Hashtable;
			h["group"]=level3Group;
            h[XCMapTile.MapQuadrant.Ground] = checkBox5;
            h[XCMapTile.MapQuadrant.Content] = checkBox6;
            h[XCMapTile.MapQuadrant.West] = checkBox7;
            h[XCMapTile.MapQuadrant.North] = checkBox8;

			h = groupCheckHash[2] as Hashtable;
			h["group"]=level2Group;
            h[XCMapTile.MapQuadrant.Ground] = checkBox9;
            h[XCMapTile.MapQuadrant.Content] = checkBox10;
            h[XCMapTile.MapQuadrant.West] = checkBox11;
            h[XCMapTile.MapQuadrant.North] = checkBox12;

			h = groupCheckHash[1] as Hashtable;
			h["group"]=level1Group;
            h[XCMapTile.MapQuadrant.Ground] = checkBox13;
            h[XCMapTile.MapQuadrant.Content] = checkBox14;
            h[XCMapTile.MapQuadrant.West] = checkBox15;
            h[XCMapTile.MapQuadrant.North] = checkBox16;

			foreach(int key in groupCheckHash.Keys)
			{
				foreach(object o in (groupCheckHash[key]as Hashtable).Keys)
                    if (o is XCMapTile.MapQuadrant)
						((groupCheckHash[key]as Hashtable)[o] as CheckBox).Checked=true;

			}
//
//			if(instance==null)
//				instance=this;
		}

//		public static CopyGroup Instance
//		{
//			get
//			{
//				if(instance==null)
//					instance = new CopyGroup();
//				return instance;
//			}
//		}

		public void HeightChanged(XCom.Interfaces.Base.IMap_Base mapFile,XCom.Interfaces.Base.HeightChangedEventArgs e)
		{
			int maxLevel = mapFile.MapSize.Height;

			for(int i=1;i<5;i++)
				((groupCheckHash[i] as Hashtable)["group"] as GroupBox).Enabled=i<=(maxLevel-e.NewHeight);
		}

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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnCopy = new System.Windows.Forms.Button();
			this.btnPaste = new System.Windows.Forms.Button();
			this.level4Group = new System.Windows.Forms.GroupBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.checkBox4 = new System.Windows.Forms.CheckBox();
			this.level3Group = new System.Windows.Forms.GroupBox();
			this.checkBox5 = new System.Windows.Forms.CheckBox();
			this.checkBox6 = new System.Windows.Forms.CheckBox();
			this.checkBox7 = new System.Windows.Forms.CheckBox();
			this.checkBox8 = new System.Windows.Forms.CheckBox();
			this.level2Group = new System.Windows.Forms.GroupBox();
			this.checkBox9 = new System.Windows.Forms.CheckBox();
			this.checkBox10 = new System.Windows.Forms.CheckBox();
			this.checkBox11 = new System.Windows.Forms.CheckBox();
			this.checkBox12 = new System.Windows.Forms.CheckBox();
			this.level1Group = new System.Windows.Forms.GroupBox();
			this.checkBox13 = new System.Windows.Forms.CheckBox();
			this.checkBox14 = new System.Windows.Forms.CheckBox();
			this.checkBox15 = new System.Windows.Forms.CheckBox();
			this.checkBox16 = new System.Windows.Forms.CheckBox();
			this.level4Group.SuspendLayout();
			this.level3Group.SuspendLayout();
			this.level2Group.SuspendLayout();
			this.level1Group.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnCopy
			// 
			this.btnCopy.Name = "btnCopy";
			this.btnCopy.TabIndex = 0;
			this.btnCopy.Text = "Copy";
			this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
			this.btnCopy.Location=new Point(3,14);
			// 
			// btnPaste
			// 
			this.btnPaste.Location = new System.Drawing.Point(0, 24);
			this.btnPaste.Name = "btnPaste";
			this.btnPaste.TabIndex = 1;
			this.btnPaste.Text = "Paste";
			this.btnPaste.Click+=new System.EventHandler(this.btnPaste_Click);
			this.btnPaste.Location=new Point(btnCopy.Left,btnCopy.Bottom);
			// 
			// level4Group
			// 
			this.level4Group.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.checkBox1,
																					this.checkBox3,
																					this.checkBox2,
																					this.checkBox4});
			this.level4Group.Location = new System.Drawing.Point(btnCopy.Right+2,8);
			this.level4Group.Name = "level4Group";
			this.level4Group.Size = new System.Drawing.Size(144, 56);
			this.level4Group.TabIndex = 2;
			this.level4Group.TabStop = false;
			this.level4Group.Text = "Level 4";
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(8, 16);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(64, 16);
			this.checkBox1.TabIndex = 3;
			this.checkBox1.Text = "ground";
			// 
			// checkBox3
			// 
			this.checkBox3.Location = new System.Drawing.Point(72, 16);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(64, 16);
			this.checkBox3.TabIndex = 5;
			this.checkBox3.Text = "content";
			// 
			// checkBox2
			// 
			this.checkBox2.Location = new System.Drawing.Point(72, 32);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(64, 16);
			this.checkBox2.TabIndex = 4;
			this.checkBox2.Text = "west";
			// 
			// checkBox4
			// 
			this.checkBox4.Location = new System.Drawing.Point(8, 32);
			this.checkBox4.Name = "checkBox4";
			this.checkBox4.Size = new System.Drawing.Size(64, 16);
			this.checkBox4.TabIndex = 6;
			this.checkBox4.Text = "north";			
			// 
			// groupBox3
			// 
			this.level3Group.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.checkBox9,
																					this.checkBox10,
																					this.checkBox11,
																					this.checkBox12});
			this.level3Group.Location = new System.Drawing.Point(level4Group.Left, level4Group.Bottom);
			this.level3Group.Name = "level3Group";
			this.level3Group.Size = new System.Drawing.Size(144, 56);
			this.level3Group.TabIndex = 8;
			this.level3Group.TabStop = false;
			this.level3Group.Text = "Level 3";
			// 
			// checkBox9
			// 
			this.checkBox9.Location = new System.Drawing.Point(8, 16);
			this.checkBox9.Name = "checkBox9";
			this.checkBox9.Size = new System.Drawing.Size(64, 16);
			this.checkBox9.TabIndex = 3;
			this.checkBox9.Text = "ground";
			// 
			// checkBox10
			// 
			this.checkBox10.Location = new System.Drawing.Point(72, 16);
			this.checkBox10.Name = "checkBox10";
			this.checkBox10.Size = new System.Drawing.Size(64, 16);
			this.checkBox10.TabIndex = 5;
			this.checkBox10.Text = "content";
			// 
			// checkBox11
			// 
			this.checkBox11.Location = new System.Drawing.Point(72, 32);
			this.checkBox11.Name = "checkBox11";
			this.checkBox11.Size = new System.Drawing.Size(64, 16);
			this.checkBox11.TabIndex = 4;
			this.checkBox11.Text = "west";
			// 
			// checkBox12
			// 
			this.checkBox12.Location = new System.Drawing.Point(8, 32);
			this.checkBox12.Name = "checkBox12";
			this.checkBox12.Size = new System.Drawing.Size(64, 16);
			this.checkBox12.TabIndex = 6;
			this.checkBox12.Text = "north";
			// 
			// groupBox2
			// 
			this.level2Group.Controls.AddRange(new System.Windows.Forms.Control[] {
																					  this.checkBox5,
																					  this.checkBox6,
																					  this.checkBox7,
																					  this.checkBox8});
			this.level2Group.Location = new System.Drawing.Point(level3Group.Left,level3Group.Bottom);
			this.level2Group.Name = "level2Group";
			this.level2Group.Size = new System.Drawing.Size(144, 56);
			this.level2Group.TabIndex = 7;
			this.level2Group.TabStop = false;
			this.level2Group.Text = "Level 2";
			// 
			// checkBox5
			// 
			this.checkBox5.Location = new System.Drawing.Point(8, 16);
			this.checkBox5.Name = "checkBox5";
			this.checkBox5.Size = new System.Drawing.Size(64, 16);
			this.checkBox5.TabIndex = 3;
			this.checkBox5.Text = "ground";
			// 
			// checkBox6
			// 
			this.checkBox6.Location = new System.Drawing.Point(72, 16);
			this.checkBox6.Name = "checkBox6";
			this.checkBox6.Size = new System.Drawing.Size(64, 16);
			this.checkBox6.TabIndex = 5;
			this.checkBox6.Text = "content";
			// 
			// checkBox7
			// 
			this.checkBox7.Location = new System.Drawing.Point(72, 32);
			this.checkBox7.Name = "checkBox7";
			this.checkBox7.Size = new System.Drawing.Size(64, 16);
			this.checkBox7.TabIndex = 4;
			this.checkBox7.Text = "west";
			// 
			// checkBox8
			// 
			this.checkBox8.Location = new System.Drawing.Point(8, 32);
			this.checkBox8.Name = "checkBox8";
			this.checkBox8.Size = new System.Drawing.Size(64, 16);
			this.checkBox8.TabIndex = 6;
			this.checkBox8.Text = "north";
			// 
			// groupBox4
			// 
			this.level1Group.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.checkBox13,
																					this.checkBox14,
																					this.checkBox15,
																					this.checkBox16});
			this.level1Group.Location = new System.Drawing.Point(level2Group.Left,level2Group.Bottom);
			this.level1Group.Name = "level1Group";
			this.level1Group.Size = new System.Drawing.Size(144, 56);
			this.level1Group.TabIndex = 9;
			this.level1Group.TabStop = false;
			this.level1Group.Text = "Level 1";
			// 
			// checkBox13
			// 
			this.checkBox13.Checked = true;
			this.checkBox13.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox13.Location = new System.Drawing.Point(8, 16);
			this.checkBox13.Name = "checkBox13";
			this.checkBox13.Size = new System.Drawing.Size(64, 16);
			this.checkBox13.TabIndex = 3;
			this.checkBox13.Text = "ground";
			// 
			// checkBox14
			// 
			this.checkBox14.Checked = true;
			this.checkBox14.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox14.Location = new System.Drawing.Point(72, 16);
			this.checkBox14.Name = "checkBox14";
			this.checkBox14.Size = new System.Drawing.Size(64, 16);
			this.checkBox14.TabIndex = 5;
			this.checkBox14.Text = "content";
			// 
			// checkBox15
			// 
			this.checkBox15.Checked = true;
			this.checkBox15.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox15.Location = new System.Drawing.Point(72, 32);
			this.checkBox15.Name = "checkBox15";
			this.checkBox15.Size = new System.Drawing.Size(64, 16);
			this.checkBox15.TabIndex = 4;
			this.checkBox15.Text = "west";
			// 
			// checkBox16
			// 
			this.checkBox16.Checked = true;
			this.checkBox16.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox16.Location = new System.Drawing.Point(8, 32);
			this.checkBox16.Name = "checkBox16";
			this.checkBox16.Size = new System.Drawing.Size(64, 16);
			this.checkBox16.TabIndex = 6;
			this.checkBox16.Text = "north";
			// 
			// CopyForm
			// 
			this.ClientSize = new System.Drawing.Size(224, 229);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.level1Group,
																		  this.level2Group,
																		  this.level4Group,
																		  this.btnPaste,
																		  this.btnCopy,
																		  this.level3Group});
			this.Name = "CopyForm";
			this.Text = "CopyForm";
			this.level4Group.ResumeLayout(false);
			this.level1Group.ResumeLayout(false);
			this.level3Group.ResumeLayout(false);
			this.level2Group.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnCopy_Click(object sender, System.EventArgs e)
		{
			MapViewPanel.Instance.View.Copy();
		}

		private void btnPaste_Click(object sender, System.EventArgs e)
		{
			MapViewPanel.Instance.View.Paste();
		}
	}
}
*/