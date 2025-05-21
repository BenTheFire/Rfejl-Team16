using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ticketmaster.Data.Services.Interfaces;
using Ticketmaster.Objects;
namespace Ticketmaster.Data.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly TicketmasterContext _context;
        public UserService(TicketmasterContext c)
        {
            _context = c;
        }

        public Task CreateUserAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityUser> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(o => o.Email == email);
            if (user != null)
            {
                return user;
            }
            else
            {
                throw new Exception("User not found");
            }
        }

        public async Task<IdentityUser> GetUserByIdAsync(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(o => o.Id == id);
            if (user != null)
            {
                return user;
            }
            else
            {
                throw new Exception("User not found");
            }
        }

        public async Task<IdentityUser> GetUserByPhoneAsync(string phone)
        {
            var user = await _context.Users.FirstOrDefaultAsync(o => o.PhoneNumber == phone);
            if (user != null)
            {
                return user;
            }
            else
            {
                throw new Exception("User not found");
            }
        }

        public async Task<List<IdentityUser>> GetUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<bool> IsInDBByEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(o => o.Email == email);
        }

        public async Task<bool> IsInDBByPhoneAsync(string phone)
        {
            return await _context.Users.AnyAsync(o => o.PhoneNumber == phone);
        }

        public async Task UpdateUser(IdentityUser user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(o => o.Id == user.Id);
            if (existingUser != null)
            {
                existingUser.Email = user.Email;
                existingUser.PhoneNumber = user.PhoneNumber;
                _context.Users.Update(existingUser);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("User not found");
            }
        }
    }
}
