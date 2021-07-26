using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BankingApp.DAL.Abstractions;
using BankingApp.DataModel.Models;

namespace Services
{
    public class TransactionService : BaseService
    {
        public TransactionService(IRepositoryWrapper repositoryWrapper) : base(repositoryWrapper)
        {
        }

        public IQueryable ViewTransactionsFor(Account account)
        {
            Expression<Func<Transaction, bool>> expression = t => t.IdAccount == account.Id;
            return RepositoryWrapper.TransactionRepository.FindByCondition(expression);
        }

        public void RecordTransaction(Transaction newTransaction)
        {
            RepositoryWrapper.TransactionRepository.Create(newTransaction);
            Save();
        }



    }
}
