using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AutoServeJobsManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.Run(new mainForm());
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show((e.ExceptionObject as Exception).Message, "Unhandled UI Exception",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            System.IO.File.AppendAllText("error_log.log", "\n====================\nUnhandled UI Exception\n" + "\nMESSAGE :-\n" + (e.ExceptionObject as Exception).Message + "\nSTACK TRACE:-\n" + (e.ExceptionObject as Exception).StackTrace + "\n====================\n");
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Unhandled Thread Exception", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            System.IO.File.AppendAllText("error_log.log", "\n====================\nUnhandled Thread Exception\n" + "\nMESSAGE :-\n" + e.Exception.Message + "\nSTACK TRACE:-\n" + e.Exception.StackTrace + "\n====================\n");
        }
    }
}
