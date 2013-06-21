using System;
using System.Reflection;
using System.Collections;
using System.Data.OleDb;
using DSShared.DB;

namespace DSShared.DB_Access
{
	/// <summary>
	/// Delegate for the Access_DBTable.FinishGetAll event
	/// </summary>
	/// <param name="table">Table that was just accessed</param>
	/// <param name="rows">Collection of rows that was just accessed</param>
	public delegate void FinishGetAllDelegate(string table,ArrayList rows);

	/// <summary>
	/// Provides basic support for insert, update and delete operations. Properties of the extended class
	/// that are tagged with DBColumnAttributes will be reflected upon so that the system knows which properties
	/// map to what columns in your table
	/// 
	/// Before creation of a an object that extends this class, make sure your type is registered with the 
	/// DBTableTypeCache
	/// </summary>
	public abstract class Access_DBTable:IComparable
	{
		/// <summary>
		/// The DBTableType that was setup upon initial table registration
		/// </summary>
		protected DSShared.DB.DBTableType myType;
		private static string paramPrefix="@DSS";

		/// <summary>
		/// When a GetAll() method is called, this event is fired right before the list of rows is 
		/// returned to the caller. This event is only raised if the list of rows is not currently
		/// cached
		/// </summary>
		public static event FinishGetAllDelegate FinishGetAll;

		/// <summary>
		/// Constructor for an access table
		/// </summary>
		/// <param name="tableName">Name of the table in the database this object represents</param>
		public Access_DBTable(string tableName)
		{
			myType = DSShared.DB.DBTableTypeCache.Instance.CacheTable(tableName);
		}

		/// <summary>
		/// Opens a connection and transaction to the database and calls GetAllHash(OleDbConnection conn,OleDbTransaction trans,string table,bool cache,params WhereCol[] whereQuery)
		/// with it. Upon success, the transaction is committed and the connection closed
		/// </summary>
		/// <param name="table">table name</param>
		/// <param name="cache">if the table does not change frequently, you should set this to true</param>
		/// <param name="whereQuery">list of columns to query upon. Be careful when used with cache'ing</param>
		/// <returns></returns>
		public static Hashtable GetAllHash(string table, bool cache, params WhereCol[] whereQuery)
		{
			if(cache && DSShared.DB.DBTableTypeCache.Instance.GetHash(table)!=null)
				return DSShared.DB.DBTableTypeCache.Instance.GetHash(table);

			Access_DBInterface.Instance.Connection.Open();
			OleDbTransaction trans = Access_DBInterface.Instance.Connection.BeginTransaction();
			Hashtable hash = GetAllHash(Access_DBInterface.Instance.Connection,trans,table,cache,whereQuery);
			trans.Commit();
			Access_DBInterface.Instance.Connection.Close();

			return hash;
		}

		/// <summary>
		/// Calls GetAllHash(table,cache,null);
		/// </summary>
		/// <param name="table"></param>
		/// <param name="cache"></param>
		/// <returns></returns>
		public static Hashtable GetAllHash(string table, bool cache)
		{
			return GetAllHash(table,cache,null);
		}

		/// <summary>
		/// Retrieves all rows from a single table according to the columns specified
		/// The keys of the hashtable are autonumber values which are retrieved from properties
		/// tagged with a DBColumn value of true for autoNumber
		/// </summary>
		/// <param name="conn">Connection to the database</param>
		/// <param name="trans">Transaction object that is open and paired with the connection</param>
		/// <param name="table">Name of the table to get all the rows from</param>
		/// <param name="cache">If true, values will be retrieved from the cache if available</param>
		/// <param name="whereQuery">List of columns to limit the search with. If you cache an incomplete row list, that list will be returned in future cached retrievals</param>
		/// <returns>A table of [key:int] [value:Access_DBTable] objects. The key is based on autonumber columns</returns>
		public static Hashtable GetAllHash(OleDbConnection conn,OleDbTransaction trans,string table,bool cache,params WhereCol[] whereQuery)
		{
			if(cache && DSShared.DB.DBTableTypeCache.Instance.GetHash(table)!=null)
				return DSShared.DB.DBTableTypeCache.Instance.GetHash(table);

			ArrayList list = GetAll(conn,trans,table,cache,whereQuery);

			DSShared.DB.DBTableType thisType = DSShared.DB.DBTableTypeCache.Instance.GetType(table);

			if(thisType.AutoNumber!=null)
			{
				PropertyInfo pi = thisType.AutoNumber;
				Hashtable hash = new Hashtable();
				foreach(Access_DBTable dt in list)
					hash[pi.GetGetMethod().Invoke(dt,null)]=dt;

				if(cache)
					DSShared.DB.DBTableTypeCache.Instance.CacheHash(table,hash);

				return hash;
			}

			throw new Exception("Call GetAllHash on table: "+table+" invalid since it does not have an autonumber defined");
		}

