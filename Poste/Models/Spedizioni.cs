using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Poste.Models
{
    public class Spedizioni
    {

        public int IdSpedizione { get; set; }

        [Required(ErrorMessage = "La data deve essere nel formato: AAAA-MM-GG")]
        public string DataSpedizione { get; set; }

        [Required(ErrorMessage = "Inserisci un peso maggiore o uguale a 0")]
        public int Peso { get; set; }

        [Required(ErrorMessage = "Inserisci una città")]
        public string CittaDestinazione { get; set; }

        [Required(ErrorMessage = "Inserisci un indirizzo")]
        public string Indirizzo { get; set; }

        [Required(ErrorMessage = "Inserisci il destinatario del pacco")]
        public string Destinatario { get; set; }

        [Required(ErrorMessage = "ID cliente non valido")]
        public int ClienteID { get; set; }

        [Required(ErrorMessage = "La data deve essere nel formato: AAAA-MM-GG")]
        public string ConsegnaPrevista { get; set; }

        public static List<Spedizioni> ListaSpedizioni { get; set; } = new List<Spedizioni>();
    }
}