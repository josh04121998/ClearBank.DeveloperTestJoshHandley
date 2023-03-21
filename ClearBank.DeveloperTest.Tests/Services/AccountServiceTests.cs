using ClearBank.DeveloperTest.Data.Interfaces;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Services
{
    [TestFixture]
    public class AccountServiceTests
    {
        private Mock<IAccountDataStoreFactory> _accountDataStoreFactoryMock;
        private Mock<IAccountDataStore> _accountDataStoreMock;
        private AccountService _accountService;

        [SetUp]
        public void SetUp()
        {
            _accountDataStoreMock = new Mock<IAccountDataStore>();
            _accountDataStoreFactoryMock = new Mock<IAccountDataStoreFactory>();
        }

        [Test]
        public void GetAccount_Should_Return_Valid_Response()
        {
            var account = new Account { AccountNumber = "123", Balance = 20 };
            _accountDataStoreMock.Setup(store => store.GetAccount(It.IsAny<string>())).Returns(account);
            _accountDataStoreFactoryMock.Setup(factory => factory.GetInstance()).Returns(_accountDataStoreMock.Object);
            _accountService = new AccountService(_accountDataStoreFactoryMock.Object);

            var result = _accountService.GetAccount("123");

            result.AccountNumber.Should().Be(account.AccountNumber);
        }

        [Test]
        public void UpdateAccount_Should_Return_Valid_Response()
        {
            _accountDataStoreFactoryMock.Setup(factory => factory.GetInstance()).Returns(_accountDataStoreMock.Object);
            _accountService = new AccountService(_accountDataStoreFactoryMock.Object);

            _accountService.UpdateAccount(new Account { AccountNumber = "123", Balance = 30 }, new MakePaymentRequest { Amount = 20 });

            _accountDataStoreMock.Verify(store => store.UpdateAccount(It.Is<Account>(account => account.Balance == 10)), Times.Once);
        }
    }
}