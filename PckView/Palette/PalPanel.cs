using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using XCom;

namespace PckView
{
	public delegate void PaletteClickDelegate(int selectedIndex);

	public enum SelectMode{Bar,Single};

	public class PalPanel : Panel
	{
		private Palette myPal;
		private SolidBrush goodBrush = new SolidBrush(Color.FromArgb(204,204,255));
		private const int space=0;
		private int height=10;
		private int width=15;

		private int selIdx;
		private int clickX,clickY;
		private SelectMode mode;

		public const int NumAcross=16;

		public event PaletteClickDelegate PaletteIndexChanged;

		public PalPanel()
		{
			this.SetStyle(ControlStyles.DoubleBuffer|ControlStyles.UserPaint|ControlStyles.AllPaintingInWmPaint,true);
			myPal = null;
			this.MouseDown+=new MouseEventHandler(mouseDown);
			//Width=(width+2*space)*NumAcross;
			//Height=(height+2*space)*NumAcross;
			clickX=-100;
			clickY=-100;
			selIdx=-1;
			mode = SelectMode.Single;
		}

		protected override void OnResize(EventArgs e)
		{
			width = (Width/NumAcross)-2*space;
			height = (Height/NumAcross)-2*space;

			switch(mode)
			{
				case SelectMode.Single:
					clickX = (selIdx%NumAcross)*(width+2*space);
					break;
				case SelectMode.Bar:
					clickX = 0;					
					break;
			}
			clickY = (selIdx/NumAcross)*(height+2*space);

			Refresh();
		}

		private void mouseDown(object sender, MouseEventArgs e)
		{
			switch(mode)
			{
				case SelectMode.Single:
					clickX = (e.X/(width+2*space))*(width+2*space);
					selIdx =  (e.X/(width+2*space))+(e.Y/(height+2*space))*NumAcross;
					break;
				case SelectMode.Bar:
					clickX = 0;					
					selIdx = (e.Y/(height+2*space))*NumAcross;
					break;
			}

			clickY = (e.Y/(height+2*space))*(height+2*space);

			if(PaletteIndexChanged!=null && selIdx<=255)
			{
				PaletteIndexChanged(selIdx);
				Refresh();
			}
		}

		[DefaultValue(SelectMode.Single)]
		[Category("Behavior")]
		public SelectMode Mode
		{
			get{return mode;}
			set{mode=value;}
		}

		[DefaultValue(null)]
		[Browsable(false)]
		public Palette Palette
		{
			get{return myPal;}
			set{myPal=value;Refresh();}
		}	

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if(myPal != null)
			{
				Graphics g = e.Graphics;

				for(int i=0,y=space;i<NumAcross;i++,y+=(height+2*space))
					for(int j=0,x=space;j<NumAcross;j++,x+=(width+2*space))
						g.FillRectangle(new SolidBrush(myPal[i*NumAcross+j]),x,y,width,height);

				switch(mode)
				{
					case SelectMode.Single:
						//g.FillRectangle(goodBrush,clickX,clickY,width+2*space-1,height+2*space-1);
						g.DrawRectangle(Pens.Red,clickX,clickY,width+2*space-1,height+2*space-1);
						break;
					case SelectMode.Bar:
						//g.FillRectangle(goodBrush,clickX,clickY,(width+2*space)*NumAcross-1,height+2*space-1);
						g.DrawRectangle(Pens.Red,clickX,clickY,(width+2*space)*NumAcross-1,height+2*space-1);
						break;
				}
			}
		}
	}
}
