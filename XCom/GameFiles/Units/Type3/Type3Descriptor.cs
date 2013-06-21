using System;
using System.Collections;
using System.IO;
//using SDLDotNet;
using XCom.Interfaces;

namespace XCom
{
	public class Type3Descriptor:IUnitDescriptor
	{
		public Type3Descriptor(string name, StreamReader sr,VarCollection vars)
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
				myFile = new Type3File(this);
				myFile.ImageFile=myPck;
			}

			return new Type3Unit((Type3File)myFile,p);
		}
	}
}
