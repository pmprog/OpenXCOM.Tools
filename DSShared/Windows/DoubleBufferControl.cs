using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DSShared.Windows
{
	public class DoubleBufferControl : Control
	{
		public DoubleBufferControl()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if (DesignMode)
			{
				base.OnPaint(e);
				e.Graphics.DrawLine(Pens.Black, 0, 0, Width, Height);
				e.Graphics.DrawLine(Pens.Black, 0, Height, Width, 0);
				ControlPaint.DrawBorder3D(e.Graphics, ClientRectangle, Border3DStyle.Flat);
				return;
			}

			Render(e.Graphics);
		}

		protected virtual void Render(Graphics backBuffer) { }
	}
	/*
    public class DoubleBufferControl : Control
    {
        private BufferedGraphicsContext context;
        private BufferedGraphics backBuffer;

        public DoubleBufferControl()
        {
            this.SuspendLayout();
            this.BackColor = System.Drawing.Color.Black;
            this.Resize += new System.EventHandler(this.doubleBufferControl_Resize);
            this.ResumeLayout(false);

			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);

            context = BufferedGraphicsManager.Current;
            context.MaximumBuffer = new Size(this.Width + 1, this.Height + 1);
            backBuffer = context.Allocate(this.CreateGraphics(), ClientRectangle);

            Application.ApplicationExit += new EventHandler(memoryCleanup);
        }

		private void memoryCleanup(object sender, EventArgs e)
		{
			if (backBuffer != null)
				backBuffer.Dispose();
		}

        protected override void OnPaint(PaintEventArgs e)
        {
            if (DesignMode) 
			{ 
				base.OnPaint(e); 
				e.Graphics.DrawLine(Pens.Black,0,0,Width,Height);
				e.Graphics.DrawLine(Pens.Black,0,Height,Width,0);
				return; 
			}

            try
            {
                //draw to back buffer
                Render(backBuffer.Graphics);

                // paint the picture in from the back buffer into the form draw area
                backBuffer.Render(e.Graphics);
            }
            catch (Exception ex) 
			{
				Console.WriteLine(ex.Message+"\n"+ex.StackTrace); 
			}
        }

		protected virtual void Render(Graphics backBuffer) { }

        private void removePaintMethods()
        {
            this.DoubleBuffered = false;

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, false);

            if (backBuffer != null)
                backBuffer.Dispose();
        }

        private void doubleBufferControl_Resize(object sender, EventArgs e)
        {
            context.MaximumBuffer = new Size(this.Width + 1, this.Height + 1);

            if (backBuffer != null)
                backBuffer.Dispose();

            backBuffer = context.Allocate(this.CreateGraphics(), ClientRectangle);

            this.Refresh();
        }
    }*/
}
