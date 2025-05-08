using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace TicketMaster.Authentication
{
    public class TicketmasterAuthenticationStateProvider : AuthenticationStateProvider
    {
        private IAuthenticateUser? _currentUser;
        private readonly HttpContext? _httpContext;
        
        public TicketmasterAuthenticationStateProvider(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext.HttpContext;
        }
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

            if (_httpContext != null)
            {
                _httpContext.SignInAsync("TicketmasterAuth", principal);
            }
            else
            {
                Console.WriteLine("Cookie could not be accessed!");
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        }

        public void MarkUserAsLoggedOut()
        {
            _currentUser = null;
            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());

            if (_httpContext != null)
            {
                _httpContext.SignOutAsync("TicketmasterAuth");
            }
            else
            {
                Console.WriteLine("Cookie could not be accessed!");
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
        }
    }
}
