using System;
using System.Linq;
using System.Windows;
using BankingApp.DAL;
using BankingApp.DAL.Abstractions;
using BankingApp.DataModel.Enums;
using BankingApp.DataModel.Models;
using Services;
using Services.Searcher;

namespace BankingAppWPF
{
    /// <summary>
    /// Interaction logic for AdminTransactionsWindows.xaml
    /// </summary>
    public partial class AdminTransactionsWindows : Window
    {
        private readonly Customer _admin;
        private readonly SearchEngine _searchEngine;
        private static readonly IRepositoryWrapper RepositoryWrapper = new RepositoryWrapper(new BankContext());
        private readonly AccountService _accountService = new(RepositoryWrapper);
        private readonly TransactionService _transactionService = new(RepositoryWrapper);
        public AdminTransactionsWindows(Customer admin, Account account)
        {
            InitializeComponent();
            TitleTextBlock.Text = $"{account.Iban} transactions";
            _admin = admin;
            var list = _transactionService.ViewTransactionsFor(account);
            _searchEngine = new SearchEngine(list.ToList());
            TransactionsView.ItemsSource = list;
            ComboBoxFilter.ItemsSource = Enum.GetValues(typeof(TransactionTypes)).Cast<TransactionTypes>();
        }

        private void HomeBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var adminHomeWindow = new AdminHomeWindow(_admin);
            adminHomeWindow.Show();
            Close();
        }

        private void HelpBtn_OnClick(object sender, RoutedEventArgs e)
        {
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


        private void SearchBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var filterButton = (TransactionTypes)ComboBoxFilter.SelectedIndex;

            if (filterButton < 0)
            {
                MessageBox.Show("Please select a filter");
            }
            else
            {
                var results = _searchEngine.Search(filterButton);
                TransactionsView.ItemsSource = results;
                TransactionsView.Items.Refresh();
            }
        }
    }
}
