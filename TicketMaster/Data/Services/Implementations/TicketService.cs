using Microsoft.EntityFrameworkCore;
using Ticketmaster.Data.DTOs;
using Ticketmaster.Data.Services.Interfaces;
using Ticketmaster.Objects;

namespace Ticketmaster.Data.Services.Implementations
{
    public class TicketService : ITicketService
    {
        private readonly TicketmasterContext _context;
        public TicketService(TicketmasterContext c)
        {
            _context = c;
        }

        public async Task CreateTicketAsync(Ticket ticket)
        {
            var updateScreening = await _context.Screenings.Where(o => o.Id == ticket.OfScreening.Id).FirstAsync();
            if (++updateScreening.SeatsTaken <= updateScreening.InLocation.Capacity)
            {
                await _context.Tickets.AddAsync(ticket);
                await _context.SaveChangesAsync();
                Console.WriteLine($"Ticket added succesfully");
            }
            else
            {
                Console.WriteLine($"Ticket not added, screening is full");
            }            
        }

        public async Task<List<Ticket>> GetTicketsAsync()
        {
            return await _context.Tickets.ToListAsync();
        }
        
        //public async Task UpdateTicket(TicketDTO ticket)
        //{
        //    try
        //    {
        //        var toUpdate = await _context.Tickets.Where(o => o.Id == ticket.Id).FirstAsync();
        //        Ticket updatedTicket = new Ticket()
        //        {
        //            Id = (int)ticket.Id,
        //            Price = ticket.Price,
        //            Seat = ticket.Seat,
        //            Status = (int)ticket.Status,
        //            PurchaseTime = ticket.PurchaseTime
        //        };
        //        updatedTicket.OfScreening = await _context.Screenings.Where(o => o.Id == ticket.OfScreeningId).FirstAsync();
        //        updatedTicket.Customer = await _context.CustomerData.Where(o => o.Id == ticket.CustomerId).FirstAsync();
        //        updatedTicket.ByVendor = await _context.Vendors.Where(o => o.Id == ticket.ByVendorId).FirstAsync();
               
        //        _context.Tickets.Remove(updatedTicket);
        //        await _context.SaveChangesAsync();
        //        await _context.Tickets.AddAsync(updatedTicket);
        //        await _context.SaveChangesAsync();
               
        //        Console.WriteLine($"Ticket ({ticket.Id}) updated succesfully");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Ticket ({ticket.Id}) not found");
        //    }
        //}

        public List<Ticket> FetchOwnedTickets(string username)
        {
            var ret = (
                from u in _context.Users
                join c in _context.CustomerData on u equals c.OfUser
                join t in _context.Tickets on c equals t.Customer
                where u.UserName == username && t.Status == 1 // approved
                orderby t.OfScreening.Time ascending
                select t).ToList();
            return ret;
        }

        //public List<Ticket> FetchPendingTickets(string vendorname)
        //{
        //    var ret = (
        //        from v in _context.Vendors
        //        join t in _context.Tickets on v equals t.ByVendor
        //        where v.Username == vendorname && t.Status == 0 // pending
        //        select t).ToList();
        //    return ret;
        //}

        public List<Ticket> FetchPurchases(string username)
        {
            var ret = (
                from u in _context.Users
                join c in _context.CustomerData on u equals c.OfUser
                join t in _context.Tickets on c equals t.Customer
                where u.UserName == username
                orderby t.PurchaseTime descending
                select t).ToList();
            return ret;
        }

        public async Task UpdateTicketAsync(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
            Console.WriteLine($"Ticket ({ticket.Id}) updated succesfully");
        }

        public async Task DeleteTicketAsync(int id)
        {
            var todelete = await _context.Tickets.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (todelete != null)
            {
                var updateScreening = await _context.Screenings.Where(o => o.Id == todelete.OfScreening.Id).FirstOrDefaultAsync();
                if (updateScreening != null)
                {
                    updateScreening.SeatsTaken--;
                    _context.Tickets.Remove(todelete);
                    await _context.SaveChangesAsync();
                    Console.WriteLine($"Ticket({id}) deleted succesfully");
                }
            }
            else
            {
                Console.WriteLine($"Ticket ({id}) not found");
            }
        }
        public async Task<Ticket> GetTicketByIdAsync(int id)
        {
            return await _context.Tickets.Where(o => o.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Ticket>> GetTicketsByUserIdAsync(string userId)
        {
            return await _context.Tickets.Where(o => o.Customer.OfUser.Id == userId)
                .Include(o => o.Customer)
                .Include(o => o.OfScreening)
                .Include(o => o.OfScreening.InLocation)
                .Include(o =>o.OfScreening.OfMovie).ToListAsync();
        }

        public async Task<List<Ticket>> GetTicketsByVendorIdAsync(string vendorId)
        {
            return await _context.Tickets.Where(o => o.OfScreening.InLocation.ByVendor.Id == vendorId)
                .Include(o => o.Customer)
                .Include(o => o.OfScreening)
                .Include(o => o.OfScreening.InLocation)
                .Include(o => o.OfScreening.OfMovie)
                .Include(o => o.Customer.OfUser)
                .ToListAsync();
        }
    }
}
