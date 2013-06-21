#define windowed

#if DIRECTX
using System;
using Microsoft.DirectX;
using Microsoft.DirectX.DirectDraw;
using System.Windows.Forms;
using System.Drawing;

namespace XCom
{
	public class GraphicsControl
	{
		protected Form target           = null;
		protected Device  graphicsDevice   = null;
		//protected Clipper graphicsClipper  = null;
		protected Surface surfacePrimary   = null;
		protected Surface surfaceSecondary = null; 
		private Rectangle srcRect;

		private static GraphicsControl instance;

		public static GraphicsControl Init(Form RenderControl)
		{
			instance = new GraphicsControl(RenderControl);
			return instance;
		}

		public static GraphicsControl GetInstance()
		{
			return instance;
		}

		/// <summary>
		/// Constructor. Initializes DirectDraw Device
		/// and surfaces.
		/// </summary>
		/// <param name="RenderControl">
		/// The control the device is connected to.
		/// </param> 
		private GraphicsControl(Form RenderControl)
		{
			// Save reference to target control
			this.target = RenderControl; 
			// Create DirectDraw device
			graphicsDevice = new Device();

#if windowed
			// In debug mode, use windowed level
			graphicsDevice.SetCooperativeLevel(this.target, 
				CooperativeLevelFlags.Normal);
#else
			// In release mode, use fullscreen...
			graphicsDevice.SetCooperativeLevel(this.target, 
			CooperativeLevelFlags.FullscreenExclusive);

			// ...and 640x480x16 resolution with 85Hz frequency
			graphicsDevice.SetDisplayMode(640, 480, 32, 85, true);
#endif

			// create surfaces
			this.CreateSurfaces();
		} 

		/// <summary>
		/// This method creates the primary and secondary surfaces
		/// </summary>
		public void CreateSurfaces()
		{
			// Every surface needs a description
			// This is where you set the parameters for the surface
			SurfaceDescription desc = new SurfaceDescription();
  
			// This is the clipper for the primary surface
			// -> connect it to the target control
			Clipper graphicsClipper = new Clipper(graphicsDevice);
			graphicsClipper.Window = target;

			// First we want to create a primary surface
			desc.SurfaceCaps.PrimarySurface = true;

#if !windowed
			// In release mode, we enable flipping, set the complex
			// flag and tell the surface that we will use one back
			// buffer
			desc.SurfaceCaps.Flip = true;
			desc.SurfaceCaps.Complex = true;
			desc.BackBufferCount = 1;
#endif

			// Create the surface
			surfacePrimary = new Surface(desc, graphicsDevice);
			srcRect = new Rectangle(0,0,target.Width,target.Height);

			// Attach clipper to the surface
			surfacePrimary.Clipper = graphicsClipper;

			// To build the secondary surface, we need 
			// a new description -> clear all values
			desc.Clear();

#if windowed
			// In debug mode, we simply copy the primary surfaces
			// dimensions and create a offscreenplain secondary
			// surface
			desc.Width = surfacePrimary.SurfaceDescription.Width;
			desc.Height = surfacePrimary.SurfaceDescription.Height;
			desc.SurfaceCaps.OffScreenPlain = true;
			surfaceSecondary = new Surface(desc, this.graphicsDevice);
#else
			// In release mode, we set the backbuffer flag to true
			// and retrieve a backbuffer surface from the primary
			// surface
			desc.SurfaceCaps.BackBuffer = true;
			surfaceSecondary = surfacePrimary.GetAttachedSurface(desc.SurfaceCaps);
#endif

			//surfaceSecondary.Clipper=graphicsClipper;
		} 

		/// <summary>
		/// This method flips the secondary surface to the
		/// primary one, thus drawing its content to the screen.
		/// </summary>
		public void Flip()
		{
			// First we check if the target control
			// is created yet.
			if (!this.target.Created || target.WindowState==FormWindowState.Minimized)
			{
				return;
			}

			// Now check if both surfaces are there.
			if (surfacePrimary == null || surfaceSecondary == null)
			{
				Console.WriteLine("surface not here");
				return;
			}			

			// Try to Draw or Flip, depending on compile mode
			try
			{
#if windowed
				
				//Point dest = target.PointToScreen(new Point(0,0));

				//surfacePrimary.DrawFast(dest.X,dest.Y,surfaceSecondary,srcRect, DrawFastFlags.Wait);

				Rectangle dest = new Rectangle(target.PointToScreen(new Point(0,0)), target.ClientSize);
				surfacePrimary.Draw(dest,surfaceSecondary,srcRect, DrawFlags.Wait);
#else
				surfacePrimary.Flip(surfaceSecondary, FlipFlags.Wait);
#endif
			}
			catch (SurfaceLostException)
			{
				// On activation of power saving mode 
				// and in other situations we may lose the surfaces
				// and have to recreate them
				this.CreateSurfaces();
				Console.WriteLine("Surface lost");
			}
		} 



		/// <summary>
		/// The DirectDraw Device
		/// </summary>
		public Device DDDevice
		{
			get
			{
				return graphicsDevice;
			}
		}

		/// <summary>
		/// This surface can be accessed to render to.
		/// </summary>
		public Surface RenderSurface
		{
			get
			{
				return surfaceSecondary;
			}
		} 

		public Control Control
		{
			get{return target;}
		}
	}
}

#endif
