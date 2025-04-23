using TicketMaster.Objects;

namespace TicketMaster.Data
{
    public interface IMovieService
    {
        public Task<Movie> FetchMovieDataAsync(string imdbId);
    }
}
