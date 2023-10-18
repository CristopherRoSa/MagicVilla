using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_API.Models
{
    public class VillaNumber
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int villaNum { get; set; }

        [Required]
        public int villaID { get; set; }

        [ForeignKey("villaID")]
        public Villa villa { get; set; }

        public string specialDetail { get; set; }

        public DateTime creationDate { get; set; }

        public DateTime updateDate { get; set; }
    }
}
