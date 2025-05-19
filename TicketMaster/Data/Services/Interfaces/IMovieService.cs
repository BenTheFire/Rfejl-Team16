using Ticketmaster.Data.DTOs;
using Ticketmaster.Objects;

namespace Ticketmaster.Data.Services.Interfaces
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
