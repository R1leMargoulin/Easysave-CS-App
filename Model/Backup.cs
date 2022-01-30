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

        public List<FileInfo> GetFileListFromDirectory(List<FileInfo> listFile, string source, string target)
        {
            var sourcedirectory = new DirectoryInfo(source);
            var targetdirectory = new DirectoryInfo(target);
            var fileListSource = sourcedirectory.GetFiles(); //Get all Files from the directory
            var fileListTarget = targetdirectory.GetFiles(); 
            List<string> test = new List<string>();
            foreach(var item in fileListTarget)
            {
                test.Add(item.Name);
            }
           
            var subdirectory = sourcedirectory.GetDirectories(); //Get all sub-directories from the current directory


            if (BackupType == BackupType.Complet)//If Backuptype is Complet, there is no comparison and add all files to the list
            {
                foreach (var item in fileListSource)
                {
                    listFile.Add(item);
                }
            }
            if (BackupType == BackupType.Differentielle)//If Backuptype is Differentielle, there is comparison in every file from the directory, and compare the name, if the name is equals, it checks the LastTimeWrite to only have the modified files.
            {
                if(fileListTarget.Length == 0)
                {
                    foreach (var item in fileListSource)
                    {
                        listFile.Add(item);
                    }
                }
                foreach (var itemsource in fileListSource)
                {
                    foreach (var itemtarget in fileListTarget)
                    {

                        if (itemsource.Name == itemtarget.Name)
                        {
                            if (itemsource.LastWriteTime != itemtarget.LastWriteTime)
                            {
                                listFile.Add(itemsource);
                            }
                            
                        }
                        
                        if (test.Contains(itemsource.Name) == false)//If the file is not contains in the list, it's add to the list
                        {
                            listFile.Add(itemsource);
                        }
                        break;
                      
                    }
                }
            }
            foreach (var subitem in subdirectory) //this loop call the function for every sub-directory, to have all files
            {
                GetFileListFromDirectory(listFile, subitem.FullName, targetdirectory.FullName);
            }

            return listFile;
        }

        //Execute the backup to the target directory
        public void BackupExecute(string source, string targetDirectory)
        {
            var sourceDirectory = new DirectoryInfo(source);
            var test = new DirectoryInfo(targetDirectory);
            var fileList = new List<FileInfo>();
            fileList = GetFileListFromDirectory(fileList, source, targetDirectory);
      
            var totalfiles = sourceDirectory.GetFiles().Length;//Count the file in source Directory
            var totalfileslefttodo = sourceDirectory.GetFiles().Length;

           
                
            foreach (var file in fileList)
             {
                string filepath;

                string subdirectorypath = file.DirectoryName.Split(sourceDirectory.Name)[1];
                
                if (subdirectorypath != string.Empty)
                {
                    filepath = targetDirectory + subdirectorypath;
                }

                else
                {
                    filepath = targetDirectory;
                }

               
                        Directory.CreateDirectory(filepath); // Create a directory at the target path
                
                    
                
                    
                Stopwatch stopwatch = Stopwatch.StartNew(); //Start a stopwatch to know the file transfer Time
                filepath = Path.Combine(filepath, file.Name);
            
                file.CopyTo(filepath, true);//Copy the file in the target directory and allowing the overwriting of an existings file
                totalfileslefttodo--;
                new LogDaily(Name, file.FullName, filepath, file.Length / 1000 , stopwatch.ElapsedMilliseconds); //Create a new LogDaily with the properties of the backup
                new LogState(Name, file.FullName, filepath, file.Length / 1000, stopwatch.ElapsedMilliseconds, totalfileslefttodo, "Active", 0, totalfiles); //Create a new LogState with the properties of the backup
                     
                stopwatch.Stop();
             }
                new LogState(Name, "", "", 0, 0, 0, "END", 0, 0);
        }       
    }
}
