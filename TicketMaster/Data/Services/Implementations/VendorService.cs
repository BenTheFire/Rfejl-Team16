using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ticketmaster.Data.Services.Interfaces;
using Ticketmaster.Extra;
using Ticketmaster.Objects;

namespace Ticketmaster.Data.Services.Implementations
{
    public class VendorService : IVendorService
    {
        private TicketmasterContext _context;
        public VendorService(TicketmasterContext context)
        {
            _context = context;
        }


        public async Task<IdentityUser> GetVendorByEmailAsync(string email)
        {
            throw new NotImplementedException();

        }

        public async Task<IdentityUser> GetVendorByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityUser> GetVendorByPhoneAsync(string phone)
        {
            throw new NotImplementedException();
        }

        public async Task<List<IdentityUser>> GetVendorsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsInDBByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsInDBByPhoneAsync(string phone)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateVendor(IdentityUser vendor)
        {
            throw new NotImplementedException();
        }
        public async Task UpdateVendorPassword(IdentityUser vendor)
        {
            throw new NotImplementedException();
        }

        public Task UpdateVendorPassword(IdentityBuilder vendor, string password)
        {
            throw new NotImplementedException();
        }
    }
}
