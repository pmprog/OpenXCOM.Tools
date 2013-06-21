using System;
using System.Drawing;
using XCom.Interfaces;

namespace XCom
{
	/// <summary>
	/// Summary description for Soldier.
	/// </summary>
	public class Type1Unit:IUnit
	{
		private Type1File images;
		private double lastTime;
		private int refresh=200,curr=0;
		private int[] bob={0,1,2,1,0,-1,-2,-1};

		public override event UnitDiedDelegate UnitDied;

		public Type1Unit(Type1File file,Palette p):base(file,p)
		{
			images=file;

			//curr = DXTimer.RandInt()%bob.Length;
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
				curr=(curr+1)%bob.Length;
				lastTime=currentTime;
			}

			if(!dying)
				images.DrawFast(target,x,y+bob[curr],width,height,moving,(Direction)direction);
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
