using BankingApp.DAL.Abstractions;
using BankingApp.DataModel.Models;

namespace BankingApp.DAL
{
    public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
    {
        public TransactionRepository(BankContext bankContext) 
            : base(bankContext)
        {
        }
    }
}
