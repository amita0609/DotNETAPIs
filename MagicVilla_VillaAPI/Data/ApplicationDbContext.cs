using MagicVilla_VillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Models.Villa> Villas { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Name = "Royal Villa",
                    Details = "The Quattrocento villa gardens were treated as a fundamental and aesthetic " +
                             "link between a residential building and the outdoors,",
                    ImageUrl = "https://dotnetmasteryimages.blab.core.windows.net/search/bluevillaimages/villa1.jpg",
                    Occupancy = 4,
                    Rate = 200,
                    Sqft = 550,
                    Amenity = "",
                    CreateDate = DateTime.Now
                },
                new Villa()
                {
                    Id = 2,
                    Name = "Premium Pool Villa",
                    Details = "The Quattrocento villa gardens were treated as a fundamental and aesthetic " +
                             "link between a residential building and the outdoors,",
                    ImageUrl = "https://dotnetmasteryimages.blab.core.windows.net/search/bluevillaimages/villa2.jpg",
                    Occupancy = 4,
                    Rate = 200,
                    Sqft = 550,
                    Amenity = "",
                    CreateDate = DateTime.Now
                },
                new Villa()
                {
                    Id = 3,
                    Name = "Luxury Pool Villa",
                    Details = "The Quattrocento villa gardens were treated as a fundamental and aesthetic " +
                             "link between a residential building and the outdoors,",
                    ImageUrl = "https://dotnetmasteryimages.blab.core.windows.net/search/bluevillaimages/villa3.jpg",
                    Occupancy = 4,
                    Rate = 300,
                    Sqft = 550,
                    Amenity = "",
                    CreateDate = DateTime.Now
                },
                new Villa()
                {
                    Id = 4,
                    Name = "Diamond villa",
                    Details = "The Quattrocento villa gardens were treated as a fundamental and aesthetic " +
                             "link between a residential building and the outdoors,",
                    ImageUrl = "https://dotnetmasteryimages.blab.core.windows.net/search/bluevillaimages/villa4.jpg",
                    Occupancy = 4,
                    Rate = 400,
                    Sqft = 750,
                    Amenity = "",
                    CreateDate=DateTime.Now
                },
                new Villa()
                {
                    Id = 5,
                    Name = "Diamond Pool villa",
                    Details = "The Quattrocento villa gardens were treated as a fundamental and aesthetic " +
                             "link between a residential building and the outdoors,",
                    ImageUrl = "https://dotnetmasteryimages.blab.core.windows.net/search/bluevillaimages/villa5.jpg",
                    Occupancy = 4,
                    Rate = 600,
                    Sqft = 990,
                    Amenity = "",
                    CreateDate = DateTime.Now

                }
            );

        }

    }
    
}
