using Ticketmaster.Authentication;
using Ticketmaster.Data.DTOs;
using Ticketmaster.Data.Services.Interfaces;
using Ticketmaster.Objects;

namespace Ticketmaster.Data.Services.Implementations
{
    public class LoginService : ILoginService
    {
        private TicketmasterAuthenticationStateProvider TASP;
        private AuthenticationService AS;
        public LoginService(TicketmasterAuthenticationStateProvider tasp, AuthenticationService authS)
        {
            TASP = tasp;
            AS = authS;
        }
        public bool LoginUser(ref bool isAdmin, ref bool isVendor, LoginUserDTO Model)
        {
            var loginUser = AS.Authenticate(Model.Username, Model.Password);
            if (loginUser == null)
            {
                isAdmin = false;
                isVendor = false;
                return false;
            }
            TASP.MarkUserAsAuthenticated(loginUser);
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

        public void LogOut() => TASP.MarkUserAsLoggedOut();
    }
}
