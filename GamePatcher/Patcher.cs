using Terminal.Gui;
using GamePatcher.Properties;
using Newtonsoft.Json.Linq;

namespace GamePatcher
{
    class Patcher
    {
        
        public static void StartPatchSwitch() {
            JObject Lenguage = JObject.Parse(Resources.en);
            MessageBox.ErrorQuery(20,7, (string)Lenguage["Error"], "Not implemented yet", "OK");
        }

        public static void StartPatchPC() {
            JObject Lenguage = JObject.Parse(Resources.en);
            MessageBox.ErrorQuery(20,7, (string)Lenguage["Error"], "Not implemented yet", "OK");
        }
        public static void StartPatchPS4() {
            JObject Lenguage = JObject.Parse(Resources.en);
            MessageBox.ErrorQuery(20,7, (string)Lenguage["Error"], "Not implemented yet", "OK");
        }
        public static void StartPatchXbox() {
            JObject Lenguage = JObject.Parse(Resources.en);
            MessageBox.ErrorQuery(20,7, (string)Lenguage["Error"], "Not implemented yet", "OK");
        }
    }
}