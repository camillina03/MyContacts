using System.ComponentModel.DataAnnotations;
namespace MyContacts.Entities
{
    public class Città
    {
        [Required]
        public required string Nome { get; set; }
    }
}
