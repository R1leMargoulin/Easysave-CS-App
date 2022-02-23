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
using EasySave.ViewModel;

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
            
            ListBoxBackup.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(BackupName);
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
       
        public void ExecuteBackup(object sender, EventArgs e)
        {
            
             if (ListBoxBackup.SelectedIndex == -1)
             {
                 MessageBox.Show("Veuillez sélectionner une sauvegarde");
             }
             else
             {
                 Backup backup = new Backup();
                 if (backup.IsProcessRunning() == false)
                 {
                     if (backup.IsDirectoryExits(BackupSourceMenu.Text) == false)
                     {
                         MessageBoxResult messageBox = MessageBox.Show("Erreur de fichier source");
                     }
                     else
                     {
                         Thread.Sleep(2000);
                         int index = ListBoxBackup.SelectedIndex;

                         MainWindow.GetMainWindow().BackupList[index].BackupExecute();
                         MessageBoxResult messageBox = MessageBox.Show("tu es très fort bg, tout est bon");
                     }
                 }
                 else
                 {
                     MessageBoxResult messageBox = MessageBox.Show("Une application métier est lancée");
                 }
             }
            //new Thread(new ThreadStart(delegate ()
            //{
            //    int index = ListBoxBackup.SelectedIndex;
            //    MainWindow.GetMainWindow().BackupList[index].BackupExecute();
            //    MessageBoxResult messageBox = MessageBox.Show("tu es très fort bg, tout est bon");
            //})).Start();


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
                    

                }
            }
        }

        private void LaunchSettings(object sender, RoutedEventArgs e)
        {
            Settings _settings = new Settings();
            _settings.Show();
        }

        public void Progress (object sender, RoutedEventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerAsync();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = 0;
            
            Backup backup = BackupList[0];
            int total = backup.Total(backup.DirectorySource);
            var sourcedirectory = new DirectoryInfo(backup.DirectorySource);
            var fileListSource = sourcedirectory.GetFiles();
            int z = 1;
            foreach(var item in fileListSource)
            {
                
                int longu = z * 100 / total;
                if(longu <=100)
            {

            
                (sender as BackgroundWorker).ReportProgress(longu);
                    z++;
                    Thread.Sleep(1000);
                }
            }
           
            
               

            
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //initialisation de la barre de progression avec le pourcentage de progression
            Progressbar.Value   = e.ProgressPercentage;

            //Affichage de la progression sur un label
            percent.Content = Progressbar.Value.ToString() +"%";



        }


    }
}
