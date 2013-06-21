#region author comment
/*

 *	http://www.codeproject.com/cs/miscctrl/collapsiblesplitter.asp
 * 
	Windows Forms Collapsible Splitter Control for .Net
	(c)Copyright 2002-2003 NJF (furty74@yahoo.com). All rights reserved.
	
	Assembly Build Dependencies:
	CollapsibleSplitter.bmp
	
	Version 1.1 Changes:
	OnPaint is now overridden instead of being a handled event, and the entire splitter is now painted rather than just the collpaser control
	The splitter rectangle is now correctly defined
	The Collapsed property was renamed to IsCollapsed, and the code changed so that no value needs to be set
	New visual styles added: Win9x, XP, DoubleDots and Lines
	
	Version 1.11 Changes:
	The OnMouseMove event handler was updated to address a flickering issue discovered by John O'Byrne
	
	Version 1.2 Changes:
	Added support for horizontal splitters
	
	Version 1.21 Changes:
	Added support for inclusion as a VS.Net ToolBox control
	Added a ToolBox bitmap
	Removed extraneous overrides
	Added summaries
	
	Version 1.22 Changes:
	Removed the ParentFolder from public properties - this is now set automatically in the OnHandleCreated event
	*Added expand/collapse animation code
	
	Version 1.3 Changes:
	Added an optional 3D border
	General code and comment cleaning
	Flagged assembly with the CLSCompliant attribute
	Added a simple designer class to filter unwanted properties	
*/

/*
  My Changes
  -added new Dock property
  changed VisualStyles to DotStyles
  changed drawing logic
  cached pens and brushes so they arent created over and over in drawing method
  turned some events (all?) into overrides
 */
