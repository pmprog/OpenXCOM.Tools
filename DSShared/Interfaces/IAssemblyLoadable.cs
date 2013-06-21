using System;
using System.Collections.Generic;
using System.Text;

namespace DSShared.Loadable
{
	/// <summary>
	/// Top-level interface for user-created addons. The classes that will be actively searched 
	/// for at runtime will be defined by the program's author.
	/// </summary>
	public interface IAssemblyLoadable
	{
		/// <summary>
		/// If this returns false, Unload() will be called immediately
		/// </summary>
		/// <returns></returns>
		bool RegisterFile();

		/// <summary>
		/// Called when this object needs to detach itself from the system. Usually with program shutdown.
		/// Any exceptions raised will not be caught. You break it you buy it
		/// </summary>
		void Unload();
	}
}
