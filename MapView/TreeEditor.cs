/*using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using XCom;
using XCom.Interfaces;
using XCom.Interfaces.Base;

namespace MapView
{
	public class TreeEditor : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TreeView treeLeft;
		private System.Windows.Forms.TreeView treeRight;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Button btnDone;
		private System.Windows.Forms.Button btnMove;
		private System.Windows.Forms.Button btnRename;
		private System.Windows.Forms.GroupBox boxRename;
		private System.ComponentModel.Container components = null;

		public TreeEditor()
		{
			InitializeComponent();

			populateLeft();

			populateRight();
		}

		private void populateRight()
		{
			treeRight.Nodes.Clear();
			ArrayList al = new ArrayList();
			foreach(object o in GameInfo.GetTileInfo().Tilesets.Keys)
				al.Add(o);

			al.Sort();		
			ArrayList al2 = new ArrayList();
			foreach(string o in al) //tileset
			{
				IXCTileset it = (IXCTileset)GameInfo.GetTileInfo().Tilesets[o];
				if(it==null)
					continue;

				TreeNode t = treeRight.Nodes.Add(o); //make the node for the tileset
				
				al2.Clear();

				foreach(string o2 in it.Subsets.Keys) //subsets
					al2.Add(o2);

				al2.Sort();
				foreach(string o2 in al2)
				{
					Dictionary<string, IXCMapData> subset = it.Subsets[o2];
					if(subset==null)
						continue;

					TreeNode subsetNode = t.Nodes.Add(o2);
				}
			}
		}

		private void populateLeft()
		{
			treeLeft.Nodes.Clear();
			ArrayList al = new ArrayList();
			foreach(object o in GameInfo.GetTileInfo().Tilesets.Keys)
				al.Add(o);

			al.Sort();		
			ArrayList al2 = new ArrayList();
			foreach(string o in al) //tileset
			{
				IXCTileset it = (IXCTileset)GameInfo.GetTileInfo().Tilesets[o];
				if(it==null)
					continue;

				TreeNode t = treeLeft.Nodes.Add(o); //make the node for the tileset
				
				al2.Clear();

				foreach(string o2 in it.Subsets.Keys) //subsets
					al2.Add(o2);

				al2.Sort();
				foreach(string o2 in al2)
				{
					Dictionary<string, IMapData> subset = it.Subsets[o2];
					if(subset==null)
						continue;

					TreeNode subsetNode = t.Nodes.Add(o2);

					ArrayList sKeys = new ArrayList();
					foreach(string sKey in subset.Keys)
						sKeys.Add(sKey);

					sKeys.Sort();

					foreach(string sKey in sKeys)
					{
						if(subset[sKey]==null)
							continue;
						subsetNode.Nodes.Add(sKey);
					}
				}
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
			this.treeLeft = new System.Windows.Forms.TreeView();
			this.treeRight = new System.Windows.Forms.TreeView();
			this.btnMove = new System.Windows.Forms.Button();
			this.txtName = new System.Windows.Forms.TextBox();
			this.btnRename = new System.Windows.Forms.Button();
			this.btnDone = new System.Windows.Forms.Button();
			this.boxRename = new System.Windows.Forms.GroupBox();
			this.boxRename.SuspendLayout();
			this.SuspendLayout();
			// 
			// treeLeft
			// 
			this.treeLeft.CheckBoxes = true;
			this.treeLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.treeLeft.ImageIndex = -1;
			this.treeLeft.Name = "treeLeft";
			this.treeLeft.SelectedImageIndex = -1;
			this.treeLeft.Size = new System.Drawing.Size(160, 221);
			this.treeLeft.TabIndex = 0;
			this.treeLeft.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeLeft_AfterSelect);
			// 
			// treeRight
			// 
			this.treeRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.treeRight.ImageIndex = -1;
			this.treeRight.Location = new System.Drawing.Point(275, 0);
			this.treeRight.Name = "treeRight";
			this.treeRight.SelectedImageIndex = -1;
			this.treeRight.Size = new System.Drawing.Size(160, 221);
			this.treeRight.TabIndex = 1;
			this.treeRight.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeRight_AfterSelect);
			// 
			// btnMove
			// 
			this.btnMove.Enabled = false;
			this.btnMove.Location = new System.Drawing.Point(180, 99);
			this.btnMove.Name = "btnMove";
			this.btnMove.TabIndex = 3;
			this.btnMove.Text = "->";
			this.btnMove.Click += new System.EventHandler(this.btnMove_Click);
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(8, 16);
			this.txtName.Name = "txtName";
			this.txtName.TabIndex = 5;
			this.txtName.Text = "";
			// 
			// btnRename
			// 
			this.btnRename.Location = new System.Drawing.Point(20, 40);
			this.btnRename.Name = "btnRename";
			this.btnRename.TabIndex = 6;
			this.btnRename.Text = "Rename";
			this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
			// 
			// btnDone
			// 
			this.btnDone.Location = new System.Drawing.Point(180, 192);
			this.btnDone.Name = "btnDone";
			this.btnDone.TabIndex = 7;
			this.btnDone.Text = "Done";
			this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
			// 
			// boxRename
			// 
			this.boxRename.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.btnRename,
																					this.txtName});
			this.boxRename.Dock = System.Windows.Forms.DockStyle.Top;
			this.boxRename.Location = new System.Drawing.Point(160, 0);
			this.boxRename.Name = "boxRename";
			this.boxRename.Size = new System.Drawing.Size(115, 72);
			this.boxRename.TabIndex = 8;
			this.boxRename.TabStop = false;
			this.boxRename.Text = "Rename Selected";
			// 
			// TreeEditor
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(435, 221);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.boxRename,
																		  this.btnDone,
																		  this.btnMove,
																		  this.treeRight,
																		  this.treeLeft});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TreeEditor";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Move Maps";
			this.boxRename.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		private void btnDone_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void btnRename_Click(object sender, System.EventArgs e)
		{
/*			TreeNode sel = treeLeft.SelectedNode;

			if(sel.Parent==null) //top node
			{
				IXCTileset tSet = getCurrset();
				GameInfo.GetTileInfo()[sel.Text]=null;

				sel.Text=txtName.Text;
				tSet.Name=sel.Text;
				GameInfo.GetTileInfo()[sel.Text]=tSet;
			}
			else if(sel.Parent.Parent==null)
			{
				IXCTileset tSet = getCurrset();
				Hashtable subset = (Hashtable)tSet.Subsets[sel.Text];
				tSet.Subsets[sel.Text]=null;

				sel.Text=txtName.Text;

				tSet.Subsets[sel.Text]=subset;				
			}

			populateRight();*/
