using TicketMaster.Data.DTOs;

namespace TicketMaster.Data.Services.Interfaces
{
    public interface IRegisterService
    {
        public Task<bool> RegisterAdmin(RegisterAdminDTO adminDTO);
        public Task<bool> RegisterUser(RegisterUserDTO userDTO);
        public Task<bool> RegisterVendor(RegisterVendorDTO vendorDTO);
    }
}
