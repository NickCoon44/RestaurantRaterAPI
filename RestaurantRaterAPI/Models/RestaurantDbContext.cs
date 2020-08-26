using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RestaurantRaterAPI.Models
{
    public class RestaurantDbContext : DbContext
    {
                                             // Name is from Web config for connection string
        public RestaurantDbContext() : base("DefaultConnection") { }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}