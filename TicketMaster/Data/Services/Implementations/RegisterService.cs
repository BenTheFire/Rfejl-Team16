using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using TicketMaster.Authentication;
using TicketMaster.Data.DTOs;
using TicketMaster.Data.Services.Interfaces;
using TicketMaster.Objects;

namespace TicketMaster.Data.Services.Implementations
{
    public class RegisterService : IRegisterService
    {
        private const string KEY = "v39g#2mK8pQx7FnWsYbRtJvU1zDeOi0lHkAg64ScBr5qYtNuZmWxEcVbN9rFjI2oP7uQyXzKlTaShGi8cD4eJfRw1sA6mQnLpZ3oIuYvXtCrEwB5nMrJqKzHdLfPgWbSj2aV8dRcXyTzUoI9pLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFeDsZwXvCbNmKlRpZqWtEvBcNxRyTzUiOpLqWeRtYhJgFUIIAIAIAUAISUIIAIUIIIAIhojhZmurd";

        private readonly TicketmasterContext _context;
        private readonly PasswordService PWS;
        private readonly TicketmasterAuthenticationStateProvider TASP;
        public RegisterService(
            TicketmasterContext context,
            PasswordService pws,
            AuthenticationStateProvider asp)
        {
            _context = context;
            PWS = pws;
            TASP = (TicketmasterAuthenticationStateProvider)asp;
        }
        public async Task<bool> RegisterUser(RegisterUserDTO userDTO)
        {
            if (_context.Users.Any(u => u.UserName == userDTO.Username))
            {
                return false;
            }

            User user = new()
            {
                UserName = userDTO.Username
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
            if (_context.Vendors.Any(v => v.UserName == vendorDTO.Username))
            {
                return false;
            }

            Vendor vendor = new()
            {
                UserName = vendorDTO.Username,
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
            if (adminDTO.SshKey != KEY)
            {
                return false;
            }

            if (_context.Administrators.Any(a => a.UserName == adminDTO.Username))
            {
                return false;
            }

            Administrator admin = new()
            {
                UserName = adminDTO.Username,
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
