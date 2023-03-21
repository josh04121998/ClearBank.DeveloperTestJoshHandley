using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using FluentAssertions;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Validators
{
    [TestFixture]
    public class BacsValidatorTests
    {
        private BacsValidator _bacsValidator;

        [SetUp]
        public void SetUp()
        {
            _bacsValidator = new BacsValidator();
        }

        [Test]
        public void IsValid_Should_Return_True_When_Account_Is_Allowed_In_Payment_Scheme_Bacs()
        {
            var result = _bacsValidator.IsValid(new MakePaymentRequest(), new Account { AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs });

            result.Should().BeTrue();
        }

        [Test]
        public void IsValid_Should_Return_False_When_Account_Is_Not_Allowed_In_Payment_Scheme_Bacs()
        {
            var result = _bacsValidator.IsValid(new MakePaymentRequest(), new Account { AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments });

            result.Should().BeFalse();
        }

        [Test]
        public void IsValid_Should_Return_False_When_Account_Is_Null()
        {
            var result = _bacsValidator.IsValid(new MakePaymentRequest(), null);

            result.Should().BeFalse();
        }
    }
}