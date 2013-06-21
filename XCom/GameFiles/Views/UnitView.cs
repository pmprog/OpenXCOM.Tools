using System;
using System.Windows.Forms;
using System.Drawing;

namespace XCom
{
	public class UnitView:Panel
	{/*
		private IUnit currUnit;
		private const int width = PckImage.IMAGE_WIDTH;
		private const int height = PckImage.IMAGE_HEIGHT;
		private MapFile preview;
		private int curr;
		private const int hWid=16;
		private const int hHeight=8;
		private int h;

		public UnitView()
		{
			currUnit=null;
			this.SetStyle(ControlStyles.DoubleBuffer|ControlStyles.AllPaintingInWmPaint|ControlStyles.UserPaint,true);
			curr=0;
		}

		public ITileset Ground
		{
			set
			{
				if(value[value.Name+"PV"]!=null)
				{
					preview = value[value.Name+"PV"].GetMapFile(Palette.TFTDPalette,null);
					Width = (preview.Rows+preview.Cols+1)*hWid;
					Height = 24+(preview.Rows+preview.Cols+1)*hHeight;
					h=preview.Height-1;
				}
				else
					preview=null;
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics g = e.Graphics;
			if(preview!=null)
			{
				for(int row=0,startX=preview.Rows*hWid,startY=0;row<preview.Rows;row++,startX-=hWid,startY+=hHeight)
				{
					for(int col=0,x=startX,y=startY;col<preview.Cols;col++,x+=hWid,y+=hHeight)
					{
						preview[row,col,h].Drawgfx(x,y,g,curr);
						if(currUnit!=null)
							if(row==currUnit.CurrentLocation.Row && col==currUnit.CurrentLocation.Col)
								currUnit.Drawgfx(g,x,y);
					}
				}
			}
			else if(currUnit!=null)
				currUnit.Drawgfx(g,0,0);
		}

		private void unitChange(object sender, EventArgs e)
		{	
			Refresh();
		}	

		public IUnit CurrUnit
		{
			get{return currUnit;}
			set
			{
				if(value!=null)
				{
					currUnit=value;
					if(preview!=null)
						currUnit.CurrentLocation=new Point3D(preview.Rows/2,preview.Cols/2,preview.Height-1);
					Refresh();
				}
			}
		}*/
	}
}
