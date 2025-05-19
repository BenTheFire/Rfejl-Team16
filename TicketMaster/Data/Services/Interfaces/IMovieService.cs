using TicketMaster.Data.DTOs;
using TicketMaster.Objects;

namespace TicketMaster.Data.Services.Interfaces
{
    public interface IMovieService
    {
        public Task<MovieWithCast> FetchMovieDataAsync(string imdbId);
        public Task<List<Screening>> FetchScreenings(string imdbId);
        public Task CreateMovie(Movie movie);
        public Task DeleteMovie(int id);
        public Task UpdateMovie(Movie movie);
    }
}
