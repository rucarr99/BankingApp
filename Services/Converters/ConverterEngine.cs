using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingApp.DataModel.Enums;

namespace Services.Converters
{
    public class ConverterEngine
    {
        private readonly IDictionary<Currency, ICurrencyConverter> _currencyConverters =
            new Dictionary<Currency, ICurrencyConverter>();

        public ConverterEngine()
        {
            _currencyConverters[Currency.Euro] = new CurrencyConverterEuroToRon();
            _currencyConverters[Currency.Lei] = new CurrencyConverterRonToEuro();
        }

        public double Convert(Currency currency, double amount)
        {
            return _currencyConverters[currency].Convert(amount);
        }
    }
}
