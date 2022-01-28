using System;
using EasySave.Model;
using EasySave.Controllers;
using EasySave.View;

namespace EasySave
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MenuModel model = new MenuModel();
            ViewMenu view = new ViewMenu(model);
            MenuControl control = new MenuControl(model, view);
            control.Start();
        }
    }
}