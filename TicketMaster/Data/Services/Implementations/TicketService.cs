using TicketMaster.Data.Services.Interfaces;
using TicketMaster.Objects;

namespace TicketMaster.Data.Services.Implementations
{
    public class TicketService : ITicketService
    {
        private readonly TicketmasterContext _context;
        public TicketService(TicketmasterContext c)
        {
            _context = c;
        }

        public List<Ticket> FetchOwnedTickets(string username)
        {
            var ret = (
                from u in _context.Users
                join c in _context.CustomerData on u equals c.OfUser
                join t in _context.Tickets on c equals t.Customer
                where u.Username == username && t.Status == 1 // approved
                orderby t.OfScreening.Time ascending
                select t).ToList();
            return ret;
        }

        public List<Ticket> FetchPendingTickets(string vendorname)
        {
            var ret = (
                from v in _context.Vendors
                join t in _context.Tickets on v equals t.ByVendor
                where v.Username == vendorname && t.Status == 0 // pending
                select t).ToList();
            return ret;
        }

        public List<Ticket> FetchPurchases(string username)
        {
            var ret = (
                from u in _context.Users
                join c in _context.CustomerData on u equals c.OfUser
                join t in _context.Tickets on c equals t.Customer
                where u.Username == username
                orderby t.PurchaseTime descending
                select t).ToList();
            return ret;
        }
    }
}