#endregion
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace DSShared.Windows
{   
	/// <summary>
	/// Specifies how the outline of the collapsible splitter is drawn
	/// </summary>
	public enum DotStyles
	{
		/// <summary>
		/// 
		/// </summary>
		Mozilla,
		/// <summary>
		/// 
		/// </summary>
		XP,
		/// <summary>
		/// 
		/// </summary>
		Win9x,
		/// <summary>
		/// 
		/// </summary>
		DoubleDots,
		/// <summary>
		/// 
		/// </summary>
		Lines
	}

	/// <summary>
	/// Splitter states
	/// </summary>
	public enum SplitterState
	{
		/// <summary>
		/// 
		/// </summary>
		Collapsed,
		/// <summary>
		/// 
		/// </summary>
		Expanding,
		/// <summary>
		/// 
		/// </summary>
		Expanded,
		/// <summary>
		/// 
		/// </summary>
		Collapsing
	}

	/// <summary>
	/// A custom collapsible splitter that can resize, hide and show associated form controls
	/// </summary>
	[ToolboxBitmap(typeof(CollapsibleSplitter))]
	[DesignerAttribute(typeof(CollapsibleSplitterDesigner))]
	public class CollapsibleSplitter : System.Windows.Forms.Splitter
	{
		#region Private Properties

		// declare and define some base properties
		private static System.Drawing.Color hotColor = CalculateColor(SystemColors.Highlight, SystemColors.Window, 70);
		private System.Windows.Forms.Control controlToHide;
		private System.Drawing.Rectangle hotArea;
		private System.Windows.Forms.Form parentForm;
		private bool expandParentForm=false,useAnimations=false,hot=false;
		private DotStyles dotStyle=DotStyles.Mozilla;
		private int hotZoneArea=115;
		//private int offX=2;
		private int offY=3;
		private int triDotSpace=5;

		//width/height are for triangles pointing left/right
		private int tHeight=6;
		private int tWidth=3;

		// Border added in version 1.3
		private System.Windows.Forms.Border3DStyle borderStyle = System.Windows.Forms.Border3DStyle.Flat;

		// animation controls introduced in version 1.22
		private System.Windows.Forms.Timer animationTimer;
		private int controlWidth,controlHeight,parentFormWidth,parentFormHeight;
		private SplitterState currentState;
		private int animationDelay = 20,animationStep = 20;
		private Brush backBrush;
		private Pen backPen;

		private static Brush hotBrush = new SolidBrush(hotColor);
		private static Pen hotPen = new Pen(hotColor,1);
		private static Brush controlDarkBrush = new SolidBrush(SystemColors.ControlDark);
		private static Pen controlDarkPen=new Pen(SystemColors.ControlDark,1);
		private static Pen controlDarkDarkPen=new Pen(SystemColors.ControlDarkDark,1);
		private static Pen controlLightLightPen=new Pen(SystemColors.ControlLightLight,1);
		private static Pen controlLightPen = new Pen(SystemColors.ControlLight);
		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="T:CollapsibleSplitter"/> class.
		/// </summary>
		public CollapsibleSplitter()
		{
			// Setup the animation timer control
			this.animationTimer = new System.Windows.Forms.Timer();
			this.animationTimer.Interval = animationDelay;
			this.animationTimer.Tick += new System.EventHandler(this.animationTimerTick);

			SetStyle(ControlStyles.AllPaintingInWmPaint|ControlStyles.UserPaint|ControlStyles.DoubleBuffer,true);

			backBrush= new SolidBrush(BackColor);	
			backPen = new Pen(BackColor,1);
			hotArea = new Rectangle(0,((Height - hotZoneArea)/2), Width, hotZoneArea);

			MinimumSize = new Size(5, 5);
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the dot style.
		/// </summary>
		/// <value>The dot style.</value>
		[Category("Custom Appearance"),
		DefaultValue(DotStyles.Mozilla),
		Description("Dot style drawn on the hotzone")]
		public DotStyles DotStyle
		{
			get{return dotStyle;}
			set{dotStyle=value;Refresh();}
		}

		/// <summary>
		/// Gets or sets the background color for the control.
		/// </summary>
		/// <value></value>
		/// <returns>A <see cref="T:System.Drawing.Color"></see> that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor"></see> property.</returns>
		/// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		public new Color BackColor
		{
			get{return base.BackColor;}
			set{base.BackColor=value;backBrush=new SolidBrush(value);backPen=new Pen(value,1);}
		}

		/// <summary>
		/// The initial state of the Splitter. Set to True if the control to hide is not visible by default
		/// </summary>
		[Category("Collapsing Options")]
		[DefaultValue(false)]
		[Description("The initial state of the Splitter. Set to True if the control to hide is not visible by default")]
		public bool IsCollapsed
		{
			get
			{ 
				if(this.controlToHide!= null)
					return !this.controlToHide.Visible; 
				else
					return true;
			}
			set
			{
				if(controlToHide!=null)
					if(controlToHide.Visible==value)
						ToggleSplitter();					
			}
		}

		/// <summary>
		/// The System.Windows.Forms.Control that the splitter will collapse
		/// </summary>
		[Category("Collapsing Options"), DefaultValue("(none)"),
		Description("The System.Windows.Forms.Control that the splitter will collapse")]
		public System.Windows.Forms.Control ControlToHide
		{
			get{ return this.controlToHide; }
			set{ this.controlToHide = value; }
		}

		/// <summary>
		/// Determines if the collapse and expanding actions will be animated
		/// </summary>
		[Category("Collapsing Options"), DefaultValue(false),
		Description("Determines if the collapse and expanding actions will be animated")]
		public bool UseAnimations
		{
			get { return this.useAnimations; }
			set { this.useAnimations = value; }
		}

		/// <summary>
		/// The delay in millisenconds between animation steps
		/// </summary>
		[Category("Collapsing Options"), DefaultValue(20),
		Description("The delay in millisenconds between animation steps")]
		public int AnimationDelay
		{
			get{ return this.animationDelay; }
			set{ this.animationDelay = value; }
		}

		/// <summary>
		/// The amount of pixels moved in each animation step
		/// </summary>
		[Category("Collapsing Options"), DefaultValue(20),
		Description("The amount of pixels moved in each animation step")]
		public int AnimationStep
		{
			get{ return this.animationStep; }
			set{ this.animationStep = value; }
		}

		/// <summary>
		/// When true the entire parent form will be expanded and collapsed, otherwise just the contol to expand will be changed
		/// </summary>
		[Category("Collapsing Options"), DefaultValue(false),
		Description("When true the entire parent form will be expanded and collapsed, otherwise just the contol to expand will be changed")]
		public bool ExpandParentForm
		{
			get{ return this.expandParentForm; }
			set{ this.expandParentForm = value; }
		}

		/// <summary>
		/// An optional border style to paint on the control. Set to Flat for no border
		/// </summary>
		[Category("Custom Appearance"), 
		DefaultValue(System.Windows.Forms.Border3DStyle.Adjust),
		Description("An optional border style to paint on the control")]
		public Border3DStyle BorderStyle3D
		{
			get{ return this.borderStyle; }
			set
			{ 
				switch(value)
				{
					case Border3DStyle.Etched:
						//offX=2;
						break;
					default:
						//offX=1;
						break;
				}
				this.borderStyle = value;
				Refresh();
			}
		}

		/// <summary>
		/// Gets or sets which <see cref="T:System.Windows.Forms.Splitter"></see> borders are docked to its parent control and determines how a <see cref="T:System.Windows.Forms.Splitter"></see> is resized with its parent.
		/// </summary>
		/// <value></value>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle"></see> values. The default is <see cref="F:System.Windows.Forms.DockStyle.Left"></see>.</returns>
		/// <exception cref="T:System.ArgumentException"><see cref="P:System.Windows.Forms.Splitter.Dock"></see> is not set to one of the valid <see cref="T:System.Windows.Forms.DockStyle"></see> values.</exception>
		/// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
		public new DockStyle Dock
		{
			get{return base.Dock;}
			set
			{
				switch(value)
				{
					case DockStyle.Fill:
						goto case DockStyle.None;
					case DockStyle.None:
						throw new Exception("CollapsibleSplitter cannot have a dock of "+value);
					case DockStyle.Left:
						goto case DockStyle.Right;
					case DockStyle.Right:
						Width=10;
						break;
					case DockStyle.Top:
						goto case DockStyle.Bottom;
					case DockStyle.Bottom:
						Height=10;
						break;
				}								
				base.Dock=value;
			}
		}

		/// <summary>
		/// Gets or sets the size of the hotzone.
		/// </summary>
		/// <value>The size of the hotzone.</value>
		[Browsable(true)]
		[DefaultValue(115)]
		[Description("The width/height of the 'hotzone' in pixels")]
		[Category("Custom Appearance")]
		public int HotzoneSize
		{
			get{return hotZoneArea;}
			set{hotZoneArea=value;OnResize(null);}
		}

		#endregion      

		#region Overrides

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated"></see> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			this.parentForm = this.FindForm();

			// set the current state
			if(this.controlToHide != null)
			{
				if(this.controlToHide.Visible)
					this.currentState = SplitterState.Expanded;
				else
					this.currentState = SplitterState.Collapsed;
			}
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown"></see> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"></see> that contains the event data.</param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			// if the hider control isn't hot, let the base resize action occur
			if(this.controlToHide!= null && (!this.hot && this.controlToHide.Visible))
				base.OnMouseDown(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.Resize"></see> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
		protected override void OnResize(System.EventArgs e)
		{
			if(Dock==DockStyle.Left || Dock==DockStyle.Right)
				hotArea = new Rectangle(0,((Height - hotZoneArea)/2),Width, hotZoneArea);
			else
				hotArea = new Rectangle((Width-hotZoneArea)/2,0,hotZoneArea,Height);

			Refresh();
		}

		// this method was updated in version 1.11 to fix a flickering problem
		// discovered by John O'Byrne
		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove"></see> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"></see> that contains the event data.</param>
		protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
		{
			// check to see if the mouse cursor position is within the bounds of our control
			if(e.X >= hotArea.X && e.X <= hotArea.X + hotArea.Width && e.Y >= hotArea.Y && e.Y <= hotArea.Y + hotArea.Height)
			{
				if(!this.hot)
				{
					this.hot = true;
					this.Cursor = Cursors.Hand;
					Refresh();
				}
			}
			else
			{
				if(this.hot)
				{
					this.hot = false;				
					Refresh();
				}

				this.Cursor = Cursors.Default;

				if(controlToHide!= null)
				{
					// Changed in v1.2 to support Horizontal Splitters
					if(controlToHide.Visible)			
					{
						if(this.Dock == DockStyle.Left || this.Dock == DockStyle.Right)
							this.Cursor = Cursors.VSplit;
						else
							this.Cursor = Cursors.HSplit;
					}
				}
			}	
			base.OnMouseMove(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave"></see> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
		protected override void OnMouseLeave(System.EventArgs e)
		{
			// ensure that the hot state is removed
			this.hot = false;
			Refresh();
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.Click"></see> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
		protected override void OnClick(System.EventArgs e)
		{
			if(controlToHide!= null && hot && 
				currentState != SplitterState.Collapsing && 
				currentState != SplitterState.Expanding)
			{
				ToggleSplitter();
			}
		}

		private void ToggleSplitter()
		{
			// if an animation is currently in progress for this control, drop out
			if(currentState == SplitterState.Collapsing || currentState == SplitterState.Expanding)
				return;

			controlWidth = controlToHide.Width;
			controlHeight = controlToHide.Height;

			if(controlToHide.Visible)
			{
				if(useAnimations)
				{
					currentState = SplitterState.Collapsing;

					if(parentForm != null)
					{
						if(this.Dock == DockStyle.Left || this.Dock == DockStyle.Right)
						{
							parentFormWidth = parentForm.Width - controlWidth;
						}
						else
						{
							parentFormHeight = parentForm.Height - controlHeight;
						}
					}

					this.animationTimer.Enabled = true;
				}
				else
				{
					// no animations, so just toggle the visible state
					currentState = SplitterState.Collapsed;
					controlToHide.Visible = false;
					if(expandParentForm && parentForm != null)
					{
						if(this.Dock == DockStyle.Left || this.Dock == DockStyle.Right)
						{
							parentForm.Width -= controlToHide.Width;
						}
						else
						{
							parentForm.Height -= controlToHide.Height;
						}
					}
				}
			}
			else
			{
				// control to hide is collapsed
				if(useAnimations)
				{
					currentState = SplitterState.Expanding;

					if(this.Dock == DockStyle.Left || this.Dock == DockStyle.Right)
					{
						if(parentForm != null)
						{
							parentFormWidth = parentForm.Width + controlWidth;
						}
						controlToHide.Width = 0;
						
					}
					else
					{
						if(parentForm != null)
						{
							parentFormHeight = parentForm.Height + controlHeight;
						}
						controlToHide.Height = 0;
					}
					controlToHide.Visible = true;
					this.animationTimer.Enabled = true;
				}
				else
				{
					// no animations, so just toggle the visible state
					currentState = SplitterState.Expanded;
					controlToHide.Visible = true;
					if(expandParentForm && parentForm != null)
					{
						if(this.Dock == DockStyle.Left || this.Dock == DockStyle.Right)
						{
							parentForm.Width += controlToHide.Width;
						}
						else
						{
							parentForm.Height += controlToHide.Height;
						}
					}
				}
			}
			Refresh();
		}

		#endregion

		#region Implementation

		#region Animation Timer Tick
		private void animationTimerTick(object sender, System.EventArgs e)
		{
			switch(currentState)
			{
				case SplitterState.Collapsing:

					if(this.Dock == DockStyle.Left || this.Dock == DockStyle.Right)
					{
						// vertical splitter
						if(controlToHide.Width > animationStep)
						{
							if(expandParentForm && parentForm.WindowState != FormWindowState.Maximized
								&& parentForm != null)
							{
								parentForm.Width -= animationStep;
							}
							controlToHide.Width -= animationStep;
						}
						else
						{
							if(expandParentForm && parentForm.WindowState != FormWindowState.Maximized
								&& parentForm != null)
							{
								parentForm.Width = parentFormWidth;
							}
							controlToHide.Visible = false;
							animationTimer.Enabled = false;
							controlToHide.Width = controlWidth;
							currentState = SplitterState.Collapsed;
							this.Invalidate();
						}
					}
					else 
					{
						// horizontal splitter
						if(controlToHide.Height > animationStep)
						{
							if(expandParentForm && parentForm.WindowState != FormWindowState.Maximized
								&& parentForm != null)
							{
								parentForm.Height -= animationStep;
							}
							controlToHide.Height -= animationStep;
						}
						else
						{
							if(expandParentForm && parentForm.WindowState != FormWindowState.Maximized
								&& parentForm != null)
							{
								parentForm.Height = parentFormHeight;
							}
							controlToHide.Visible = false;
							animationTimer.Enabled = false;
							controlToHide.Height = controlHeight;
							currentState = SplitterState.Collapsed;
							this.Invalidate();
						}
					}
					break;

				case SplitterState.Expanding:

					if(this.Dock == DockStyle.Left || this.Dock == DockStyle.Right)
					{
						// vertical splitter
						if(controlToHide.Width < (controlWidth - animationStep))
						{
							if(expandParentForm && parentForm.WindowState != FormWindowState.Maximized
								&& parentForm != null)
							{
								parentForm.Width += animationStep;
							}
							controlToHide.Width += animationStep;
						}
						else
						{
							if(expandParentForm && parentForm.WindowState != FormWindowState.Maximized
								&& parentForm != null)
							{
								parentForm.Width = parentFormWidth;
							}
							controlToHide.Width = controlWidth;
							controlToHide.Visible = true;
							animationTimer.Enabled = false;
							currentState = SplitterState.Expanded;
							this.Invalidate();
						}
					}
					else 
					{
						// horizontal splitter
						if(controlToHide.Height < (controlHeight - animationStep))
						{
							if(expandParentForm && parentForm.WindowState != FormWindowState.Maximized
								&& parentForm != null)
							{
								parentForm.Height += animationStep;
							}
							controlToHide.Height += animationStep;
						}
						else
						{
							if(expandParentForm && parentForm.WindowState != FormWindowState.Maximized
								&& parentForm != null)
							{
								parentForm.Height = parentFormHeight;
							}
							controlToHide.Height = controlHeight;
							controlToHide.Visible = true;
							animationTimer.Enabled = false;
							currentState = SplitterState.Expanded;
							this.Invalidate();
						}

					}
					break;
			}
		}

		#endregion

		// OnPaint is now an override rather than an event in version 1.1
		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.Paint"></see> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"></see> that contains the event data.</param>
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			// get the graphics object
			Graphics g = e.Graphics;
			
			/********** Vertical splitter ********/
			if(this.Dock == DockStyle.Left || this.Dock == DockStyle.Right)
			{		
				if(hot)
					g.FillRectangle(hotBrush,hotArea);
				//else
				//	g.FillRectangle(backBrush, hotArea);

				// draw the top & bottom lines for our control image
				g.DrawLine(controlDarkPen, hotArea.X, hotArea.Y, hotArea.X + hotArea.Width, hotArea.Y);
				g.DrawLine(controlDarkPen, hotArea.X, hotArea.Y + hotArea.Height, hotArea.X + hotArea.Width, hotArea.Y + hotArea.Height);

				if(this.Enabled && controlToHide!=null)
				{
					// draw the arrows for our control image
					GraphicsPath leftRight=new GraphicsPath();
					if(	(Dock==DockStyle.Right && controlToHide.Visible) || 
						(Dock==DockStyle.Left && !controlToHide.Visible))
					{
						leftRight.AddLine((Width-tWidth)/2,hotArea.Y+offY,(Width-tWidth)/2+tWidth,hotArea.Y+offY+tHeight/2);
						leftRight.AddLine((Width-tWidth)/2+tWidth,hotArea.Y+offY+tHeight/2,(Width-tWidth)/2,hotArea.Y+offY+tHeight);
						leftRight.CloseFigure();

						leftRight.AddLine((Width-tWidth)/2,hotArea.Y+hotArea.Height-offY,(Width-tWidth)/2,hotArea.Y+hotArea.Height-offY-tHeight);
						leftRight.AddLine((Width-tWidth)/2,hotArea.Y+hotArea.Height-offY-tHeight,(Width-tWidth)/2+tWidth,hotArea.Y+hotArea.Height-(offY+tHeight/2));
						leftRight.CloseFigure();					
					}
					else
					{
						leftRight.AddLine((Width-tWidth)/2+tWidth,hotArea.Y+offY,(Width-tWidth)/2,hotArea.Y+offY+tHeight/2);
						leftRight.AddLine((Width-tWidth)/2,hotArea.Y+offY+tHeight/2,(Width-tWidth)/2+tWidth,hotArea.Y+offY+tHeight);
						leftRight.CloseFigure();

						leftRight.AddLine((Width-tWidth)/2+tWidth,hotArea.Y+hotArea.Height-offY,(Width-tWidth)/2+tWidth,hotArea.Y+hotArea.Height-offY-tHeight);
						leftRight.AddLine((Width-tWidth)/2+tWidth,hotArea.Y+hotArea.Height-offY-tHeight,(Width-tWidth)/2,hotArea.Y+hotArea.Height-(offY+tHeight/2));
						leftRight.CloseFigure();	
					}
					g.FillPath(controlDarkBrush,leftRight);					
				}

				// draw the dots for our control image using a loop
				int x = Width/2-1;
				int y = hotArea.Y+offY+tHeight+triDotSpace;
				int dotSpace=3;

				// Visual Styles added in version 1.1
				switch(dotStyle)
				{
					case DotStyles.Mozilla:
						while(y<hotArea.Y+hotArea.Height-(offY+tHeight+triDotSpace))
						{
							// light dot
							g.DrawLine(controlLightLightPen, x, y , x+1, y + 1);
							// dark dot
							g.DrawLine(controlDarkDarkPen, x+1, y + 1, x+2, y + 2);
							// overdraw the background color as we actually drew 2px diagonal lines, not just dots
							if(hot)
								g.DrawLine(hotPen, x+2, y + 1, x+2, y + 2);
							else
								g.DrawLine(backPen, x+2, y + 1, x+2, y + 2);
							y+=dotSpace;
						}
						break;

					case DotStyles.DoubleDots:
						while(y<hotArea.Y+hotArea.Height-(offY+tHeight+triDotSpace))
						{
							// light dot
							g.DrawRectangle(controlLightLightPen, x, y + 1, 1, 1 );
							// dark dot
							g.DrawRectangle(controlDarkPen, x - 1, y, 1, 1 );
							y+=dotSpace;
							// light dot
							g.DrawRectangle(controlLightLightPen, x + 2, y + 1, 1, 1 );
							// dark dot
							g.DrawRectangle(controlDarkPen, x + 1, y, 1, 1 );
							y+=dotSpace;
						}
						break;

					case DotStyles.Win9x:
						g.DrawLine(controlLightLightPen, x, y, x + 2, y);
						g.DrawLine(controlLightLightPen, x, y, x,hotArea.Y+hotArea.Height-(offY+tHeight+triDotSpace));
						g.DrawLine(controlDarkPen, x + 2, y, x + 2, hotArea.Y+hotArea.Height-(offY+tHeight+triDotSpace));
						g.DrawLine(controlDarkPen, x, hotArea.Y+hotArea.Height-(offY+tHeight+triDotSpace), x + 2, hotArea.Y+hotArea.Height-(offY+tHeight+triDotSpace));
						break;

					case DotStyles.XP:
						dotSpace=5;
						while(y<hotArea.Y+hotArea.Height-(offY+tHeight+triDotSpace))
						{
							// light dot
							g.DrawRectangle(controlLightPen, x, y , 2, 2 );
							// light light dot
							g.DrawRectangle(controlLightLightPen, x + 1, y + 1, 1, 1 );
							// dark dark dot
							g.DrawRectangle(controlDarkDarkPen, x, y, 1, 1 );
							// dark fill
							g.DrawLine(controlDarkPen, x, y , x, y + 1);
							g.DrawLine(controlDarkPen, x, y , x + 1, y );
							y+=dotSpace;
						}
						break;

					case DotStyles.Lines:
						dotSpace=2;
						while(y<hotArea.Y+hotArea.Height-(offY+tHeight+triDotSpace))
						{
							g.DrawLine(controlDarkDarkPen, x, y , x + 2, y);
							y+=dotSpace;
						}
						break;
				}
			}
				// Horizontal Splitter support added in v1.2
			else if (this.Dock == DockStyle.Top || this.Dock == DockStyle.Bottom)
			{				
				// draw the background color for our control image
				if(hot)
					g.FillRectangle(new SolidBrush(hotColor), hotArea);
				else
					g.FillRectangle(new SolidBrush(this.BackColor), hotArea);

				// draw the left & right lines for our control image
				g.DrawLine(new Pen(SystemColors.ControlDark, 1), hotArea.X, hotArea.Y + 1, hotArea.X, hotArea.Y + hotArea.Height - 2);
				g.DrawLine(new Pen(SystemColors.ControlDark, 1), hotArea.X + hotArea.Width, hotArea.Y + 1, hotArea.X + hotArea.Width, hotArea.Y + hotArea.Height - 2);

				if(this.Enabled && controlToHide!=null)
				{
					// draw the arrows for our control image
					// the ArrowPointArray is a point array that defines an arrow shaped polygon
//					g.FillPolygon(new SolidBrush(SystemColors.ControlDarkDark), ArrowPointArray(hotArea.X + 3, hotArea.Y + 2,Dock,controlToHide.Visible));
//					g.FillPolygon(new SolidBrush(SystemColors.ControlDarkDark), ArrowPointArray(hotArea.X +  hotArea.Width - 9, hotArea.Y + 2,Dock,controlToHide.Visible));

					// draw the arrows for our control image
					/*GraphicsPath upDown=new GraphicsPath();
					if(	(Dock==DockStyle.Bottom && controlToHide.Visible) || //point down
						(Dock==DockStyle.Top && !controlToHide.Visible))
					{
						upDown.AddLine(hotArea.X+offX,(Height+tHeight)/2,hotArea.X+offX+tWidth*2,(Height+tHeight)/2);
						upDown.AddLine(hotArea.X+offX+tWidth*2,(Height+tHeight)/2,hotArea.X+offX+tWidth,(Height+tHeight)/2+tHeight);
						upDown.CloseFigure();

						upDown.AddLine((Width-tWidth)/2,hotArea.Y+hotArea.Height-offY,(Width-tWidth)/2,hotArea.Y+hotArea.Height-offY-tHeight);
						upDown.AddLine((Width-tWidth)/2,hotArea.Y+hotArea.Height-offY-tHeight,(Width-tWidth)/2+tWidth,hotArea.Y+hotArea.Height-(offY+tHeight/2));
						upDown.CloseFigure();					
					}
					else
					{
						upDown.AddLine((Width-tWidth)/2+tWidth,hotArea.Y+offY,(Width-tWidth)/2,hotArea.Y+offY+tHeight/2);
						upDown.AddLine((Width-tWidth)/2,hotArea.Y+offY+tHeight/2,(Width-tWidth)/2+tWidth,hotArea.Y+offY+tHeight);
						upDown.CloseFigure();

						upDown.AddLine((Width-tWidth)/2+tWidth,hotArea.Y+hotArea.Height-offY,(Width-tWidth)/2+tWidth,hotArea.Y+hotArea.Height-offY-tHeight);
						upDown.AddLine((Width-tWidth)/2+tWidth,hotArea.Y+hotArea.Height-offY-tHeight,(Width-tWidth)/2,hotArea.Y+hotArea.Height-(offY+tHeight/2));
						upDown.CloseFigure();	
					}
					g.FillPath(controlDarkBrush,upDown);*/	
				}

				// draw the dots for our control image using a loop
				int x = hotArea.X + 14;
				int y = hotArea.Y + 3;

				// Visual Styles added in version 1.1
				switch(dotStyle)
				{
					case DotStyles.Mozilla:

						for(int i=0; i < 30; i++)
						{
							// light dot
							g.DrawLine(new Pen(SystemColors.ControlLightLight), x + (i*3), y, x + 1 + (i*3), y + 1);
							// dark dot
							g.DrawLine(new Pen(SystemColors.ControlDarkDark), x + 1 + (i*3), y + 1, x + 2 + (i*3), y + 2);
							// overdraw the background color as we actually drew 2px diagonal lines, not just dots
							if(hot)
								g.DrawLine(new Pen(hotColor), x + 1 + (i*3), y + 2, x + 2 + (i*3), y + 2);
							else
								g.DrawLine(new Pen(this.BackColor),  x + 1 + (i*3), y + 2, x + 2 + (i*3), y + 2);
						}
						break;

					case DotStyles.DoubleDots:
						for(int i=0; i < 30; i++)
						{
							// light dot
							g.DrawRectangle(new Pen(SystemColors.ControlLightLight), x + 1 + (i*3), y, 1, 1 );
							// dark dot
							g.DrawRectangle(new Pen(SystemColors.ControlDark), x + (i*3), y - 1, 1, 1 );
							i++;
							// light dot
							g.DrawRectangle(new Pen(SystemColors.ControlLightLight), x + 1 + (i*3), y + 2, 1, 1 );
							// dark dot
							g.DrawRectangle(new Pen(SystemColors.ControlDark), x + (i*3), y + 1, 1, 1 );
						}
						break;

					case DotStyles.Win9x:
						g.DrawLine(new Pen(SystemColors.ControlLightLight), x, y, x, y + 2);
						g.DrawLine(new Pen(SystemColors.ControlLightLight), x, y, x + 88, y);
						g.DrawLine(new Pen(SystemColors.ControlDark), x, y + 2, x + 88, y + 2);
						g.DrawLine(new Pen(SystemColors.ControlDark), x + 88, y, x + 88, y + 2);
						break;

					case DotStyles.XP:
						for(int i=0; i < 18; i++)
						{
							// light dot
							g.DrawRectangle(new Pen(SystemColors.ControlLight), x + (i*5), y, 2, 2 );
							// light light dot
							g.DrawRectangle(new Pen(SystemColors.ControlLightLight), x + 1 + (i*5), y + 1, 1, 1 );
							// dark dark dot
							g.DrawRectangle(new Pen(SystemColors.ControlDarkDark), x +(i*5), y, 1, 1 );
							// dark fill
							g.DrawLine(new Pen(SystemColors.ControlDark), x + (i*5), y, x + (i*5) + 1, y);
							g.DrawLine(new Pen(SystemColors.ControlDark), x + (i*5), y, x + (i*5), y + 1);
						}
						break;

					case DotStyles.Lines:
						for(int i=0; i < 44; i++)
							g.DrawLine(new Pen(SystemColors.ControlDark), x + (i*2), y, x + (i*2), y + 2);
						break;
				}
			}

			//if(this.borderStyle != System.Windows.Forms.Border3DStyle.Flat)
			ControlPaint.DrawBorder3D(e.Graphics,ClientRectangle,borderStyle);			
		}

		// This creates a point array to draw a arrow-like polygon
//		private static GraphicsPath ArrowPointArray(int x, int y,DockStyle dock,bool visible)
//		{
//			Point[] point = new Point[3];
//
//			// decide which direction the arrow will point
//			if ( (dock == DockStyle.Right && visible) || 
//				(dock == DockStyle.Left && !visible))
//			{
//				// right arrow
//				point[0] = new Point(x,y);
//				point[1] = new Point(x + tWidth, y + tHeight/2);
//				point[2] = new Point(x, y + tHeight);
//			}
//			else if ( (dock == DockStyle.Right && !visible) || 
//				(dock == DockStyle.Left && visible))
//			{
//				// left arrow
//				point[0] = new Point(x + tWidth ,y);
//				point[1] = new Point(x, y + tHeight/2);
//				point[2] = new Point(x + tWidth, y + tHeight);
//			}
//				// Up/Down arrows added in v1.2
//			else if ( (dock == DockStyle.Top && visible) || 
//				(dock == DockStyle.Bottom && !visible))
//			{
//				// up arrow
//				point[0] = new Point(x + tHeight/2, y);
//				point[1] = new Point(x + tHeight, y + tWidth);
//				point[2] = new Point(x, y + tWidth);
//			}
//			else if ( (dock == DockStyle.Top && !visible) ||
//				(dock == DockStyle.Bottom && visible))
//			{
//				// down arrow
//				point[0] = new Point(x,y);
//				point[1] = new Point(x + tHeight, y);
//				point[2] = new Point(x + tHeight/2, y + tWidth);
//			}
//
//			return point;
//		}

		// this method was borrowed from the RichUI Control library by Sajith M
		private static Color CalculateColor(Color front, Color back, int alpha)
		{	
			// solid color obtained as a result of alpha-blending

			Color frontColor = Color.FromArgb(255, front);
			Color backColor = Color.FromArgb(255, back);
										
			float frontRed = frontColor.R;
			float frontGreen = frontColor.G;
			float frontBlue = frontColor.B;
			float backRed = backColor.R;
			float backGreen = backColor.G;
			float backBlue = backColor.B;
				
			float fRed = frontRed*alpha/255 + backRed*((float)(255-alpha)/255);
			byte newRed = (byte)fRed;
			float fGreen = frontGreen*alpha/255 + backGreen*((float)(255-alpha)/255);
			byte newGreen = (byte)fGreen;
			float fBlue = frontBlue*alpha/255 + backBlue*((float)(255-alpha)/255);
			byte newBlue = (byte)fBlue;

			return  Color.FromArgb(newRed, newGreen, newBlue);
		}
	        

		#endregion
	}

	/// <summary>
	/// A simple designer class for the CollapsibleSplitter control to remove 
	/// unwanted properties at design time.
	/// </summary>
	public class CollapsibleSplitterDesigner : System.Windows.Forms.Design.ControlDesigner
	{
		/// <summary>
		/// 
		/// </summary>
		public CollapsibleSplitterDesigner()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="properties"></param>
		protected override void PreFilterProperties(System.Collections.IDictionary properties)
		{
			properties.Remove("BorderStyle");
		}
	}
}

