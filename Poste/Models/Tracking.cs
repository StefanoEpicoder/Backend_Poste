using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Poste.Models
{
    [Authorize(Roles = "Admin")]
    public class Tracking
    {
        public int IdSpedizione { get; set; }

        public string CodiceTasse { get; set; }
        public string IdTasse { get; internal set; }
        public int IdCliente { get; internal set; }
        public string Importo { get; internal set; }
        public string NomeCliente { get; internal set; }

        // Remove the implicit operator
    }
}