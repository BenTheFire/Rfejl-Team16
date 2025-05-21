using Microsoft.AspNetCore.Identity;

namespace Ticketmaster.Data.Services.Interfaces
{
    public interface IUserService
    {
        public Task<List<IdentityUser>> GetUsersAsync();
        public Task<IdentityUser> GetUserByIdAsync(string id);
        public Task<IdentityUser> GetUserByEmailAsync(string email);
        public Task<IdentityUser> GetUserByPhoneAsync(string phone);
        public Task<bool> IsInDBByEmailAsync(string email);
        public Task<bool> IsInDBByPhoneAsync(string phone);
        public Task CreateUserAsync(IdentityUser user);
        public Task DeleteUserById(string id);
        public Task UpdateUser(IdentityUser user);
    }
}
