using System;

namespace DSShared.Lists
{
	/// <summary>
	/// Specifies the kind of editing that a property is able to have
	/// </summary>
	public enum EditStrType
	{
		/// <summary>
		/// Property takes any alpha-numeric string combination
		/// </summary>
		String,
		/// <summary>
		/// Property takes integers that satisfy int.Parse()
		/// </summary>
		Int,
		/// <summary>
		/// Property takes floats that satisfy double.Parse()
		/// </summary>
		Float,
		/// <summary>
		/// Property is not editable with the keyboard
		/// </summary>
		None,
		/// <summary>
		/// Property will be editable based on the function provided in ObjProperty.KeyFunction
		/// </summary>
		Custom
	};

	/// <summary>
	/// Delegate that gets called when a keyboard button is pressed after a row has been clicked on
	/// </summary>
	/// <param name="row"></param>
	/// <param name="col"></param>
	/// <param name="e"></param>
	public delegate void EditStrDelegate(ObjRow row, CustomListColumn col, System.Windows.Forms.KeyPressEventArgs e);

	/// <summary>
	/// Tags a property as being editable in some fashion when used in a CustomList
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class EditStrAttribute:Attribute
	{
		private EditStrType editType;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:EditStrAttribute"/> class.
		/// </summary>
		/// <param name="editType">Type of the edit.</param>
		public EditStrAttribute(EditStrType editType)
		{
			this.editType=editType;
		}

		/// <summary>
		/// Gets the type of the edit.
		/// </summary>
		/// <value>The type of the edit.</value>
		public EditStrType EditType
		{
			get{return editType;}
		}
	}
}
