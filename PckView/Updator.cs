using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace PckView
{
	public class Updator:DSShared.Interfaces.IUpdater
	{
		public string UpdatePath
		{
			get { return ""; }
		}

		public string DisplayDescription
		{
			get { return "Import/Export program for xcom image formats"; }
		}
	}
}