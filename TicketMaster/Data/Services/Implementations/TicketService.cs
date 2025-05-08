using Microsoft.EntityFrameworkCore;
using TicketMaster.Data.DTOs;
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

        public async Task CreateTicket(TicketDTO ticket)
        {
            try
            {
                Ticket newTicket = new Ticket()
                {
                    Price = ticket.Price,
                    Seat = ticket.Seat,
                    Status = ticket.Status,
                    PurchaseTime = ticket.PurchaseTime
                };
                newTicket.OfScreening = await _context.Screenings.Where(o => o.Id == ticket.OfScreeningId).FirstAsync();
                newTicket.Customer = await _context.CustomerData.Where(o => o.Id == ticket.CustomerId).FirstAsync();
                newTicket.ByVendor = await _context.Vendors.Where(o => o.Id == ticket.ByVendorId).FirstAsync();
                await _context.Tickets.AddAsync(newTicket);
                await _context.SaveChangesAsync();
                Console.WriteLine($"New ticket created succesfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task DeleteTicket(int id)
        {
            try
            {
                var todelete = await _context.Tickets.Where(o => o.Id == id).FirstAsync();
                if (todelete != null)
                {
                    _context.Tickets.Remove(todelete);
                    await _context.SaveChangesAsync();
                    Console.WriteLine($"Ticket({id}) deleted succesfully");
                }
            } catch (Exception e) 
            {
                Console.WriteLine($"Ticket ({id}) not found");
            }
        }
        public async Task UpdateTicket(TicketDTO ticket)
        {
            try
            {
                var toUpdate = await _context.Tickets.Where(o => o.Id == ticket.Id).FirstAsync();
                Ticket updatedTicket = new Ticket()
                {
                    Id = ticket.Id,
                    Price = ticket.Price,
                    Seat = ticket.Seat,
                    Status = ticket.Status,
                    PurchaseTime = ticket.PurchaseTime
                };
                updatedTicket.OfScreening = await _context.Screenings.Where(o => o.Id == ticket.OfScreeningId).FirstAsync();
                updatedTicket.Customer = await _context.CustomerData.Where(o => o.Id == ticket.CustomerId).FirstAsync();
                updatedTicket.ByVendor = await _context.Vendors.Where(o => o.Id == ticket.ByVendorId).FirstAsync();
               
                _context.Tickets.Remove(updatedTicket);
                await _context.SaveChangesAsync();
                await _context.Tickets.AddAsync(updatedTicket);
                await _context.SaveChangesAsync();
               
                Console.WriteLine($"Ticket ({ticket.Id}) updated succesfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ticket ({ticket.Id}) not found");
            }
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
