using System;
using System.IO;
using System.Collections.Generic;

namespace XCom
{
	public class ImageInfo:FileDesc
	{
		private Dictionary<string,ImageDescriptor> images;
		private VarCollection vars;

		public ImageInfo():base("")
		{
			images = new Dictionary<string, ImageDescriptor>();
			vars = new VarCollection();
		}

		public ImageDescriptor this[string name]
		{
			get{return images[name.ToUpper()];}
			set{images[name.ToUpper()]=value;}
		}

		public ImageInfo(string inFile,VarCollection v):base(inFile)
		{
			images = new Dictionary<string, ImageDescriptor>();
			StreamReader sr = new StreamReader(File.OpenRead(inFile));
			VarCollection vars = new VarCollection(sr,v);

			KeyVal kv = null;

			while((kv=vars.ReadLine())!=null)
			{
				ImageDescriptor img = new ImageDescriptor(kv.Keyword.ToUpper(),kv.Rest);
				images[kv.Keyword] = img;
			}
			sr.Close();
		}

		public override void Save(string outFile)
		{
			StreamWriter sw = new StreamWriter(outFile);

			List<string> a = new List<string>(images.Keys);
			a.Sort();
			Dictionary<string, Variable> vars = new Dictionary<string, Variable>();

			foreach(string str in a)
				if(images[str]!=null)
				{
					ImageDescriptor id = images[str];
					if(!vars.ContainsKey(id.BasePath))
						vars[id.BasePath]=new Variable(id.BaseName+":",id.BasePath);
					else
						vars[id.BasePath].Inc(id.BaseName+":");
				}

			foreach(string basePath in vars.Keys)
				vars[basePath].Write(sw);

			sw.Flush();
			sw.Close();
		}

		public Dictionary<string, ImageDescriptor> Images
		{
			get{return images;}
		}
	}
}
