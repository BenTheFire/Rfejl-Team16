using Ticketmaster.Objects;

namespace Ticketmaster.Data.Services.Interfaces
{
    public interface IPeopleService
    {
        public Task<List<Person>> GetAllPeople();
        public Task<Person> GetPersonById(int id);
        public Task CreatePerson(Person person);
        public Task DeletePerson(int id);
        public Task UpdatePerson(Person person);
    }
}
