//////////////////////////////////////////////////////////////////////
// General Purpose Dir/folder picker, 100% .NET, not relying on Internet Explorer's 
// shell extensions.
//////////////////////////////////////////////////////////////////////
// COPYRIGHTS:
// Copyright (c)2002 Solutions Design. All rights reserved.
// 
// Released under the following license: (BSD2)
// -------------------------------------------
// Redistribution and use in source and binary forms, with or without modification, 
// are permitted provided that the following conditions are met: 
//
// 1) Redistributions of source code must retain the above copyright notice, this list of 
//    conditions and the following disclaimer. 
// 2) Redistributions in binary form must reproduce the above copyright notice, this list of 
//    conditions and the following disclaimer in the documentation and/or other materials 
//    provided with the distribution. 
// 
// THIS SOFTWARE IS PROVIDED BY SOLUTIONS DESIGN ``AS IS'' AND ANY EXPRESS OR IMPLIED WARRANTIES, 
// INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A 
// PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL SOLUTIONS DESIGN OR CONTRIBUTORS BE LIABLE FOR 
// ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
// NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR 
// BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, 
// STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE 
// USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE. 
//
// The views and conclusions contained in the software and documentation are those of the authors 
// and should not be interpreted as representing official policies, either expressed or implied, 
// of Solutions Design. 
//
//////////////////////////////////////////////////////////////////////
// Contributers to the code:
//		- Frans Bouma [FB]
//////////////////////////////////////////////////////////////////////
// VERSION INFORMATION.
//
// V1.01: 09282002. Fixed some small issues.
// v1.0: First version
//////////////////////////////////////////////////////////////////////
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Management;
using System.IO;
using System.Runtime.InteropServices;

namespace MapView
{
	/// <summary>
	/// Purpose: general purpose folder/dir picker. 
	/// </summary>
	public class DirPicker : System.Windows.Forms.Form
	{
		#region Class Member Declarations
		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbxCurrentPath;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.TreeView tvMain;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ImageList ilMain;
		private bool	m_bCancelClicked = false;
		#endregion

		// For extracting the icons from the Shell DLL, so we get native icons in every OS version.
		// XP and up should use a manifest file.
		[DllImport("Shell32.dll",EntryPoint="ExtractIconExW",CharSet=CharSet.Unicode, ExactSpelling=true,CallingConvention=CallingConvention.StdCall)]
		public static extern int ExtractIconEx(string sFile,int iIndex, out IntPtr piLargeVersion, out IntPtr piSmallVersion, int iAmountIcons);

		public DirPicker()
		{
			IntPtr	piLarge, piSmall;
			Icon	icExtracted;

			InitializeComponent();

			// Add the icons to the image list.
			// Extract Normal folder
			ExtractIconEx("Shell32.dll", 3, out piLarge,out piSmall, 1);
			icExtracted= Icon.FromHandle(piSmall);
			ilMain.Images.Add(icExtracted);
			// Extract Open folder
			ExtractIconEx("Shell32.dll", 4, out piLarge,out piSmall, 1);
			icExtracted= Icon.FromHandle(piSmall);
			ilMain.Images.Add(icExtracted);
			// Extract A drive
			ExtractIconEx("Shell32.dll", 6, out piLarge,out piSmall, 1);
			icExtracted= Icon.FromHandle(piSmall);
			ilMain.Images.Add(icExtracted);
			// Extract Harddisk
			ExtractIconEx("Shell32.dll", 8, out piLarge,out piSmall, 1);
			icExtracted= Icon.FromHandle(piSmall);
			ilMain.Images.Add(icExtracted);
			// Extract Network drive
			ExtractIconEx("Shell32.dll", 9, out piLarge,out piSmall, 1);
			icExtracted= Icon.FromHandle(piSmall);
			ilMain.Images.Add(icExtracted);
			// Extract CDROM drive
			ExtractIconEx("Shell32.dll", 11, out piLarge,out piSmall, 1);
			icExtracted= Icon.FromHandle(piSmall);
			ilMain.Images.Add(icExtracted);
			
			InitTreeAndFillRoot();			
		}


