using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.ComponentModel;
using Microsoft.Win32;
using DSShared.Windows;
using System.Reflection;

namespace DSShared.Lists
{
	/// <summary>
	/// Delegate for the CustomList.RowClick event
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="clicked"></param>
	public delegate void RowClickEventHandler(object sender, ObjRow clicked);

	/// <summary>
	/// Delegate for the CustomList.RowTextChange event
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="current"></param>
	public delegate void RowTextChangeEventHandler(object sender, ObjRow current);

	/// <summary>
	/// A custom list view control allowing you to specify a list of columns that retrieve their row information via reflection
	/// </summary>
	public class CustomList:Control
	{
		private CustomListColumnCollection columns;
		private List<ObjRow> items;

		/// <summary>
		/// The currently selected ObjRow
		/// </summary>
		protected ObjRow selected;

		/// <summary>
		/// The last ObjRow clicked on
		/// </summary>
		protected ObjRow clicked;

		private int startY=0;
		private int yOffset=0;
		private int selRow=-1;
		private VScrollBar vert;
		private CustomListColumn overCol=null;
		private DSShared.Windows.RegistryInfo ri;
		private Type rowType;
		private string name="";

		/// <summary>
		/// Occurs when a row is clicked
		/// </summary>
		public event RowClickEventHandler RowClick;

		/// <summary>
		/// Occurs when the text is changed in a row
		/// </summary>
		public event RowTextChangeEventHandler RowTextChange;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:CustomList"/> class.
		/// </summary>
		public CustomList()
		{
			columns = new CustomListColumnCollection();
			columns.OffX=1;
			columns.OffY=1;
			columns.Font=Font;

			columns.RefreshEvent+=new RefreshDelegate(Refresh);
			columns.RowMoveOver+=new RowMoveDelegate(mouseOverRows);
			columns.RowClicked+=new MouseEventHandler(rowClicked);
			columns.Parent=this;

			items = new List<ObjRow>();			

			SetStyle(ControlStyles.DoubleBuffer|ControlStyles.AllPaintingInWmPaint|ControlStyles.UserPaint,true);		

			startY=columns.HeaderHeight;

			rowType = typeof(ObjRow);
		}

		/// <summary>
		/// Gets or sets the name of the control.
		/// </summary>
		/// <value></value>
		/// <returns>The name of the control. The default is an empty string ("").</returns>
		public new string Name
		{
			get{return name;}
			set{name=value;}
		}

		/// <summary>
		/// Gets or sets the type of the row.
		/// </summary>
		/// <value>The type of the row.</value>
		[Browsable(false)]
		[DefaultValue(typeof(ObjRow))]
		public Type RowType
		{
			get{return rowType;}
			set{rowType=value;}
		}

		/// <summary>
		/// Gets the items.
		/// </summary>
		/// <value>The items.</value>
		public List<ObjRow> Items
		{
			get{return items;}
		}

		/// <summary>
		/// Gets or sets the registry info object.
		/// </summary>
		/// <value>The registry info.</value>
		[Browsable(false)]
		[DefaultValue(null)]
		public DSShared.Windows.RegistryInfo RegistryInfo
		{
			get{return ri;}
			set
			{
				ri=value;
				ri.Loading+=new RegistrySaveLoadHandler(loading);
				ri.Saving += new RegistrySaveLoadHandler(saving);
			}
		}

		private void loading(object sender, RegistrySaveLoadEventArgs e)
		{
			RegistryKey key = e.OpenKey;
			Graphics g = Graphics.FromHwnd(this.Handle);
			foreach(CustomListColumn cc in columns)
			{
				try{cc.Width = (int)key.GetValue("strLen"+name+cc.Index,cc.Width);}
				catch{cc.Width = (int)g.MeasureString(cc.Title,Font).Width+2;}
			}
		}

