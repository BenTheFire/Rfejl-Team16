using Microsoft.EntityFrameworkCore;

namespace TicketMaster.Objects
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Capacity { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
    }
}