/*		}

		private IXCTileset getCurrset()
		{
			TreeNode t = treeLeft.SelectedNode;

			if(t.Parent!=null) 
			{
				if(t.Parent.Parent!=null)//inner node
					return (IXCTileset)GameInfo.GetTileInfo().Tilesets[t.Parent.Parent.Text];
				else //subset node
					return (IXCTileset)GameInfo.GetTileInfo().Tilesets[t.Parent.Text];
			}
			else //parent node
				return (IXCTileset)GameInfo.GetTileInfo().Tilesets[t.Text];
		}

		private void treeLeft_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			boxRename.Enabled=true;
			if(treeLeft.SelectedNode.Parent!=null)
			{
				if(treeLeft.SelectedNode.Parent.Parent!=null) //map
				{
					boxRename.Enabled=false;
				}
				else //subset
				{

				}
			}
			else //top level node
			{

			}
		}

		private void treeRight_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			btnMove.Enabled=false;
			if(treeRight.SelectedNode.Parent!=null)
				btnMove.Enabled=true;
		}

		private void btnMove_Click(object sender, System.EventArgs e)
		{
			TreeNode destSet = treeRight.SelectedNode;

			for(int i=0;i<treeLeft.Nodes.Count;i++)
			{
				TreeNode top = treeLeft.Nodes[i];
				for(int j=0;j<top.Nodes.Count;j++)
				{
					TreeNode sub = top.Nodes[j];
					for(int k=0;k<sub.Nodes.Count;k++)
					{
						TreeNode map = sub.Nodes[k];
						if(map.Checked)
						{
							IXCMapData imd = ((IXCTileset)GameInfo.GetTileInfo().Tilesets[top.Text]).RemoveMap(map.Text, sub.Text);
							((IXCTileset)GameInfo.GetTileInfo().Tilesets[destSet.Parent.Text]).AddMap(imd, destSet.Text);
							
						}
					}
				}
			}

			populateLeft();
		}
	}
}
*/