using EasySave.Model;
using EasySave.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Navigation;
using System.Text.Json;
using System.Text.Json.Serialization;
using EasySave.ViewModel;
using System.Windows.Controls;

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

            LocUtils.SetDefaultLanguage(this);

            foreach (MenuItem item in menuItemLanguages.Items) 
            {
                if (item.Tag.ToString().Equals(LocUtils.GetCurrentCultureName(this)))
                    item.IsChecked = true;
            }
            
            
            if (File.Exists(@"Settings.json"))
            {
                string jsonSettings = File.ReadAllText(@"Settings.json");
                Model.Settings settings = System.Text.Json.JsonSerializer.Deserialize<Model.Settings>(jsonSettings); //reprise des parametres mis dans le fichier settings.json
                language = Model.Settings.setting_language;
                logformat = Model.Settings.setting_log;
                //Settings.setting_log = Log_Format.xml;
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
        
        public void MenuItem_Click(Object sender, RoutedEventArgs e)
        {
            foreach (MenuItem item in menuItemLanguages.Items)
            {
                item.IsChecked = false;
            }
            MenuItem mi = sender as MenuItem;
            mi.IsChecked = true ;
            LocUtils.SwitchLanguage(this, mi.Tag.ToString());

        }

        
        



        public void SettingUpdate()
        {
            //on a first hand, we read the file and change only what we want to change in it
            //(this will be usefull if we want to easily add settings content)

            if (File.Exists(@"Settings.json"))
            {
                string jsonSettings = File.ReadAllText(@"Settings.json");
                Model.Settings settings = System.Text.Json.JsonSerializer.Deserialize<Model.Settings>(jsonSettings);
                Model.Settings.setting_language = language;
                Model.Settings.setting_log = logformat;

                //then, on another hand, we save our file settings
                jsonSettings = System.Text.Json.JsonSerializer.Serialize(settings);
                File.WriteAllText(@"Settings.json", jsonSettings);
            }
            else
            {
                Model.Settings sett = new Model.Settings();
                string jsonSettings = System.Text.Json.JsonSerializer.Serialize(sett);
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

        private void ExitApp(object sender, EventArgs e)
        {
            base.OnClosed(e);
            Process.GetCurrentProcess().Kill();
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
        //test
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
           if(ListBoxBackup.SelectedIndex == -1)
            {
                MessageBox.Show("Veuillez sélectionner une sauvegarde");
            }
            else
            {

            
            UpdateBackup updateBackup = new UpdateBackup(IndexList());
            updateBackup.Show(); 
           }
        }

        public void DeleteBackup(object sender, EventArgs e)
        {
            if (ListBoxBackup.SelectedIndex == -1)
            {
                MessageBox.Show("Veuillez sélectionner une sauvegarde");
            }
            else
            {
                List<Backup> list = MainWindow.GetMainWindow().BackupList;
                Backup backup = IndexList();
                list.Remove(backup);
                MainWindow.SaveBackup(list);
                Refresh();
            }
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
        private static Thread ExecuteAllThread;
        public void ExecuteBackup(object sender, EventArgs e)
        {
            int index = ListBoxBackup.SelectedIndex;
            if (ListBoxBackup.SelectedIndex == -1)
            {
                MessageBox.Show("Veuillez sélectionner une sauvegarde");
            }
            else
            { 
            new Thread(new ThreadStart(delegate ()
            {

                {
                    Backup backup = new Backup();
                    if (backup.IsProcessRunning() == false)
                    /*{
                        if (backup.IsDirectoryExits(BackupSourceMenu.Text) == false)
                        {
                            MessageBoxResult messageBox = MessageBox.Show("Erreur de fichier source");
                        }
                        else*/
                        {

                            MainWindow.GetMainWindow().BackupList[index].BackupExecuteThread();
                            //MessageBoxResult messageBox = MessageBox.Show("tu es très fort bg, tout est bon");
                        }
                    //}
                    else
                    {
                        MessageBoxResult messageBox = MessageBox.Show("Une application métier est lancée");
                    }
                }
            })).Start();
            }



        }

        public void ExecuteAllBackup(object sender, EventArgs e)
        {

            ExecuteAllThread = new Thread(new ThreadStart(delegate ()
            {
                foreach (var item in BackupList)
                {
                    item.BackupExecuteThread();
                }
            }));

            ExecuteAllThread.Start();
        }

        private void Button_Pause(object sender, RoutedEventArgs e)
        {
            if (ListBoxBackup.SelectedIndex == -1)
            {
                MessageBox.Show("Veuillez sélectionner une sauvegarde");
            }
            else
            {
                foreach (var item in BackupList)
                {
                    item.Pause();
                    //MessageBoxResult messageBox = MessageBox.Show("La copie de fichier a été mis en pause");
                }
                MessageBoxResult messageBox = MessageBox.Show("La copie de fichier a été mis en pause");
            }
        }

        private void Button_Play(object sender, RoutedEventArgs e)
        {
            if (ListBoxBackup.SelectedIndex == -1)
            {
                MessageBox.Show("Veuillez sélectionner une sauvegarde");
            }
            else
            {
                foreach (var item in BackupList)
                {
                    item.ResumePlay();

                }
            }
        }

        private void Button_Stop(object sender, RoutedEventArgs e)
        {
            if (ListBoxBackup.SelectedIndex == -1)
            {
                MessageBox.Show("Veuillez sélectionner une sauvegarde");
            }
            else
            {
                foreach (var item in BackupList)
                {
                  //  item.Active = false;
                }
            }
        }

        private void LaunchSettings(object sender, RoutedEventArgs e)
        {
            ViewModel.Settings _settings = new ViewModel.Settings();
            _settings.Show();
        }

        private void StartConnection(object sender, RoutedEventArgs e)
        {
            Server server = new Server();
            server.RunNetwork();
        }



    }
}
