using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using XCom;
using XCom.Interfaces;
using Microsoft.Win32;
using XCom.Interfaces.Base;

namespace MapView
{
	/// <summary>
	/// Summary description for PathsEditor.
	/// </summary>
	public class PathsEditor : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.MenuItem miFile;
		private System.Windows.Forms.TabControl tabs;
		private System.Windows.Forms.TabPage tabPaths;
		private System.Windows.Forms.TabPage tabMaps;
		private System.Windows.Forms.TabPage tabImages;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtMap;
		private System.Windows.Forms.TextBox txtImages;
		private System.Windows.Forms.TextBox txtCursor;
		private System.Windows.Forms.Label lblReminder;
		private System.Windows.Forms.ListBox lstImages;
		private System.Windows.Forms.TextBox txtImagePath;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox grpMapGroup;
		private System.Windows.Forms.GroupBox grpMap;
		private System.Windows.Forms.TextBox txtRoot;
		private System.Windows.Forms.TextBox txtRmp;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TreeView treeMaps;
		private System.Windows.Forms.ContextMenu cmTree;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ListBox listMapImages;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ListBox listAllImages;
		private System.Windows.Forms.Button btnMoveLeft;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button btnDown;
		private System.Windows.Forms.Button btnUp;
		private System.Windows.Forms.Button btnClearRegistry;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox txtPalettes;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Button btnFindMap;
		private System.Windows.Forms.Button btnFindImage;
		private System.Windows.Forms.OpenFileDialog openFile;
		private System.Windows.Forms.ContextMenu imagesCM;
		private System.Windows.Forms.MenuItem addImageset;
		private System.Windows.Forms.MenuItem delImageset;
		private System.Windows.Forms.MenuItem newGroup;
		private System.Windows.Forms.MenuItem addMap;
		private System.Windows.Forms.MenuItem delMap;
		private System.Windows.Forms.MenuItem delGroup;

		private static bool saveRegistry=true;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.Button runInstaller;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnSaveImages;
		private System.Windows.Forms.Label lblImage2;
		private System.Windows.Forms.TextBox txtImage2;
		private System.Windows.Forms.ComboBox cbPalette;
		private System.Windows.Forms.Button btnSaveMapEdit;
		private System.Windows.Forms.MenuItem addSub;
		private System.Windows.Forms.MenuItem delSub;
		private System.Windows.Forms.MenuItem addNewMap;
		private System.Windows.Forms.MenuItem addExistingMap;
		private System.Windows.Forms.Button btnEditTree;
		private System.Windows.Forms.MenuItem closeItem;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox txtBlank;
		private System.Windows.Forms.Button btnCopy;
		private System.Windows.Forms.Button btnPaste;
		private string paths;
		private string[] images;

		public PathsEditor(string pathsPath)
		{
			paths=pathsPath;
			InitializeComponent();

			//txtCursor.Text = GameInfo.CursorPath;
			//txtCursor.Text = GameInfo.MiscInfo.CursorFile;
			txtMap.Text = GameInfo.TilesetInfo.Path;
			txtImages.Text = GameInfo.ImageInfo.Path;
			txtImage2.Text=GameInfo.ImageInfo.Path;
			//txtPalettes.Text = GameInfo.PalettePath;

			populateImageList();

			populateTree();

			cbPalette.Items.Add(Palette.TFTDBattle);
			cbPalette.Items.Add(Palette.UFOBattle);
		}

