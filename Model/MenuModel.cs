using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace EasySave.Model
{
    public class MenuModel
    {
        public string language;
        public List<string> languageList; 
        private string menuView;
        

        public MenuModel()
        {
            string jsonSettings = File.ReadAllText(@"Settings.json");
            Settings settings = JsonSerializer.Deserialize<Settings>(jsonSettings);
            language = settings.setting_language;
            languageList = new List<string> { Language.fr.ToString(), Language.en.ToString()};
            menuView = "0";
        }

        public string GetMenuView()
        {
            return menuView;
        }
        public void SetMenuView(string a)
        {
            menuView = a;
        }
        //public String GetLanguage()
        //{
        //    return language;
        //}

        public void SetLanguage(string lang)
        {
            language = lang;
            SettingUpdate();
           
        }

        public List<String> GetLanguageList()
        {
           return languageList;
        }

        public void SettingUpdate()
        {
            string jsonSettings = File.ReadAllText(@"Settings.json");
            Settings settings = JsonSerializer.Deserialize<Settings>(jsonSettings);
            settings.setting_language = language;

            jsonSettings = JsonSerializer.Serialize(settings);
            File.WriteAllText(@"Settings.json", jsonSettings);
        }

    }
}
