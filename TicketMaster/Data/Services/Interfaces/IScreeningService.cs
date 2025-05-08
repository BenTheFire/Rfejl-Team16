using TicketMaster.Data.DTOs;
using TicketMaster.Objects;

namespace TicketMaster.Data.Services.Interfaces
{
    public interface IScreeningService
    {
        public Task<ScreeningWithTicketsDTO> FetchTicketsForScreening(Screening screening);
        public Task<List<Screening>> FetchScreenings();
        public Task<Screening> FetchScreening(int id);
        public Task<List<Screening>> FetchScreeningsByLocation(Location location);
        public Task<List<Screening>> FetchScreeningsByVendor(Vendor vendor);
        public Task CreateScreening(ScreeningDTO screening);
        public Task UpdateScreening(ScreeningDTO screening);
        public Task DeleteScreening(int id);
    }
}
