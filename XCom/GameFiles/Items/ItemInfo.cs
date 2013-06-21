using System;
using System.Collections;
using System.IO;

namespace XCom.Interfaces
{
	public class ItemInfo
	{
		protected Hashtable items;
		protected string handImg,groundImg;

		public ItemInfo()
		{
			items = new Hashtable();
		}

		public PckFile HandImages
		{
			get{return GameInfo.CachePck(handImg,"",4,GameInfo.DefaultPalette);}
		}

		public PckFile GroundImages
		{
			get{return GameInfo.CachePck(groundImg,"",4,GameInfo.DefaultPalette);}
		}

		public Hashtable Items
		{
			get{return items;}
		}

		public ItemDescriptor this[string key]
		{
			get{return (ItemDescriptor)items[key];}
		}

		public ItemInfo(Stream file,VarCollection v):this()
		{
			StreamReader sr = new StreamReader(file);
			VarCollection vars = new VarCollection(sr,v);
			KeyVal line=null;

			while((line=vars.ReadLine())!=null)
			{
				switch(line.Keyword.ToLower())
				{
					case "ground":
						groundImg = line.Rest;
						break;
					case "hand":
						handImg = line.Rest;
						break;
					case "weapon":
						items[line.Rest] = new WeaponDescriptor(line.Rest,vars);
						break;
//					case "item":
//						break;
				}
			}

			sr.Close();
		}
	}
}
