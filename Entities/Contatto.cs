using System.ComponentModel.DataAnnotations;

namespace MyContacts.Entities
{
    public class Contatto
    {
        [Required]
        public required string Nome { get; set; }

        [Required]
        public required string Cognome { get; set; }

        [Required]
        [EnumDataType(typeof(Gender))]
        public Gender Sesso { get; set; }

        [Key]
        [Required]
        [EmailAddress]
        public required string Mail { get; set; }

        public DateTime? DataDiNascita { get; set; }

        [ValidPhoneNumber(ErrorMessage = "Il formato del numero di telefono non è valido.")]
        public string? Telefono { get; set; }

        public string? Città { get; set; }
    }

    internal class ValidPhoneNumberAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return true; // Il campo è opzionale, quindi il valore nullo è accettabile
            }

            // Espressione regolare per un formato di numero di telefono (esempio)
            string phoneNumberPattern = "^[0-9]{5,15}$";

            if (!System.Text.RegularExpressions.Regex.IsMatch(value.ToString(), phoneNumberPattern))
            {
                return false; // Non corrisponde al formato del numero di telefono
            }

            return true;
        }
    }
}
