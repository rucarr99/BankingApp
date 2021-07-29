using System;
using BankingApp.DAL.Abstractions;
using BankingApp.DataModel.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Schema;
using Services.Exceptions;

namespace Services
{
    public class AccountService : BaseService
    {
        private readonly AccountTypeService _accountTypeService;
        public AccountService(IRepositoryWrapper repositoryWrapper) : base(repositoryWrapper)
        {
            _accountTypeService = new AccountTypeService(repositoryWrapper);
        }

        public ReadOnlyCollection<Account> GetAccounts()
        {
            return RepositoryWrapper.AccountRepository.FindAll().ToList().AsReadOnly();
        }

        public void AddAccount(Account account)
        {
            if (account != null)
            {
                RepositoryWrapper.AccountRepository.Create(account);
            }
            else
            {
                throw new Exception();
            }
            
        }

        public Account GetAccountByIban(string iban)
        {
            Expression<Func<Account, bool>> expression = a => a.Iban == iban;
            var customer = RepositoryWrapper.AccountRepository.FindByCondition(expression).FirstOrDefault();
            if (customer != null)
            {
                return customer;
            }

            throw new AccountNotFoundException("Account not found, please enter the IBan again");
        }

        public ReadOnlyCollection<Account> GetCustomerAccounts(Customer customer)
        {
            Expression<Func<Account, bool>> expression = a => a.ClientId == customer.Id;
            return RepositoryWrapper.AccountRepository.FindByCondition(expression).ToList().AsReadOnly();
        }

        public double CheckBalance(Account account)
        {
            if (account.Status)
            {
                return account.Balance;
            }

            throw new AccountClosedException("Account closed", account);
        }

        public void TransferMoney(Account fromAccount, Account toAccount, double amount)
        {
            if (fromAccount.Status)
            {
                fromAccount.Balance -= amount;
                toAccount.Balance += amount;
                UpdateAccountBalance(fromAccount);
                UpdateAccountBalance(toAccount);
                Save();
            }
            else
            {
                throw new AccountClosedException("Account closed", fromAccount);
            }
            //more logic that I don't know
        }
        public double Deposit(double amount, Account account)
        {
            if (account.Status)
            {
                var accountType = _accountTypeService.GetAccountTypeForAccount(account);
                account.Balance += (amount - (accountType.DepositFixedRate + 
                                              accountType.DepositPercentageRate / 100 * amount));
                UpdateAccountBalance(account);
                Save();
                return account.Balance;
            }

            throw new AccountClosedException("Account closed", account);
        }

        private void UpdateAccountBalance(Account account)
        {
            RepositoryWrapper.AccountRepository.Update(account);
        }

        public double Withdraw(Account account, double amount)
        {
            if (account.Status)
            {
                var accountType = _accountTypeService.GetAccountTypeForAccount(account);
                var totalAmount = amount + accountType.WithdrawalFixedRate +
                                  accountType.WithdrawalPercentageRate / 100 * amount;
                if (account.Balance > totalAmount)
                {
                    account.Balance -= totalAmount;
                    UpdateAccountBalance(account);
                    Save();
                    return account.Balance;
                }

                throw new InsufficientMoneyException("Not enough money");
            }

            throw new AccountClosedException("Account closed", account);
        }


    }
}
