using GamePatcher.Properties;
using Newtonsoft.Json.Linq;
using System;
using Terminal.Gui;

namespace GamePatcher
{
    class Menu : Patcher
    {
        public static void InitMenu()
        {

            JObject Lenguage = JObject.Parse(Resources.en);
            // Start App
            Application.Init();
            var top = Application.Top;
            var Main = new Terminal.Gui.Window((string)Lenguage["Title"])
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };
            top.Add(Main);
            // Create a menu
            var menu = new MenuBar(new MenuBarItem[] {
            new MenuBarItem ((string) Lenguage["Menu_File"], new MenuItem [] {
                new MenuItem ((string) Lenguage["Menu_File_Exit"], "", null)
            })});
            top.Add(menu);
            Main.Add(
                // Switch
                new Button(10, 2, (string)Lenguage["Menu_patch_Switch"])
                {
                    Clicked = () =>
                    {
                        var result = MessageBox.Query(20, 7, (string)Lenguage["Menu_warning_title"], (string)Lenguage["Menu_warning_install"], (string)Lenguage["Menu_Confirm"], (string)Lenguage["Menu_Decline"]);
                        if (result == 0)
                        {
                            SwitchDialoge();
                            //MessageBox.ErrorQuery(20, 7, (string)Lenguage["Error"], "Not implemented yet", "OK");
                        }
                    }
                },
                // PS4
                new Button(10, 3, (string)Lenguage["Menu_patch_PS4"])
                {
                    Clicked = () =>
                    {
                        var result = MessageBox.Query(20, 7, (string)Lenguage["Menu_warning_title"], (string)Lenguage["Menu_warning_install"], (string)Lenguage["Menu_Confirm"], (string)Lenguage["Menu_Decline"]);
                        if (result == 0)
                        {
                            PS4Dialoge();
                            //MessageBox.ErrorQuery(20, 7, (string)Lenguage["Error"], "Not implemented yet", "OK");
                        }
                    }
                },
                // PC
                new Button(10, 4, (string)Lenguage["Menu_patch_PC"])
                {
                    Clicked = () =>
                    {
                        var result = MessageBox.Query(20, 7, (string)Lenguage["Menu_warning_title"], (string)Lenguage["Menu_warning_install"], (string)Lenguage["Menu_Confirm"], (string)Lenguage["Menu_Decline"]);
                        if (result == 0)
                        {
                            PCDialoge();
                        }
                    }
                },
                // XBOX
                new Button(10, 5, (string)Lenguage["Menu_patch_Xbox"])
                {
                    Clicked = () =>
                    {
                        var result = MessageBox.Query(20, 7, (string)Lenguage["Menu_warning_title"], (string)Lenguage["Menu_warning_install"], (string)Lenguage["Menu_Confirm"], (string)Lenguage["Menu_Decline"]);
                        if (result == 0)
                        {
                            //XboxDialoge();
                            MessageBox.ErrorQuery(20, 7, (string)Lenguage["No"], "Nope, no por ahora", "OK");
                        }
                    }
                }
                );
        }

        private static void SwitchDialoge()
        {
            var Patch_Window = new Terminal.Gui.Window("Switch")
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };
            var NSPlabel = new Label ("NSP path: ") { X = 5, Y = 2 };
            var NSPpath = new TextField(""){ X = 19, Y = 2, Width = 50};
            var NSP_Button = new Button(70, 2, "Examinar"){ Clicked = () => {
                
                var File = new OpenDialog("Open","Select the NSP");
                Application.Run(File);
                if(!File.Canceled) NSPpath.Text = File.FilePath;
            }};
            var KeysetLabel = new Label("Keyset path: ") {X = 5, Y = 4};
            var KeysetPath = new TextField(""){X = 19, Y = 4, Width = 50};
            var Keyset_Button = new Button(70, 4, "Examinar"){ Clicked = () => {
                var File = new OpenDialog("Open","Select the Keyset");
                Application.Run(File);
                if(!File.Canceled) KeysetPath.Text = File.FilePath;
            }};
            var tkeysetLabel = new Label("TkeySet path: ") {X = 5, Y = 6};
            var tkeysetPath = new TextField(""){X = 19, Y = 6, Width = 50};
            var tkeyset_Button = new Button(70,6, "Examinar"){ Clicked = () => {
                var File = new OpenDialog("Open","Select the Tkeyset");
                Application.Run(File);
                if(!File.Canceled) tkeysetPath.Text = File.FilePath;
            }};
            var OK = new Button(5,8,"Ok",true){
                Clicked = () => {
                    if (KeysetPath.Text != null && NSPpath.Text != null) {
                        StartPatchSwitch(KeysetPath.Text.ToString(), tkeysetPath.Text.ToString(), NSPpath.Text.ToString());
                    }
                }
            };
            var Cancel = new Button(16,8,"Cancel",true) {
                Clicked = () => Application.Run()
            };
            
            Patch_Window.Add(NSPlabel, NSPpath, NSP_Button, KeysetLabel, KeysetPath, Keyset_Button, tkeysetLabel, tkeysetPath, tkeyset_Button, OK, Cancel);
            Application.Run(Patch_Window);
        }

        private static void PCDialoge()
        {
            var Patch_Window = new Terminal.Gui.Window("PC")
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            var Surveylabel = new Label ("Folder path: ") { X = 5, Y = 2 };
            var Surveylabelpath = new TextField(""){ X = 19, Y = 2, Width = 50};
            var Surveylabel_Button = new Button(70, 2, "Examinar"){ Clicked = () => {
                
                var File = new OpenDialog("Open","Select the Folder");
                Application.Run(File);
                if(!File.Canceled) Surveylabelpath.Text = File.FilePath;
            }};

            var OK = new Button(5,6,"Ok",true){
                Clicked = () => StartPatchPC(Surveylabelpath.Text.ToString())
            };
            var Cancel = new Button(16,6,"Cancel",true) {
                Clicked = () => Application.Run()
            };
            
            Patch_Window.Add( OK, Cancel, Surveylabel, Surveylabelpath, Surveylabel_Button);
            Application.Run(Patch_Window);
        }

        private static void PS4Dialoge()
        {
            var Patch_Window = new Terminal.Gui.Window("PS4")
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            var PKGlabel = new Label ("PKG path: ") { X = 5, Y = 2 };
            var PKGpath = new TextField(""){ X = 19, Y = 2, Width = 50};
            var PKG_Button = new Button(70, 2, "Examinar"){ Clicked = () => {
                
                var File = new OpenDialog("Open","Select the NSP");
                Application.Run(File);
                if(!File.Canceled) PKGpath.Text = File.FilePath;
            }};

            var OK = new Button(5,6,"Ok",true){
                Clicked = () => StartPatchPS4(PKGpath.Text.ToString())
            };
            var Cancel = new Button(16,6,"Cancel",true) {
                Clicked = () => Application.Run()
            };
            
            Patch_Window.Add(OK, Cancel, PKGlabel, PKGpath, PKG_Button);
            Application.Run(Patch_Window);
        }

        private static void XboxDialoge()
        {
            var Patch_Window = new Terminal.Gui.Window("Xbox")
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };

            var OK = new Button(5,6,"Ok",true){
                Clicked = () => StartPatchXbox()
            };
            var Cancel = new Button(16,6,"Cancel",true) {
                Clicked = () => Application.Run()
            };
            
            Patch_Window.Add( OK, Cancel);
            Application.Run(Patch_Window);
        }
    }
}