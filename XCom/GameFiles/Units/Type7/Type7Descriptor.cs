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
	public class Type7Descriptor:IUnitDescriptor
	{
		private Hashtable weapons = new Hashtable(5);
		private int numFrames=4,start=0;

		public Type7Descriptor(string name, StreamReader sr,VarCollection vars)
			:base(name,sr,vars){}

		protected override void ParseLine(string keyword, string rest, StreamReader sr, VarCollection vars)
		{
			switch(keyword)
			{
				case "weapon":
					string[]dat = rest.Split(',');
					weapons[dat[0]]=int.Parse(dat[1]);
					break;
				case "start":
					start=int.Parse(rest);
					break;
				case "frames":
					numFrames=int.Parse(rest);
					break;
				default:
					Console.WriteLine("Unknown line in unit description "+this.Name);
					break;
			}
		}

		public Hashtable WeaponHash
		{
			get{return weapons;}
		}

		public int NumFrames
		{
			get{return numFrames;}
		}

		public int StartIdx
		{
			get{return start;}
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
				myFile = new Type7File(this);
				myFile.ImageFile=myPck;
			}
            
			return new Type7Unit((Type7File)myFile,p);
		}
	}
}
