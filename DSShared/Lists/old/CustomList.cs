using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Reflection;
using System.ComponentModel;
using System.Data;
using System.IO;

using Microsoft.Win32;

namespace DSShared.Old
{
	public delegate void OptionClickEventHandler(object sender, OptionClickEventArgs e);
	//public enum ListType{Horizontal,Vertical};

	[DesignerAttribute(typeof(CustomListDesigner))]
	[DefaultEvent("OptionClick")]
	public abstract class CustomList:Control
	{
		protected ArrayList collection;
		protected int[] widths,locs;
		
		protected StrPropertyList properties;		
		protected string[] strList;

		protected bool[] moving;		
		protected bool[] showing;
		protected bool[] over;
		protected Brush selBrush = new SolidBrush(Color.LightGray);

		protected int threshhold=5;
		protected int headerHeight=16;
		protected int startY=16;
		protected Font myFont = new Font("Arial",12);
		protected Font smallFont = new Font("Arial",8);
		protected int rowHeight=14;

		protected bool useColorable=false;
		protected int overY;

		protected Color drawColor=Color.Black;
		protected VScrollBar vert;
		protected int space=2;

		protected int minWidth=20;
		protected int minSelColumn=-1;
		protected int maxSelColumn=-1;
//		protected Hashtable allowedChars;
		
		public event OptionClickEventHandler OptionClick;
		protected string name;

		protected IColorable selectedObject;
		private Border3DStyle border;
		private BorderStyle bStyle;
		private int offX=0,offY=0;
		private bool useScroll=true;

		protected bool keepFocus=true;

		protected CustomList(string name)
		{
			this.name=name;
			SetStyle(ControlStyles.AllPaintingInWmPaint|ControlStyles.DoubleBuffer|ControlStyles.UserPaint,true);

			collection = new ArrayList();

			vert = new VScrollBar();
			vert.Height=Height;
			vert.Location=new Point(Width-vert.Width,0);
			vert.Minimum=0;
			vert.Maximum=1;
			vert.Scroll+=new ScrollEventHandler(scroll);
			Controls.Add(vert);

			strList = new string[]{"Edit","Delete"};

			border = Border3DStyle.Etched;
			bStyle=BorderStyle.None;

			properties = new StrPropertyList();

			StrProperty.Update+=new NoArgsDelegate(Refresh);
//			allowedChars = new Hashtable();
//			allowedChars[Keys.Space]=' ';
		}

		[Browsable(false)]
		public int PreferredHeight
		{
			get
			{
				return headerHeight+((collection.Count+1)*smallFont.Height);
			}
		}

		[Browsable(true)]
		[Category("Behavior")]
		[DefaultValue(true)]
		[Description("If true, scroll bars will appear when the number of items drawn exceeds the bounds of the control")]
		public bool UseScrollBars
		{
			get{return useScroll;}
			set{useScroll=value;OnResize(null);}
		}

