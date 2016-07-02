using System.IO;

namespace WCF.BufferedFileTransfer.Service
{
    public class BufferedFileService : IFileService
    {
        readonly IBufferedTransferService _transferService;

        public BufferedFileService(IBufferedTransferService transferService)
        {
            _transferService = transferService;
        }

        public void Upload(Stream source, string dest)
        {
            var buffer = new byte[0x1000];
            int bytes, offset = 0;

            while((bytes = source.Read(buffer, 0, 0x1000)) > 0)
            {
                _transferService.WriteBytes(dest, offset, buffer, bytes);

                offset += bytes;
            }
        }

        public Stream Download(string source)
        {
            var stream = new TempFileStream();

            var offset = 0;

            var chunk = _transferService.ReadBytes(source, offset, 0x1000);

            while(chunk.Count > 0)
            {
                stream.Write(chunk.Bytes, offset, chunk.Count);

                offset += chunk.Count;

                chunk = _transferService.ReadBytes(source, offset, 0x1000);
            }

            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }
    }
}
