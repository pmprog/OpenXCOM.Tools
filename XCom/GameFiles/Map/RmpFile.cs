using System;
using System.IO;
using System.Collections;

//when determining spawn positions the following nodes are tried
//Leader > Nav > Eng> Unknown8 > Med > Soldier > Any
//Nav > Eng > Soldier > Any
//Human is only used for xcom units
//civilians can only start on any nodes
//unknown6 is only used as an alternate choice to a medic
//unknown8 is only used by squad leaders and commanders

//unknown8 = any alien
//unknown6= Medic,Navigator,Soldier,Terrorist

	/*
		TFTD ---- UFO
		Commander ---- Commander
		Navigator ---- Leader
		Medic ---- Engineer
		Technition ---- Medic
		SquadLeader ---- Navigator 
		Soldier ---- Soldier
	*/

#region about the RMP file
/*
 // Route Record for RMP files
 //
 // RMP records describe the points in a lattice and the links connecting
 // them.  However, special values of rmpindex are:
 //   FF = Not used
 //   FE = Exit terrain to north
 //   FD = Exit terrain to east
 //   FC = Exit terrain to south
 //   FB = Exit terrain to west
 
 typedef struct latticelink
 {
    unsigned char rmpindex;              // Index of a linked point in the
 lattice
    unsigned char distance;              // Approximate distance from current
 point
    unsigned char linktype;              // Uses RRT_xxxx
 } LatticeLink;
 
 #define MaxLinks      5
 
 typedef struct rmprec
 {
    unsigned char row;
    unsigned char col;
    unsigned char lvl;
    unsigned char zero03;             // Always 0
    LatticeLink   latlink[MaxLinks];  // 5 3-byte entries (shown above)
    unsigned char type1;              // Uses RRT_xxxx: 0,1,2,4          
 observed
    unsigned char type2;              // Uses RRR_xxxx: 0,1,2,3,4,5, ,7, 
 observed
    unsigned char type3;              // Uses RRR_xxxx: 0,1,2,3,4,5,6,7,8
 observed
    unsigned char type4;              // Almost always 0
    unsigned char type5;              // 0=Don't use 1=Use most of the time...2+
 = Use less and less often, 0 thru A observed
 } RmpRec;
 
 // Types of Units
 
 #define RRT_Any       0             
 #define RRT_Flying    1             
 #define RRT_Small     2             
 #define RRT_4         4              // Unknown
 
 // Ranks of Units
 
 #define RRR_0         0              // Unknown
 #define RRR_Human     1             
 #define RRR_Soldier   2             
 #define RRR_Navigator 3
 #define RRR_Leader    4
 #define RRR_Engineer  5
 #define RRR_6         6              // Commander?
 #define RRR_Medic     7
 #define RRR_8         8              // Unknown
*/

#endregion
namespace XCom
{
	public enum UnitType : byte { Any = 0, Flying, Small, FlyingLarge, Large };

	public enum UnitRankUFO:byte{Civilian=0,XCom,Soldier,Navigator,LeaderCommander,Engineer,Misc1,Medic,Misc2};
	public enum UnitRankTFTD:byte{Civilian=0,XCom,Soldier,SquadLeader,LeaderCommander,Medic,Misc1,Technician,Misc2};
	public enum SpawnUsage:byte{NoSpawn=0,Spawn1=1,Spawn2=2,Spawn3=3,Spawn4=4,Spawn5=5,Spawn6=6,Spawn7=7,Spawn8=8,Spawn9=9,Spawn10=10};

	public enum UnitRankNum:byte{Zero=0,One,Two,Three,Four,Five,Six,Seven,Eight};
	public enum LinkTypes:byte{NotUsed=0xFF,ExitNorth=0xFE,ExitEast=0xFD,ExitSouth=0xFC,ExitWest=0xFB};
	

	public class RmpFile:IEnumerable
	{
		private ArrayList entries;
		private string basename, basePath;

		public static readonly object[] UnitRankUFO = { new StrEnum("0:Civ-Scout",XCom.UnitRankUFO.Civilian),
											new StrEnum("1:XCom",XCom.UnitRankUFO.XCom),
											new StrEnum("2:Soldier",XCom.UnitRankUFO.Soldier),
											new StrEnum("3:Navigator",XCom.UnitRankUFO.Navigator),
											new StrEnum("4:Leader/Commander",XCom.UnitRankUFO.LeaderCommander),
											new StrEnum("5:Engineer",XCom.UnitRankUFO.Engineer),
											new StrEnum("6:Misc1",XCom.UnitRankUFO.Misc1),
											new StrEnum("7:Medic",XCom.UnitRankUFO.Medic),
											new StrEnum("8:Misc2",XCom.UnitRankUFO.Misc2)};

