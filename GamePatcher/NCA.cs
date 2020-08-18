using System.IO;
using Terminal.Gui;
using LibHac;
using LibHac.Common;
using LibHac.Fs;
using LibHac.FsSystem;
using LibHac.FsSystem.NcaUtils;
using LibHac.Npdm;

namespace GamePatcher
{
    internal static class NCA
    {
        public static void ProcessNCA(Keyset keyset, string ncapath, string outdir)
        {
            using (IStorage file = new LocalStorage(ncapath, FileAccess.Read))
            {
                var ctx = new Context();
                var nca = new Nca(keyset, file);
                Nca baseNca = null;

                var ncaHolder = new NcaHolder { Nca = nca };
                if (outdir != null || outdir != null)
                {
                    if (!nca.SectionExists(NcaSectionType.Data))
                    {
                        // set error
                        MessageBox.ErrorQuery(20, 7, "Error", "Not implemented yet", "OK");
                        //ctx.Logger.LogMessage("NCA has no RomFS section");
                        return;
                    }

                    

                    if (outdir != null)
                    {
                        FileSystemClient fs = new FileSystemClient(new StopWatchTimeSpanGenerator());

                        fs.Register("rom".ToU8Span(), OpenFileSystemByType(NcaSectionType.Data));
                        fs.Register("output".ToU8Span(), new LocalFileSystem(outdir));

                        FsUtils.CopyDirectoryWithProgress(fs, "rom:/".ToU8Span(), "output:/".ToU8Span(), logger: ctx.Logger).ThrowIfFailure();

                        fs.Unmount("rom".ToU8Span());
                        fs.Unmount("output".ToU8Span());
                    }
                }
                IStorage OpenStorage(int index)
                {
                    if (ctx.Options.Raw)
                    {
                        if (baseNca != null) return baseNca.OpenRawStorageWithPatch(nca, index);

                        return nca.OpenRawStorage(index);
                    }

                    if (baseNca != null) return baseNca.OpenStorageWithPatch(nca, index, 0);

                    return nca.OpenStorage(index, 0);
                }

                IFileSystem OpenFileSystem(int index)
                {
                    if (baseNca != null) return baseNca.OpenFileSystemWithPatch(nca, index, 0);

                    return nca.OpenFileSystem(index, 0);
                }

                IStorage OpenStorageByType(NcaSectionType type)
                {
                    return OpenStorage(Nca.GetSectionIndexFromType(type, nca.Header.ContentType));
                }

                IFileSystem OpenFileSystemByType(NcaSectionType type)
                {
                    return OpenFileSystem(Nca.GetSectionIndexFromType(type, nca.Header.ContentType));
                }
            }

        }

        private class NcaHolder
        {
            public Nca Nca;
            public Validity[] Validities = new Validity[4];
        }
    }
}