using Ticketmaster.Objects;
using Ticketmaster.Data.DTOs;

namespace Ticketmaster.Data.Services.Interfaces
{
    public interface ITicketService
    {
        public Task<List<Ticket>> GetTicketsAsync();
        public Task<Ticket> GetTicketByIdAsync(int id);
        public Task<List<Ticket>> GetTicketsByUserIdAsync(string userId);
        public Task<List<Ticket>> GetTicketsByVendorIdAsync(string vendorId);
        public List<Ticket> FetchPurchases(string username);
        //public List<Ticket> FetchPendingTickets(string vendorUsername);
        public List<Ticket> FetchOwnedTickets(string username);
        public Task CreateTicketAsync(Ticket ticket);
        public Task UpdateTicketAsync(Ticket ticket);
        public Task DeleteTicketAsync(int id);
    }
}
