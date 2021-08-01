using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BankingApp.DAL;
using BankingApp.DAL.Abstractions;
using BankingApp.DataModel.Models;
using Services;
using Exception = System.Exception;

namespace BankingAppWPF
{
    /// <summary>
    /// Interaction logic for AdminAccountsWindow.xaml
    /// </summary>
    public partial class AdminAccountsWindow : Window
    {
        private readonly Customer _customer;
        private readonly Customer _admin;
        private static readonly IRepositoryWrapper RepositoryWrapper = new RepositoryWrapper(new BankContext());
        private readonly CustomerService _customerService = new(RepositoryWrapper);
        private readonly AccountService _accountService = new(RepositoryWrapper);
        public AdminAccountsWindow(Customer admin, Customer customer)
        {
            InitializeComponent();
            CustomerNameText.Text = $"{customer.FirstName} Accounts";
            _customer = customer;
            _admin = admin;
            AccountsView.ItemsSource = _customerService.GetAccounts(customer);
        }

        private void LogoutBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var messageBoxResult =
                MessageBox.Show("Are you sure?", "Logout confirmation", MessageBoxButton.YesNo);

            if (messageBoxResult != MessageBoxResult.Yes) return;
            var main = new MainWindow();
            Close();
            main.Show();
        }

        private void HelpBtn_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void OpenAccountBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var openAccountWindow = new CreateAccountWindow(_customer);
            openAccountWindow.Closed += OpenAccountWindow_Closed;
            openAccountWindow.ShowDialog();
            AccountsView.Items.Refresh();
        }

        private void OpenAccountWindow_Closed(object sender, EventArgs e)
        {
            if ((sender as Window)?.DialogResult == true)
            {
                AccountsView.ItemsSource = _customerService.GetAccounts(_customer).ToList().AsReadOnly();
            }
        }

        private void ListViewItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var lvi = (ListViewItem)sender;
            AccountsView.SelectedItem = lvi.DataContext;
        }


        private void CloseBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedAccount = AccountsView.SelectedItem as Account;
            var messageBoxResult = MessageBox.Show("Are you sure?", "Confirm", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    _accountService.CloseAccount(selectedAccount);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }

        }

        private void TransactionBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedAccount = AccountsView.SelectedItem as Account;
            var transactionsWindow = new AdminTransactionsWindows(_admin, selectedAccount);
            transactionsWindow.Show();
            Close();
        }

        private void SeeCustomers_OnClick(object sender, RoutedEventArgs e)
        {
            var adminWindow = new AdminHomeWindow(_admin);
            adminWindow.Show();
            Close();
        }
    }
}
