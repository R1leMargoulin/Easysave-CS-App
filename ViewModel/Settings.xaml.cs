﻿using System;
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
            LanguesSettings();

            

            
            
        }

      

        public void LanguesSettings()
        {
            Model.Settings settings =  new Model.Settings();
            settings.FileSettings();
            var lang = settings.setting_language;
            if(lang == Model.Language.fr)
            {
                MainWindow mainWindow = MainWindow.GetMainWindow();
                var t = "fr-FR";
                LocUtils.SwitchLanguage(this, t);
            }
            if (lang == Model.Language.en)
            {
                MainWindow mainWindow = MainWindow.GetMainWindow();
                var t = "en-US";
                LocUtils.SwitchLanguage(this, t);
            }

        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
