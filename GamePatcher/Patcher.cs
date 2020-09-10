using GamePatcher.Properties;
using LibHac;
using Newtonsoft.Json.Linq;
using System.IO;
using Terminal.Gui;
using Xdelta;

namespace GamePatcher
{
    class Patcher
    {

        public static void StartPatchSwitch(string Keyset, NStack.ustring tkeyset, string NSPpath)
        {
            Keyset keyset;
            // Setup Keyset
            if (tkeyset == "") keyset = ExternalKeyReader.ReadKeyFile(Keyset); 
            else
            keyset = ExternalKeyReader.ReadKeyFile(Keyset, tkeyset.ToString());
            JObject Lenguage = JObject.Parse(Resources.en);
            if (!NSPpath.Contains(".nsp"))
            {
                MessageBox.ErrorQuery(20, 7, (string)Lenguage["Error"], "you have not chosen an nsp file", "OK");
            }
            else
            {
                
                string tempdir = GetTemporaryDirectory();
                // Extract nsp

                NSP.ProcessNSP(NSPpath, Path.Combine(tempdir, "NSP"));
                string ncaFile = GetBigfile(Path.Combine(tempdir, "NSP"));

                NCA.ProcessNCA(keyset, Path.Combine(tempdir, "NSP", ncaFile), Path.Combine(tempdir, "Romfs"));
                string[] Gamefile = Directory.GetFiles(Path.Combine(tempdir, "Romfs"), "game.win", SearchOption.AllDirectories);
                string[] Gamefile1 = Directory.GetFiles(Path.Combine(tempdir, "Romfs"), "lang_en.json", SearchOption.AllDirectories);
                string[] Gamefile2 = Directory.GetFiles(Path.Combine(tempdir, "Romfs"), "dontforget.ogg", SearchOption.AllDirectories);
                createoutfolder("Switch");
                var dontforgetogg = new FileStream(Gamefile2[0], FileMode.Open, FileAccess.Read);
                ApplyPatch(dontforgetogg, Resources.dontforget, "Switch/010023800D64A000/romfs/mus/dontforget.ogg");
                // patch game
                var Gamewin = new FileStream(Gamefile[0], FileMode.Open, FileAccess.Read);
                ApplyPatch(Gamewin, Resources.Switch, "Switch/010023800D64A000/romfs/game.win");
                // patch dontforget.ogg

                // patch json
                JObject Json1 = JObject.Parse(Resources.lang);
                JObject Json2 = JObject.Parse(File.ReadAllText(Gamefile1[0]));
                Json2.Merge(Json1, new JsonMergeSettings { MergeArrayHandling = MergeArrayHandling.Union });
                File.WriteAllText("Switch/010023800D64A000/romfs/lang/lang_en.json", Json2.ToString());
                MessageBox.Query(27, 10, "Parche listo", "Se ha creado el parche satisfactoriamente", "OK");
            }
        }

        public static void StartPatchPC(string SurveyDir)
        {
            JObject Lenguage = JObject.Parse(Resources.en);
            string[] Gamefile = Directory.GetFiles(SurveyDir, "data.win", SearchOption.AllDirectories);
            string[] Gamefile1 = Directory.GetFiles(SurveyDir, "lang_en.json", SearchOption.AllDirectories);
            string[] Gamefile2 = Directory.GetFiles(SurveyDir, "dontforget.ogg", SearchOption.AllDirectories);
            if (Gamefile == null && Gamefile1 == null)
            {
                MessageBox.ErrorQuery(20, 7, (string)Lenguage["Error"], "", "OK");
            }
            else
            {
                string tempdir = GetTemporaryDirectory();
                createoutfolder("PC");
                var Gamewin = new FileStream(Path.Combine(SurveyDir, Gamefile[0]), FileMode.Open, FileAccess.Read);
                // patch game
                ApplyPatch(Gamewin, Resources.PC, "PC/data.win");
                // patch dontforget.ogg
                var dontforgetogg = new FileStream(Gamefile2[0], FileMode.Open, FileAccess.Read);
                ApplyPatch(dontforgetogg, Resources.dontforget, "PC/mus/dontforget.ogg");
                // patch json
                JObject Json1 = JObject.Parse(Resources.lang);
                JObject Json2 = JObject.Parse(File.ReadAllText(Gamefile1[0]));
                Json2.Merge(Json1, new JsonMergeSettings { MergeArrayHandling = MergeArrayHandling.Union });
                File.WriteAllText("PC/lang/lang_en.json", Json2.ToString());
                var result = MessageBox.Query(27, 10, "Parche listo", "Se ha creado el parche satisfactoriamente", "OK");
                if (result == 0) Terminal.Gui.Application.Run();
            }

        }

