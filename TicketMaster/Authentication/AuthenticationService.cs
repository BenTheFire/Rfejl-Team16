using Microsoft.AspNetCore.Authentication;
using TicketMaster.Objects;

namespace TicketMaster.Authentication
{
    public class AuthenticationService
    {
        private readonly TicketmasterContext _context;
        private readonly PasswordService _passwordService;

        public AuthenticationService(TicketmasterContext context, PasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        public AuthenticateUser? Authenticate(string username, string password)
        {
            AuthenticateUser? authuser = 
                _context.Administrators.Where(o => o.UserName == username).FirstOrDefault() as AuthenticateUser
                ?? _context.Vendors.Where(o => o.UserName == username).FirstOrDefault() as AuthenticateUser
                ?? _context.Users.Where(o => o.UserName == username).FirstOrDefault() as AuthenticateUser
                ?? null;

            if (authuser == null || !_passwordService.VerifyPassword(authuser, password))
            {
                return null;
            }

            return authuser;
        }
    }
}
