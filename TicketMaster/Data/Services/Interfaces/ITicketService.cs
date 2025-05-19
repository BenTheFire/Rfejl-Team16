using Ticketmaster.Objects;
using Ticketmaster.Data.DTOs;

namespace Ticketmaster.Data.Services.Interfaces
{
    public interface ITicketService
    {
        public List<Ticket> FetchPurchases(string username);
        public List<Ticket> FetchPendingTickets(string vendorUsername);
        public List<Ticket> FetchOwnedTickets(string username);
        public Task CreateTicket(TicketDTO ticket);
        public Task UpdateTicket(TicketDTO ticket);
        public Task DeleteTicket(int id);
    }
}
