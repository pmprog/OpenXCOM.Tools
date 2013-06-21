using System;
using System.Collections;
using System.Drawing;
using XCom.Interfaces;

#region About the mcdEntry
/* from http://ufo2k-allegro.lxnt.info/srcdocs/terrapck_8h-source.html
struct MCD	 
{
 -unsigned char Frame[8];      //Each frame is an index into the ____.TAB file; it rotates between the frames constantly.
 -unsigned char LOFT[12];      //The 12 levels of references into GEODATA\LOFTEMPS.DAT
 -short int ScanG;      //A reference into the GEODATA\SCANG.DAT
 unsigned char u23;	22
 unsigned char u24;	23
 unsigned char u25;	24
 unsigned char u26;	25
 unsigned char u27;	26
 unsigned char u28;	27
 unsigned char u29;	28
 unsigned char u30;	29
 unsigned char UFO_Door;      //If it's a UFO door it uses only Frame[0] until it is walked through, then it animates once and becomes Alt_MCD.  It changes back at the end of the turn
 unsigned char Stop_LOS;      //You cannot see through this tile.
 unsigned char No_Floor;      //If 1, then a non-flying unit can't stand here
 unsigned char Big_Wall;      //It's an object (tile type 3), but it acts like a wall
 unsigned char Gravlift;
 unsigned char Door;      //It's a human style door--you walk through it and it changes to Alt_MCD
 unsigned char Block_Fire;       //If 1, fire won't go through the tile
 unsigned char Block_Smoke;      //If 1, smoke won't go through the tile
 unsigned char u39;
 unsigned char TU_Walk;       //The number of TUs require to pass the tile while walking.  An 0xFF (255) means it's unpassable.
 unsigned char TU_Fly;        // remember, 0xFF means it's impassable!
 unsigned char TU_Slide;      // sliding things include snakemen and silacoids
 unsigned char Armour;        //The higher this is the less likely it is that a weapon will destroy this tile when it's hit.
 unsigned char HE_Block;      //How much of an explosion this tile will block
 unsigned char Die_MCD;       //If the terrain is destroyed, it is set to 0 and a tile of type Die_MCD is added
 unsigned char Flammable;     //How flammable it is (the higher the harder it is to set aflame)
 unsigned char Alt_MCD;       //If "Door" or "UFO_Door" is on, then when a unit walks through it the door is set to 0 and a tile type Alt_MCD is added.
 unsigned char u48;
 signed char T_Level;      //When a unit or object is standing on the tile, the unit is shifted by this amount
 unsigned char P_Level;      //When the tile is drawn, this amount is subtracted from its y (so y position-P_Level is where it's drawn)
 unsigned char u51;
 unsigned char Light_Block;     //The amount of light it blocks, from 0 to 10
 unsigned char Footstep;         //The Sound Effect set to choose from when footsteps are on the tile
 unsigned char Tile_Type;       //This is the type of tile it is meant to be -- 0=floor, 1=west wall, 2=north wall, 3=object .  When this type of tile is in the Die_As or Open_As flags, this value is added to the tile coordinate to determine the byte in which the tile type should be written.
 unsigned char HE_Type;         //0=HE  1=Smoke
 unsigned char HE_Strength;     //The strength of the explosion caused when it's destroyed.  0 means no explosion.
 unsigned char Smoke_Blockage;      //? Not sure about this ...
 unsigned char Fuel;      //The number of turns the tile will burn when set aflame
 unsigned char Light_Source;      //The amount of light this tile produces
 unsigned char Target_Type;       //The special properties of the tile
 unsigned char u61;
 unsigned char u62;

	 };
	*/
#endregion

namespace XCom
{
	public enum SpecialType{Tile=0,StartPoint,IonBeamAccel,DestroyObjective,MagneticNav,AlienCryo,AlienClon,AlienLearn,AlienImplant,Unknown9,AlienPlastics,ExamRoom,DeadTile,EndPoint,MustDestroy};
	public enum TileType{Ground=0,WestWall=1,NorthWall=2,Object=3,All=-1};

	public class McdEntry:XCom.Interfaces.Base.IInfo
	{
		private byte[] info;
		private Rectangle rect;
		private int width, height;

		private static int globalStaticID=0;

		internal McdEntry(byte[] info)
			:base(globalStaticID++)
		{
			this.info = info;
			rect = new Rectangle(0,this.TileOffset,PckImage.Width,PckImage.Height-TileOffset);
			width=PckImage.Width;
			height = PckImage.Height-TileOffset;
			//staticID=globalStaticID++;
		}

		public Rectangle Bounds
		{
			get{return rect;}
		}

		public int Width
		{
			get{return width;}
		}

		public int Height
		{
			get{return height;}
		}

		public byte Image1{get{return info[0];}}
		public byte Image2{get{return info[1];}}
		public byte Image3{get{return info[2];}}
		public byte Image4{get{return info[3];}}
		public byte Image5{get{return info[4];}}
		public byte Image6{get{return info[5];}}
		public byte Image7{get{return info[6];}}
		public byte Image8{get{return info[7];}}

		public byte Loft1{get{return info[8];}}
		public byte Loft2{get{return info[9];}}
		public byte Loft3{get{return info[10];}}
		public byte Loft4{get{return info[11];}}
		public byte Loft5{get{return info[12];}}
		public byte Loft6{get{return info[13];}}
		public byte Loft7{get{return info[14];}}
		public byte Loft8{get{return info[15];}}
		public byte Loft9{get{return info[16];}}
		public byte Loft10{get{return info[17];}}
		public byte Loft11{get{return info[18];}}
		public byte Loft12{get{return info[19];}}
		public ushort ScanG{get{return (ushort)(info[21]*255+info[20]);}}

