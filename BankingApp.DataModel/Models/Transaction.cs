using System;
using System.Collections.Generic;
using BankingApp.DataModel.Enums;

#nullable disable

namespace BankingApp.DataModel.Models
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public int IdAccount { get; set; }
        public TransactionTypes TransactionType { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }
        public string DestinationAccount { get; set; }

        public virtual Account IdAccountNavigation { get; set; }
    }
}
