using Microsoft.EntityFrameworkCore;
using TicketMaster.Data.DTOs;
using TicketMaster.Data.Services.Interfaces;
using TicketMaster.Objects;

namespace TicketMaster.Data.Services.Implementations
{
    public class MovieService : IMovieService
    {
        private TicketmasterContext _context;

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
