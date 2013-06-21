using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DSShared.Windows;

namespace PckView
{
	public partial class BmpForm : System.Windows.Forms.Form
	{
		private Bitmap bmp = null;
		private Pen drawPen;
		//private XCom.SharedSpace space;

		public class cbItem
		{
			public XCom.Interfaces.IXCImageFile itm;
			public string text;

			public cbItem(XCom.Interfaces.IXCImageFile itm, string text)
			{
				this.itm = itm;
				this.text = text;
			}

			public override string ToString()
			{
				return text;
			}
		}

		public BmpForm()
		{
			InitializeComponent();

			drawPen = new Pen(Brushes.Red, 1);

			RegistryInfo ri = new RegistryInfo(this);
			ri.AddProperty("LineColor");

			DialogResult = DialogResult.Cancel;

			foreach (XCom.Interfaces.IXCImageFile xcf in XCom.SharedSpace.Instance.GetImageModList())
				if (xcf.FileOptions[XCom.Interfaces.IXCImageFile.Filter.Bmp])
					cbTypes.Items.Add(new cbItem(xcf, xcf.ExplorerDescription));

			if (cbTypes.Items.Count > 0)
				cbTypes.SelectedIndex = 0;
		}

		public string LineColor
		{
			get { return drawPen.Color.R + "|" + drawPen.Color.G + "|" + drawPen.Color.B; }
			set
			{
				string[] cols = value.Split('|');
				drawPen.Color = Color.FromArgb(int.Parse(cols[0]), int.Parse(cols[1]), int.Parse(cols[2]));
			}
		}

		public System.Drawing.Size SelectedSize
		{
			get
			{
				return new System.Drawing.Size(scrollWidth.Value, scrollHeight.Value);
			}
		}

		public int SelectedSpace
		{
			get { return scrollSpace.Value; }
		}

		public Bitmap Bitmap
		{
			set
			{
				bmp = value;
				guess();
				Refresh();
			}
		}

		private void guess()
		{
			if (bmp != null && cbTypes.Items.Count > 0)
			{
				cbItem cbSel = null;
				foreach (cbItem cb in cbTypes.Items)
				{
					XCom.Interfaces.IXCImageFile xcf = cb.itm;

					if ((bmp.Width + 1) % (xcf.ImageSize.Width + 1) == 0 && (bmp.Height + 1) % (xcf.ImageSize.Height + 1) == 0)
					{
						cbSel = cb;
						break;
					}
				}

				if (cbSel != null)
					cbTypes.SelectedItem = cbSel;
			}
		}

		private void scrollWidth_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
		{
			txtWidth.Text = scrollWidth.Value.ToString();
			drawPanel.Refresh();
		}

		private void scrollHeight_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
		{
			txtHeight.Text = scrollHeight.Value.ToString();
			drawPanel.Refresh();
		}

		private void scrollSpace_Scroll(object sender, ScrollEventArgs e)
		{
			txtSpace.Text = scrollSpace.Value.ToString();
			drawPanel.Refresh();
		}

		private void drawPanel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.DrawImage(bmp, 0, 0);
			for (int i = scrollWidth.Value; i < drawPanel.Width; i += scrollWidth.Value + scrollSpace.Value)
			{
				e.Graphics.DrawLine(drawPen, i, 0, i, drawPanel.Height);
				e.Graphics.DrawLine(drawPen, i + scrollSpace.Value - 1, 0, i + scrollSpace.Value - 1, drawPanel.Height);
			}

			for (int i = scrollHeight.Value; i < drawPanel.Height; i += scrollHeight.Value + scrollSpace.Value)
			{
				e.Graphics.DrawLine(drawPen, 0, i, drawPanel.Width, i);
				e.Graphics.DrawLine(drawPen, 0, i + scrollSpace.Value - 1, drawPanel.Width, i + scrollSpace.Value - 1);
			}
		}

		private void miClose_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void miLineColor_Click(object sender, System.EventArgs e)
		{
			colors.Color = drawPen.Color;
			if (colors.ShowDialog() == DialogResult.OK)
			{
				drawPen.Color = colors.Color;
				Refresh();
			}
		}

		private void cbTypes_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			scrollHeight.Value = ((cbItem)cbTypes.SelectedItem).itm.ImageSize.Height;
			scrollWidth.Value = ((cbItem)cbTypes.SelectedItem).itm.ImageSize.Width;
			scrollSpace.Value = ((cbItem)cbTypes.SelectedItem).itm.FileOptions.Space;
			scrollWidth_Scroll(null, null);
			scrollHeight_Scroll(null, null);
			scrollSpace_Scroll(null, null);
		}

		private void txtWidth_TextChanged(object sender, System.EventArgs e)
		{
			try
			{
				scrollWidth.Value = int.Parse(txtWidth.Text);
				scrollWidth_Scroll(null, null);
			}
			catch { }
		}

		private void txtHeight_TextChanged(object sender, System.EventArgs e)
		{
			try
			{
				scrollHeight.Value = int.Parse(txtHeight.Text);
				scrollHeight_Scroll(null, null);
			}
			catch { }
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void txtSpace_TextChanged(object sender, EventArgs e)
		{
			try
			{
				scrollSpace.Value = int.Parse(txtSpace.Text);
				scrollSpace_Scroll(null, null);
			}
			catch { }
		}
	}
}
