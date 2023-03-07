using System.ComponentModel.DataAnnotations;

namespace E6K_NetAng_GestContact.Api.Models.Forms
{
#nullable disable
    public class ContactDto
    {
        [Required]
        [MaxLength(50)]
        [RegularExpression(@"\A[^\s](\w|\s|'|-){0,49}\w\Z")]
        public string Nom { get; set; }
        [Required]
        [MaxLength(50)]
        [RegularExpression(@"\A[^\s](\w|\s|'|-){0,49}\w\Z")]
        public string Prenom { get; set; }
    }
}
