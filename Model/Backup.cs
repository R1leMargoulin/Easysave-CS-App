using System;
using System.Collections.Generic;
using System.Diagnostics;
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


        public void DirectoryCopy(string source, string targetDirectory)
        {
            var sourceDirectory = new DirectoryInfo(source);

            if(BackupType == BackupType.Differentielle)
            {
                var target = new DirectoryInfo(DirectoryTarget);
                FileInfo[] targetFile = target.GetFiles();
               sourceDirectory.GetFiles();
             
                foreach (var file in sourceDirectory.GetFiles())
                {
                    foreach(var item in targetFile)
                    {
                        if(file.Name == item.Name)
                        {

                      
                                 if(file.LastWriteTime != item.LastWriteTime)
                            {
                            Stopwatch stopwatch = Stopwatch.StartNew();
                            var path = Path.Combine(targetDirectory, file.Name);
                               
                            file.CopyTo(path, true);
                            new LogDaily(Name, file.FullName, path, file.Length / 1000, stopwatch.ElapsedMilliseconds);
                            stopwatch.Stop();
                             } 
                        }
                    }
                }


            }
            if(BackupType == BackupType.Complet)
            {

           
          foreach (FileInfo file in sourceDirectory.GetFiles())
            {
                
                Directory.CreateDirectory(targetDirectory);
                Stopwatch stopwatch = Stopwatch.StartNew();
                var path = Path.Combine(targetDirectory, file.Name);
                
                file.CopyTo(path, true);
                new LogDaily(Name, file.FullName, path, file.Length / 1000 , stopwatch.ElapsedMilliseconds);
                stopwatch.Stop();
            } 
            
            }
        }

        public List<FileInfo> GetFiles(DirectoryInfo directorySource, List<FileInfo> files, string directoryTarget)
        {
            FileInfo[] fileInfos = directorySource.GetFiles(directorySource.FullName);   

            foreach (FileInfo file in fileInfos)
            {
                files.Add(file);
          }

            return files;
        }


    }
}
