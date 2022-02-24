using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using System.Xml.XPath;
using System.Xml;
using System.IO;

namespace EasySave.Model
{
    public class ArgsLogDaily{
        public string logname { get; set; }
        public string logfilesource { get; set; }
        public string logfiletarget { get; set; }
        public long logsize { get; set; }
        public double logduration { get; set; }
        public ArgsLogDaily(string alogname, string alogfilesource, string alogfiletarget, long alogsize, double alogduration)
        {
            logname = alogname;
            logfilesource = alogfilesource;
            logfiletarget = alogfiletarget;
            logsize = alogsize;
            logduration = alogduration;
        }



    }
    public class LogDaily
    {
        private string Pathlog { get; set; }
        private string Namelog { get; set; }
        private string Sourcelog { get; set; }
        private string Targetlog { get; set; }
        private long Sizelog { get; set; }
        private double Durationlog { get; set; }
        private DateTime DateTimelog { get; set; }

        private static LogDaily _instance;



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
   

        private LogDaily(ArgsLogDaily arg)
        {
            
            Namelog = arg.logname;
            Sourcelog = arg.logfilesource;
            Targetlog = arg.logfiletarget;
            Sizelog = arg.logsize;
            Durationlog = arg.logduration;
            DateTimelog = DateTime.Now;
            Pathlog = $"./LogPath/Logs_{DateTime.Now:dd-MM-yyyy}";

            //Check if the directory exist and create it if it's doesn't exist
            Directory.CreateDirectory("./LogPath");
            Settings settings = new Settings();
            settings.FileSettings();
            if (settings.setting_log == Log_Format.json)
            {
                //Check if the file exist in the directory and create it if it doesn't exist
                string path = $"{Pathlog}.json";
                if (!File.Exists($"{Pathlog}.json"))
                {
                    StreamWriter file = File.AppendText($"{Pathlog}.json");
                    file.Close();
                }

                string LOGJSONData = File.ReadAllText(path); //Get data from the logFile

                List<LogDailyData> logDailyData = JsonConvert.DeserializeObject<List<LogDailyData>>(LOGJSONData) ?? new List<LogDailyData>(); //Put the LOGJSONdata in List and check if the file is empty

                logDailyData.Add(new LogDailyData()
                {
                   
                    Filesize = Sizelog,
                    time = DateTimelog.ToString(),
                    FileSource = Sourcelog,
                    FileTarget = Targetlog,
                    FileTransferTime = Durationlog,
                    Name = Namelog

                });

                string ObjectJsonData = JsonConvert.SerializeObject(logDailyData, Newtonsoft.Json.Formatting.Indented); //Put the List in a Json string 

                
                File.WriteAllText(path, ObjectJsonData);
               //Write the Json string in the file
            }


            if (settings.setting_log == Log_Format.xml)
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
                        xml.WriteElementString("FileSource",Sourcelog);
                        xml.WriteElementString("FileTarget", Targetlog);
                        xml.WriteElementString("DestinationPath", Sourcelog);
                        xml.WriteElementString("FileSize", Convert.ToString(Sizelog));
                        xml.WriteElementString("FileTransferTime", Convert.ToString(Durationlog));
                        xml.WriteElementString("Time",DateTimelog.ToString());
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
                        xml.WriteElementString("FileSource", Sourcelog);
                        xml.WriteElementString("FileTarget", Targetlog);
                        xml.WriteElementString("DestinationPath", Sourcelog);
                        xml.WriteElementString("FileSize", Convert.ToString(Sizelog));
                        xml.WriteElementString("FileTransferTime", Convert.ToString(Durationlog));
                        xml.WriteElementString("Time", DateTimelog.ToString());
                        xml.WriteEndElement();
                    }

                    xmlDocument.Save(path);
                }


            }
           
           
        }
        public static LogDaily GetInstance(ArgsLogDaily arg)
        {
            if (_instance == null)
            {
                _instance = new LogDaily(arg);
                _instance = null;
            }
           
            return _instance;
        }
    }


}

