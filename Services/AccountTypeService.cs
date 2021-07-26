
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BankingApp.DAL.Abstractions;
using BankingApp.DataModel.Models;

namespace Services
{
    public class AccountTypeService : BaseService
    {
        public AccountTypeService(IRepositoryWrapper repositoryWrapper) : base(repositoryWrapper)
        {
        }

        public AccountType GetAccountTypeForAccount(Account account)
        {
            Expression<Func<AccountType, bool>> expression = ac => ac.Id == account.Type;
            return RepositoryWrapper.AccountTypeRepository.FindByCondition(expression).FirstOrDefault();
        }
    }
}
