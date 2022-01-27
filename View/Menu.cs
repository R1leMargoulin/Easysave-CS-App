using System;
using System.Collections.Generic;
using System.Text;
using EasySave.Model;
using EasySave.Controller;

namespace EasySave.View
{
    class Menu
    {
        MenuModel model;
        public Menu(MenuModel mdl)
        {
            model = mdl;
        }

        public void Print(String a)
        {
            Console.WriteLine(a);
        }
        
    }
}
