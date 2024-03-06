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
    
    public class AggiornamentiController : Controller
    {
        public ActionResult Index()
        {
            // Creazione di una lista di oggetti Aggiornamenti
            List<Aggiornamenti> aggiornamentiList = new List<Aggiornamenti>();

            // Recupero della stringa di connessione dal file di configurazione
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString;

            // Creazione di una nuova connessione al database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Query per selezionare tutti i record dalla tabella Aggiornamenti
                string query = "select * from Aggiornamenti";

                // Creazione di un nuovo comando SQL con la query e la connessione
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Esecuzione del comando e recupero dei dati tramite un reader
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Iterazione sui record restituiti dal reader
                        while (reader.Read())
                        {
                            // Creazione di un nuovo oggetto Aggiornamenti e popolamento dei suoi attributi
                            Aggiornamenti aggiornamenti = new Aggiornamenti
                            {
                                SpedizioneID = (int)reader["SpedizioneID"],
                                Satus = reader["Satus"].ToString(),
                                Luogo = reader["Luogo"].ToString(),
                                Descrizione = reader["Descrizione"].ToString(),
                                DataEOra = reader["DataEOra"].ToString(),
                                CodiceTasseCliente = reader["CodiceTasseCliente"].ToString(),
                            };

                            // Aggiunta dell'oggetto Aggiornamenti alla lista
                            aggiornamentiList.Add(aggiornamenti);
                        }
                    }
                }
            }

            // Restituzione della vista Index con la lista di Aggiornamenti
            return View(aggiornamentiList);
        }

        [HttpGet]
        public ActionResult Aggiorna()
        {
            // Restituzione della vista Aggiorna
            return View();
        }

        [HttpPost]
        public ActionResult Aggiorna(Aggiornamenti newAggiornamento)
        {
            // Recupero della stringa di connessione dal file di configurazione
            string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionstring);

            // Verifica della validità del modello
            if (ModelState.IsValid)
            {
                // Aggiunta del nuovo Aggiornamenti alla lista statica
                Aggiornamenti.ListaAggiornamenti.Add(newAggiornamento);

                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;

                    // Comando SQL per l'inserimento del nuovo Aggiornamenti nel database
                    cmd.CommandText = "insert into Aggiornamenti values (@SpedizioneID, @Satus, @Luogo, @Descrizione, @DataEOra, @CodiceTasseCliente)";

                    // Aggiunta dei parametri al comando SQL
                    cmd.Parameters.AddWithValue("SpedizioneID", newAggiornamento.SpedizioneID);
                    cmd.Parameters.AddWithValue("Satus", newAggiornamento.Satus);
                    cmd.Parameters.AddWithValue("Luogo", newAggiornamento.Luogo);
                    cmd.Parameters.AddWithValue("Descrizione", newAggiornamento.Descrizione);
                    cmd.Parameters.AddWithValue("DataEOra", newAggiornamento.DataEOra);
                    cmd.Parameters.AddWithValue("CodiceTasseCliente", newAggiornamento.CodiceTasseCliente);

                    // Esecuzione del comando SQL e recupero del numero di righe inserite
                    int insertedSuccessfully = cmd.ExecuteNonQuery();

                    if (insertedSuccessfully > 0)
                    {
                        // Stampa del messaggio di conferma
                        Response.Write("Inserito nel database!");
                    }
                }
                catch (Exception ex)
                {
                    // Stampa dell'errore in caso di eccezione
                    Response.Write("Errore: " + ex.Message);
                }
                finally
                {
                    // Chiusura della connessione al database
                    conn.Close();
                }

                // Reindirizzamento alla vista Index del controller Aggiornamenti
                return RedirectToAction("Index", "Aggiornamenti");
            }
            else
            {
                // Restituzione della vista Aggiorna in caso di modello non valido
                return View();
            }
        }
        [HttpPost]
        public ActionResult EliminaAggiornamento(int spedizioneID)
        {
            // Recupero della stringa di connessione dal file di configurazione
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString;

            // Creazione di una nuova connessione al database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Comando SQL per l'eliminazione dell'aggiornamento con l'ID specificato
                string query = "delete from Aggiornamenti where SpedizioneID = @SpedizioneID";

                // Creazione di un nuovo comando SQL con la query e la connessione
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Aggiunta del parametro al comando SQL
                    cmd.Parameters.AddWithValue("SpedizioneID", spedizioneID);

                    // Esecuzione del comando SQL e recupero del numero di righe eliminate
                    int deletedSuccessfully = cmd.ExecuteNonQuery();

                    if (deletedSuccessfully > 0)
                    {
                        // Stampa del messaggio di conferma
                        Response.Write("Aggiornamento eliminato!");
                    }
                }
            }

            // Reindirizzamento alla vista Index del controller Aggiornamenti
            return RedirectToAction("Index", "Aggiornamenti");
        }

    }
}