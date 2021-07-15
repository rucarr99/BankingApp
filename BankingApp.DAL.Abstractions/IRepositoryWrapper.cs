namespace BankingApp.DAL.Abstractions
{
    public interface IRepositoryWrapper
    {
        IAccountRepository AccountRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        ITransactionRepository TransactionRepository { get; }
        IAccountTypeRepository AccountTypeRepository { get; }

        void Save();
    }
}
