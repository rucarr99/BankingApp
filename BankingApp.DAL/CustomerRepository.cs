using BankingApp.DAL.Abstractions;
using BankingApp.DataModel.Models;

namespace BankingApp.DAL
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(BankContext bankContext) 
            : base(bankContext)
        {
        }
    }
}
