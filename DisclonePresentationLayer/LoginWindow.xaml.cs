using LogicLayer;
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

namespace DisclonePresentationLayer
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private IUserManager _userManager;
        public LoginWindow(IUserManager userManager)
        {
            InitializeComponent();
            splLogin.Visibility = Visibility.Visible;
            splCreateAccount.Visibility = Visibility.Collapsed;

            _userManager = userManager;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BadPractice.User = _userManager.AuthenticateUser(txtEmail.Text, pwdPassword.Password);
                this.DialogResult = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException ?? ex.InnerException.Message);
            }
           

        }

        private void btnNewAccount_Click(object sender, RoutedEventArgs e)
        {
            splLogin.Visibility = Visibility.Collapsed;
            splCreateAccount.Visibility = Visibility.Visible;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            splLogin.Visibility = Visibility.Visible;
            splCreateAccount.Visibility = Visibility.Collapsed;
        }

        private void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(pwdNewPassword.Password == pwdConfirmPassword.Password)
                {
                    if(_userManager.AddUser(txtNewEmail.Text, txtNewUsername.Text, pwdNewPassword.Password))
                    {
                        splLogin.Visibility = Visibility.Visible;
                        splCreateAccount.Visibility = Visibility.Collapsed;
                        MessageBox.Show("Account created, please login");
                    }
                }
                else
                {
                    throw new ApplicationException("Passwords do not match");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException ?? ex.InnerException.Message);
            }
        }
    }
}
