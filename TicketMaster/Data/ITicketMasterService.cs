using TicketMaster.Objects;

namespace TicketMaster.Data
{
    public interface ITicketMasterService
    {
        public List<Movie> FetchMoviesBetween(int first, int last);
    }
}