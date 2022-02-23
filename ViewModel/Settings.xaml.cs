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
            LogFormat.ItemsSource = new List<string>() { "json", "xml" };
        }


        public void LogFormatComboBox(object sender, RoutedEventArgs e)
        {
            var log = LogFormat.SelectedItem;
            Model.Settings settings = new Model.Settings();
            if (log.Equals("json"))
            {
                settings.LogJson();
            }

            if (log.Equals("xml"))
            {
                settings.LogXml();
            }

        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
