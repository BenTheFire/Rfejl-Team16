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
        public bool LoginUser(ref bool isAdmin, ref bool isVendor, LoginUserDTO Model)
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
                    ?? _context.Administrators.Where(o => o.Username == Model.Username).FirstOrDefault()?.Username
                    ?? _context.Vendors.Where(o => o.Username == Model.Username).FirstOrDefault()?.Username
                    ?? throw new Exception("Shit went down");
                passwordHash = _context.Users.Where(o => o.Username == Model.Username).FirstOrDefault()?.PasswordHash
                    ?? _context.Administrators.Where(o => o.Username == Model.Username).FirstOrDefault()?.PasswordHash
                    ?? _context.Vendors.Where(o => o.Username == Model.Username).FirstOrDefault()?.PasswordHash
                    ?? throw new Exception("Shit went down");
            }
            catch
            {
                return false;
            }

            if (isAdmin = _context.Administrators.Any(o => o.Username == username)) { isVendor = true; }
            else isVendor = _context.Vendors.Any(o => o.Username == username);


                return username.Equals(Model.Username) &&
                    /*new PasswordHasher<string>().HashPassword(Model.Username, */Model.Password//)
                        .Equals(passwordHash);
        }
    }
}
