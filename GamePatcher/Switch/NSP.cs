using LibHac.FsSystem;
using System.IO;

namespace GamePatcher
{
    internal static class NSP
    {
        public static void ProcessNSP(string PathFile, string OutDir)
        {
            using (var file = new LocalStorage(PathFile, FileAccess.Read))
            {
                var pfs = new PartitionFileSystem(file);

                if (OutDir != null)
                {
                    pfs.Extract(OutDir);
                }
            }
        }
    }
}