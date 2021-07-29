using System.Windows;
using System.Windows.Input;
using BankingApp.DAL;
using BankingApp.DAL.Abstractions;
using BankingApp.DataModel.Models;
using Services;
using Services.Exceptions;

namespace BankingAppWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private static readonly IRepositoryWrapper RepositoryWrapper = new RepositoryWrapper(new BankContext());
        private readonly CustomerService _customerService = new CustomerService(RepositoryWrapper);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void LoginBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var cnp = TxtCnp.Text;
            var pin = TxtPin.Password;

            try
            {
                var customer = _customerService.LogIn(cnp, pin);
                if (customer.IsAdmin)
                {
                    MessageBox.Show("not done yet");
                }
                else
                {
                    var userHome = new UserHomeWindow(customer);
                    userHome.Show();
                    Close();
                }
            }
            catch (UserNotFoundException exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
