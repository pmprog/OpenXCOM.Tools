using System;
using XCom.Interfaces;

namespace XCom
{
	public class Type1File:IUnitFile
	{
		private PartDirection moving,notMoving;
		
		public Type1File(Type1Descriptor desc):base(desc)
		{
			notMoving = new PartDirection(new int[]{0,1,2,3,4,5,6,7});
			moving = new PartDirection(new int[]{8,9,10,11,12,13,14,15});
			deathImages = new int[]{16,17,18,19};
		}

		public override int[] DrawIndexes(Direction dir,WeaponDescriptor weapon)
		{
			return new int[]{notMoving[dir]};
		}		
	}
}
