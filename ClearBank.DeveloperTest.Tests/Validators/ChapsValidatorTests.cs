using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using FluentAssertions;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Validators
{
    [TestFixture]
    public class ChapsValidatorTests
    {
        private ChapsValidator _chapsValidator;

        [SetUp]
        public void SetUp()
        {
            _chapsValidator = new ChapsValidator();
        }

        [Test]
        public void IsValid_Should_Return_True_When_Account_Is_In_State_Live()
        {
            var result = _chapsValidator.IsValid(
                new MakePaymentRequest(),
                new Account
                {
                    AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                    Status = AccountStatus.Live
                });

            result.Should().BeTrue();
        }

        [Test]
        public void IsValid_Should_Return_False_When_Account_Is_Not_Allowed_In_Payment_Scheme_Chaps()
        {
            var result = _chapsValidator.IsValid(
                 new MakePaymentRequest(),
                 new Account
                 {
                     AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs,
                     Status = AccountStatus.Live
                 });

            result.Should().BeFalse();
        }

        [Test]
        public void IsValid_Should_Return_False_When_Account_Is_In_State_Disabled()
        {
            var result = _chapsValidator.IsValid(
                new MakePaymentRequest(),
                new Account
                {
                    AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                    Status = AccountStatus.Disabled
                });

            result.Should().BeFalse();
        }

        [Test]
        public void IsValid_Should_Return_False_When_Account_Is_In_State_Live()
        {
            var result = _chapsValidator.IsValid(
                new MakePaymentRequest(),
                new Account
                {
                    AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                    Status = AccountStatus.InboundPaymentsOnly
                });

            result.Should().BeFalse();
        }

        [Test]
        public void IsValid_Should_Return_False_When_Account_Is_Null()
        {
            var result = _chapsValidator.IsValid(new MakePaymentRequest(), null);

            result.Should().BeFalse();
        }
    }
}