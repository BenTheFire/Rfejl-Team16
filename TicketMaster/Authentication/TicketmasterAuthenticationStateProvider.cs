using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace TicketMaster.Authentication
{
    public class TicketmasterAuthenticationStateProvider : AuthenticationStateProvider
    {
        private AuthenticateUser? _currentUser;
        private SignInManager<AuthenticateUser> SIM;
        public TicketmasterAuthenticationStateProvider(SignInManager<AuthenticateUser> signInManager)
        {
            SIM = signInManager;
        }
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = _currentUser != null ? new ClaimsIdentity
            (
                new[] 
                {
                    new Claim(ClaimTypes.Name, _currentUser.UserName),
                    new Claim(ClaimTypes.Role, _currentUser.GetType().Name)
                }, 
                "TicketmasterAuth"
            ) : new ClaimsIdentity();

            var principal = new ClaimsPrincipal(identity);
            return Task.FromResult(new AuthenticationState(principal));
        }

        public async Task MarkUserAsAuthenticated(AuthenticateUser user)
        {
            _currentUser = user;

            await SIM.SignInWithClaimsAsync(_currentUser, true, new[]
                {
                    new Claim(ClaimTypes.Name, _currentUser.UserName),
                    new Claim(ClaimTypes.Role, _currentUser.GetType().Name)
                });
        }

        public async Task MarkUserAsLoggedOut()
        {
            _currentUser = null;

            await SIM.SignOutAsync();
        }
    }
}
