using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.XPath;
using System.Xml;
using System.IO;

namespace EasySave.Model
{
    public class LogDaily
    {
        private string Pathlog { get; set; }
        private string Namelog { get; set; }
        private string Sourcelog { get; set; }
        private string Targetlog { get; set; }
        private long Sizelog { get; set; }
        private double Durationlog { get; set; }
        private DateTime DateTimelog { get; set; }

        public string logformat;


        public class LogDailyData
        {
            public string Name;
            public string FileSource;
            public string FileTarget;
            public string destPath;
            public long Filesize;
            public double FileTransferTime;
            public string time;
        }

        public LogDaily(string logname, string logfilesource, string logfiletarget, long logsize, double logduration)
        {
            Namelog = logname;
            Sourcelog = logfilesource;
            Targetlog = logfiletarget;
            Sizelog = logsize;
            Durationlog = logduration;
            DateTimelog = DateTime.Now;
            Pathlog = $"./LogPath/Logs_{DateTime.Now:dd-MM-yyyy}";

            Directory.CreateDirectory("./LogPath");


            string path = $"{Pathlog}.json";
            if (!File.Exists($"{Pathlog}.json"))
            {
                StreamWriter file = File.AppendText($"{Pathlog}.json");
                file.Close();
            }


            string LOGJSONData = File.ReadAllText(path);

            List<LogDailyData> logDailyData = JsonConvert.DeserializeObject<List<LogDailyData>>(LOGJSONData) ?? new List<LogDailyData>();

            logDailyData.Add(new LogDailyData()
            {
                destPath = Sourcelog,
                Filesize = Sizelog,
                time = DateTimelog.ToString(),
                FileSource = Sourcelog,
                FileTarget = Targetlog,
                FileTransferTime = Durationlog,
                Name = Namelog

            });

            string ObjectJsonData = JsonConvert.SerializeObject(logDailyData, Newtonsoft.Json.Formatting.Indented);


            File.WriteAllText(path, ObjectJsonData);






        }
    }











    public class LogState
    {
        private string Pathlog { get; set; }
        private string Namelog { get; set; }
        private string SourceBackup { get; set; }
        private string TargetBackup { get; set; }
        public int TotalFilesToCopy { get; set; }
        public long TotalFilesSize { get; set; }
        public int NbFilesLeftToDo { get; set; }
        public int Progression { get; set; }
        public DateTime DateTime { get; set; }
        public string State { get; set; }
        

        private class LogStateData
        {
            public string Name;
            public string SourceBackup;
            public string TargetBackup;
            public string StateLog;
            public int TotalFilesToCopy;
            public long TotalFilesSize;
            public int NbFilesLeftToDo;
            public int Progression;
            public string DatetimeLog;
           
        }

        public LogState(string logname, string logfilesource, string logfiletarget, long logsize, double logduration, int nbFilesLeft, string state, int progression, int totalfiles)
        {
            Namelog = logname;
            SourceBackup = logfilesource;
            TargetBackup = logfiletarget;
            TotalFilesSize = logsize;
            NbFilesLeftToDo = nbFilesLeft;
            DateTime = DateTime.Now;
            State = state;
            Progression = progression;
            TotalFilesToCopy = totalfiles;
            Pathlog = $"./StateLogPath/StateLogs";

           



            Directory.CreateDirectory("./StateLogPath");


            string path = $"{Pathlog}.json";
            if (!File.Exists($"{Pathlog}.json"))
            {
                StreamWriter file = File.AppendText($"{Pathlog}.json");
                file.Close();
            }


            string LOGJSONData = File.ReadAllText(path);

            List<LogStateData> logDailyData = JsonConvert.DeserializeObject<List<LogStateData>>(LOGJSONData) ?? new List<LogStateData>();

            logDailyData.Add(new LogStateData()
            {
                TargetBackup = TargetBackup,
                TotalFilesSize = TotalFilesSize,
                DatetimeLog = DateTime.ToString(),
                SourceBackup = SourceBackup,
                TotalFilesToCopy = TotalFilesToCopy,
                Progression = Progression,
                Name = Namelog,
                NbFilesLeftToDo = NbFilesLeftToDo,
                StateLog = State
                


            }) ;

            string ObjectJsonData = JsonConvert.SerializeObject(logDailyData, Newtonsoft.Json.Formatting.Indented);


            File.WriteAllText(path, ObjectJsonData);


        }



    }
}
