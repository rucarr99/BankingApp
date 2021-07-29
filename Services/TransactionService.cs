using System;
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

        public IQueryable ViewTransactionsFor(Account account)
        {
            Expression<Func<Transaction, bool>> expression = t => t.IdAccount == account.Id;
            return RepositoryWrapper.TransactionRepository.FindByCondition(expression);
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
