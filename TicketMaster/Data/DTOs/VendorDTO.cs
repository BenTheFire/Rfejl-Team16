using Microsoft.EntityFrameworkCore;

namespace TicketMaster.Data.DTOs
{
    public class VendorDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public int LocationId { get; set; }
    }
}
