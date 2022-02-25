using System;
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

        public List<string> listpriori()
        {
            Settings settings = new Settings();
            settings.FileSettings();
            return settings.setting_importantfile;
        }

        public List<FileInfo> priori = new List<FileInfo>();

        public List<Thread> listThread = new List<Thread>();
        public List<Thread> listTPaused = new List<Thread>();

        public string Name { get; set; }
        public string DirectorySource { get; set; }
        public string DirectoryTarget { get; set; }
        public BackupType BackupType { get; set; }

        public bool IsEncrypted { get; set; }
        // public bool Active { get; set; }



        public bool IsProcessRunning()
        {
            Settings settings = new Settings();
            settings.FileSettings();
            foreach(var item in settings.setting_process)
            { 
            Process[] process = Process.GetProcessesByName(item);
            if(process.Length > 0)
            {
                return true;
            }
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
                    if (listpriori().Contains(item.Extension))
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
                                if (listpriori().Contains(itemsource.Extension))
                                {
                                    priori.Add(itemsource);
                                }
                                else
                                {
                                    listFile.Add(itemsource);

                                }
                                
                            }

                        }

                        if (test.Contains(itemsource.Name) == false)//If the file is not contains in the list, it's add to the list
                        {
                            if (listpriori().Contains(itemsource.Extension))
                            {
                                priori.Add(itemsource);
                            }
                            else
                            {

                          
                            listFile.Add(itemsource); 
                            
                            }
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

        public void BackupExecuteThread()
        {
            listThread.Add(new Thread(() => BackupExecute(this, listThread.Count - 1)));
            listThread[listThread.Count - 1].Start();
            
           
        }
        public void BackupExecute(Backup dir, int indexCurrentThread)
        {
            var sourceDirectory = new DirectoryInfo(dir.DirectorySource);
            var test = new DirectoryInfo(dir.DirectoryTarget);
            var fileList = new List<FileInfo>();
            fileList = GetFileListFromDirectory(fileList, dir.DirectorySource, dir.DirectoryTarget);

            var totalfiles = sourceDirectory.GetFiles().Length;//Count the file in source Directory
            var totalfileslefttodo = sourceDirectory.GetFiles().Length + 1;

            Process cryptoProcess = new Process();
            cryptoProcess.StartInfo.UseShellExecute = false;
            cryptoProcess.StartInfo.FileName = "../../../CryptoSoft/CryptoSoft.exe";
            Model.Settings settings = new Settings();
            settings.FileSettings();

            long lenght = 0;
            foreach (var file in fileList)
            {
                if (file.Length < (settings.setting_maxsize * 1000))
                {
                    lenght += file.Length;

                }
                else //if the file is eavier than the defined size
                {
                    Thread.Sleep(1000); //in the case of an ExecuteAll we let a second to get sure that all other threads have been created
                    for(int i = 0; i < listThread.Count; i++) //for each threads in our list of threads
                    {
                        int verif = 0;
                        while (verif != 1) 
                        {
                            if (LogState.InstanceState() == null & LogDaily.InstanceState() == null) //this is the verif, we dont want to interrupt a thread that was using ur log singleton because it will be an interlocking
                            {
                                if (i != indexCurrentThread) //if that is not our current thread (cith the big file)
                                {
                                    listThread[i].Interrupt(); //we interrupt and add at in a kind of waitlist
                                    listTPaused.Add(listThread[i]);

                                }
                                verif = 1;
                            }
                        }
                    }
                    lenght += file.Length;
                }
            }

            if(priori != null) //If the list is not null and if the list has file, the list is prioritory
            {
                foreach( var file in priori)
                {

                    while(IsProcessRunning() == true)
                    {
                        waitHandle.Reset();
                        MainWindow mainWindow = MainWindow.GetMainWindow();
                        mainWindow.ProcessRunningError();
                        waitHandle.Set();
                    }
                    waitHandle.WaitOne();
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

                    if (dir.IsEncrypted)
                    {
                        cryptoProcess.StartInfo.Arguments = file.FullName + " 11001101 " + filepath;
                        cryptoProcess.Start();
                    }
                    else
                    {
                        file.CopyTo(filepath, true);//Copy the file in the target directory and allowing the overwriting of an existings file
                    }
                    totalfileslefttodo--;
                    ArgsLogDaily arglogDaily =new ArgsLogDaily(Name, file.FullName, filepath, file.Length / 1000, stopwatch.ElapsedMilliseconds); //Create a new LogDaily with the properties of the
                    LogDaily.GetInstance(arglogDaily);
                    ArgsLogState argLogState = new ArgsLogState(Name, file.FullName, filepath, lenght / 1000, stopwatch.ElapsedMilliseconds, totalfileslefttodo, "Active", 0, totalfiles); //Create a new LogState with the properties of the backup
                    LogState.GetInstance(argLogState);

                    stopwatch.Stop();
                    Server serverexe = new Server();
                    //serverexe.RunNetwork(Name);
                }
            }

            foreach (var file in fileList)
            {

                while (IsProcessRunning() == true) // While the process is running,  the Thread is pause
                {
                    waitHandle.Reset();
                    MainWindow mainWindow = MainWindow.GetMainWindow();
                    mainWindow.ProcessRunningError();
                    waitHandle.Set();
                }
                waitHandle.WaitOne();
               
                string filepath;

                string subdirectorypath = file.DirectoryName.Split(sourceDirectory.Name)[1];

                if (subdirectorypath != String.Empty)
                {
                    filepath = dir.DirectoryTarget + subdirectorypath;
                }

                else
                {
                    filepath = dir.DirectoryTarget;
                }


                Directory.CreateDirectory(filepath); // Create a directory at the target path




                Stopwatch stopwatch = Stopwatch.StartNew(); //Start a stopwatch to know the file transfer Time
                filepath = Path.Combine(filepath, file.Name);

                if (dir.IsEncrypted)
                {
                    cryptoProcess.StartInfo.Arguments = file.FullName + " 11001101 " + filepath;
                    cryptoProcess.Start();
                }
                else
                {
                    file.CopyTo(filepath, true);//Copy the file in the target directory and allowing the overwriting of an existings file
                }
                totalfileslefttodo--;
                ArgsLogDaily arglogDaily = new ArgsLogDaily(Name, file.FullName, filepath, file.Length / 1000, stopwatch.ElapsedMilliseconds); //Create a new LogDaily with the properties of the
                LogDaily.GetInstance(arglogDaily);
                ArgsLogState argLogState = new ArgsLogState(Name, file.FullName, filepath, lenght / 1000, stopwatch.ElapsedMilliseconds, totalfileslefttodo, "Active", 0, totalfiles); //Create a new LogState with the properties of the backup
                LogState.GetInstance(argLogState);

                stopwatch.Stop();
            }
            ArgsLogState o = new ArgsLogState(Name, "", "", 0, 0, 0, "END", 0, 0);
            LogState.GetInstance(o);

            if(listTPaused != null)
            {
                foreach(Thread pausedT in listTPaused)
                {
                    pausedT.Resume();
                }
                listTPaused.Clear();

            }
            listThread.Remove(listThread[indexCurrentThread]);
        }

        public void StopThreads()
        {
           /* foreach(Thread t in listThread)
            {
                t.Suspend();
            }*/
            listThread.Clear();
        }

    }
}
