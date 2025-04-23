using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TicketMaster.Objects;

namespace TicketMaster.Data
{
    public class MovieService : IMovieService
    {
        TicketmasterContext _context = new();

        public async Task<Movie> FetchMovieDataAsync(string imdbId)
        {
            int imdbIdInt = Convert.ToInt32(imdbId);
            return await _context.Movies
                .Select(movie => movie)
                .Where(movie => movie.ImdbId == imdbIdInt)
                .FirstAsync();
        }
    }
}
