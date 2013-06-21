using System;
using System.Collections;
using XCom.Interfaces;

namespace XCom
{
	public class Type7File:IUnitFile
	{
		private int numFrames;
		private MovingPartDirection north,south,east,west;

		public Type7File(Type7Descriptor desc):this(desc,0,4){}

		public Type7File(Type7Descriptor desc,int start,int frames):base(desc)
		{
			numFrames=frames;

			if(desc!=null)
			{
				this.numFrames=desc.NumFrames;
				start = desc.StartIdx;
			}

			int[,] curr = new int[8,numFrames];

//			Console.WriteLine("North:");

			int x=0;
			for(int pos=0;pos<8;pos++)
				for(int fram=0;fram<numFrames;fram++)
				{
					curr[pos,fram]=start+(pos*numFrames*4)+x+fram;
					//Console.Write((start+(pos*numFrames*4)+x+fram)+" ");
				}

			north = new MovingPartDirection(curr);
			curr = new int[8,numFrames];

//			Console.WriteLine("\nEast:");
			x=numFrames;
			for(int pos=0;pos<8;pos++)
				for(int fram=0;fram<numFrames;fram++)
				{
					curr[pos,fram]=start+(pos*numFrames*4)+x+fram;
//					Console.Write((start+(pos*numFrames*4)+x+fram)+" ");
				}

			east = new MovingPartDirection(curr);
			curr = new int[8,numFrames];

//			Console.WriteLine("\nWest:");
			x=numFrames*2;
			for(int pos=0;pos<8;pos++)
				for(int fram=0;fram<numFrames;fram++)
				{
					curr[pos,fram]=start+(pos*numFrames*4)+x+fram;
//					Console.Write((start+(pos*numFrames*4)+x+fram)+" ");
				}

			west = new MovingPartDirection(curr);
			curr = new int[8,numFrames];

//			Console.WriteLine("\nSouth:");
			x=numFrames*3;
			for(int pos=0;pos<8;pos++)
				for(int fram=0;fram<numFrames;fram++)
				{
					curr[pos,fram]=start+(pos*numFrames*4)+x+fram;
//					Console.Write((start+(pos*numFrames*4)+x+fram)+" ");
				}

			south = new MovingPartDirection(curr);			
		}

		public override int[] DrawIndexes(Direction dir, WeaponDescriptor weapon)
		{
			return new int[]{
								north[dir,0],
								south[dir,0],
								east[dir,0],
								west[dir,0]
							};			
		}

		public int[] DrawIndexes(Direction dir, WeaponDescriptor weapon,int frame)
		{
			return new int[]{
								north[dir,frame],
								south[dir,frame],
								east[dir,frame],
								west[dir,frame]
							};			
		}
#if WORKING
#if DIRECTX
		public void DrawFast(Surface target,int x, int y,int width,int height,int frame,Direction dir)
		{		
			north[dir,frame].DrawFast(target,x,y-PckImage.Width/2,width,height);
			south[dir,frame].DrawFast(target,x,y,width,height);
			east[dir,frame].DrawFast(target,x+PckImage.Width/2,y-PckImage.Width/4,width,height);
			west[dir,frame].DrawFast(target,x-PckImage.Width/2,y-PckImage.Width/4,width,height);		
		}
#else
		public void Draw(Graphics g, int x, int y, int frame,Direction dir)
		{
			north[dir,frame].Draw(g,x,y-(int)((PckImage.Width*PckImage.Scale)/2));
			south[dir,frame].Draw(g,x,y);
			east[dir,frame].Draw(g,x+(int)((PckImage.Width*PckImage.Scale)/2),y-(int)((PckImage.Width*PckImage.Scale)/4));
			west[dir,frame].Draw(g,x-(int)((PckImage.Width*PckImage.Scale)/2),y-(int)((PckImage.Width*PckImage.Scale)/4));	
		}

		public void Draw(Bitmap b, int x, int y, int frame,Direction dir)
		{
			Bmp.Draw(north[dir,frame].Image,b,x,y-(int)((PckImage.Width*PckImage.Scale)/2));
			Bmp.Draw(south[dir,frame].Image,b,x,y);
			Bmp.Draw(east[dir,frame].Image,b,x+(int)((PckImage.Width*PckImage.Scale)/2),y-(int)((PckImage.Width*PckImage.Scale)/4));
			Bmp.Draw(west[dir,frame].Image,b,x-(int)((PckImage.Width*PckImage.Scale)/2),y-(int)((PckImage.Width*PckImage.Scale)/4));	
		}
#endif
#endif
//		public int NumFrames
//		{
//			get{return numFrames;}
//		}

//		public int[] GroundImage
//		{
//			get{return ground;}
//		}
	}
}
