using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using XCom;
using XCom.Interfaces;
using XCom.Interfaces.Base;

namespace MapView
{
	public class MapInfoForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblDimensions;
		private System.Windows.Forms.Label lblPckFiles;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label lblPckImages;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label lblMcd;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label lblFilled;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ProgressBar pBar;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.GroupBox groupAnalyze;
		private System.Windows.Forms.GroupBox groupInfo;

		private IMap_Base map;
		private int slotsUsed=0;

		public MapInfoForm()
		{
			InitializeComponent();
		}

		public IMap_Base Map
		{
			set
			{
				map=value;
				startAnalyzing();
			}
		}

		private void startAnalyzing()
		{
			groupAnalyze.Visible=true;
			Hashtable imgHash=new Hashtable();
			Hashtable mcdHash=new Hashtable();
			groupInfo.Text="Map: "+map.Name;
			lblDimensions.Text = map.MapSize.Rows + "," + map.MapSize.Cols + "," + map.MapSize.Height;

			lblPckFiles.Text="";
			bool one=true;
			int totalImages=0;
			int totalMcd=0;
			if(map is XCMapFile)
				foreach(string s in ((XCMapFile)map).Dependencies)
				{
					if(one)
						one=false;
					else
						lblPckFiles.Text+=",";

					totalImages+=GameInfo.ImageInfo[s].GetPckFile().Count;
					totalMcd+=GameInfo.ImageInfo[s].GetMcdFile().Length;
					lblPckFiles.Text+=s;
				}

			pBar.Maximum = map.MapSize.Rows * map.MapSize.Cols * map.MapSize.Height;
			pBar.Value=0;

			for (int h = 0; h < map.MapSize.Height; h++)
				for (int r = 0; r < map.MapSize.Rows; r++)
					for (int c = 0; c < map.MapSize.Cols; c++)
					{
                        XCMapTile tile = (XCMapTile)map[r, c, h];
                        if (!tile.Blank)
						{
                            if (tile.Ground != null)
							{
                                count(imgHash, mcdHash, tile.Ground);
                                if (tile.Ground is XCTile)
                                    count(imgHash, mcdHash, ((XCTile)tile.Ground).Dead);
								slotsUsed++;
							}
                            if (tile.West != null)
							{
                                count(imgHash, mcdHash, tile.West);
                                if (tile.West is XCTile)
                                    count(imgHash, mcdHash, ((XCTile)tile.West).Dead);
								slotsUsed++;
							}
                            if (tile.North != null)
							{
                                count(imgHash, mcdHash, tile.North);
                                if (tile.North is XCTile)
                                    count(imgHash, mcdHash, ((XCTile)tile.North).Dead);
								slotsUsed++;
							}
                            if (tile.Content != null)
							{
                                count(imgHash, mcdHash, tile.Content);
                                if (tile.Content is XCTile)
                                    count(imgHash, mcdHash, ((XCTile)tile.Content).Dead);
								slotsUsed++;
							}

							pBar.Value=(r+1)*(c+1)*(h+1);
							pBar.Refresh();
						}
					}
							
			lblMcd.Text=mcdHash.Keys.Count+"/"+totalMcd+"   "+Math.Round(100*((mcdHash.Keys.Count*1.0)/(totalMcd)),2)+"%";
			lblPckImages.Text=imgHash.Keys.Count+"/"+totalImages+"   "+Math.Round(100*((imgHash.Keys.Count*1.0)/(totalImages)),2)+"%";
			lblFilled.Text=slotsUsed+"/"+(pBar.Maximum*4)+"   "+Math.Round(100*((slotsUsed*1.0)/(pBar.Maximum*4)),2)+"%";

			groupAnalyze.Visible=false;
		}