		public byte Unknown22{get{return info[22];}}//				unsigned char u62;
		public byte Unknown23{get{return info[23];}}//				unsigned char u62;
		public byte Unknown24{get{return info[24];}}//				unsigned char u62;
		public byte Unknown25{get{return info[25];}}//				unsigned char u62;
		public byte Unknown26{get{return info[26];}}//				unsigned char u62;
		public byte Unknown27{get{return info[27];}}//				unsigned char u62;
		public byte Unknown28{get{return info[28];}}//				unsigned char u62;
		public byte Unknown29{get{return info[29];}}//				unsigned char u62;

		public override bool UFODoor{get{return info[30]==1;}}//				If it's a UFO door it uses only Frame[0] until it is walked through, then it animates once and becomes Alt_MCD.  It changes back at the end of the turn
		public bool StopLOS{get{return info[31]!=1;}}//				unsigned char Stop_LOS;      //You cannot see through this tile.
		public bool NoGround{get{return info[32]==1;}}//			unsigned char No_Floor;      //If 1, then a non-flying unit can't stand here
		public bool BigWall{get{return info[33]==1;}}//				unsigned char Big_Wall;      //It's an object (tile type 3), but it acts like a wall
		public bool GravLift{get{return info[34]==1;}}//			unsigned char Gravlift;
		public override bool HumanDoor{get{return info[35]==1;}}//			unsigned char Door;      //It's a human style door--you walk through it and it changes to Alt_MCD - does not change back at end of turn
		public bool BlockFire{get{return info[36]==1;}}//			unsigned char Block_Fire;       //If 1, fire won't go through the tile
		public bool BlockSmoke{get{return info[37]==1;}}//			unsigned char Block_Smoke;      //If 1, smoke won't go through the tile

		public byte Unknown38{get{return info[38];}}//				unsigned char u39;
		public byte TU_Walk{get{return info[39];}}//				unsigned char TU_Walk;       //The number of TUs require to pass the tile while walking.  An 0xFF (255) means it's unpassable.
		public byte TU_Fly{get{return info[40];}}//					unsigned char TU_Fly;        // remember, 0xFF means it's impassable!
		public byte TU_Slide{get{return info[41];}}//				unsigned char TU_Slide;      // sliding things include snakemen and silacoids
		public byte Armor{get{return info[42];}}//					unsigned char Armour;        //The higher this is the less likely it is that a weapon will destroy this tile when it's hit.
		public byte HE_Block{get{return info[43];}}//				unsigned char HE_Block;      //How much of an explosion this tile will block
		public byte DieTile{get{return info[44];}}//				unsigned char Die_MCD;       //If the terrain is destroyed, it is set to 0 and a tile of type Die_MCD is added
		public byte Flammable{get{return info[45];}}//				unsigned char Flammable;     //How flammable it is (the higher the harder it is to set aflame)
		public byte Alt_MCD{get{return info[46];}}//				unsigned char Alt_MCD;       //If "Door" or "UFO_Door" is on, then when a unit walks through it the door is set to 0 and a tile type Alt_MCD is added.
		public byte Unknown47{get{return info[47];}}//				unsigned char u48;
		public override sbyte StandOffset{get{return (sbyte)info[48];}}//	signed char T_Level;      //When a unit or object is standing on the tile, the unit is shifted by this amount
		public override sbyte TileOffset{get{return (sbyte)info[49];}}//		unsigned char P_Level;      //When the tile is drawn, this amount is subtracted from its y (so y position-P_Level is where it's drawn)
		public byte Unknown50{get{return info[50];}}//				unsigned char u51;
		public sbyte LightBlock{get{return (sbyte)info[51];}}//		unsigned char Light_Block;     //The amount of light it blocks, from 0 to 10
		public sbyte Footstep{get{return (sbyte)info[52];}}//		unsigned char Footstep;         //The Sound Effect set to choose from when footsteps are on the tile

		public override TileType TileType{get{return (TileType)info[53];}}//	unsigned char Tile_Type;       //This is the type of tile it is meant to be -- 0=floor, 1=west wall, 2=north wall, 3=object .  When this type of tile is in the Die_As or Open_As flags, this value is added to the tile coordinate to determine the byte in which the tile type should be written.
		public sbyte HE_Type{get{return (sbyte)info[54];}}//			unsigned char HE_Type;         //0=HE  1=Smoke
		public sbyte HE_Strength{get{return (sbyte)info[55];}}//		unsigned char HE_Strength;     //The strength of the explosion caused when it's destroyed.  0 means no explosion.
		public sbyte SmokeBlockage{get{return (sbyte)info[56];}}//	unsigned char Smoke_Blockage;      //? Not sure about this ...
		public sbyte Fuel{get{return (sbyte)info[57];}}//				unsigned char Fuel;      //The number of turns the tile will burn when set aflame
		public sbyte LightSource{get{return (sbyte)info[58];}}//		unsigned char Light_Source;      //The amount of light this tile produces
		public override SpecialType TargetType{get{return (SpecialType)(sbyte)info[59];}}//		unsigned char Target_Type;       //The special properties of the tile
		public byte Unknown60{get{return info[60];}}//				unsigned char u61;
		public byte Unknown61{get{return info[61];}}//				unsigned char u62;

		public byte this[int i]
		{
			get{return info[i];}
		}

		public int Length
		{
			get{return info.Length;}
		}
	}
}
