//#define USING_MYSQL
#if USING_MYSQL
using System;
using System.Reflection;
using System.Collections;
using MySql.Data.MySqlClient;

namespace DSShared.DB_MySql
{
	public delegate void FinishGetAllDelegate(string table,ArrayList rows);
	public abstract class MySQL_DBTable:IComparable
	{
		protected DSShared.DB.DBTableType myType;
		private static string paramPrefix="IFU";

		public static event FinishGetAllDelegate FinishGetAll;

		public MySQL_DBTable(string tableName)
		{
			myType = DSShared.DB.DBTableTypeCache.Instance.CacheTable(tableName);
		}

		/// <summary>
		/// Retrieves all rows from a single table according to the columns specified
		/// </summary>
		/// <param name="table">table name</param>
		/// <param name="cache">if the table does not change frequently, you should set this to true</param>
		/// <param name="whereQuery">list of columns to query upon. Be careful when used with cache'ing</param>
		/// <returns></returns>
		public static Hashtable GetAllHash(string table, bool cache, params WhereCol[] whereQuery)
		{
			if(cache && DSShared.DB.DBTableTypeCache.Instance.GetHash(table)!=null)
				return DSShared.DB.DBTableTypeCache.Instance.GetHash(table);

			MySQL_DBInterface.Instance.Connection.Open();
			MySqlTransaction trans = MySQL_DBInterface.Instance.Connection.BeginTransaction();
			Hashtable hash = GetAllHash(MySQL_DBInterface.Instance.Connection,trans,table,cache,whereQuery);
			trans.Commit();
			MySQL_DBInterface.Instance.Connection.Close();

			return hash;
		}

		public static Hashtable GetAllHash(MySqlConnection conn,MySqlTransaction trans,string table,bool cache,params WhereCol[] whereQuery)
		{
			if(cache && DSShared.DB.DBTableTypeCache.Instance.GetHash(table)!=null)
				return DSShared.DB.DBTableTypeCache.Instance.GetHash(table);

			ArrayList list = GetAll(conn,trans,table,cache,whereQuery);

			DSShared.DB.DBTableType thisType = DSShared.DB.DBTableTypeCache.Instance.GetType(table);

			if(thisType.AutoNumber!=null)
			{
				PropertyInfo pi = thisType.AutoNumber;
				Hashtable hash = new Hashtable();
				foreach(MySQL_DBTable dt in list)
					hash[pi.GetGetMethod().Invoke(dt,null)]=dt;

				if(cache)
					DSShared.DB.DBTableTypeCache.Instance.CacheHash(table,hash);

				return hash;
			}

			throw new Exception("Call GetAllHash on table: "+table+" invalid since it does not have an autonumber defined");
		}

		public static ArrayList GetAll(string table, bool cache,params WhereCol[] whereQuery)
		{
			if(cache && DSShared.DB.DBTableTypeCache.Instance.GetList(table)!=null)
				return DSShared.DB.DBTableTypeCache.Instance.GetList(table);

			MySQL_DBInterface.Instance.Connection.Open();
			MySqlTransaction trans = MySQL_DBInterface.Instance.Connection.BeginTransaction();
			ArrayList list = GetAll(MySQL_DBInterface.Instance.Connection,trans,table,cache,whereQuery);
			trans.Commit();
			MySQL_DBInterface.Instance.Connection.Close();

			return list;
		}

		public static ArrayList GetAll(MySqlConnection conn,MySqlTransaction trans,string table,bool cache,params WhereCol[] whereQuery)
		{
			DSShared.DB.DBTableType thisTable = DSShared.DB.DBTableTypeCache.Instance.CacheTable(table);

			MySqlCommand comm = new MySqlCommand("SELECT * FROM "+thisTable.TableName,conn,trans);

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

					string paramID = "?"+paramPrefix+(i++);

					comm.CommandText+=dbc.Column+"="+paramID;
					comm.Parameters.Add(paramID,dbc.Data);
				}
			}
			MySqlDataReader res = comm.ExecuteReader();
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

