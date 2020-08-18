using System;
using System.IO;
using Terminal.Gui;

namespace GamePatcher
{
    class Program : Menu 
    {
        static void Main()
        {
            InitMenu();
            Application.Run();
        }

        public string GetTempDirectory()
        {

            string path = Path.GetRandomFileName();

            Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), path));

            return path;

        }
    }
}
