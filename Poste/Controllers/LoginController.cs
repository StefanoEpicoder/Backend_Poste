using Poste.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Poste.Controllers
{
    public class LoginController : Controller
    {
        // Metodo per visualizzare la pagina di login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // Metodo per effettuare il login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Username, Password")] Users u)
        {
            string conn = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(conn);

            try
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("select * from Users where Username=@Username and Password=@Password and Role='Admin'", sqlConnection);
                sqlCommand.Parameters.AddWithValue("Username", u.Username);
                sqlCommand.Parameters.AddWithValue("Password", u.Password);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    // Imposta la sessione con il nome utente
                    Session["Username"] = u.Username;
                    FormsAuthentication.SetAuthCookie(u.Username, false);
                    return RedirectToAction("RegisterPackage", "Spedizioni");
                }
                else
                {
                    // Mostra un messaggio di errore se l'utente non è un amministratore
                    ViewBag.AuthError = "Non sei amministratore";
                    return View();
                }
            }
            catch (Exception ex)
            {
                // Gestisci eventuali eccezioni
            }
            finally
            {
                // Chiudi la connessione al database
                sqlConnection.Close();
            }

            return View();
        }

        // Metodo per effettuare il login con user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginUser([Bind(Include = "Username, Password")] Users u)
        {
            string conn = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(conn);

            try
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("select * from Users where Username=@Username and Password=@Password and Role='user'", sqlConnection);
                sqlCommand.Parameters.AddWithValue("Username", u.Username);
                sqlCommand.Parameters.AddWithValue("Password", u.Password);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    // Imposta la sessione con il nome utente
                    Session["Username"] = u.Username;
                    FormsAuthentication.SetAuthCookie(u.Username, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Mostra un messaggio di errore se l'utente non è un utente
                    ViewBag.AuthError = "Non sei un utente";
                    return View();
                }
            }
            catch (Exception ex)
            {
                // Gestisci eventuali eccezioni
            }
            finally
            {
                // Chiudi la connessione al database
                sqlConnection.Close();
            }

            return View();
        }

        // Metodo per effettuare il logout
        [HttpPost]
        public ActionResult Logout()
        {
            // Cancella la sessione
            Session.Clear();

            // Effettua il logout
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}