using System.Windows;
using System.Windows.Controls;
using BankingApp.DataModel.Models;

namespace BankingAppWPF.CustomControls
{
    /// <summary>
    /// Interaction logic for ClientControl.xaml
    /// </summary>
    public partial class ClientControl : UserControl
    {

        public Customer Customer
        {
            get => (Customer)(GetValue(CustomerProperty));
            set => SetValue(CustomerProperty, value);
        }

        public static readonly DependencyProperty CustomerProperty = DependencyProperty.Register("Customer", typeof(Customer), typeof(ClientControl), new PropertyMetadata(new Customer()
        {
            FirstName = "FirstName",
            LastName = "LastName",
            Cnp = "cnp",
            Address = "Address",
            PhoneNumber = "00000"
        }, SetText));

        private static void SetText(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not ClientControl control) return;
            control.FirstName.Text = (e.NewValue as Customer)?.FirstName;
            control.LastName.Text = (e.NewValue as Customer)?.LastName;
            control.CNP.Text = (e.NewValue as Customer)?.Cnp;
            control.Address.Text = (e.NewValue as Customer)?.Address;
            control.PhoneNumber.Text = (e.NewValue as Customer)?.PhoneNumber;
        }

        public ClientControl()
        {
            InitializeComponent();
        }


    }
}
