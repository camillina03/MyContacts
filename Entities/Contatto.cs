using System;
using System.ComponentModel.DataAnnotations;

namespace MyContacts.Entities
{
    public class Contatto
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Cognome { get; set; }

        [Required]
        public string Sesso { get; set; }

        public DateOnly? DataNascita { get; set; }


        public string NumeroTelefono { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Citta { get; set; }
    }
}
