using System;

namespace DSShared.DB
{
	/// <summary>
	/// Class to specify columns in a WHERE clause of a sql query
	/// </summary>
	public class WhereCol
	{
		/// <summary>
		/// Table column identifier
		/// </summary>
		public string Column;

		/// <summary>
		/// Data to check
		/// </summary>
		public object Data;

		/// <summary>
		/// constructor, sets the public properties
		/// </summary>
		/// <param name="column"></param>
		/// <param name="data"></param>
		public WhereCol(string column,object data)
		{
			this.Column=column;
			this.Data=data;
		}
	}
}