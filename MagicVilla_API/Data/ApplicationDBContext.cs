using MagicVilla_API.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Data
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options): base(options)
        {

        }
        public DbSet<Villa> Villas { get; set; }
        public DbSet<VillaNumber> VillaNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    id = 1,
                    name = "Villa Real",
                    detail = "Detalle de la villa...",
                    urlImage = "",
                    occupants = 5,
                    squareMeter = 50,
                    fee = 200,
                    amenity = "",
                    creationDate = DateTime.Now,
                    updateDate = DateTime.Now
                },
                new Villa()
                {
                    id = 2,
                    name = "Premium vista a la piscina",
                    detail = "Detalle de la villa...",
                    urlImage = "",
                    occupants = 4,
                    squareMeter = 40,
                    fee = 150,
                    amenity = "",
                    creationDate = DateTime.Now,
                    updateDate = DateTime.Now
                }
            );
        }

    }
}
