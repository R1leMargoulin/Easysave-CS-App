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

            //Check if the directory exist and create it if it's doesn't exist
            Directory.CreateDirectory("./LogPath");

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

                List<LogDailyData> logDailyData = JsonConvert.DeserializeObject<List<LogDailyData>>(LOGJSONData) ?? new List<LogDailyData>(); //Put the LOGJSONdata in List and check if the file is empty

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
                        xml.WriteStartElement($"logs_{ DateTime.Now:dd - MM - yyyy}");
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
                    xmlDocument.Load($"{path}.xml");

                    XPathNavigator navigator = xmlDocument.CreateNavigator();

                    using (XmlWriter xml = navigator.AppendChild())
                    {
                        xml.WriteStartElement($"logs_{ DateTime.Now:dd - MM - yyyy}");
                        xml.WriteStartElement(Namelog);
                        xml.WriteElementString("FileSource", Sourcelog);
                        xml.WriteElementString("FileTarget", Targetlog);
                        xml.WriteElementString("DestinationPath", Sourcelog);
                        xml.WriteElementString("FileSize", Convert.ToString(Sizelog));
                        xml.WriteElementString("FileTransferTime", Convert.ToString(Durationlog));
                        xml.WriteElementString("Time", DateTimelog.ToString());
                        xml.WriteEndElement();

                    }
                    xmlDocument.Save($"{path}.xml");
                }


            }

        }
    }


}

