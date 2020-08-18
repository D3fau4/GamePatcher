using LibOrbisPkg.GP4;
using LibOrbisPkg.PFS;
using LibOrbisPkg.PKG;
using LibOrbisPkg.SFO;
using LibOrbisPkg.Util;
using System.IO;

namespace GamePatcher {
    class PKG {
        public static void extractPKG(string OutDir, string FilePkg){
            Gp4Creator.CreateProjectFromPKG(OutDir, FilePkg, null);
        }
        public static void buikdPKG(string Gp4File, string OutPkg){
            var project = Gp4Project.ReadFrom(File.OpenRead(Gp4File));
            var props = PkgProperties.FromGp4(project, Path.GetDirectoryName(Gp4File));
            new PkgBuilder(props).Write(Path.Combine(OutPkg, $"{project.volume.Package.ContentId}.pkg"));
        }
    }
}