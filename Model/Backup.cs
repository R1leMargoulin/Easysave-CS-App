﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;

namespace EasySave.Model
{
    public class Backup
    {
        private static EventWaitHandle waitHandle = new ManualResetEvent(initialState: true);
        public static List<string> AcceptedExtensionFiles = new List<string>()
        {
            ".pdf",
            ".jpeg",
            ".jpg"
            
        };

        public List<FileInfo> priori = new List<FileInfo>();

        public string Name { get; set; }
        public string DirectorySource { get; set; }
        public string DirectoryTarget { get; set; }
        public BackupType BackupType { get; set; }

        

        public bool IsProcessRunning()
        {
            Process[] process = Process.GetProcessesByName("notepad");
            if(process.Length > 0)
            {
                return true;
            }
            return false;
        }

        public bool IsDirectoryExits(string source)
        {
            var sourcedirectory = new DirectoryInfo(source);
            if (!sourcedirectory.Exists)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Pause()
        {
            waitHandle.Reset();
        }

        public void ResumePlay()
        {
            waitHandle.Set();
        }

        public List<FileInfo> GetFileListFromDirectory(List<FileInfo> listFile, string source, string target)
        {
            
            var sourcedirectory = new DirectoryInfo(source);
            var targetdirectory = new DirectoryInfo(target);
            var fileListSource = sourcedirectory.GetFiles(); //Get all Files from the directory
            var fileListTarget = targetdirectory.GetFiles();
           
            List<string> test = new List<string>();
            
            foreach (var item in fileListTarget)
            {
                test.Add(item.Name);
            }

            var subdirectory = sourcedirectory.GetDirectories(); //Get all sub-directories from the current directory


            if (BackupType == BackupType.Complet)//If Backuptype is Complet, there is no comparison and add all files to the list
            {
                foreach (var item in fileListSource)
                {
                    if (AcceptedExtensionFiles.Contains(item.Extension))
                    {
                        priori.Add(item);
                    }
                    else
                    {
                        listFile.Add(item);
                    }
                    
                }
            }
            if (BackupType == BackupType.Differentielle)//If Backuptype is Differentielle, there is comparison in every file from the directory, and compare the name, if the name is equals, it checks the LastTimeWrite to only have the modified files.
            {
                if (fileListTarget.Length == 0)
                {
                    foreach (var item in fileListSource)
                    {
                        
                        listFile.Add(item);
                    }
                }

                foreach (var itemtarget in fileListTarget)
                {
                    foreach (var itemsource in fileListSource)
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
                    }

                }


            }
            foreach (var subitem in subdirectory) //this loop call the function for every sub-directory, to have all files
            {
                GetFileListFromDirectory(listFile, subitem.FullName, targetdirectory.FullName);
            }
        
            return listFile;
        }
      

        private static Mutex mutex = new Mutex();
        //Execute the backup to the target directory
        public void BackupExecute()
        {
            var sourceDirectory = new DirectoryInfo(DirectorySource);
            var test = new DirectoryInfo(DirectoryTarget);
            var fileList = new List<FileInfo>();
            fileList = GetFileListFromDirectory(fileList, DirectorySource, DirectoryTarget);

            var totalfiles = sourceDirectory.GetFiles().Length;//Count the file in source Directory
            var totalfileslefttodo = sourceDirectory.GetFiles().Length + 1;

            long lenght = 0;
            foreach (var file in fileList)
            {
                lenght += file.Length;
            }

            if(priori != null)
            {
                foreach( var file in priori)
                {
                    string filepath;

                    string subdirectorypath = file.DirectoryName.Split(sourceDirectory.Name)[1];

                    if (subdirectorypath != String.Empty)
                    {
                        filepath = DirectoryTarget + subdirectorypath;
                    }

                    else
                    {
                        filepath = DirectoryTarget;
                    }


                    Directory.CreateDirectory(filepath); // Create a directory at the target path




                    Stopwatch stopwatch = Stopwatch.StartNew(); //Start a stopwatch to know the file transfer Time
                    filepath = Path.Combine(filepath, file.Name);

                    file.CopyTo(filepath, true);//Copy the file in the target directory and allowing the overwriting of an existings file
                    totalfileslefttodo--;
                    new LogDaily(Name, file.FullName, filepath, file.Length / 1000, stopwatch.ElapsedMilliseconds); //Create a new LogDaily with the properties of the backup
                    new LogState(Name, file.FullName, filepath, lenght / 1000, stopwatch.ElapsedMilliseconds, totalfileslefttodo, "Active", 0, totalfiles); //Create a new LogState with the properties of the backup

                    stopwatch.Stop();
                }
            }

            foreach (var file in fileList)
            {

               
                string filepath;

                string subdirectorypath = file.DirectoryName.Split(sourceDirectory.Name)[1];

                if (subdirectorypath != String.Empty)
                {
                    filepath = DirectoryTarget + subdirectorypath;
                }

                else
                {
                    filepath = DirectoryTarget;
                }


                Directory.CreateDirectory(filepath); // Create a directory at the target path




                Stopwatch stopwatch = Stopwatch.StartNew(); //Start a stopwatch to know the file transfer Time
                filepath = Path.Combine(filepath, file.Name);

                file.CopyTo(filepath, true);//Copy the file in the target directory and allowing the overwriting of an existings file
                totalfileslefttodo--;
                new LogDaily(Name, file.FullName, filepath, file.Length / 1000, stopwatch.ElapsedMilliseconds); //Create a new LogDaily with the properties of the backup
                new LogState(Name, file.FullName, filepath, lenght / 1000, stopwatch.ElapsedMilliseconds, totalfileslefttodo, "Active", 0, totalfiles); //Create a new LogState with the properties of the backup

                stopwatch.Stop();
            }
            new LogState(Name, "", "", 0, 0, 0, "END", 0, 0);
        }
    }
}
