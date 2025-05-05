using TicketMaster.Objects;

namespace TicketMaster.Data
{
    public interface IMovieService
    {
        public Task<MovieWithCast> FetchMovieDataAsync(string imdbId);
    }
}
