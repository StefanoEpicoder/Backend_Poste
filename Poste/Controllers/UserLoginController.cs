﻿using Poste.Models;
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
    public class UserLoginController : Controller
    {
        // Metodo per visualizzare la pagina di login
        public ActionResult Index()
        {
            return View();
        }

        // Metodo per effettuare il login dell'utente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users u)
        {
            string conn = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(conn);

            try
            {
                sqlConnection.Open();

                // Query per verificare le credenziali dell'utente
                SqlCommand sqlCommand = new SqlCommand("select * from Users where Username=@Username and Password=@Password and Role='User'", sqlConnection);
                sqlCommand.Parameters.AddWithValue("Username", u.Username);
                sqlCommand.Parameters.AddWithValue("Password", u.Password);

                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    // Imposta la sessione e l'autenticazione dell'utente
                    Session["Username"] = u.Username;
                    FormsAuthentication.SetAuthCookie(u.Username, false);
                    return RedirectToAction("RegisterPackage", "Spedizioni");
                }
                else
                {
                    // Mostra un messaggio di errore se l'utente non è un utente registrato
                    ViewBag.Error = "Non sei un User";
                    return View();
                }
            }
            catch (Exception ex)
            {
                // Gestisci eventuali eccezioni
            }
            finally { sqlConnection.Close(); }

            return View();
        }

        // Metodo per effettuare il logout dell'utente
        [HttpPost]
        public ActionResult Logout()
        {
            // Cancella la sessione e l'autenticazione dell'utente
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}