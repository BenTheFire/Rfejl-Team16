using Microsoft.EntityFrameworkCore;
using TicketMaster.Data.DTOs;
using TicketMaster.Data.Services.Interfaces;
using TicketMaster.Objects;

namespace TicketMaster.Data.Services.Implementations
{
    public class ScreeningService : IScreeningService
    {
        private TicketmasterContext _context;
        public ScreeningService(TicketmasterContext c)
        {
            _context = c;
        }
        public async Task<ScreeningWithTicketsDTO> FetchTicketsForScreening(Screening screening)
        {
            var tickets = await _context.Tickets.Where(o => o.OfScreening == screening).ToListAsync();
            return new ScreeningWithTicketsDTO() { Screening = screening, Tickets = tickets } ;
        }
        public async Task<List<Screening>> FetchScreenings()
        {
            return await _context.Screenings.ToListAsync();
        }
        public async Task<List<Screening>> FetchScreeningsByLocation(Location location)
        {
            return await _context.Screenings.Where(o => o.InLocation == location).ToListAsync();
        }
        public async Task<List<Screening>> FetchScreeningsByVendor(Vendor vendor)
        {
            return await _context.Screenings.Where(o => o.InLocation.Vendors.Contains(vendor)).ToListAsync();
        }
        public async Task<Screening> FetchScreening(int id)
        {
            return new Screening()
            {
                Id = id,
                InLocation = await _context.Screenings.Where(o => o.Id == id).Select(o => o.InLocation).FirstAsync(),
                OfMovie = await _context.Screenings.Where(o => o.Id == id).Select(o => o.OfMovie).FirstAsync(),
                Time = (await _context.Screenings.Where(o => o.Id == id).FirstAsync()).Time,
                SeatsTaken = (await _context.Screenings.Where(o => o.Id == id).FirstAsync()).SeatsTaken
            };
        }
    }
}
