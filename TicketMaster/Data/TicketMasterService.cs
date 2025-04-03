using Microsoft.EntityFrameworkCore;
using TicketMaster.Objects;

namespace TicketMaster.Data
{
    public class TicketMasterService
    {
        private TicketmasterContext _context = new();
        public List<Movie> FetchMoviesBetween(int first, int last)
        {
            return _context.Movies
                    .Select(movie => movie)
                    .Where(movie => movie.Id < last && movie.Id >= first)
                    .ToList();
        }
    }
}
