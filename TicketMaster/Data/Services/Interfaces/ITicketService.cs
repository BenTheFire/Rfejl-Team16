using TicketMaster.Objects;

namespace TicketMaster.Data.Services.Interfaces
{
    public interface ITicketService
    {
        public List<Ticket> FetchPurchases(string username);
        public List<Ticket> FetchPendingTickets(string vendorUsername);
        public List<Ticket> FetchOwnedTickets(string username);
    }
}
