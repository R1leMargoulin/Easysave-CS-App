using System;
using System.Collections.Generic;
using System.Text;
using EasySave.Model;

namespace EasySave.Controllers
{
   public class BackupControllers
    {

        public List<Backup> BackupList { get; set; }

        public void AddBackup(Backup backup)
        {
            BackupList.Add(backup);
        }

        public void ExecuteBackup()
        {

            Backup backup = BackupList[BackupList.Count - 1];
            backup.DirectoryCopy();

        }
    }
}
