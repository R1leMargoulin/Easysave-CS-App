﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EasySave
{
    class Model
    {
        private String language;
        private int menuView;

        public Model()
        {
            language = "fr";
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
        public String GetLanguage()
        {
            return language;
        }
    }
}
