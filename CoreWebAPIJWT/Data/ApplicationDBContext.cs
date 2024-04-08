using CoreWebAPIJWT.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreWebAPIJWT.Data
{
    public class ApplicationDBContext:DbContext
    {   //all db changes we can do
        //Adding db context class that access the data from database
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) 
        {
        
        } 
        public DbSet<Villa> Villas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
               new Villa()
               {
                   Id = 1,
                   Name = "Royal Villa",
                   Details = "Focus 11 tincidunt maximus leo and sed scelerious .Beautiful Desinged Created thats has very Old strcture.",
                   ImageUrl = "https://unsplash.com/photos/a-laptop-and-a-potted-plant-IqBY9blj8Ks",
                   Occupancy = 5,
                   Rate = 500,
                   Sqft = 550,
                   Amenity = "",
                   CreatedDate = DateTime.Now
               },


                new Villa()
                {
                    Id = 2,
                    Name = "Royal Pool",
                    Details = "Beautiful Desinged Created thats has very Old strcture.",
                    ImageUrl = "https://unsplash.com/photos/macbook-pro-on-top-of-brown-table-1SAnrIxw5OY",
                    Occupancy = 3,
                    Rate = 450,
                    Sqft = 500,
                    Amenity = "",
                    CreatedDate= DateTime.Now
                },
                 new Villa()
                 {
                     Id = 3,
                     Name = "Royal split Villa",
                     Details = "Beautiful Desinged Created thats has very Old strcture.",
                     ImageUrl = "https://unsplash.com/photos/a-woman-sitting-on-a-couch-using-a-laptop-computer-IhO7j8qEaVc",
                     Occupancy = 6,
                     Rate = 650,
                     Sqft = 1150,
                     Amenity = "",
                     CreatedDate = DateTime.Now
                 },
                  new Villa()
                  {
                      Id = 4,
                      Name = "Diamond Villa",
                      Details = "Beautiful Desinged Created thats has very Old strcture.",
                      ImageUrl = "https://unsplash.com/photos/apple-macbook-beside-computer-mouse-on-table-9l_326FISzk",
                      Occupancy = 5,
                      Rate = 400,
                      Sqft = 350,
                      Amenity = "",
                      CreatedDate = DateTime.Now
                  },

                   new Villa()
                   {
                       Id = 5,
                       Name = "Diamond Pool Villa",
                       Details = "Beautiful Desinged Created thats has very Old strcture.",
                       ImageUrl = "https://unsplash.com/photos/a-laptop-and-a-potted-plant-IqBY9blj8Ks",
                       Occupancy = 2,
                       Rate = 500,
                       Sqft = 550,
                       Amenity = "",
                       CreatedDate=DateTime.Now
                   }

               );
        }
    }
}
