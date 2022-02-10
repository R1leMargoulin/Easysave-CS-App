using EasySave.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace EasySave.ViewModel
{
    public class BackupViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;





        protected void OnpropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private string _name;
        private string _directorySource;
        private string _directoryTarget;
        private string _progressbar;
        public List<Backup> BackupList { get; set; }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnpropertyChanged(nameof(Name));
            }
        }
        public string DirectorySource
        {
            get { return _directorySource; }
            set
            {
                _directorySource = value;
                OnpropertyChanged(nameof(DirectorySource));
            }
        }
        public string DirectoryTarget
        {
            get { return _directoryTarget; }
            set
            {
                _directoryTarget = value;
                OnpropertyChanged(nameof(DirectoryTarget));
            }
        }
        public BackupType BackupType { get; set; }


        public string ProgressBar
        {
            get { return _progressbar; }
            set
            {
                _progressbar = value;
                OnpropertyChanged(Name);
            }
        }

        public void AddBackup(Backup backup)
        {
            int index = BackupList.Count();


            BackupList.Add(backup);
            SaveBackup();



        }
        public void ExecuteAllBackup()
        {
            foreach (Backup backup in BackupList)
            {
                backup.BackupExecute();
            }

        }

        public void DeleteBackup(int index)
        {
            int backupnum = index - 1;
            BackupList.RemoveAt(backupnum);
            SaveBackup();
        }

        public void UpdateBackup(int index, Backup Updatebackup)
        {
            int backupnum = index - 1;
            BackupList[backupnum] = Updatebackup;
            SaveBackup();
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

            }

            else
            {
                BackupList = new List<Backup>();
            }
        }

        public void SaveBackup()
        {
            string backuppath = Environment.CurrentDirectory + @"\ListBackup.json";
            var data = JsonConvert.SerializeObject(BackupList, Formatting.Indented);
            File.WriteAllText(backuppath, data);
        }

        public string DisplayAllBackup(List<Backup> list)
        {

            string content = "";
            int i = 1;

            foreach (var item in list)
            {
                content += $"[{i}]" + " " + item.Name + "\n";
                i++;
            }

            return content;
        }

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }

        private System.Collections.IEnumerable viewModel;

        public System.Collections.IEnumerable ViewModel { get => viewModel; set => SetProperty(ref viewModel, value); }



















    }

}
