using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MapView
{
    /// <summary>
    /// Class that starts program execution.
    /// </summary>
    public class Startup : MarshalByRefObject
    {
        public void RunProgram()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
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
    }
}
