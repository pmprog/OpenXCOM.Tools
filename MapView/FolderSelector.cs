using System;
using System.Windows.Forms;

namespace MapView
{
	public class FolderSelector:System.Windows.Forms.Design.FolderNameEditor
	{
		public enum Folder
		{
			Desktop=(int)FolderBrowserFolder.Desktop,
			Favorites=(int)FolderBrowserFolder.Favorites,
			MyComputer=(int)FolderBrowserFolder.MyComputer,
			MyDocuments=(int)FolderBrowserFolder.MyDocuments,
			MyPictures=(int)FolderBrowserFolder.MyPictures,
			NetAndDialUpConnections=(int)FolderBrowserFolder.NetAndDialUpConnections,
			NetworkNeighborhood=(int)FolderBrowserFolder.NetworkNeighborhood,
			Printers=(int)FolderBrowserFolder.Printers,
			Recent=(int)FolderBrowserFolder.Recent,
			SendTo=(int)FolderBrowserFolder.SendTo,
			StartMenu=(int)FolderBrowserFolder.StartMenu,
			Templates=(int)FolderBrowserFolder.Templates
		}

		public enum BrowserStyles
		{
			BrowseForComputer,
			BrowseForEverything,
			BrowseForPrinter,
			RestrictToDomain,
			RestrictToFileSystem,
			RestrictToSubfolders,
			ShowTextBox
		}

		private FolderBrowser browser;

		public FolderSelector()
		{
			browser = new FolderBrowser();
		}

		public DialogResult ShowDialog()
		{
			return browser.ShowDialog();
		}

		public DialogResult ShowDialog(IWin32Window parent)
		{
			return browser.ShowDialog(parent);
		}

		/// <summary>
		/// The path the user has chosen
		/// </summary>
		public string DirectoryPath
		{
			get{return browser.DirectoryPath;}
		}

		/// <summary>
		/// gets or sets the caption of the window
		/// </summary>
		public string Description
		{
			get{return browser.Description;}
			set{browser.Description=value;}
		}

		public Folder StartLocation
		{
			get{return (Folder)browser.StartLocation;}
			set{browser.StartLocation=(FolderBrowserFolder)value;}
		}

		public BrowserStyles Style
		{
			get{return (BrowserStyles)browser.Style;}
			set{browser.Style=(FolderBrowserStyles)value;}
		}
	}
}

