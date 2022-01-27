using EasySave.Model;
using System;

namespace EasySave
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Backup backup = new Backup();
           
            backup.DirectoryCopy(@"C:\Users\Utilisateur\Documents\cesi\Stage A2", @"C:\Users\Utilisateur\Documents\test");
        }
    }
}
