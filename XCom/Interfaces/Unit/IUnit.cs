/*using System;
using System.Drawing;

namespace XCom.Interfaces
{
	public delegate void UnitDiedDelegate(IUnit deadUnit);

	public abstract class IUnit
	{
		public abstract event UnitDiedDelegate UnitDied;

		protected int direction=4,step=0;
		protected bool crouch=false,moving=false,dying=false,flying=false,male=true,underwater=true,zombie=false,big=false;
		protected ItemDescriptor weapon;
		protected PckImage deathFrame;
		protected int dieFrame=-1;
		protected string name;
		protected IUnitFile file;
		protected Palette pal;
		private int imgIdx;

		public IUnit(IUnitFile file,Palette p)
		{	
			this.file=file;
			pal=p;
		}

		public ItemDescriptor Weapon
		{
			get{return weapon;}
			set{weapon=value;}
		}

		public int ImageIndex
		{
			get{return imgIdx;}
			set{imgIdx=value;}
		}

		public Palette Palette
		{
			get{return pal;}
		}

		public string Name
		{
			get{return name;}
			set{name=value;}
		}

		public bool Big
		{
			get{return big;}
		}			

		public bool Flying
		{
			get{return flying;}
			set{flying=value;}
		}

		public bool Zombie
		{
			get{return zombie;}
			set{zombie=value;}
		}

		public bool Underwater
		{
			get{return underwater;}
			set{underwater=value;}
		}

		public bool Male
		{
			get{return male;}
			set{male=value;}
		}

		public void StopMoving()
		{
			moving=false;
		}

		public void TurnLeft()
		{
			if(!dying)
			{
				if(direction!=0)
					direction--;
				else
					direction=7;
			}
		}

		public int Direction
		{
			get{return direction;}
			set
			{
				if(!dying)
					direction=value;
			}
		}

		public void TurnRight()
		{
			if(!dying)
				direction=(direction+1)%8;
		}

//		public void Arm(ItemDescriptor wd)
//		{
//			weapon=wd;
//		}

		public bool Crouching
		{
			get{return crouch;}
			set{crouch=value;}
		}

		public void StepOne()
		{
			step=(step+1)%8;
		}

		public IUnitFile UnitFile
		{
			get{return file;}
		}

		public virtual void Die(){}
		public virtual int[] DrawFrames(){return new int[]{};}

#if DIRECTX
		public virtual void Draw(Microsoft.DirectX.DirectDraw.Surface target, int x, int y,int width,int height){}
#endif

#if WORKING
#if DIRECTX
		public abstract void DrawFast(Surface target,int x, int y,int width,int height);
#else
		public abstract void Draw(Graphics g, int x, int y);

		public abstract void Draw(Bitmap b, int x, int y);
#endif
#endif
	}
}
*/