		private void saving(object sender, RegistrySaveLoadEventArgs e)
		{
			RegistryKey key = e.OpenKey;
			foreach(CustomListColumn cc in columns)
				key.SetValue("strLen"+name+cc.Index,cc.Width);
		}

		private void rowClicked(object sender, MouseEventArgs e)
		{
			//int overY=(e.Y-(columns.HeaderHeight+yOffset))/(Font.Height+columns.RowSpace*2);
			if(clicked!=null)
				clicked.UnClick();

			if(selected!=null && overCol!=null)
				selected.Click(overCol);

			clicked=selected;

			if(RowClick!=null && clicked!=null)
				RowClick(this,clicked);
		}

		/// <summary>
		/// Gets the preferred height of the control. This is the draw height of all the rows
		/// </summary>
		[Browsable(false)]
		public int PreferredHeight
		{
			get
			{
				return columns.HeaderHeight+((items.Count+1)*RowHeight);
			}
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.Resize"></see> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			columns.Width=Width;
			columns.Height=Height;

			if(vert!=null)
			{
				vert.Value=vert.Minimum;
				startY = columns.HeaderHeight+vert.Value;
				if(PreferredHeight>Height)
				{
					vert.Maximum=(((items.Count+1)*(RowHeight+3)))-Height;
					vert.Enabled=true;
				}
				else
					vert.Enabled=false;
			}
			Refresh();
		}

		/// <summary>
		/// Gets or sets the vertical scroll bar. 
		/// </summary>
		/// <value>The vert scroll.</value>
		[DefaultValue(null)]
		public VScrollBar VertScroll
		{
			get{return vert;}
			set
			{
				if (vert != null)
					vert.Scroll -= new ScrollEventHandler(scroll);

				vert = value; 
				vert.Minimum = 0; 
				vert.Scroll += new ScrollEventHandler(scroll);
			}
		}

		private void scroll(object sender, ScrollEventArgs e)
		{
			yOffset = -vert.Value;
			Refresh();
		}

