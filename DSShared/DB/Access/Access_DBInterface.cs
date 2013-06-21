using System;
using System.Data.OleDb;

namespace DSShared.DB_Access
{
	/// <summary>
	/// Singleton object providing a central location to the connection object for an Access database
	/// </summary>
	public class Access_DBInterface
	{
		private static Access_DBInterface instanceObject=null;
		private OleDbConnection connection;

		private Access_DBInterface()
		{
		}

		/// <summary>
		/// Gets the OleDbConnection used for an access database. This will be null until Initialize is called
		/// </summary>
		public OleDbConnection Connection
		{
			get{return connection;}
		}

		/// <summary>
		/// Initializes the connection, creating a basic connection of the form:
		/// Provider=Microsoft.Jet.OLEDB.4.0; Data Source="+dbFile
		/// </summary>
		/// <param name="dbFile"></param>
		public static void Initialize(string dbFile)
		{
			string connStr = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source="+dbFile;
			Instance.connection = new OleDbConnection(connStr);
		}

		/// <summary>
		/// Singleton access to this object
		/// </summary>
		public static Access_DBInterface Instance
		{
			get
			{
				if(instanceObject==null)
					instanceObject=new Access_DBInterface();
				return instanceObject;
			}
		}
	}
}
