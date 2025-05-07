using TicketMaster.Data.DTOs;

namespace TicketMaster.Data.Services.Interfaces
{
    public interface IRegisterService
    {
        public void RegisterUser(LoginUserDTO userDTO);
        public void RegisterVendor(RegisterVendorDTO vendorDTO);
    }
}
