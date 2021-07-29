using System;

namespace Services.Converters
{
    public class CurrencyConverterEuroToRon : ICurrencyConverter
    {
        public double Convert(double amount)
        {
            return amount * 4.92d;
        }
    }
}