		/// <summary>
		/// Calls GetAll(table,cache,null)
		/// </summary>
		/// <param name="table">Table name</param>
		/// <param name="cache">If true, cache'ing will be used</param>
		/// <returns>A list of Access_DBTable in the order returned by the query</returns>
		public static ArrayList GetAll(string table, bool cache)
		{
			return GetAll(table,cache,null);
		}

		/// <summary>
		/// Opens a database connection and calls GetAll(OleDbConnection conn,OleDbTransaction trans,string table,bool cache,params WhereCol[] whereQuery)
		/// Upon success, the transaction is committed and the connection is closed
		/// </summary>
		/// <param name="table">Table name</param>
		/// <param name="cache">If true, cache'ing will be used</param>
		/// <param name="whereQuery">List of columns to limit the search with. If you cache an incomplete row list, that list will be returned in future cached retrievals</param>
		/// <returns>A list of Access_DBTable in the order returned by the query</returns>
		public static ArrayList GetAll(string table, bool cache,params WhereCol[] whereQuery)
		{
			if(cache && DSShared.DB.DBTableTypeCache.Instance.GetList(table)!=null)
				return DSShared.DB.DBTableTypeCache.Instance.GetList(table);

			Access_DBInterface.Instance.Connection.Open();
			OleDbTransaction trans = Access_DBInterface.Instance.Connection.BeginTransaction();
			ArrayList list = GetAll(Access_DBInterface.Instance.Connection,trans,table,cache,whereQuery);
			trans.Commit();
			Access_DBInterface.Instance.Connection.Close();

			return list;
		}

		/// <summary>
		/// Retrieves all rows from a single table according to the columns specified
		/// The order of items is based on the order retrieved from the query
		/// Generally, this method does a SELECT * FROM table
		/// </summary>
		/// <param name="conn">Connection to use</param>
		/// <param name="trans">Transaction to use</param>
		/// <param name="table">Table name</param>
		/// <param name="cache">If true, cache'ing will be used</param>
		/// <param name="whereQuery">List of columns to limit the search with. If you cache an incomplete row list, that list will be returned in future cached retrievals</param>
		/// <returns>A list of Access_DBTable in the order returned by the query</returns>
		public static ArrayList GetAll(OleDbConnection conn,OleDbTransaction trans,string table,bool cache,params WhereCol[] whereQuery)
		{
			DSShared.DB.DBTableType thisTable = DSShared.DB.DBTableTypeCache.Instance.CacheTable(table);

			OleDbCommand comm = new OleDbCommand("SELECT * FROM "+thisTable.TableName,conn,trans);

			if(whereQuery!=null && whereQuery.Length>0)
			{
				bool flag=false;
				comm.CommandText+=" WHERE ";
				int i=0;
				foreach(WhereCol dbc in whereQuery)
				{
					if(flag)
						comm.CommandText+=" AND ";
					else
						flag=true;

					string paramID = paramPrefix+(i++);

					comm.CommandText+=dbc.Column+"="+paramID;
					comm.Parameters.AddWithValue(paramID, dbc.Data);
					//comm.Parameters.Add(paramID,dbc.Data);
				}
			}

			OleDbDataReader res = comm.ExecuteReader();
			ArrayList list = new ArrayList();
			Type myType = DSShared.DB.DBTableTypeCache.Instance.GetTableType(thisTable.TableName);
			while(res.Read())
			{
				//if null object here, then the type does not have a default constructor defined
				object newObject = myType.GetConstructor(new Type[]{}).Invoke(null);
				foreach(PropertyInfo pi in thisTable.Columns)
				{
					if(thisTable[pi]!=null)
						pi.SetValue(newObject,res[thisTable[pi].ColumnName],null);
				}
				
				//Console.WriteLine("Type: "+objType);
				list.Add(newObject);

				//((DBTable)newObject).FinishGet(conn2);
			}
			res.Close();

			foreach(Access_DBTable dbt in list)
				dbt.FinishGet(conn,trans);

			if(cache)
				DSShared.DB.DBTableTypeCache.Instance.CacheList(table,list);

			if(FinishGetAll!=null)
				FinishGetAll(table,list);

			return list;
		}

