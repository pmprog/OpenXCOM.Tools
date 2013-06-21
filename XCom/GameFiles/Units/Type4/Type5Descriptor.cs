using System;
using System.Collections;
using System.IO;
using XCom.Interfaces;

namespace XCom
{
	/// <summary>
	/// Default unit type 0
	/// </summary>
	public class Type5Descriptor:Type4Descriptor
	{
		public Type5Descriptor(string name, StreamReader sr,VarCollection vars)
			:base(name,sr,vars)
		{
			fly=true;	
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
				myFile = new Type4File(this);
				myFile.ImageFile=myPck;
			}
			
			return new HumanSoldier2((Type4File)myFile,p);
		}
	}
}
