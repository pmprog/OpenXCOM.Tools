using System;

namespace DSShared
{
	/// <summary>
	/// A Lazy Class for storing a string and an int. CompareTo operates on the string held
	/// </summary>
	public class StrInt:IComparable
	{
		/// <summary>
		/// string parameter
		/// </summary>
		public string str;

		/// <summary>
		/// int parameter
		/// </summary>
		public int i;

		/// <summary>
		/// constructor
		/// </summary>
		/// <param name="str"></param>
		/// <param name="i"></param>
		public StrInt(string str, int i)
		{
			this.str=str;
			this.i=i;
		}

		/// <summary>
		/// returns str
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return str;
		}

		/// <summary>
		/// compares the ToString value
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public int CompareTo(object other)
		{
			return str.CompareTo(other.ToString());
		}

		/// <summary>
		/// compares the ToString value
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public override bool Equals(object other)
		{
			return str.Equals(other.ToString());
		}

		/// <summary>
		/// returns the GetHashCode value of str
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return str.GetHashCode();
		}
	}
}
