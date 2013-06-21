using System;
using XCom.Interfaces;

namespace XCom
{
	public class Type0File:IUnitFile
	{
		private int offset;
		private BodyPart torso,legs,leftArm,rightArm,rightArmed1,rightArmed2,leftArmed2;
		
		public Type0File(Type0Descriptor desc,int numImages):this(desc,numImages,-5){}

		public Type0File(Type0Descriptor desc,int numImages,int offset):base(desc)
		{
			if(desc!=null)
				this.offset=desc.YOffset;		
			else
				this.offset=offset;

			torso = new BodyPart(new int[]{32,33,34,35,36,37,38,39});

			int i=16;
			legs = new BodyPart(new int[]{i,i+1,i+2,i+3,i+4,i+5,i+6,i+7},
				new int[,]{{i+40,i+41,i+42,i+43,i+44,i+45,i+46,i+47},
								{i+64,i+65,i+66,i+67,i+68,i+69,i+70,i+7},
								{i+88,i+89,i+90,i+91,i+92,i+93,i+94,i+95},
								{i+112,i+113,i+114,i+115,i+116,i+117,i+118,i+119},
								{i+136,i+137,i+138,i+139,i+140,i+141,i+142,i+143},
								{i+160,i+161,i+162,i+163,i+164,i+165,i+166,i+167},
								{i+184,i+185,i+186,i+187,i+188,i+189,i+190,i+191},
								{i+208,i+209,i+210,i+211,i+212,i+213,i+214,i+215}});

			i=0;
			leftArm = new BodyPart(new int[]{i,i+1,i+2,i+3,i+4,i+5,i+6,i+7},
				new int[,]{{i+40,i+41,i+42,i+43,i+44,i+45,i+46,i+47},
								{i+64,i+65,i+66,i+67,i+68,i+69,i+70,i+71},
								{i+88,i+89,i+90,i+91,i+92,i+93,i+94,i+95},
								{i+112,i+113,i+114,i+115,i+116,i+117,i+118,i+119},
								{i+136,i+137,i+138,i+139,i+140,i+141,i+142,i+143},
								{i+160,i+161,i+162,i+163,i+164,i+165,i+166,i+167},
								{i+184,i+185,i+186,i+187,i+188,i+189,i+190,i+191},
								{i+208,i+209,i+210,i+211,i+212,i+213,i+214,i+215}});

			i=8;
			rightArm = new BodyPart(new int[]{i,i+1,i+2,i+3,i+4,i+5,i+6,i+7},
				new int[,]{{i+40,i+41,i+42,i+43,i+44,i+45,i+46,i+47},
								{i+64,i+65,i+66,i+67,i+68,i+69,i+70,i+71},
								{i+88,i+89,i+90,i+91,i+92,i+93,i+94,i+95},
								{i+112,i+113,i+114,i+115,i+116,i+117,i+118,i+119},
								{i+136,i+137,i+138,i+139,i+140,i+141,i+142,i+143},
								{i+160,i+161,i+162,i+163,i+164,i+165,i+166,i+167},
								{i+184,i+185,i+186,i+187,i+188,i+189,i+190,i+191},
								{i+208,i+209,i+210,i+211,i+212,i+213,i+214,i+215}});

			rightArmed1 = new BodyPart(new int[]{248,249,250,251,252,253,254,255});
			rightArmed2 = new BodyPart(new int[]{240,241,242,243,244,245,246,247});
			leftArmed2 = new BodyPart(new int[]{232,233,234,235,236,237,238,239});

			deathImages = new int[numImages-256];
			for(i=0;i<deathImages.Length;i++)
				deathImages[i] = 256+i; 
		}

