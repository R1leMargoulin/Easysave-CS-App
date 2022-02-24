using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EasySave.ViewModel
{
    /// <summary>
    /// Logique d'interaction pour Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        private static Settings settingspage = null;
        public Settings()
        {
            settingspage = this;
            InitializeComponent();
            LocUtils.SetDefaultLanguage(this);
            test();

            

            
            
        }

      

        public void test()
        {
            MainWindow mainWindow = MainWindow.GetMainWindow();
            var t = "fr-FR";
            LocUtils.SwitchLanguage(this, t);
            
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
