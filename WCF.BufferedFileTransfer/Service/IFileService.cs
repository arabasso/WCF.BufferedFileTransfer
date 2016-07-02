using System.IO;

namespace WCF.BufferedFileTransfer.Service
{
    public interface IFileService
    {
        Stream Download(string source);
        void Upload(Stream source, string dest);
    }
}
