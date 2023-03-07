using System.ComponentModel.DataAnnotations;

namespace E6K_NetAng_GestContact.Api.Models.DTO
{
#nullable disable
    public class FullNameValue
    {
        [Required]
        [MaxLength(50)]
        [RegularExpression(@"\A[^\s](\w|\s|'|-){0,49}\w\Z")]
        public string Value { get; set; }
    }
}
