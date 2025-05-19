using Ticketmaster.Data.DTOs;

namespace Ticketmaster.Data.Services.Interfaces
{
    public interface ILoginService
    {
        public bool LoginUser(ref bool isAdmin, ref bool isVendor, LoginUserDTO Model);
        void LogOut();
    }
}
