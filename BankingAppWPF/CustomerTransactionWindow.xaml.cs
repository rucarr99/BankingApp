using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using BankingApp.DAL;
using BankingApp.DAL.Abstractions;
using BankingApp.DataModel.Enums;
using BankingApp.DataModel.Models;
using Services;
using Services.Generators;
using Services.Searcher;

namespace BankingAppWPF
{
    /// <summary>
    /// Interaction logic for CustomerTransactionWindow.xaml
    /// </summary>
    public partial class CustomerTransactionWindow : Window
    {
        private readonly SearchEngine _searchEngine;
        private static readonly IRepositoryWrapper RepositoryWrapper = new RepositoryWrapper(new BankContext());
        private readonly TransactionService _transactionService  = new(RepositoryWrapper);
        private readonly IGenerator _reportGenerator;
        private readonly Account _account;
        private readonly Customer _customer;

        public CustomerTransactionWindow(Customer customer)
        {
            InitializeComponent();
            TransactionsView.ItemsSource = _transactionService.ViewTransactionsFor(customer);
            ComboBoxFilter.ItemsSource = Enum.GetValues(typeof(TransactionTypes)).Cast<TransactionTypes>();
        }

        public CustomerTransactionWindow(Account account, Customer customer)
        {
            InitializeComponent();
            _account = account;
            _customer = customer;
            var list = _transactionService.ViewTransactionsFor(account);
            TransactionsView.ItemsSource = list;
            ComboBoxFilter.ItemsSource = Enum.GetValues(typeof(TransactionTypes)).Cast<TransactionTypes>();
            _searchEngine = new SearchEngine(list.ToList());
            _reportGenerator = new DocGenerator();

        }

        private void HomeBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var userHomeWindow = new UserHomeWindow(_customer);
            userHomeWindow.Show();
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


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {

            var checkedTransactions = new List<Transaction>();

            if (TransactionsView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select transactions you want to print", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                foreach (var transaction in TransactionsView.SelectedItems)
                {
                    checkedTransactions.Add((Transaction) transaction);
                }

                _reportGenerator.Generate(checkedTransactions, _account);

                MessageBox.Show("Report generated successfully");
                var homeUserHomeWindow = new UserHomeWindow(_customer);
                homeUserHomeWindow.Show();
                Close();
            }
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