		private void mouseOverRows(int mouseY,CustomListColumn overCol)
		{
			int overY=(mouseY-(columns.HeaderHeight+yOffset))/(Font.Height+columns.RowSpace*2);
			
			if(selected!=null)
				selected.MouseLeave();

			selected=null;

			if(overCol!=null && overY>=0 && overY<items.Count)
			{
				selRow=overY;
				selected=items[selRow];
				selected.MouseOver(overCol);
			}
			this.overCol=overCol;
			//else its the same, do nothing
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove"></see> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"></see> that contains the event data.</param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			columns.MouseMove(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown"></see> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"></see> that contains the event data.</param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			columns.MouseDown(e);
			Focus();
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseUp"></see> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"></see> that contains the event data.</param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			columns.MouseUp(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.LostFocus"></see> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
		protected override void OnLostFocus(EventArgs e)
		{
			if(clicked!=null)
				clicked.UnClick();

			clicked=null;
			Refresh();
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.Paint"></see> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"></see> that contains the event data.</param>
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			int rowHeight=0;
			for(int i=0;i<items.Count;i++)
			{
				ObjRow row = (ObjRow)items[i];
				if(rowHeight+yOffset+row.Height>=0 && rowHeight+yOffset<Height)
					row.Render(e,yOffset);

				rowHeight+=row.Height;
			}
			columns.Render(e,rowHeight,yOffset);
		}

		//if row.equals(any other row) then we start to have weird behavior

		/// <summary>
		/// Deletes the row.
		/// </summary>
		/// <param name="row">The row to delete.</param>
		public virtual void DeleteRow(ObjRow row)
		{
			Object obj=null;
			for(int i=0;i<items.Count;i++)
			{
				if(obj!=null) //move this row's object to the one above it, move obj to the end to delete
					items[i-1].Object=items[i].Object;
				else if(obj==null && items[i].Equals(row)) //found it, will start moving up on the next iteration
					obj=items[i].Object;
			}

			if(obj!=null)//actually deleted something
			{
				items[items.Count-1].Object=obj;
				items[items.Count-1].RefreshEvent-=new RefreshDelegate(Refresh);

				items.Remove(items[items.Count-1]);
				startY-=Font.Height+columns.RowSpace*2;

				if(refreshOnAdd)
					Refresh();
			}
		}

		private bool refreshOnAdd=true;
		/// <summary>
		/// Gets or sets a value indicating whether to refresh the list when a row is added to the collection
		/// </summary>
		/// <value><c>true</c> if [refresh on add]; otherwise, <c>false</c>.</value>
		[Browsable(false)]
		[DefaultValue(true)]
		public bool RefreshOnAdd
		{
			get{return refreshOnAdd;}
			set{refreshOnAdd=value;}
		}

		/// <summary>
		/// Adds an item to the collection. Creates an ObjRow and calls AddItem(ObjRow row)
		/// </summary>
		/// <param name="o">The item to add</param>
		public virtual void AddItem(object o)
		{
			ConstructorInfo ci = rowType.GetConstructor(new Type[]{typeof(object)});
			ObjRow row = (ObjRow)ci.Invoke(new object[]{o});
			AddItem(row);
		}

		/// <summary>
		/// Adds an ObjRow to the collection
		/// </summary>
		/// <param name="row">The row to add</param>
		public virtual void AddItem(ObjRow row)
		{
			row.Top=startY;
			row.Width=Width;
			//row.Height=RowHeight;
			row.Height+=Font.Height+columns.RowSpace*2;
			row.Columns=columns;
			row.RefreshEvent+=new RefreshDelegate(Refresh);
			row.RowIndex=items.Count;
			items.Add(row);
			startY+=Font.Height+columns.RowSpace*2;

			if(refreshOnAdd)
				Refresh();
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.KeyPress"></see> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs"></see> that contains the event data.</param>
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			if(clicked!=null)
			{
				clicked.KeyPress(e);
				if(RowTextChange!=null)
					RowTextChange(this,clicked);
			}
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.KeyDown"></see> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs"></see> that contains the event data.</param>
		protected override void OnKeyDown(KeyEventArgs e)
		{
			if(clicked!=null)
				clicked.KeyDown(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.KeyUp"></see> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs"></see> that contains the event data.</param>
		protected override void OnKeyUp(KeyEventArgs e)
		{
			if(clicked!=null)
				clicked.KeyUp(e);
		}

		/// <summary>
		/// Gets the height of a row.
		/// </summary>
		/// <value>The height of a row.</value>
		public int RowHeight
		{
			get{return Font.Height+columns.RowSpace*2;}
		}

		/// <summary>
		/// Clears all ObjRows from the internal collection.
		/// </summary>
		public virtual void Clear()
		{
			items = new List<ObjRow>();
			startY=columns.HeaderHeight;
			Refresh();
		}

		/// <summary>
		/// Gets or sets the font of the text displayed by the control.
		/// </summary>
		/// <value></value>
		/// <returns>The <see cref="T:System.Drawing.Font"></see> to apply to the text displayed by the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultFont"></see> property.</returns>
		/// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		public new Font Font
		{
			get{return base.Font;}
			set{base.Font=value;columns.Font=value;}
		}

		/// <summary>
		/// Adds a column to the collection
		/// </summary>
		/// <param name="column">The column to add</param>
		public void AddColumn(CustomListColumn column)
		{
			columns.Add(column);
			column.ResizeTitle(Font);
		}

		/// <summary>
		/// Gets a column by the specified title
		/// </summary>
		/// <param name="name">The name of the column to get</param>
		/// <returns></returns>
		public CustomListColumn GetColumn(string name)
		{
			return columns.GetColumn(name);
		}

		/// <summary>
		/// Gets the collection of columns that is displayed in the control
		/// </summary>
		/// <value></value>
		[Browsable(false)]
		public CustomListColumnCollection Columns
		{
			get{return columns;}
		}
	}
}
