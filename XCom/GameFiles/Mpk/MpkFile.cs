using System;
using System.IO;
using System.Collections;
using XCom.Mpk;

namespace XCom
{
	/* File format:
	 * <header>
	 * <file 1>
	 * ...
	 * <file n> 
	 * 
	 * Header:
	 * string(256)  Version 1.0
	 * byte        number of files
	 * int1        size of file 1
	 * ...         etc
	 * intn        size of file n
	 * 
	 * File:
	 * string:     Filename (null terminated)
	 * byte        file type
	 * byte[]      binary file data
	 */
	public enum MpkOption{Save,Load,SaveImages,Bpp,SavePalette,SaveMinimal,NumImages,FileName};

	public class MpkFile
	{
		private Hashtable options;
		private ArrayList maps;

		private const string v1String="Version 1.0";
		private const int verLength=256;

		public MpkFile()
		{
			if(!IMpkEntry.IsInit)
				IMpkEntry.InitTypes();
		}

		public ArrayList Maps
		{
			get{return maps;}
			set{maps=value;}
		}

		public void WriteFile(string file)
		{
			BinaryWriter bw = new BinaryWriter(File.OpenWrite(file));
			WriteFile(bw);
			bw.Flush();
			bw.Close();
		}	

		public void WriteFile(BinaryWriter bw)
		{
			MpkFileHeader header = new MpkFileHeader();
			ArrayList entries = new ArrayList();
			
			//... put all IMpkEntries into arraylist

			header.Entries=entries;
			header.Write(bw);
			bw.Flush();
			foreach(IMpkEntry imp in entries)
			{
				bw.Write(imp.FileName.ToCharArray());
				bw.Write((char)'\0');
				bw.Write((byte)imp.Type);
				bw.Write(imp.Data);
				bw.Flush();
			}
		}

		public void ReadFile(string file)
		{
			BinaryReader br = new BinaryReader(File.OpenRead(file));
			ReadFile(br);
			br.Close();
		}

		private void readV1(BinaryReader br,MpkFileHeader header)
		{
			ArrayList files = new ArrayList();
			foreach(int size in header.Sizes)
			{
				ArrayList chars = new ArrayList();
				char ch;
				while((ch=br.ReadChar())!='\0')
					chars.Add(ch);
				
				byte[] data = br.ReadBytes(size);
				IMpkEntry ime = IMpkEntry.GetType((MpkEntryType)br.ReadByte());
				ime.Data=data;
				ime.FileName=new string((char[])chars.ToArray(typeof(char)));
				files.Add(ime);					
			}
		}

		public void ReadFile(BinaryReader br)
		{
			MpkFileHeader header = new MpkFileHeader();
			header.Read(br);
			switch(header.VersionString)
			{
				case "Version 1.0":
					readV1(br,header);
					break;
			}
			ReadFile(br);
		}

		public void SetOption(MpkOption option, object val)
		{
			if(options==null)
				options = new Hashtable();
			options[option]=val;
		}

		public string Version
		{
			get{return v1String;}
		}

		private class MpkFileHeader
		{
			private ArrayList entries;
			private string version;
			private int[] sizes;

			public MpkFileHeader()
			{
				entries=new ArrayList();
				sizes = new int[0];
			}

			public string VersionString
			{
				get{return version;}
				set{version=value;}
			}

			public int[] Sizes
			{
				get{return sizes;}
			}

			public ArrayList Entries
			{
				set{entries=value;}
			}

			/* 	 
			* Header:
			* string(verLength)  Version 1.0
			* byte        number of files
			* int1        size of file 1
			* ...         etc
			* intn        size of file n
			*/
			public void Write(BinaryWriter bw)
			{
				char[] chrs = new char[verLength];
				for(int i=0;i<version.Length;i++)
					chrs[i]=version[i];				
				for(int i=version.Length;i<chrs.Length;i++)
					chrs[i]=' ';		
		
				bw.Write(chrs);
				bw.Write((byte)entries.Count);
				foreach(IMpkEntry imp in entries)
					bw.Write((int)imp.Data.Length);
			}

			public void Read(BinaryReader br)
			{
				version = new string(br.ReadChars(verLength)).Trim();
				
				switch(version)
				{
					case v1String:
					{
						sizes = new int[br.ReadByte()];
						for(int i=0;i<sizes.Length;i++)
							sizes[i]=br.ReadInt32();
					}break;
				}
			}
		}


		public class MpkEntryHeader
		{
			private string file;
			private MpkEntryType type;

			public MpkEntryHeader(){}
			public MpkEntryHeader(string file, MpkEntryType type){this.file=file;this.type=type;}

			public void Read(BinaryReader br)
			{
				byte len = br.ReadByte();
				file = new string(br.ReadChars(len));
				type = (MpkEntryType)br.ReadByte();
			}
			public void Write(BinaryWriter bw)
			{
				bw.Write((byte)file.Length);
				bw.Write(file.ToCharArray());
				bw.Write((byte)type);
			}
		}
	}
}
