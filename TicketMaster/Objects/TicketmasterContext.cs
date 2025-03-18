using Microsoft.EntityFrameworkCore;

namespace TicketMaster.Objects
{
    public class TicketmasterContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;" +
            "Database=TicketmasterDbTeam16;" +
            "Trusted_Connection=True;" +
            "TrustServerCertificate=True;");
        }

        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Credit> Credits { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Screening> Screenings { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<CustomerData> CustomerData { get; set; }
        public DbSet<User> Users { get; set; }
    }

}
