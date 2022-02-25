using EasySave.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EasySave.View
{
    /// <summary>
    /// Interaction logic for CreateBackup.xaml
    /// </summary>
    public partial class CreateBackup : Window

    {
        private static CreateBackup createBackup = null;
        public CreateBackup()
        {
            createBackup = this;
            InitializeComponent();
            LocUtils.SetDefaultLanguage(this);
            LanguesSettings();

        }

        public static CreateBackup GetCreateBackup()
        {
            if(createBackup != null)
                return createBackup;
            return new CreateBackup();  
        }

        private void CreateBackupAdd( object sender, RoutedEventArgs e)
        {
            Backup backup = new Backup();
            backup.Name = createBackup.Name.Text;
            backup.DirectorySource = createBackup.Source.Text;
            backup.DirectoryTarget = createBackup.Target.Text;
            backup.IsEncrypted = Encrypted.IsChecked.Value;
            if(RadioComplet.IsChecked == true)
            {
                backup.BackupType = BackupType.Complet;
            }
            if (RadioDiff.IsChecked == true)
            {
                backup.BackupType = BackupType.Differentielle;
            }
            if (createBackup.Name.Text == "" || createBackup.Source.Text =="" || createBackup.Target.Text=="" || RadioComplet.IsChecked == false && RadioDiff.IsChecked == false)
            {
                System.Windows.MessageBox.Show(messageError());
            }
            else
            {
                List<Backup> list = MainWindow.ListBackup();

                list.Add(backup);
                MainWindow.SaveBackup(list);
                MainWindow.GetMainWindow().Refresh();
            }

            Close();

            

            
            

        }


        // Use the Language options write down on the settings files
        // to change all the text in the window.
        public void LanguesSettings()
        {
            Model.Settings settings = new Model.Settings();
            settings.FileSettings();
            var lang = settings.setting_language;
            if (lang == Model.Language.fr)
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
        private void BrowseSourceButton(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK )
            {
                Source.Text = fbd.SelectedPath;
            }
        }

        private void BrowseTargetButton(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Target.Text = fbd.SelectedPath;
            }
        }

    
        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        public static string messageError()
        {
            return "Erreur de Saisie";
        }
    }
}
