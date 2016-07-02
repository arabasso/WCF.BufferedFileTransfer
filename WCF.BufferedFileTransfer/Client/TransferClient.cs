using System.ServiceModel;

namespace WCF.BufferedFileTransfer.Client
{
    public class TransferClient : ClientBase<Service.IBufferedTransferService>, Service.IBufferedTransferService
    {
        public TransferClient()
        {
        }

        public TransferClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }

        public TransferClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }

        public TransferClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }

        public TransferClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }

        public Service.Chunk ReadBytes(string filename, int offset, int bufferSize)
        {
            return Channel.ReadBytes(filename, offset, bufferSize);
        }

        public void WriteBytes(string filename, int offset, byte[] bytes, int count)
        {
            Channel.WriteBytes(filename, offset, bytes, count);
        }
    }
}
