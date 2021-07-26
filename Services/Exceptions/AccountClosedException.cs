using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingApp.DataModel.Models;

namespace Services.Exceptions
{
    public class AccountClosedException : Exception
    {
        public AccountClosedException(string message, Account account) : base(message)
        {

        }
    }
}
