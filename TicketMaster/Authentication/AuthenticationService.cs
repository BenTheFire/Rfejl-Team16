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

        public IAuthenticateUser? Authenticate(string username, string password)
        {
            IAuthenticateUser? authuser = _context.Users.Where(o => o.Username == username).FirstOrDefault() as IAuthenticateUser
                ?? _context.Administrators.Where(o => o.Username == username).FirstOrDefault() as IAuthenticateUser
                ?? _context.Vendors.Where(o => o.Username == username).FirstOrDefault() as IAuthenticateUser
                ?? null;

            if (authuser == null || !_passwordService.VerifyPassword(authuser, password))
            {
                return null;
            }

            return authuser;
        }
    }
}
