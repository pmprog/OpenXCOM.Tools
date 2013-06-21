using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace PckView
{
	public delegate void TryDecodeEventHandler(object sender, TryDecodeEventArgs e);

	public partial class OpenCustomForm : System.Windows.Forms.Form
	{
		private XCom.SharedSpace space;
		private string file, directory;

		public event TryDecodeEventHandler TryClick;

		public OpenCustomForm(string directory,string file)
		{
			this.directory=directory;
			this.file=file;
			InitializeComponent();

			//Console.WriteLine("File: "+file);

			DSShared.Windows.RegistryInfo ri = new DSShared.Windows.RegistryInfo(this);
			ri.AddProperty("WidVal");
			ri.AddProperty("HeiVal");

			space = XCom.SharedSpace.Instance;

			foreach (XCom.Interfaces.IXCImageFile xcf in space.GetImageModList())
				if (xcf.FileOptions[XCom.Interfaces.IXCImageFile.Filter.Custom])
					cbTypes.Items.Add(new BmpForm.cbItem(xcf, xcf.ExplorerDescription));

			if (cbTypes.Items.Count > 0)
				cbTypes.SelectedIndex = 0;
		}

		public string ErrorString
		{
			get{return txtErr.Text;}
			set{txtErr.Text=value;}
		}

		public int WidVal
		{
			get{return scrollWid.Value;}
			set{scrollWid.Value=value;wid_Scroll(null,null);}
		}

		public int HeiVal
		{
			get{return scrollHei.Value;}
			set{scrollHei.Value=value;hei_Scroll(null,null);}
		}

		private void wid_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
		{
			txtWid.Text = scrollWid.Value.ToString();
		}

		private void hei_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
		{
			txtHei.Text = scrollHei.Value.ToString();
		}

		private void btnTry_Click(object sender, System.EventArgs e)
		{
			if(TryClick!=null)
			{
				try
				{
					TryClick(this,new TryDecodeEventArgs(scrollWid.Value,scrollHei.Value,directory,file,((BmpForm.cbItem)cbTypes.SelectedItem).itm));
					txtErr.Text="";
					Height=184;
				}
				catch (Exception ex)
				{
					txtErr.Text = ex.Message + "\n" + ex.StackTrace;
					if (Height <= 184)
						Height = 184 + 200;
				}
			}
		}

		private void btnProfile_Click(object sender, System.EventArgs e)
		{
			SaveProfileForm spf = new SaveProfileForm();
			spf.ImgHei=scrollHei.Value;
			spf.ImgWid=scrollWid.Value;
			spf.ImgType=((BmpForm.cbItem)cbTypes.SelectedItem).itm;
			spf.FileString = file;

			if (spf.ShowDialog(this) == DialogResult.OK)
				Close();
		}

		private void txtWid_TextChanged(object sender, EventArgs e)
		{
			int val = scrollWid.Value;
			int.TryParse(txtWid.Text, out val);
			if(val >= scrollWid.Minimum && val <= scrollWid.Maximum)
				scrollWid.Value = val;
		}

		private void txtHei_TextChanged(object sender, EventArgs e)
		{
			int val = scrollHei.Value;
			int.TryParse(txtHei.Text, out val);
			if (val >= scrollHei.Minimum && val <= scrollHei.Maximum)
				scrollHei.Value = val;
		}
	}

	public class TryDecodeEventArgs:EventArgs
	{
		private int width,height;
		private string directory,file;
		private XCom.Interfaces.IXCImageFile itm;

		public TryDecodeEventArgs(int width, int height, string directory, string file, XCom.Interfaces.IXCImageFile itm)
		{
			this.itm=itm;
			this.file=file;
			this.width=width;
			this.height=height;
			this.directory=directory;
		}

		public XCom.Interfaces.IXCImageFile XCFile
		{
			get{return itm;}			
		}

		public string File
		{
			get{return file;}
		}

		public string Directory
		{
			get{return directory;}
		}

		public int TryWidth
		{
			get{return width;}
		}

		public int TryHeight
		{		
			get{return height;}
		}
	}
}
