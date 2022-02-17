﻿using EasySave.Model;
using EasySave.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Navigation;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EasySave
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Backup> BackupList { get; set; }
        private static MainWindow home = null;

        public Language language;
        public Log_Format logformat;

        private ObservableCollection<Backup> users = new ObservableCollection<Backup>();

        public MainWindow()
        {
           
            InitializeComponent();
            home = this;

            
            if (File.Exists(@"Settings.json"))
            {
                string jsonSettings = File.ReadAllText(@"Settings.json");
                Settings settings = System.Text.Json.JsonSerializer.Deserialize<Settings>(jsonSettings); //reprise des parametres mis dans le fichier settings.json
                language = settings.setting_language;
                logformat = settings.setting_log;
            }
            else
            {
                language = Model.Language.fr;
                logformat = Model.Log_Format.json;
                SettingUpdate();

            }


            ListBoxBackup.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(BackupName);
            Refresh();
           
        }



        public void SettingUpdate()
        {
            //on a first hand, we read the file and change only what we want to change in it
            //(this will be usefull if we want to easily add settings content)

            if (File.Exists(@"Settings.json"))
            {
                string jsonSettings = File.ReadAllText(@"Settings.json");
                Settings settings = System.Text.Json.JsonSerializer.Deserialize<Settings>(jsonSettings);
                settings.setting_language = language;
                settings.setting_log = logformat;

                //then, on another hand, we save our file settings
                jsonSettings = System.Text.Json.JsonSerializer.Serialize(settings);
                File.WriteAllText(@"Settings.json", jsonSettings);
            }
            else
            {
                Settings settings = new Settings { setting_language = language, setting_log = logformat };
                string jsonSettings = System.Text.Json.JsonSerializer.Serialize(settings);
                File.WriteAllText(@"Settings.json", jsonSettings);
            }
        }

        public void SetLanguage(Language a)
        {
            language = a;
            SettingUpdate();
        }
        public void SetLogFormat(Log_Format a)
        {
            logformat = a;
            SettingUpdate();
        }

        internal static List<Backup> ListBackup()
        {
            string backuppath = Environment.CurrentDirectory + @"\ListBackup.json";
            List<Backup> ListBackup;


            if (!File.Exists(backuppath))
            {
                File.Create(backuppath);
            }

            if (new FileInfo(backuppath).Length != 0)
            {
                var backupList = File.ReadAllText(backuppath);
                ListBackup = JsonConvert.DeserializeObject<List<Backup>>(backupList); 
            }

            else
            {
                ListBackup = new List<Backup>();
            }return ListBackup;
        }

        public static MainWindow GetMainWindow()
        {
            if(home != null)
                return home;
           return new MainWindow();
        }
        public void Button_Add(object sender, EventArgs e)
        {
            CreateBackup createBackup = new CreateBackup();
            createBackup.Show();
          
        }
        internal static void SaveBackup(List<Backup> list)
        {
            string backuppath = Environment.CurrentDirectory + @"\ListBackup.json";
            var data = JsonConvert.SerializeObject(list, Formatting.Indented);
            File.WriteAllText(backuppath, data);
        }

      public Backup IndexList()
        {
            int index = ListBoxBackup.SelectedIndex;
            Backup backup = BackupList[index];

            return backup;

        }

        internal void BackupName(object sender, EventArgs e)
        {
            int index = ListBoxBackup.SelectedIndex;
            if (index >= 0)
            {
                Dispatcher.Invoke(() => { BackupNameMenu.Text = BackupList[index].Name; });
                Dispatcher.Invoke(() => { BackupSourceMenu.Text = BackupList[index].DirectorySource; });
                Dispatcher.Invoke(() => { BackupTargetMenu.Text = BackupList[index].DirectoryTarget; });
                Dispatcher.Invoke(() => { if (BackupList[index].BackupType == BackupType.Complet) { CompleteMenu.IsChecked = true; } if (BackupList[index].BackupType == BackupType.Differentielle) { DiffMenu.IsChecked = true; } });
            }
            else
            {
                BackupNameMenu.Text = "";
            }
        }

        public void Button_Update(object sender, EventArgs e)
        {
            UpdateBackup updateBackup = new UpdateBackup(IndexList());
            updateBackup.Show();
        }

        public void DeleteBackup(object sender, EventArgs e)
        {
            List<Backup> list = MainWindow.GetMainWindow().BackupList;
            Backup backup = IndexList();
            list.Remove(backup);
            MainWindow.SaveBackup(list);
            Refresh();
        }

        internal void Refresh()
        {
            
            BackupList = MainWindow.ListBackup();
            ListBoxBackup.Items.Clear();
            foreach (var backup in BackupList)
            {
                ListBoxBackup.Items.Add(backup.Name);
            }
                      
        }
       
        public void ExecuteBackup(object sender, EventArgs e)
        {
            Backup backup = new Backup();
            if(backup.IsProcessRunning() == false)
            {

                
            int index = ListBoxBackup.SelectedIndex;
                
                MainWindow.GetMainWindow().BackupList[index].BackupExecute();
            MessageBoxResult messageBox = MessageBox.Show("tu es très fort bg, tout est bon"); 
            }

            else
            {
                MessageBoxResult messageBox = MessageBox.Show("Une application métier est lancée");
            }
        }

        public void ExecuteAllBackup(object sender, EventArgs e)
        {
            foreach(var item in BackupList)
            {
                item.BackupExecute();
            }
        }

        private void Button_Pause(object sender, RoutedEventArgs e)
        {
            foreach(var item in BackupList)
            {
                item.Pause();
                MessageBoxResult messageBox = MessageBox.Show("La copie de fichier a été mis en pause");
            }
        }

        private void Button_Play(object sender, RoutedEventArgs e)
        {
            foreach (var item in BackupList)
            {
                item.Play();
                
            }
        }

        private void ListBoxBackup_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
