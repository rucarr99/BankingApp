using System;
using System.Collections.Generic;
using ConsoleMenuComponent;
using ConsoleMenuComponent.Abstractions;

namespace BankingApp
{
    public class BankingAppMenu
    {
        private ConsoleMenu _consoleMenu;

        private List<BaseMenuItem> UserMenu()
        {
            var userMenuItems = new List<BaseMenuItem>
            {
                new ConsoleMenuItem(1, "Check Balance", _ => CheckBalance()),
                new ConsoleMenuItem(2, "Withdraw", _ => Withdraw()),
                new ConsoleMenuItem(3, "Deposit", _ => Deposit()),
                new ConsoleMenuItem(4, "Change pin", _ => ChangePin())
            };

            return userMenuItems;
        }

        private List<BaseMenuItem> AdminMenu()
        {
            var adminMenuItems = new List<BaseMenuItem>
            {
                new ConsoleMenuItem(1, "Open new account", _ => CheckBalance()),
                new ConsoleMenuItem(2, "Show customers", _ => Withdraw()),
                new ConsoleMenuItem(3, "Close customer's account", _ => Deposit()),
                new ConsoleMenuItem(4, "Transfer money", _ => ChangePin()),
                new ConsoleMenuItem(5, "Generate report", _ => ChangePin())
            };

            return adminMenuItems;
        }

        public void Run()
        {
            _consoleMenu?.Execute(this);
            Console.WriteLine("Closing application ...");
        }

        private void ChangePin()
        {
            
        }

        private void Deposit()
        {
            
        }

        private void Withdraw()
        {
            
        }

        private void CheckBalance()
        {
            
        }

        public void LoginScreen()
        {
            var mainMenuItems = new List<BaseMenuItem>
            {
                new ConsoleMenu(1, "User Interface", UserMenu()),
                new ConsoleMenu(2, "AdminInterface", AdminMenu())

            };

            _consoleMenu = new ConsoleMenu(mainMenuItems);
        }


    }
}
