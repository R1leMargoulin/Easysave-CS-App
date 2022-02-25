using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace EasySave
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            if (TestIfExist())
            {
                System.Windows.MessageBox.Show(messageErrorProcess());
                Application.Current.Shutdown();
            }
            else
            {
                
                new MainWindow().Show(); //launch the main windows

            }
        }

        public static bool TestIfExist() //Check if the process easysave is running
        {
            Process[] processes = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
            if (processes.Length == 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        

        public static string messageErrorProcess()
        {
            return "L'application est déja lancée";  //Launch a message error for the application

        }
        public static string messageError()
        {
            return "Input error";

        }
    }
}
