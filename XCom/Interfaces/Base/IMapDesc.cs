using System;
using System.IO;
using System.Collections.Generic;
using DSShared.Loadable;
using DSShared.Interfaces;

namespace XCom.Interfaces.Base
{
	public class IMapDesc:IAssemblyLoadable,IOpenSave
	{
		protected string name;
		protected string expDesc="No Description";
		protected string ext = ".unused";

		public IMapDesc() { throw new Exception("Parameterless constructor for IMapDesc should not be used"); }
		public IMapDesc(string name) { this.name = name; }

		public override string ToString() { return name; }
		public string Name { get { return name; } }
		public virtual IMap_Base GetMapFile()
		{
			throw new Exception("GetMapFile() is not overridden");
		}

		public virtual void Unload(){}

		public virtual string FileFilter
		{
			get
			{
				return "*" + ext + " - " + expDesc + "|*" + ext;
			}
		}

		/// <summary>
		/// See: AssemblyLoadable.RegisterFile
		/// </summary>
		/// <returns></returns>
		public virtual bool RegisterFile()
		{
			return GetType() != typeof(IMapDesc);
		}

		/// <summary>
		/// See: AssemblyLoadable.ExplorerDescription
		/// </summary>
		public virtual string ExplorerDescription { get { return expDesc; } }
	}
}