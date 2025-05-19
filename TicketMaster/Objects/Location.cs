using Microsoft.EntityFrameworkCore;

namespace Ticketmaster.Objects
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Capacity { get; set; }
        //public DbSet<Vendor> ByVendor { get; set; }
    }
}
