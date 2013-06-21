using System;

namespace DSShared.Interfaces
{
	/// <summary>
	/// XCSuite uses this interface to populate its list of items for version control
	/// Assemblies are loaded at runtime and a class implementing this interface is looked for
	/// </summary>
	public interface IUpdater
	{
		/// <summary>
		/// network path to update file
		/// </summary>
		string UpdatePath { get;}

		/// <summary>
		/// Description to display for this assembly
		/// </summary>
		string DisplayDescription { get;}
	}
}
