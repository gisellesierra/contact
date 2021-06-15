using System;
using System.Collections.Generic;

namespace Model
{

    public class Contact
    {

        public string NAS { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Adress { get; set; }
        public string PostalCode { get; set; }
        public DateTime DoB { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int Id_LoginUtilisateur { get; set; }



        public void Print()
        {
            Console.WriteLine("NAS : {0}", this.NAS);
            Console.WriteLine("Name : {0}, {1}", this.FirstName, this.LastName);
            Console.WriteLine("Adress : {0}", this.Adress);
            Console.WriteLine("Postal Code : {0}", this.PostalCode);
            Console.WriteLine("Date Of Birth : {0}", this.DoB);
            Console.WriteLine("Phone : {0}", this.PhoneNumber);
            Console.WriteLine("Email : {0}", this.Email);
        }

    }

    public class Login
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Contact> ContactList { get; set; }
    }
    class Test
    {

        static void Main(string[] args)
        {

        }
    }
}
