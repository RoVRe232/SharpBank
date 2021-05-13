using BankAPI.Entities;
using BankAPI.Repositories.Interfaces;
using BankAPI.Services;
using BankAPI.Utilities;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpBank.Models;
using SharpBank.Utils;

namespace AppLogic.Tests
{
    [TestClass]
    public class EncryptorDecryptorTest
    {
        private string initial;
        private string encodedBase64;
        private string decoded;
        private const string key = "bce2ea2315a1916b14ca5898a4e4133b";

        [TestInitialize]
        public void InitializeTestData()
        {
            initial = "asdfghjhZZZ12344_@qewrttZ";
            encodedBase64 = "";
            decoded = "";
        }

        [TestMethod]
        public void CodecTest()
        {
            encodedBase64 = Codec.EncryptString(key, initial);
            decoded = Codec.DecryptString(key, encodedBase64);

            Assert.AreEqual(initial, decoded);
        }
    }
}

