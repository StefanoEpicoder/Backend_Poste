using Poste.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Poste.Controllers
{
    public class TrackingController : Controller
    {
        [HttpGet]
        public ActionResult GetTrackingList()
        {
            // Creazione di una lista di oggetti Tracking
            List<Tracking> trackingList = new List<Tracking>();

            // Recupero della stringa di connessione dal file di configurazione
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString;

            // Creazione di una nuova connessione al database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Query per recuperare i dati di tracciamento
                string query = "SELECT Spedizioni.IdSpedizione, Spedizioni.IdCliente, Clienti.Nome FROM Spedizioni JOIN Clienti ON Spedizioni.IdCliente = Clienti.IdCliente";

                // Creazione di un nuovo comando SQL con la query e la connessione
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Esecuzione del comando SQL e recupero dei dati tramite un reader
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Iterazione sui risultati del reader
                        while (reader.Read())
                        {
                            // Creazione di un nuovo oggetto Tracking con i dati recuperati
                            Tracking spedizione = new Tracking
                            {
                                IdSpedizione = (int)reader["IdSpedizione"],
                                NomeCliente = reader["Nome"].ToString(),
     
                            };

                            // Aggiunta dell'oggetto Tracking alla lista
                            trackingList.Add(spedizione);
                        }
                    }
                }
            }

            // Restituzione della vista con la lista di tracciamenti
            return View(trackingList);
        }
    }
}