/*using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using XCom;

namespace PckView
{
	public partial class SaveForm : System.Windows.Forms.Form
	{
		private XCImageCollection collection;
		private XCom.SharedSpace shared;
		private DSShared.Windows.RegistryInfo ri;

		public SaveForm(XCImageCollection collection,XCom.SharedSpace sharedSpace)
		{
			InitializeComponent();
			this.collection=collection;
			this.shared=sharedSpace;

			ri =new DSShared.Windows.RegistryInfo(this);
			/*
			Hashtable multiTable = (Hashtable)sharedSpace[XCom.SharedSpace.Keys.Multi];
			ArrayList singleList = (ArrayList)sharedSpace[XCom.SharedSpace.Keys.Single];

			TabPage outerTab=tabSingle;
			TabPage innerTab=null;
			foreach(string key in multiTable.Keys)
			{
				XCom.Interfaces.IXCFile xcf = (XCom.Interfaces.IXCFile)multiTable[key];
				if(!xcf.FileOptions.SaveDialog)
					continue;

				TabPage tc = createTab(xcf,tabsMulti);
				tc.Text=xcf.FileExtension;

				if(xcf==collection.IXCFile)
				{
					outerTab = tabMulti;
					innerTab = tc;
				}
			}

			foreach(XCom.Interfaces.IXCFile xcf in singleList)
			{
				if(!xcf.FileOptions.SaveDialog)
					continue;

				TabPage tc = createTab(xcf,tabsSingle);

				if(xcf==collection.IXCFile)
				{
					outerTab = tabSingle;
					innerTab = tc;
				}
			}

			saveTabs.SelectedTab=outerTab;
			if(outerTab==tabMulti)
				tabsMulti.SelectedTab=innerTab;
			else
				tabsSingle.SelectedTab=innerTab;
			*/
/*
			tabOld.Visible=false;
		}

		private TabPage createTab(XCom.Interfaces.IXCImageFile xcf, TabControl outer)
		{
			TabPage tc = new TabPage();
			tc.Text = xcf.SingleFileName;
			Panel p = xcf.SavingOptions;
			if(p==null)
				p = new DefaultOptionsPanel();

			p.Dock=DockStyle.Fill;
			tc.Controls.Add(p);

			tc.Tag=xcf;
			outer.TabPages.Add(tc);
			return tc;
		}

		private void btnPckSelect_Click(object sender, System.EventArgs e)
		{
			if(savePck.ShowDialog()==DialogResult.OK)
				txtPckPath.Text = savePck.FileName;
		}

		private void btnPckSave_Click(object sender, System.EventArgs e)
		{
//			int bpp=2;
//			if(radio4bpp.Checked)
//				bpp=4;
//			collection.Save(txtPckPath.Text,bpp);	
//			this.Close();
		}

		private void btnBmpSelect_Click(object sender, System.EventArgs e)
		{
			if(saveBmp.ShowDialog()==DialogResult.OK)
				txtBmpPath.Text = saveBmp.FileName;		
		}

		private void btnBmpSave_Click(object sender, System.EventArgs e)
		{
			TotalViewPck.Instance.View.SaveBMP(txtBmpPath.Text,collection.Pal);
			this.Close();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			if(txtOut.Text=="")
			{
				MessageBox.Show(this,"You need to specify an output file","Save?",MessageBoxButtons.OK,MessageBoxIcon.Information);
				return;
			}

			string dir = txtOut.Text.Substring(0,txtOut.Text.LastIndexOf("\\"));
			string file = txtOut.Text.Substring(txtOut.Text.LastIndexOf("\\")+1);
			file = file.Substring(0,file.LastIndexOf("."));


			try
			{
				if (saveTabs.SelectedTab == tabSingle)
					((XCom.Interfaces.IXCImageFile)tabsSingle.SelectedTab.Tag).SaveCollection(dir, file, collection);
				else
					((XCom.Interfaces.IXCImageFile)tabsMulti.SelectedTab.Tag).SaveCollection(dir, file, collection);

				Close();
			}
			catch(Exception ex)
			{
				MessageBox.Show(this, "There were errors during save\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void SaveForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			foreach (XCom.Interfaces.IXCImageFile xcf in shared.GetImageModList())
				xcf.SavingOptions=null;
		}

		private void btnFind_Click(object sender, System.EventArgs e)
		{
			savePck.Filter="All Files|*.*";
			if(savePck.ShowDialog()==DialogResult.OK)
				txtOut.Text=savePck.FileName;
		}
	}
}
*/