using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasySave.Model
{
    public class Settings
    {
        public Language setting_language { get; set; }
        public Log_Format setting_log { get; set; }
        public List<String> setting_process { get; set; }




        public void FileSettings()
        {

            string path = @"Settings.json";

            if (!File.Exists(path))
            {
                StreamWriter file = File.AppendText(path);
                file.Close();

            }

            if (new FileInfo(path).Length > 0)
            {
                var settings = File.ReadAllText(path);
                var allsettings = JsonConvert.DeserializeObject<Settings>(settings);
                setting_language = allsettings.setting_language;
                setting_log = allsettings.setting_log;
                setting_process = allsettings.setting_process;


            }

            else
            {
                var settings = new Settings();
                settings.setting_language = Language.fr;
                settings.setting_log = Log_Format.json;
                settings.setting_process = new List<string>() { "notepad" };
                var data = JsonConvert.SerializeObject(settings, Formatting.Indented);
                File.WriteAllText(path, data);

            }

        }

        public void LogJson()
        {
            string path = @"Settings.json";
            var settings = new Settings();
            settings.setting_language = setting_language;
            settings.setting_log = Log_Format.json;
            settings.setting_process = setting_process;
            var data = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(path, data);
        }

        public void LogXml()
        {
            string path = @"Settings.json";
            var settings = new Settings();
            settings.setting_language = setting_language;
            settings.setting_log = Log_Format.xml;
            settings.setting_process = setting_process;
            var data = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(path, data);
        }
    }

}

