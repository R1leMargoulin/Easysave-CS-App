using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave.Model
{
    public class MenuModel
    {
        public string language;
        public List<string> languageList; 
        private int menuView;
        

        public MenuModel()
        {
            languageList = new List<string> { Language.fr.ToString(), Language.en.ToString()};
            language = Convert.ToString(Language.fr);
            menuView = 0;
        }

        public int GetMenuView()
        {
            return menuView;
        }
        public void SetMenuView(int a)
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
           
        }

        public List<String> GetLanguageList()
        {
           return languageList;
        }
    }
}
