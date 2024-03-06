using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Poste.Models
{
    public class Aggiornamenti
    {

        [Required(ErrorMessage = "ID spedizione non valido")]
        public int SpedizioneID { get; set; }

        [Required(ErrorMessage = "Inserisci lo stato dell'ordine")]
        public string Satus { get; set; }

        [Required(ErrorMessage = "Inserisci la posizione attuale del pacchetto")]
        public string Luogo { get; set; }

        [Required(ErrorMessage = "Inserisci la descrizione")]
        public string Descrizione { get; set; }

        [Required(ErrorMessage = "Inserisci data e ora: AAAA-MM-GG HH-MM-SS")]
        public string DataEOra { get; set; }

        [Required(ErrorMessage = "Inserisci il CF o la P.Iva del cliente")]
        public string CodiceTasseCliente { get; set; }
        public static List<Aggiornamenti> ListaAggiornamenti { get; set; } = new List<Aggiornamenti>();
    }
}