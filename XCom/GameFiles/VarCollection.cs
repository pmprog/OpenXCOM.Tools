using System;
using System.Collections;
using System.IO;

namespace XCom
{
	public class VarCollection
	{
		private Hashtable vars;
		private VarCollection other;
		private string baseVar;
		private StreamReader sr;

		public VarCollection()
		{
			vars = new Hashtable();
			other=null;
			baseVar="";
		}

		public StreamReader BaseStream
		{
			get{return sr;}
		}

		public VarCollection(StreamReader sr)
		{
			this.sr=sr;
			vars = new Hashtable();
			other=null;
		}

		public VarCollection(string baseVar):this()
		{
			this.baseVar=baseVar;
		}

		public VarCollection(VarCollection other):this()
		{
			this.other=other;
		}

		public VarCollection(StreamReader sr,VarCollection vars)
		{
			this.vars=new Hashtable();
			this.sr=sr;
			other=vars;
		}

		public void AddVar(string flag, string val)
		{
			if(vars[val]==null)
				vars[val] = new Variable(baseVar,flag+":",val);
			else
				((Variable)vars[val]).Inc(flag+":");
		}

		public VarCollection Other
		{
			get{return other;}
		}

		public Hashtable Vars
		{
			get{return vars;}
		}

		public string ParseVar(string line)
		{			
			foreach(string s in vars.Keys)
				line = line.Replace(s,(string)vars[s]);

			if(other!=null)
				return other.ParseVar(line);

			return line;
		}

		public ICollection Variables
		{
			get{return vars.Keys;}
		}

		public string this[string var]
		{
			get
			{
				if(other == null || vars[var]!=null)
					return (string)vars[var];
				return other[var];
			}
			set
			{
				vars[var]=value;
			}
		}

		public KeyVal ReadLine()
		{
			string line = ReadLine(sr,this);
			if(line==null)
				return null;
			int idx = line.IndexOf(':');
			if(idx>0)
				return new KeyVal(line.Substring(0,idx),line.Substring(idx+1));
			else
				return new KeyVal(line,"");
		}

		public string ReadLine(StreamReader sr)
		{
			return ReadLine(sr,this);
		}

		public static string ReadLine(StreamReader sr,VarCollection vars)
		{
			string line = "";

			while(true)
			{
				do //get a good line - not a comment or empty string
				{
					if(sr.Peek()!=-1)
						line = sr.ReadLine().Trim();
					else
						return null;
				}while(line.Length==0 || line[0]=='#');

				if(line[0]=='$') //cache variable, get another line
				{
					int idx = line.IndexOf(':');
					string var = line.Substring(0,idx);
					string val = vars.ParseVar(line.Substring(idx+1));
					vars[var]=val;
				}
				else //got a line
					break;
			}

			if(line.IndexOf("$")>0) //replace any variables the line might have
				line = vars.ParseVar(line);
			
			return line;
		}
	}

	public class KeyVal
	{
		private string keyword,rest;

		public KeyVal(string keyword,string rest)
		{
			this.keyword=keyword;
			this.rest=rest;
		}

		public string Keyword
		{
			get{return keyword;}
		}

		public string Rest
		{
			get{return rest;}
		}

		public override string ToString()
		{
			return keyword+":"+rest;
		}
	}
}