using NUnit.Framework;
using System;
using System.IO;
using System.ServiceModel;

namespace WCF.BufferedFileTransfer.Test
{
    [SetUpFixture]
    public class ServiceHostSetup
    {
        ServiceHost _serviceHost;

        [OneTimeSetUp]
        public void SetUp()
        {
            var dir = Path.GetDirectoryName(GetType().Assembly.Location);

            Directory.SetCurrentDirectory(dir);

            _serviceHost = new ServiceHost(typeof(Service.BufferedTransferService));

            _serviceHost.Open();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _serviceHost.Close();
        }
    }
}
