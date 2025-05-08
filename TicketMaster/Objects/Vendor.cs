using Microsoft.EntityFrameworkCore;
using TicketMaster.Authentication;

namespace TicketMaster.Objects
{
    public class Vendor : AuthenticateUser
    {
        public DbSet<Location> Locations { get; set; }
    }
}
