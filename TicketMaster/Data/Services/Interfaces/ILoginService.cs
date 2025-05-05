using TicketMaster.Data.DTOs;

namespace TicketMaster.Data.Services.Interfaces
{
    public interface ILoginService
    {
        public bool LoginUser(ref bool isAdmin, ref bool isVendor, LoginUserDTO Model);
    }
}
