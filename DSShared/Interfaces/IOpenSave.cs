using System;
using System.Collections.Generic;
using System.Text;

namespace DSShared.Interfaces
{
	/// <summary>
	/// Interface to define methods to property display information in open/save dialog boxes
	/// </summary>
	public interface IOpenSave
	{
		/// <summary>
		/// Short description to use in the open/save file dialog
		/// </summary>
		string ExplorerDescription { get;}

		/// <summary>
		/// a string in the format of "Description|*.ext" that will be added to the open/save file dialogs
		/// </summary>
		string FileFilter { get;}
	}
}
