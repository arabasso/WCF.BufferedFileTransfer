using NUnit.Framework;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace WCF.BufferedFileTransfer.Test
{
    [TestFixture]
    public class BufferedTransferServiceTest
    {
        Client.TransferClient _transferClient;
        Service.IBufferedTransferService _transferService;
        byte [] _bytes;
        string _tempFile;

        [SetUp]
        public void SetUp()
        {
            _transferService = _transferClient = new Client.TransferClient();

            _transferClient.Open();

            _tempFile = Path.GetTempFileName();

            File.Copy("TextFile.txt", _tempFile, true);
            _bytes = File.ReadAllBytes("TextFile.txt");
        }

        [TearDown]
        public void TearDown()
        {
            _transferClient.Close();

            File.Delete(_tempFile);
        }

        [TestCase(0, 3, ExpectedResult = 3)]
        [TestCase(4, 14, ExpectedResult = 14)]
        [TestCase(50, 10, ExpectedResult = 6)]
        public int CountReadBytes(int offset, int count)
        {
            var chunk = _transferService.ReadBytes(_tempFile, offset, count);

            return chunk.Count;
        }

        [TestCase(0, 3, ExpectedResult = "WCF")]
        [TestCase(4, 14, ExpectedResult = "implementation")]
        [TestCase(50, 10, ExpectedResult = "tions.")]
        public string ReadBytes(int offset, int count)
        {
            var chunk = _transferService.ReadBytes(_tempFile, offset, count);

            return Encoding.UTF8.GetString(chunk.Bytes.Take(chunk.Count).ToArray());
        }

        [TestCase("WCF")]
        [TestCase("WCF implementation")]
        public void WriteBytes(string text)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            var tempFile = Path.GetTempFileName();

            _transferService.WriteBytes(tempFile, 0, bytes, bytes.Length);

            Assert.That(File.ReadAllBytes(tempFile), Is.EqualTo(bytes));

            File.Delete(tempFile);
        }
    }
}
