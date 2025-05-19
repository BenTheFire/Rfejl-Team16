using Ticketmaster.Objects;

namespace Ticketmaster.Data.Services.Interfaces
{
    public interface ITicketMasterService
    {
        public Task<List<Movie>> FetchMoviesBetweenAsync(int first, int last);
        public Task UpdateImagesAndDateAsync();
    }
}