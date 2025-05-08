using Microsoft.AspNetCore.Identity;
using TicketMaster.Objects;

namespace TicketMaster.Authentication
{
    public class PasswordService
    {
        private readonly PasswordHasher<AuthenticateUser> _passwordHasher;

        public PasswordService()
        {
            _passwordHasher = new PasswordHasher<AuthenticateUser>();
        }

        public string HashPassword(AuthenticateUser user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }

        public bool VerifyPassword(AuthenticateUser user, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
