using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using TicketMaster.Data.DTOs;
using TicketMaster.Data.Services.Interfaces;
using TicketMaster.Extra;
using TicketMaster.Objects;
using TicketMaster.Objects.Users;

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
            string username;
            string password;
            isAdmin = false;
            if (Model.Username.IsNullOrEmpty() && Model.Password.IsNullOrEmpty())
            {
                var user = _context.Users.Where(o => o.Username == Model.Username).First();
                username = user.Username;
                password = user.PasswordHash;
                if (!_context.Users.Any(o => o.Username.CompareTo(Model.Username) == 0))
                {
                    isAdmin = true;
                }
                return username.Equals(Model.Username) && 
                    new PasswordHasher<string>().HashPassword(Model.Username, Model.Password)
                        .Equals(password);
            }
            else
            {
                return false;
            }
        }
    }
}
