using EasySave.Model;
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

namespace EasySave
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Backup> BackupList { get; set; }
        private static MainWindow home = null;

        private ObservableCollection<Backup> users = new ObservableCollection<Backup>();

        public MainWindow()
        {
           
            InitializeComponent();
            home = this;
            
            Refresh();
           
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

        public static MainWindow GetPage()
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

        public void Button_Update(object sender, EventArgs e)
        {
            UpdateBackup updateBackup = new UpdateBackup(IndexList());
            updateBackup.Show();
        }

        public void DeleteBackup(object sender, EventArgs e)
        {
            List<Backup> list = MainWindow.GetPage().BackupList;
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
                
                MainWindow.GetPage().BackupList[index].BackupExecute();
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
    }
}
