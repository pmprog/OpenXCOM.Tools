using System;
using System.IO;

namespace XCom
{
	public class LogFile
	{
		private StreamWriter sw;

		private static LogFile myFile;
		public static readonly string DefaultFile="debug.log";
		private static bool debugOn=false;

		private LogFile(string filename)
		{
			if(debugOn)
				sw = new StreamWriter(File.Open(filename,FileMode.Create));
		}

		public static bool DebugOn
		{
			get{return debugOn;}
			set{debugOn=value;}
		}

		public static LogFile Instance
		{
			get{return Init(DefaultFile);}
		}

		public static LogFile Init(string filename)
		{
#if DEBUG
			debugOn=true;
#endif
			if(myFile==null)
				myFile = new LogFile(filename);
			return myFile;
		}

		public void Write(string text)
		{
			if((debugOn || sw!=null) && sw!=null)
			{
				sw.Write(text);
				sw.Flush();
			}
		}

		public void WriteLine(string text)
		{
			if((debugOn || sw!=null) && sw!=null)
			{
				sw.WriteLine(text);
				sw.Flush();
			}
		}

		public void Close()
		{
			if((debugOn || sw!=null) && sw!=null)
			{
				sw.Close();
			}
		}
	}
}
