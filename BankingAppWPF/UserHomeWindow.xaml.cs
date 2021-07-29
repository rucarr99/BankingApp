using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BankingApp.DAL;
using BankingApp.DAL.Abstractions;
using BankingApp.DataModel.Models;
using Services;

namespace BankingAppWPF
{
    /// <summary>
    /// Interaction logic for UserHomeWindow.xaml
    /// </summary>
    public partial class UserHomeWindow : Window
    {
        private readonly Customer _customer;
        private static readonly IRepositoryWrapper RepositoryWrapper = new RepositoryWrapper(new BankContext());
        private readonly CustomerService _customerService = new CustomerService(RepositoryWrapper);
        public UserHomeWindow(Customer customer)
        {
            InitializeComponent();
            _customer = customer;
            var accounts = _customerService.GetAccounts(customer);
            AccountsView.ItemsSource = accounts.ToList();
        }

        private void TransactionsBtn_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void HelpBtn_OnClick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://google.com");
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

        private void ListViewItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var lvi = (ListViewItem) sender;
            AccountsView.SelectedItem = lvi.DataContext;
        }

        private void ButtonDeposit_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedAccount = AccountsView.SelectedItem as Account;
            var depositWindow = new DepositMoneyWindow(selectedAccount);
            depositWindow.Closed += Window_Closed;
            depositWindow.ShowDialog();
            AccountsView.Items.Refresh();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if ((sender as Window)?.DialogResult == true)
            {
                AccountsView.ItemsSource = _customerService.GetAccounts(_customer).ToList();
            }
        }

        private void ButtonWithdraw_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedAccount = AccountsView.SelectedItem as Account;
            var withdrawWindows = new WithdrawMoneyWindow(selectedAccount);
            withdrawWindows.Closed += Window_Closed;
            withdrawWindows.ShowDialog();
            AccountsView.Items.Refresh();
        }

        private void ButtonTransfer_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedAccount = AccountsView.SelectedItem as Account;
            var transferWindow = new TransferWindow(selectedAccount);
            transferWindow.Closed += Window_Closed;
            transferWindow.ShowDialog();
            AccountsView.Items.Refresh();
        }
    }
}
