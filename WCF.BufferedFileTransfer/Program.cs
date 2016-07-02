using System;
using System.IO;
using System.ServiceModel;

namespace WCF.BufferedFileTransfer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1) return;

            if (args[0] == "-s")
            {
                RunService(args);

                return;
            }

            RunClient(args);
        }

        private static void RunClient(string[] args)
        {
            if (args.Length < 3) return;

            var transferClient = args.Length == 4
                ? new Client.TransferClient(new BasicHttpBinding(), new EndpointAddress($"http://{args[3]}/FileTransferService"))
                : new Client.TransferClient();

            transferClient.Open();

            Service.IFileService fileService = new Service.BufferedFileService(transferClient);

            switch (args[0])
            {
                case "-d":
                    using (var inputStream = fileService.Download(args[1]))
                    using (var outputStream = File.OpenWrite(args[2]))
                    {
                        inputStream.CopyTo(outputStream);
                    }
                    break;

                case "-u":
                    using (var inputStream = File.OpenRead(args[1]))
                    {
                        fileService.Upload(inputStream, args[2]);
                    }
                    break;
            }

            transferClient.Close();
        }

        private static void RunService(string[] args)
        {
            var serviceHost = args.Length == 2
                                ? new ServiceHost(typeof(Service.BufferedTransferService), new Uri($"http://{args[1]}/FileTransferService"))
                                : new ServiceHost(typeof(Service.BufferedTransferService));

            serviceHost.Open();

            var addresses = string.Join(", ", serviceHost.BaseAddresses);

            Console.WriteLine();
            Console.WriteLine($"Service running on {addresses}");

            Console.WriteLine("Press any key to exit. . .");
            Console.ReadKey();

            serviceHost.Close();
        }
    }
}
