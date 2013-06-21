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
	public class Type2Descriptor:IUnitDescriptor
	{
		private int start;
		private int numDeath;

		public Type2Descriptor(string name, StreamReader sr,VarCollection vars)
			:base(name,sr,vars){}

		protected override void ParseLine(string keyword,string rest, StreamReader sr, VarCollection vars)
		{
			switch(keyword)
			{
				case "start":
					start = int.Parse(rest);
					break;
				case "numdeath":
					numDeath=int.Parse(rest);
					break;
			}
		}

		public int Start
		{
			get{return start;}
		}

		public int NumDeathFrames
		{
			get{return numDeath;}
		}

		public override IUnit GetNewUnit(Palette p)
		{
			//if(myFile==null)
			//	myFile = new Type2File(this);

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
				myFile = new Type2File(this);
				myFile.ImageFile=myPck;
			}

			return new Type2Unit((Type2File)myFile,p);
		}
	}
}
