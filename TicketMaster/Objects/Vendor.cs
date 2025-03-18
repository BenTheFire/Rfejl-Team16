using Microsoft.EntityFrameworkCore;

namespace TicketMaster.Objects
{
    public class Vendor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public DbSet<Location> Locations { get; set; }
    }
}
