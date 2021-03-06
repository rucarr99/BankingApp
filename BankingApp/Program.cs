using System;

namespace BankingApp
{
    public class Program
    {
        static void Main(string[] args)
        {

            var app = new BankingAppMenu();
            try
            {
                app.LogInScreen();
                app.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Oops, something went wrong: {e.InnerException?.Message}");
            }
        }
    }
}
