using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace MagicVilla_API.Models.DTO
{
    public class VillaCreateDTO
    {
        [Required]
        [MaxLength(30)]
        public string name { get; set; }
        public string detail { get; set; }
        public double fee { get; set; }
        public int occupants { get; set; }
        public int squareMeter { get; set; }
        public string urlImage { get; set; }
        public string amenity { get; set; }


    }
}
