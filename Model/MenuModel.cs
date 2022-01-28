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
            //string jsonSettings = File.ReadAllText(@"Settings.json");
            // Settings settings = JsonSerializer.Deserialize<Settings>(jsonSettings); //reprise des parametres mis dans le fichier settings.json
            // language = settings.setting_language;
            language = Convert.ToString(Language.fr);
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

        public void SetLanguage(string lang)
        {
            language = lang;
            SettingUpdate(); //to save our language setting even if we close the app
           
        }

        public List<String> GetLanguageList()
        {
            return languageList;
        }

        public void SettingUpdate()
        {
            //on a first hand, we read the file and change only what we want to change in it
            //(this will be usefull if we want to easily add settings content)
            string jsonSettings = File.ReadAllText(@"Settings.json");
            Settings settings = JsonSerializer.Deserialize<Settings>(jsonSettings);
            settings.setting_language = language;

            //then, on another hand, we save our file settings
            jsonSettings = JsonSerializer.Serialize(settings);
            File.WriteAllText(@"Settings.json", jsonSettings);
        }

    }
}
