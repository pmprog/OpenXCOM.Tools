using System;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace DSShared.Lists
{
	/// <summary>
	/// Delegate for use in the CustomListColumn.WidthChanged and LeftChanged events
	/// </summary>
	/// <param name="columnChanged">Column that is changed</param>
	/// <param name="changeAmount">How much it has changed</param>
	public delegate void CustomListColumChangedDelegate(CustomListColumn columnChanged,int changeAmount);

	/// <summary>
	/// Delegate for use when a column is clicked on
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	public delegate void ColClickDelegate(object sender, RowClickEventArgs e);

	/// <summary>
	/// Delegate for use when a key is pressed on a row in this column
	/// </summary>
	/// <param name="row"></param>
	/// <param name="col"></param>
	/// <param name="e"></param>
	public delegate void KeyPressDelegate(ObjRow row, CustomListColumn col, KeyPressEventArgs e);

	/// <summary>
	/// Class representing a column in a CustomList control
	/// </summary>
	public class CustomListColumn
	{
		private ObjProperty colProperty;
		private string title;
		private int width=50,left,index;

		/// <summary>
		/// Minimum width of a column
		/// </summary>
		public static int MinWidth=20;

		/// <summary>
		/// Fired when a column's width changes
		/// </summary>
		public event CustomListColumChangedDelegate WidthChanged;

		/// <summary>
		/// Fired when a column's left parameter changes
		/// </summary>
		public event CustomListColumChangedDelegate LeftChanged;

		/// <summary>
		/// Fired when a row has been clicked on under this column
		/// </summary>
		public event ColClickDelegate OnClick;

		/// <summary>
		/// Fired when a row gets keyboard events under this column
		/// </summary>
		public event KeyPressDelegate KeyPress;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:CustomListColumn"/> class.
		/// </summary>
		/// <param name="title">Column title</param>
		/// <param name="property">ObjProperty that will reflect on the objects contained in the list</param>
		public CustomListColumn(string title, ObjProperty property)
		{
			this.title=title;
			this.colProperty=property;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:CustomListColumn"/> class.
		/// </summary>
		/// <param name="title">Column title</param>
		/// <param name="property">PropertyInfo that will reflect on the objects contained in the list</param>
		public CustomListColumn(string title, System.Reflection.PropertyInfo property)
		{
			this.title=title;
			this.colProperty = new ObjProperty(property);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:CustomListColumn"/> class.
		/// </summary>
		/// <param name="title">Column title</param>
		public CustomListColumn(string title):this(title,(ObjProperty)null){}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:CustomListColumn"/> class.
		/// </summary>
		public CustomListColumn():this("",(ObjProperty)null){}

		/// <summary>
		/// Equality test. Based on GetHashCode() between objects of this type
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public override bool Equals(object other)
		{
			if(other is CustomListColumn)
				return GetHashCode()==other.GetHashCode();
			return false;
		}

		/// <summary>
		/// Raises the KeyPress event for the specified row
		/// </summary>
		/// <param name="row">Row to raise event with</param>
		/// <param name="e">Args</param>
		public void FireKeyPress(ObjRow row, KeyPressEventArgs e)
		{
			if(KeyPress!=null)
				KeyPress(row,this,e);
		}

		/// <summary>
		/// Gets the hash code of the title of this column
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return title.GetHashCode();
		}

		/// <summary>
		/// Raises the OnClick event for the specified row
		/// </summary>
		/// <param name="row">Row to raise event with</param>
		public void FireClick(ObjRow row)
		{
			if(OnClick!=null)
				OnClick(this,new RowClickEventArgs(row,this));
		}

		/// <summary>
		/// Resizes the title width based on the size of the input font
		/// </summary>
		/// <param name="font"></param>
		public void ResizeTitle(System.Drawing.Font font)
		{
			Width = Math.Max(MinWidth,2+(int)System.Drawing.Graphics.FromImage(new System.Drawing.Bitmap(1,1)).MeasureString(title,font).Width);
		}


		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public string Title
		{
			get{return title;}
			set{title=value;}
		}

		/// <summary>
		/// Gets or sets the index. This value is the position in the column list
		/// </summary>
		/// <value>The index.</value>
		public int Index
		{
			get{return index;}
			set{index=value;}
		}

		/// <summary>
		/// Gets or sets the property this column holds
		/// </summary>
		/// <value>The property.</value>
		public ObjProperty Property
		{
			get{return colProperty;}
			set{colProperty=value;}
		}

		/// <summary>
		/// Gets or sets the left in screen coordinates
		/// </summary>
		/// <value>The left.</value>
		public int Left
		{
			get{return left;}
			set
			{
				int diff = left-value;
				left=value;
				if(LeftChanged!=null)
					LeftChanged(this,diff);
			}
		}

		/// <summary>
		/// Gets or sets the width of the column in screen coordinates
		/// </summary>
		/// <value>The width.</value>
		public int Width
		{
			get{return width;}
			set
			{
				if(value<MinWidth)
					return;

				int diff = width-value;
				width=value;
				if(WidthChanged!=null)
					WidthChanged(this,diff);
			}
		}
	}

	/// <summary>
	/// Args class that holds a row and a column. 
	/// </summary>
	public class RowClickEventArgs:EventArgs
	{
		private ObjRow row;
		private CustomListColumn col;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:RowClickEventArgs"/> class.
		/// </summary>
		/// <param name="row">The row that was clicked on</param>
		/// <param name="col">The column that was clicked under</param>
		public RowClickEventArgs(ObjRow row, CustomListColumn col)
		{
			this.row=row;
			this.col=col;
		}

		/// <summary>
		/// Gets the row.
		/// </summary>
		/// <value>The row.</value>
		public ObjRow Row
		{
			get{return row;}
		}

		/// <summary>
		/// Gets the column.
		/// </summary>
		/// <value>The column.</value>
		public CustomListColumn Column
		{
			get{return col;}
		}
	}
}
