using System;

namespace XCom.Interfaces.Base
{
	/// <summary>
	/// This class provides all the necessary information to draw an animated sprite
	/// </summary>
	public class ITile
	{
		protected XCImage[] image;
		protected int id,mapID;
		protected IInfo info;

		public ITile(int id) { this.id = id; mapID = -1; info = null; }

		/// <summary>
		/// This is the ID unique to this ITile after it has been loaded
		/// </summary>
		public int ID
		{
			get { return id; }
		}

		/// <summary>
		/// This is the ID by which the map knows this tile by
		/// </summary>
		public int MapID
		{
			get { return mapID; }
			set { mapID = value; }
		}

		/// <summary>
		/// Gets an image at the specified animation frame
		/// </summary>
		/// <param name="i"></param>
		/// <returns></returns>
		public XCImage this[int i]
		{
			get { return image[i]; }
			set { image[i] = value; }
		}

		/// <summary>
		/// Gets the image array used to animate this tile
		/// </summary>
		public XCImage[] Images
		{
			get { return image; }
		}

		/// <summary>
		/// The Info object that has additional flags and information about this tile
		/// </summary>
		public IInfo Info
		{
			get { return info; }
		}
	}
}
