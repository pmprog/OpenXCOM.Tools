using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using XCom;

namespace MapView
{
	/// <summary>
	/// Summary description for HelpScreen.
	/// </summary>
	public class HelpScreen : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl tabMain;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label l1;
		private System.Windows.Forms.Label l2;
		private System.Windows.Forms.Label l3;
		private System.Windows.Forms.Label l6;
		private System.Windows.Forms.Label l5;
		private System.Windows.Forms.Label l4;
		private System.Windows.Forms.Label l9;
		private System.Windows.Forms.Label l8;
		private System.Windows.Forms.Label l7;
		private System.Windows.Forms.Label l12;
		private System.Windows.Forms.Label l11;
		private System.Windows.Forms.Label l10;
		private System.Windows.Forms.Label l14;
		private System.Windows.Forms.Label l13;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public HelpScreen()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			l1.ForeColor=TilePanel.tileTypes[(int)SpecialType.Tile];
			l2.ForeColor=TilePanel.tileTypes[(int)SpecialType.StartPoint];
			l3.ForeColor=TilePanel.tileTypes[(int)SpecialType.IonBeamAccel];
			l4.ForeColor=TilePanel.tileTypes[(int)SpecialType.DestroyObjective];
			l5.ForeColor=TilePanel.tileTypes[(int)SpecialType.MagneticNav];
			l6.ForeColor=TilePanel.tileTypes[(int)SpecialType.AlienCryo];
			l7.ForeColor=TilePanel.tileTypes[(int)SpecialType.AlienClon];
			l8.ForeColor=TilePanel.tileTypes[(int)SpecialType.AlienLearn];
			l9.ForeColor=TilePanel.tileTypes[(int)SpecialType.AlienImplant];
			l10.ForeColor=TilePanel.tileTypes[(int)SpecialType.AlienPlastics];
			l11.ForeColor=TilePanel.tileTypes[(int)SpecialType.ExamRoom];
			l12.ForeColor=TilePanel.tileTypes[(int)SpecialType.DeadTile];
			l13.ForeColor=TilePanel.tileTypes[(int)SpecialType.EndPoint];
			l14.ForeColor=TilePanel.tileTypes[(int)SpecialType.MustDestroy];

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			this.tabMain = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.label17 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.label22 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.label20 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.label19 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.l1 = new System.Windows.Forms.Label();
			this.l2 = new System.Windows.Forms.Label();
			this.l3 = new System.Windows.Forms.Label();
			this.l6 = new System.Windows.Forms.Label();
			this.l5 = new System.Windows.Forms.Label();
			this.l4 = new System.Windows.Forms.Label();
			this.l9 = new System.Windows.Forms.Label();
			this.l8 = new System.Windows.Forms.Label();
			this.l7 = new System.Windows.Forms.Label();
			this.l12 = new System.Windows.Forms.Label();
			this.l11 = new System.Windows.Forms.Label();
			this.l10 = new System.Windows.Forms.Label();
			this.l14 = new System.Windows.Forms.Label();
			this.l13 = new System.Windows.Forms.Label();
			this.tabMain.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabMain
			// 
			this.tabMain.Controls.AddRange(new System.Windows.Forms.Control[] {
																				  this.tabPage1,
																				  this.tabPage2,
																				  this.tabPage3,
																				  this.tabPage4});
			this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabMain.Name = "tabMain";
			this.tabMain.SelectedIndex = 0;
			this.tabMain.Size = new System.Drawing.Size(456, 273);
			this.tabMain.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.label17,
																				   this.label12,
																				   this.label2,
																				   this.label1});
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(448, 247);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Main Window";
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(0, 72);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(448, 16);
			this.label17.TabIndex = 3;
			this.label17.Text = "You MUST save before selecting another map or your changes will be lost";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(0, 48);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(448, 16);
			this.label12.TabIndex = 2;
			this.label12.Text = "Your window locations will be saved on program exit";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(0, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(448, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "Turning the animation off makes it harder to see which tile you are going to clic" +
				"k on";
			// 
			// label1
			// 
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(448, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Click anywhere to set the tile to edit";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.label22,
																				   this.label11,
																				   this.label10,
																				   this.label9,
																				   this.label8,
																				   this.label7,
																				   this.label6,
																				   this.label5,
																				   this.label4,
																				   this.label3});
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(448, 247);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Top View";
			// 
			// label22
			// 
			this.label22.Location = new System.Drawing.Point(0, 120);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(448, 32);
			this.label22.TabIndex = 9;
			this.label22.Text = "Right clicking on the grid will set the selected tile in tileView in the selected" +
				" portion on the bottom";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(0, 96);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(448, 16);
			this.label11.TabIndex = 8;
			this.label11.Text = "Setting the size will make things larger/smaller";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(0, 208);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(64, 16);
			this.label10.TabIndex = 7;
			this.label10.Text = "Color key:";
			// 
			// label9
			// 
			this.label9.ForeColor = System.Drawing.Color.Red;
			this.label9.Location = new System.Drawing.Point(168, 208);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(72, 16);
			this.label9.TabIndex = 6;
			this.label9.Text = "North/West";
			// 
			// label8
			// 
			this.label8.ForeColor = System.Drawing.Color.Green;
			this.label8.Location = new System.Drawing.Point(112, 208);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(56, 16);
			this.label8.TabIndex = 5;
			this.label8.Text = "Content";
			// 
			// label7
			// 
			this.label7.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(128)), ((System.Byte)(0)));
			this.label7.Location = new System.Drawing.Point(64, 208);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(56, 16);
			this.label7.TabIndex = 4;
			this.label7.Text = "Ground";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(0, 72);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(448, 16);
			this.label6.TabIndex = 3;
			this.label6.Text = "Double right click to clear the clicked on tile";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(0, 48);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(448, 16);
			this.label5.TabIndex = 2;
			this.label5.Text = "Right click to set the currently selected tile to the one selected in Tile View";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(0, 24);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(448, 16);
			this.label4.TabIndex = 1;
			this.label4.Text = "Double left click to set the currently selected tile in Tile View";
			// 
			// label3
			// 
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(376, 16);
			this.label3.TabIndex = 0;
			this.label3.Text = "Click anywhere to set the tile to edit";
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.label20,
																				   this.label18,
																				   this.label13,
																				   this.label14,
																				   this.label15,
																				   this.label16,
																				   this.label21});
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(448, 247);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Rmp View";
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(0, 88);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(448, 16);
			this.label20.TabIndex = 19;
			this.label20.Text = "Right click on the grid to place a new node";
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(0, 64);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(448, 16);
			this.label18.TabIndex = 18;
			this.label18.Text = "When editing the distance text box, you must hit enter to save your change";
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(0, 40);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(376, 16);
			this.label13.TabIndex = 17;
			this.label13.Text = "Clicking a green square will select a node to edit";
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(0, 219);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(64, 16);
			this.label14.TabIndex = 16;
			this.label14.Text = "Color key:";
			// 
			// label15
			// 
			this.label15.ForeColor = System.Drawing.Color.Black;
			this.label15.Location = new System.Drawing.Point(120, 219);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(72, 16);
			this.label15.TabIndex = 15;
			this.label15.Text = "North/West";
			// 
			// label16
			// 
			this.label16.ForeColor = System.Drawing.Color.Gray;
			this.label16.Location = new System.Drawing.Point(64, 219);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(56, 16);
			this.label16.TabIndex = 14;
			this.label16.Text = "Content";
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(0, 11);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(376, 16);
			this.label21.TabIndex = 9;
			this.label21.Text = "Click anywhere to set the tile to edit";
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.groupBox1,
																				   this.label19});
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size(448, 247);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Tile View";
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(0, 8);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(376, 16);
			this.label19.TabIndex = 10;
			this.label19.Text = "Left click to set the tile to place";
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.SystemColors.ControlDark;
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.l14,
																					this.l13,
																					this.l12,
																					this.l11,
																					this.l10,
																					this.l9,
																					this.l8,
																					this.l7,
																					this.l6,
																					this.l5,
																					this.l4,
																					this.l3,
																					this.l2,
																					this.l1});
			this.groupBox1.Location = new System.Drawing.Point(8, 56);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(320, 136);
			this.groupBox1.TabIndex = 11;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Color Key";
			// 
			// l1
			// 
			this.l1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.l1.Location = new System.Drawing.Point(8, 16);
			this.l1.Name = "l1";
			this.l1.Size = new System.Drawing.Size(84, 16);
			this.l1.TabIndex = 12;
			this.l1.Text = "Tile";
			// 
			// l2
			// 
			this.l2.Location = new System.Drawing.Point(104, 16);
			this.l2.Name = "l2";
			this.l2.Size = new System.Drawing.Size(84, 16);
			this.l2.TabIndex = 13;
			this.l2.Text = "Xcom unit start";
			// 
			// l3
			// 
			this.l3.Location = new System.Drawing.Point(200, 16);
			this.l3.Name = "l3";
			this.l3.Size = new System.Drawing.Size(112, 16);
			this.l3.TabIndex = 14;
			this.l3.Text = "Ion beam accelerator";
			// 
			// l6
			// 
			this.l6.Location = new System.Drawing.Point(200, 40);
			this.l6.Name = "l6";
			this.l6.Size = new System.Drawing.Size(84, 16);
			this.l6.TabIndex = 17;
			this.l6.Text = "Cryogenics";
			// 
			// l5
			// 
			this.l5.Location = new System.Drawing.Point(104, 40);
			this.l5.Name = "l5";
			this.l5.Size = new System.Drawing.Size(84, 16);
			this.l5.TabIndex = 16;
			this.l5.Text = "Navigation";
			// 
			// l4
			// 
			this.l4.Location = new System.Drawing.Point(8, 40);
			this.l4.Name = "l4";
			this.l4.Size = new System.Drawing.Size(96, 16);
			this.l4.TabIndex = 15;
			this.l4.Text = "Destroy Objective";
			// 
			// l9
			// 
			this.l9.Location = new System.Drawing.Point(200, 64);
			this.l9.Name = "l9";
			this.l9.Size = new System.Drawing.Size(84, 16);
			this.l9.TabIndex = 20;
			this.l9.Text = "Implanter";
			// 
			// l8
			// 
			this.l8.Location = new System.Drawing.Point(104, 64);
			this.l8.Name = "l8";
			this.l8.Size = new System.Drawing.Size(84, 16);
			this.l8.TabIndex = 19;
			this.l8.Text = "Learning Array";
			// 
			// l7
			// 
			this.l7.Location = new System.Drawing.Point(8, 64);
			this.l7.Name = "l7";
			this.l7.Size = new System.Drawing.Size(84, 16);
			this.l7.TabIndex = 18;
			this.l7.Text = "Cloning";
			// 
			// l12
			// 
			this.l12.Location = new System.Drawing.Point(200, 88);
			this.l12.Name = "l12";
			this.l12.Size = new System.Drawing.Size(84, 16);
			this.l12.TabIndex = 23;
			this.l12.Text = "Dead tile";
			// 
			// l11
			// 
			this.l11.Location = new System.Drawing.Point(104, 88);
			this.l11.Name = "l11";
			this.l11.Size = new System.Drawing.Size(84, 16);
			this.l11.TabIndex = 22;
			this.l11.Text = "Exam room";
			// 
			// l10
			// 
			this.l10.Location = new System.Drawing.Point(8, 88);
			this.l10.Name = "l10";
			this.l10.Size = new System.Drawing.Size(84, 16);
			this.l10.TabIndex = 21;
			this.l10.Text = "Plastics";
			// 
			// l14
			// 
			this.l14.Location = new System.Drawing.Point(104, 112);
			this.l14.Name = "l14";
			this.l14.Size = new System.Drawing.Size(84, 16);
			this.l14.TabIndex = 25;
			this.l14.Text = "Must Destroy";
			// 
			// l13
			// 
			this.l13.Location = new System.Drawing.Point(8, 112);
			this.l13.Name = "l13";
			this.l13.Size = new System.Drawing.Size(84, 16);
			this.l13.TabIndex = 24;
			this.l13.Text = "End Point";
			// 
			// HelpScreen
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(456, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.tabMain});
			this.Name = "HelpScreen";
			this.Text = "HelpScreen";
			this.tabMain.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.tabPage4.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
