using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Repositories
{
    public class RatingRepository : IRatingRepository
    {

        private readonly ApiDBContext _apiDbContext;
        public RatingRepository(ApiDBContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }
        public async Task<Rating> AddRating(Rating newRating)
        {
            await _apiDbContext.Ratings.AddAsync(newRating);
            await _apiDbContext.SaveChangesAsync();
            return newRating;
        }
    }
}
