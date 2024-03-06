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
    [Authorize]
    public class SpedizioniController : Controller
    {
        // Metodo per visualizzare l'elenco delle spedizioni
        public ActionResult Index()
        {
            // Creazione di una lista vuota di oggetti Spedizioni
            List<Spedizioni> spedizioniList = new List<Spedizioni>();

            // Recupero della stringa di connessione dal file di configurazione
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString;

            // Creazione di una connessione al database utilizzando la stringa di connessione
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Query per selezionare tutte le spedizioni dal database
                string query = "select * from Spedizioni";

                // Creazione di un comando SQL utilizzando la query e la connessione
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Esecuzione del comando e recupero dei dati tramite un lettore
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Iterazione sui risultati restituiti dal lettore
                        while (reader.Read())
                        {
                            // Creazione di un oggetto Spedizioni e popolamento delle sue proprietà
                            Spedizioni spedizione = new Spedizioni
                            {
                                IdSpedizione = (int)reader["IdSpedizione"],
                                DataSpedizione = reader["DataSpedizione"].ToString(),
                                Peso = (int)reader["Peso"],
                                CittaDestinazione = reader["CittaDestinazione"].ToString(),
                                Indirizzo = reader["Indirizzo"].ToString(),
                                Destinatario = reader["Destinatario"].ToString(),
                                ClienteID = (int)reader["ClienteID"],
                                ConsegnaPrevista = reader["ConsegnaPrevista"].ToString()
                            };

                            // Aggiunta dell'oggetto Spedizioni alla lista
                            spedizioniList.Add(spedizione);
                        }
                    }
                }
            }

            // Restituzione della vista con la lista delle spedizioni
            return View(spedizioniList);
        }

        // Metodo per registrare una nuova spedizione (GET)
        [HttpGet]
        public ActionResult RegisterPackage()
        {
            return View();
        }

        // Metodo per registrare una nuova spedizione (POST)
        [HttpPost]
        public ActionResult RegisterPackage(Spedizioni newSpedizione)
        {
            // Recupero della stringa di connessione dal file di configurazione
            string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionstring);

            // Verifica della validità del modello
            if (ModelState.IsValid)
            {
                // Aggiunta della nuova spedizione alla lista delle spedizioni
                Spedizioni.ListaSpedizioni.Add(newSpedizione);

                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;

                    // Comando SQL per inserire i dati della nuova spedizione nel database
                    cmd.CommandText = "insert into Spedizioni values (@DataSpedizione, @Peso, @CittaDestinazione, @Indirizzo, @Destinatario, @ClienteID, @ConsegnaPrevista)";

                    // Aggiunta dei parametri al comando SQL
                    cmd.Parameters.AddWithValue("DataSpedizione", newSpedizione.DataSpedizione);
                    cmd.Parameters.AddWithValue("Peso", newSpedizione.Peso);
                    cmd.Parameters.AddWithValue("CittaDestinazione", newSpedizione.CittaDestinazione);
                    cmd.Parameters.AddWithValue("Indirizzo", newSpedizione.Indirizzo);
                    cmd.Parameters.AddWithValue("Destinatario", newSpedizione.Destinatario);
                    cmd.Parameters.AddWithValue("ClienteID", newSpedizione.ClienteID);
                    cmd.Parameters.AddWithValue("ConsegnaPrevista", newSpedizione.ConsegnaPrevista);

                    // Esecuzione del comando e recupero del numero di righe inserite
                    int insertedSuccessfully = cmd.ExecuteNonQuery();

                    if (insertedSuccessfully > 0)
                    {
                        Response.Write("Inserito nel database!");
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("Errore: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }

                // Reindirizzamento alla pagina di visualizzazione delle spedizioni
                return RedirectToAction("Index", "Spedizioni");
            }
            else
            {
                return View();
            }
        }
        // Metodo per eliminare una spedizione
        [HttpPost]
        public ActionResult DeletePackage(int idSpedizione)
        {
            // Recupero della stringa di connessione dal file di configurazione
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString;

            // Creazione di una connessione al database utilizzando la stringa di connessione
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Comando SQL per eliminare la spedizione dal database
                string query = "delete from Spedizioni where IdSpedizione = @IdSpedizione";

                // Creazione di un comando SQL utilizzando la query e la connessione
                using (SqlCommand cmd1 = new SqlCommand(query, conn))
                {
                    // Aggiunta del parametro al comando SQL
                    cmd1.Parameters.AddWithValue("IdSpedizione", idSpedizione);

                    // Esecuzione del comando e recupero del numero di righe eliminate
                    int deletedSuccessfully = cmd1.ExecuteNonQuery();
                    string queryAggiornamenti = "delete from Aggiornamenti where SpedizioneID = @IdSpedizione";
                    using (SqlCommand cmd2 = new SqlCommand(queryAggiornamenti, conn))
                    {
                        cmd2.Parameters.AddWithValue("IdSpedizione", idSpedizione);
                        cmd2.ExecuteNonQuery();
                    }
                    if (deletedSuccessfully > 0)
                    {
                        Response.Write("Spedizione eliminata!");
                    }
                }
            }

            // Reindirizzamento alla pagina di visualizzazione delle spedizioni
            return RedirectToAction("Index", "Spedizioni");
        }

    }
}