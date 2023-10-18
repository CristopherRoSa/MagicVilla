using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Models.DTO
{
    public class VillaNumberCreateDTO
    {
        [Required]
        public int villaNum { get; set; }
        [Required]
        public int villaID { get; set; }
        public string specialDetail { get; set; }
    }
}
