using Terminal.Gui;
using System;
using System.Runtime.InteropServices;

namespace GamePatcher
{
    class Program : Menu 
    {
        public static class OperatingSystem
        {
            public static bool IsWindows() =>
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

            public static bool IsMacOS() =>
                RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

            public static bool IsLinux() =>
                RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        }
        
        static void Main()
        {
            if (OperatingSystem.IsWindows() == true) {
                Console.SetWindowSize(120,30);
            }
            InitMenu();
            Application.Run();
        }
    }
}
