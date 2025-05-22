using Microsoft.AspNetCore.Identity;

namespace Ticketmaster.Objects
{
    public class CustomerData
    {
        public int Id { get; set; }
        public IdentityUser? OfUser { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
    }
}