		public override int[] DrawIndexes(Direction dir,WeaponDescriptor weapon)
		{
			BodyPart p1,p2,p3,p4;

			p2=torso;
			p3=legs;

			if(weapon==null)
			{
				if((int)dir<4)
				{				
					p1=leftArm;
					p4=rightArm;
				}
				else
				{
					p1=rightArm;
					p4=leftArm;
				}
			}
			else 
			{	
//				if((int)dir>=6 || (int)dir==0)
//					weapon[dir].DrawFast(target,x,y+offset,width,height);

				if(weapon.NumHands==1)
				{
					if((int)dir<4)
					{						
						p1=leftArm;
						p4=rightArmed1;
					}
					else
					{
						p1=rightArmed1;
						p4=leftArm;
					}				
				}
				else
				{
					if((int)dir<4)
					{						
						p1=leftArmed2;
						p4=rightArmed2;
					}
					else
					{
						p1=rightArmed2;
						p4=leftArmed2;
					}
				}

//				if((int)dir>=1 && (int)dir<6)
//					weapon[dir].DrawFast(target,x,y+offset,width,height);
			}
			return new int[]{	p1.Stationary[dir],
								p2.Stationary[dir],
								p3.Stationary[dir],
								p4.Stationary[dir]};
		}
#if WORKING
#if DIRECTX
		public void DrawFast(Surface target,int x, int y,int width,int height,Direction dir,WeaponDescriptor weapon)
		{
			if(weapon==null)
			{
				if((int)dir<4)
				{						
					leftArm.Stationary[dir].DrawFast(target,x,y,width,height);
					torso.Stationary[dir].DrawFast(target,x,y,width,height);
					legs.Stationary[dir].DrawFast(target,x,y,width,height);
					rightArm.Stationary[dir].DrawFast(target,x,y,width,height);
				}
				else
				{
					rightArm.Stationary[dir].DrawFast(target,x,y,width,height);
					torso.Stationary[dir].DrawFast(target,x,y,width,height);
					legs.Stationary[dir].DrawFast(target,x,y,width,height);
					leftArm.Stationary[dir].DrawFast(target,x,y,width,height);
				}
			}
			else 
			{	
				if((int)dir>=6 || (int)dir==0)
					weapon[dir].DrawFast(target,x,y+offset,width,height);

				if(weapon.OneHanded)
				{
					if((int)dir<4)
					{						
						leftArm.Stationary[dir].DrawFast(target,x,y,width,height);
						torso.Stationary[dir].DrawFast(target,x,y,width,height);
						legs.Stationary[dir].DrawFast(target,x,y,width,height);
						rightArmed1.Stationary[dir].DrawFast(target,x,y,width,height);
					}
					else
					{
						rightArmed1.Stationary[dir].DrawFast(target,x,y,width,height);
						torso.Stationary[dir].DrawFast(target,x,y,width,height);
						legs.Stationary[dir].DrawFast(target,x,y,width,height);
						leftArm.Stationary[dir].DrawFast(target,x,y,width,height);
					}				
				}
				else
				{
					if((int)dir<4)
					{						
						leftArmed2.Stationary[dir].DrawFast(target,x,y,width,height);
						torso.Stationary[dir].DrawFast(target,x,y,width,height);
						legs.Stationary[dir].DrawFast(target,x,y,width,height);
						rightArmed2.Stationary[dir].DrawFast(target,x,y,width,height);
					}
					else
					{
						rightArmed2.Stationary[dir].DrawFast(target,x,y,width,height);
						torso.Stationary[dir].DrawFast(target,x,y,width,height);
						legs.Stationary[dir].DrawFast(target,x,y,width,height);
						leftArmed2.Stationary[dir].DrawFast(target,x,y,width,height);
					}
				}

				if((int)dir>=1 && (int)dir<6)
					weapon[dir].DrawFast(target,x,y+offset,width,height);
			}
		}
#else
		public void Draw(Graphics g,int x, int y,Direction dir,ItemDescriptor weapon)
		{
			if(weapon==null)
			{
				if((int)dir<4)
				{						
					leftArm.Stationary[dir].Draw(g,x,y);
					torso.Stationary[dir].Draw(g,x,y);
					legs.Stationary[dir].Draw(g,x,y);
					rightArm.Stationary[dir].Draw(g,x,y);
				}
				else
				{
					rightArm.Stationary[dir].Draw(g,x,y);
					torso.Stationary[dir].Draw(g,x,y);
					legs.Stationary[dir].Draw(g,x,y);
					leftArm.Stationary[dir].Draw(g,x,y);
				}
			}
			else 
			{	
				if((int)dir>=6 || (int)dir==0)
					handFile[weapon.HandIndex+(int)dir].Draw(g,x,y+offset);

				if(weapon.NumHands==1)
				{
					if((int)dir<4)
					{						
						leftArm.Stationary[dir].Draw(g,x,y);
						torso.Stationary[dir].Draw(g,x,y);
						legs.Stationary[dir].Draw(g,x,y);
						rightArmed1.Stationary[dir].Draw(g,x,y);
					}
					else
					{
						rightArmed1.Stationary[dir].Draw(g,x,y);
						torso.Stationary[dir].Draw(g,x,y);
						legs.Stationary[dir].Draw(g,x,y);
						leftArm.Stationary[dir].Draw(g,x,y);
					}				
				}
				else
				{
					if((int)dir<4)
					{						
						leftArmed2.Stationary[dir].Draw(g,x,y);
						torso.Stationary[dir].Draw(g,x,y);
						legs.Stationary[dir].Draw(g,x,y);
						rightArmed2.Stationary[dir].Draw(g,x,y);
					}
					else
					{
						rightArmed2.Stationary[dir].Draw(g,x,y);
						torso.Stationary[dir].Draw(g,x,y);
						legs.Stationary[dir].Draw(g,x,y);
						leftArmed2.Stationary[dir].Draw(g,x,y);
					}
				}

				if((int)dir>=1 && (int)dir<6)
					handFile[weapon.HandIndex+(int)dir].Draw(g,x,y+offset);
			}
		}

