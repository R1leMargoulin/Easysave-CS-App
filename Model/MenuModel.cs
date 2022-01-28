using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave.Model
{
    public class MenuModel
    {
        public string language;
        public List<string> languageList; 
        private string menuView;
       
        public MenuModel()
        {
            languageList = new List<string> { Language.fr.ToString(), Language.en.ToString()};
            language = Convert.ToString(Language.fr); //default language of the application
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
        }

        public List<String> GetLanguageList()
        {
           return languageList;
        }
    }
}
