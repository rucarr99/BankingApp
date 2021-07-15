using BankingApp.DAL.Abstractions;
using BankingApp.DataModel.Models;

namespace BankingApp.DAL
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(BankContext bankContext)
            : base(bankContext)
        {
        }
    }
}