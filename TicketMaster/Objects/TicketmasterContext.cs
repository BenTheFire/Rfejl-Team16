using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace TicketMaster.Objects
{
    public class TicketmasterContext : IdentityDbContext
    {
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;" +
            "Database=TicketmasterDbTeam16;" +
            "Trusted_Connection=True;" +
            "TrustServerCertificate=True;");
        }*/
        public TicketmasterContext(DbContextOptions<TicketmasterContext> o) : base(o) { }
        public DbSet<Credit> Credits { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Screening> Screenings { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<CustomerData> CustomerData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

}
