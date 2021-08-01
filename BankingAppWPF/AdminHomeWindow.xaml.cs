using System;
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
    /// Interaction logic for AdminHomeWindow.xaml
    /// </summary>
    public partial class AdminHomeWindow : Window
    {
        private readonly Customer _admin;
        private static readonly IRepositoryWrapper RepositoryWrapper = new RepositoryWrapper(new BankContext());
        private readonly CustomerService _customerService = new CustomerService(RepositoryWrapper);
        public AdminHomeWindow(Customer admin)
        {
            InitializeComponent();
            _admin = admin;
            CustomerView.ItemsSource = _customerService.GetCustomers();
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
            var selectedCustomer = CustomerView.SelectedItem as Customer;
            var accountWindow = new AdminAccountsWindow(_admin,selectedCustomer);
            accountWindow.Show();
            Close();
        }

        private void ListViewItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var lvi = (ListViewItem)sender;
            CustomerView.SelectedItem = lvi.DataContext;
        }

    }
}
