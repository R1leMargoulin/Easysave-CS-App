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

        public void Print(String a)
        {
            Console.WriteLine(a);
        }
        public String Ask()
        {
            String ret = Console.ReadLine();
            return ret;
        }

        public void Clear()
        {
            Console.Clear();
        }
        
    }
}
