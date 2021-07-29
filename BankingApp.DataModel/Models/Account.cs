using System;
using System.Collections.Generic;
using BankingApp.DataModel.Enums;

#nullable disable

namespace BankingApp.DataModel.Models
{
    public partial class Account
    {
        public Account()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public string Iban { get; set; }
        public int Type { get; set; }
        public Currency Currency { get; set; }
        public int ClientId { get; set; }
        public double Balance { get; set; }
        public bool Status { get; set; }

        public virtual Customer Client { get; set; }
        public virtual AccountType TypeNavigation { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