		private void populateTree()
		{
			treeMaps.Nodes.Clear();
			ArrayList al = new ArrayList();
			foreach(object o in GameInfo.TilesetInfo.Tilesets.Keys)
				al.Add(o);

			al.Sort();		
			ArrayList al2 = new ArrayList();
			foreach(string o in al) //tileset
			{
				ITileset it = GameInfo.TilesetInfo.Tilesets[o];
				if(it==null)
					continue;

				TreeNode t = treeMaps.Nodes.Add(o); //make the node for the tileset
				
				al2.Clear();

				foreach(string o2 in it.Subsets.Keys) //subsets
					al2.Add(o2);

				al2.Sort();
				foreach(string o2 in al2)
				{
					Dictionary<string, IMapDesc> subset = it.Subsets[o2];
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

		private void populateImageList()
		{
			ImageInfo ii = GameInfo.ImageInfo;
			ArrayList al = new ArrayList();
			foreach(object o in ii.Images.Keys)
				al.Add(o);

			al.Sort();
			foreach(object o in al)
			{
				lstImages.Items.Add(o);
				listAllImages.Items.Add(o);
			}
		}

		public static bool SaveRegistry
		{
			get{return saveRegistry;}
			set{saveRegistry=value;}
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
			this.mainMenu = new System.Windows.Forms.MainMenu();
			this.miFile = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.closeItem = new System.Windows.Forms.MenuItem();
			this.tabs = new System.Windows.Forms.TabControl();
			this.tabPaths = new System.Windows.Forms.TabPage();
			this.btnSave = new System.Windows.Forms.Button();
			this.runInstaller = new System.Windows.Forms.Button();
			this.btnFindImage = new System.Windows.Forms.Button();
			this.btnFindMap = new System.Windows.Forms.Button();
			this.txtPalettes = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.btnClearRegistry = new System.Windows.Forms.Button();
			this.lblReminder = new System.Windows.Forms.Label();
			this.txtCursor = new System.Windows.Forms.TextBox();
			this.txtImages = new System.Windows.Forms.TextBox();
			this.txtMap = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.tabMaps = new System.Windows.Forms.TabPage();
			this.grpMap = new System.Windows.Forms.GroupBox();
			this.btnUp = new System.Windows.Forms.Button();
			this.btnDown = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.btnMoveLeft = new System.Windows.Forms.Button();
			this.listAllImages = new System.Windows.Forms.ListBox();
			this.label9 = new System.Windows.Forms.Label();
			this.listMapImages = new System.Windows.Forms.ListBox();
			this.label8 = new System.Windows.Forms.Label();
			this.grpMapGroup = new System.Windows.Forms.GroupBox();
			this.cbPalette = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.txtRmp = new System.Windows.Forms.TextBox();
			this.txtRoot = new System.Windows.Forms.TextBox();
			this.treeMaps = new System.Windows.Forms.TreeView();
			this.cmTree = new System.Windows.Forms.ContextMenu();
			this.newGroup = new System.Windows.Forms.MenuItem();
			this.delGroup = new System.Windows.Forms.MenuItem();
			this.addSub = new System.Windows.Forms.MenuItem();
			this.delSub = new System.Windows.Forms.MenuItem();
			this.addMap = new System.Windows.Forms.MenuItem();
			this.addNewMap = new System.Windows.Forms.MenuItem();
			this.addExistingMap = new System.Windows.Forms.MenuItem();
			this.delMap = new System.Windows.Forms.MenuItem();
			this.btnSaveMapEdit = new System.Windows.Forms.Button();
			this.btnEditTree = new System.Windows.Forms.Button();
			this.tabImages = new System.Windows.Forms.TabPage();
			this.lblImage2 = new System.Windows.Forms.Label();
			this.txtImage2 = new System.Windows.Forms.TextBox();
			this.btnSaveImages = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.txtImagePath = new System.Windows.Forms.TextBox();
			this.lstImages = new System.Windows.Forms.ListBox();
			this.imagesCM = new System.Windows.Forms.ContextMenu();
			this.addImageset = new System.Windows.Forms.MenuItem();
			this.delImageset = new System.Windows.Forms.MenuItem();
			this.openFile = new System.Windows.Forms.OpenFileDialog();
			this.label11 = new System.Windows.Forms.Label();
			this.txtBlank = new System.Windows.Forms.TextBox();
			this.btnCopy = new System.Windows.Forms.Button();
			this.btnPaste = new System.Windows.Forms.Button();
			this.tabs.SuspendLayout();
			this.tabPaths.SuspendLayout();
			this.tabMaps.SuspendLayout();
			this.grpMap.SuspendLayout();
			this.grpMapGroup.SuspendLayout();
			this.tabImages.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.miFile});
			// 
			// miFile
			// 
			this.miFile.Index = 0;
			this.miFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																				   this.menuItem1,
																				   this.closeItem});
			this.miFile.Text = "File";
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "Import map settings";
			// 
			// closeItem
			// 
			this.closeItem.Index = 1;
			this.closeItem.Text = "Close";
			this.closeItem.Click += new System.EventHandler(this.closeItem_Click);
			// 
			// tabs
			// 
			this.tabs.Controls.AddRange(new System.Windows.Forms.Control[] {
																			   this.tabPaths,
																			   this.tabMaps,
																			   this.tabImages});
			this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabs.Name = "tabs";
			this.tabs.SelectedIndex = 0;
			this.tabs.Size = new System.Drawing.Size(432, 427);
			this.tabs.TabIndex = 0;
			// 
			// tabPaths
			// 
			this.tabPaths.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.btnSave,
																				   this.runInstaller,
																				   this.btnFindImage,
																				   this.btnFindMap,
																				   this.txtPalettes,
																				   this.label10,
																				   this.btnClearRegistry,
																				   this.lblReminder,
																				   this.txtCursor,
																				   this.txtImages,
																				   this.txtMap,
																				   this.label3,
																				   this.label2,
																				   this.label1});
			this.tabPaths.Location = new System.Drawing.Point(4, 22);
			this.tabPaths.Name = "tabPaths";
			this.tabPaths.Size = new System.Drawing.Size(424, 329);
			this.tabPaths.TabIndex = 0;
			this.tabPaths.Text = "Paths";
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(8, 192);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(80, 23);
			this.btnSave.TabIndex = 14;
			this.btnSave.Text = "Save paths";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// runInstaller
			// 
			this.runInstaller.Location = new System.Drawing.Point(144, 296);
			this.runInstaller.Name = "runInstaller";
			this.runInstaller.Size = new System.Drawing.Size(88, 23);
			this.runInstaller.TabIndex = 13;
			this.runInstaller.Text = "Run installer";
			this.runInstaller.Click += new System.EventHandler(this.runInstaller_Click);
			// 
			// btnFindImage
			// 
			this.btnFindImage.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.btnFindImage.Location = new System.Drawing.Point(376, 32);
			this.btnFindImage.Name = "btnFindImage";
			this.btnFindImage.Size = new System.Drawing.Size(48, 23);
			this.btnFindImage.TabIndex = 11;
			this.btnFindImage.Text = "Find";
			this.btnFindImage.Click += new System.EventHandler(this.btnFindImage_Click);
			// 
			// btnFindMap
			// 
			this.btnFindMap.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.btnFindMap.Location = new System.Drawing.Point(376, 8);
			this.btnFindMap.Name = "btnFindMap";
			this.btnFindMap.Size = new System.Drawing.Size(48, 23);
			this.btnFindMap.TabIndex = 10;
			this.btnFindMap.Text = "Find";
			this.btnFindMap.Click += new System.EventHandler(this.btnFindMap_Click);
			// 
			// txtPalettes
			// 
			this.txtPalettes.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.txtPalettes.Location = new System.Drawing.Point(64, 80);
			this.txtPalettes.Name = "txtPalettes";
			this.txtPalettes.Size = new System.Drawing.Size(352, 20);
			this.txtPalettes.TabIndex = 9;
			this.txtPalettes.Text = "";
			this.txtPalettes.Visible = false;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(0, 80);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(64, 20);
			this.label10.TabIndex = 8;
			this.label10.Text = "Palettes:";
			this.label10.Visible = false;
			// 
			// btnClearRegistry
			// 
			this.btnClearRegistry.Location = new System.Drawing.Point(8, 296);
			this.btnClearRegistry.Name = "btnClearRegistry";
			this.btnClearRegistry.Size = new System.Drawing.Size(128, 23);
			this.btnClearRegistry.TabIndex = 7;
			this.btnClearRegistry.Text = "Clear Registry Settings";
			this.btnClearRegistry.Click += new System.EventHandler(this.btnClearRegistry_Click);
			// 
			// lblReminder
			// 
			this.lblReminder.Location = new System.Drawing.Point(8, 160);
			this.lblReminder.Name = "lblReminder";
			this.lblReminder.Size = new System.Drawing.Size(264, 32);
			this.lblReminder.TabIndex = 6;
			this.lblReminder.Text = "None of your changes will be made until you click the save button below";
			// 
			// txtCursor
			// 
			this.txtCursor.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.txtCursor.Location = new System.Drawing.Point(64, 56);
			this.txtCursor.Name = "txtCursor";
			this.txtCursor.Size = new System.Drawing.Size(352, 20);
			this.txtCursor.TabIndex = 5;
			this.txtCursor.Text = "";
			// 
			// txtImages
			// 
			this.txtImages.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.txtImages.Location = new System.Drawing.Point(64, 32);
			this.txtImages.Name = "txtImages";
			this.txtImages.Size = new System.Drawing.Size(312, 20);
			this.txtImages.TabIndex = 4;
			this.txtImages.Text = "";
			// 
			// txtMap
			// 
			this.txtMap.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.txtMap.Location = new System.Drawing.Point(64, 8);
			this.txtMap.Name = "txtMap";
			this.txtMap.Size = new System.Drawing.Size(312, 20);
			this.txtMap.TabIndex = 3;
			this.txtMap.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(0, 56);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64, 20);
			this.label3.TabIndex = 2;
			this.label3.Text = "Cursor:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(0, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 20);
			this.label2.TabIndex = 1;
			this.label2.Text = "Images:";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "Map Data:";
			// 
			// tabMaps
			// 
			this.tabMaps.Controls.AddRange(new System.Windows.Forms.Control[] {
																				  this.grpMap,
																				  this.grpMapGroup,
																				  this.treeMaps,
																				  this.btnSaveMapEdit,
																				  this.btnEditTree});
			this.tabMaps.Location = new System.Drawing.Point(4, 22);
			this.tabMaps.Name = "tabMaps";
			this.tabMaps.Size = new System.Drawing.Size(424, 401);
			this.tabMaps.TabIndex = 1;
			this.tabMaps.Text = "Map Files";
			// 
			// grpMap
			// 
			this.grpMap.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left);
			this.grpMap.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.btnPaste,
																				 this.btnCopy,
																				 this.btnUp,
																				 this.btnDown,
																				 this.button2,
																				 this.btnMoveLeft,
																				 this.listAllImages,
																				 this.label9,
																				 this.listMapImages,
																				 this.label8});
			this.grpMap.Enabled = false;
			this.grpMap.Location = new System.Drawing.Point(152, 184);
			this.grpMap.Name = "grpMap";
			this.grpMap.Size = new System.Drawing.Size(272, 215);
			this.grpMap.TabIndex = 2;
			this.grpMap.TabStop = false;
			this.grpMap.Text = "Map File";
			// 
			// btnUp
			// 
			this.btnUp.Location = new System.Drawing.Point(104, 32);
			this.btnUp.Name = "btnUp";
			this.btnUp.Size = new System.Drawing.Size(56, 23);
			this.btnUp.TabIndex = 7;
			this.btnUp.Text = "Up";
			this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
			// 
			// btnDown
			// 
			this.btnDown.Location = new System.Drawing.Point(104, 56);
			this.btnDown.Name = "btnDown";
			this.btnDown.Size = new System.Drawing.Size(56, 23);
			this.btnDown.TabIndex = 6;
			this.btnDown.Text = "Down";
			this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(104, 112);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(56, 23);
			this.button2.TabIndex = 5;
			this.button2.Text = "->";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// btnMoveLeft
			// 
			this.btnMoveLeft.Location = new System.Drawing.Point(104, 88);
			this.btnMoveLeft.Name = "btnMoveLeft";
			this.btnMoveLeft.Size = new System.Drawing.Size(56, 23);
			this.btnMoveLeft.TabIndex = 4;
			this.btnMoveLeft.Text = "<-";
			this.btnMoveLeft.Click += new System.EventHandler(this.btnMoveLeft_Click);
			// 
			// listAllImages
			// 
			this.listAllImages.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left);
			this.listAllImages.Location = new System.Drawing.Point(168, 32);
			this.listAllImages.Name = "listAllImages";
			this.listAllImages.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listAllImages.Size = new System.Drawing.Size(96, 173);
			this.listAllImages.TabIndex = 3;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(168, 16);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(96, 16);
			this.label9.TabIndex = 2;
			this.label9.Text = "Available Images";
			// 
			// listMapImages
			// 
			this.listMapImages.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left);
			this.listMapImages.Location = new System.Drawing.Point(8, 32);
			this.listMapImages.Name = "listMapImages";
			this.listMapImages.Size = new System.Drawing.Size(88, 173);
			this.listMapImages.TabIndex = 1;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(8, 16);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(100, 16);
			this.label8.TabIndex = 0;
			this.label8.Text = "Image List";
			// 
			// grpMapGroup
			// 
			this.grpMapGroup.Controls.AddRange(new System.Windows.Forms.Control[] {
																					  this.label11,
																					  this.txtBlank,
																					  this.cbPalette,
																					  this.label7,
																					  this.label6,
																					  this.label5,
																					  this.txtRmp,
																					  this.txtRoot});
			this.grpMapGroup.Enabled = false;
			this.grpMapGroup.Location = new System.Drawing.Point(152, 0);
			this.grpMapGroup.Name = "grpMapGroup";
			this.grpMapGroup.Size = new System.Drawing.Size(272, 184);
			this.grpMapGroup.TabIndex = 1;
			this.grpMapGroup.TabStop = false;
			this.grpMapGroup.Text = "Map Group";
			// 
			// cbPalette
			// 
			this.cbPalette.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbPalette.Location = new System.Drawing.Point(8, 152);
			this.cbPalette.Name = "cbPalette";
			this.cbPalette.Size = new System.Drawing.Size(104, 21);
			this.cbPalette.TabIndex = 5;
			this.cbPalette.SelectedIndexChanged += new System.EventHandler(this.cbPalette_SelectedIndexChanged);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8, 136);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(100, 16);
			this.label7.TabIndex = 4;
			this.label7.Text = "Palette";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 56);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 16);
			this.label6.TabIndex = 3;
			this.label6.Text = "Rmp Path";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 16);
			this.label5.TabIndex = 2;
			this.label5.Text = "Root Path";
			// 
			// txtRmp
			// 
			this.txtRmp.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.txtRmp.Location = new System.Drawing.Point(8, 72);
			this.txtRmp.Name = "txtRmp";
			this.txtRmp.Size = new System.Drawing.Size(256, 20);
			this.txtRmp.TabIndex = 1;
			this.txtRmp.Text = "";
			this.txtRmp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRmp_KeyPress);
			this.txtRmp.Leave += new System.EventHandler(this.txtRmp_Leave);
			// 
			// txtRoot
			// 
			this.txtRoot.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.txtRoot.Location = new System.Drawing.Point(8, 32);
			this.txtRoot.Name = "txtRoot";
			this.txtRoot.Size = new System.Drawing.Size(256, 20);
			this.txtRoot.TabIndex = 0;
			this.txtRoot.Text = "";
			this.txtRoot.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRoot_KeyPress);
			this.txtRoot.Leave += new System.EventHandler(this.txtRoot_Leave);
			// 
			// treeMaps
			// 
			this.treeMaps.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left);
			this.treeMaps.ContextMenu = this.cmTree;
			this.treeMaps.ImageIndex = -1;
			this.treeMaps.Location = new System.Drawing.Point(0, 24);
			this.treeMaps.Name = "treeMaps";
			this.treeMaps.SelectedImageIndex = -1;
			this.treeMaps.Size = new System.Drawing.Size(152, 376);
			this.treeMaps.TabIndex = 0;
			this.treeMaps.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeMaps_AfterSelect);
			// 
			// cmTree
			// 
			this.cmTree.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																				   this.newGroup,
																				   this.delGroup,
																				   this.addSub,
																				   this.delSub,
																				   this.addMap,
																				   this.delMap});
			// 
			// newGroup
			// 
			this.newGroup.Index = 0;
			this.newGroup.Text = "New group";
			this.newGroup.Click += new System.EventHandler(this.newGroup_Click);
			// 
			// delGroup
			// 
			this.delGroup.Index = 1;
			this.delGroup.Text = "Delete group";
			this.delGroup.Click += new System.EventHandler(this.delGroup_Click);
			// 
			// addSub
			// 
			this.addSub.Index = 2;
			this.addSub.Text = "Add sub-group";
			this.addSub.Click += new System.EventHandler(this.addSub_Click);
			// 
			// delSub
			// 
			this.delSub.Index = 3;
			this.delSub.Text = "Delete sub-group";
			this.delSub.Click += new System.EventHandler(this.delSub_Click);
			// 
			// addMap
			// 
			this.addMap.Index = 4;
			this.addMap.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																				   this.addNewMap,
																				   this.addExistingMap});
			this.addMap.Text = "Add map";
			// 
			// addNewMap
			// 
			this.addNewMap.Index = 0;
			this.addNewMap.Text = "New Map";
			this.addNewMap.Click += new System.EventHandler(this.addNewMap_Click);
			// 
			// addExistingMap
			// 
			this.addExistingMap.Index = 1;
			this.addExistingMap.Text = "Existing Map";
			this.addExistingMap.Click += new System.EventHandler(this.addExistingMap_Click);
			// 
			// delMap
			// 
			this.delMap.Index = 5;
			this.delMap.Text = "Delete map";
			this.delMap.Click += new System.EventHandler(this.delMap_Click);
			// 
			// btnSaveMapEdit
			// 
			this.btnSaveMapEdit.Name = "btnSaveMapEdit";
			this.btnSaveMapEdit.Size = new System.Drawing.Size(48, 23);
			this.btnSaveMapEdit.TabIndex = 8;
			this.btnSaveMapEdit.Text = "Save";
			this.btnSaveMapEdit.Click += new System.EventHandler(this.btnSaveMapEdit_Click);
			// 
			// btnEditTree
			// 
			this.btnEditTree.Location = new System.Drawing.Point(48, 0);
			this.btnEditTree.Name = "btnEditTree";
			this.btnEditTree.Size = new System.Drawing.Size(64, 23);
			this.btnEditTree.TabIndex = 6;
			this.btnEditTree.Text = "Edit Tree";
			this.btnEditTree.Click += new System.EventHandler(this.moveMaps_Click);
			// 
			// tabImages
			// 
			this.tabImages.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.lblImage2,
																					this.txtImage2,
																					this.btnSaveImages,
																					this.label4,
																					this.txtImagePath,
																					this.lstImages});
			this.tabImages.Location = new System.Drawing.Point(4, 22);
			this.tabImages.Name = "tabImages";
			this.tabImages.Size = new System.Drawing.Size(424, 329);
			this.tabImages.TabIndex = 2;
			this.tabImages.Text = "Image Files";
			// 
			// lblImage2
			// 
			this.lblImage2.Location = new System.Drawing.Point(120, 208);
			this.lblImage2.Name = "lblImage2";
			this.lblImage2.Size = new System.Drawing.Size(100, 16);
			this.lblImage2.TabIndex = 14;
			this.lblImage2.Text = "Image.dat path";
			// 
			// txtImage2
			// 
			this.txtImage2.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.txtImage2.Location = new System.Drawing.Point(120, 224);
			this.txtImage2.Name = "txtImage2";
			this.txtImage2.ReadOnly = true;
			this.txtImage2.Size = new System.Drawing.Size(272, 20);
			this.txtImage2.TabIndex = 12;
			this.txtImage2.Text = "";
			// 
			// btnSaveImages
			// 
			this.btnSaveImages.Location = new System.Drawing.Point(120, 248);
			this.btnSaveImages.Name = "btnSaveImages";
			this.btnSaveImages.TabIndex = 3;
			this.btnSaveImages.Text = "Save";
			this.btnSaveImages.Click += new System.EventHandler(this.btnSaveImages_Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(120, 96);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 16);
			this.label4.TabIndex = 2;
			this.label4.Text = "Path";
			// 
			// txtImagePath
			// 
			this.txtImagePath.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.txtImagePath.Location = new System.Drawing.Point(120, 112);
			this.txtImagePath.Name = "txtImagePath";
			this.txtImagePath.ReadOnly = true;
			this.txtImagePath.Size = new System.Drawing.Size(296, 20);
			this.txtImagePath.TabIndex = 1;
			this.txtImagePath.Text = "";
			this.txtImagePath.TextChanged += new System.EventHandler(this.txtImagePath_TextChanged);
			// 
			// lstImages
			// 
			this.lstImages.ContextMenu = this.imagesCM;
			this.lstImages.Dock = System.Windows.Forms.DockStyle.Left;
			this.lstImages.Name = "lstImages";
			this.lstImages.Size = new System.Drawing.Size(120, 329);
			this.lstImages.TabIndex = 0;
			this.lstImages.SelectedIndexChanged += new System.EventHandler(this.lstImages_SelectedIndexChanged);
			// 
			// imagesCM
			// 
			this.imagesCM.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.addImageset,
																					 this.delImageset});
			// 
			// addImageset
			// 
			this.addImageset.Index = 0;
			this.addImageset.Text = "Add";
			this.addImageset.Click += new System.EventHandler(this.addImageset_Click);
			// 
			// delImageset
			// 
			this.delImageset.Index = 1;
			this.delImageset.Text = "Remove";
			this.delImageset.Click += new System.EventHandler(this.delImageset_Click);
			// 
			// openFile
			// 
			this.openFile.Filter = "map files|*.map|dat files|*.dat|Pck files|*.pck|All files|*.*";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(8, 96);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(100, 16);
			this.label11.TabIndex = 7;
			this.label11.Text = "Blanks Path";
			// 
			// txtBlank
			// 
			this.txtBlank.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.txtBlank.Location = new System.Drawing.Point(8, 112);
			this.txtBlank.Name = "txtBlank";
			this.txtBlank.Size = new System.Drawing.Size(256, 20);
			this.txtBlank.TabIndex = 6;
			this.txtBlank.Text = "";
			// 
			// btnCopy
			// 
			this.btnCopy.Location = new System.Drawing.Point(104, 152);
			this.btnCopy.Name = "btnCopy";
			this.btnCopy.Size = new System.Drawing.Size(56, 23);
			this.btnCopy.TabIndex = 8;
			this.btnCopy.Text = "Copy";
			this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
			// 
			// btnPaste
			// 
			this.btnPaste.Location = new System.Drawing.Point(104, 176);
			this.btnPaste.Name = "btnPaste";
			this.btnPaste.Size = new System.Drawing.Size(56, 23);
			this.btnPaste.TabIndex = 9;
			this.btnPaste.Text = "Paste";
			this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
			// 
			// PathsEditor
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(432, 427);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.tabs});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Menu = this.mainMenu;
			this.MinimizeBox = false;
			this.Name = "PathsEditor";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "PathsEditor";
			this.tabs.ResumeLayout(false);
			this.tabPaths.ResumeLayout(false);
			this.tabMaps.ResumeLayout(false);
			this.grpMap.ResumeLayout(false);
			this.grpMapGroup.ResumeLayout(false);
			this.tabImages.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void lstImages_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			txtImagePath.Text = GameInfo.ImageInfo[(string)lstImages.SelectedItem].BasePath;
		}

		private void treeMaps_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			TreeNode t = e.Node;
			IXCTileset it = null;

			grpMapGroup.Enabled=true;
			grpMap.Enabled=false;
			delMap.Enabled=false;

			if(t.Parent!=null) 
			{
				addMap.Enabled=true;
				delSub.Enabled=true;

				if(t.Parent.Parent!=null)//inner node
				{
					grpMapGroup.Enabled=false;
					grpMap.Enabled=true;
					delMap.Enabled=true;

					it = (IXCTileset)GameInfo.TilesetInfo.Tilesets[t.Parent.Parent.Text];

					XCMapDesc imd = (XCMapDesc)it[t.Text];
					listMapImages.Items.Clear();
					if(imd != null)  
						foreach(string s in imd.Dependencies)  
							listMapImages.Items.Add(s);  
					else
						it.AddMap(new XCMapDesc(t.Text, it.MapPath, it.BlankPath, it.RmpPath, new string[] { }, it.Palette), t.Parent.Text); 
				}
				else //subset node
					it = (IXCTileset)GameInfo.TilesetInfo.Tilesets[t.Parent.Text];

			}
			else //parent node
			{
				it = (IXCTileset)GameInfo.TilesetInfo.Tilesets[t.Text];
				addMap.Enabled=false;
				delMap.Enabled=false;
				delSub.Enabled=false;
			}

			txtRoot.Text = it.MapPath;
			txtRmp.Text = it.RmpPath;
			txtBlank.Text=it.BlankPath;
			cbPalette.SelectedItem = it.Palette;
		}

		private void txtRoot_Leave(object sender, System.EventArgs e)
		{
			IXCTileset it = getCurrset();
			if(!Directory.Exists(txtRoot.Text))
			{
				if(NoDirForm.Show(txtRoot.Text)!=DialogResult.OK)
					txtRoot.Text = it.MapPath;
			}
			it.MapPath = txtRoot.Text;
		}

		private void txtRoot_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar==13)
				txtRoot_Leave(null,null);
		}

		private void txtRmp_Leave(object sender, System.EventArgs e)
		{
			IXCTileset it = getCurrset();
			if(!Directory.Exists(txtRmp.Text))
			{
				if(NoDirForm.Show(txtRmp.Text)!=DialogResult.OK)
					txtRmp.Text = it.RmpPath;
			}

			it.RmpPath = txtRmp.Text;
		}

		private void txtRmp_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(e.KeyChar==13)
				txtRmp_Leave(null,null);		
		}

		private void btnUp_Click(object sender, System.EventArgs e)
		{
			IXCTileset it = getCurrset();
			string[] dep = ((XCMapDesc)it[treeMaps.SelectedNode.Text]).Dependencies;
			for(int i=1;i<dep.Length;i++)
			{
				if(dep[i]==(string)listMapImages.SelectedItem)
				{
					string old = dep[i-1];
					dep[i-1]=dep[i];
					dep[i]=old;
					((XCMapDesc)it[treeMaps.SelectedNode.Text]).Dependencies = dep;
					listMapImages.Items.Clear();

					foreach(string s in dep)
						listMapImages.Items.Add(s);
					listMapImages.SelectedItem = dep[i-1];
					return;
				}
			}
		}

		private void btnDown_Click(object sender, System.EventArgs e)
		{
			IXCTileset it = getCurrset();
			string[] dep = ((XCMapDesc)it[treeMaps.SelectedNode.Text]).Dependencies;
			for(int i=0;i<dep.Length-1;i++)
			{
				if(dep[i]==(string)listMapImages.SelectedItem)
				{
					string old = dep[i+1];
					dep[i+1]=dep[i];
					dep[i]=old;
					((XCMapDesc)it[treeMaps.SelectedNode.Text]).Dependencies = dep;
					
					listMapImages.Items.Clear();
					foreach(string s in dep)
						listMapImages.Items.Add(s);
					listMapImages.SelectedItem = dep[i+1];
					return;
				}
			}		
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			IXCTileset it = getCurrset();
			ArrayList dep = new ArrayList(((XCMapDesc)it[treeMaps.SelectedNode.Text]).Dependencies);
			dep.Remove(listMapImages.SelectedItem);
			((XCMapDesc)it[treeMaps.SelectedNode.Text]).Dependencies = (string[])dep.ToArray(typeof(string));
			
			listMapImages.Items.Clear();
			foreach(string s in dep)
				listMapImages.Items.Add(s);
		}

		private void btnMoveLeft_Click(object sender, System.EventArgs e)
		{
			IXCTileset it = getCurrset();
			ArrayList dep = new ArrayList(((XCMapDesc)it[treeMaps.SelectedNode.Text]).Dependencies);
			foreach(object o in listAllImages.SelectedItems)
				if(!dep.Contains(o))
				{
					dep.Add(o);
					((XCMapDesc)it[treeMaps.SelectedNode.Text]).Dependencies = (string[])dep.ToArray(typeof(string));
				
					listMapImages.Items.Clear();
					foreach(string s in dep)
						listMapImages.Items.Add(s);
				}		
		}

		private void btnClearRegistry_Click(object sender, System.EventArgs e)
		{
			RegistryKey swKey = Registry.CurrentUser.OpenSubKey("Software",true);
			swKey.DeleteSubKeyTree("MapView");
			saveRegistry=false;
		}

		private void btnFindMap_Click(object sender, System.EventArgs e)
		{
			openFile.Title="Find the map data file";
			openFile.Multiselect=false;
			openFile.FilterIndex=1;
			if(openFile.ShowDialog()==DialogResult.OK)
				txtMap.Text = openFile.FileName;
		}

		private void btnFindImage_Click(object sender, System.EventArgs e)
		{
			openFile.Title="Find the image data file";
			openFile.Multiselect=false;
			openFile.FilterIndex=1;
			if(openFile.ShowDialog()==DialogResult.OK)
				txtImages.Text = openFile.FileName;		
		}
