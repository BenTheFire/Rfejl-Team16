using TicketMaster.Data.DTOs;
using TicketMaster.Objects;

namespace TicketMaster.Data.Services.Interfaces
{
    public interface IMovieService
    {
        public Task<MovieWithCast> FetchMovieDataAsync(string imdbId);
        Task<List<Screening>> FetchScreenings(string imdbId);
    }
}
