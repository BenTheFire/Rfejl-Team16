using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ticketmaster.Data.Services.Interfaces;
using Ticketmaster.Extra;
using Ticketmaster.Objects;

namespace Ticketmaster.Data.Services.Implementations
{

    public class VendorService : IVendorService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public VendorService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }


        public async Task<IdentityUser> GetVendorByEmailAsync(string email)
        {
            throw new NotImplementedException();

        }

        public async Task<IdentityUser> GetVendorByIdAsync(string id)
        {
            var vendor = await _userManager.FindByIdAsync(id);
            if (vendor == null)
            {
                Console.WriteLine($"Vendor with ID {id} not found.");
                return null;
            }
            return vendor;
        }

        public async Task<IdentityUser> GetVendorByPhoneAsync(string phone)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<IdentityUser>> GetVendorsAsync()
        {
            return await _userManager.GetUsersInRoleAsync("Vendor");
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
