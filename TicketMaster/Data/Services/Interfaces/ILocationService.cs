using Ticketmaster.Objects;
using Ticketmaster.Data.DTOs;

namespace Ticketmaster.Data.Services.Interfaces
{
    public interface ILocationService
    {
        //public Task CreateLocation(LocationDTO location);
        public Task DeleteLocation(int id);
        //public Task UpdateLocation(LocationDTO location);
    }
}
