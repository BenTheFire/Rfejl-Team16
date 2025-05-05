using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using TicketMaster.Data.DTOs;
using TicketMaster.Data.Services.Interfaces;
using TicketMaster.Extra;
using TicketMaster.Objects;

namespace TicketMaster.Data.Services.Implementations
{
    public class LoginService : ILoginService
    {
        public LoginService(TicketmasterContext c)
        {
            _context = c;
        }
        private readonly TicketmasterContext _context;
        public bool LoginUser(ref bool isAdmin, LoginUserDTO Model)
        {
            if (Model.Username.IsNullOrEmpty() || Model.Password.IsNullOrEmpty())
            {
                isAdmin = false;
                return false;
            }
            string username;
            string passwordHash;
            try
            {
                username = _context.Users.Where(o => o.Username == Model.Username).FirstOrDefault()?.Username
                    ?? _context.Administrators.Where(o => o.Name == Model.Username).FirstOrDefault()?.Name
                    ?? throw new Exception("Shit went down");
                passwordHash = _context.Users.Where(o => o.Username == Model.Username).FirstOrDefault()?.PasswordHash
                    ?? _context.Administrators.Where(o => o.Name == Model.Username).FirstOrDefault()?.PasswordHash
                    ?? throw new Exception("Shit went down");
            }
            catch
            {
                return false;
            }

            isAdmin = !_context.Users.Any(o => o.Username.CompareTo(Model.Username) == 0);

            return username.Equals(Model.Username) &&
                /*new PasswordHasher<string>().HashPassword(Model.Username, */Model.Password//)
                    .Equals(passwordHash);
        }
    }
}
