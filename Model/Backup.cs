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

        //Execute the backup to the target directory
        public void BackupExecute(string source, string targetDirectory)
        {
            var sourceDirectory = new DirectoryInfo(source); 

            if(BackupType == BackupType.Differentielle)
            {
                var target = new DirectoryInfo(DirectoryTarget);
                FileInfo[] targetFile = target.GetFiles();
              
             //Compare the file in the source directory and target Directory and copy the modified files
                foreach (var file in sourceDirectory.GetFiles())
                {
                    foreach(var item in targetFile)
                    {
                     
                        if(file.Name == item.Name)
                        {
                            if(file.LastWriteTime != item.LastWriteTime)
                            {
                                Stopwatch stopwatch = Stopwatch.StartNew(); //Start a stopwatch to know the file transfer Time
                                var path = Path.Combine(targetDirectory, file.Name);
                                item.Delete();          //Delete the modified file
                                file.CopyTo(path, true);        //Copy the modified file in the target directory with allowing the overwriting of an existing file
                                new LogDaily(Name, file.FullName, path, file.Length / 1000, stopwatch.ElapsedMilliseconds); 
                                new LogState(Name, file.FullName, path, file.Length / 1000, stopwatch.ElapsedMilliseconds, targetFile.Length -1 , "Active", 0, targetFile.Length); 
                                stopwatch.Stop();
                             } 
                        }
                    }
                }


            }

        if(BackupType == BackupType.Complet)

          {
                
                var totalfiles = sourceDirectory.GetFiles().Length;//Count the file in source Directory
                var totalfileslefttodo = sourceDirectory.GetFiles().Length; 
                
                
                
            foreach (FileInfo file in sourceDirectory.GetFiles())
             {
                   
                Directory.CreateDirectory(targetDirectory); // Create a directory at the target path
                Stopwatch stopwatch = Stopwatch.StartNew(); //Start a stopwatch to know the file transfer Time
                var path = Path.Combine(targetDirectory, file.Name);
            
                file.CopyTo(path, true);//Copy the file in the target directory and allowing the overwriting of an existings file
                    totalfileslefttodo--;
                new LogDaily(Name, file.FullName, path, file.Length / 1000 , stopwatch.ElapsedMilliseconds); //Create a new LogDaily with the properties of the backup
                new LogState(Name, file.FullName, path, file.Length / 1000, stopwatch.ElapsedMilliseconds, totalfileslefttodo, "Active", 0, totalfiles); //Create a new LogState with the properties of the backup
                   
                   
                stopwatch.Stop();
             }

                new LogState(Name, "", "", 0, 0, 0, "END", 0, 0);
          }
        }
    }
}
