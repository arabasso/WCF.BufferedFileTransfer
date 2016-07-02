using System.ServiceModel;

namespace WCF.BufferedFileTransfer.Service
{
    [ServiceContract]
    public interface IBufferedTransferService
    {
        [OperationContract]
        Chunk ReadBytes(string filename, int offset, int count);
        [OperationContract]
        void WriteBytes(string filename, int offset, byte[] bytes, int count);
    }
}