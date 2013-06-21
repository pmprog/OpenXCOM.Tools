using System;
using XCom.Interfaces;

namespace XCom
{
	public class Type4File:IUnitFile
	{
		private int[] underwaterDeath;
		private readonly int[] bounceFactor = new int[]{2,1,0,1,2,1,0,1};
		private bool zombie,flyable;

		private BodyPart torso,legs,leftArm,rightArm,rightArmed1,rightArmed2,leftArmed2,crouchLegs,femHead,maleHead,zombieHead;
		private BodyPart flyHead,flyLegs;

		#region offsets
//		public const int StandingOffset=0;
//		public const int Armed2HandOffset=8;
//		public const int Armed1HandOffset=16;
//		public const int WalkingUnarmedOffset=24;
//		public const int WalkingArmed2HandOffset=88;
//		public const int WalkingArmed1HandOffset=152;
//		public const int CrouchingOffset=216;
//		public const int CrouchingArmed2HandOffset=224;
//		public const int CrouchingArmed1HandOffset=232;
//
//		public const int AboveWaterDeathOffset=0;
//		public const int UnderwaterDeathOffset=3;
//
//		public const int FemaleHead = 8;
//		public const int MaleHead = 16;
//		public const int ZombieHead = 24;
//
//		public const int CrouchOffset=5;
		#endregion

		public Type4File(Type4Descriptor desc):this(desc,true,true){}

