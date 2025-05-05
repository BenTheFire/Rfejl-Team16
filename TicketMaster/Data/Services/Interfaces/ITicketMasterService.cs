using TicketMaster.Objects;

namespace TicketMaster.Data.Services.Interfaces
{
    public interface ITicketMasterService
    {
        public Task<List<Movie>> FetchMoviesBetweenAsync(int first, int last);
        public Task FetchMoviesDataAsync();
    }
}