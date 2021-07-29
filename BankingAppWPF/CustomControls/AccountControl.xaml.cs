using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using BankingApp.DataModel.Models;

namespace BankingAppWPF.CustomControls
{
    /// <summary>
    /// Interaction logic for AccountControl.xaml
    /// </summary>
    public partial class AccountControl : UserControl
    {
        public Account Account
        {
            get => (Account)(GetValue(AccountProperty));
            set => SetValue(AccountProperty, value);
        }

        public static readonly DependencyProperty AccountProperty =
            DependencyProperty.Register("Account", typeof(Account), typeof(AccountControl), new PropertyMetadata(new Account()
            { Iban = "Iban", Balance = 0, Currency = 0 }, SetText));

        public static void SetText(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not AccountControl control) return;
            control.Iban.Text = (e.NewValue as Account)?.Iban;
            control.Balance.Text = (e.NewValue as Account)?.Balance.ToString(CultureInfo.InvariantCulture);
            control.Currency.Text = (e.NewValue as Account)?.Currency.ToString();
        }

        public AccountControl()
        {
            InitializeComponent();
        }

    }
}