		/// <summary>
		/// Creates and performs a delete query
		/// </summary>
		/// <param name="conn">Connection to use for the delete</param>
		/// <param name="trans">Transaction to use for the delete</param>
		/// <param name="table">Name of the table to delete</param>
		/// <param name="whereQuery">Columns to base the delete query on</param>
		public static void Delete(OleDbConnection conn,OleDbTransaction trans,string table, params WhereCol[]whereQuery)
		{
			DSShared.DB.DBTableType thisTable = DSShared.DB.DBTableTypeCache.Instance.CacheTable(table);

			OleDbCommand comm = new OleDbCommand("DELETE FROM "+thisTable.TableName+" WHERE ",conn,trans);

			if(whereQuery!=null && whereQuery.Length>0)
			{
				bool flag=false;
				int i=0;
				foreach(WhereCol dbc in whereQuery)
				{
					if(flag)
						comm.CommandText+=" AND ";
					else
						flag=true;

					string paramID = paramPrefix+(i++);

					comm.CommandText+=dbc.Column+"="+paramID;
					comm.Parameters.AddWithValue(paramID, dbc.Data);
					//comm.Parameters.Add(paramID,dbc.Data);
				}
			}

			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// Once all the rows of a table have been retrieved with GetAll(), this method gets called
		/// to allow the table to perform any additional information retrieval necessary
		/// This is the time to link up fields in other tables that have been cached
		/// </summary>
		/// <param name="conn">Connection that was used for the GetAll()</param>
		/// <param name="trans">Transaction used for the GetAll()</param>
		protected virtual void FinishGet(OleDbConnection conn, OleDbTransaction trans){}

		/// <summary>
		/// It is up to the implementing class to determine how this table is saved,
		/// either with an Insert() or an Update()
		/// </summary>
		/// <param name="conn">Connection to use for the save</param>
		/// <param name="trans">Transaction to use for the save</param>
		public virtual void Save(OleDbConnection conn,OleDbTransaction trans){}

		/// <summary>
		/// It is up to the implementing class to determine how this table compares to other tables
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public abstract int CompareTo(object other);

		/// <summary>
		/// It is up to the implementing class to determine how this table equals other tables
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public override abstract bool Equals(object other);

		/// <summary>
		/// It is up to the implementing class to determine the hash code of this table
		/// </summary>
		/// <returns></returns>
		public override abstract int GetHashCode();

		/// <summary>
		/// Gets the name of the table this object represents
		/// </summary>
		public string TableName
		{
			get{return myType.TableName;}
		}

		/// <summary>
		/// Prints table information to the Console
		/// </summary>
		public void PrintTableInfo()
		{
			Console.WriteLine("Table: "+myType.TableName);
			foreach(PropertyInfo pi in myType.Columns)
				Console.WriteLine("Property: "+pi.Name+" -> col "+myType[pi].ColumnName+(myType.AutoNumber==pi?" autonumber":""));
		}

		/// <summary>
		/// Opens a database connection and transaction and calls Save(Access_DBInterface.Instance.Connection,trans)
		/// </summary>
		public void Save()
		{
			Access_DBInterface.Instance.Connection.Open();
			OleDbTransaction trans = Access_DBInterface.Instance.Connection.BeginTransaction();
			Save(Access_DBInterface.Instance.Connection,trans);
			trans.Commit();
			Access_DBInterface.Instance.Connection.Close();
		}	
	
		/// <summary>
		/// Opens a connection to the database and calls Delete(OleDbConnection conn,OleDbTransaction trans)
		/// </summary>
		public void Delete()
		{
			Access_DBInterface.Instance.Connection.Open();
			OleDbTransaction trans = Access_DBInterface.Instance.Connection.BeginTransaction();
			Delete(Access_DBInterface.Instance.Connection,trans);
			trans.Commit();
			Access_DBInterface.Instance.Connection.Close();
		}

		/// <summary>
		/// Deletes a row based on the AutoNumber property of the internal DBTableType. An exception will
		/// be thrown if your class does not have an autonumber attribute set and this method is not overridden
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="trans"></param>
		public virtual void Delete(OleDbConnection conn,OleDbTransaction trans)
		{
			if(myType.AutoNumber!=null)
				Delete(conn,trans,myType[myType.AutoNumber].ColumnName);
			else
				throw new Exception("Calling delete on a table with no autonumber");
		}

		/// <summary>
		/// Updates a row based on the AutoNumber property
		/// </summary>
		/// <param name="conn">Connection to use</param>
		/// <param name="trans">Transaction to use</param>
		public void Update(OleDbConnection conn,OleDbTransaction trans)
		{
			if(myType.AutoNumber!=null)
				Update(conn,trans,myType[myType.AutoNumber].ColumnName);
			else
				throw new Exception("Calling update on a table with no autonumber");
		}

		/// <summary>
		/// Generic update method. Will update a row based on the column strings passed in whereCols2
		/// </summary>
		/// <param name="conn">Connection to use</param>
		/// <param name="trans">Transaction to use</param>
		/// <param name="whereCols">Columns to limit the updates to</param>
		public virtual void Update(OleDbConnection conn,OleDbTransaction trans,params string[]whereCols)
		{
			//figure out which propertyInfo objects we wont be updating
			Hashtable updateHash = new Hashtable();
			foreach(string uc in whereCols)
			{
				foreach(PropertyInfo pi in myType.Columns)
				{
					DSShared.DB.DBColumnAttribute attr = myType[pi];
					if(uc==attr.ColumnName)
					{
						updateHash[pi]=true;
						break;
					}
				}
			}

			string commStr = "UPDATE "+myType.TableName+" SET ";
			OleDbCommand comm = new OleDbCommand();
			comm.Connection=conn;
			comm.Transaction=trans;

			bool flag=false;

			int i=0;
			//build SET clause
			foreach(PropertyInfo pi in myType.Columns)
			{
				if(updateHash[pi]!=null) //if true, this property is part of the WHERE clause
					continue;

				object val = pi.GetValue(this,null);
				DSShared.DB.DBColumnAttribute attr = myType[pi];

				if(!attr.IsAutoNumber && val!=null)
				{
					if(flag)
						commStr+=",";
					else
						flag=true;

					commStr+=attr.ColumnName;
					string paramID = paramPrefix+(i++);
					commStr+="="+paramID;

					if(val is DateTime)
						comm.Parameters.AddWithValue(paramID, val.ToString());
						//comm.Parameters.Add(paramID,val.ToString());
					else
						comm.Parameters.AddWithValue(paramID, val);
						//comm.Parameters.Add(paramID,val);
				}
			}

			commStr +=" WHERE ";
			flag=false;

			foreach(PropertyInfo pi in updateHash.Keys)
			{
				object val = pi.GetValue(this,null);
				DSShared.DB.DBColumnAttribute attr = myType[pi];

				if(val!=null)
				{
					if(flag)
						commStr+=",";
					else
						flag=true;

					commStr+=attr.ColumnName;
					string paramID = paramPrefix+(i++);
					commStr+="="+paramID;

					if (val is DateTime)
						comm.Parameters.AddWithValue(paramID, val.ToString());
					//comm.Parameters.Add(paramID,val.ToString());
					else
						comm.Parameters.AddWithValue(paramID, val);
					//comm.Parameters.Add(paramID,val);
				}
			}

			comm.CommandText=commStr;
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// Performs an insert operation. It is up to the user to ensure that this operation will not fail
		/// (ie: calling insert twice on the same object)
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="trans"></param>
		public virtual void Insert(OleDbConnection conn,OleDbTransaction trans)
		{
			string commStr = string.Format("INSERT INTO {0} (",myType.TableName);
			string valStr = "VALUES(";

			OleDbCommand comm = new OleDbCommand();
			comm.Connection=conn;
			comm.Transaction=trans;

			bool flag=false;

			int i=0;
			foreach(PropertyInfo pi in myType.Columns)
			{
				object val = pi.GetValue(this,null);
				DSShared.DB.DBColumnAttribute attr = myType[pi];

				if(!attr.IsAutoNumber && val!=null)
				{
					if(flag)
					{
						commStr+=",";
						valStr+=",";
					}
					else
						flag=true;

					commStr+=attr.ColumnName;
					string paramID = paramPrefix+(i++);
					valStr+=paramID;

					if (val is DateTime)
						comm.Parameters.AddWithValue(paramID, val.ToString());
					//comm.Parameters.Add(paramID,val.ToString());
					else
						comm.Parameters.AddWithValue(paramID, val);
					//comm.Parameters.Add(paramID,val);

					//Console.WriteLine("Insert: "+columns[c].Name+":"+columns[c].Value+" type: "+columns[c].Value.GetType());
				}
			}

			comm.CommandText=commStr+") "+valStr+")";
			comm.ExecuteNonQuery();

			if(myType.AutoNumber!=null)
			{
				comm = new OleDbCommand("SELECT @@IDENTITY",conn,trans);
				myType.AutoNumber.SetValue(this,Convert.ToInt32(comm.ExecuteScalar()),null);
			}
		}
	}
}
