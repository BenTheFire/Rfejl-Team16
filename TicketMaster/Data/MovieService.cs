using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using TicketMaster.Objects;

namespace TicketMaster.Data
{
    public class MovieService : IMovieService
    {
        private TicketmasterContext _context = new();

        public async Task<(Movie movie, List<(Person person, string role)> cast)> FetchMovieDataAsync(string imdbId)
        {
            int imdbIdInt = Convert.ToInt32(imdbId);

            var result = await (
                from movie in _context.Movies
                join credit in _context.Credits on movie equals credit.OfMovie
                join person in _context.People on credit.WhoIs equals person
                group new { person, credit.Role } by credit.OfMovie into gr
                where gr.Key.ImdbId == imdbIdInt
                select new
                {
                    Cast = gr.ToList(),
                    Movie = gr.Key
                }).FirstAsync();

            (Movie movie, List<(Person person, string role)> cast) resultConverted = (result.Movie, new());

            foreach (var casting in result.Cast)
            {
                resultConverted.cast.Add((casting.person, casting.Role));
            }

            return resultConverted;

        }
    }
}
