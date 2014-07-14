using System;
using System.Collections.Generic;
using System.IO;
using XCom.Interfaces.Base;

namespace XCom
{
	public class XCMapDesc:IMapDesc
	{
		protected Palette myPal;
		protected bool isStatic;
		protected string[] dependencies;
		protected string basename, basePath, rmpPath, blankPath;
		//protected string tileset;

		public XCMapDesc(
			string basename,
			string basePath,
			string blankPath,
			//string tileset,
			string rmpPath,
			string[] dependencies,
			Palette myPal):base(basename)
		{
			this.myPal = myPal;
			this.basename = basename;
			this.basePath = basePath;
			this.rmpPath = rmpPath;
			this.blankPath = blankPath;
			//this.tileset = tileset;
			this.dependencies = dependencies;
			isStatic = false;
		}

		public override IMap_Base GetMapFile() 
		{
            var filePath = basePath + basename + ".MAP";
		    if (!File.Exists(filePath)) return null;
			ImageInfo images = GameInfo.ImageInfo;

			List<ITile> a = new List<ITile>();
			//if (p == null)
			//    p = GameInfo.DefaultPalette;

			foreach (string s in dependencies)
			{
				if (images[s] != null)
				{
					McdFile mcd = images[s].GetMcdFile(myPal);
					foreach (XCTile t in mcd)
						a.Add(t);
				}
			}

            XCMapFile map = new XCMapFile(basename, basePath, blankPath, a, dependencies);
			map.Rmp = new RmpFile(basename, rmpPath);
			return map;
		}
		public string[] Dependencies { get { return dependencies; } set { dependencies = value; } }
		public bool IsStatic { get { return isStatic; } set { isStatic = value; } }
		public int CompareTo(object other)
		{
			if (other is XCMapDesc)
			{
				return basename.CompareTo(((XCMapDesc)other).basename);
			}
			return 1;
		}
	}
}
