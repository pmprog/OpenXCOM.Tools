using System;
using System.Reflection;
//using DSShared.Exceptions;

namespace DSShared.Lists
{
	/// <summary>
	/// Class that a CustomList uses to figure out the value of a particular row+column
	/// </summary>
	public class ObjProperty
	{
		private PropertyInfo property;
		private ObjProperty nested;
		private object[] propertyIndex;
		private EditStrType editType = EditStrType.None;
		private EditStrDelegate editFunc=null;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ObjProperty"/> class.
		/// </summary>
		/// <param name="property">The propertyInfo object that will reflect on objects later on to display information with</param>
		public ObjProperty(PropertyInfo property):this(property,null,null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ObjProperty"/> class.
		/// </summary>
		/// <param name="property">The propertyInfo object that will reflect on objects later on to display information with</param>
		/// <param name="nested">If the information required resides in a property's property, this parameter represents that information</param>
		public ObjProperty(PropertyInfo property, ObjProperty nested):this(property,null,nested)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ObjProperty"/> class.
		/// </summary>
		/// <param name="property">The propertyInfo object that will reflect on objects later on to display information with</param>
		/// <param name="propertyIndex">An array of index parameters if the property parameter represents an indexex property</param>
		public ObjProperty(PropertyInfo property,object[] propertyIndex):this(property,propertyIndex,null)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ObjProperty"/> class.
		/// </summary>
		/// <param name="property">The propertyInfo object that will reflect on objects later on to display information with</param>
		/// <param name="propertyIndex">An array of index parameters if the property parameter represents an indexex property</param>
		/// <param name="nested">If the information required resides in a property's property, this parameter represents that information</param>
		public ObjProperty(PropertyInfo property,object[] propertyIndex, ObjProperty nested)
		{
			this.property=property;
			this.nested=nested;
			this.propertyIndex=propertyIndex;

			if(property!=null)
			{
				object[] attr = property.GetCustomAttributes(typeof(EditStrAttribute),true);

				if(attr!=null && attr.Length>0)
				{
					editType = ((EditStrAttribute)attr[0]).EditType;
				}
			}
		}

		/// <summary>
		/// Gets or sets the key function. This is called when a key is pressed on a selected row
		/// </summary>
		/// <value>The key function.</value>
		public EditStrDelegate KeyFunction
		{
			get{return editFunc;}
			set{editFunc=value;}
		}

		/// <summary>
		/// Gets or sets the type of the edit.
		/// </summary>
		/// <value>The type of the edit.</value>
		public EditStrType EditType
		{
			get{return editType;}
			set{editType=value;}
		}

		/// <summary>
		/// Sets the value of the provided object's property to the provided value
		/// </summary>
		/// <param name="obj">The obj.</param>
		/// <param name="val">The val.</param>
		public void SetValue(object obj,object val)
		{
			if(nested==null)
				property.SetValue(obj,val,propertyIndex);
			else
				nested.SetValue(property.GetValue(obj,propertyIndex),val);
		}

		/// <summary>
		/// Gets the value of the provided object's property.
		/// </summary>
		/// <param name="o">The object</param>
		/// <returns></returns>
		public object Value(object o)
		{
			if(property==null)
				return "<no property>";

			if(o!=null)
			{
				object obj = property.GetValue(o,propertyIndex);
				if(obj!=null)
				{
					if(nested==null)
						return obj;
					else
						return nested.Value(obj);
				}
				return "";
			}

			throw new Exception("value is null");
			//throw new ObjPropertyNullValueException();
		}

		/// <summary>
		/// Test for equality between two objects. Test is based on the property's hashcode
		/// </summary>
		/// <param name="other">The other object to test with</param>
		/// <returns></returns>
		public override bool Equals(object other)
		{
			if(other is ObjProperty)
				return GetHashCode()==other.GetHashCode();
			return false;
		}

		/// <summary>
		/// returns the constructor parameter: property.GetHashCode() or 0 if null
		/// </summary>
		/// <returns>
		/// A hash code for the current <see cref="T:System.Object"></see>.
		/// </returns>
		public override int GetHashCode()
		{
			if(property!=null)
				return property.GetHashCode();
			return 0;
		}
	}
}