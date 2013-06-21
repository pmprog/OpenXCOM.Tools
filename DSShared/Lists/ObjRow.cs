using System;
using System.Drawing;
using System.Windows.Forms;

namespace DSShared.Lists
{
	/// <summary>
	/// Class that represents a row in a CustomList
	/// </summary>
	public class ObjRow:IComparable
	{
		#region protected variables
		/// <summary>
		/// The object this row wraps around
		/// </summary>
		protected Object obj;

		/// <summary>
		/// The list of columns that specify what information of the obj is being displayed
		/// </summary>
		protected CustomListColumnCollection columns;

		/// <summary>
		/// Row screen information
		/// </summary>
		protected int width,height,top;

		/// <summary>
		/// Selected column
		/// </summary>
		protected CustomListColumn selCol=null;

		/// <summary>
		/// Clicked-on column
		/// </summary>
		protected CustomListColumn clickCol=null;

		/// <summary>
		/// Timer to make a blinking cursor when an editable cell is clicked on
		/// </summary>
		protected Timer cursorTimer=null;

		/// <summary>
		/// String for the blinking cursor
		/// </summary>
		protected string addStr="";

		/// <summary>
		/// Timer information
		/// </summary>
		protected bool flip = true, timerStarted, putDecimal = false;

		/// <summary>
		/// Row index
		/// </summary>
		protected int rowIdx=0;

		#endregion

		/// <summary>
		/// Flag to tell if we are currently in edit mode
		/// </summary>
		private bool editing=false;

		/// <summary>
		/// Raised when the control needs to refresh itself
		/// </summary>
		public event RefreshDelegate RefreshEvent;

		private static int numCreated=0;
		private int createdNum;

		private string editBuffer="";

