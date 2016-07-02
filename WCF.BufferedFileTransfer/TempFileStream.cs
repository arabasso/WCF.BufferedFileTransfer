using System.IO;

namespace WCF.BufferedFileTransfer
{
    public class TempFileStream : FileStream
    {
        public TempFileStream() :
            base(Path.GetTempFileName(), FileMode.Create, FileAccess.ReadWrite, FileShare.Read, 0x1000, FileOptions.DeleteOnClose) { }
        public TempFileStream(FileAccess access) :
            base(Path.GetTempFileName(), FileMode.Create, access, FileShare.Read, 0x1000, FileOptions.DeleteOnClose) { }
        public TempFileStream(FileAccess access, FileShare share) :
            base(Path.GetTempFileName(), FileMode.Create, access, share, 0x1000, FileOptions.DeleteOnClose) { }
        public TempFileStream(FileAccess access, FileShare share, int bufferSize) :
            base(Path.GetTempFileName(), FileMode.Create, access, share, bufferSize, FileOptions.DeleteOnClose) { }
    }
}
