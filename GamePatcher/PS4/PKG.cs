using LibOrbisPkg.GP4;

namespace GamePatcher {
    class PKG {
        public static void extractPKG(string OutDir, string FilePkg){
            Gp4Creator.CreateProjectFromPKG(OutDir, FilePkg, null);
        }
    }
}