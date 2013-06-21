using System;
using System.Collections;
using System.Reflection;

namespace XCom.Mpk
{
	public enum MpkEntryType{Pal=0,Pck,Tab,Rmp,Map};
	public abstract class IMpkEntry
	{
		protected MpkEntryType mpkType;
		protected static Hashtable typeHash;
		protected byte[] data;
		protected string fileName;

		public virtual string FileName{get{return fileName;}set{fileName=value;}}
		public virtual byte[] Data{get{return data;}set{data=value;}}
		public virtual MpkEntryType Type{get{return mpkType;}}

		#region static Init
		private static bool init=false;
		public static void InitTypes()
		{
			typeHash = new Hashtable();
			typeHash[MpkEntryType.Pal]=typeof(MpkPalette);
			typeHash[MpkEntryType.Pck]=typeof(MpkPckFile);
			typeHash[MpkEntryType.Tab]=typeof(MpkTabFile);
			typeHash[MpkEntryType.Rmp]=typeof(MpkRmpFile);
			typeHash[MpkEntryType.Map]=typeof(MpkMapFile);
			init=true;
		}

		public static bool IsInit
		{
			get{return init;}
		}

		public static IMpkEntry GetType(MpkEntryType typ)
		{
			if(!init)
				InitTypes();
			if(typeHash[typ]==null)
				return null;

			Type t = (Type)typeHash[typ];
			ConstructorInfo ci = t.GetConstructor(new Type[]{});
			return (IMpkEntry)ci.Invoke(new Object[]{});
		}
		#endregion
	}
}
