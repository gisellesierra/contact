using DAL;
using Model;
using System.Collections.Generic;

namespace BLL
{
    public class BusinessLogicLayer
    {
        public static int identifiant;

        public static void AjouterContact(Contact contact)
        {
            DataAccessLayer.AjouterContact(contact);
        }

        public static void EditionContact(Contact contact)
        {
            DataAccessLayer.EditionContact(contact);
        }

        public static List<Contact> GetDataBase(int id)
        {
            return DataAccessLayer.GetDataBase(id);

        }

        public static void Suppression(string nas, int Id_LoginUtilisateur)
        {
            DataAccessLayer.Suppression(nas, Id_LoginUtilisateur);
        }

        public static List<Contact> Recherche(string nas, int Id_LoginUtilisateur)
        {
            return DataAccessLayer.Recherche(nas, Id_LoginUtilisateur);
        }

        public static List<Contact> voidTriRegroup(string nom, int Id_LoginUtilisateur)
        {
            return DataAccessLayer.TriRegroup(nom, Id_LoginUtilisateur);
        }

        public static bool Login(string username, string password)
        {
            return DataAccessLayer.Login(username, password);
        }

        public static void Inscription(string username, string password)
        {
            DataAccessLayer.Inscription(username, password);
        }

        public static int RechercheLog(string username)
        {
            return DataAccessLayer.RechercheLog(username);
        }


    }
}
