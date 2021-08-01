using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingApp.DataModel.Enums;
using BankingApp.DataModel.Models;

namespace Services.Searcher
{
    public interface ISearchStrategy<T>
    {
        IReadOnlyCollection<Transaction> Search(TransactionTypes type);
    }
}
