using System;

namespace XCom.Interfaces.Base
{
	public class IInfo
	{
		protected int id;

		protected IInfo(int id)
		{
			this.id = id;
		}

		public int ID { get { return id; } }
		public virtual sbyte TileOffset { get { return 0; } }
		public virtual sbyte StandOffset { get { return 0; } }
		public virtual TileType TileType { get { return TileType.All; } }
		public virtual SpecialType TargetType { get { return SpecialType.Tile; } }
		public virtual bool HumanDoor { get { return false; } }
		public virtual bool UFODoor { get { return false; } }
	}
}
