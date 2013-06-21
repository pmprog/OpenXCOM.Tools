using System;
using XCom.Interfaces;

namespace XCom
{
	public class Type2File:IUnitFile
	{
		private BodyPart body;

		public Type2File(Type2Descriptor desc):this(desc,0,3){}

		public Type2File(Type2Descriptor desc,int start, int numDeath):base(desc)
		{
			if(desc!=null)
			{
				start = desc.Start;
				numDeath = desc.NumDeathFrames;
			}

			if(start==0)
				body = new BodyPart(new int[]{start+67,start+66,start+65,start+64,start+71,start+70,start+69,start+68},
					new int[,]{{start,start+1,start+2,start+3,start+4,start+5,start+6,start+7},
									{start+8,start+9,start+10,start+11,start+12,start+13,start+14,start+15},
									{start+16,start+17,start+18,start+19,start+20,start+21,start+22,start+23},
									{start+24,start+25,start+26,start+27,start+28,start+29,start+30,start+31},
									{start+32,start+33,start+34,start+35,start+36,start+37,start+38,start+39},
									{start+40,start+41,start+42,start+43,start+44,start+45,start+46,start+47},
									{start+48,start+49,start+50,start+51,start+52,start+53,start+54,start+55},
									{start+56,start+57,start+58,start+59,start+60,start+61,start+62,start+63}});
			else
				body = new BodyPart(new int[]{start+64,start+65,start+66,start+67,start+68,start+69,start+70,start+71},
					new int[,]{{start,start+1,start+2,start+3,start+4,start+5,start+6,start+7},
									{start+8,start+9,start+10,start+11,start+12,start+13,start+14,start+15},
									{start+16,start+17,start+18,start+19,start+20,start+21,start+22,start+23},
									{start+24,start+25,start+26,start+27,start+28,start+29,start+30,start+31},
									{start+32,start+33,start+34,start+35,start+36,start+37,start+38,start+39},
									{start+40,start+41,start+42,start+43,start+44,start+45,start+46,start+47},
									{start+48,start+49,start+50,start+51,start+52,start+53,start+54,start+55},
									{start+56,start+57,start+58,start+59,start+60,start+61,start+62,start+63}});

			deathImages = new int[numDeath];
			for(int i=0;i<numDeath;i++)
				deathImages[i] = start+72+i;
			//deathImages = new PckImage[]{start+72,start+73,start+74,start+75]};
		}

		public override int[] DrawIndexes(Direction dir, WeaponDescriptor weapon)
		{
			return new int[]{body.Stationary[dir]};
		}
	}
}
