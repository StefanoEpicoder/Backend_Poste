using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Poste.Models
{
    [Authorize(Roles = "Admin")]
    public class Clienti
    {

        public int IdCliente { get; set; }

        [Required(ErrorMessage = "Inserisci il nome del cliente")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Inserisci il CF o la P.Iva")]
        public string CodiceTasse { get; set; }

        [Required(ErrorMessage = "Inserisci l'indirizzo del cliente")]
        public string Indirizzo { get; set; }

        public static List<Clienti> ListClienti { get; set; } = new List<Clienti>();
    }
}