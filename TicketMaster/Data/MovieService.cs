using Microsoft.EntityFrameworkCore;
using TicketMaster.Objects;

namespace TicketMaster.Data
{
    public class MovieService : IMovieService
    {
        private TicketmasterContext _context = new();

        public async Task<MovieWithCast> FetchMovieDataAsync(string imdbId)
        {
            int imdbIdInt = Convert.ToInt32(imdbId);

            var result = await (
                from movie in _context.Movies
                join credit in _context.Credits on movie equals credit.OfMovie
                join person in _context.People on credit.WhoIs equals person
                where movie.ImdbId == imdbIdInt
                select new
                {
                    Movie = movie,
                    Person = person,
                    credit.Role
                }).ToListAsync();

            var cast = result
                .GroupBy(x => x.Movie)
                .Select(g => (
                    movie: g.Key,
                    cast: g.Select(x => (x.Person, x.Role)).ToList()
                ))
                .First();

            return new(cast);

        }
    }
}