		[Browsable(true)]
		[Category("Appearance")]
		[DefaultValue(BorderStyle.None)]
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
						goto case BorderStyle.None;
					case BorderStyle.None:
						offX=offY=0;
						break;
					default:
						offX=offY=2;
						break;	
				}
				Refresh();
			}
		}

		[Browsable(true)]
		[Category("Appearance")]
		[DefaultValue(Border3DStyle.Etched)]
		[Description("Displays a border around the control")]
		public Border3DStyle Border3DStyle
		{
			get{return border;}
			set{border=value;Refresh();}
		}


		[Browsable(false)]
		public ArrayList Items
		{
			get{return collection;}
			set{collection=value;Refresh();}
		}

		[Browsable(true)]
		[Category("Behavior")]
		[DefaultValue(false)]
		[Description("If true, the entire row will be colored")]
		public bool UseColorable
		{
			get{return useColorable;}
			set{useColorable=value;Refresh();}
		}

		[Browsable(true)]
		[Category("Behavior")]
		[DefaultValue(new string[]{"Edit","Delete"})]
		[Description("Columns that are not part of the object being displayed")]
		public string[] StringList
		{
			get{return strList;}
			set{strList=value;calcX(true);}
		}

		[Browsable(false)]
		public DataTable DataTable
		{
			get
			{
				DataTable dt = new DataTable();
				foreach(StrProperty st in properties)
				{
					if(st.name!="")
						dt.Columns.Add(st.Name);
				}

				foreach(object o in Items)
				{
					DataRow dr = dt.NewRow();
					foreach(StrProperty st in properties)
						if(st.name!="")
							dr[st.Name]=st.Value(o);
					dt.Rows.Add(dr);
				}

				return dt;
			}
		}

		private void scroll(object sender,ScrollEventArgs e)
		{
			startY = headerHeight-vert.Value;
			Refresh();
		}

		public void ForceResize()
		{
			OnResize(null);
		}

		protected void writeCSS(StreamWriter sw)
		{
			sw.WriteLine("<style type=\"text/css\">"+@"
			<!--
			basefont, body, td, table
			{
				font-family: verdana, arial;
				font-size: 10px;
				color: #000000;
				border:1px solid black;
				border-collapse:collapse;
			}

			body
			{
				background-color: #FFFFFF;
			}

			td
			{
				text-align: center;
				padding: 2px;
			}

			td.name
			{
				background-color:#CCCCCC
			}
			-->
			</style>
			");
		}

		public virtual void MakeWebpage(string title,string path)
		{
			StreamWriter sw = new StreamWriter(path);	
		
			sw.WriteLine("<html><head>");
			sw.WriteLine("<title>"+title+"</title>");
			writeCSS(sw);
			sw.WriteLine("</head>");

			sw.WriteLine("<table border=1>");

			sw.Write("<tr>");
			foreach(StrProperty st in properties)
				sw.Write("<td class=\"name\">"+st.name+"</td>");
			sw.Write("</tr>");

			foreach(object o in Items)
			{
				sw.Write("<tr>");
				foreach(StrProperty st in properties)
					sw.Write("<td>"+st.Value(o)+"</td>");
				sw.Write("</tr>");
			}

			sw.WriteLine("</table>");
			sw.WriteLine("</html>");

			sw.Flush();
			sw.Close();
		}

		protected bool movingFlag=false;
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if(movingFlag)
			{
				for(int i=1;i<moving.Length;i++)
					if(moving[i] && e.X-locs[i-1]>minWidth)
					{
						widths[i-1]=e.X-locs[i-1];
						calcX(false);
						Refresh();
						break;
					}
			}
			else
			{
				if(e.Y<headerHeight+(collection.Count+1)*smallFont.Height)
				{
					bool flag=false;
					for(int i=1;i<locs.Length;i++)
						if(e.X>=locs[i]-threshhold && e.X<=locs[i]+threshhold)
						{
							flag=true;
							break;
						}

					if(flag)
						Cursor=Cursors.VSplit;
					else
						Cursor=Cursors.Arrow;
				}
				else
					Cursor=Cursors.Arrow;
			}

			if(e.Y>headerHeight)
			{
				for(int i=1;i<properties.Count+strList.Length+1;i++)
					over[i-1]=(locs[i-1]-space<e.X && locs[i]+2*space>e.X);

				overY=(e.Y-startY)/rowHeight;

				for(int i=0;i<strList.Length+properties.Count;i++)
					if(over[i])
					{
						Refresh();
						break;
					}
			}
			else
			{
				overY=-1;
				Refresh();
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			for(int i=0;i<locs.Length;i++)
				moving[i]=false;
			Cursor = Cursors.Arrow;
			movingFlag=false;
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			keepFocus=true;
			for(int i=1;i<locs.Length;i++)
			{
				if(e.X>=locs[i]-threshhold && e.X<=locs[i]+threshhold)
				{
					movingFlag=true;
					moving[i]=true;
					this.Cursor=Cursors.VSplit;
					break;
				}
			}

			if(overY>=0 && overY<collection.Count && Cursor!=Cursors.VSplit)
			{
				for(int i=minSelColumn;i<maxSelColumn && i<over.Length;i++)
					if(over[i]) 
					{
						if(selectedObject!=null)
							selectedObject.ColorIndex=-1;

						selectedObject=collection[overY] as IColorable;

						if(selectedObject!=null)
							selectedObject.ColorIndex=i;

						if(i-properties.Count>=0)
							OnOptionClick(new OptionClickEventArgs(strList[i-properties.Count],collection[overY],i));
						else
						{
							OnOptionClick(new OptionClickEventArgs(properties[i].Name,collection[overY],i));
							properties[i].ToggleSelected(true,collection[overY]);
						}
						Refresh();
						break;
					}
			}
			if(keepFocus)
				Focus();
		}

		protected virtual void OnOptionClick(OptionClickEventArgs e)
		{
			if(OptionClick!=null)
				OptionClick(this,e);			
		}

		protected void calcX(bool load)
		{
			if(load)
			{
				Graphics g = Graphics.FromHwnd(this.Handle);

				widths = new int[properties.Count+strList.Length];
				moving = new bool[properties.Count+strList.Length+1];
				locs = new int[properties.Count+strList.Length+1];
				showing = new bool[properties.Count+strList.Length];
				over = new bool[properties.Count+strList.Length];

				RegistryKey swKey = Registry.CurrentUser.CreateSubKey("Software");
				RegistryKey riKey = swKey.CreateSubKey(Globals.RegistryKey);
				RegistryKey ppKey = riKey.CreateSubKey(name);
				int extraWid=10;

				for(int i=0;i<properties.Count;i++)
				{
					try{widths[i] = (int)ppKey.GetValue("strLen"+i,g.MeasureString(properties[i].Name,myFont).Width+extraWid);}
					catch{widths[i] = (int)g.MeasureString(properties[i].Name,myFont).Width+extraWid;}

					moving[i]=false;
					over[i]=false;
					showing[i]=true;
				}

				for(int i=properties.Count;i<properties.Count+strList.Length;i++)
				{
					try{widths[i] = (int)ppKey.GetValue("strLen"+i,g.MeasureString(strList[i-properties.Count],myFont).Width+extraWid);}
					catch{widths[i] = (int)g.MeasureString(strList[i-properties.Count],myFont).Width+extraWid;}

					moving[i]=false;
					over[i]=false;
					showing[i]=true;
				}

				ppKey.Close();
				riKey.Close();
				swKey.Close();

				if(minSelColumn==-1)
					minSelColumn=properties.Count;

				if(maxSelColumn==-1)
					maxSelColumn=properties.Count+strList.Length;
			}

			locs[0]=0;
			for(int i=1;i<locs.Length;i++)
				locs[i]=widths[i-1]+locs[i-1]+2*space;
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			if(e.Delta<0)
			{
				if(vert.Value+10<vert.Maximum)
					vert.Value+=10;
				else
					vert.Value = vert.Maximum;
			}
			else
			{
				if(vert.Value-10>vert.Minimum)
					vert.Value-=10;
				else
					vert.Value=vert.Minimum;
			}

			this.scroll(null,null);
		}

		protected override void OnResize(EventArgs e)
		{			
			vert.Height=Height;
			vert.Location=new Point(Width-vert.Width,0);
			vert.Value=vert.Minimum;
			startY = headerHeight-vert.Minimum;
							//preferredHeight
							//headerHeight+((collection.Count+1)*smallFont.Height)
			//if(useScroll && headerHeight+((collection.Count+1)*(smallFont.Height+1))>Height)
			if(useScroll && PreferredHeight>Height)
			{
				vert.Maximum=(headerHeight+((collection.Count+1)*(smallFont.Height+1)))-Height;
				vert.Visible=true;
			}
			else
				vert.Visible=false;

			Refresh();
		}

		private bool drawRow=false;
		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics g = e.Graphics;	
			drawRow=false;

			float y = startY+offY;
			for(int i=0;i<collection.Count;i++)
			{
				renderLine(g,y,rowHeight,collection[i],i,smallFont);
				y+=rowHeight;
			}

			int ht = startY+collection.Count*rowHeight;
			int wd = locs[locs.Length-1]-space;

			g.FillRectangle(new SolidBrush(BackColor),offX,offY,wd,headerHeight);

			for(int i=1;i<locs.Length-1;i++)
				g.DrawLine(Pens.Black,locs[i]-space+offX,offY,locs[i]-space+offX,ht+offY);

			for(int i=0;i<properties.Count;i++)
				g.DrawString(properties[i].Name,myFont,Brushes.Black,new RectangleF(locs[i]+offX,offY,widths[i],headerHeight));//locs[i],0);

			for(int i=0;i<strList.Length;i++)
				g.DrawString(strList[i],myFont,Brushes.Black,new RectangleF(locs[i+properties.Count]+offX,offY,widths[i+properties.Count],headerHeight));

			if(drawRow)
				g.DrawRectangle(Pens.Red,offX,startY+overY*rowHeight+offY,locs[locs.Length-1]-space,rowHeight);

			g.DrawLine(Pens.Black,offX,headerHeight+offY,wd+offX,headerHeight+offY);
			g.DrawRectangle(Pens.Black,offX,offY,wd,ht);

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

		protected virtual void renderLine(Graphics g, float y, float height,object p,int rowNum,Font f)
		{
			Brush use = new SolidBrush(BackColor);
			if(useColorable && p is IColorable && ((IColorable)p).UseColor())
				use = new SolidBrush(((IColorable)p).DrawColor);
			else if(rowNum%2==0)
				use = Brushes.White;

			g.FillRectangle(use,offX,y,locs[locs.Length-1]-space,height);

			if(overY==rowNum)
			{
				for(int i=minSelColumn;i<over.Length && i<maxSelColumn;i++)
				{
					if(over[i])
					{
						g.FillRectangle(selBrush,locs[i]-space+offX,y,widths[i]+2*space,height);
						drawRow=true;
						break;
					}
				}
			}

			if(p is IColorable)
				for(int i=minSelColumn;i<over.Length && i<maxSelColumn;i++)
				{
					if(((IColorable)p).ColorIndex==i)
					{
						g.FillRectangle(new SolidBrush(((IColorable)p).DrawColor),locs[i]-space+offX,y,widths[i]+2*space,height);					
						break;
					}
				}

			for(int i=0;i<properties.Count;i++)
				g.DrawString(properties[i].Value(p),f,Brushes.Black,new RectangleF(locs[i]+offX,y,widths[i],height));
		}

		public virtual void Closing(object sender, CancelEventArgs e)
		{
			RegistryKey swKey = Registry.CurrentUser.CreateSubKey("Software");
			RegistryKey riKey = swKey.CreateSubKey(Globals.RegistryKey);
			RegistryKey ppKey = riKey.CreateSubKey(name);

			for(int i=0;i<widths.Length;i++)
				ppKey.SetValue("strLen"+i,widths[i]);

			ppKey.Close();
			riKey.Close();
			swKey.Close();
		}
/*
		protected override void OnKeyDown(KeyEventArgs e)
		{
			switch(e.KeyData)
			{
				#region number pad
				case Keys.NumPad0:
					goto case Keys.D0;
				case Keys.NumPad1:
					goto case Keys.D1;
				case Keys.NumPad2:
					goto case Keys.D2;
				case Keys.NumPad3:
					goto case Keys.D3;
				case Keys.NumPad4:
					goto case Keys.D4;
				case Keys.NumPad5:
					goto case Keys.D5;
				case Keys.NumPad6:
					goto case Keys.D6;
				case Keys.NumPad7:
					goto case Keys.D7;
				case Keys.NumPad8:
					goto case Keys.D8;
				case Keys.NumPad9:
					goto case Keys.D9;
					#endregion

				case Keys.D0:
					readDigit(0);
					break;
				case Keys.D1:
					readDigit(1);
					break;
				case Keys.D2:
					readDigit(2);
					break;
				case Keys.D3:
					readDigit(3);
					break;
				case Keys.D4:
					readDigit(4);
					break;
				case Keys.D5:
					readDigit(5);
					break;
				case Keys.D6:
					readDigit(6);
					break;
				case Keys.D7:
					readDigit(7);
					break;
				case Keys.D8:
					readDigit(8);
					break;
				case Keys.D9:
					readDigit(9);
					break;
				case Keys.Back:
					backspace();
					break;
				case Keys.Enter:
					if(selectedObject!=null)
						selectedObject.ColorIndex=-1;
					for(int i=minSelColumn;i<maxSelColumn && i<over.Length;i++)
						if(over[i]) 
							properties[i].ToggleSelected(false,selectedObject);
					enterKey();
					selectedObject=null;					
					break;
				default:

					if(e.Shift && allowedChars["s"+e.KeyCode]!= null)
						readChar((char)allowedChars["s"+e.KeyCode]);
					else if(!e.Shift && allowedChars[e.KeyCode]!=null)
						readChar((char)allowedChars[e.KeyCode]);
					else if(e.KeyValue>='A' && e.KeyValue<='Z')
					{
						if(e.Shift)
							readChar((char)e.KeyValue);
						else
							readChar((char)(e.KeyValue-'A'+'a'));
					}
//					else
//						Console.WriteLine("Invalid character: "+e.KeyCode+":"+e.KeyValue+":"+(char)e.KeyValue);
		
					break;
			}
			Refresh();
		}*/

		protected virtual void readDigit(int digit){}
		protected virtual void backspace(){}
		protected virtual void readChar(char c){}
		protected virtual void enterKey(){}

		protected bool keystrokeProcessed=false;
		protected bool clearSelected=true;
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			clearSelected=true;
			if (!keystrokeProcessed) 
			{
				char c = e.KeyChar;
				int cValue = (int) c;
				if (cValue==8) //backspace
					backspace();
				else if(cValue==13)//enter
				{
					if(selectedObject!=null)
						selectedObject.ColorIndex=-1;
					for(int i=minSelColumn;i<maxSelColumn && i<over.Length;i++)
						if(over[i]) 
							properties[i].ToggleSelected(false,selectedObject);
					enterKey();
					if(clearSelected)
						selectedObject=null;					
					else
						for(int i=minSelColumn;i<maxSelColumn && i<over.Length;i++)
							if(over[i]) 
							{
								selectedObject.ColorIndex=i;
								properties[i].ToggleSelected(true,selectedObject);
							}
				}
				else 
				{
					if(c>='0' && c<='9')
						readDigit((int)(c-'0'));
					else
						readChar(c);
				}
				Refresh();
			}
		}
