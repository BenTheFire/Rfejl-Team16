using Microsoft.EntityFrameworkCore;
using Ticketmaster.Data.Services.Interfaces;
using Ticketmaster.Objects;

namespace Ticketmaster.Data.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly TicketmasterContext _context;
        public CustomerService(TicketmasterContext c)
        {
            _context = c;
        }

        public async Task CreateCustomerAsync(CustomerData customer)
        {
            await _context.CustomerData.AddAsync(customer);
            await _context.SaveChangesAsync();
            Console.WriteLine($"Added new customer succesfully");
        }

        public Task DeleteCustomerAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerData> GetCustomerByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CustomerData>> GetCustomersAsync()
        {
            return await _context.CustomerData.ToListAsync();
        }

        public Task UpdateCustomerAsync(CustomerData customer)
        {
            throw new NotImplementedException();
        }
    }
}
