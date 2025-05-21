using Microsoft.AspNetCore.Identity;
using Ticketmaster.Data.DTOs;

namespace Ticketmaster.Data.Services.Interfaces
{
    public interface IVendorService
    {
        public Task<List<IdentityUser>> GetVendorsAsync();
        public Task<IdentityUser> GetVendorByIdAsync(string id);
        public Task<IdentityUser> GetVendorByEmailAsync(string email);
        public Task<IdentityUser> GetVendorByPhoneAsync(string phone);
        public Task<bool> IsInDBByEmailAsync(string email);
        public Task<bool> IsInDBByPhoneAsync(string phone);
        public Task CreateVendorAsync(IdentityUser vendor);
        public Task DeleteVendorById(string id);
        public Task UpdateVendor(IdentityUser vendor);
    }
}
