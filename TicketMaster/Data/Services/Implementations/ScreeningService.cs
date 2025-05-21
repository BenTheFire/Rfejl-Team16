using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        public async Task<List<Screening>> GetScreeningsAsync()
        {
            return await _context.Screenings.Include(o => o.InLocation).Include(o => o.OfMovie).ToListAsync();
        }
        public async Task<List<Screening>> FetchScreeningsByLocation(Location location)
        {
            return await _context.Screenings.Where(o => o.InLocation == location).ToListAsync();
        }
        //public async Task<List<Screening>> FetchScreeningsByVendor(Vendor vendor)
        //{
        //    return await _context.Screenings.Where(o => o.InLocation.Vendors.Contains(vendor)).ToListAsync();
        //}
        public async Task<List<Screening>> FetchScreeningsByMovie(Movie movie)
        {
            return await _context.Screenings.Where(o => o.OfMovie == movie).ToListAsync();
        }
        public bool IsOngoing(Screening screening) 
        {
            if (DateTime.Now > screening.Time & DateTime.Now < screening.Time.AddSeconds(screening.OfMovie.LengthInSeconds)) 
            {
                return true;
            }
            return false;
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
        public async Task CreateScreening(Screening screening)
        {
            Screening newScreening = new Screening()
            {
                Time = screening.Time,
                SeatsTaken = (int)screening.SeatsTaken
            };
            newScreening.InLocation = await _context.Locations.Where(o => o.Id == screening.InLocation.Id).FirstAsync();
            newScreening.OfMovie = await _context.Movies.Where(o => o.Id == screening.OfMovie.Id).FirstAsync();
            _context.Screenings.Add(newScreening);
            await _context.SaveChangesAsync();
            Console.WriteLine("Screening added succesfully");
        }

        public async Task UpdateScreening(Screening screening)
        {
            _context.Update(screening);
            await _context.SaveChangesAsync();
            Console.WriteLine($"Screening ({screening.Id}) updated succesfully");
            /*try
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
            }*/
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

        public async Task<Screening> GetScreeningByIdAsync(int id)
        {
            var result = await _context.Screenings.Where(o => o.Id == id)
                .Include(o => o.InLocation)
                .Include(o => o.OfMovie).FirstOrDefaultAsync();
            if (result != null)
            {
                return result;
            }
            else
            {
                Console.WriteLine($"Screening ({id}) not found");
                return null;
            }
        }
    }
}
