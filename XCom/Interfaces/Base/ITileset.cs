using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace XCom.Interfaces.Base
{
	public class ITileset
	{
		protected string name;
		protected Dictionary<string, IMapDesc> maps;

		// <tilest name>
		// | <map name> -> map data
		// | <map name> -> map data
		protected Dictionary<string, Dictionary<string, IMapDesc>> subsets;

		protected ITileset(string name)
		{
			this.name = name;
			maps = new Dictionary<string, IMapDesc>();
			subsets = new Dictionary<string, Dictionary<string, IMapDesc>>();
		}

		public ICollection MapList { get { return maps.Keys; } }
		public IMapDesc this[string mapName]
		{
			get { return maps[mapName]; }
			set
			{
				if (!maps.ContainsKey(mapName))
					maps.Add(mapName, value);
				maps[mapName] = value;
			}
		}
		public Dictionary<string, Dictionary<string, IMapDesc>> Subsets { get { return subsets; } }		
		public string Name { get { return name; } }
	}
}

