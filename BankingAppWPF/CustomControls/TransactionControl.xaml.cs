using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using BankingApp.DataModel.Enums;
using BankingApp.DataModel.Models;

namespace BankingAppWPF.CustomControls
{
    /// <summary>
    /// Interaction logic for TransactionControl.xaml
    /// </summary>
    public partial class TransactionControl : UserControl
    {
        public Transaction Transaction
        {
            get => (Transaction)(GetValue(TransactionProperty));
            set => SetValue(TransactionProperty, value);
        }

        public static readonly DependencyProperty TransactionProperty =
            DependencyProperty.Register("Transaction", typeof(Transaction), typeof(TransactionControl), new PropertyMetadata(new Transaction()
                { TransactionType = TransactionTypes.Deposit, Description = "description", Date = DateTime.Now }, SetText));

        public static void SetText(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not TransactionControl control) return;
            control.TransactionType.Text = ((e.NewValue as Transaction)?.TransactionType).ToString();
            control.Description.Text = (e.NewValue as Transaction)?.Description.ToString(CultureInfo.InvariantCulture);
            control.Date.Text = (e.NewValue as Transaction)?.Date.ToString();
        }
        public TransactionControl()
        {
            InitializeComponent();
        }
    }
}
