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
    public abstract class DoubleBufferControl : Control
    {
        private BufferedGraphicsContext graphicManager;
        private BufferedGraphics managedBackBuffer;

        public DoubleBufferControl()
        {
            this.SuspendLayout();
            this.BackColor = System.Drawing.Color.Black;
            this.Resize += new System.EventHandler(this.doubleBufferControl_Resize);
            this.ResumeLayout(false);

			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);

            graphicManager = BufferedGraphicsManager.Current;
            graphicManager.MaximumBuffer = new Size(this.Width + 1, this.Height + 1);
            managedBackBuffer = graphicManager.Allocate(this.CreateGraphics(), ClientRectangle);

            Application.ApplicationExit += new EventHandler(memoryCleanup);
        }

        private void memoryCleanup(object sender, EventArgs e)
        {
            if (managedBackBuffer != null)
                managedBackBuffer.Dispose();
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
                Render(managedBackBuffer.Graphics);

                // paint the picture in from the back buffer into the form draw area
                managedBackBuffer.Render(e.Graphics);
            }
            catch (Exception ex) 
			{
				Console.WriteLine(ex.Message+"\n"+ex.StackTrace); 
			}
        }

        protected abstract void Render(Graphics backBuffer);

        private void removePaintMethods()
        {
            this.DoubleBuffered = false;

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, false);

            if (managedBackBuffer != null)
                managedBackBuffer.Dispose();
        }

        private void doubleBufferControl_Resize(object sender, EventArgs e)
        {
            graphicManager.MaximumBuffer = new Size(this.Width + 1, this.Height + 1);

            if (managedBackBuffer != null)
                managedBackBuffer.Dispose();

            managedBackBuffer = graphicManager.Allocate(this.CreateGraphics(), ClientRectangle);

            this.Refresh();
        }

        /*private void LunchGraphicTest(Graphics TempGraphics)
        {
            int i;
            Random Rnd = new Random();
            Pen BlackPen = new Pen(new SolidBrush(Color.Black));
            Pen ColorPen = null;
            Rectangle TempRectangle;
            LinearGradientBrush ColorBrush = null;

            TempGraphics.Clear(Color.Wheat);

            switch (GraphicTest)
            {
                case GraphicTestMethods.DrawTest:
                    for (i = 0; i < 100; i++)
                    {
                        TempRectangle = new Rectangle(
                            Rnd.Next(0, Width),
                            Rnd.Next(0, Height),
                            Width - i,
                            Height - i);

                        ColorPen = new Pen(Color.FromArgb(127, Rnd.Next(0, 256), Rnd.Next(256), Rnd.Next(256)));
                        TempGraphics.DrawRectangle(ColorPen, TempRectangle);
                    }

                    ColorPen.Dispose();
                    break;

                case GraphicTestMethods.FillTest:
                    for (i = 0; i < 100; i++)
                    {
                        TempRectangle = new Rectangle(
                            Rnd.Next(0, Width),
                            Rnd.Next(0, Height),
                            Width - i,
                            Height - i);

                        ColorBrush = new LinearGradientBrush(
                                TempRectangle,
                                Color.FromArgb(127, Rnd.Next(0, 256), Rnd.Next(256), Rnd.Next(256)),
                                Color.FromArgb(127, Rnd.Next(0, 256), Rnd.Next(256), Rnd.Next(256)),
                                (LinearGradientMode)Rnd.Next(3));

                        TempGraphics.FillEllipse(ColorBrush, TempRectangle);

                    }

                    ColorBrush.Dispose();
                    break;
            }
        }*/
    }
}
