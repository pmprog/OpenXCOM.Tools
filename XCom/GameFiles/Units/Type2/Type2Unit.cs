using System;
using System.Drawing;
using XCom.Interfaces;

namespace XCom
{
	/// <summary>
	/// Summary description for Soldier.
	/// </summary>
	public class Type2Unit:IUnit
	{
		private Type2File images;

		public override event UnitDiedDelegate UnitDied;

		public Type2Unit(Type2File file,Palette p):base(file,p)
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

#if WORKING
#if DIRECTX
		public override void DrawFast(Surface target,int x, int y,int width,int height)
		{
			if(!dying)
				images.DrawFast(target,x,y,width,height,(Direction)direction);
			else if(!moving)
				images.Death[dieFrame].DrawFast(target,x,y,width,height);
		}
#else
		public override void Draw(Graphics g, int x, int y)
		{
			images.Draw(g,x,y,(Direction)direction);
		}

		public override void Draw(Bitmap g, int x, int y)
		{
			images.Draw(g,x,y,(Direction)direction);
		}
#endif
#endif
	}
}
