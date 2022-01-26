using EasySave.Model;
using System;

namespace EasySave
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Backup backup = new Backup();

            backup.DirectoryCopy();
        }
    }
}
