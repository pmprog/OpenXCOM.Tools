using System;
using XCom.Interfaces;

namespace XCom
{
	public class Type6File:IUnitFile
	{
		private int[] north,south,east,west;

		public Type6File(Type6Descriptor desc):base(desc)
		{
			north = new int[]{0,1,2,3,4,5,6,7};
			south = new int[]{24,25,26,27,28,29,30,31};
			east = new int[]{8,9,10,11,12,13,14,15};
			west = new int[]{16,17,18,19,20,21,22,23};
		}

		public override int[] DrawIndexes(Direction dir, WeaponDescriptor weapon)
		{
			return new int[]{
				north[(int)dir],
				south[(int)dir],
				east[(int)dir],
				west[(int)dir]
							};
		}

#if WORKING
#if DIRECTX
		public void DrawFast(Surface target,int x, int y,int width,int height,int frame)
		{			
			north[frame].DrawFast(target,x,y-PckImage.Width/2,width,height);
			south[frame].DrawFast(target,x,y,width,height);
			east[frame].DrawFast(target,x+PckImage.Width/2,y-PckImage.Width/4,width,height);
			west[frame].DrawFast(target,x-PckImage.Width/2,y-PckImage.Width/4,width,height);		
		}
#else
		public void Draw(Graphics g, int x, int y, int frame)
		{
			north[frame].Draw(g,x,y-(int)((PckImage.Width*PckImage.Scale)/2));
			south[frame].Draw(g,x,y);
			east[frame].Draw(g,x+(int)((PckImage.Width*PckImage.Scale)/2),y-(int)((PckImage.Width*PckImage.Scale)/4));
			west[frame].Draw(g,x-(int)((PckImage.Width*PckImage.Scale)/2),y-(int)((PckImage.Width*PckImage.Scale)/4));	
		}

		public void Draw(Bitmap b, int x, int y, int frame)
		{
			Bmp.Draw(north[frame].Image,b,x,y-(int)((PckImage.Width*PckImage.Scale)/2));
			Bmp.Draw(south[frame].Image,b,x,y);
			Bmp.Draw(east[frame].Image,b,x+(int)((PckImage.Width*PckImage.Scale)/2),y-(int)((PckImage.Width*PckImage.Scale)/4));
			Bmp.Draw(west[frame].Image,b,x-(int)((PckImage.Width*PckImage.Scale)/2),y-(int)((PckImage.Width*PckImage.Scale)/4));	
		}
#endif
#endif
	}
}
