using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Poste.Models
{
    // Classe che estende RoleProvider per gestire i ruoli del sito
    public class SiteRole : RoleProvider
    {
        // Proprietà ApplicationName
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        // Metodo per aggiungere gli utenti ai ruoli
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        // Metodo per creare un nuovo ruolo
        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        // Metodo per eliminare un ruolo
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        // Metodo per trovare gli utenti in un ruolo
        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        // Metodo per ottenere tutti i ruoli
        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        // Metodo per ottenere i ruoli di un utente
        public override string[] GetRolesForUser(string username)
        {
            {
                // Ottieni la stringa di connessione dal file di configurazione
                string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ConnectionString.ToString();
                SqlConnection conn = new SqlConnection(connectionString);

                // Crea il comando SQL per selezionare il ruolo dell'utente
                SqlCommand cmd = new SqlCommand("SELECT Role FROM Users WHERE Username=@Username", conn);
                cmd.Parameters.AddWithValue("Username", username);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                List<string> roles = new List<string>();

                // Leggi i risultati della query e aggiungi i ruoli alla lista
                while (reader.Read())
                {
                    string ruolo = reader["Role"].ToString();
                    roles.Add(ruolo);
                }
                conn.Close();

                return roles.ToArray();
            }
        }

        // Metodo per ottenere gli utenti in un ruolo
        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        // Metodo per verificare se un utente è in un ruolo
        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        // Metodo per rimuovere gli utenti dai ruoli
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        // Metodo per verificare se un ruolo esiste
        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}