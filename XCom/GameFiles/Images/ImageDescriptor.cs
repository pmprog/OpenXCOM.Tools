using System;
using System.Collections;
using System.IO;

namespace XCom
{
	/// <summary>
	/// Describes information about imagesets: the path to the pck, tab and mcd files
	/// </summary>
	public class ImageDescriptor:IComparable
	{
		private string basename;
		private string basePath;
		//private PckFile myPck;
		//private McdFile myMcd;
		private Hashtable pckTab,mcdTab;

		public ImageDescriptor(string basename)
		{
			this.basename = basename;
			basePath="";
			pckTab = new Hashtable(3);
			mcdTab = new Hashtable(3);
		}

		public ImageDescriptor(string basename, string path):this(basename)
		{
			basePath=path;
		}

		public PckFile GetPckFile(Palette p)
		{
			if(pckTab[p]==null)			
				pckTab[p] = GameInfo.CachePck(basePath,basename,2,p);//new PckFile(File.OpenRead(basePath+basename+".PCK"),File.OpenRead(basePath+basename+".TAB"),2,p,screen);//GameInfo.GetPckFile(basename,basePath,p,2,screen);
			
			return (PckFile)pckTab[p];
		}

		public PckFile GetPckFile()
		{
			return GetPckFile(GameInfo.DefaultPalette);
		}

		public McdFile GetMcdFile(Palette p)
		{
			if(mcdTab[p]==null)
				mcdTab[p] = new McdFile(basename,basePath,GetPckFile(p));//GameInfo.GetMcdFile(basename,basePath,GetPckFile(p));
			return (McdFile)mcdTab[p];
		}

		public McdFile GetMcdFile()
		{
			return GetMcdFile(GameInfo.DefaultPalette);
		}

		public override string ToString()
		{
			return basename;
		}

		public int CompareTo(object other)
		{
			return basename.CompareTo(other.ToString());
		}

		public string BaseName
		{
			get{return basename;}
		}

		public string BasePath
		{
			get{return basePath;}
			set{basePath=value;}
		}
	}
}
