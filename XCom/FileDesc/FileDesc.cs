using System;
using System.Collections.Generic;
using System.Text;

namespace XCom
{
	public abstract class FileDesc
	{
		private string path;

		public FileDesc(string path) { this.path = path; }

		public abstract void Save(string outFile);

		public string Path
		{
			get { return path; }
		}
	}
}
