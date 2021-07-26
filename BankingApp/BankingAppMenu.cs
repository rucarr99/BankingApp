using System;
using System.Collections.Generic;
using BankingApp.DAL;
using BankingApp.DAL.Abstractions;
using BankingApp.DataModel.Enums;
using BankingApp.DataModel.Models;
using ConsoleMenuComponent;
using ConsoleMenuComponent.Abstractions;
using Services;
using Services.Exceptions;
using static BankingApp.Helpers;


namespace BankingApp
{
    //de refactorizat in mai multe clase
    public class BankingAppMenu
    {
        private ConsoleMenu _consoleMenu;
        private static readonly IRepositoryWrapper RepositoryWrapper = new RepositoryWrapper(new BankContext());
        private readonly CustomerService _customerService = new(RepositoryWrapper);
        private readonly AccountService _accountService = new(RepositoryWrapper);
        private readonly TransactionService _transactionService = new(RepositoryWrapper);

        private List<BaseMenuItem> AccountList(Customer customer)
        {
            var accountList = _customerService.GetAccounts(customer);
            var items = new List<BaseMenuItem>();
            foreach (var account in accountList)
            {
                var currentItem = new ConsoleMenu(account.Id, account.Iban, UserMenu(account));
                items.Add(currentItem);
            }

            return items;
        }

        private List<BaseMenuItem> UserMenu(Account account)
        {
            var userMenuItems = new List<BaseMenuItem>
            {
                new ConsoleMenuItem(1, "Check Balance", _ => CheckBalance(account)),
                new ConsoleMenuItem(2, "Withdraw", _ => Withdraw(account)),
                new ConsoleMenuItem(3, "Deposit", _ => Deposit(account)),
                new ConsoleMenuItem(4, "Change pin", _ => ChangePin(account))
            };

            return userMenuItems;
        }

        private List<BaseMenuItem> AdminMenu()
        {
            var adminMenuItems = new List<BaseMenuItem>
            {
                new ConsoleMenuItem(1, "Open new account", _ => OpenNewAccount()),
                new ConsoleMenuItem(2, "Show customers", _ => ShowCustomers()),
                new ConsoleMenuItem(3, "Close customer's account", _ => CloseCustomerAccount()),
                new ConsoleMenuItem(4, "Transfer money", _ => TransferMoney()),
                new ConsoleMenuItem(5, "Generate report", _ => GenerateReport())
            };

            return adminMenuItems;
        }

        private void GenerateReport()
        {
            
        }

        private void TransferMoney()
        {
            
        }

        private void CloseCustomerAccount()
        {
            
        }

        private void ShowCustomers()
        {
            
        }

        private void OpenNewAccount()
        {
            
        }

        public void Run()
        {
            _consoleMenu?.Execute(this);
            Console.WriteLine("Closing application ...");
        }

        private void ChangePin(Account account)
        {
            Console.Clear();
            var customer =_customerService.GetCustomerByAccount(account);   
            Console.WriteLine("Please enter the old PIN");
            var oldPin = ReadChoice();
            if (oldPin.Equals(customer.Pin))
            {
                Console.WriteLine("Please enter the new PIN");
                var newPin = ReadChoice();
                _customerService.ChangePin(customer, newPin);
            }
            else
            {
                Console.WriteLine("Incorrect pin, please press any key");
                Console.ReadLine();
            }
        }

        private void Deposit(Account account)
        {   
            Console.Clear();
            Console.WriteLine("Please enter the amount you want to deposit");
            var amount = ReadInput();
            try
            {
                _accountService.Deposit(amount, account);
            }
            catch (AccountClosedException exception)
            {
                Console.WriteLine(exception.Message);
            }

            var newTransaction = new Transaction()
            {
                IdAccount = account.Id,
                TransactionType = TransactionTypes.Deposit,
                Description = $"Deposit {amount} {account.Currency}",
                Date = DateTime.Now,
                DestinationAccount = null
            };
            _transactionService.RecordTransaction(newTransaction);
        }

        private void Withdraw(Account account)
        {
            Console.Clear();
            Console.WriteLine("Enter the amount you want to withdraw");
            var amount = ReadInput();
            try
            {
                _accountService.Withdraw(account, amount);
            }
            catch (InsufficientMoneyException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (AccountClosedException exception)
            {
                Console.WriteLine(exception.Message);
            }

            var newTransaction = new Transaction()
            {
                IdAccount = account.Id,
                TransactionType = TransactionTypes.Withdrawal,
                Description = $"Withdraw {amount} {account.Currency}",
                Date = DateTime.Now
            };
            _transactionService.RecordTransaction(newTransaction);
        }

        private void CheckBalance(Account account)
        {
            Console.Clear();
            Console.WriteLine($"Your balance is: {_accountService.CheckBalance(account)}");
            Console.ReadKey();
        }

        public void LogInScreen()
        {
            Console.WriteLine("Please enter your CNP:");
            var cnp = Console.ReadLine();
            Console.WriteLine("Please enter your pin");
            var pin = Console.ReadLine();
            try
            {
                var customer = _customerService.LogIn(cnp, pin);
                _consoleMenu = customer.IsAdmin ? new ConsoleMenu(AdminMenu()) : new ConsoleMenu(AccountList(customer));
            }
            catch (UserNotFoundException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }




    }
}
