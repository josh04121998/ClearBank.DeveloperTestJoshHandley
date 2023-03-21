using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Services.Interfaces;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Services
{
    [TestFixture]
    public class PaymentServiceTests
    {
        private Mock<IPaymentValidatorFactory> _paymentValidatorFactoryMock;
        private Mock<IPaymentValidator> _paymentValidatorMock;
        private Mock<IAccountService> _accountServiceMock;
        private PaymentService _paymentService;

        [SetUp]
        public void SetUp()
        {
            _accountServiceMock = new Mock<IAccountService>();
            _paymentValidatorMock = new Mock<IPaymentValidator>();

            _paymentValidatorFactoryMock = new Mock<IPaymentValidatorFactory>();

            _paymentService = new PaymentService(_accountServiceMock.Object, _paymentValidatorFactoryMock.Object);
        }

        [Test]
        public void MakePayment_Should_Return_Response_False()
        {
            _paymentValidatorMock
                .Setup(validator => validator.IsValid(It.IsAny<MakePaymentRequest>(), It.IsAny<Account>()))
                .Returns(false);

            _paymentValidatorFactoryMock.Setup(factory => factory.GetPaymentValidator(It.IsAny<MakePaymentRequest>()))
                .Returns(_paymentValidatorMock.Object);

            var result = _paymentService.MakePayment(new MakePaymentRequest());

            result.Success.Should().BeFalse();

            _accountServiceMock.Verify(store => store.UpdateAccount(It.IsAny<Account>(), It.IsAny<MakePaymentRequest>()), Times.Never);
        }

        [Test]
        public void MakePayment_Should_Return_Response_True()
        {
            _paymentValidatorMock
                .Setup(validator => validator.IsValid(It.IsAny<MakePaymentRequest>(), It.IsAny<Account>()))
                .Returns(true);

            _paymentValidatorFactoryMock.Setup(factory => factory.GetPaymentValidator(It.IsAny<MakePaymentRequest>()))
                .Returns(_paymentValidatorMock.Object);        

            var result = _paymentService.MakePayment(new MakePaymentRequest());

            result.Success.Should().BeTrue();

            _accountServiceMock.Verify(store => store.UpdateAccount(It.IsAny<Account>(), It.IsAny<MakePaymentRequest>()), Times.Once);
        }
    }
}