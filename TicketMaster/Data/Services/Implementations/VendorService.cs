using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ticketmaster.Data.Services.Interfaces;
using Ticketmaster.Extra;

namespace Ticketmaster.Data.Services.Implementations
{
    public class VendorService : IVendorService
    {
        //private TicketmasterContext _context;
        public VendorService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public async Task CreateVendorAsync(IdentityUser vendor)
        {
            var user = await _userManager.CreateAsync(vendor);
            if (user.Succeeded)
            {
                string roleName = "Vendor";
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    var role = await _roleManager.CreateAsync(new IdentityRole(roleName));
                    if (!role.Succeeded)
                    {
                        return;
                    }
                }
                var result = await _userManager.AddToRoleAsync(vendor, roleName);
                if (!result.Succeeded)
                {
                    await _userManager.DeleteAsync(vendor);
                    return;
                }
            }
        }

        public Task DeleteVendorById(string id)
        {
            _userManager.DeleteAsync(_userManager.Users.FirstOrDefault(u => u.Id == id));
            return Task.CompletedTask;
        }

        public async Task<IdentityUser> GetVendorByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityUser> GetVendorByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<IdentityUser> GetVendorByPhoneAsync(string phone)
        {
            return await _userManager.Users
                .FirstOrDefaultAsync(u => u.PhoneNumber == phone);
        }

        public async Task<List<IdentityUser>> GetVendorsAsync()
        {
            return await _userManager.Users
                .Where(u => _userManager.IsInRoleAsync(u, "Vendor").Result)
                .ToListAsync();
        }

        public async Task<bool> IsInDBByEmailAsync(string email)
        {
            return await _userManager.Users
                .AnyAsync(u => u.Email == email);
        }

        public async Task<bool> IsInDBByPhoneAsync(string phone)
        {
            return await _userManager.Users
                .AnyAsync(u => u.PhoneNumber == phone);
        }

        public async Task UpdateVendor(IdentityUser vendor)
        {
            var current = await _userManager.FindByIdAsync(vendor.Id);
            if (current != null)
            {
                current.Email = vendor.Email;
                current.PhoneNumber = vendor.PhoneNumber;
                current.UserName = vendor.UserName;
                await _userManager.UpdateAsync(current);
            }
        }
        public async Task UpdateVendorPassword(IdentityUser vendor)
        {
            var current = await _userManager.FindByIdAsync(vendor.Id);
            if (current != null && !vendor.PasswordHash.IsNullOrEmpty())
            {
                await _userManager.ChangePasswordAsync(current, current.PasswordHash, vendor.PasswordHash);
            }
        }
    }
}
