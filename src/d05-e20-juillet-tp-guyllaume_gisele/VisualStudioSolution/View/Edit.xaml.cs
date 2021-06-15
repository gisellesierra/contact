
using Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace View
{
    /// <summary>
    /// Interaction logic for Edit.xaml
    /// </summary>
    public partial class Edit : Page
    {
        public Edit()
        {
            InitializeComponent();
        }

        private void EditCommit_Click(object sender, RoutedEventArgs e)
        {
            Contact contact = new Contact();

            contact.NAS = this.EditTextboxNAS.Text;
            contact.FirstName = this.EditTextboxFirstName.Text;
            contact.LastName = this.EditTextboxLastName.Text;
            contact.Adress = this.EditTextboxAdress.Text;
            contact.PostalCode = this.EditTextboxPostalCode.Text;
            contact.DoB = DateTime.Parse(this.EditTextboxDoB.Text);
            contact.PhoneNumber = this.EditTextboxPhone.Text;
            contact.Email = this.EditTextboxEmail.Text;

            BLL.BusinessLogicLayer.EditionContact(contact);
        }

        private void EditViewUpdate_Click(object sender, RoutedEventArgs e)
        {
            List<Contact> list = new List<Contact>();
            list = BLL.BusinessLogicLayer.voidTriRegroup(this.EditUserSelectTextbox.Text, BLL.BusinessLogicLayer.identifiant);

            if (list.Count != 0)
            {
                string display;

                foreach (Contact elements in list)
                {
                    display = "NAS : " + elements.NAS +
                        "\n" + "First Name : " + elements.FirstName +
                        "\n" + "Last Name : " + elements.LastName +
                        "\n" + "Adress : " + elements.Adress +
                        "\n" + "Postal Code : " + elements.PostalCode +
                        "\n" + "DoB : " + elements.DoB +
                        "\n" + "Phone : " + elements.PhoneNumber +
                        "\n" + "Email : " + elements.Email +
                        "\n";


                    this.EditTextboxDisplay.Text = display;
                }
            }
            else
            {
                this.EditTextboxDisplay.Text = "User not found";
            }
        }
    }
}
