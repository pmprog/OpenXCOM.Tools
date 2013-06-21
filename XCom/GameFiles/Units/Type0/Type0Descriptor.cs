using System;
using System.Collections;
using System.IO;
//using SDLDotNet;
using XCom.Interfaces;

namespace XCom
{
	/// <summary>
	/// Default unit type 0
	/// </summary>
	public class Type0Descriptor:IUnitDescriptor
	{
		private Hashtable unitHash = new Hashtable(3);
		private int offset;

		public Type0Descriptor(string name, StreamReader sr,VarCollection vars)
			:base(name,sr,vars){}

		protected override void ParseLine(string keyword, string rest,StreamReader sr, VarCollection vars)
		{
			switch(keyword)
			{
				case "yoffset":
					offset = int.Parse(rest);
					break;
			}
		}

		public int YOffset
		{
			get{return offset;}
		}

		public override IUnit GetNewUnit(Palette p)
		{
			if(myFile==null)
			{
				PckFile myPck;
				try
				{
					myPck = GameInfo.CachePck(basePath,basename,4,p);
				}
				catch
				{
					myPck = GameInfo.CachePck(basePath,basename,2,p);
				}
				myFile = new Type0File(this,myPck.Count);
				myFile.ImageFile=myPck;
			}
			
			AlienSoldier a = new AlienSoldier((Type0File)myFile,p);
			a.Name = basename;
			return a;
		}
	}
}
