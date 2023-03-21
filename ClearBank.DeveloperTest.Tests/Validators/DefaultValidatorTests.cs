using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using FluentAssertions;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.Validators
{
    [TestFixture]
    public class DefaultValidatorTests
    {
        private DefaultValidator _defaultValidator;

        [SetUp]
        public void SetUp()
        {
            _defaultValidator = new DefaultValidator();
        }

        [Test]
        public void IsValid_Should_Return_False()
        {
            var result = _defaultValidator.IsValid(
                new MakePaymentRequest(),
                new Account
                {
                    AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                    Status = AccountStatus.Live
                });

            result.Should().BeFalse();
        }
    }
    
}