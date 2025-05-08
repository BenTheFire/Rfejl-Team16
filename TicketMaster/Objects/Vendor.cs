using Microsoft.EntityFrameworkCore;
using TicketMaster.Authentication;

namespace TicketMaster.Objects
{
    public class Vendor : IAuthenticateUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public DbSet<Location> Locations { get; set; }
    }
}
