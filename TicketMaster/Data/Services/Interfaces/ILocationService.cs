using TicketMaster.Objects;
using TicketMaster.Data.DTOs;

namespace TicketMaster.Data.Services.Interfaces
{
    public interface ILocationService
    {
        public Task CreateLocation(LocationDTO location);
        public Task DeleteLocation(int id);
        public Task UpdateLocation(LocationDTO location);
    }
}
