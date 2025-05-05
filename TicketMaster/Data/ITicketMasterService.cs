using TicketMaster.Objects;

namespace TicketMaster.Data
{
    public interface ITicketMasterService
    {
        public Task<List<Movie>> FetchMoviesBetweenAsync(int first, int last);
    }
}