        public static void StartPatchPS4(string pkgpath)
        {
            JObject Lenguage = JObject.Parse(Resources.en);
            if (!pkgpath.Contains(".pkg"))
            {
                MessageBox.ErrorQuery(20, 7, (string)Lenguage["Error"], "you have not chosen an pkg file", "OK");
            }
            else
            {
                string tempdir = GetTemporaryDirectory();
                PKG.extractPKG(Path.Combine(tempdir, "PKG"), pkgpath);
                string[] Gamefile = Directory.GetFiles(Path.Combine(tempdir, "PKG"), "game.win", SearchOption.AllDirectories);
                string[] Gamefile1 = Directory.GetFiles(Path.Combine(tempdir, "PKG"), "lang_en.json", SearchOption.AllDirectories);
                string[] Gamefile2 = Directory.GetFiles(Path.Combine(tempdir, "PKG"), "*.GP4", SearchOption.AllDirectories);
                string[] Gamefile3 = Directory.GetFiles(Path.Combine(tempdir, "PKG"), "dontforget.ogg", SearchOption.AllDirectories);
                createoutfolder("PS4");
                var Gamewin = new FileStream(Gamefile[0], FileMode.Open, FileAccess.Read);
                // patch game
                ApplyPatch(Gamewin, Resources.PS4, Path.Combine(tempdir, "PKG/games/game1.win"));
                Gamewin.Close();
                File.Delete(Path.Combine(tempdir, "PKG/games/game.win"));
                File.Move(Path.Combine(tempdir, "PKG/games/game1.win"), Path.Combine(tempdir, "PKG/games/game.win"));
                // patch dontforget.ogg
                var dontforgetogg = new FileStream(Gamefile3[0], FileMode.Open, FileAccess.Read);
                ApplyPatch(dontforgetogg, Resources.dontforget, Path.Combine(tempdir, "PKG/games/mus/dontforget1.ogg"));
                dontforgetogg.Close();
                File.Delete(Path.Combine(tempdir, "PKG/games/mus/dontforget.ogg"));
                File.Move(Path.Combine(tempdir, "PKG/games/mus/dontforget1.ogg"), Path.Combine(tempdir, "PKG/games/mus/dontforget.ogg"));
                // patch json
                JObject Json1 = JObject.Parse(Resources.lang);
                JObject Json2 = JObject.Parse(File.ReadAllText(Gamefile1[0]));
                Json2.Merge(Json1, new JsonMergeSettings { MergeArrayHandling = MergeArrayHandling.Union });
                File.WriteAllText(Path.Combine(tempdir, "PKG/games/lang/lang_en.json"), Json2.ToString());
                PKG.buikdPKG(Gamefile2[0], "ps4/");
                var result = MessageBox.Query(27, 10, "Parche listo", "Se ha creado el parche satisfactoriamente", "OK");
                if (result == 0) Terminal.Gui.Application.Run();
            }
        }

        public static void StartPatchXbox()
        {
            JObject Lenguage = JObject.Parse(Resources.en);
            MessageBox.ErrorQuery(20, 7, (string)Lenguage["Error"], "Not implemented yet", "OK");
        }

        public static void ApplyPatch(FileStream file, byte[] patch, string outfile)
        {
            var outStream = new FileStream(outfile, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
            Stream xdelta = new MemoryStream(patch);
            Decoder decoder = new Decoder(file, xdelta, outStream);
            decoder.Run();
            outStream.Close();
        }
        private static void createoutfolder(string platform)
        {
            string outputfolder = null;
            string secoutputfolder = null;
            switch (platform)
            {
                case "PS4":
                    outputfolder = "ps4";
                    break;
                case "Switch":
                    outputfolder = "Switch/010023800D64A000/romfs/lang";
                    secoutputfolder = "Switch/010023800D64A000/romfs/mus";
                    break;
                case "PC":
                    outputfolder = "PC/lang";
                    secoutputfolder = "PC/mus";
                    break;
            }
            Directory.CreateDirectory(outputfolder);
            if (secoutputfolder != null) Directory.CreateDirectory(secoutputfolder);
        }
        public static string GetTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }
        public static string GetBigfile(string Dir)
        {
            DirectoryInfo folderInfo = new DirectoryInfo(Dir);
            FileInfo[] files = folderInfo.GetFiles();
            long largestSize = 0;
            string name = null;
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Length > largestSize)
                {
                    largestSize = files[i].Length;
                    name = files[i].Name;
                }
            }
            return name;
        }
    }
}