using Microsoft.AspNetCore.Identity;
using Ticketmaster.Authentication;
using Ticketmaster.Data.DTOs;
using Ticketmaster.Data.Services.Interfaces;
using Ticketmaster.Objects;

namespace Ticketmaster.Data.Services.Implementations
{
    public class RegisterService : IRegisterService
    {
        private const string SECRET_KEY = "v39g#2mK8pQx7FnWsYbRtJvU1zDeOi0lHkAg64ScBr5qYtNuZmWxEcVbN9rFjI2oP7uQyXzKlTaShGi8cD4eJfRw1sA6mQnLpZ3oIuYvXtCrEwB5nMrJqKzHdLfPgWbSj2aV8dRcXyTzUoI9pLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFUIIAIAIAUAISUIIAIUIIIAIhojhZmurd";

        private readonly TicketmasterContext _context;
        private readonly PasswordService PWS;
        private readonly TicketmasterAuthenticationStateProvider TASP;
        public RegisterService(
            TicketmasterContext context,
            PasswordService pws,
            TicketmasterAuthenticationStateProvider tasp)
        {
            _context = context;
            PWS = pws;
            TASP = tasp;
        }
        public async Task<bool> RegisterUser(RegisterUserDTO userDTO)
        {
            if (_context.Users.Any(u => u.Username == userDTO.Username))
            {
                return false;
            }

            User user = new()
            {
                Username = userDTO.Username
            };
            user.PasswordHash = PWS.HashPassword(user, userDTO.Password);

            await _context.Users.AddAsync(user);
            await _context.CustomerData.AddAsync(new CustomerData
            {
                Email = userDTO.Email,
                Phone = userDTO.Phone,
                OfUser = user
            });

            TASP.MarkUserAsAuthenticated(user);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RegisterVendor(RegisterVendorDTO vendorDTO)
        {
            if (_context.Vendors.Any(v => v.Username == vendorDTO.Username))
            {
                return false;
            }

            Vendor vendor = new()
            {
                Username = vendorDTO.Username,
                Email = vendorDTO.Email
            };
            vendor.PasswordHash = PWS.HashPassword(vendor, vendorDTO.Password);
            await vendor.Locations.AddRangeAsync(vendorDTO.Locations);

            await _context.Vendors.AddAsync(vendor);

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> RegisterAdmin(RegisterAdminDTO adminDTO)
        {
            if (adminDTO.SshKey != SECRET_KEY) //IMPLEMENT SSH CHECK
            {
                return false;
            }

            if (_context.Administrators.Any(a => a.Username == adminDTO.Username))
            {
                return false;
            }

            Administrator admin = new()
            {
                Username = adminDTO.Username,
                Email = adminDTO.Email
            };
            admin.PasswordHash = PWS.HashPassword(admin, adminDTO.Password);

            await _context.Administrators.AddAsync(admin);

            TASP.MarkUserAsAuthenticated(admin);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
