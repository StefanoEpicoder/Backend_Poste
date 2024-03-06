using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Poste.Models
{
    public class Users
    {
        [Required(ErrorMessage = "Inserisci Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Inserisci una Password")]
        public string Password { get; set; }

    }
}