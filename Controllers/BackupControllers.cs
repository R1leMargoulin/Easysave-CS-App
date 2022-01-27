using System;
using System.Collections.Generic;
using System.Text;
using EasySave.Model;

namespace EasySave.Controllers
{
   public class BackupControllers
    {
        public List<Backup> BackupList { get; set; }

        public List<Backup> BackupList { get; set; }

        public void AddBackup(Backup backup)
        {
            BackupList.Add(backup);
        }

        public void DeleteBackup(int index)
        {
            int backupnum = index - 1;
            BackupList.RemoveAt(backupnum); 
        }

        public void UpdateBackup(int index, Backup Updatebackup)
        {
            int backupnum = index - 1;
            BackupList[backupnum] = Updatebackup;
        }
    }
}
