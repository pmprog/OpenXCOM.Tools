using System;
using System.IO;
using System.Collections;

namespace PckView
{
	/// <summary>
	/// TabFiles contain offset information for PckImages
	/// </summary>
	public class TabFile:IEnumerable
	{
		uint[] table;
		long eof;
		
		internal TabFile(Stream s,int bytesPerRecord)
		{
			BinaryReader input = new BinaryReader(s);
			table = new uint[input.BaseStream.Length/bytesPerRecord];
			eof=input.BaseStream.Length;

			if(bytesPerRecord==2)
				for(int i=0;i<table.Length;i++)
					table[i] = input.ReadUInt16();
			else //4 bytes
				for(int i=0;i<table.Length;i++)
					table[i] = input.ReadUInt32();
			
			input.Close();

			/*
			BufferedStream input = new BufferedStream(s);

			table = new int[input.Length/bytesPerRecord];
			eof = (int)input.Length;
			for(int i=0;i<table.Length;i++)
			{
				int val =0;
				for(int j=0;j<bytesPerRecord;j++)
				{
					val = val | (input.ReadByte() << (j*8));
				}
				table[i]=val;
			}

			input.Close();*/
		}

		public int Length
		{
			get{return table.Length;}
		}

		public long this[int i]
		{
			get
			{
				if(i>=0 && i<table.Length)
					return table[i];
				else if(i==table.Length)
					return eof;
				else return -1;
			}
		}

		public IEnumerator GetEnumerator()
		{
			return table.GetEnumerator();
		}
	}
}
