using Microsoft.EntityFrameworkCore;
using TicketMaster.Authentication;

namespace TicketMaster.Objects
{
    public class Vendor : AuthenticateUser
    {
        public new int Id { get => int.Parse(base.Id); set => base.Id = value.ToString(); }
        public DbSet<Location> Locations { get; set; }
    }
}
