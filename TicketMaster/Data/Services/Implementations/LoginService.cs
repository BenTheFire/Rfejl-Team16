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
                if (_context.Users.Any(o => o.Username.CompareTo(Model.Username) == 0))
                {
                    username = _context.Users.Where(o => o.Username == Model.Username).Select(o => o.Username).First();
                    password = _context.Users.Where(o => o.Username == Model.Username).Select(o => o.PasswordHash).First();
                }
                else
                {
                    username = _context.Administrators.Where(o => o.Name == Model.Username).Select(o => o.Name).First();
                    password = _context.Administrators.Where(o => o.Name == Model.Username).Select(o => o.PasswordHash).First();
                    isAdmin = true;
                }
                return username.Equals(Model.Username) && password.Equals(Model.Password);
            }
            else
            {
                return false;
            }
        }
    }
}
