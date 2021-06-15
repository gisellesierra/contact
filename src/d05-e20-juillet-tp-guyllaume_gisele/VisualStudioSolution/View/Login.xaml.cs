using BLL;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {

            if (BusinessLogicLayer.Login(usernameTextbox.Text, userPassword.Text) == true)
            {
                if (BusinessLogicLayer.RechercheLog(usernameTextbox.Text) == -1)
                {
                    Console.WriteLine("User ID not found");
                }
                else
                {
                    BusinessLogicLayer.identifiant = BusinessLogicLayer.RechercheLog(usernameTextbox.Text);
                    UI uIWindow = new UI();
                    this.NavigationService.Navigate(uIWindow);
                }
            }
            else
            {
                this.usernameTextbox.Text = "Wrong login or password";
                this.userPassword.Text = "";
            }


        }

        private void TextblockSignUp_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            SignUp sign = new SignUp();
            this.NavigationService.Navigate(sign);
        }

        private void textblockForgotPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            SignUp sign = new SignUp();
            this.NavigationService.Navigate(sign);
        }
    }
}
