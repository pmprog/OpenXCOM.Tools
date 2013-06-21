using System;
using System.Collections.Generic;
using System.Drawing;
using XCom.Interfaces;

namespace XCom
{
	public class XCImageCollection:List<XCImage>
	{
		protected string name,path;
		private Palette pal;
		protected int scale=1;
		private XCom.Interfaces.IXCImageFile ixcFile;

		public string Name
		{
			get{return name;}
			set{name=value;}
		}

		public string Path
		{
			get{return path;}
			set{path=value;}
		}

		public IXCImageFile IXCFile
		{
			get{return ixcFile;}
			set{ixcFile = value;}
		}

		public void Hq2x()
		{
			foreach(XCImage i in this)
				i.Hq2x();
			scale*=2;
		}

		public virtual Palette Pal
		{
			get{return pal;}
			set
			{
				foreach(XCImage i in this)
					i.Image.Palette = value.Colors;
				pal=value;
			}
		}

		public new XCImage this[int i]
		{
			get{return (i<Count && i>=0 ? base[i]:null);}
			set
			{
				if(i<Count && i>=0)
					base[i]=value;
				else 
				{
					value.FileNum = Count;
					Add(value);					
				}
			}
		}	

		public void Remove(int i)
		{
			RemoveAt(i);
		}
	}
}
