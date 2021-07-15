using BankingApp.DAL.Abstractions;
using BankingApp.DataModel.Models;

namespace BankingApp.DAL
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly BankContext _bankContext;
        private ICustomerRepository _customerRepository;
        private IAccountRepository _accountRepository;
        private ITransactionRepository _transactionRepository;
        private IAccountTypeRepository _accountTypeRepository;

        public RepositoryWrapper(BankContext bankContext)
        {
            _bankContext = bankContext;
        }

        public IAccountRepository AccountRepository =>
            _accountRepository ??= new AccountRepository(_bankContext);

        public ICustomerRepository CustomerRepository =>
            _customerRepository ??= new CustomerRepository(_bankContext);

        public ITransactionRepository TransactionRepository =>
            _transactionRepository ??= new TransactionRepository(_bankContext);

        public IAccountTypeRepository AccountTypeRepository =>
            _accountTypeRepository ??= new AccountTypeRepository(_bankContext);

        public void Save()
        {
            _bankContext.SaveChanges();
        }
    }
}
