using Ticketmaster.Objects;
using Ticketmaster.Data.DTOs;

namespace Ticketmaster.Data.Services.Interfaces
{
    public interface ILocationService
    {
        public Task CreateLocationAsync(Location location);
        public Task DeleteLocationAsync(int id);
        public Task UpdateLocationAsync(Location location);
        public Task<List<Location>> GetLocationsAsync();
        public Task<Location> GetLocationByIdAsync(int id);
        public Task<List<Location>> GetLocationsByVendorIdAsync(string vendorId);
    }
}
