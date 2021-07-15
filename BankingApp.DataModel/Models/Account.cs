using System;
using System.Collections.Generic;

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
        public int Type { get; set; }
        public int Currency { get; set; }
        public int ClientId { get; set; }
        public int Balance { get; set; }
        public bool Status { get; set; }

        public virtual Customer Client { get; set; }
        public virtual AccountType TypeNavigation { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
