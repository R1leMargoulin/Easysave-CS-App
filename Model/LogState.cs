using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Text;
using System.Xml.XPath;
using System.Xml;

namespace EasySave.Model
{
    public class ArgsLogState
    {
        public string logname { get; set; }
        public string logfilesource { get; set; }
        public string logfiletarget { get; set; }
        public long logsize { get; set; }
        public double logduration { get; set; }
        public int nbFilesLeft { get; set; }
        public string state { get; set; }
        public int progression { get; set; }
        public int totalfiles { get; set; }

        public ArgsLogState(string alogname, string alogfilesource, string alogfiletarget, long alogsize, double alogduration, int anbFilesLeft, string astate, int aprogression, int atotalfiles)
        {
            logname = alogname;
            logfilesource = alogfilesource;
            logfiletarget = alogfiletarget;
            logsize = alogsize;
            logduration = alogduration;
            nbFilesLeft = anbFilesLeft;
            state = astate;
            progression = aprogression;
            totalfiles = atotalfiles;
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

        private static LogState _instance;

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

        public LogState(ArgsLogState arg)
        {
            Namelog = arg.logname;
            SourceBackup = arg.logfilesource;
            TargetBackup = arg.logfiletarget;
            TotalFilesSize = arg.logsize;
            NbFilesLeftToDo = arg.nbFilesLeft;
            DateTime = DateTime.Now;
            State = arg.state;
            Progression = arg.progression;
            TotalFilesToCopy = arg.totalfiles;
            Pathlog = $"./StateLogPath/StateLogs";

        //Check if the directory exist and create it if it's doesn't exist
        Directory.CreateDirectory("./StateLogPath");

            if (Settings.setting_log == Log_Format.json)
            {
                //Check if the file exist in the directory and create it if it doesn't exist
                string path = $"{Pathlog}.json";
                if (!File.Exists($"{Pathlog}.json"))
                {
                    StreamWriter file = File.AppendText($"{Pathlog}.json");
                    file.Close();
                }

                string LOGJSONData = File.ReadAllText(path); //Get data from the logFile

                List<LogStateData> logDailyData = JsonConvert.DeserializeObject<List<LogStateData>>(LOGJSONData) ?? new List<LogStateData>(); //Put the LOGJSONdata in List and check if the file is empty

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
                });

                string ObjectJsonData = JsonConvert.SerializeObject(logDailyData, Newtonsoft.Json.Formatting.Indented); //Put the List in a Json string 

                File.WriteAllText(path, ObjectJsonData); //Write the Json string in the file
            }



            if (Settings.setting_log == Log_Format.xml)
            {
                //Check if the file exist in the directory and create it if it doesn't exist
                string path = $"{Pathlog}.xml";
                if (!File.Exists($"{Pathlog}.xml"))
                {
                    XmlWriterSettings xmlWriterSettings = new XmlWriterSettings { Indent = true };
                    using (XmlWriter xml = XmlWriter.Create(path, xmlWriterSettings))
                    {
                        xml.WriteStartElement($"logs_{ DateTime.Now:dd-MM-yyyy}");
                        xml.WriteStartElement(Namelog);
                        xml.WriteElementString("FileSource", SourceBackup);
                        xml.WriteElementString("FileTarget", TargetBackup);
                        xml.WriteElementString("DestinationPath", SourceBackup);
                        xml.WriteElementString("TotalFilesSize", Convert.ToString(TotalFilesSize));
                        xml.WriteElementString("FileTransferTime", Convert.ToString(Progression));
                        xml.WriteElementString("Statelog", State);
                        xml.WriteElementString("Time", DateTime.ToString());
                        xml.WriteEndElement();

                    }
                }
                else
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.Load(path);

                    XPathNavigator navigator = xmlDocument.CreateNavigator();
                    navigator.MoveToChild($"logs_{ DateTime.Now:dd-MM-yyyy}", "");

                    using (XmlWriter xml = navigator.AppendChild())
                    {

                        //xml.WriteStartElement($"logs_{ DateTime.Now:dd-MM-yyyy}");
                        xml.WriteStartElement(Namelog);
                        xml.WriteElementString("FileSource", SourceBackup);
                        xml.WriteElementString("FileTarget", TargetBackup);
                        xml.WriteElementString("DestinationPath", SourceBackup);
                        xml.WriteElementString("TotalFilesSize", Convert.ToString(TotalFilesSize));
                        xml.WriteElementString("FileTransferTime", Convert.ToString(Progression));
                        xml.WriteElementString("Statelog", State);
                        xml.WriteElementString("Time", DateTime.ToString());
                        xml.WriteEndElement();

                    }
                    xmlDocument.Save(path);
                }


            }
        }
        public static LogState GetInstance(ArgsLogState arg)
        {
            if (_instance == null)
            {
                _instance = new LogState(arg);
            }
            return _instance;
        }
    }

}