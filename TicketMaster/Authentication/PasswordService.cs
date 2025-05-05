using Microsoft.AspNetCore.Identity;
using TicketMaster.Objects;

namespace TicketMaster.Authentication
{
    public class PasswordService
    {
        private readonly PasswordHasher<IAuthenticateUser> _passwordHasher;

        public PasswordService()
        {
            _passwordHasher = new PasswordHasher<IAuthenticateUser>();
        }

        public string HashPassword(IAuthenticateUser user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }

        public bool VerifyPassword(IAuthenticateUser user, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
