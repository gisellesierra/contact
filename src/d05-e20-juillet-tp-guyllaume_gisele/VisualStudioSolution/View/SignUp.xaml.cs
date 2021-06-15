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
using System.Windows.Shapes;

namespace View
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Page
    {
        public SignUp()
        {
            InitializeComponent();
        }

       
        private void ButtonSignup_Click(object sender, RoutedEventArgs e)   
        {
            BusinessLogicLayer.Inscription(userSign.Text.Trim(), passSign.Text.Trim());
            Login fenetre = new Login();
            this.NavigationService.Navigate(fenetre);
        }

       
    }
}