			foreach(MySQL_DBTable dbt in list)
				dbt.FinishGet(conn,trans);

			if(cache)
				DSShared.DB.DBTableTypeCache.Instance.CacheList(table,list);

			if(FinishGetAll!=null)
				FinishGetAll(table,list);

			return list;
		}

		protected virtual void FinishGet(MySqlConnection conn, MySqlTransaction trans){}

		public void PrintTableInfo()
		{
			Console.WriteLine("Table: "+myType.TableName);
			foreach(PropertyInfo pi in myType.Columns)
				Console.WriteLine("Property: "+pi.Name+" -> col "+myType[pi].ColumnName+(myType.AutoNumber==pi?" autonumber":""));
		}

		public string TableName
		{
			get{return myType.TableName;}
		}

		public void Save()
		{
			MySQL_DBInterface.Instance.Connection.Open();
			MySqlTransaction trans = MySQL_DBInterface.Instance.Connection.BeginTransaction();
			Save(MySQL_DBInterface.Instance.Connection,trans);
			trans.Commit();
			MySQL_DBInterface.Instance.Connection.Close();
		}

		public virtual void Save(MySqlConnection conn,MySqlTransaction trans){}

		public void Delete(MySqlConnection conn,MySqlTransaction trans)
		{
			if(myType.AutoNumber!=null)
				Delete(conn,trans,myType[myType.AutoNumber].ColumnName);
			else
				throw new Exception("Calling delete on a table with no autonumber");
		}

		public void Update(MySqlConnection conn,MySqlTransaction trans)
		{
			if(myType.AutoNumber!=null)
				Update(conn,trans,myType[myType.AutoNumber].ColumnName);
			else
				throw new Exception("Calling update on a table with no autonumber");
		}

		public virtual void Delete(MySqlConnection conn,MySqlTransaction trans,params string[]delCols){}

		public virtual void Update(MySqlConnection conn,MySqlTransaction trans,params string[]whereCols2)
		{
			//figure out which propertyInfo objects we wont be updating
			Hashtable updateHash = new Hashtable();
			foreach(string uc in whereCols2)
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
			MySqlCommand comm = new MySqlCommand();
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
					string paramID = "?"+paramPrefix+(i++);
					commStr+="="+paramID;

					if(val is DateTime)
						comm.Parameters.Add(paramID,val.ToString());
					else
						comm.Parameters.Add(paramID,val);
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
					string paramID = "?"+paramPrefix+(i++);
					commStr+="="+paramID;

					if(val is DateTime)
						comm.Parameters.Add(paramID,val.ToString());
					else
						comm.Parameters.Add(paramID,val);
				}
			}

			comm.CommandText=commStr;
			comm.ExecuteNonQuery();
		}

		//command parameters in mysql start with ?
		public virtual void Insert(MySqlConnection conn,MySqlTransaction trans)
		{
			string commStr = string.Format("INSERT INTO {0} (",myType.TableName);
			string valStr = "VALUES(";

			MySqlCommand comm = new MySqlCommand();
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
					string paramID = "?"+paramPrefix+(i++);
					valStr+=paramID;

					if(val is DateTime)
						comm.Parameters.Add(paramID,val.ToString());
					else
						comm.Parameters.Add(paramID,val);

					//Console.WriteLine("Insert: "+columns[c].Name+":"+columns[c].Value+" type: "+columns[c].Value.GetType());
				}
			}

			comm.CommandText=commStr+") "+valStr+")";
			comm.ExecuteNonQuery();

			if(myType.AutoNumber!=null)
			{
				comm = new MySqlCommand("SELECT @@IDENTITY",conn,trans);
				myType.AutoNumber.SetValue(this,Convert.ToInt32(comm.ExecuteScalar()),null);
			}
		}

		public abstract int CompareTo(object other);
		public override abstract bool Equals(object other);
		public override abstract int GetHashCode();
	}

	public struct WhereCol
	{
		public string Column;
		public object Data;

		public WhereCol(string column,object data)
		{
			this.Column=column;
			this.Data=data;
		}
	}
}
#endif
