using Ticketmaster.Data.DTOs;
using Ticketmaster.Objects;

namespace Ticketmaster.Data.Services.Interfaces
{
    public interface IMovieService
    {
        public Task<MovieWithCast> FetchMovieDataAsync(string imdbId);
        public Task<List<Screening>> FetchScreenings(string imdbId);
        public Task<bool> IsInDBByTitle(string title);
        public Task<bool> IsInDBByImdbId(string imdbId);
        public Task CreateMovie(Movie movie);
        public Task DeleteMovieByTitle(string title);
        public Task DeleteMovieByImdbId(string imdbId);
        public Task UpdateMovie(Movie movie);
        public Task UpdateMovieFromOmdbByTitle(string title);
    }

    public class MovieDbException : Exception { }
}
