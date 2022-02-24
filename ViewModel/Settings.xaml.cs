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
            RefreshProcess();
            RefreshImportantFile();
            RefreshEncrypt();
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

        internal void RefreshProcess()
        {
            Model.Settings settings = new Model.Settings();
            settings.FileSettings();
            var list = settings.setting_process;
            ListExtentionProcess.Items.Clear();
            foreach(var item in list)
            {
                ListExtentionProcess.Items.Add(item);
            }
        }

        public void AddProcess(object sender, RoutedEventArgs e)
        {
            Model.Settings settings = new Model.Settings();
            settings.FileSettings();

            settings.ProcessAdd(TextBoxToAdd.Text);
            RefreshProcess();

        }

        public void DeleteProcess(object sender, RoutedEventArgs e)
        {
            Model.Settings settings = new Model.Settings();
            settings.FileSettings();
            var list = settings.setting_process;
            int i = ListExtentionProcess.SelectedIndex;
            list.RemoveAt(i);
            settings.ProcessDelete(list);
            
            RefreshProcess();

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
        internal void RefreshImportantFile()
        {
            Model.Settings settings = new Model.Settings();
            settings.FileSettings();
            var list = settings.setting_importantfile;
            ListExtensionImportantFiles.Items.Clear();
            foreach (var item in list)
            {
                ListExtensionImportantFiles.Items.Add(item);
            }
        }
        public void AddImportantFile(object sender, RoutedEventArgs e)
        {
            Model.Settings settings = new Model.Settings();
            settings.FileSettings();

            settings.ImportantFileAdd(TextBoxToAdd.Text);
            RefreshImportantFile();

        }

        public void DeleteImportantFile(object sender, RoutedEventArgs e)
        {
            Model.Settings settings = new Model.Settings();
            settings.FileSettings();
            var list = settings.setting_importantfile;
            int i = ListExtensionImportantFiles.SelectedIndex;
            list.RemoveAt(i);
            settings.ImportantFileDelete(list);

            RefreshImportantFile();

        }


        internal void RefreshEncrypt()
        {
            Model.Settings settings = new Model.Settings();
            settings.FileSettings();
            var list = settings.setting_encryptfile;
            ListEncrypt.Items.Clear();
            foreach (var item in list)
            {
                ListEncrypt.Items.Add(item);
            }
        }

        public void AddEncryptFile(object sender, RoutedEventArgs e)
        {
            Model.Settings settings = new Model.Settings();
            settings.FileSettings();

            settings.EncryptFileAdd(TextBoxToAdd.Text);
            RefreshEncrypt();

        }

        public void DeleteEncryptFile(object sender, RoutedEventArgs e)
        {
            Model.Settings settings = new Model.Settings();
            settings.FileSettings();
            var list = settings.setting_encryptfile;
            int i = ListEncrypt.SelectedIndex;
            list.RemoveAt(i);
            settings.EncryptFileDelete(list);

            RefreshEncrypt();

        }


        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