		public Type4File(Type4Descriptor desc,bool useZombie,bool useFlyable):base(desc)
		{
			if(desc!=null)
			{
				zombie=desc.ZombieAllowed;
				flyable=desc.FlyAllowed;
			}
			else
			{
				zombie=useZombie;
				flyable=useFlyable;
			}
			
			torso = new BodyPart(new int[]{32,33,34,35,36,37,38,39});

			int i=16;
			legs = new BodyPart(new int[]{i,i+1,i+2,i+3,i+4,i+5,i+6,i+7},
				new int[,]{{i+40,i+41,i+42,i+43,i+44,i+45,i+46,i+47},
								{i+64,i+65,i+66,i+67,i+68,i+69,i+70,i+71},
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
			crouchLegs= new BodyPart(new int[]{24,25,26,27,28,29,30,31});
			femHead= new BodyPart(new int[]{262,263,264,265,266,267,268,269});
			maleHead= new BodyPart(new int[]{270,271,272,273,274,275,276,277});
			
			if(zombie)
				zombieHead= new BodyPart(new int[]{278,279,280,281,282,283,284,285});

			if(flyable)
			{
				//Console.WriteLine("Init flying");
				flyHead = new BodyPart(new int[]{286,287,288,289,290,291,292,293});
				flyLegs = new BodyPart(new int[]{294,295,296,297,298,299,300,301});
			}


			underwaterDeath = new int[]{259,260,261};
			deathImages = new int[]{256,257,258};
		}

		public int[] DrawIndexes(Direction dir, WeaponDescriptor weapon, bool flying, bool underwater, bool isZombie,bool male,bool crouch)
		{
			BodyPart p1,p2,p3,p4;

			BodyPart head=torso;
			BodyPart legs=this.legs;

			if(flyable)
			{
				head = flyHead;
				if(flying)
					legs=flyLegs;
			}
			else if(underwater)
			{
				if(isZombie)
				{
					if(zombie)
						head=zombieHead;
				}
				else
					head=torso;
			}
			else
			{
				if(isZombie)
				{
					if(zombie)
						head=zombieHead;
				}
				else if(male)
					head=maleHead;
				else
					head=femHead;
			}

			if(crouch)
			{
				legs=crouchLegs;
				//offset=5;
			}

			p2=legs;
			p3=head;

			if(weapon==null)
			{
				if((int)dir<4)
				{						
					p1=leftArm;//.Stationary[dir].DrawFast(target,x,y+offset,width,height);									
					p4=rightArm;//.Stationary[dir].DrawFast(target,x,y+offset,width,height);
				}
				else
				{
					p1=rightArm;//.Stationary[dir].DrawFast(target,x,y+offset,width,height);
					p4=leftArm;//.Stationary[dir].DrawFast(target,x,y+offset,width,height);
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
						p1=leftArm;//.Stationary[dir].DrawFast(target,x,y+offset,width,height);					
						p4=rightArmed1;//.Stationary[dir].DrawFast(target,x,y+offset,width,height);
					}
					else
					{
						p1=rightArmed1;//.Stationary[dir].DrawFast(target,x,y+offset,width,height);					
						p4=leftArm;//.Stationary[dir].DrawFast(target,x,y+offset,width,height);
					}				
				}
				else
				{
					if((int)dir<4)
					{						
						p1=leftArmed2;//.Stationary[dir].DrawFast(target,x,y+offset,width,height);					
						p4=rightArmed2;//.Stationary[dir].DrawFast(target,x,y+offset,width,height);
					}
					else
					{
						p1=rightArmed2;//.Stationary[dir].DrawFast(target,x,y+offset,width,height);					
						p4=leftArmed2;//.Stationary[dir].DrawFast(target,x,y+offset,width,height);
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

		public override int[] DrawIndexes(Direction dir, WeaponDescriptor weapon)
		{
			return DrawIndexes(dir,weapon,false,true,false,true,false);
		}
#if WORKING
#if DIRECTX
		public void DrawFast(Surface target,int x, int y,int width,int height,bool underwater,bool male,bool isZombie,bool flying,bool crouch,WeaponDescriptor weapon,Direction dir)
		{
			BodyPart head=torso;
			BodyPart legs=this.legs;
			int offset=0;
			if(flyable)
			{
				head = flyHead;
				if(flying)
					legs=flyLegs;
			}
			else if(underwater)
			{
				if(isZombie)
				{
					if(zombie)
						head=zombieHead;
				}
				else
					head=torso;
			}
			else
			{
				if(isZombie)
				{
					if(zombie)
						head=zombieHead;
				}
				else if(male)
					head=maleHead;
				else
					head=femHead;
			}

			if(crouch)
			{
				legs=crouchLegs;
				offset=5;
			}




			if(weapon==null)
			{
				if((int)dir<4)
				{						
					leftArm.Stationary[dir].DrawFast(target,x,y+offset,width,height);					
					legs.Stationary[dir].DrawFast(target,x,y,width,height);					
					head.Stationary[dir].DrawFast(target,x,y+offset,width,height);					
					rightArm.Stationary[dir].DrawFast(target,x,y+offset,width,height);
				}
				else
				{
					rightArm.Stationary[dir].DrawFast(target,x,y+offset,width,height);
					legs.Stationary[dir].DrawFast(target,x,y,width,height);
					head.Stationary[dir].DrawFast(target,x,y+offset,width,height);
					leftArm.Stationary[dir].DrawFast(target,x,y+offset,width,height);
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
						leftArm.Stationary[dir].DrawFast(target,x,y+offset,width,height);
						legs.Stationary[dir].DrawFast(target,x,y,width,height);
						head.Stationary[dir].DrawFast(target,x,y+offset,width,height);						
						rightArmed1.Stationary[dir].DrawFast(target,x,y+offset,width,height);
					}
					else
					{
						rightArmed1.Stationary[dir].DrawFast(target,x,y+offset,width,height);
						legs.Stationary[dir].DrawFast(target,x,y,width,height);
						head.Stationary[dir].DrawFast(target,x,y+offset,width,height);						
						leftArm.Stationary[dir].DrawFast(target,x,y+offset,width,height);
					}				
				}
				else
				{
					if((int)dir<4)
					{						
						leftArmed2.Stationary[dir].DrawFast(target,x,y+offset,width,height);
						legs.Stationary[dir].DrawFast(target,x,y,width,height);
						head.Stationary[dir].DrawFast(target,x,y+offset,width,height);						
						rightArmed2.Stationary[dir].DrawFast(target,x,y+offset,width,height);
					}
					else
					{
						rightArmed2.Stationary[dir].DrawFast(target,x,y+offset,width,height);
						legs.Stationary[dir].DrawFast(target,x,y,width,height);
						head.Stationary[dir].DrawFast(target,x,y+offset,width,height);						
						leftArmed2.Stationary[dir].DrawFast(target,x,y+offset,width,height);
					}
				}

				if((int)dir>=1 && (int)dir<6)
					weapon[dir].DrawFast(target,x,y+offset,width,height);
			}
		}
#else
		public void Draw(Graphics g,int x,int y,bool underwater,bool male,bool isZombie,bool crouch,bool flying,Direction dir,ItemDescriptor weapon)
		{
			BodyPart head=torso;
			BodyPart legs=this.legs;
			int offset=0;
			if(flyable)
			{
				head = flyHead;
				if(flying)
					legs=flyLegs;
			}
			else if(underwater)
			{
				if(isZombie)
				{
					if(zombie)
						head=zombieHead;
				}
				else
					head=torso;
			}
			else
			{
				if(isZombie)
				{
					if(zombie)
						head=zombieHead;
				}
				else if(male)
					head=maleHead;
				else
					head=femHead;
			}

			if(crouch)
			{
				legs=crouchLegs;
				offset=(int)(5*PckImage.Scale);
			}

			if(weapon==null)
			{				
				BodyPart armLeft=rightArm;
				BodyPart armRight=leftArm;

				if((int)dir<4)
				{
					armLeft=leftArm;
					armRight=rightArm;
				}

				armLeft.Stationary[dir].Draw(g,x,y+offset);					
				legs.Stationary[dir].Draw(g,x,y);					
				head.Stationary[dir].Draw(g,x,y+offset);					
				armRight.Stationary[dir].Draw(g,x,y+offset);

//				if((int)dir<4)
//				{						
//					leftArm.Stationary[dir].Draw(g,x,y+offset);					
//					legs.Stationary[dir].Draw(g,x,y);					
//					head.Stationary[dir].Draw(g,x,y+offset);					
//					rightArm.Stationary[dir].Draw(g,x,y+offset);
//				}
//				else
//				{
//					rightArm.Stationary[dir].Draw(g,x,y+offset);
//					legs.Stationary[dir].Draw(g,x,y);
//					head.Stationary[dir].Draw(g,x,y+offset);
//					leftArm.Stationary[dir].Draw(g,x,y+offset);
//				}
			}
			else 
			{	
				if((int)dir>=6 || (int)dir==0)
					handFile[weapon.HandIndex+(int)dir].Draw(g,x,y+offset);

				BodyPart armLeft=leftArmed2;
				BodyPart armRight=rightArmed2;

				//switch arms if one handed
				if(weapon.NumHands==1)
				{
					armLeft=leftArm;
					armRight=rightArmed1;
				}

				//switch arms if direction
				if((int)dir<4)
				{
					BodyPart tmp = armLeft;
					armLeft=armRight;
					armRight=tmp;
				}

				armRight.Stationary[dir].Draw(g,x,y+offset);
				legs.Stationary[dir].Draw(g,x,y);
				head.Stationary[dir].Draw(g,x,y+offset);						
				armLeft.Stationary[dir].Draw(g,x,y+offset);
/*
				if(weapon.OneHanded)
				{
					if((int)dir<4)
					{						
						leftArm.Stationary[dir].Draw(g,x,y+offset);
						legs.Stationary[dir].Draw(g,x,y);
						head.Stationary[dir].Draw(g,x,y+offset);						
						rightArmed1.Stationary[dir].Draw(g,x,y+offset);
					}
					else
					{
						rightArmed1.Stationary[dir].Draw(g,x,y+offset);
						legs.Stationary[dir].Draw(g,x,y);
						head.Stationary[dir].Draw(g,x,y+offset);						
						leftArm.Stationary[dir].Draw(g,x,y+offset);
					}				
				}
				else
				{

					if((int)dir<4)
					{						
						leftArmed2.Stationary[dir].Draw(g,x,y+offset);
						legs.Stationary[dir].Draw(g,x,y);
						head.Stationary[dir].Draw(g,x,y+offset);						
						rightArmed2.Stationary[dir].Draw(g,x,y+offset);
					}
					else
					{
						rightArmed2.Stationary[dir].Draw(g,x,y+offset);
						legs.Stationary[dir].Draw(g,x,y);
						head.Stationary[dir].Draw(g,x,y+offset);						
						leftArmed2.Stationary[dir].Draw(g,x,y+offset);
					}
				}
				*/

				if((int)dir>=1 && (int)dir<6)
					handFile[weapon.HandIndex+(int)dir].Draw(g,x,y+offset);
			}
		}

		public void Draw(Bitmap b,int x,int y,bool underwater,bool male,bool isZombie,bool crouch,bool flying,Direction dir,ItemDescriptor weapon)
		{
			BodyPart head=torso;
			BodyPart legs=this.legs;
			int offset=0;
			if(flyable)
			{
				head = flyHead;
				if(flying)
					legs=flyLegs;
			}
			else if(underwater)
			{
				if(isZombie)
				{
					if(zombie)
						head=zombieHead;
				}
				else
					head=torso;
			}
			else
			{
				if(isZombie)
				{
					if(zombie)
						head=zombieHead;
				}
				else if(male)
					head=maleHead;
				else
					head=femHead;
			}

			if(crouch)
			{
				legs=crouchLegs;
				offset=(int)(5*PckImage.Scale);
			}

			if(weapon==null)
			{				
				BodyPart armLeft=rightArm;
				BodyPart armRight=leftArm;

				if((int)dir<4)
				{
					armLeft=leftArm;
					armRight=rightArm;
				}

				Bmp.Draw(armLeft.Stationary[dir].Image,b,x,y+offset);					
				Bmp.Draw(legs.Stationary[dir].Image,b,x,y);				
				Bmp.Draw(head.Stationary[dir].Image,b,x,y+offset);			
				Bmp.Draw(armRight.Stationary[dir].Image,b,x,y+offset);
			}
			else 
			{	
				if((int)dir>=6 || (int)dir==0)
					Bmp.Draw(handFile[weapon.HandIndex+(int)dir].Image,b,x,y+offset);

				BodyPart armLeft=leftArmed2;
				BodyPart armRight=rightArmed2;

				//switch arms if one handed
				if(weapon.NumHands==1)
				{
					armLeft=leftArm;
					armRight=rightArmed1;
				}

				//switch arms if direction
				if((int)dir<4)
				{
					BodyPart tmp = armLeft;
					armLeft=armRight;
					armRight=tmp;
				}

				Bmp.Draw(armRight.Stationary[dir].Image,b,x,y+offset);
				Bmp.Draw(legs.Stationary[dir].Image,b,x,y);
				Bmp.Draw(head.Stationary[dir].Image,b,x,y+offset);						
				Bmp.Draw(armLeft.Stationary[dir].Image,b,x,y+offset);

				if((int)dir>=1 && (int)dir<6)
					Bmp.Draw(handFile[weapon.HandIndex+(int)dir].Image,b,x,y+offset);
			}
		}
#endif
#endif
		public int[] UnderwaterDeath
		{
			get{return underwaterDeath;}
		}
	}
}
