using System;
using System.Collections.Generic;
using System.Text;

//about the documentation tags
//http://msdn.microsoft.com/library/default.asp?url=/library/en-us/csref/html/vclrftagsfordocumentationcomments.asp

namespace DSShared
{
	/// <summary>
	/// Class that implements the IUpdater interface
	/// </summary>
	public class Updator : DSShared.Interfaces.IUpdater
	{
		/// <summary>
		/// See: IUpdater.UpdatePath
		/// </summary>
		public string UpdatePath
		{
			get { return ""; }
		}

		/// <summary>
		/// See: IUpdater.DisplayDescription
		/// </summary>
		public string DisplayDescription
		{
			get { return "Program neutral utility library"; }
		}
	}
}