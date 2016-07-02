using System.Runtime.Serialization;

namespace WCF.BufferedFileTransfer.Service
{
    [DataContract]
    public class Chunk
    {
        [DataMember]
        public int Count
        {
            get;
            set;
        }

        [DataMember]
        public byte[] Bytes
        {
            get;
            set;
        }

        public Chunk()
        {
        }

        public Chunk(int bufferSize)
        {
            Bytes = new byte[bufferSize];
        }
    }
}