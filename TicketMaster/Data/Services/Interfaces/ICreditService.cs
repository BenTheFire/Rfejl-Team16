using Ticketmaster.Objects;

namespace Ticketmaster.Data.Services.Interfaces
{
    public interface ICreditService
    {
        public Task<List<Credit>> GetAllCredits();
        public Task<Credit> GetCreditById(int id);
        public Task CreateCredit(Credit credit);
        public Task DeleteCredit(int id);
        public Task UpdateCredit(Credit credit);
        public Task<List<Credit>> GetCreditsByPersonId(int personId);
        public Task<List<Credit>> GetCreditsByMovieId(int movieId);
    }
}
