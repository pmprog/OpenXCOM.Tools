using System;
using System.Drawing;
using XCom.Interfaces;

namespace XCom
{
	public class Type6Unit:IUnit
	{
		private Type6File images;
		private double lastTime;
		private int refresh=150,curr=0;

		public override event UnitDiedDelegate UnitDied;

		public Type6Unit(Type6File file,Palette p):base(file,p)
		{
			images=file;
			//curr = DXTimer.RandInt()%8;
			big=true;
		}

		public override void Die()
		{
//			dying=true;
//			if(dieFrame<images.Death.Length)
//				dieFrame++;
//			else
//				if(UnitDied!=null)
//				UnitDied(this);
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
			//else 
			//	images.Death[dieFrame].DrawFast(target,x,y,width,height);
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
