using System;

namespace DSShared
{
	/// <summary>
	/// Class to help pass around file paths
	/// </summary>
	public class PathInfo
	{
		private string path="", file="", ext="";

		/// <summary>
		/// Extension part of the path (.exe)
		/// </summary>
		public string Ext
		{
			get { return ext; }
			set { ext = value; }
		}

		/// <summary>
		/// Filename part of the path, without extension (file1)
		/// </summary>
		public string File
		{
			get { return file; }
			set { file = value; }
		}

		/// <summary>
		/// Directory path (C:\directory1\directory2)
		/// </summary>
		public string Path
		{
			get { return path; }
			set { path = value; }
		}

		/// <summary>
		/// First checks if the directory exists, than checks if file exists
		/// </summary>
		/// <returns>System.IO.File.Exists(ToString())</returns>
		public bool Exists()
		{
			if(System.IO.Directory.Exists(path))
				return System.IO.File.Exists(ToString());
			return false;
		}

		/// <summary>
		/// Calling this will create the directory if it does not exist
		/// </summary>
		public void EnsureDirectoryExists()
		{
			if (!System.IO.Directory.Exists(path))
				System.IO.Directory.CreateDirectory(path);
		}


		/// <summary>
		/// Initializes a new instance of the <see cref="T:PathInfo"/> class.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="file">The file.</param>
		/// <param name="ext">The ext.</param>
		public PathInfo(string path, string file, string ext)
		{
			this.path = path;
			this.file = file;
			this.ext = ext;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:PathInfo"/> class.
		/// </summary>
		/// <param name="fullPath">The full path.</param>
		public PathInfo(string fullPath):this(fullPath,true){}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:PathInfo"/> class.
		/// </summary>
		/// <param name="fullPath">The full path.</param>
		/// <param name="parseFile">if set to <c>true</c> the path will be broken down into filename and extension parts. You should pass false if the path string does not describe a file location</param>
		public PathInfo(string fullPath, bool parseFile)
		{
			if (parseFile && fullPath.IndexOf(".") > 0)
			{
				ext = fullPath.Substring(fullPath.LastIndexOf(".") + 1);
				file = fullPath.Substring(fullPath.LastIndexOf("\\") + 1);
				file = file.Substring(0, file.LastIndexOf("."));
				path = fullPath.Substring(0, fullPath.LastIndexOf("\\"));
			}
			else
			{
				ext = "";
				file = "";
				path = fullPath;
			}
		}

		/// <summary>
		/// String representation of this path, with the supplied extension added on instead of the one this object was constructed with
		/// </summary>
		/// <param name="newExt">The extension that will replace the one in Ext</param>
		/// <returns></returns>
		public string ToStringExt(string newExt)
		{
			return path + "\\" + file + "." + newExt;
		}


		/// <summary>
		/// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		/// </returns>
		public override string ToString()
		{
			if(ext!="")
				return path + "\\" + file + "." + ext;
			return path + "\\" + file;
		}
	}
}
