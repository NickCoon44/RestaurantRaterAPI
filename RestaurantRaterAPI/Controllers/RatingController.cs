using RestaurantRaterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRaterAPI.Controllers
{
    public class RatingController : ApiController
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext();
        // Create new ratings
        [HttpPost]
        public async Task<IHttpActionResult> CreateRating(Rating model)
        {
            // Check to see if the model is NOT valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Find the targeted Restaurant
            var restaurant = await _context.Restaurants.FindAsync(model.RestaurantId);
            if (restaurant == null)
            {
                return BadRequest($"The target restaurant with the ID of {model.RestaurantId} does not exist.");
            }

            // The restaurant isn't null, so we can successfully rate it.
            _context.Ratings.Add(model);
            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok($"You rated {restaurant.Name} successfully!");
            }

            return InternalServerError();
        }

        // Get a rating by its ID ?
        [HttpGet]
        public async Task<IHttpActionResult> GetByID(int id)
        {
            Rating rating = await _context.Ratings.FindAsync(id);
            if (rating != null)
            {
                //var model = (Rating)_context.Entry(rating).CurrentValues.ToObject();
                var model = new RatingDetail
                {
                    Id = rating.Id,
                    RestaurantId = rating.RestaurantId,
                    FoodScore = rating.FoodScore,
                    EnvironmentScore = rating.EnvironmentScore,
                    CleanlinessScore = rating.CleanlinessScore,
                    AverageRating = rating.AverageRating

                };

                return Ok(model);
            }
            return NotFound();
        }

        // Get ALL Ratings for a specific restaurant (just copied from Restaurant Controller)
        [Route("api/Rating/GetAll/{id}")]
        public async Task<IHttpActionResult> GetAllByRestaurantId(int id)
        {
            {
                Restaurant restaurant = await _context.Restaurants.FindAsync(id);

                if (restaurant != null)
                {
                    return Ok(restaurant);
                }
                return NotFound();
            }
        }

        // Update Rating
        [HttpPut]
        public async Task<IHttpActionResult> UpdateRating([FromUri] int id, [FromBody] Rating updatedRating)
        {
            if (ModelState.IsValid)
            {
                Rating rating = await _context.Ratings.FindAsync(id);

                if (rating != null)
                {
                    rating.FoodScore = updatedRating.FoodScore;
                    rating.EnvironmentScore = updatedRating.EnvironmentScore;
                    rating.CleanlinessScore = updatedRating.CleanlinessScore;

                    await _context.SaveChangesAsync();

                    return Ok();
                }

                return NotFound();
            }
            return BadRequest(ModelState);
        }

        // Delete Rating
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRatingById(int id)
        {
            Rating rating = await _context.Ratings.FindAsync(id);

            if (rating == null)
            {
                return NotFound();
            }

            _context.Ratings.Remove(rating);

            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok("The rating was deleted");
            }

            return InternalServerError();
        }
    }
}
