using TicketMaster.Data.DTOs;
using TicketMaster.Data.Services.Interfaces;
using TicketMaster.Objects;

namespace TicketMaster.Data.Services.Implementations
{
    public class RegisterService : IRegisterService
    {
        private readonly TicketmasterContext _context;
        public RegisterService(TicketmasterContext context)
        {
            _context = context;
        }
        public void RegisterUser(LoginUserDTO userDTO)
        {

        }

        public void RegisterVendor(RegisterVendorDTO vendorDTO)
        {

        }
    }
}
