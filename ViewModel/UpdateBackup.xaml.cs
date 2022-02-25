﻿using EasySave.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EasySave.View
{
    /// <summary>
    /// Logique d'interaction pour UpdateBackup.xaml
    /// </summary>
    public partial class UpdateBackup : Window
    {
        public Backup Backup;
        public UpdateBackup(Backup backup)
        {
            InitializeComponent();
            initializeValue(backup);
            LocUtils.SetDefaultLanguage(this);
            LanguesSettings();
            Backup = backup;

        }



        public void initializeValue(Backup backup)
        {
            BackupName.Text = backup.Name;
            DirectorySource.Text = backup.DirectorySource;
            DirectoryTarget.Text = backup.DirectoryTarget;
            if (backup.BackupType == BackupType.Complet)
            {
                RadioComplete.IsChecked = true;
            }
            if (backup.BackupType == BackupType.Differentielle)
            {
                RadioDiffe.IsChecked = true;
            }

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
            private void UpdateBackupadd(object sender, RoutedEventArgs e)
        {

            Backup.Name = BackupName.Text;
            Backup.DirectorySource = DirectorySource.Text;
            Backup.DirectoryTarget = DirectoryTarget.Text;
            if (RadioComplete.IsChecked == true)
            {
                Backup.BackupType = BackupType.Complet; 
            }
            if (RadioDiffe.IsChecked == true)
            {
                Backup.BackupType = BackupType.Differentielle;
            }
            MainWindow mainWindow = MainWindow.GetMainWindow();
            int index = mainWindow.BackupList.IndexOf(Backup);
            mainWindow.BackupList[index] = Backup;

            MainWindow.SaveBackup(mainWindow.BackupList);
            mainWindow.Refresh();
            Close();
        }

        private void BrowseSourceButton(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DirectorySource.Text = fbd.SelectedPath;
            }
        }

        private void BrowseTargetButton(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DirectoryTarget.Text = fbd.SelectedPath;
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
