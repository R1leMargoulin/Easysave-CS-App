using System;
using EasySave.Model;
using EasySave.Controller;
using EasySave.View;

namespace EasySave
{
    internal class Program
    {
        static void Main(string[] args)
        {

            MenuModel model = new MenuModel();
            Menu view = new Menu(model);
            MenuControl control = new MenuControl(model, view);

            control.Start();
        }
    }
}
