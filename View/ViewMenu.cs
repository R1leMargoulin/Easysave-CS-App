using System;
using System.Collections.Generic;
using System.Text;
using EasySave.Model;
using EasySave.Controller;

namespace EasySave.View
{
    class ViewMenu
    {
        MenuModel model;
        public ViewMenu(MenuModel mdl)
        {
            model = mdl;
        }

        public void Print(String contenu)
        {
            Console.WriteLine(contenu);
        }
        public String Ask(string contenudemande)
        {
            Console.Write(contenudemande);
            String ret = Console.ReadLine();
            return ret;
        }
        public String AskOnly()
        {
            String rat = Console.ReadLine();
            return rat;
        }


        public void Clear()
        {
            Console.Clear();
        }
        
    }
}
