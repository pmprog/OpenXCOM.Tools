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
	public class Type6Descriptor:IUnitDescriptor
	{
		public Type6Descriptor(string name, StreamReader sr,VarCollection vars)
			:base(name,sr,vars){}

		protected override void ParseLine(string keyword, string rest, StreamReader sr, VarCollection vars)
		{

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
				myFile = new Type6File(this);
				myFile.ImageFile=myPck;
			}
			
			return new Type6Unit((Type6File)myFile,p);
		}
	}
}
