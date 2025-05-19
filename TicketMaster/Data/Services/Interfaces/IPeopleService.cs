using TicketMaster.Objects;

namespace TicketMaster.Data.Services.Interfaces
{
    public interface IPeopleService
    {
        public Task CreatePerson(Person person);
        public Task DeletePerson(int id);
        public Task UpdatePerson(Person person);
    }
}
