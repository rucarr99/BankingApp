using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BankingApp.DAL;
using BankingApp.DAL.Abstractions;
using BankingApp.DataModel.Models;
using Services;
using Services.Exceptions;

namespace BankingAppWPF
{
    /// <summary>
    /// Interaction logic for WithdrawMoneyWindow.xaml
    /// </summary>
    public partial class WithdrawMoneyWindow : Window
    {
        private readonly Account _account;
        private static readonly IRepositoryWrapper RepositoryWrapper = new RepositoryWrapper(new BankContext());
        private readonly AccountService _accountService = new(RepositoryWrapper);
        private readonly TransactionService _transactionService = new(RepositoryWrapper);
        public WithdrawMoneyWindow(Account account)
        {
            InitializeComponent();
            _account = account;
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ButtonWithdraw_OnClick(object sender, RoutedEventArgs e)
        {
            var amount = Convert.ToDouble(AmountTextBox.Text);
            try
            {
                _accountService.Withdraw(_account, amount);
                _transactionService.RecordNewWithdraw(_account, amount);

            }
            catch (InsufficientMoneyException exception)
            {
                MessageBox.Show(exception.Message);
            }
            catch (AccountClosedException ex)
            {
                MessageBox.Show(ex.Message);
            }
            Close();
        }

        private void CancelBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
