using System;
using System.Collections.Generic;

#nullable disable

namespace BankingApp.DataModel.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Accounts = new HashSet<Account>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cnp { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Pin { get; set; }
        public bool IsAdmin { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
