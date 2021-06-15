using Model;
using BLL;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

namespace View
{
    /// <summary>
    /// Interaction logic for UI.xaml
    /// </summary>
    public partial class UI : Page
    {
        public UI()
        {
            InitializeComponent();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            Contact contact = new Contact();
            this.TextboxDoB.Text = "" + DateTime.Now;

            if (this.TextboxNAS.Text.Length == 0 || this.TextboxFirstName.Text.Length == 0 || this.TextboxLastName.Text.Length == 0 ||
                this.TextboxAdress.Text.Length == 0 || this.TextboxPostalCode.Text.Length == 0)
            {
                this.TextboxDisplay.Text = "";
                this.TextboxDisplay.Text = "Required Fields Are : " +
                      "\n" + "NAS" +
                      "\n" + "First Name" +
                      "\n" + "Last Name" +
                      "\n" + "Adress" +
                      "\n" + "Postal Code" +
                      "\n" + "DoB";

            }
            else
            {
                contact.NAS = this.TextboxNAS.Text;
                contact.Id_LoginUtilisateur = BusinessLogicLayer.identifiant;
                contact.FirstName = this.TextboxFirstName.Text;
                contact.LastName = this.TextboxLastName.Text;
                contact.Adress = this.TextboxAdress.Text;
                contact.PostalCode = this.TextboxPostalCode.Text;
                contact.DoB = DateTime.Parse(this.TextboxDoB.Text);
                contact.PhoneNumber = this.TextboxPhone.Text;
                contact.Email = this.TextboxEmail.Text;


                BLL.BusinessLogicLayer.AjouterContact(contact);
                this.TextboxDisplay.Text = "Contact Added";
            }

        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            this.TextboxNAS.Text = "";
            this.TextboxFirstName.Text = "";
            this.TextboxLastName.Text = "";
            this.TextboxAdress.Text = "";
            this.TextboxPostalCode.Text = "";
            this.TextboxDoB.Text = "";
            this.TextboxPhone.Text = "";
            this.TextboxEmail.Text = "";

            this.TextboxDisplay.Text = "";
        }

        private void ButtonDisplay_Click(object sender, RoutedEventArgs e)
        {
            this.TextboxDisplay.Text = "";
            List<Contact> contactList = new List<Contact>();
            contactList = BLL.BusinessLogicLayer.GetDataBase(BusinessLogicLayer.identifiant);
            if (contactList.Count != 0)
            {
                string display;

                foreach (Contact elements in contactList)
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
                    this.TextboxDisplay.Text += display + "\n";
                }
            }
            else
            {
                this.TextboxDisplay.Text = "Contact list Empty";
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            BLL.BusinessLogicLayer.Suppression(this.TextboxNAS.Text, BLL.BusinessLogicLayer.identifiant);
            this.TextboxDisplay.Text = "Contact Deleted";

        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            Edit editWindow = new Edit();
            this.NavigationService.Navigate(editWindow);
            this.TextboxDisplay.Text = "Contact Edited";

        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            List<Contact> list = new List<Contact>();
            list = BLL.BusinessLogicLayer.Recherche(this.TextboxNAS.Text, BLL.BusinessLogicLayer.identifiant);
            if (list.Count != 0)
            {
                string display;
                this.TextboxDisplay.Text = "";

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
                    this.TextboxDisplay.Text += display + "\n";
                }
            }
            else
            {
                this.TextboxDisplay.Text = "Contact not found";
            }
        }
    }
}
