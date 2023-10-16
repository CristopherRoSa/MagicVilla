using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_API.Models
{
    public class Villa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string name { get; set; }
        public DateTime creationDate { get; set; }        
        public string detail { get; set; }
        [Required]
        public double fee { get; set; }
        public int occupants { get; set; }
        public int squareMeter { get; set; }
        public string urlImage { get; set; }
        public string amenity{ get; set; }
        public DateTime updateDate { get; set; }

    }
}


