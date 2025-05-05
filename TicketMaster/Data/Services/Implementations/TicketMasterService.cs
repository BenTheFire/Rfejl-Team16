using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TicketMaster.Data.Services.Interfaces;
using TicketMaster.Objects;

namespace TicketMaster.Data.Services.Implementations
{
    public class TicketMasterService : ITicketMasterService
    {
        private readonly TicketmasterContext _context;
        public TicketMasterService(TicketmasterContext context)
        {
            //needed constructor
            _context = context;
        }

        public async Task<List<Movie>> FetchMoviesBetweenAsync(int first, int last)
        {
            return await _context.Movies
                    .Where(movie => movie.Id < last && movie.Id >= first)
                    .ToListAsync();
        }
    }
}
