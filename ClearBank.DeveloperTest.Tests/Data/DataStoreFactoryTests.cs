using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Services.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Data
{
    [TestFixture]
    public class PaymentValidatorFactoryTests
    {
        private AccountDataStoreFactory _accountDataStoreFactory;
        private Mock<IConfigSettings> _configSettings;

        [SetUp]
        public void SetUp()
        {
            _configSettings = new Mock<IConfigSettings>();
            _accountDataStoreFactory = new AccountDataStoreFactory(_configSettings.Object);
        }

        [Test]
        public void GetInstance_Should_Return_AccountDataStore()
        {
            _configSettings.Setup(x => x.DataStoreType).Returns("AccountDataStore");

            var result = _accountDataStoreFactory.GetInstance();

            result.GetType().Should().Be(typeof(AccountDataStore));
        }

        [Test]
        public void GetInstance_Should_Return_BackupAccountDataStore()
        {
            _configSettings.Setup(x => x.DataStoreType).Returns(Constants.DataStoreType.Backup);

            var result = _accountDataStoreFactory.GetInstance();

            result.GetType().Should().Be(typeof(BackupAccountDataStore));
        }
    }
}