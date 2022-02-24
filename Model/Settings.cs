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
        public  List<String> setting_importantfile { get; set; }
        public List<String> setting_encryptfile { get; set; }   




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
                setting_encryptfile = allsettings.setting_encryptfile;
                setting_importantfile = allsettings.setting_importantfile;





            }

            else
            {
                var settings = new Settings();
                settings.setting_language = Language.fr;
                settings.setting_log = Log_Format.json;
                settings.setting_process = new List<string>() { "notepad" };
                settings.setting_importantfile = new List<string>() { ".pdf" };
                settings.setting_encryptfile = new List<string>() { ".txt" };
                var data = JsonConvert.SerializeObject(settings, Formatting.Indented);
                File.WriteAllText(path, data);

            }

        }

        public void LogJson()
        {
            string path = @"Settings.json";
            var settingss = File.ReadAllText(path);
            var allsettings = JsonConvert.DeserializeObject<Settings>(settingss);
            var settings = new Settings();
            settings.setting_language = allsettings.setting_language;
            settings.setting_log = Log_Format.json;
            settings.setting_process = allsettings.setting_process;
            settings.setting_importantfile = allsettings.setting_importantfile;
            settings.setting_encryptfile = allsettings.setting_encryptfile;
            var data = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(path, data);
        }

        public void LogXml()
        {
            string path = @"Settings.json";
            var settingss = File.ReadAllText(path);
            var allsettings = JsonConvert.DeserializeObject<Settings>(settingss);
            var settings = new Settings();
            settings.setting_language = allsettings.setting_language;
            settings.setting_log = Log_Format.xml;
            settings.setting_process = allsettings.setting_process;
            settings.setting_importantfile = allsettings.setting_importantfile;
            settings.setting_encryptfile = allsettings.setting_encryptfile;
            var data = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(path, data);
        }


        public void ProcessAdd(string process)
        {
            string path = @"Settings.json";
            var settingss = File.ReadAllText(path);
            var allsettings = JsonConvert.DeserializeObject<Settings>(settingss);
            var settings = new Settings();
            settings.setting_language = allsettings.setting_language;
            settings.setting_log = allsettings.setting_log;
            allsettings.setting_process.Add(process);
            settings.setting_process = allsettings.setting_process;
            settings.setting_importantfile = allsettings.setting_importantfile;
            settings.setting_encryptfile = allsettings.setting_encryptfile;
            var data = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(path, data);
        }

        public void ProcessDelete(List<string> process)
        {
            string path = @"Settings.json";
            var settingss = File.ReadAllText(path);
            var allsettings = JsonConvert.DeserializeObject<Settings>(settingss);
            var settings = new Settings();
            settings.setting_language = allsettings.setting_language;
            settings.setting_log = allsettings.setting_log;
            settings.setting_process = process;
            settings.setting_importantfile = allsettings.setting_importantfile;
            settings.setting_encryptfile = allsettings.setting_encryptfile;
            var data = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(path, data);
        }

        public void ImportantFileAdd(string importantfile)
        {
            string path = @"Settings.json";
            var settingss = File.ReadAllText(path);
            var allsettings = JsonConvert.DeserializeObject<Settings>(settingss);
            var settings = new Settings();
            settings.setting_language = allsettings.setting_language;
            settings.setting_log = allsettings.setting_log;
            settings.setting_process = allsettings.setting_process;
            allsettings.setting_importantfile.Add(importantfile);
            settings.setting_importantfile = allsettings.setting_importantfile;
            settings.setting_encryptfile = allsettings.setting_encryptfile;
            var data = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(path, data);
        }

        public void ImportantFileDelete(List<string> importantfile)
        {
            string path = @"Settings.json";
            var settingss = File.ReadAllText(path);
            var allsettings = JsonConvert.DeserializeObject<Settings>(settingss);
            var settings = new Settings();
            settings.setting_language = allsettings.setting_language;
            settings.setting_log = allsettings.setting_log;
            settings.setting_process = allsettings.setting_process;
            settings.setting_importantfile = importantfile;
            settings.setting_encryptfile = allsettings.setting_encryptfile;
            var data = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(path, data);
        }

        public void EncryptFileAdd(string importantfile)
        {
            string path = @"Settings.json";
            var settingss = File.ReadAllText(path);
            var allsettings = JsonConvert.DeserializeObject<Settings>(settingss);
            var settings = new Settings();
            settings.setting_language = allsettings.setting_language;
            settings.setting_log = allsettings.setting_log;
            settings.setting_process = allsettings.setting_process;
            allsettings.setting_importantfile.Add(importantfile);
            settings.setting_importantfile = allsettings.setting_importantfile;
            allsettings.setting_encryptfile.Add(importantfile);
            settings.setting_encryptfile = allsettings.setting_encryptfile;
            var data = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(path, data);
        }

        public void EncryptFileDelete(List<string> encryptfile)
        {
            string path = @"Settings.json";
            var settingss = File.ReadAllText(path);
            var allsettings = JsonConvert.DeserializeObject<Settings>(settingss);
            var settings = new Settings();
            settings.setting_language = allsettings.setting_language;
            settings.setting_log = allsettings.setting_log;
            settings.setting_process = allsettings.setting_process;
            settings.setting_importantfile = allsettings.setting_importantfile;
            settings.setting_encryptfile = encryptfile;
            var data = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(path, data);
        }

    }

}

