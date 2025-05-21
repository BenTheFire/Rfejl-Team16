using Microsoft.EntityFrameworkCore;
using Ticketmaster.Data.Services.Interfaces;
using Ticketmaster.Objects;

namespace Ticketmaster.Data.Services.Implementations
{
    public class PeopleService : IPeopleService
    {
        private TicketmasterContext _context;
        public PeopleService(TicketmasterContext c)
        {
            _context = c;
        }
        public async Task CreatePerson(Person person)
        {
            _context.People.Add(person);
            await _context.SaveChangesAsync();
            Console.WriteLine("Person created succesfully");
        }

        public async Task DeletePerson(int id)
        {
            try
            {
                var toRemove = await _context.People.Where(o => o.Id == id).FirstAsync();
                if (toRemove != null)
                {
                    _context.People.Remove(toRemove);
                    await _context.SaveChangesAsync();
                    Console.WriteLine($"Person ({id}) removed succesfully");
                }
                else
                {
                    Console.WriteLine($"Person ({id}) not found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Person ({id}) not found");
            }

        }

        public async Task<List<Person>> GetAllPeople()
        {
            return await _context.People.ToListAsync();
        }

        public async Task UpdatePerson(Person person)
        {
            if (!await _context.People.AnyAsync(o => o.Id == person.Id))
            {
                Console.WriteLine($"Person ({person.Id}) not found");
                return;
            }
            _context.People.Update(person);
            await _context.SaveChangesAsync();
            Console.WriteLine($"Person ({person.Id}) updated succesfully");
        }
        public async Task<Person> GetPersonById(int id)
        {
            var person = await _context.People.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (person != null)
            {
                return person;
            }
            else
            {
                Console.WriteLine($"Person ({id}) not found");
                return null;
            }

        }
    }
}
