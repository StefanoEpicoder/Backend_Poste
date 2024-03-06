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
    public class ClientiController : Controller
    {
        // Metodo per visualizzare l'elenco dei clienti
        public ActionResult Index()
        {
            List<Clienti> clientiList = new List<Clienti>();

            // Recupera la stringa di connessione dal file di configurazione
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "select * from Clienti";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Crea un oggetto Clienti per ogni riga del risultato della query
                            Clienti cliente = new Clienti
                            {
                                IdCliente = (int)reader["IdCliente"],
                                Nome = reader["Nome"].ToString(),
                                CodiceTasse = reader["CodiceTasse"].ToString(),
                                Indirizzo = reader["Indirizzo"].ToString()
                            };

                            // Aggiungi il cliente alla lista dei clienti
                            clientiList.Add(cliente);
                        }
                    }
                }
            }

            // Restituisce la vista con l'elenco dei clienti
            return View(clientiList);
        }

        // Metodo per visualizzare il form di aggiunta di un nuovo cliente
        [HttpGet]
        public ActionResult AddClienti()
        {
            return View();
        }

        // Metodo per gestire la richiesta di aggiunta di un nuovo cliente
        [HttpPost]
        public ActionResult AddClienti(Clienti newCliente)
        {
            // Recupera la stringa di connessione dal file di configurazione
            string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionstring);

            if (ModelState.IsValid)
            {
                // Aggiungi il nuovo cliente alla lista dei clienti
                Clienti.ListClienti.Add(newCliente);

                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;

                    cmd.CommandText = "insert into Clienti values (@Nome, @CodiceTasse, @Indirizzo)";

                    // Imposta i parametri della query con i valori del nuovo cliente
                    cmd.Parameters.AddWithValue("Nome", newCliente.Nome);
                    cmd.Parameters.AddWithValue("CodiceTasse", newCliente.CodiceTasse);
                    cmd.Parameters.AddWithValue("Indirizzo", newCliente.Indirizzo);

                    // Esegue la query di inserimento
                    int insertedSuccessfully = cmd.ExecuteNonQuery();

                    if (insertedSuccessfully > 0)
                    {
                        // Stampa un messaggio di conferma
                        Response.Write("Inserito nel database!");
                    }

                }
                catch (Exception ex)
                {
                    // Stampa un messaggio di errore in caso di eccezione
                    Response.Write("Errore: " + ex.Message);
                }
                finally
                {
                    // Chiude la connessione al database
                    conn.Close();
                }

                // Reindirizza all'azione Index del controller Clienti
                return RedirectToAction("Index", "Clienti");
            }
            else
            {
                // Se il modello non è valido, restituisce la vista del form di aggiunta
                return View();
            }
        }
        // Metodo per eliminare un cliente
        [HttpPost]
        public ActionResult DeleteCliente(int idCliente)
        {
            // Recupera la stringa di connessione dal file di configurazione
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "delete from Aggiornamenti where SpedizioneID in (select SpedizioneID from Spedizioni where ClienteID = @IdCliente); delete from Spedizioni where ClienteID = @IdCliente; delete from Clienti where IdCliente = @IdCliente";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Imposta il parametro della query con l'ID del cliente da eliminare
                    cmd.Parameters.AddWithValue("IdCliente", idCliente);

                    // Esegue la query di eliminazione
                    int deletedSuccessfully = cmd.ExecuteNonQuery();

                    if (deletedSuccessfully > 0)
                    {
                        // Stampa un messaggio di conferma
                        Response.Write("Cliente eliminato!");
                    }
                }
            }

            // Reindirizza all'azione Index del controller Clienti
            return RedirectToAction("Index", "Clienti");
        }
    }
}