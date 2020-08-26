using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantRaterAPI.Models
{
    public class RatingDetail
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public double FoodScore { get; set; }
        public double EnvironmentScore { get; set; }
        public double CleanlinessScore { get; set; }
        public double AverageRating { get; set; }
    }
}