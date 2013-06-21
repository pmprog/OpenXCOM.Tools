using System;
using XCom.Interfaces;
using System.Windows.Forms;

namespace XCom.GameFiles.Images.xcFiles
{
	public class xcPck:IXCImageFile
	{
		private const string TAB_EXT = ".tab";
		private Panel savePanel;
		private RadioButton radio2, radio4;

		public xcPck():base(32,40)
		{
			ext=".pck";
			author="Ben Ratzlaff";
			desc="Standard pck file codec";

			expDesc = "Pck File";
		}

		protected override XCImageCollection LoadFileOverride(string directory,string file,int imgWid,int imgHei,Palette pal)
		{
			System.IO.Stream tabStream=null;

			string tabBase = file.Substring(0,file.LastIndexOf("."));

			try
			{
			    if(System.IO.File.Exists(directory+"\\"+tabBase+TAB_EXT))
			        tabStream = System.IO.File.OpenRead(directory+"\\"+tabBase+TAB_EXT);

			    return new PckFile(System.IO.File.OpenRead(directory+"\\"+file),
			        tabStream,
			        2,
			        pal,
			        imgHei,
			        imgWid);
			}
			catch(Exception)
			{
				if(System.IO.File.Exists(directory+"\\"+tabBase+TAB_EXT))
					tabStream = System.IO.File.OpenRead(directory+"\\"+tabBase+TAB_EXT);

				return new PckFile(System.IO.File.OpenRead(directory+"\\"+file),
					tabStream,
					4,
					pal,
					imgHei,
					imgWid);
			}
		}

		private System.Windows.Forms.Panel SavingOptions
		{
			get
			{
				if(savePanel==null)
				{
					savePanel = new Panel();

					GroupBox gb = new GroupBox();
					gb.Text="Bpp Options";

					Panel top = new Panel();
					top.Dock=DockStyle.Top;
					top.Height=50;

					radio2=new RadioButton();
					radio2.Text="2";
					radio2.Dock=DockStyle.Left;
					radio2.Height=50;
					radio2.Width=40;
					radio2.TextAlign=System.Drawing.ContentAlignment.TopLeft;
					radio2.CheckAlign=System.Drawing.ContentAlignment.TopLeft;
					radio2.Checked=true;
					radio2.CheckedChanged+=new EventHandler(checkChange);

					Label l = new Label();
					l.Text = "UFO/TFTD Terrain\nUFO Units";
					l.Dock=DockStyle.Fill;

					top.Controls.AddRange(new Control[]{l,radio2});

					Panel mid = new Panel();
					mid.Dock=DockStyle.Fill;

					radio4=new RadioButton();
					radio4.Text="4";
					radio4.Dock=DockStyle.Left;
					radio4.Width=radio2.Width;
					radio4.TextAlign=System.Drawing.ContentAlignment.TopLeft;
					radio4.CheckAlign=System.Drawing.ContentAlignment.TopLeft;
					radio4.CheckedChanged+=new EventHandler(checkChange);

					l = new Label();
					l.Text="TFTD Units";
					l.Dock=DockStyle.Fill;

					mid.Controls.AddRange(new Control[]{l,radio4});

					gb.Controls.AddRange(new Control[]{mid,top});
					gb.Dock=DockStyle.Left;

					Panel left = new Panel();
					left.Dock=DockStyle.Top;
					left.Height=100;

					left.Controls.Add(gb);

					l = new Label();
					l.Text="If you are unsure about the correct bpp option, open up the original .pck file and see what the number is in the lower right of the main screen";
					l.Dock=DockStyle.Fill;

					savePanel.Controls.AddRange(new Control[]{l,left});					
				}

				return savePanel;
			}
			set
			{
				savePanel=value;
			}
		}

		private void checkChange(object sender, EventArgs e)
		{
			if(sender == radio2 && radio2.Checked)
				radio4.Checked=false;
			else if(sender==radio4 && radio4.Checked)
				radio2.Checked=false;
		}

		//Method to save a collection in its original format
		public override void SaveCollection(string directory, string file,XCImageCollection images)
		{
			DSShared.Windows.InputBox ib = new DSShared.Windows.InputBox("Enter Pck Options", SavingOptions);

			if (ib.ShowDialog() == DialogResult.OK)
			{
				int bpp = 4;
				if (radio2.Checked)
					bpp = 2;

				PckFile.Save(directory, file, images, bpp);
			}
		}
	}

	public class xcPckTab : xcPck
	{
		public xcPckTab()
		{
			ext=".tab";
			author="Ben Ratzlaff";
			desc="Opens tab files as pck";

			expDesc = "Tab File";

			fileOptions.Init(false, false, false, false);
		}

		protected override XCImageCollection LoadFileOverride(string directory, string file, int imgWid, int imgHei, Palette pal)
		{
			string fileBase = file.Substring(0, file.IndexOf("."));

			return base.LoadFileOverride(directory, fileBase+".pck", imgWid, imgHei, pal);
		}
	}
}
