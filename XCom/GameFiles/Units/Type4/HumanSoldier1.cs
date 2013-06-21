using System;
using System.Drawing;
using XCom.Interfaces;

namespace XCom
{
	public class HumanSoldier1:IUnit
	{
		private Type4File images;

		public override event UnitDiedDelegate UnitDied;
		#region offsets
/*
		private const int LeftArmStill = 0;
		private const int RightArmStill = 8;
		private const int LegsStillStand=16;
		private const int LegsStillCrouch=24;
		private const int UnderwaterHead = 32;
		private const int LeftArmMove = 40;
		private const int RightArmMove = 48;
		private const int LegsMove = 56;

		private const int LeftArmArmed2 = 232;
		private const int RightArmArmed2 = 240;
		private const int RightArmArmed1 = 248;

		private const int DeathFrames = 3;
		private const int DeathAboveWater = 256;
		private const int DeathUnderWater = 259;

		private const int FemaleHead = 262;
		private const int MaleHead = 270;
		private const int ZombieHead = 278;*/
		#endregion

		public HumanSoldier1(Type4File file,Palette p):base(file,p)
		{
			images=file;
		}

		public override void Die()
		{
			dying=true;
			if(underwater)
			{
				if(dieFrame<images.UnderwaterDeath.Length)
					dieFrame++;
				else
					if(UnitDied!=null)
					UnitDied(this);
			}
			else
			{
				if(dieFrame<images.DeathIndexes.Length)
					dieFrame++;
				else
					if(UnitDied!=null)
					UnitDied(this);
			}
		}

		public override int[] DrawFrames()
		{
			return images.DrawIndexes((Direction)direction,(WeaponDescriptor)weapon,false,underwater,zombie,male,crouch);
		}

#if WORKING
#if DIRECTX
		public override void DrawFast(Surface target,int x, int y,int width,int height)
		{
			if(!dying)
				images.DrawFast(target,x,y,width,height,underwater,male,zombie,false,crouch,weapon,(Direction)direction);
			else
			{
				if(underwater)
					images.UnderwaterDeath[dieFrame].DrawFast(target,x,y,width,height);
				else
					images.NormalDeath[dieFrame].DrawFast(target,x,y,width,height);
			}
		}
#else
		public override void Draw(Graphics g, int x, int y)
		{
			images.Draw(g,x,y,underwater,male,zombie,crouch,flying,(Direction)direction,weapon);
		}

		public override void Draw(Bitmap g, int x, int y)
		{
			images.Draw(g,x,y,underwater,male,zombie,crouch,flying,(Direction)direction,weapon);
		}
#endif
#endif
	}
}
