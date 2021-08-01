using System.Collections.Generic;
using System.Linq;
using BankingApp.DataModel.Enums;
using BankingApp.DataModel.Models;

namespace Services.Searcher
{
    public class SearchEngine : ISearchStrategy<Transaction>
    {
        private readonly ICollection<Transaction> _transactions;
        public SearchEngine(ICollection<Transaction> transactions)
        {
            _transactions = transactions;
        }
        public IReadOnlyCollection<Transaction> Search(TransactionTypes type)
        {
            return _transactions.Where(transaction => transaction.TransactionType == type).ToList().AsReadOnly();
        }
    }
}
