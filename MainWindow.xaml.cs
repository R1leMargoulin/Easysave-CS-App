using EasySave.Model;
using EasySave.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

        private ObservableCollection<Backup> users = new ObservableCollection<Backup>();

        public MainWindow()
        {
           
            InitializeComponent();
            ListBackup();
           
        }
      
        public void ListBackup()
        {
            string backuppath = Environment.CurrentDirectory + @"\ListBackup.json";

            if (!File.Exists(backuppath))
            {
                File.Create(backuppath);
            }

            if (new FileInfo(backuppath).Length != 0)
            {
                var backupList = File.ReadAllText(backuppath);
                BackupList = JsonConvert.DeserializeObject<List<Backup>>(backupList);

                tanguy.ItemsSource = BackupList;

            }

            else
            {
                BackupList = new List<Backup>();
            }
        }
        public void Button_Add(object sender, EventArgs e)
        {
            MenuView menuView = new MenuView();
             menuView.Show();
          
        }
        public void SaveBackup()
        {
            string backuppath = Environment.CurrentDirectory + @"\ListBackup.json";
            var data = JsonConvert.SerializeObject(BackupList, Formatting.Indented);
            File.WriteAllText(backuppath, data);
        }

       

        public void Refresh(object sender, RoutedEventArgs e)
        {
            tanguy.ItemsSource = null;
            tanguy.ItemsSource = BackupList;
            tanguy.Items.Refresh();
            
           
            
        }

        private void tanguy_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
