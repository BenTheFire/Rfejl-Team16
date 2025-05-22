using Ticketmaster.Objects;

namespace Ticketmaster.Data.Services.Interfaces
{
    public interface ICustomerService
    {
        public Task<List<CustomerData>> GetCustomersAsync();
        public Task<CustomerData> GetCustomerByIdAsync(int id);
        public Task<CustomerData> GetCustomerByUserIdAsync(string id);
        public Task CreateCustomerAsync(CustomerData customer);
        public Task UpdateCustomerAsync(CustomerData customer);
        public Task DeleteCustomerAsync(int id);
    }
}
