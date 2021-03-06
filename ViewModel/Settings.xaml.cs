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
            LanguesSettings();
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
            settings.FileSettings();    //Init a new settings from the settings file 
            var list = settings.setting_process;
            ListExtentionProcess.Items.Clear(); //Clear the ListBox 
            foreach(var item in list)
            {
                ListExtentionProcess.Items.Add(item);       //Add the list of process in listbox Process to refresh
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
            settings.FileSettings();                 //Init a new settings from the settings file 
            var list = settings.setting_process;
            int i = ListExtentionProcess.SelectedIndex; //Check the selected index
            list.RemoveAt(i);       //Remove the backup from the list
            settings.ProcessDelete(list);  //Write the list of process in settings file
            
            RefreshProcess(); //Refresh the listbox

            LocUtils.SetDefaultLanguage(this);
            LanguesSettings();

            

            
            
        }

        // Use the Language options write down on the settings files
        // to change all the text in the window.

        public void LanguesSettings()
        {
            Model.Settings settings =  new Model.Settings();
            settings.FileSettings(); //Set a new string with the value of the settings file
            var lang = settings.setting_language;
            if(lang == Model.Language.fr) //Check the language and switch the language of the page
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


        public void AddMaxFileSize(object sender, RoutedEventArgs e)
        {
            Model.Settings settings = new Model.Settings();
            settings.FileSettings();
            var i = int.Parse(MaxSize.Text);
            settings.AddMaxSize(i);
            
            if(settings.setting_language == Model.Language.fr)
            {
                MessageBox.Show("La taille des fichiers max a été modifié");
            }
            if (settings.setting_language == Model.Language.en)
            {
                MessageBox.Show("the max file size has been modified");
            }


        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
