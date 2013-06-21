using System;
using System.Drawing;
using XCom.Interfaces;

namespace XCom
{
	public class HumanSoldier2:IUnit
	{
		private Type4File images;
//		private bool zombie=false,underwater=true,male=true;

		public override event UnitDiedDelegate UnitDied;

		public HumanSoldier2(Type4File file,Palette p):base(file,p)
		{
			images=file;
		}

//		public bool Zombie
//		{
//			get{return zombie;}
//			set{zombie=value;}
//		}
//
//		public bool Underwater
//		{
//			get{return underwater;}
//			set{underwater=value;}
//		}
//
//		public bool Male
//		{
//			get{return male;}
//			set{male=value;}
//		}

//		public bool Flying
//		{
//			get{return flying;}
//			set{flying=value;}
//		}

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
			return images.DrawIndexes((Direction)direction,(WeaponDescriptor)weapon,flying,underwater,zombie,male,crouch);
		}
#if WORKING
#if DIRECTX
		public override void DrawFast(Surface target,int x, int y,int width,int height)
		{
			if(!dying)
				images.DrawFast(target,x,y,width,height,underwater,male,zombie,flying,crouch,weapon,(Direction)direction);
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
