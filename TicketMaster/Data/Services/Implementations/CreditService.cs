using Microsoft.EntityFrameworkCore;
using Ticketmaster.Data.Services.Interfaces;
using Ticketmaster.Objects;

namespace Ticketmaster.Data.Services.Implementations
{
    public class CreditService : ICreditService
    {
        private TicketmasterContext _context;
        public CreditService(TicketmasterContext c)
        {
            _context = c;
        }
        public async Task CreateCredit(Credit credit)
        {
            await _context.Credits.AddAsync(credit);
            await _context.SaveChangesAsync();
            Console.WriteLine("Credit created successfully");
        }

        public async Task DeleteCredit(int id)
        {
            var remove = await _context.Credits.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (remove != null)
            {
                _context.Credits.Remove(remove);
                await _context.SaveChangesAsync();
                Console.WriteLine($"Credit ({id}) removed successfully");
            }
            else
            {
                Console.WriteLine($"Credit ({id}) not found");
            }
        }

        public async Task<List<Credit>> GetAllCredits()
        {
            return await _context.Credits.ToListAsync();
        }

        public async Task<Credit> GetCreditById(int id)
        {
            var credit = await _context.Credits.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (credit == null)
            {
                Console.WriteLine($"Credit ({id}) not found");
                return null;
            }
            return credit;
        }

        public async Task<List<Credit>> GetCreditsByMovieId(int movieId)
        {
            List<Credit> credits = await _context.Credits.Where(o => o.OfMovie.Id == movieId).Include(o => o.OfMovie).Include(o => o.WhoIs).ToListAsync();
            return credits;
        }

        public async Task<List<Credit>> GetCreditsByPersonId(int personId)
        {
            List<Credit> credits = await _context.Credits.Where(o => o.WhoIs.Id == personId).Include(o => o.OfMovie).Include(o => o.WhoIs).ToListAsync();
            return credits;
        }

        public async Task UpdateCredit(Credit credit)
        {
            _context.Credits.Update(credit);
            await _context.SaveChangesAsync();
            Console.WriteLine($"Credit ({credit.Id}) updated successfully");
        }
    }
}
