using TicketMaster.Objects;

namespace TicketMaster.Data
{
    public interface IMovieService
    {
        public Task<(Movie movie, List<(Person person, string role)> cast)> FetchMovieDataAsync(string imdbId);
    }
}
