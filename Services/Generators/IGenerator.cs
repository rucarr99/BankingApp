using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingApp.DataModel.Models;

namespace Services.Generators
{
    public interface IGenerator
    {
        void Generate(IList<Transaction> transactions, Account account);
    }
}
