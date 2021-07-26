using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BankingApp.DAL.Abstractions;
using BankingApp.DataModel.Models;
using Services.Exceptions;

namespace Services
{
    public class CustomerService : BaseService
    {
        public CustomerService(IRepositoryWrapper repositoryWrapper) : base(repositoryWrapper)
        {
        }

        public Customer LogIn(string cnp, string pin)
        {
            if (cnp == null) throw new ArgumentNullException(nameof(cnp));
            if (pin == null) throw new ArgumentNullException(nameof(pin));
            Expression<Func<Customer, bool>> ex = c => c.Cnp == cnp && c.Pin == pin;
            var customer = RepositoryWrapper.CustomerRepository.FindByCondition(ex).FirstOrDefault();
            if (customer != null)
            {
                return customer;
            }

            throw new UserNotFoundException("CNP or PIN incorrect.");
        }

        public IQueryable<Account> GetAccounts(Customer customer)
        {
            Expression<Func<Account, bool>> expression = account => account.Client == customer;
            return RepositoryWrapper.AccountRepository.FindByCondition(expression);
        }

        public void ChangePin(Customer customer, string newPin)
        {
            customer.Pin = newPin;
            RepositoryWrapper.CustomerRepository.Update(customer);
            Save();
        }

        public Customer GetCustomerByAccount(Account account)
        {
            Expression<Func<Customer, bool>> expression = c => c.Id == account.ClientId;
            return RepositoryWrapper.CustomerRepository.FindByCondition(expression).FirstOrDefault();
        }
    }
}
