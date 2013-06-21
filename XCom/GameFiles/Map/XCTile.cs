using System;
using System.Drawing;
using XCom.Interfaces;

namespace XCom
{
	public class XCTile : XCom.Interfaces.Base.ITile
	{
		private McdFile mcdFile;
		
		private XCTile dead;
		private XCTile alternate;
		private PckFile myFile;
		private XCTile[] tiles;
		private const int numImages=8;

		public XCTile(int id,PckFile file, McdEntry info,McdFile mFile):base(id)
		{
			this.info = info;
			myFile = file;
			mcdFile = mFile;

			image = new PckImage[numImages];	
	
			if(!info.UFODoor && !info.HumanDoor)
				MakeAnimate();
			else
				StopAnimate();

			dead =null;
			alternate = null;
		}

		public void MakeAnimate()
		{
			image[0] = myFile[((McdEntry)info).Image1];
			image[1] = myFile[((McdEntry)info).Image2];
			image[2] = myFile[((McdEntry)info).Image3];
			image[3] = myFile[((McdEntry)info).Image4];
			image[4] = myFile[((McdEntry)info).Image5];
			image[5] = myFile[((McdEntry)info).Image6];
			image[6] = myFile[((McdEntry)info).Image7];
			image[7] = myFile[((McdEntry)info).Image8];
		}

		public void StopAnimate()
		{
			image[0] = myFile[((McdEntry)info).Image1];
			image[1] = myFile[((McdEntry)info).Image1];
			image[2] = myFile[((McdEntry)info).Image1];
			image[3] = myFile[((McdEntry)info).Image1];
			image[4] = myFile[((McdEntry)info).Image1];
			image[5] = myFile[((McdEntry)info).Image1];
			image[6] = myFile[((McdEntry)info).Image1];
			image[7] = myFile[((McdEntry)info).Image1];
		}

		public XCTile[] Tiles
		{
			get{return tiles;}
			set
			{
				tiles=value;
				try
				{
					if(((McdEntry)info).DieTile!=0)
						dead = tiles[((McdEntry)info).DieTile];
				}
				catch
				{
					if (this.mapID == 102 || mapID == 0)
						dead = tiles[7];
					else
						Console.WriteLine("Error, could not set dead tile: {0} mapID:{1}",((McdEntry)info).DieTile,mapID);
				}

				if(((McdEntry)info).UFODoor || ((McdEntry)info).HumanDoor || ((McdEntry)info).Alt_MCD!=0)
					alternate = tiles[((McdEntry)info).Alt_MCD];
			}
		}

		public XCTile Dead
		{
			get{return dead;}
			set{dead=value;}
		}

		public XCTile Alternate
		{
			get{return alternate;}
			set{alternate=value;}
		}
	}
}
