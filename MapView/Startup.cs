using System;
using System.Threading;
using System.Windows.Forms;
using MapView.Forms.Error;

namespace MapView
{
    /// <summary>
    /// Class that starts program execution.
    /// </summary>
    public class Startup : MarshalByRefObject
    { 
        private readonly IErrorHandler _errorHandler;

        public Startup()
        {
            _errorHandler = new ErrorWindowAdapter();
        }

        public void RunProgram()
        {
            Application.EnableVisualStyles(); 
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += Application_ThreadException;

            MainWindow mw = new MainWindow();
           // mw.SendMessage += new StringDelegate(mw_SendMessage);

            Application.Run(mw);

            // Get this AppDomain's settings and display some of them.
            //AppDomainSetup ads = AppDomain.CurrentDomain.SetupInformation;
            //Console.WriteLine("AppName={0}, AppBase={1}, ConfigFile={2}",
            //    ads.ApplicationName,
            //    ads.ApplicationBase,
            //    ads.ConfigurationFile
            //);
        }

        void mw_SendMessage(object sender, string args)
        {
            Console.WriteLine("External command: " + args);
        }

        private void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            _errorHandler.HandleException(e.Exception);
        }
    }
}