		/// <summary>
		/// Purpose: Initializes the tree and fill the root node with the initial nodes.
		/// </summary>
		private void InitTreeAndFillRoot()
		{			
			// Add all devices in this machine. Get them from WMI. See for
			// detailed WMI information the WMI Platform SDK Documentation.
			SelectQuery sqLogicalDrives = new SelectQuery ("SELECT * FROM Win32_LogicalDisk");
			ManagementObjectSearcher mosSearcher = new ManagementObjectSearcher(sqLogicalDrives);
			ManagementObjectCollection mocLogicalDrives = mosSearcher.Get();

			tvMain.BeginUpdate();
			foreach (ManagementObject moDrive in mocLogicalDrives)
			{
				// add a node. Select an image for the drive type.
				TreeNode tnNode = tvMain.Nodes.Add(moDrive.Properties["DeviceID"].Value.ToString());
				int iDriveType = int.Parse(moDrive.Properties["DriveType"].Value.ToString());
				if(iDriveType < 2 || iDriveType > 5)
				{
					tnNode.ImageIndex = 0;
					tnNode.SelectedImageIndex = 0;
				}
				else
				{
					tnNode.ImageIndex = iDriveType;
					tnNode.SelectedImageIndex = iDriveType;
				}
				// add dummy node so the user can browse fast through the directories.
				// add an empty string, a directory can't be an empty string.
				TreeNode tnDummy = tnNode.Nodes.Add("");
			}
			tvMain.EndUpdate();
		}