		public void Draw(Bitmap b, int x, int y, Direction dir,ItemDescriptor weapon)
		{
			if(weapon==null)
			{
				if((int)dir<4)
				{	
					Bmp.Draw(leftArm.Stationary[dir].Image,b,x,y);
					Bmp.Draw(torso.Stationary[dir].Image,b,x,y);
					Bmp.Draw(legs.Stationary[dir].Image,b,x,y);
					Bmp.Draw(rightArm.Stationary[dir].Image,b,x,y);
				}
				else
				{
					Bmp.Draw(rightArm.Stationary[dir].Image,b,x,y);
					Bmp.Draw(torso.Stationary[dir].Image,b,x,y);
					Bmp.Draw(legs.Stationary[dir].Image,b,x,y);
					Bmp.Draw(leftArm.Stationary[dir].Image,b,x,y);
				}
			}
			else 
			{	
				if((int)dir>=6 || (int)dir==0)
					Bmp.Draw(handFile[weapon.HandIndex+(int)dir].Image,b,x,y+offset);

				if(weapon.NumHands==1)
				{
					if((int)dir<4)
					{						
						Bmp.Draw(leftArm.Stationary[dir].Image,b,x,y);
						Bmp.Draw(torso.Stationary[dir].Image,b,x,y);
						Bmp.Draw(legs.Stationary[dir].Image,b,x,y);
						Bmp.Draw(rightArmed1.Stationary[dir].Image,b,x,y);
					}
					else
					{
						Bmp.Draw(rightArmed1.Stationary[dir].Image,b,x,y);
						Bmp.Draw(torso.Stationary[dir].Image,b,x,y);
						Bmp.Draw(legs.Stationary[dir].Image,b,x,y);
						Bmp.Draw(leftArm.Stationary[dir].Image,b,x,y);
					}				
				}
				else
				{
					if((int)dir<4)
					{						
						Bmp.Draw(leftArmed2.Stationary[dir].Image,b,x,y);
						Bmp.Draw(torso.Stationary[dir].Image,b,x,y);
						Bmp.Draw(legs.Stationary[dir].Image,b,x,y);
						Bmp.Draw(rightArmed2.Stationary[dir].Image,b,x,y);
					}
					else
					{
						Bmp.Draw(rightArmed2.Stationary[dir].Image,b,x,y);
						Bmp.Draw(torso.Stationary[dir].Image,b,x,y);
						Bmp.Draw(legs.Stationary[dir].Image,b,x,y);
						Bmp.Draw(leftArmed2.Stationary[dir].Image,b,x,y);
					}
				}

				if((int)dir>=1 && (int)dir<6)
					Bmp.Draw(handFile[weapon.HandIndex+(int)dir].Image,b,x,y+offset);
			}
		}
#endif
#endif
	}
}
