using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EasySave.Model;
using Newtonsoft.Json;


namespace EasySave.Controllers
{
   public class BackupControllers
    {
        public BackupControllers()
        {
            ListBackup();
        }

        public List<Backup> BackupList { get; set; }

        public void AddBackup(Backup backup)
        {
            int index = BackupList.Count();
            if(index < 5)
            {

            BackupList.Add(backup);
            SaveBackup();
            
            }
            
        }

        public void ExecuteAllBackup()
        {
            foreach(Backup backup in BackupList)
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

            if(new FileInfo(backuppath).Length != 0)
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

            foreach(var item in list)
            {
                content += $"[{i}]" + " " + item.Name + "\n";
                i++;
            }

            return content;
        }        
        
    }
}
