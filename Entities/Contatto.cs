using System;
using System.ComponentModel.DataAnnotations;

namespace MyContacts.Entities
{
    public class Contatto
    {
        //public int Id { get; set; }

        [Required]
        public required string Nome { get; set; }

        [Required]
        public required string Cognome { get; set; }

        [Required]
        public required string Sesso { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        public DateOnly? DataNascita { get; set; }

        public string? NumeroTelefono { get; set; }

        public string? Citta { get; set; }
    }
}
