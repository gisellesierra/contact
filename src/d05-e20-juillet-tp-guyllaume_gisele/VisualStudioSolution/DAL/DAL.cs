using System;
using Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Data;


namespace DAL
{
    public class DataAccessLayer
    {
        static readonly string connectionString = @"Data Source=G0J5XS2\SQLEXPRESS;Initial Catalog=Contacts;Integrated Security=True;Connect Timeout=10";
        //static readonly string connectionString = @"Data Source=J2F-VMT2\SQLEXPRESS;Initial Catalog=Contacts;Integrated Security=True;Connect Timeout=10";
        public static void AjouterContact(Contact contact)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    conn.Open();
                    Console.WriteLine(conn.State);

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"INSERT into InfoClient(NAS, Id_LoginUtilisateur, FirstName, LastName, Adress, PostalCode, DoB, PhoneNumber, Email) values (@NAS, @Id_LoginUtilisateur, @FirstName, @LastName, @Adress, @PostalCode, @DoB, @PhoneNumber, @Email)";
                        cmd.Parameters.AddWithValue("@NAS", contact.NAS);
                        cmd.Parameters.AddWithValue(@"Id_LoginUtilisateur", contact.Id_LoginUtilisateur);
                        cmd.Parameters.AddWithValue("@FirstName", contact.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", contact.LastName);
                        cmd.Parameters.AddWithValue("@Adress", contact.Adress);
                        cmd.Parameters.AddWithValue("@PostalCode", contact.PostalCode);
                        cmd.Parameters.AddWithValue("@DoB", contact.DoB);


                        if (contact.PhoneNumber != null)
                        {
                            cmd.Parameters.AddWithValue("@PhoneNumber", contact.PhoneNumber);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@PhoneNumber", DBNull.Value);
                        }
                        if (contact.Email != null)
                        {
                            cmd.Parameters.AddWithValue("@Email", contact.Email);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Email", DBNull.Value);
                        }

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Contact Added");
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Invalid Operation Exception raised");
                Console.WriteLine(ex.Message);
                // Console.WriteLine(ex.StackTrace);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Exception raised");
                Console.WriteLine(ex.Message);
                //Console.WriteLine(ex.StackTrace);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception raised");
                Console.WriteLine(ex.Message);
                // Console.WriteLine(ex.StackTrace);
            }
            Console.WriteLine("Connection closeds");
        }

