using System;

namespace DSShared.DB
{
	/// <summary>
	/// Tag properties with this attribute to enable the automatic retrieval and updating of information 
	/// to that column
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class DBColumnAttribute:System.Attribute
	{
		private string col;
		private bool autoNum;

		/// <summary>
		/// Constructor, calls this(columnName,false)
		/// </summary>
		/// <param name="columnName">Name of the column in the table this property corresponds to</param>
		public DBColumnAttribute(string columnName):this(columnName,false){}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="columnName">Name of the column in the table this property corresponds to</param>
		/// <param name="autoNumber">If true, this column is tagged as an autoNumber and will enable easy updating and deletion</param>
		public DBColumnAttribute(string columnName,bool autoNumber)
		{
			this.col=columnName;
			this.autoNum=autoNumber;
		}

		/// <summary>
		/// Gets a value indicating if this column is an autoNumber
		/// </summary>
		public bool IsAutoNumber
		{
			get{return autoNum;}
		}

		/// <summary>
		/// Name of the column in the database this property represents
		/// </summary>
		public string ColumnName
		{
			get{return col;}
		}
	}
}