		//scanning of object should have taken place before this object is created

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ObjRow"/> class.
		/// </summary>
		/// <param name="obj">The obj.</param>
		/// <param name="columns">The columns.</param>
		public ObjRow(object obj,CustomListColumnCollection columns)
		{
			this.obj=obj;
			this.columns=columns;

			if(cursorTimer==null)
			{
				cursorTimer = new Timer();
				cursorTimer.Interval=500;
				cursorTimer.Tick+=new EventHandler(timerTick);

				timerStarted=false;
			}

			createdNum=numCreated++;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ObjRow"/> class.
		/// </summary>
		/// <param name="obj">The obj.</param>
		public ObjRow(object obj):this(obj,null){}


		/// <summary>
		/// Compares one row to another. other must be an ObjRow and ObjRow.obj must implement IComparable
		/// </summary>
		/// <param name="other">The object to compare with</param>
		/// <returns></returns>
		public int CompareTo(object other)
		{
			if(other is ObjRow)
			{
				if(obj is IComparable)
					return ((IComparable)obj).CompareTo(((ObjRow)other).obj);
			}
			return -1;
		}

		/// <summary>
		/// Gets or sets the object this row displays
		/// </summary>
		/// <value>The object.</value>
		public object Object
		{
			get{return obj;}
			set{obj=value;}
		}

		/// <summary>
		/// Gets or sets the index of the row. This is its position in the list
		/// </summary>
		/// <value>The index of the row.</value>
		public int RowIndex
		{
			get{return rowIdx;}
			set{rowIdx=value;}
		}

		/// <summary>
		/// Equality test against another object. Calls Object.Equals(other.Object)
		/// </summary>
		/// <param name="other">The other object to test against</param>
		/// <returns></returns>
		public override bool Equals(object other)
		{
			if(obj==null)
				return this==other;

			if(other is ObjRow)
				return obj.Equals(((ObjRow)other).obj);

			return false;
		}

		/// <summary>
		/// Serves as a hash function for a particular type. <see cref="M:System.Object.GetHashCode"></see> is suitable for use in hashing algorithms and data structures like a hash table.
		/// </summary>
		/// <returns>
		/// A hash code for the current <see cref="T:System.Object"></see>.
		/// </returns>
		public override int GetHashCode()
		{
			if(obj==null)
				return createdNum;

			return obj.GetHashCode()>>1;
		}

		private void timerTick(object sender, EventArgs e)
		{
			addStr=flip?"":"|";
			flip=!flip;

			if(RefreshEvent !=null)
				RefreshEvent();
		}

		private void startTimer()
		{
			if(!timerStarted)
				cursorTimer.Start();
		}

		private void stopTimer()
		{
			if(timerStarted)
				cursorTimer.Stop();
		}

		/// <summary>
		/// Gets or sets the width of the row. This is used for the drawing function and should not be changed
		/// by the user
		/// </summary>
		/// <value>The width.</value>
		public int Width
		{
			get{return width;}
			set{width=value;}
		}

		/// <summary>
		/// Gets or sets the height of the row. This is used for the drawing function and should not be changed
		/// by the user
		/// </summary>
		/// <value>The height.</value>
		public int Height
		{
			get{return height;}
			set{height=value;}
		}

		/// <summary>
		/// Gets or sets the top. This is used for the drawing function and should not be changed by the user
		/// </summary>
		/// <value>The top.</value>
		public int Top
		{
			get{return top;}
			set{top=value;}
		}

		/// <summary>
		/// Sets the column collection used to pull information from the object
		/// </summary>
		/// <value>The columns.</value>
		public CustomListColumnCollection Columns
		{
			set{columns=value;}
		}

		/// <summary>
		/// called when a mouse moves over the row
		/// </summary>
		/// <param name="col"></param>
		public void MouseOver(CustomListColumn col)
		{
			selCol=col;
		}

		/// <summary>
		/// Called when the mouse leaves the row's bounding rectangle
		/// </summary>
		public void MouseLeave()
		{	
			selCol=null;
		}

		/// <summary>
		/// Called when the mouse clicks on a row 
		/// </summary>
		/// <param name="col">The column the mouse was over when the button was clicked</param>
		public void Click(CustomListColumn col)
		{
			clickCol=col;
			addStr="|";
			cursorTimer.Start();
			if(col.Property!=null)
			{
				editBuffer = clickCol.Property.Value(obj).ToString();
				editing=true;
			}

			col.FireClick(this);
		}

		/// <summary>
		/// This method is called before another row is clicked. This is used as a 'turn off' function
		/// </summary>
		public void UnClick()
		{			
			cursorTimer.Stop();

			if(clickCol!=null && clickCol.Property!=null)
			{
				try
				{
					switch(clickCol.Property.EditType)
					{
						case EditStrType.String:
							clickCol.Property.SetValue(obj,editBuffer);
							break;
						case EditStrType.Int:
							clickCol.Property.SetValue(obj,int.Parse(editBuffer));
							break;
						case EditStrType.Float:
							clickCol.Property.SetValue(obj,double.Parse(editBuffer));
							break;
					}
				}
				catch{}
				editing=false;
			}

			clickCol=null;
			addStr="";

			FireRefresh();
		}

		/// <summary>
		/// Outside access to fire the refresh event
		/// </summary>
		public void FireRefresh()
		{
			if(RefreshEvent!=null)
				RefreshEvent();
		}

		/// <summary>
		/// Called when a key is pressed on the keyboard and this row is selected
		/// </summary>
		/// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
		public void KeyPress(KeyPressEventArgs e)
		{
			clickCol.FireKeyPress(this,e);

			if(clickCol!=null && clickCol.Property.EditType!=EditStrType.None)
			{
				switch(clickCol.Property.EditType)
				{
					case EditStrType.Custom:
						if(clickCol.Property.KeyFunction==null)
							throw new Exception("KeyFunction was not initialized");
						clickCol.Property.KeyFunction(this,clickCol,e);
						break;
					default:
						if(e.KeyChar=='\b')
						{
							if(editBuffer.Length>0)
								editBuffer = editBuffer.Substring(0,editBuffer.Length-1);
						}
						else if(e.KeyChar>=32) //printable characters only
							editBuffer += e.KeyChar;

						if(RefreshEvent!=null)
							RefreshEvent();
						break;
				}
			}
		}

		/// <summary>
		/// This function currently does nothing
		/// </summary>
		/// <param name="e"></param>
		public void KeyDown(KeyEventArgs e)
		{

		}

		/// <summary>
		/// This function currently does nothing
		/// </summary>
		/// <param name="e"></param>
		public void KeyUp(KeyEventArgs e)
		{

		}

		/// <summary>
		/// Method that paints this row
		/// </summary>
		/// <param name="e"></param>
		/// <param name="yOffset"></param>
		public virtual void Render(PaintEventArgs e,int yOffset)
		{
			//base.OnPaint(e);

			if(obj!=null)
			{
				int startX=0;
				System.Drawing.RectangleF rowRect = new System.Drawing.RectangleF(columns.OffX,top+yOffset+1,columns.TableWidth-1,columns.Font.Height+columns.RowSpace*2-1);
				if(selCol!=null)
					e.Graphics.FillRectangle(Brushes.LightGreen,rowRect);

				if(clickCol!=null)
				{
					System.Drawing.Rectangle rect = new System.Drawing.Rectangle(clickCol.Left,top+yOffset+1,clickCol.Width,columns.Font.Height+columns.RowSpace+1);
					e.Graphics.FillRectangle(Brushes.LightSeaGreen,rowRect);
					e.Graphics.FillRectangle(Brushes.LightSteelBlue,rect);
				}

				for(int i=0;i<columns.Count;i++)
				{
					CustomListColumn col = columns[i] as CustomListColumn;

					System.Drawing.Rectangle rect = new System.Drawing.Rectangle(startX+columns.OffX,top+yOffset+columns.RowSpace,col.Width-4,columns.Font.Height);
					
					if(clickCol==col && col.Property!=null && clickCol.Property.EditType==EditStrType.Custom)
						e.Graphics.DrawString(col.Property.Value(obj).ToString()+addStr,columns.Font,System.Drawing.Brushes.Black,rect);
					else if(clickCol==col && col.Property!=null && clickCol.Property.EditType!=EditStrType.None)
						e.Graphics.DrawString((editing?editBuffer:col.Property.Value(obj).ToString())+addStr,columns.Font,System.Drawing.Brushes.Black,rect);
					else if(col.Property!=null)
						e.Graphics.DrawString(col.Property.Value(obj).ToString()+(putDecimal?".":""),columns.Font,System.Drawing.Brushes.Black,rect);

					startX+=col.Width;
					if(selCol==col)
						e.Graphics.DrawRectangle(Pens.Red,rect);
				}
				e.Graphics.DrawLine(Pens.Black,columns.OffX,top+columns.Font.Height+columns.RowSpace*2+yOffset,columns.TableWidth-1,top+columns.Font.Height+columns.RowSpace*2+yOffset);

			}
		}
/*
		protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseDown(e);
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
		}
		*/
	}
}
