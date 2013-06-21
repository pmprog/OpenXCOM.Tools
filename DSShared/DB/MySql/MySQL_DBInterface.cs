//#define USING_MYSQL




#if USING_MYSQL
using System;
using MySql.Data.MySqlClient;

namespace DSShared.DB_MySql
{
	public class MySQL_DBInterface
	{
		private static MySQL_DBInterface instanceObject=null;
		private MySql.Data.MySqlClient.MySqlConnection connection;
//		private MySql.Data.MySqlClient.MySqlDataReader res;

		private MySQL_DBInterface()
		{
		}
/*
		public MySqlDataReader ExecuteReader(MySqlCommand comm)
		{
			res = comm.ExecuteReader();
			return res;
		}

		public void CloseReader()
		{
			res.Close();
			res=null;
		}

		public object GetColumn(string col)
		{
			if(res==null)
				throw new Exception("Call to DBInterface::GetColumn on a null reader");

			int colNum = res.GetOrdinal(col);
			Console.WriteLine("Type: "+res.GetDataTypeName(colNum));
			return res[col];
		}*/

		public MySql.Data.MySqlClient.MySqlConnection Connection
		{
			get{return connection;}
		}

		public static void Initialize(string server, string username, string password,string database)
		{
			string connStr = String.Format("server={0};user id={1}; password={2}; database={3}; pooling=false",
				server, username, password, database );

			Instance.connection=new MySql.Data.MySqlClient.MySqlConnection(connStr);
		}

		public static MySQL_DBInterface Instance
		{
			get
			{
				if(instanceObject==null)
					instanceObject=new MySQL_DBInterface();
				return instanceObject;
			}
		}
	}
}
#endif