        public static void EditionContact(Contact contact)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"update InfoClient Set NAS = @NAS, FirstName = @FirstName, LastName = @LastName, Adress = @Adress, PostalCode = @PostalCode, DoB = @DoB, PhoneNumber = @PhoneNumber, Email = @Email  where NAS = @NAS";
                        cmd.Parameters.AddWithValue("@NAS", contact.NAS);
                        cmd.Parameters.AddWithValue("@FirstName", contact.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", contact.LastName);
                        cmd.Parameters.AddWithValue("@Adress", contact.Adress);
                        cmd.Parameters.AddWithValue("@PostalCode", contact.PostalCode);
                        cmd.Parameters.AddWithValue("@DoB", contact.DoB);

                        if (contact.PhoneNumber != null)
                        {
                            cmd.Parameters.AddWithValue("@PhoneNumber", contact.PhoneNumber);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@PhoneNumber", DBNull.Value);
                        }
                        if (contact.Email != null)
                        {
                            cmd.Parameters.AddWithValue("@Email", contact.Email);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Email", DBNull.Value);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }
                Console.WriteLine("updated");
            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.Message);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception raised");
                Console.WriteLine(ex.Message);
                // Console.WriteLine(ex.StackTrace);
            }
        }

        public static List<Contact> GetDataBase(int Id_LoginUtilisateur)
        {

            List<Contact> contactList = new List<Contact>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine(connection.State);

                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM InfoClient where Id_LoginUtilisateur = @Id_LoginUtilisateur";
                        cmd.Parameters.Add(new SqlParameter("Id_LoginUtilisateur", Id_LoginUtilisateur));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Contact contact = new Contact();

                                contact.NAS = reader.GetString(0);
                                contact.Id_LoginUtilisateur = reader.GetInt32(1);
                                contact.FirstName = reader.GetString(2);
                                contact.LastName = reader.GetString(3);
                                contact.Adress = reader.GetString(4);
                                contact.PostalCode = reader.GetString(5);
                                contact.DoB = reader.GetDateTime(6);
                                if (reader.GetValue(7) != DBNull.Value)
                                {
                                    contact.PhoneNumber = reader.GetString(7);
                                }
                                else
                                {
                                    contact.PhoneNumber = null;
                                }
                                if (reader.GetValue(8) != DBNull.Value)
                                {
                                    contact.Email = reader.GetString(8);
                                }
                                else
                                {
                                    contact.Email = null;
                                }

                                contactList.Add(contact);

                            }
                        }

                        /* Uniquement a des fins test d'output console */
                        foreach (Contact element in contactList)
                        {
                            element.Print();
                        }
                        Console.WriteLine("List of Contacts {0}", contactList.Count);

                        return contactList;
                    }

                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Invalid Operation Exception raised");
                Console.WriteLine(ex.Message);
                // Console.WriteLine(ex.StackTrace);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Exception raised");
                Console.WriteLine(ex.Message);
                // Console.WriteLine(ex.StackTrace);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception raised");
                Console.WriteLine(ex.Message);
                //Console.WriteLine(ex.StackTrace);
            }
            Console.WriteLine("Connection closed");

            return contactList;
        }

        public static void Suppression(string nas, int Id_LoginUtilisateur)
        {
            try
            {

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"delete from InfoClient where NAS = @NAS AND Id_LoginUtilisateur = @Id_LoginUtilisateur";
                        cmd.Parameters.AddWithValue("@NAS", nas);
                        cmd.Parameters.AddWithValue("@Id_LoginUtilisateur", Id_LoginUtilisateur);
                        cmd.ExecuteNonQuery();
                    }
                }
                Console.WriteLine("Contact supprimé");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Invalid Operation Exception raised");
                Console.WriteLine(ex.Message);
                // Console.WriteLine(ex.StackTrace);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Exception raised");
                Console.WriteLine(ex.Message);
                // Console.WriteLine(ex.StackTrace);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception raised");
                Console.WriteLine(ex.Message);
                //Console.WriteLine(ex.StackTrace);
            }
        }

        public static List<Contact> Recherche(string nas, int Id_LoginUtilisateur)
        {
            List<Contact> maList = new List<Contact>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"select * from InfoClient where NAS = @NAS AND Id_LoginUtilisateur = @Id_LoginUtilisateur";
                        cmd.Parameters.Add(new SqlParameter("@NAS", nas));
                        cmd.Parameters.Add(new SqlParameter("@Id_LoginUtilisateur", Id_LoginUtilisateur));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Contact contact = new Contact();
                                contact.NAS = reader.GetString(0);
                                contact.Id_LoginUtilisateur = reader.GetInt32(1);
                                contact.FirstName = reader.GetString(2);
                                contact.LastName = reader.GetString(3);
                                contact.Adress = reader.GetString(4);
                                contact.PostalCode = reader.GetString(5);
                                contact.DoB = reader.GetDateTime(6);
                                if (reader.GetValue(7) != DBNull.Value)
                                {
                                    contact.PhoneNumber = reader.GetString(7);
                                }
                                else
                                {
                                    contact.PhoneNumber = null;
                                }
                                if (reader.GetValue(8) != DBNull.Value)
                                {
                                    contact.Email = reader.GetString(8);
                                }
                                else
                                {
                                    contact.Email = null;
                                }

                                maList.Add(contact);
                            }
                        }

                        foreach (Contact element in maList)
                        {
                            element.Print();

                        }
                        Console.WriteLine("List of Contacts {0}", maList.Count);
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Invalid Operation Exception raised");
                Console.WriteLine(ex.Message);
                // Console.WriteLine(ex.StackTrace);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Exception raised");
                Console.WriteLine(ex.Message);
                // Console.WriteLine(ex.StackTrace);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception raised");
                Console.WriteLine(ex.Message);
                //Console.WriteLine(ex.StackTrace);
            }
            return maList;
        }

        public static List<Contact> TriRegroup(string nom, int Id_LoginUtilisateur)
        {
            List<Contact> maList = new List<Contact>();
            Contact contact = new Contact();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"select * from InfoClient where FirstName like @FirstName + '%'  AND Id_LoginUtilisateur = @Id_LoginUtilisateur order by NAS ";
                    cmd.Parameters.Add(new SqlParameter("FirstName", nom));
                    cmd.Parameters.Add(new SqlParameter("@Id_LoginUtilisateur", Id_LoginUtilisateur));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {


                            contact.NAS = reader.GetString(0);
                            contact.Id_LoginUtilisateur = reader.GetInt32(1);
                            contact.FirstName = reader.GetString(2);
                            contact.LastName = reader.GetString(3);
                            contact.Adress = reader.GetString(4);
                            contact.PostalCode = reader.GetString(5);
                            contact.DoB = reader.GetDateTime(6);
                            if (reader.GetValue(7) != DBNull.Value)
                            {
                                contact.PhoneNumber = reader.GetString(7);
                            }
                            else
                            {
                                contact.PhoneNumber = null;
                            }
                            if (reader.GetValue(8) != DBNull.Value)
                            {
                                contact.Email = reader.GetString(8);
                            }
                            else
                            {
                                contact.Email = null;
                            }

                            maList.Add(contact);
                        }
                    }

                    /* Test output */
                    foreach (Contact element in maList)
                    {
                        element.Print();

                    }
                    Console.WriteLine("List of Contacts {0}", maList.Count);
                }
            }
            return maList;
        }

        public static bool Login(string username, string password)
        {
            Login l = new Login();
            bool connection = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {

                        cmd.CommandText = @"select * from LoginUtilisateur where username=@username and password=@password";
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                l.Id = reader.GetInt32(0);
                                l.Username = reader.GetString(1);
                                l.Password = reader.GetString(2);

                                if (username == l.Username && password == l.Password)
                                {
                                    connection = true;
                                }
                                else
                                {
                                    connection = false;
                                }

                            }
                        }
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Invalid Operation Exception raised");
                Console.WriteLine(ex.Message);
                // Console.WriteLine(ex.StackTrace);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Exception raised");
                Console.WriteLine(ex.Message);
                // Console.WriteLine(ex.StackTrace);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception raised");
                Console.WriteLine(ex.Message);
                //Console.WriteLine(ex.StackTrace);
            }
            return connection;
        }

        public static void Inscription(String username, String password)
        {
            Login l = new Login();

            l.Username = username;
            l.Password = password;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {

                        cmd.CommandText = @"insert into LoginUtilisateur(username,password) values (@username,@password)";

                        cmd.Parameters.AddWithValue("@username", l.Username);
                        cmd.Parameters.AddWithValue("@password", l.Password);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Invalid Operation Exception raised");
                Console.WriteLine(ex.Message);
                // Console.WriteLine(ex.StackTrace);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Exception raised");
                Console.WriteLine(ex.Message);
                // Console.WriteLine(ex.StackTrace);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception raised");
                Console.WriteLine(ex.Message);


            }


        }

        public static int RechercheLog(string username)
        {
            int idUser = -1;

            try
            {

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"select id
                            from LoginUtilisateur where username = @username";
                        cmd.Parameters.Add(new SqlParameter("username", username));

                        try
                        {

                            object idRecherche = cmd.ExecuteScalar();
                            idUser = (int)idRecherche;
                        }
                        catch (InvalidCastException ex)
                        {
                            Console.WriteLine("Invalid Cast Exception raised");
                            Console.WriteLine(ex.Message);
                            // Console.WriteLine(ex.StackTrace);
                        }

                        return idUser;
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Invalid Operation Exception raised");
                Console.WriteLine(ex.Message);
                // Console.WriteLine(ex.StackTrace);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Exception raised");
                Console.WriteLine(ex.Message);
                // Console.WriteLine(ex.StackTrace);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception raised");
                Console.WriteLine(ex.Message);
            }
            return idUser;
        }
    }
}
