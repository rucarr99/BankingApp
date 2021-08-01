using System;
using System.Linq;
using System.Windows;
using BankingApp.DAL;
using BankingApp.DAL.Abstractions;
using BankingApp.DataModel.Enums;
using BankingApp.DataModel.Models;
using Services;

namespace BankingAppWPF
{
    /// <summary>
    /// Interaction logic for CreateAccountWindow.xaml
    /// </summary>
    public partial class CreateAccountWindow : Window
    {
        private readonly Customer _customer;
        private static readonly IRepositoryWrapper RepositoryWrapper = new RepositoryWrapper(new BankContext());
        private readonly CustomerService _customerService = new(RepositoryWrapper);
        private readonly AccountService _accountService = new(RepositoryWrapper);
        public CreateAccountWindow(Customer customer)
        {
            InitializeComponent();
            _customer = customer;
            CurrencyTypeComboBox.ItemsSource = Enum.GetValues(typeof(Currency)).Cast<Currency>();
            AccountTypeComboBox.ItemsSource = Enum.GetValues(typeof(AccountTypes)).Cast<AccountTypes>();
        }
        

        private void CreateAccountBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var iban = IbanTextBox.Text;
            var accountType = (AccountTypes) AccountTypeComboBox.SelectedItem;
            var currency = (Currency) CurrencyTypeComboBox.SelectedItem;

            try
            {
                _accountService.CreateAccount(_customer, accountType, currency, iban);
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        }

        private void CancelBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