		public static readonly object[] UnitRankTFTD = { new StrEnum("0:Civ-Scout",XCom.UnitRankTFTD.Civilian),
														  new StrEnum("1:XCom",XCom.UnitRankTFTD.XCom),
														  new StrEnum("2:Soldier",XCom.UnitRankTFTD.Soldier),
														  new StrEnum("3:Squad Leader",XCom.UnitRankTFTD.SquadLeader),
														  new StrEnum("4:Leader/Commander",XCom.UnitRankTFTD.LeaderCommander),
														  new StrEnum("5:Medic",XCom.UnitRankTFTD.Medic),
														  new StrEnum("6:Misc1",XCom.UnitRankTFTD.Misc1),
														  new StrEnum("7:Technician",XCom.UnitRankTFTD.Technician),
														  new StrEnum("8:Misc2",XCom.UnitRankTFTD.Misc2)};

		public static readonly object[] SpawnUsage = { new StrEnum("0:No Spawn",XCom.SpawnUsage.NoSpawn),
													   new StrEnum("1:Spawn",XCom.SpawnUsage.Spawn1),
													   new StrEnum("2:Spawn",XCom.SpawnUsage.Spawn2),
													   new StrEnum("3:Spawn",XCom.SpawnUsage.Spawn3),
													   new StrEnum("4:Spawn",XCom.SpawnUsage.Spawn4),
													   new StrEnum("5:Spawn",XCom.SpawnUsage.Spawn5),
													   new StrEnum("6:Spawn",XCom.SpawnUsage.Spawn6),
													   new StrEnum("7:Spawn",XCom.SpawnUsage.Spawn7),
													   new StrEnum("8:Spawn",XCom.SpawnUsage.Spawn8),
													   new StrEnum("9:Spawn",XCom.SpawnUsage.Spawn9),
													   new StrEnum("10:Spawn",XCom.SpawnUsage.Spawn10)};

		internal RmpFile(string basename, string basePath)
		{
			this.basename=basename;
			this.basePath=basePath;
			entries = new ArrayList();

			if(File.Exists(basePath+basename+".RMP"))
			{
				BufferedStream bs = new BufferedStream(File.OpenRead(basePath+basename+".RMP"));

				for(byte i=0;i<bs.Length/24;i++)
				{
					byte[] data = new byte[24];
					bs.Read(data,0,24);
					entries.Add(new RmpEntry(i,data));
				}
				bs.Close();
			}
		}

		public void Save()
		{	
			Save(File.Create(basePath+basename+".RMP"));
		}

		public void Save(FileStream fs)
		{
			for(int i=0;i<entries.Count;i++)
				((RmpEntry)entries[i]).Save(fs);	
			fs.Close();
		}

		public IEnumerator GetEnumerator()
		{
			return entries.GetEnumerator();
		}

		public RmpEntry this[int i]
		{
			get
      {
        if (entries.Count <= i)
          return null;
        return (RmpEntry)entries[i];
      }
		}

		public int Length
		{
			get{return entries.Count;}
		}

		public void RemoveEntry(RmpEntry r)
		{
			int oldIdx = r.Index;
			//Console.WriteLine("delete: "+r);

			entries.Remove(r);
			foreach(RmpEntry rr in entries)
			{
				if(rr.Index > oldIdx)
					rr.Index--;

				for(int i=0;i<5;i++)
				{
					Link l = rr[i];
					if(l.Index==oldIdx)
						l.Index=Link.NotUsed;
					else if(l.Index>oldIdx && l.Index<0xFB)
						l.Index--;
				}
			}

		}

		public RmpEntry AddEntry(byte row, byte col,byte height)
		{
			//Console.WriteLine("Adding {0},{1},{2}",row,col,height);
			RmpEntry re = new RmpEntry((byte)entries.Count,row,col,height);
			entries.Add(re);
			return re;
		}
	}

