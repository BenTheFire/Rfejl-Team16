using Microsoft.EntityFrameworkCore;
using TicketMaster.Objects;

namespace TicketMaster.Data.DTOs
{
    public class RegisterVendorDTO
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public List<Location> Locations { get; set; }
    }
}
