using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BankingApp.DAL.Abstractions;
using BankingApp.DataModel.Enums;
using BankingApp.DataModel.Models;

namespace Services
{
    public class TransactionService : BaseService
    {
        public TransactionService(IRepositoryWrapper repositoryWrapper) : base(repositoryWrapper)
        {
        }

        public IReadOnlyCollection<Transaction> ViewTransactionsFor(Account account)
        {
            Expression<Func<Transaction, bool>> expression = t => t.IdAccount == account.Id;
            return RepositoryWrapper.TransactionRepository.FindByCondition(expression).ToList().AsReadOnly();
        }

        public IReadOnlyCollection<Transaction> ViewTransactionsFor(Customer customer)
        {
            var transactions = RepositoryWrapper.CustomerRepository.FindAll().Join(
                RepositoryWrapper.AccountRepository.FindAll()
                , c => customer.Id, a => a.ClientId, (c, a) => new { c, a })
                .Join(RepositoryWrapper.TransactionRepository.FindAll(), a => a.a.Id, transaction => transaction.IdAccount,  (a, transaction) => new {transaction}).Distinct().ToList();

            var list = transactions.Select(i => i.transaction).ToList().AsReadOnly();


            return list;
        }

        private void RecordTransaction(Transaction newTransaction)
        {
            RepositoryWrapper.TransactionRepository.Create(newTransaction);
            Save();
        }

        public void RecordNewDeposit(Account account, double amount)
        {
            var deposit = new Transaction()
            {
                Date = DateTime.Now,
                Description = $"Deposit {amount} {account.Currency}",
                TransactionType = TransactionTypes.Deposit,
                IdAccount = account.Id
            };

            RecordTransaction(deposit);
        }

        public void RecordNewTransfer(Account fromAccount, Account toAccount, double amount)
        {
            var transfer = new Transaction()
            {
                Date = DateTime.Now,
                Description = $"Transfer {amount} from {fromAccount.Iban} to {toAccount.Iban}",
                TransactionType = TransactionTypes.Transfer,
                IdAccount = fromAccount.Id,
                DestinationAccount = toAccount.Iban
            };
            RecordTransaction(transfer);
        }

        public void RecordNewWithdraw(Account account, double amount)
        {
            var withdraw = new Transaction()
            {
                Date = DateTime.Now,
                Description = $"Withdraw {amount} {account.Currency}",
                TransactionType = TransactionTypes.Withdrawal,
                IdAccount = account.Id
            };

            RecordTransaction(withdraw);
        }



    }
}
