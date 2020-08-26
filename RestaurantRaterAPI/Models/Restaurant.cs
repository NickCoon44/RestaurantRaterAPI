using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantRaterAPI.Models
{
    // Restaurant Entity (The Class that gets stored in the Database)
    public class Restaurant
    {
        // Primary Key
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Rating 
        {
            get
            {
                // Calculate a total average score based on Ratings
                double totalAverageRating = 0;

                // Add all Ratings together to get total Average Rating
                foreach(var rating in Ratings)
                {
                    totalAverageRating += rating.AverageRating;
                }

                // Return Average of Total if the count is above 0
                return (Ratings.Count > 0) ? totalAverageRating / Ratings.Count : 0;
            }
        
        }

        // Average Food Rating
        public double FoodRating
        {
            get
            {
                double totalFoodScore = 0;

                foreach (var rating in Ratings)
                {
                    totalFoodScore += rating.FoodScore;
                }

                return (Ratings.Count > 0) ? totalFoodScore / Ratings.Count : 0;
            }

        }

        // Average Environment Rating
        public double EnvironmentRating
        {
            get
            {
                // Another way of doing this.
                IEnumerable<double> scores = Ratings.Select(rating => rating.EnvironmentScore);
                double totalEnvironmentScore = scores.Sum();
                return (Ratings.Count > 0) ? totalEnvironmentScore / Ratings.Count : 0;
            }

        }

        // Average Cleanliness Rating
        public double CleanlinessRating
        {
            get
            {
                IEnumerable<double> scores = Ratings.Select(rating => rating.CleanlinessScore);
                double totalCleanlinessScore = scores.Sum();
                return (Ratings.Count > 0) ? totalCleanlinessScore / Ratings.Count : 0;
            }

        }


        //Alt: public bool IsRecommended => Rating > 3.5;
        public bool IsRecommended
        {
            get
            {
                return Rating >= 8.5;
            }
        }

        // All of the associated Rating objects from the database
        // based on the Foreign Key Relationship
        public virtual List<Rating> Ratings { get; set; } = new List<Rating>();
    }
}