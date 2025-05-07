using TicketMaster.Authentication;
using TicketMaster.Data.DTOs;
using TicketMaster.Data.Services.Interfaces;
using TicketMaster.Objects;

namespace TicketMaster.Data.Services.Implementations
{
    public class LoginService : ILoginService
    {
        private TicketmasterAuthenticationStateProvider TASP;
        private AuthenticationService AS;
        public LoginService(TicketmasterContext c, TicketmasterAuthenticationStateProvider tasp, AuthenticationService authS)
        {
            _context = c;
            TASP = tasp;
            AS = authS;
        }
        private readonly TicketmasterContext _context;
        public bool LoginUser(ref bool isAdmin, ref bool isVendor, LoginUserDTO Model)
        {
            var loginUser = AS.Authenticate(Model.Username, Model.Password);
            if (loginUser != null)
            {
                TASP.MarkUserAsAuthenticated(loginUser);
            }
            else
            {
                isAdmin = false;
                isVendor = false;
                return false;
            }
            switch (loginUser)
            {
                case User:
                    isAdmin = false;
                    isVendor = false;
                    return true;
                case Administrator:
                    isAdmin = true;
                    isVendor = true;
                    return true;
                case Vendor:
                    isAdmin = false;
                    isVendor = true;
                    return true;
                default:
                    isAdmin = false;
                    isVendor = false;
                    return false;
            }
        }
    }
}
