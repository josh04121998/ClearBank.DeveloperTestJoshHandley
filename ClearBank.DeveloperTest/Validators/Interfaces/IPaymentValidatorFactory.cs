using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators.Interfaces;
public interface IPaymentValidatorFactory
{
    IPaymentValidator GetPaymentValidator(MakePaymentRequest request);
}