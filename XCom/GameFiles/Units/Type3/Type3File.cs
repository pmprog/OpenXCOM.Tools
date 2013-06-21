using System;
using XCom.Interfaces;

namespace XCom
{
	public class Type3File:IUnitFile
	{
		private int[] images;

		public Type3File(Type3Descriptor desc):base(desc)
		{
			images = new int[]{0,1,2,3,4,5,6,7};

			deathImages = new int[]{8,9,10};
		}

		public override int[] DrawIndexes(Direction dir, WeaponDescriptor weapon)
		{
			return new int[]{images[(int)dir]};
		}
	}
}