	public class RmpEntry
	{
		#region rmprec
		/*
		typedef struct rmprec
		{
			unsigned char row;
			unsigned char col;
			unsigned char lvl;
			unsigned char zero03;             // Always 0
			LatticeLink   latlink[MaxLinks];  // 5 3-byte entries (shown above)
			unsigned char type1;              // Uses RRT_xxxx: 0,1,2,4          
		observed
			unsigned char type2;              // Uses RRR_xxxx: 0,1,2,3,4,5, ,7, 
		observed
			unsigned char type3;              // Uses RRR_xxxx: 0,1,2,3,4,5,6,7,8
		observed
			unsigned char type4;              // Almost always 0
			unsigned char type5;              // 0=Don't use 1=Use most of the time...2+
		= Use less and less often, 0 thru A observed
		} RmpRec;		  
		 */
		#endregion
		private byte row,col,height;
		private Link[] links;
		private UnitType unitType;
		private byte unitRank1;
		private UnitRankNum unitRank2;
		private byte zero1,index;
		private SpawnUsage usage;
		//private byte[] data;

		public RmpEntry(byte idx,byte[] data)
		{
			//this.data = data;
			index=idx;
			row = data[0];
			col = data[1];
			height = data[2];

			links = new Link[5];

			int x=4;
			for(int i=0;i<5;i++)
			{
				links[i] = new Link(data[x],data[x+1],data[x+2]);
				x+=3;
			}

			unitType = (UnitType)data[19];
			unitRank1 = data[20];
			unitRank2 = (UnitRankNum)data[21];
			zero1 = data[22];
			usage = (SpawnUsage)data[23];
		}

		public RmpEntry(byte idx, byte row, byte col, byte height)
		{
			index = idx;
			this.row=row;
			this.col=col;
			this.height=height;
			links = new Link[5];
			for(int i=0;i<5;i++)
			{
				links[i] = new Link(Link.NotUsed,0,0);
			}
			unitType = (UnitType)0;
			unitRank1 = 0;
			unitRank2 = (UnitRankNum)0;
			zero1 = 0;
			usage = 0;
		}

		public override bool Equals(object o)
		{
			if(o is RmpEntry)
			{
				return index==((RmpEntry)o).index;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return index;
		}
        
		public void Save(FileStream fs)
		{
			//fs.Write(data,0,data.Length);
			
			fs.WriteByte(row);
			fs.WriteByte(col);
			fs.WriteByte(height);
			fs.WriteByte(0);
			for(int i=0;i<5;i++)
			{
				fs.WriteByte(links[i].Index);
				fs.WriteByte(links[i].Distance);
				fs.WriteByte((byte)links[i].UType);
			}
			fs.WriteByte((byte)unitType);
			fs.WriteByte((byte)unitRank1);
			fs.WriteByte((byte)unitRank2);
			fs.WriteByte(zero1);
			fs.WriteByte((byte)usage);
		}

		public override string ToString()
		{
			string res = "";
			res+="r:"+row+" c:"+col+" h:"+height;
			return res;
		}

		public byte Row
		{
			get{return row;}
		}

		public byte Col
		{
			get{return col;}
		}

		public byte Height
		{
			get{return height;}
		}

		public UnitType UType
		{
			get{return unitType;}
			set{unitType=value;}
		}
		public byte URank1
		{
			get{return unitRank1;}
			set{unitRank1=value;}
		}
		public UnitRankNum URank2
		{
			get{return unitRank2;}
			set{unitRank2=value;}
		}
		public byte Zero1
		{
			get{return zero1;}
			set{zero1=value;}
		}
		public SpawnUsage Usage
		{
			get{return usage;}
			set{usage=value;}
		}
	
		public int NumLinks
		{
			get{return links.Length;}
		}

		public Link this[int i]
		{
			get{return links[i];}
		}

		/// <summary>
		/// gets the index of this RmpEntry
		/// </summary>
		public byte Index
		{
			get{return index;}
			set{index=value;}
		}
	}

	public class Link
	{
		public const byte NotUsed=0xFF;
		public const byte ExitNorth=0xFE;
		public const byte ExitEast=0xFD;
		public const byte ExitSouth=0xFC;
		public const byte ExitWest=0xFB;

		private byte index, distance;
		private UnitType unitType;

		public Link(byte index, byte distance, byte type)
		{
			this.index=index;
			this.distance=distance;
			unitType=(UnitType)type;
		}

		/// <summary>
		/// Gets or sets the index of the destination node
		/// </summary>
		public byte Index
		{
			get{return index;}
			set{index=value;}
		}

		/// <summary>
		/// gets or sets the distance to the destination node
		/// </summary>
		public byte Distance
		{
			get{return distance;}
			set{distance=value;}
		}

		/// <summary>
		/// gets or sets the unit type that can use this link
		/// </summary>
		public UnitType UType
		{
			get{return unitType;}
			set{unitType=value;}
		}
	}
}
