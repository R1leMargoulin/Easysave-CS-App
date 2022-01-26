using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.XPath;
using System.Xml;
using System.IO;

namespace EasySave.Controllers
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


        private class LogDailyData
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
            
            try
            {
                Directory.CreateDirectory("./logs");

                switch (logformat)
                {
                    case "xml":
                        {
                            if (!File.Exists($"{Pathlog}.xml"))
                            {
                                XmlWriterSettings xmlSettings = new XmlWriterSettings
                                {
                                    Indent = true,
                                };

                                using XmlWriter xml = XmlWriter.Create($"{Pathlog}.xml", xmlSettings);
                                {
                                    xml.WriteStartElement($"Logs_{DateTime.Now:dd-MM-yyyy}");

                                    xml.WriteStartElement(Namelog);
                                    xml.WriteElementString("SourceFile", Sourcelog);
                                    xml.WriteElementString("DestinationFile", Targetlog);
                                    xml.WriteElementString("DestinationPath", Targetlog.Substring(0, Targetlog.LastIndexOf("\\")));
                                    xml.WriteElementString("Size",Sizelog.ToString());
                                    xml.WriteElementString("TransferTime", Durationlog.ToString());
                                    xml.WriteElementString("Time",DateTimelog.ToString());
                                    xml.WriteEndElement();  
                                }
                            }
                            else
                            {
                                XmlDocument xmlDocument= new XmlDocument();
                                xmlDocument.Load($"{Pathlog}.xml");

                                XPathNavigator navigator = xmlDocument.CreateNavigator();
                                navigator.MoveToChild($"Logs_{DateTime.Now:dd-MM-yyyy}","");

                                using (XmlWriter xml = navigator.AppendChild())
                                {
                                    xml.WriteStartElement(Namelog);
                                    xml.WriteElementString("SourceFile", Sourcelog);
                                    xml.WriteElementString("DestinationFile", Targetlog);
                                    xml.WriteElementString("DestinationPath", Targetlog.Substring(0, Targetlog.LastIndexOf("\\")));
                                    xml.WriteElementString("Size", Sizelog.ToString());
                                    xml.WriteElementString("TransferTime", Durationlog.ToString());
                                    xml.WriteElementString("Time", DateTimelog.ToString());
                                    xml.WriteEndElement();

                                }

                                xmlDocument.Save($"{Pathlog}.xml");
                            }
                        }
                        break;
                }
                switch (logformat)
                {
                    case "json":
                        {
                            if(!File.Exists($"{Pathlog}.json"))
                            {
                                File.Create($"{Pathlog}.json");
                            }

                            string LOGJSONData = File.ReadAllText($"{Pathlog}.json");
                            
                            List<LogDailyData> datainjson = JsonConvert.DeserializeObject<List<LogDailyData>>(LOGJSONData) ?? new List<LogDailyData>();

                            datainjson.Add(new LogDailyData());


                        }       
                        break;

                    case "xml":
                        {
                            if (!File.Exists($"{Pathlog}.xml"))
                            {
                                File.Create($"{Pathlog}.xml");
                            }
                        }
                        break ;


                        

                        
                }
            }
            catch (Exception exception)
            {
                //Console.WriteLine(exception);
            }
        }




    }

    public class LogState
    {
        private string Pathlog { get; set; }
        private string Namelog { get; set; }
        private string Sourcelog { get; set; }
        private string Targetlog { get; set; }
        private long Sizelog { get; set; }
        private double Durationlog { get; set; }
        private DateTime DateTimelog { get; set; }

        private class LogStateData
        {
            public string Name;
            public string SourceFilePath;
            public string TargetFilePath;
            public string State;
            public int TotalFilesToCopy;
            public long TotalFilesSize;
            public int NbFilesLeftToDo;
            public long FilesSizeLeftToDo;
            public int Progression;
        }

        public LogState(string logname, string logfilesource, string logfiletarget, long logsize, double logduration)
        {
            Namelog = logname;
            Sourcelog = logfilesource;
            Targetlog = logfiletarget;
            Sizelog = logsize;
            Durationlog = logduration;
            DateTimelog = DateTime.Now;
            Pathlog = $"./LogPath/Logs_{DateTime.Now:dd-MM-yyyy}";
        }

    }
}