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
	public class Type1Descriptor:IUnitDescriptor
	{
		private Hashtable unitHash = new Hashtable(3);

		public Type1Descriptor(string name, StreamReader sr,VarCollection vars)
			:base(name,sr,vars){}

		protected override void ParseLine(string keyword, string line, StreamReader sr, VarCollection vars)
		{}
		
		public override IUnit GetNewUnit(Palette p)
		{
			//if(myFile==null)
			//	myFile = new Type1File(this);

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
				myFile = new Type1File(this);
				myFile.ImageFile=myPck;
			}
			
			return new Type1Unit((Type1File)myFile,p);
		}
	}
}
