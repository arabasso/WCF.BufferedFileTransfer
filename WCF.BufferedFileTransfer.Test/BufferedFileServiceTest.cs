using NUnit.Framework;
using System.IO;

namespace WCF.BufferedFileTransfer.Test
{
    [TestFixture]
    public class BufferedFileServiceTest
    {
        private const string TextFile = "TextFile.txt";
        string _tempFile;
        Client.TransferClient _transferClient;
        Service.IFileService _fileService;

        [SetUp]
        public void SetUp()
        {
            _transferClient = new Client.TransferClient();

            _transferClient.Open();

            _fileService = new Service.BufferedFileService(_transferClient);

            _tempFile = Path.GetTempFileName();
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(_tempFile);

            _transferClient.Close();
        }

        [Test]
        public void UploadFile()
        {
            _fileService.Upload(File.OpenRead(TextFile), _tempFile);

            Assert.That(File.ReadAllBytes(_tempFile), Is.EqualTo(File.ReadAllBytes(TextFile)));
        }

        [Test]
        public void DownloadFile()
        {
            using (var stream = _fileService.Download(_tempFile))
            {
                var bytes = new byte[stream.Length];

                stream.Write(bytes, 0, bytes.Length);

                Assert.That(bytes, Is.EqualTo(File.ReadAllBytes(_tempFile)));
            }
        }
    }
}
