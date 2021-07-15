using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleMenuComponent.Abstractions;

namespace ConsoleMenuComponent
{
    public class ConsoleMenu : BaseMenuItem
    {
        private readonly List<BaseMenuItem> _menuItems = new();
        private bool _continueExecution;

        public ConsoleMenu(int shortcut, string text, List<BaseMenuItem> menuItems) : base(shortcut, text)
        {
            _menuItems.Add(new ConsoleMenuItem(0,
                    "Exit",
                    _ => { _continueExecution = false; }
                )
            );
            _menuItems.AddRange(menuItems);
        }
        public ConsoleMenu(List<BaseMenuItem> menuItems) : this(0, "", menuItems)
        {
        }

        private void DisplayMenu()
        {
            Console.Clear();
            foreach (var menuItem in _menuItems)
            {
                Console.WriteLine($"{menuItem.Shortcut}. {menuItem.Text}");
            }
            Console.WriteLine("Please enter your option");
        }

        private int ReadCurrentOption()
        {
            var currentKey = Console.ReadKey();
            var keyCode = currentKey.KeyChar - '0';
            return keyCode;
        }

        private void DisplayInvalidOption()
        {
            Console.WriteLine("Invalid option. Please select a value in the menu");
        }

        public override void Execute(object parentObject)
        {
            _continueExecution = true;
            while (_continueExecution)
            {
                DisplayMenu();
                var currentOption = ReadCurrentOption();
                var currentItem = _menuItems.SingleOrDefault(menuItem => menuItem.Shortcut == currentOption);
                if (currentItem == null)
                {
                    DisplayInvalidOption();
                    continue;
                }

                currentItem.Execute(parentObject);
            }
        }
    }
}
