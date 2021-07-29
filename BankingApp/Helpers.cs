using System;

namespace BankingApp
{
    public class Helpers
    {
        public static string ReadChoice()
        {
            Console.Write("Your choice: ");
            var userInput = Console.ReadLine();
            return userInput;
        }

        public static double ReadInput()
        {
            return (double.TryParse(ReadChoice(), out var value) ? value : 0);
        }
    }
}
