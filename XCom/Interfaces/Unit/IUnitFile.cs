/*using System;

namespace XCom.Interfaces
{
	public abstract class IUnitFile
	{
		protected int[] deathImages;
		protected IUnitDescriptor descriptor;
		protected PckFile imageFile;

		protected IUnitFile(IUnitDescriptor desc)
		{
			descriptor=desc;
		}

		public int[] DeathIndexes
		{
			get{return deathImages;}
		}

		public IUnitDescriptor Descriptor
		{
			get{return descriptor;}
		}

		public PckFile ImageFile
		{
			get{return imageFile;}
			set{imageFile=value;}
		}

		public abstract int[] DrawIndexes(Direction dir, WeaponDescriptor weapon);
	}
}
*/