/*
		private void miSave_Click(object sender, System.EventArgs e)
		{
			StreamWriter sw = new StreamWriter(new FileStream(this.paths,FileMode.Create));
			
			GameInfo.CursorPath = txtCursor.Text;
			GameInfo.MapPath = txtMap.Text;
			GameInfo.ImagePath = txtImages.Text;
			//GameInfo.PalettePath = txtPalettes.Text;

			sw.WriteLine("mapdata:"+txtMap.Text);
			sw.WriteLine("images:"+txtImages.Text);
			sw.WriteLine("cursor:"+txtCursor.Text);
			//sw.WriteLine("palettes:"+txtPalettes.Text);

			sw.Flush();
			sw.Close();

			//GameInfo.GetImageInfo().Save(new FileStream(txtImages.Text,FileMode.Create));
			//GameInfo.GetTileInfo().Save(new FileStream(txtMap.Text,FileMode.Create));
		}*/

		private void txtImagePath_TextChanged(object sender, System.EventArgs e)
		{
			GameInfo.ImageInfo[(string)lstImages.SelectedItem].BasePath = txtImagePath.Text;
		}

		private void addImageset_Click(object sender, System.EventArgs e)
		{
			openFile.Title="Add images";
			openFile.Multiselect=true;
			openFile.FilterIndex=2;
			if(openFile.ShowDialog(this)==DialogResult.OK)
			{
				foreach(string s in openFile.FileNames)
				{
					string path = s.Substring(0,s.LastIndexOf(@"\")+1);
					string file = s.Substring(s.LastIndexOf(@"\")+1);
					file = file.Substring(0,file.IndexOf("."));
					GameInfo.ImageInfo[file] = new ImageDescriptor(file,path);
				}

				lstImages.Items.Clear();
				listAllImages.Items.Clear();

				populateImageList();
			}
		}

		private void delImageset_Click(object sender, System.EventArgs e)
		{
			GameInfo.ImageInfo.Images.Remove(lstImages.SelectedItem.ToString());

			lstImages.Items.Clear();
			listAllImages.Items.Clear();

			populateImageList();
		}

		private void runInstaller_Click(object sender, System.EventArgs e)
		{
			InstallWindow iw = new InstallWindow();
			iw.ShowDialog(this);
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			StreamWriter sw = new StreamWriter(new FileStream(this.paths,FileMode.Create));
			
			//GameInfo.CursorPath = txtCursor.Text;
			//GameInfo.MapPath = txtMap.Text;
			//GameInfo.ImagePath = txtImages.Text;
			//GameInfo.PalettePath = txtPalettes.Text;

			sw.WriteLine("mapdata:"+txtMap.Text);
			sw.WriteLine("images:"+txtImages.Text);
			sw.WriteLine("cursor:"+txtCursor.Text);
			//sw.WriteLine("palettes:"+txtPalettes.Text);

			sw.Flush();
			sw.Close();
		}

		private void btnSaveImages_Click(object sender, System.EventArgs e)
		{
			GameInfo.ImageInfo.Save(txtImages.Text);
		}

		private void newGroup_Click(object sender, System.EventArgs e)
		{
			TilesetForm tf = new TilesetForm();
			tf.ShowDialog(this);

			if(tf.TilesetText!=null)
			{
				IXCTileset tSet = (IXCTileset)GameInfo.TilesetInfo.AddTileset(tf.TilesetText, tf.MapPath, tf.RmpPath, tf.BlankPath);
				//addTileset(tSet.Name);
				treeMaps.Nodes.Add(tSet.Name);

				txtRoot.Text=tSet.MapPath;
				txtRmp.Text=tSet.RmpPath;

				//saveMapedit();
			}
		}

		private void cbPalette_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(treeMaps.SelectedNode.Parent==null)
				getCurrset().Palette = (Palette)cbPalette.SelectedItem;
		}

		private void saveMapedit()
		{
			System.Windows.Forms.Cursor oldCur = System.Windows.Forms.Cursor.Current;
			System.Windows.Forms.Cursor.Current=Cursors.WaitCursor;

			string path=txtMap.Text.Substring(0,txtMap.Text.LastIndexOf("\\")+1);
			string file=txtMap.Text.Substring(txtMap.Text.LastIndexOf("\\")+1);
			string ext = file.Substring(file.LastIndexOf("."));
			file = file.Substring(0,file.LastIndexOf("."));

			StreamWriter sw = new StreamWriter(new FileStream(path+file+".new",FileMode.Create));
			try
			{
				//GameInfo.TilesetInfo.Save(sw);
				sw.Flush();
				sw.Close();

				if(File.Exists(txtMap.Text))
				{
					if(File.Exists(path+file+".old"))
						File.Delete(path+file+".old");
					File.Move(txtMap.Text,path+file+".old");
				}

				File.Move(path+file+".new",txtMap.Text);

				System.Windows.Forms.Cursor.Current=oldCur;
				Text="PathsEditor : Saved Mapedit.dat";
			}
			catch(Exception e)
			{
				sw.Close();

				if(File.Exists(path+file+".new"))
					File.Delete(path+file+".new");

				throw e;
			}			
		}

		private void btnSaveMapEdit_Click(object sender, System.EventArgs e)
		{
			saveMapedit();
		}

		private void addSub_Click(object sender, System.EventArgs e)
		{
			SubsetForm ss = new SubsetForm();
			ss.ShowDialog(this);
			if(ss.SubsetName!=null)
			{
				IXCTileset it = getCurrset();
				TreeNode t = treeMaps.SelectedNode;

				it.Subsets[ss.SubsetName] = new Dictionary<string, IMapDesc>();
				//it.NewSubset(ss.SubsetName);
				//saveMapedit();
				populateTree();
			}
		}

		private IXCTileset getCurrset()
		{
			TreeNode t = treeMaps.SelectedNode;

			if(t.Parent!=null) 
			{
				if(t.Parent.Parent!=null)//inner node
					return (IXCTileset)GameInfo.TilesetInfo.Tilesets[t.Parent.Parent.Text];
				else //subset node
					return (IXCTileset)GameInfo.TilesetInfo.Tilesets[t.Parent.Text];
			}
			else //parent node
				return (IXCTileset)GameInfo.TilesetInfo.Tilesets[t.Text];
		}
/*
		private void btnSave2_Click(object sender, System.EventArgs e)
		{
			saveMapedit();
		}*/

		private void addNewMap_Click(object sender, System.EventArgs e)
		{
			NewMapForm nfm = new NewMapForm();
			nfm.ShowDialog(this);

			if(nfm.MapName!=null)
			{
				if(treeMaps.SelectedNode.Parent!=null) //add to here
				{
					string path=txtRoot.Text+nfm.MapName+".MAP";
					if(File.Exists(path))
					{
						ChoiceDialog cd = new ChoiceDialog(path);
						cd.ShowDialog(this);
						switch(cd.Choice)
						{
							case Choice.Cancel:
								return;
							case Choice.Overwrite:
								XCMapFile.NewMap(File.OpenWrite(txtRoot.Text+nfm.MapName+".MAP"),nfm.MapRows,nfm.MapCols,nfm.MapHeight);
								FileStream fs = File.OpenWrite(txtRmp.Text+nfm.MapName+".RMP");
								fs.Close();
								break;
							case Choice.UseExisting:
								break;
						}
					}
					else
					{
						XCMapFile.NewMap(File.OpenWrite(txtRoot.Text+nfm.MapName+".MAP"),nfm.MapRows,nfm.MapCols,nfm.MapHeight);
						FileStream fs = File.OpenWrite(txtRmp.Text+nfm.MapName+".RMP");
						fs.Close();
					}

					IXCTileset tSet;
					string sSet="";

					if(treeMaps.SelectedNode.Parent.Parent==null)//subset
					{
						tSet = (IXCTileset)GameInfo.TilesetInfo.Tilesets[treeMaps.SelectedNode.Parent.Text];
						treeMaps.SelectedNode.Nodes.Add(nfm.MapName);
						sSet = treeMaps.SelectedNode.Text;
					}
					else //subset is parent
					{
						tSet = (IXCTileset)GameInfo.TilesetInfo.Tilesets[treeMaps.SelectedNode.Parent.Parent.Text];
						treeMaps.SelectedNode.Parent.Nodes.Add(nfm.MapName);
						sSet = treeMaps.SelectedNode.Parent.Text;
					}

					//Type1MapData tmd = new Type1MapData(nfm.MapName,nfm.mapp
					
					tSet.AddMap(nfm.MapName,sSet);
					//Console.WriteLine("map added: "+tSet.Name);
					//saveMapedit();
				}
				else //top node, baaaaad
				{
					//					tSet = GameInfo.GetTileInfo()[treeMaps.SelectedNode.Parent.Text];					
					//					treeMaps.SelectedNode.Parent.Nodes.Add(nfm.MapName);
				}								
			}
		}

		private void addExistingMap_Click(object sender, System.EventArgs e)
		{
			openFile.InitialDirectory=txtRoot.Text;
			openFile.Title="Select maps from this directory only";
			openFile.Multiselect=true;
			openFile.RestoreDirectory=true;

			if(openFile.ShowDialog()==DialogResult.OK)
			{
				if(treeMaps.SelectedNode.Parent!=null) //add to here
				{
					TreeNode subset;
					if(treeMaps.SelectedNode.Parent.Parent==null)
						subset = treeMaps.SelectedNode;
					else
						subset = treeMaps.SelectedNode.Parent;

					IXCTileset tSet = (IXCTileset)GameInfo.TilesetInfo.Tilesets[subset.Parent.Text];
					foreach(string file in openFile.FileNames)
					{
						int start = file.LastIndexOf(@"\")+1;
						int end = file.LastIndexOf(".");

						string name = file.Substring(start,end-start);
						tSet.AddMap(name,subset.Text);
						subset.Nodes.Add(name);
					}

					//saveMapedit();
				}
				else //top node, baaaaad
				{
					//					tSet = GameInfo.GetTileInfo()[treeMaps.SelectedNode.Parent.Text];					
					//					treeMaps.SelectedNode.Parent.Nodes.Add(nfm.MapName);
				}	
			}
		}

		private void delGroup_Click(object sender, System.EventArgs e)
		{
			IXCTileset tSet = getCurrset();
			GameInfo.TilesetInfo.Tilesets[tSet.Name] = null;
			if(treeMaps.SelectedNode.Parent==null)
				treeMaps.Nodes.Remove(treeMaps.SelectedNode);
			else
			{
				if(treeMaps.SelectedNode.Parent.Parent==null)
					treeMaps.Nodes.Remove(treeMaps.SelectedNode.Parent);
				else
					treeMaps.Nodes.Remove(treeMaps.SelectedNode.Parent.Parent);
			}

			//saveMapedit();
		}

		private void delSub_Click(object sender, System.EventArgs e)
		{
			TreeNode subset=null;

			if(treeMaps.SelectedNode.Parent!=null)
			{
				if(treeMaps.SelectedNode.Parent.Parent==null)
					subset = treeMaps.SelectedNode;
				else
					subset = treeMaps.SelectedNode.Parent;
			}

			if(subset!=null)
			{
				IXCTileset tSet = getCurrset();
				tSet.Subsets[subset.Text]=null;
				subset.Parent.Nodes.Remove(subset);
			}
		}

		private void delMap_Click(object sender, System.EventArgs e)
		{
			TreeNode map = null;

			if(treeMaps.SelectedNode.Parent!=null)
			{
				if(treeMaps.SelectedNode.Parent.Parent!=null)
					map = treeMaps.SelectedNode;
			}

			if(map!=null)
			{
				IXCTileset tSet = getCurrset();
				tSet.Subsets[map.Parent.Text][map.Text]=null;
				tSet[map.Text]=null;

				map.Parent.Nodes.Remove(map);
			}
		}

		private void moveMaps_Click(object sender, System.EventArgs e)
		{
			//TreeEditor mmf = new TreeEditor();
			//mmf.ShowDialog(this);
			//populateTree();


			//saveMapedit();
		}

		private void closeItem_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void btnCopy_Click(object sender, System.EventArgs e)
		{
			IXCTileset it = getCurrset();
			images = new string[((XCMapDesc)it[treeMaps.SelectedNode.Text]).Dependencies.Length];

			for(int i=0;i<((XCMapDesc)it[treeMaps.SelectedNode.Text]).Dependencies.Length;i++)
				images[i]=((XCMapDesc)it[treeMaps.SelectedNode.Text]).Dependencies[i];	
		}

		private void btnPaste_Click(object sender, System.EventArgs e)
		{
			IXCTileset it = getCurrset();
			((XCMapDesc)it[treeMaps.SelectedNode.Text]).Dependencies = new string[images.Length];

			listMapImages.Items.Clear();

			for(int i=0;i<images.Length;i++)
			{
				((XCMapDesc)it[treeMaps.SelectedNode.Text]).Dependencies[i]=images[i];	
				listMapImages.Items.Add(images[i]);
			}
		}
	}
}
