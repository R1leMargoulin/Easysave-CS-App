using System;
using EasySave.Model;


namespace EasySave.View
{
    class ViewMenu
    {
        MenuModel model;
        public ViewMenu(MenuModel mdl)
        {
            model = mdl;
        }

        // Display function from a string
        public void Print(string contenu)
        {
            Console.WriteLine(contenu);
        }

        //Retrieves function of the content entered in the console, also dispaly a message from a string
        public string Ask(string contenudemande)
        {
            Console.Write(contenudemande);
            string ret = Console.ReadLine();
            return ret;
        }

        //Retrieves function of the content entered in the console, without any display
        public string AskOnly()
        {
            string rat = Console.ReadLine();
            return rat;
        }

        //Clear function, delete all the content in display in the console
        public void Clear()
        {
            Console.Clear();
        }

    }
}
