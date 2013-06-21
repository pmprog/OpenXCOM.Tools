using System;
using System.Drawing;
using XCom.Interfaces;

namespace XCom
{
	/// <summary>
	/// Summary description for Soldier.
	/// </summary>
	public class Type3Unit:IUnit
	{
		private Type3File images;

		public override event UnitDiedDelegate UnitDied;
		private double lastTime;
		private int refresh=200,curr=0;

		public Type3Unit(Type3File file,Palette p):base(file,p)
		{
			images=file;
			//curr = DXTimer.RandInt()%8;
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
			double currentTime = DXTimer.GetCurrMillis();
			if(lastTime+refresh<currentTime)
			{
				curr=(curr+1)%8;
				lastTime=currentTime;
			}

			if(!dying)
				images.DrawFast(target,x,y,width,height,curr);
			else if(!moving)
				images.Death[dieFrame].DrawFast(target,x,y,width,height);
		}
#else
		public override void Draw(Graphics g, int x, int y)
		{
			images.Draw(g,x,y,curr);
		}

		public override void Draw(Bitmap g, int x, int y)
		{
			images.Draw(g,x,y,curr);
		}
#endif
#endif
	}
}
