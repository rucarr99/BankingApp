using BankingApp.DAL.Abstractions;
using BankingApp.DataModel.Models;

namespace BankingApp.DAL
{
    public class AccountTypeRepository : RepositoryBase<AccountType>, IAccountTypeRepository
    {
        public AccountTypeRepository(BankContext bankContext) 
            : base(bankContext)
        {
        }
    }
}
