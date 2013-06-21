using System;
using System.Drawing;
using System.ComponentModel;

namespace DSShared
{
	public abstract class IColorable
	{
		protected int idx=-1;
		protected Color drawColor=Color.LightGray;

		public virtual bool UseColor(){return idx!=-1;}
		[Browsable(false)]
		public virtual int ColorIndex{get{return idx;}set{idx=value;}}
		[Browsable(false)]
		public virtual Color DrawColor{get{return drawColor;}set{drawColor=value;}}				
	}
}
