using Microsoft.EntityFrameworkCore;
using Ticketmaster.Data.DTOs;
using Ticketmaster.Data.Services.Interfaces;
using Ticketmaster.Objects;

namespace Ticketmaster.Data.Services.Implementations
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
        public async Task CreateScreening(ScreeningDTO screening)
        {
            Screening newScreening = new Screening()
            {
                Time = screening.Time,
                SeatsTaken = (int)screening.SeatsTaken
            };
            newScreening.InLocation = await _context.Locations.Where(o => o.Id == screening.InLocationId).FirstAsync();
            newScreening.OfMovie = await _context.Movies.Where(o => o.Id == screening.OfMovieId).FirstAsync();
            _context.Screenings.Add(newScreening);
            await _context.SaveChangesAsync();
            Console.WriteLine("Screening added succesfully");
        }

        public async Task UpdateScreening(ScreeningDTO screening)
        {
            try
            {
                Screening updatedScreening = await _context.Screenings.Where(o => o.Id == screening.Id).FirstAsync();
                if (updatedScreening != null)
                {
                    updatedScreening.Time = screening.Time;
                    updatedScreening.SeatsTaken = (int)screening.SeatsTaken;
                    updatedScreening.InLocation = await _context.Locations.Where(o => o.Id == screening.InLocationId).FirstAsync();
                    updatedScreening.OfMovie = await _context.Movies.Where(o => o.Id == screening.OfMovieId).FirstAsync();
                    await _context.SaveChangesAsync();
                    Console.WriteLine($"Screening ({screening.Id}) updated succesfully");
                }
            } catch (Exception e)
            {
                Console.WriteLine($"Screening ({screening.Id}) not found");
            }
        }

        public async Task DeleteScreening(int id)
        {
            try
            {
                Screening delete = await _context.Screenings.Where(o => o.Id == id).FirstAsync();
                if (delete != null)
                {
                    _context.Screenings.Remove(delete);
                    await _context.SaveChangesAsync();
                    Console.WriteLine($"Screening ({id}) deleted succesfully");
                }
            }catch (Exception e)
            { 
                Console.WriteLine($"Screening ({id}) not found");
            }
        }
    }
}
