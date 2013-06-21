using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace DSShared.DB
{
	/// <summary>
	/// Singleton class to keep track of open database tables. This is done as an optimization for when we
	/// keep an entire table in memory - usually ones that dont change much (flags) so that we dont have 
	/// to continually pull the same table information from the access database file. 
	/// </summary>
	public class DBTableTypeCache
	{
		private static DBTableTypeCache cache;
		private Dictionary<string, DBTableType> typeHash;
		private Dictionary<string, ArrayList> listHash;
		private Dictionary<string,Hashtable> hashHash;
		private Dictionary<string,Type> nameType;

		private DBTableTypeCache()
		{
			typeHash = new Dictionary<string, DBTableType>();
			listHash = new Dictionary<string, ArrayList>();
			hashHash = new Dictionary<string, Hashtable>();
			nameType = new Dictionary<string, Type>();
		}

		/// <summary>
		/// Sets up a database table with an object type for future cache'ing
		/// </summary>
		/// <param name="table">Name of the table that this type will be accessed by</param>
		/// <param name="type">Object type that will represent this table</param>
		public void RegisterNameType(string table, Type type)
		{
			nameType[table]=type;
		}

		/// <summary>
		/// Returns the type that has been registered with the table name
		/// </summary>
		/// <param name="table">Name of the table</param>
		/// <returns>Type registered previously using <see cref="M:DSShared.DB.DBTableTypeCache.RegisterNameType"/></returns>
		public Type GetTableType(string table)
		{
			return nameType[table];
		}

		/// <summary>
		/// Caches a table using the table parameter as a key
		/// </summary>
		/// <param name="table">Name of the table</param>
		/// <param name="hash">Hashtable of row information</param>
		public void CacheHash(string table, Hashtable hash)
		{
			hashHash[table]=hash;
		}

		/// <summary>
		/// Caches an arraylist using the table parameter as a key
		/// </summary>
		/// <param name="table">Name of the table</param>
		/// <param name="list">list of row information</param>
		public void CacheList(string table, ArrayList list)
		{
			listHash[table]=list;
		}

		/// <summary>
		/// A cached database table stored as a hashtable. The keys are determined by autonumber properties
		/// that are tagged as such by the objects stored in the table
		/// </summary>
		/// <param name="table">Name of the table</param>
		/// <returns>null if the table was not cached earlier</returns>
		public Hashtable GetHash(string table)
		{
			return hashHash[table];
		}

		/// <summary>
		/// A cached database table stored as an array list
		/// </summary>
		/// <param name="table">Name of the table</param>
		/// <returns>null if the table was not cached earlier</returns>
		public ArrayList GetList(string table)
		{
			return listHash[table];
		}

		/// <summary>
		/// Caches a table given its table name. The table must have been registered before calling this or an exception will be raised
		/// If the object associated with the table name has not been created yet, it will be created and stored for future use
		/// </summary>
		/// <param name="table">Name of the table you wish to access</param>
		/// <returns>The object representing the table in the database</returns>
		public DBTableType CacheTable(string table)
		{
			if(nameType[table]==null)
				throw new Exception("Type for table: "+table+" has not been registered yet");

			if(typeHash[table]==null)
				typeHash[table] = new DBTableType(table,nameType[table]);
			return typeHash[table];
		}

		/// <summary>
		/// Caches a table given its object
		/// </summary>
		/// <param name="dbType">DBTableType to cache</param>
		public void CacheTable(DBTableType dbType)
		{
			typeHash[dbType.TableName]=dbType;
		}

		/// <summary>
		/// returns a DBTableType object based on the name of the table you have previously cached
		/// </summary>
		/// <param name="table">name of the table</param>
		/// <returns>A DBTableType object that was previously cached</returns>
		public DBTableType GetType(string table)
		{
			return typeHash[table];
		}

		/// <summary>
		/// Singleton accessor for this class
		/// </summary>
		public static DBTableTypeCache Instance
		{
			get
			{
				if(cache==null)
					cache = new DBTableTypeCache();
				return cache;
			}
		}
	}
}
