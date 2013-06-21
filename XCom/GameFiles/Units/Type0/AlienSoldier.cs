using System;
using System.Drawing;
using XCom.Interfaces;

namespace XCom
{
	public class AlienSoldier:IUnit
	{
		private Type0File images;
		public override event UnitDiedDelegate UnitDied;

		public AlienSoldier(Type0File file,Palette p):base(file,p)
		{
			images=file;
		}

		public override void Die()
		{
			dying=true;
			if(dieFrame<images.DeathIndexes.Length)
				dieFrame++;
			else
				if(UnitDied!=null)
				UnitDied(this);
		}

		public override int[] DrawFrames()
		{
			return images.DrawIndexes((Direction)direction,(WeaponDescriptor)weapon);
		}
	}
}
