namespace Services.Converters
{
    public class CurrencyConverterRonToEuro : ICurrencyConverter
    {
        public double Convert(double amount)
        {
            return amount * 0.20327552d;
        }
    }
}
