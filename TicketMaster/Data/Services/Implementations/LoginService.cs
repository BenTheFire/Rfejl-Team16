using Microsoft.AspNetCore.Identity;
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
            if (!Model.Username.IsNullOrEmpty() || !Model.Password.IsNullOrEmpty())
                return false;


            var user = _context.Users.Where(o => o.Username == Model.Username).First();
            string username = user.Username;
            string passwordHash = user.PasswordHash;

            isAdmin = !_context.Users.Any(o => o.Username.CompareTo(Model.Username) == 0) ? true : false;

            return username.Equals(Model.Username) &&
                new PasswordHasher<string>().HashPassword(Model.Username, Model.Password)
                    .Equals(passwordHash);
        }
    }
}
