using Terminal.Gui;
using System.IO;
using LibHac;
using GamePatcher.Properties;
using Newtonsoft.Json.Linq;

namespace GamePatcher
{
    class Patcher
    {

        public static void StartPatchSwitch(string Keyset, string tkeyset, string NSPpath)
        {
            Keyset keyset;
            // Setup Keyset
            /*if (tkeyset == " ") keyset = ExternalKeyReader.ReadKeyFile(Keyset); else */keyset = ExternalKeyReader.ReadKeyFile(Keyset);
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
                NCA.ProcessNCA(keyset, Path.Combine(tempdir, "NSP", ncaFile), Path.Combine(tempdir, "NCA"));
                MessageBox.ErrorQuery(20, 7, (string)Lenguage["Error"], "Not implemented yet" + tempdir, "OK");
            }
        }

        public static void StartPatchPC()
        {
            JObject Lenguage = JObject.Parse(Resources.en);
            MessageBox.ErrorQuery(20, 7, (string)Lenguage["Error"], "Not implemented yet", "OK");
        }

        public static void StartPatchPS4()
        {
            JObject Lenguage = JObject.Parse(Resources.en);
            MessageBox.ErrorQuery(20, 7, (string)Lenguage["Error"], "Not implemented yet", "OK");
        }

        public static void StartPatchXbox()
        {
            JObject Lenguage = JObject.Parse(Resources.en);
            MessageBox.ErrorQuery(20, 7, (string)Lenguage["Error"], "Not implemented yet", "OK");
        }

        public static string GetTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }
        public static string GetBigfile(string Dir){
                DirectoryInfo folderInfo = new DirectoryInfo(Dir);
                FileInfo[] files = folderInfo.GetFiles();
                long largestSize = 0;
                string name = null;
                for (int i = 0; i < files.Length; i++)
                {
                    if (files[i].Length > largestSize) {
                        largestSize = files[i].Length;
                        name = files[i].Name;
                    }
                }
                return name;
        }
    }
}