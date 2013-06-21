using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace DSShared.DB
{
	/// <summary>
	/// Class that wraps around a Type object that automates the retrieval and updating of rows in a database
	/// </summary>
	public class DBTableType
	{
		private Type type;
		private string table;
		private Dictionary<PropertyInfo, DBColumnAttribute> myColumns;
		private PropertyInfo autoNumber;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="table">Name of the table in the database this object represents</param>
		/// <param name="type">Underlying type that operates directly on the database table</param>
		public DBTableType(string table, Type type)
		{
			this.type = type;
			this.table = table;
			myColumns = new Dictionary<PropertyInfo, DBColumnAttribute>();

			foreach (PropertyInfo pi in type.GetProperties())
			{
				object[] attrs = pi.GetCustomAttributes(typeof(DBColumnAttribute), true);
				if (attrs.Length > 0)
				{
					myColumns[pi] = (DBColumnAttribute)attrs[0];
					if (myColumns[pi].IsAutoNumber)
						autoNumber = pi;
				}
			}
		}

		/// <summary>
		/// Name of the table this object represents in the database
		/// </summary>
		public string TableName
		{
			get { return table; }
		}

		/// <summary>
		/// ICollection of PropertyInfo objects which correspond to get/set properties representing column fields
		/// </summary>
		public System.Collections.ICollection Columns
		{
			get { return myColumns.Keys; }
		}

		/// <summary>
		/// Gets the DBColumnAttribute attached to the specified PropertyInfo
		/// </summary>
		/// <param name="pi"></param>
		/// <returns></returns>
		public DBColumnAttribute this[PropertyInfo pi]
		{
			get { return myColumns[pi]; }
		}

		/// <summary>
		/// Gets the PropertyInfo object that corresponds to the autonumber field for this type
		/// </summary>
		public PropertyInfo AutoNumber
		{
			get { return autoNumber; }
		}
	}
}
