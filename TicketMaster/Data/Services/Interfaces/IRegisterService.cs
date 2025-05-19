using Ticketmaster.Data.DTOs;

namespace Ticketmaster.Data.Services.Interfaces
{
    public interface IRegisterService
    {
        public Task<bool> RegisterAdmin(RegisterAdminDTO adminDTO);
        public Task<bool> RegisterUser(RegisterUserDTO userDTO);
        public Task<bool> RegisterVendor(RegisterVendorDTO vendorDTO);
    }
}
