using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace TicketMaster.Authentication
{
    public class TicketmasterAuthenticationStateProvider : AuthenticationStateProvider
    {
        private IAuthenticateUser? _currentUser;
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = _currentUser != null ? new ClaimsIdentity
            (
                new[] 
                {
                    new Claim(ClaimTypes.Name, _currentUser.Username),
                    new Claim(ClaimTypes.Role, _currentUser.GetType().Name)
                }, 
                "TicketmasterAuth"
            ) : new ClaimsIdentity();

            var principal = new ClaimsPrincipal(identity);
            return Task.FromResult(new AuthenticationState(principal));
        }

        public void MarkUserAsAuthenticated(IAuthenticateUser user)
        {
            _currentUser = user;
            var identity = _currentUser != null ? new ClaimsIdentity
            (
                new[]
                {
                    new Claim(ClaimTypes.Name, _currentUser.Username),
                    new Claim(ClaimTypes.Role, _currentUser.GetType().Name)
                },
                "TicketmasterAuth"
            ) : new ClaimsIdentity();

            var principal = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        }

        public void MarkUserAsLoggedOut()
        {
            _currentUser = null;
            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
        }
    }
}
