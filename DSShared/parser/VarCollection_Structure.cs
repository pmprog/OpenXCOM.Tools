using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DSShared
{
	/// <summary>
	/// Class to automatically parse out a VC file into a tree structure
	/// </summary>
	public class VarCollection_Structure
	{
		private KeyVal root;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="sr">File to read</param>
		public VarCollection_Structure(StreamReader sr)
		{
			VarCollection vc = new VarCollection(sr);

			root = new KeyVal("parent", "parent");
			root.SubHash = new Dictionary<string, KeyVal>();
			
			parse_block(vc,root);
		}

		/// <summary>
		/// VC structure of the file
		/// </summary>
		public Dictionary<string,KeyVal> KeyValList
		{
			get { return root.SubHash; }
		}

		private void parse_block(VarCollection vc,KeyVal parent)
		{
			KeyVal kv;
			KeyVal lastKV=null;
			while (vc.ReadLine(out kv))
			{
				switch (kv.Keyword)
				{
					case "{":
						lastKV.SubHash = new Dictionary<string, KeyVal>();
						parse_block(vc,lastKV);
						break;
					case "}":
						return;
					default:
						parent.SubHash[kv.Keyword]=kv;
						lastKV = kv;
						break;
				}
			}
		}
	}
}
