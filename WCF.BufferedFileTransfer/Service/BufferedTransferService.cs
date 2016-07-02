using System.IO;

namespace WCF.BufferedFileTransfer.Service
{
    public class BufferedTransferService : IBufferedTransferService
    {
        public Chunk ReadBytes(string filename, int offset, int count)
        {
            var chunk = new Chunk(count);

            using(var stream = File.OpenRead(filename))
            {
                if (stream.Length > offset)
                {
                    stream.Seek(offset, SeekOrigin.Begin);

                    chunk.Count = stream.Read(chunk.Bytes, 0, count);
                }
            }

            return chunk;
        }

        public void WriteBytes(string filename, int offset, byte[] bytes, int count)
        {
            using(var stream = File.OpenWrite(filename))
            {
                stream.Seek(offset, SeekOrigin.Begin);

                stream.Write(bytes, 0, count);
            }
        }
    }
}
