using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using BankingApp.DAL;
using BankingApp.DAL.Abstractions;
using BankingApp.DataModel.Enums;
using BankingApp.DataModel.Models;
using Services;
using Services.Converters;
using Services.Exceptions;

namespace BankingAppWPF
{
    /// <summary>
    /// Interaction logic for DepositMoneyWindow.xaml
    /// </summary>
    public partial class DepositMoneyWindow : Window
    {
        private readonly Account _account;
        private static readonly IRepositoryWrapper RepositoryWrapper = new RepositoryWrapper(new BankContext());
        private readonly AccountService _accountService= new(RepositoryWrapper);
        private readonly TransactionService _transactionService = new(RepositoryWrapper);
        private readonly ConverterEngine _converter = new();
        public DepositMoneyWindow(Account account)
        {
            _account = account;
            InitializeComponent();
            CurrencyComboBox.ItemsSource = Enum.GetValues(typeof(Currency)).Cast<Currency>();
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ButtonDeposit_OnClick(object sender, RoutedEventArgs e)
        {
            var amount = Convert.ToDouble(AmountTextBox.Text);
            var currency = (Currency) CurrencyComboBox.SelectedItem;
            if (currency != _account.Currency)
            {
                amount = _converter.Convert(currency, amount);
            }

            try
            {
                _accountService.Deposit(amount, _account);
                _transactionService.RecordNewDeposit(_account, amount);
            }
            catch (AccountClosedException exception)
            {
                MessageBox.Show(exception.Message);
            }

            MessageBox.Show("Deposit successfully");
            Close();
        }

        private void CancelBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