		/// <summary>
		/// Purpose: checks if the selected node has childs. if not, the 'directory' the node
		/// represents is read and added as childnodes (1 node per directory entry). 
		/// Each node will have a dummy node, so the treeview will show a [+] for fast browsing.
		/// This dummy node is removed with the first call to this routine. 
		/// </summary>
		/// <param name="tvSelectedNode">The node selected by the user.</param>
		private void GetSubDirectoryNodes(TreeNode tnSelectedNode)
		{
			try
			{
				this.Cursor = Cursors.AppStarting;
				tvMain.BeginUpdate();
				
				// first strip off dummy node IF it's present
				if(tnSelectedNode.Nodes.Count == 1)
				{
					// check if it's the dummy
					TreeNode tnChild = tnSelectedNode.Nodes[0];
					if(tnChild.Text.Length==0)
					{
						// it's the dummy
						tnSelectedNode.Nodes.Remove(tnChild);
					}
				}
				
				if(tnSelectedNode.Nodes.Count <= 0)
				{
					// not expanded before. read subdirs
					string sFullPath = tnSelectedNode.FullPath + @"\";
					DirectoryInfo diCurrentDir = new DirectoryInfo(sFullPath);
					
					foreach(DirectoryInfo diDirectory in diCurrentDir.GetDirectories())
					{
						// add new node to current node
						TreeNode tnNode = tnSelectedNode.Nodes.Add(diDirectory.Name);
						tnNode.ImageIndex = 0;
						tnNode.SelectedImageIndex = 0;
						FileAttributes faAttributes = diDirectory.Attributes;
						// check the attributes, set foreground color.
						if(((faAttributes & FileAttributes.System) > 0) || ((faAttributes & FileAttributes.Hidden) > 0))
						{
							tnNode.ForeColor = Color.Gray;
						}
						if((faAttributes & FileAttributes.Compressed)>0)
						{
							tnNode.ForeColor = Color.Blue;
						}
						// add dummy node to new node created
						TreeNode tnDummy = tnNode.Nodes.Add("");
					}
				}
			}
			catch(Exception ex)
			{
				// View exception. Adjust this code to meet your exception viewing/handling
				MessageBox.Show("Error occured: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				tvMain.EndUpdate();
				this.Cursor = Cursors.Default;
			}
		}


		/// <summary>
		/// Purpose: when the property for the current path is set, this path should be reflected
		/// in the tree visible. The complete path is searched in the tree, and per directory element in
		/// the passed path, that directory is added to the tree.
		/// </summary>
		private void ReflectCurrentPathInTree()
		{
			TreeNode	tnPrevious, tnCurrent;
		
			string[] arrsDirsInCurrentPath = tbxCurrentPath.Text.Split('\\');

			if(arrsDirsInCurrentPath.Length<=0)
			{
				// invalid split.
				return;
			}
			tnCurrent = FindNodeInNodes(arrsDirsInCurrentPath[0],tvMain.Nodes);

			if(tnCurrent==null)
			{
				// not found. directory path is nonexisting.
				// select the first root node
				tnCurrent = tvMain.Nodes[0];
			}

			for(int i = 1; i < arrsDirsInCurrentPath.Length;i++)
			{
				tnPrevious = tnCurrent;
				// for this directory, first get all the subdirectories.
				GetSubDirectoryNodes(tnCurrent);
				
				// Get the node of the current directory.
				tnCurrent = FindNodeInNodes(arrsDirsInCurrentPath[i], tnPrevious.Nodes);
				if(tnCurrent!=null)
				{
					// found
					tnCurrent.Expand();
				}
				else
				{
					// not valid. quit for loop
					tnCurrent = tnPrevious;
					break;
				}
			}
			// select tnCurrent.
			tvMain.SelectedNode = tnCurrent;
			tnCurrent.EnsureVisible();
			Application.DoEvents();
		}


		/// <summary>
		/// Purpose: finds the node with the given sNodetext in the collection tncNodes. If found,
		/// a reference to the nodeobject is returned, otherwise null is returned
		/// </summary>
		/// <param name="sNodeText">Labeltext of TreeNode to find</param>
		/// <param name="tncNodes">Collection of TreeNode objects to search</param>
		/// <returns>reference to the searched TreeNode object if found, otherwise null.</returns>
		private TreeNode FindNodeInNodes(string sNodeText, TreeNodeCollection tncNodes)
		{
			foreach(TreeNode tnNode in tncNodes)
			{
				// Thanks to Uwe Hein for this tip.
				if(tnNode.Text.ToUpper() == sNodeText.ToUpper())
				{
					// found it
					return tnNode;
				}
			}
			// not found
			return null;
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
			this.components = new System.ComponentModel.Container();
			this.lblDescription = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.tvMain = new System.Windows.Forms.TreeView();
			this.ilMain = new System.Windows.Forms.ImageList(this.components);
			this.label1 = new System.Windows.Forms.Label();
			this.tbxCurrentPath = new System.Windows.Forms.TextBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblDescription
			// 
			this.lblDescription.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.lblDescription.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblDescription.Location = new System.Drawing.Point(6, 9);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = new System.Drawing.Size(390, 18);
			this.lblDescription.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.tvMain});
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(3, 30);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(393, 351);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Directory structure";
			// 
			// tvMain
			// 
			this.tvMain.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.tvMain.HideSelection = false;
			this.tvMain.ImageList = this.ilMain;
			this.tvMain.Indent = 19;
			this.tvMain.Location = new System.Drawing.Point(6, 15);
			this.tvMain.Name = "tvMain";
			this.tvMain.Size = new System.Drawing.Size(381, 321);
			this.tvMain.Sorted = true;
			this.tvMain.TabIndex = 0;
			this.tvMain.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvMain_AfterExpand);
			this.tvMain.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvMain_AfterCollapse);
			this.tvMain.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvMain_AfterSelect);
			this.tvMain.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvMain_BeforeExpand);
			// 
			// ilMain
			// 
			this.ilMain.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.ilMain.ImageSize = new System.Drawing.Size(16, 16);
			this.ilMain.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// label1
			// 
			this.label1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label1.Location = new System.Drawing.Point(6, 390);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 18);
			this.label1.TabIndex = 2;
			this.label1.Text = "Current path:";
			// 
			// tbxCurrentPath
			// 
			this.tbxCurrentPath.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.tbxCurrentPath.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.tbxCurrentPath.Location = new System.Drawing.Point(75, 387);
			this.tbxCurrentPath.Name = "tbxCurrentPath";
			this.tbxCurrentPath.ReadOnly = true;
			this.tbxCurrentPath.Size = new System.Drawing.Size(318, 20);
			this.tbxCurrentPath.TabIndex = 3;
			this.tbxCurrentPath.Text = "";
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(321, 414);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(72, 24);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.Location = new System.Drawing.Point(243, 414);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(72, 24);
			this.btnOK.TabIndex = 5;
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// frmDirPicker
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(400, 447);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.btnOK,
																		  this.btnCancel,
																		  this.tbxCurrentPath,
																		  this.label1,
																		  this.groupBox1,
																		  this.lblDescription});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "frmDirPicker";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select a directory";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			m_bCancelClicked = true;
			this.Close();
		}

		private void tvMain_AfterCollapse(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			// change image of collapsed node
			if(e.Node.Parent!=null)
			{
				e.Node.ImageIndex = 0;
				e.Node.SelectedImageIndex = 0;
			}
		}


		private void tvMain_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			// Mirror full path in textbox
			tbxCurrentPath.Text = e.Node.FullPath+@"\";
		}

		private void tvMain_BeforeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			// get the subdirectories if needed.
			GetSubDirectoryNodes(e.Node);

			Application.DoEvents();
		}

		private void tvMain_AfterExpand(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			// change image of expanded node.
			if(e.Node.Parent!=null)
			{
				e.Node.ImageIndex = 1;
				e.Node.SelectedImageIndex = 1;
			}
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		
		#region Class Property Declarations
		public string Description
		{
			get
			{
				return lblDescription.Text;
			}
			set
			{
				if(value!=null)
				{
					lblDescription.Text = value;
				}
				else
				{
					// invalid, throw exception.
					throw new ArgumentNullException("Description","Description can't be NULL");
				}
			}
		}
		public string CurrentPath
		{
			get
			{
				return tbxCurrentPath.Text;
			}
			set
			{
				if(value!=null)
				{
					tbxCurrentPath.Text = value;
					if(value.Length > 0)
					{
						ReflectCurrentPathInTree();
					}
				}
				else
				{
					// invalid, throw exception.
					throw new ArgumentNullException("CurrentPath","CurrentPath can't be NULL");
				}
			}
		}

		public bool CancelClicked
		{
			get
			{
				return m_bCancelClicked;
			}
		}
		#endregion
	}
}
