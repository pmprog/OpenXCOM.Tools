using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace DSShared.Lists
{
	/// <summary>
	/// Delegate for when the control wants to be refreshed
	/// </summary>
	public delegate void RefreshDelegate();

	/// <summary>
	/// Delegate for when the mouse moves over a row
	/// </summary>
	/// <param name="mouseY">y-coordinate of the mouse</param>
	/// <param name="overCol">Column that the y-coordinate is under</param>
	public delegate void RowMoveDelegate(int mouseY,CustomListColumn overCol);

	/// <summary>
	/// Class that manages a collection of CustomListColumn objects in a CustomList control
	/// </summary>
	public class CustomListColumnCollection:System.Collections.IEnumerable
	{
		private List<CustomListColumn> list;
		private Dictionary<string, CustomListColumn> colHash;
		private int tableWidth=0;
		private int offX,offY;
		private int colSpace=2,rowSpace=2,headerHeight=14;

		private BorderStyle bStyle=BorderStyle.FixedSingle;
		private Border3DStyle border = Border3DStyle.Etched;

		private SolidBrush foreBrush,headerBrush;
		private Font font;
		private int width,height,threshhold=5;

		private CustomListColumn movingCol=null,overCol=null,overThreshhold;
		private CustomList parent;

		/// <summary>
		/// Event for when this collection wants to be refreshed
		/// </summary>
		public event RefreshDelegate RefreshEvent;

		/// <summary>
		/// Event for when the mouse moves under a column
		/// </summary>
		public event RowMoveDelegate RowMoveOver;

		/// <summary>
		/// Event for when a row gets clicked
		/// </summary>
		public event MouseEventHandler RowClicked;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:CustomListColumnCollection"/> class.
		/// </summary>
		public CustomListColumnCollection()
		{
			//SetStyle(ControlStyles.SupportsTransparentBackColor|ControlStyles.DoubleBuffer|ControlStyles.AllPaintingInWmPaint|ControlStyles.UserPaint|ControlStyles.ResizeRedraw,true);
			headerBrush = new SolidBrush(Color.LightGray);
			foreBrush = new SolidBrush(Color.Black);
			headerHeight=14;

			list = new List<CustomListColumn>();
			colHash = new Dictionary<string, CustomListColumn>();
		}

		/// <summary>
		/// Gets or sets the parent control of this object
		/// </summary>
		/// <value>The parent.</value>
		public CustomList Parent
		{
			get{return parent;}
			set{parent=value;}
		}

		/// <summary>
		/// Gets or sets the width.
		/// </summary>
		/// <value>The width.</value>
		public int Width
		{
			get{return width;}
			set{width=value;}
		}

		/// <summary>
		/// Gets or sets the height.
		/// </summary>
		/// <value>The height.</value>
		public int Height
		{
			get{return height;}
			set{height=value;}
		}

		/// <summary>
		/// Gets or sets the font.
		/// </summary>
		/// <value>The font.</value>
		public Font Font
		{
			get{return font;}
			set{font=value;}
		}

		/// <summary>
		/// Gets or sets the <see cref="T:CustomListColumn"/> with the specified idx.
		/// </summary>
		/// <value></value>
		public CustomListColumn this[int idx]
		{
			get{return list[idx];}
			set{list[idx]=value;}
		}

		/// <summary>
		/// Adds the specified column to the collection
		/// </summary>
		/// <param name="column">The column.</param>
		public void Add(CustomListColumn column)
		{
			if (list.Contains(column))
				return;

			colHash[column.Title]=column;

			column.Left=tableWidth;
			column.Index=list.Count;

			column.LeftChanged += new CustomListColumChangedDelegate(colLeftChanged);
			column.WidthChanged += new CustomListColumChangedDelegate(colWidthChanged);
			tableWidth+=column.Width;

			list.Add(column);
		}

		/// <summary>
		/// Gets the column with the specified name
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		public CustomListColumn GetColumn(string name)
		{
			return colHash[name];
		}

		/// <summary>
		/// Gets or sets the x-offset value. Tweaking this will move where the control is drawn
		/// </summary>
		/// <value></value>
		public int OffX
		{
			get{return offX;}
			set{offX=value;}
		}

		/// <summary>
		/// Gets or sets the Y-offset value. Tweaking this will move where the control is drawn
		/// </summary>
		/// <value></value>
		public int OffY
		{
			get{return offY;}
			set{offY=value;}
		}

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
		/// </returns>
		public System.Collections.IEnumerator GetEnumerator()
		{
			return list.GetEnumerator();
		}

		/// <summary>
		/// Gets the number of columns in the collection
		/// </summary>
		/// <value>The count.</value>
		public int Count
		{
			get{return list.Count;}
		}

		/// <summary>
		/// Gets the width of the table.
		/// </summary>
		/// <value>The width of the table.</value>
		public int TableWidth
		{
			get{return tableWidth;}
		}

		/// <summary>
		/// Gets or sets the row space.
		/// </summary>
		/// <value>The row space.</value>
		public int RowSpace
		{
			get{return rowSpace;}
			set{rowSpace=value;}
		}

		private void colWidthChanged(CustomListColumn col,int amount)
		{
			tableWidth-=amount;

			if(!leftChangeLock)
			{
				leftChangeLock=true;
				for(int i=col.Index+1;i<Count;i++)
					this[i].Left-=amount;

				if(RefreshEvent!=null)
					RefreshEvent();
				leftChangeLock=false;
			}
		}

		private bool leftChangeLock=false;
		private void colLeftChanged(CustomListColumn col,int amount)
		{
			if(!leftChangeLock)
			{
				if(col.Index==0)
					return;

				if(this[col.Index-1].Width-amount>=CustomListColumn.MinWidth)
				{
					leftChangeLock=true;
					this[col.Index-1].Width-=amount;

					int startX=this[col.Index-1].Width+this[col.Index-1].Left;
					for(int i=col.Index;i<Count;i++)
					{
						this[i].Left=startX;
						startX+=this[i].Width;
					}

					if(RefreshEvent!=null)
						RefreshEvent();

					leftChangeLock=false;
				}
			}
		}

		/// <summary>
		/// Gets or sets the color of the foreground
		/// </summary>
		/// <value></value>
		public Color ForeColor
		{
			get{return foreBrush.Color;}
			set{foreBrush.Color=value;}
		}

		/// <summary>
		/// Gets or sets the color of the header.
		/// </summary>
		/// <value>The color of the header.</value>
		[Browsable(true)]
		[Category("Appearance")]
		[Description("Background color of the column-row")]
		public Color HeaderColor
		{
			get{return headerBrush.Color;}
			set{headerBrush.Color=value;}
		}

		/// <summary>
		/// Gets or sets the height of the header.
		/// </summary>
		/// <value>The height of the header.</value>
		[Browsable(true)]
		[Category("Appearance")]
		[DefaultValue(14)]
		[Description("Height of the row with column headers")]
		public int HeaderHeight
		{
			get{return offY+headerHeight+rowSpace*2;}
			set{headerHeight=value;}
		}

		/// <summary>
		/// Gets or sets the border style.
		/// </summary>
		/// <value>The border style.</value>
		[Browsable(true)]
		[Category("Appearance")]
		[DefaultValue(BorderStyle.FixedSingle)]
		[Description("Displays a border around the control")]
		public System.Windows.Forms.BorderStyle BorderStyle
		{
			get{return bStyle;}
			set
			{
				bStyle=value;

				switch(bStyle)
				{
					case BorderStyle.FixedSingle:
						offX=offY=1;
						break;
					case BorderStyle.None:
						offX=offY=0;
						break;
					default:
						offX=offY=2;
						break;	
				}
			}
		}

		/// <summary>
		/// Gets or sets the border3D style.
		/// </summary>
		/// <value>The border3D style.</value>
		[Browsable(true)]
		[Category("Appearance")]
		[DefaultValue(Border3DStyle.Etched)]
		[Description("Displays a border around the control")]
		public Border3DStyle Border3DStyle
		{
			get{return border;}
			set{border=value;}
		}

		/// <summary>
		/// Renders this control to the supplied PaintEventArgs
		/// </summary>
		/// <param name="e">The <see cref="T:System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
		/// <param name="rowHeight">Height of the row.</param>
		/// <param name="yOffset">The y offset.</param>
		public void Render(PaintEventArgs e,int rowHeight,int yOffset)
		{
			e.Graphics.FillRectangle(headerBrush,offX,offY,tableWidth-1,headerHeight+rowSpace*2);

			int startX=0;
			for(int i=0;i<list.Count;i++)
			{
				CustomListColumn col = list[i] as CustomListColumn;
				if(col==overCol)
					e.Graphics.FillRectangle(Brushes.LightBlue,col.Left,offY,col.Width,headerHeight+rowSpace*2);

				//vertical lines
				e.Graphics.DrawLine(Pens.Black,col.Width+startX-colSpace+offX,offY,col.Width+startX-colSpace+offX,HeaderHeight+rowHeight+yOffset);
				e.Graphics.DrawString(col.Title,Font,foreBrush,new RectangleF(startX+offX,offY,startX+col.Width,Font.Height));//locs[i],0);
				//e.Graphics.DrawLine(Pens.Red,col.Left+col.Width,HeaderHeight+rowHeight+yOffset,col.Left+col.Width,Height);

				startX+=col.Width;
			}

			e.Graphics.DrawLine(Pens.Black,offX,offY+headerHeight+rowSpace*2,tableWidth-1,offY+headerHeight+rowSpace*2);

			switch(bStyle)
			{
				case BorderStyle.Fixed3D:
					System.Windows.Forms.ControlPaint.DrawBorder3D(e.Graphics,0,0,Width,Height,border);
					break;
				case BorderStyle.FixedSingle:
					System.Windows.Forms.ControlPaint.DrawBorder(e.Graphics,new Rectangle(0,0,Width,Height),Color.Black,ButtonBorderStyle.Solid);
					break;
			}
		}

		/// <summary>
		/// The parent calls this when the mouse button is released
		/// </summary>
		/// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
		public void MouseUp(MouseEventArgs e)
		{
			movingCol=null;
		}

		/// <summary>
		/// The parent calls this when the mouse button is pressed
		/// </summary>
		/// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
		public void MouseDown(MouseEventArgs e)
		{
			if(e.Y<headerHeight)
			{
				if(overThreshhold!=null)
					movingCol=overThreshhold;
			}
			else
			{
				if(RowClicked!=null)
					RowClicked(this,e);
			}
		}

		/// <summary>
		/// The parent calls this when the mouse is moved
		/// </summary>
		/// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
		public void MouseMove(MouseEventArgs e)
		{
			if(movingCol!=null)
			{
				if(this[movingCol.Index].Left+CustomListColumn.MinWidth<e.X)
				{
					if(movingCol.Index+1!=Count)
						this[movingCol.Index+1].Left=e.X;
					else
						movingCol.Width=e.X-movingCol.Left;
				}
			}
			else
			{
				if(e.Y<headerHeight)
				{
					overThreshhold=null;
					for(int i=0;i<Count;i++)
						if(e.X>=this[i].Left+this[i].Width-threshhold && e.X<=this[i].Left+this[i].Width+threshhold)
						{
							overThreshhold=this[i];
							break;
						}

					if(overThreshhold!=null)
						parent.Cursor=Cursors.VSplit;
					else
						parent.Cursor=Cursors.Arrow;
				}
				else
					parent.Cursor=Cursors.Arrow;
			}

			overCol=null;
			foreach(CustomListColumn cc in this)
				if(cc.Left<e.X && cc.Left+cc.Width>e.X)
				{
					overCol=cc;
					if(RefreshEvent!=null)
						RefreshEvent();

					break;
				}

			if(e.Y>headerHeight && RowMoveOver !=null)
				RowMoveOver(e.Y,overCol);
		}
	}
}