/*
		protected override bool ProcessDialogKey(Keys keyData)
		{
			keystrokeProcessed = true;
			switch (keyData) 
			{
				case Keys.Down:
					caretY++;
					if (caretY == rowCount)
						caretY = 0;
					break;
				case Keys.Up:
					caretY--;
					if (caretY  < 0)
						caretY = rowCount - 1;
					break;
				case Keys.Left:
					caretX--;
					if (caretX  < 0)
					{
						caretX = columnCount - 1;
						caretY--;
						if (caretY  < 0)
							caretY = rowCount -1;
					}
					break;
				case Keys.Right:
					caretX++;
					if (caretX == columnCount)
					{
						caretX = 0;
						caretY++;
						if (caretY == rowCount)
							caretY = 0;
					}
					break;
				case Keys.Control | Keys.R:
					this.BackColor = Color.Red;
					break;
				case Keys.Control | Keys.G:
					this.BackColor = Color.Green;
					break;
				case Keys.Control | Keys.B:
					this.BackColor = Color.Blue;
					break;
				case Keys.Control | Keys.Alt | Keys.R:
					foreColor = Color.Red;
					break;
				case Keys.Control | Keys.Alt | Keys.G:
					foreColor = Color.Green;
					break;
				case Keys.Control | Keys.Alt | Keys.B:
					foreColor = Color.Blue;
					break;
				case Keys.Escape:
					this.BackColor = Color.White;
					break;
				case Keys.Alt | Keys.F4:
					Application.Exit(); //uu... abrupt exit.
					break;
				case Keys.F1:
					MessageBox.Show("Help is on the way", "Message from ProcessDialogKey");
					break;
				default:
					if ((int)(Keys.Control & keyData) != 0) 
					{
						//The control key is pressed. Do something here if you want.
						return true;
					}
					else if ((int)(Keys.Alt & keyData) != 0) 
					{
						//The Alt key is pressed. Do something here if you want.
						return true;
					}
					else 
					{
						keystrokeProcessed = false; // let KeyPress event handler handle this keystroke.
					}
					break;
			}
			this.Invalidate();
			this.Update();
			return base.ProcessDialogKey(keyData);
		}*/


		public virtual void Save(){}
	}

	public class OptionClickEventArgs:EventArgs
	{
		private string option;
		private object item;
		private int column;

		public OptionClickEventArgs(string option, object item,int column)
		{
			this.option=option;
			this.item=item;
			this.column=column;
		}

		public OptionClickEventArgs():this(null,null,0){}

		public string ColumnTitle
		{
			get{return option;}
			set{option=value;}
		}

		public object SelectedObject
		{
			get{return item;}
			set{item=value;}
		}

		public int ColumnIndex
		{
			get{return column;}
			set{column=value;}
		}
	}

	public class StrPropertyList:ArrayList
	{
		public StrPropertyList(params StrProperty[] itms)
		{
			foreach(object o in itms)
				Add(o);
		}

		public new StrProperty this[int i]
		{
			get{return (StrProperty)base[i];}
			set{base[i]=value;}
		}
	}

	public delegate void NoArgsDelegate();
	public class StrProperty
	{
		public object name;
		public object[]Index;
		public PropertyInfo Property;
		public StrProperty Nested;
		public string HelpText;
		private bool selectable=false;
		private static Timer cursorTimer;
		private string addText="";
		private static bool tick=false;
		private static bool timerStarted=false;
		private static object selObject=null;
		private static StrProperty selProperty=null;

		public static event NoArgsDelegate Update;

		public StrProperty(object name, System.Reflection.PropertyInfo property,string helpText)
			:this(name,null,property,null,helpText){}

		public StrProperty(object name, System.Reflection.PropertyInfo property)
			:this(name,null,property){}
		public StrProperty(object name, System.Reflection.PropertyInfo property,bool selectable)
			:this(name,null,property,selectable){}

		public StrProperty(object name, PropertyInfo property,StrProperty nested)
			:this(name,null,property,nested,false){}
		public StrProperty(object name, PropertyInfo property,StrProperty nested,bool selectable)
			:this(name,null,property,nested,selectable){}
		
		public StrProperty(object name,object[] index, PropertyInfo property,StrProperty nested)
			:this(name,index,property,nested,null){}
		public StrProperty(object name,object[] index, PropertyInfo property,StrProperty nested,bool selectable)
			:this(name,index,property,nested,null,selectable){}

		public StrProperty(object name,object[] index, System.Reflection.PropertyInfo property,bool selectable)
			:this(name,index,property,null,selectable){}
		public StrProperty(object name,object[] index, System.Reflection.PropertyInfo property)
			:this(name,index,property,null){}

		public StrProperty(object name,object[] index, PropertyInfo property,StrProperty nested,string helpText)
			:this(name,index,property,nested,helpText,false){}

		public StrProperty(object name,object[] index, PropertyInfo property,StrProperty nested,string helpText,bool selectable)
		{
			this.name=name;
			Index=index;
			Property=property;
			Nested=nested;
			HelpText=helpText;
			this.selectable=selectable;
		}

		public string Name
		{
			get{return name.ToString();}
			set{name=value;}
		}

		public object Object
		{
			get{return name;}
		}

		public bool Selectable
		{
			get{return selectable;}
			set{selectable=value;}
		}

		private void timerTick(object sender,EventArgs e)
		{
			if(tick)
				addText="|";
			else
				addText="";
			tick=!tick;

			if(Update!=null)
				Update();
		}

		public void ToggleSelected(bool isSelected,object selObj)
		{
			if(!selectable)
				return;

			StrProperty.selObject=selObj;

			if(cursorTimer==null)
			{
				cursorTimer=new Timer();
				cursorTimer.Interval=500;				
			}

			if(selProperty!=null)
			{
				cursorTimer.Tick-=new EventHandler(selProperty.timerTick);
				selProperty.addText="";
			}
			cursorTimer.Tick+=new EventHandler(timerTick);
			selProperty=this;

			if(isSelected && !timerStarted)
			{		
				cursorTimer.Start();
				timerStarted=true;
				addText="|";
			}
			else if(!isSelected && timerStarted)
			{
				//cursorTimer.Tick-=new EventHandler(timerTick);
				cursorTimer.Stop();
				timerStarted=false;
				addText="";
			}

			if(Update!=null)
				Update();
		}

		public string Value(object o)
		{
			if(o!=null)
			{
				object obj = Property.GetValue(o,Index);

				if(obj!=null)
				{
					if(Nested==null)
					{
						if(o==selObject)
							return obj.ToString()+addText;
						else
							return obj.ToString();
					}
					else
					{
						if(o==selObject)
							return Nested.Value(obj)+addText;			
						else
							return Nested.Value(obj);
					}
				}
			}
			else
				Console.WriteLine("Rendering null object");
			return "";
		}
	}

	/// <summary>
	/// A simple designer class for the CollapsibleSplitter control to remove 
	/// unwanted properties at design time.
	/// </summary>
	public class CustomListDesigner : System.Windows.Forms.Design.ControlDesigner
	{
		public CustomListDesigner()
		{
		}

		protected override void PreFilterProperties(System.Collections.IDictionary properties)
		{
			properties.Remove("RightToLeft");
			properties.Remove("AccessibleRole");
			properties.Remove("AccessibleName");
			properties.Remove("AccessibleDescription");
			properties.Remove("Text");
			properties.Remove("AllowDrop");
			properties.Remove("ContextMenu");
			properties.Remove("ImeMode");
			properties.Remove("BackgroundImage");
			properties.Remove("CausesValidation");
		}
	}
}
