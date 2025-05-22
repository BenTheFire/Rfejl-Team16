using Ticketmaster.Data.DTOs;
using Ticketmaster.Objects;

namespace Ticketmaster.Data.Services.Interfaces
{
    public interface IScreeningService
    {
        public Task<ScreeningWithTicketsDTO> FetchTicketsForScreening(Screening screening);
        public Task<List<Screening>> GetScreeningsAsync();
        public Task<Screening> GetScreeningByIdAsync(int id);
        public Task<List<Screening>> GetScreeningsByVendorIdAsync(string vendorId);
        public Task<List<Screening>> FetchScreeningsByLocation(Location location);
        //public Task<List<Screening>> FetchScreeningsByVendor(Vendor vendor);
        public bool IsOngoing(Screening screening);
        public Task<List<Screening>> FetchScreeningsByMovie(Movie movie);
        public Task CreateScreening(Screening screening);
        public Task UpdateScreening(Screening screening);
        public Task DeleteScreening(int id);
    }
}