		private void count(Hashtable img, Hashtable mcd, ITile tile)
		{
			if(tile!=null)
			{
				foreach(PckImage pi in tile.Images)
					img[pi.StaticID]=true;
				mcd[tile.Info.ID]=true;
			}
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
			this.label1 = new System.Windows.Forms.Label();
			this.lblDimensions = new System.Windows.Forms.Label();
			this.lblPckFiles = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.lblPckImages = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.lblMcd = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.lblFilled = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.pBar = new System.Windows.Forms.ProgressBar();
			this.groupAnalyze = new System.Windows.Forms.GroupBox();
			this.groupInfo = new System.Windows.Forms.GroupBox();
			this.groupAnalyze.SuspendLayout();
			this.groupInfo.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Dimensions (r,c,h): ";
			// 
			// lblDimensions
			// 
			this.lblDimensions.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.lblDimensions.Location = new System.Drawing.Point(112, 16);
			this.lblDimensions.Name = "lblDimensions";
			this.lblDimensions.Size = new System.Drawing.Size(288, 16);
			this.lblDimensions.TabIndex = 1;
			// 
			// lblPckFiles
			// 
			this.lblPckFiles.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.lblPckFiles.Location = new System.Drawing.Point(112, 32);
			this.lblPckFiles.Name = "lblPckFiles";
			this.lblPckFiles.Size = new System.Drawing.Size(288, 16);
			this.lblPckFiles.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 32);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(104, 16);
			this.label3.TabIndex = 2;
			this.label3.Text = "Pck files used:";
			// 
			// lblPckImages
			// 
			this.lblPckImages.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.lblPckImages.Location = new System.Drawing.Point(112, 48);
			this.lblPckImages.Name = "lblPckImages";
			this.lblPckImages.Size = new System.Drawing.Size(288, 16);
			this.lblPckImages.TabIndex = 5;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 48);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(104, 16);
			this.label4.TabIndex = 4;
			this.label4.Text = "Pck images used:";
			// 
			// lblMcd
			// 
			this.lblMcd.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.lblMcd.Location = new System.Drawing.Point(112, 64);
			this.lblMcd.Name = "lblMcd";
			this.lblMcd.Size = new System.Drawing.Size(288, 16);
			this.lblMcd.TabIndex = 7;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 64);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(104, 16);
			this.label6.TabIndex = 6;
			this.label6.Text = "Mcd entries used:";
			// 
			// lblFilled
			// 
			this.lblFilled.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.lblFilled.Location = new System.Drawing.Point(112, 80);
			this.lblFilled.Name = "lblFilled";
			this.lblFilled.Size = new System.Drawing.Size(288, 16);
			this.lblFilled.TabIndex = 9;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(8, 80);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(104, 16);
			this.label8.TabIndex = 8;
			this.label8.Text = "% of map filled";
			// 
			// pBar
			// 
			this.pBar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pBar.Location = new System.Drawing.Point(3, 16);
			this.pBar.Name = "pBar";
			this.pBar.Size = new System.Drawing.Size(402, 21);
			this.pBar.TabIndex = 1;
			// 
			// groupAnalyze
			// 
			this.groupAnalyze.Controls.AddRange(new System.Windows.Forms.Control[] {
																					   this.pBar});
			this.groupAnalyze.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.groupAnalyze.Location = new System.Drawing.Point(0, 104);
			this.groupAnalyze.Name = "groupAnalyze";
			this.groupAnalyze.Size = new System.Drawing.Size(408, 40);
			this.groupAnalyze.TabIndex = 11;
			this.groupAnalyze.TabStop = false;
			this.groupAnalyze.Text = "Analyzing";
			// 
			// groupInfo
			// 
			this.groupInfo.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.lblPckImages,
																					this.label3,
																					this.lblPckFiles,
																					this.label1,
																					this.lblMcd,
																					this.label4,
																					this.label6,
																					this.lblFilled,
																					this.label8,
																					this.lblDimensions});
			this.groupInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupInfo.Name = "groupInfo";
			this.groupInfo.Size = new System.Drawing.Size(408, 104);
			this.groupInfo.TabIndex = 12;
			this.groupInfo.TabStop = false;
			this.groupInfo.Text = "Map: ";
			// 
			// MapInfoForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(408, 144);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.groupInfo,
																		  this.groupAnalyze});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "MapInfoForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Map Info";
			this.groupAnalyze.ResumeLayout(false);
			this.groupInfo.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
