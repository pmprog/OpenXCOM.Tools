/*
using System;
using XCom.Interfaces;
using System.Reflection;
using System.IO;
using System.Collections;

namespace XCom
{
	public class Loader:ILoader
	{
		ArrayList mapList;
		Assembly thisAssembly;
		string basename;

		private static readonly Loader myInstance = new Loader();

		private Loader()
		{
			thisAssembly = Assembly.GetExecutingAssembly();
			mapList = new ArrayList(thisAssembly.GetManifestResourceNames());
			basename = "XCom._Embedded.";
		}

		public static ILoader Instance
		{
			get{return myInstance;}
		}

		public Stream GetFile(string file)
		{
			return thisAssembly.GetManifestResourceStream(basename+file);
		}

		public IList MapList
		{
			get{return mapList;}
		}
	}
}
*/