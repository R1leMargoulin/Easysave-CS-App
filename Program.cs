using System;

namespace EasySave
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Model model = new Model();
            Controller control = new Controller(model);
            View view = new View(model, control);

            view.Start();
        }
    }
}
