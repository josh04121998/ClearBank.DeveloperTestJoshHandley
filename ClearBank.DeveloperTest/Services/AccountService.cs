using ClearBank.DeveloperTest.Data.Interfaces;
using ClearBank.DeveloperTest.Services.Interfaces;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountDataStore _accountDataStore;

        public AccountService(IAccountDataStoreFactory accountDataStoreFactory)
        {
            _accountDataStore = accountDataStoreFactory.GetInstance();
        }

        public Account GetAccount(string accountNumber)
        {
            return _accountDataStore.GetAccount(accountNumber);
        }

        public void UpdateAccount(Account account, MakePaymentRequest request)
        {
            account.Balance -= request.Amount;

            _accountDataStore.UpdateAccount(account);
        }
    }
}