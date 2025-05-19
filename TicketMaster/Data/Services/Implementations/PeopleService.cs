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

        public async Task UpdatePerson(Person person)
        {
            try
            {
                var toUpdate = await _context.People.Where(o => o.Id == person.Id).FirstAsync();
                if (toUpdate != null)
                {
                    toUpdate.Name = person.Name;
                    toUpdate.BirthDate = person.BirthDate;
                    toUpdate.Nationality = person.Nationality;
                    await _context.SaveChangesAsync();
                    Console.WriteLine($"Person ({person.Id}) updated sucecsfully");
                }
            } catch (Exception e)
            {
                Console.WriteLine($"Person ({person.Id}) not found");
            }
        }
    }
}
