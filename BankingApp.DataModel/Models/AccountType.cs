using System;
using System.Collections.Generic;
using BankingApp.DataModel.Enums;

#nullable disable

namespace BankingApp.DataModel.Models
{
    public partial class AccountType
    {
        public AccountType()
        {
            Accounts = new HashSet<Account>();
        }

        public AccountTypes Id { get; set; }
        public string Name { get; set; }
        public double WithdrawalPercentageRate { get; set; }
        public double DepositPercentageRate { get; set; }
        public double WithdrawalFixedRate { get; set; }
        public double DepositFixedRate { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
