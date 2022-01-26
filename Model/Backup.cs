using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasySave.Model
{
    public class Backup
    {

        public string Name { get; set; }
        public string DirectorySource { get; set; } 
        public string DirectoryTarget { get; set; } 
        public BackupType BackupType { get; set; }


        public void DirectoryCopy()
        {
            var sourceDirectory = new DirectoryInfo(DirectorySource);

          foreach (FileInfo file in sourceDirectory.GetFiles())
            {
                
                Directory.CreateDirectory(DirectoryTarget);   
                var path = Path.Combine(DirectoryTarget, file.Name);
                
                file.CopyTo(path, true);
            }
        }

        //public List<FileInfo> GetFiles(DirectoryInfo directorySource, List<FileInfo> files, string directoryTarget)
        //{
        //    FileInfo[] fileInfos = directorySource.GetFiles(directorySource.FullName);   

        //    foreach (FileInfo file in fileInfos)
        //    {
        //        files.Add(file);
        //    }

        //    return files;
        //}


    }
}
