using MagicVilla_API.Models.DTO;

namespace MagicVilla_API.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> villaList = new List<VillaDTO>
        {
            new VillaDTO { id = 1, name = "Vista a la piscina", occupants = 3, squareMeter = 50},
            new VillaDTO { id = 2, name = "Vista a la playa", occupants = 4, squareMeter = 80}
        };
    }
}
