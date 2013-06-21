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
	public class Type4Descriptor:IUnitDescriptor
	{
		protected bool zombie=true,fly=false;

		public Type4Descriptor(string name, StreamReader sr,VarCollection vars)
			:base(name,sr,vars)
		{
			fly=false;
		}

		protected override void ParseLine(string keyword, string rest, StreamReader sr, VarCollection vars)
		{
			switch(keyword)
			{
				case "nozombie":
					zombie=false;
					break;
			}
		}

		public bool ZombieAllowed
		{
			get{return zombie;}
		}

		public bool FlyAllowed
		{
			get{return fly;}
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
			
			return new HumanSoldier1((Type4File)myFile,p);
		}
	}
}
