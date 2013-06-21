using System;
using System.Collections.Generic;
using XCom.Interfaces;

namespace XCom
{
	public class SharedSpace
	{
		private static SharedSpace instance;

		private Dictionary<string,object> mySpace;
		private SharedSpace()
		{
			mySpace = new Dictionary<string,object>();
		}

		public static SharedSpace Instance
		{
			get{if(instance==null)instance=new SharedSpace();return instance;}
		}

		public object GetObj(string key)
		{
			return GetObj(key, null);
		}

		public object GetObj(string key, object defaultIfNull)
		{
			if (!mySpace.ContainsKey(key))
				mySpace.Add(key, defaultIfNull);
			else if (mySpace[key] == null)
				mySpace[key] = defaultIfNull;

			return mySpace[key];
		}

		public object this[string key]
		{
			get{return mySpace[key];}
			set{mySpace[key]=value;}
		}

		public int GetInt(string key)
		{
			return (int)mySpace[key];
		}

		public string GetString(string key)
		{
			return (string)mySpace[key];
		}

		public double GetDouble(string key)
		{
			return (double)mySpace[key];
		}

		public List<XCom.Interfaces.IXCImageFile> GetImageModList()
		{
			return (List<XCom.Interfaces.IXCImageFile>)mySpace["ImageMods"];
		}

		public Dictionary<string, XCom.Palette> GetPaletteTable()
		{
			return (Dictionary<string, XCom.Palette>)mySpace["Palettes"];
		}
	}
}
