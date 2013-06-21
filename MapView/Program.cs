using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;

namespace MapView
{
	static class Program
	{
		[STAThread]
		public static void Main()
		{
			// Construct and initialize settings for a second AppDomain.
#if RELEASE
            AppDomainSetup ads = new AppDomainSetup();
            ads.ApplicationBase =
                System.Environment.CurrentDirectory;
            ads.DisallowBindingRedirects = false;
            ads.DisallowCodeDownload = true;
            ads.ConfigurationFile =
                AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;

            // Create the second AppDomain.
            AppDomain ad2 = AppDomain.CreateDomain("MV Domain", null, ads);

            // Create an instance of MarshalbyRefType in the second AppDomain. 
            // A proxy to the object is returned.
            Startup startup =
                (Startup)ad2.CreateInstanceAndUnwrap(
                    Assembly.GetEntryAssembly().FullName,
                    typeof(Startup).FullName
                );

            startup.RunProgram();

            Console.WriteLine("Disposing of appdomain");
            AppDomain.Unload(ad2);
#else
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainWindow());
#endif
		}


		/*		[STAThread]
		static void Main()
		{
			//#if !DEBUG
			//			try
			//			{
			//				Application.Run(new MainWindow());
			//			}
			//			catch(Exception e)
			//			{
			//				xConsole.AddLine("There was an error: "+e.Message+"\n"+e.StackTrace);
			//				xConsole.AddLine("Type OK to close");
			//				xConsole.Instance.ExternalCall+=new ExternalCallDelegate(myQuit);
			//				Application.Run(xConsole.Instance);
			//			}
			//#else
			try
			{
				Application.Run(new MainWindow());
			}
			catch (Exception ex)
			{
				MessageBox.Show("Unhandled exception, set the line logFile:true in your paths.pth and include the output from debug.log and this error message in your bug report. Thanks =)\n" + ex.Message + "\n" + ex.StackTrace);
			}
			//#endif
		}*/
	}
}