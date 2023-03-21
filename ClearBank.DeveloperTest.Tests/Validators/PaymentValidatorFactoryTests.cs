using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using NUnit.Framework;
using FluentAssertions;

namespace ClearBank.DeveloperTest.Tests.Validators
{
    [TestFixture]
    public class PaymentValidatorFactoryTests
    {
        private PaymentValidatorFactory _paymentValidatorFactory;

        [SetUp]
        public void SetUp()
        {
            _paymentValidatorFactory = new PaymentValidatorFactory();
        }

        [Test]
        public void GetInstance_Should_Return_BacsValidator()
        {
            var result = _paymentValidatorFactory.GetPaymentValidator(new MakePaymentRequest { PaymentScheme = PaymentScheme.Bacs });

            result.GetType().Should().Be(typeof(BacsValidator));
        }

        [Test]
        public void GetInstance_Should_Return_FasterPaymentsValidator()
        {
            var result = _paymentValidatorFactory.GetPaymentValidator(new MakePaymentRequest { PaymentScheme = PaymentScheme.FasterPayments });

            result.GetType().Should().Be(typeof(FasterPaymentsValidator));
        }

        [Test]
        public void GetInstance_Should_Return_ChapsValidator()
        {
            var result = _paymentValidatorFactory.GetPaymentValidator(new MakePaymentRequest { PaymentScheme = PaymentScheme.Chaps });

            result.GetType().Should().Be(typeof(ChapsValidator));
        }

        [Test]
        public void GetInstance_Should_Return_DefaultValidator()
        {
            var result = _paymentValidatorFactory.GetPaymentValidator(new MakePaymentRequest());

            result.GetType().Should().Be(typeof(DefaultValidator));
        }
    }
}