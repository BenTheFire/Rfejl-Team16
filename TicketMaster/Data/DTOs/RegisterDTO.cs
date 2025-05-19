using Microsoft.EntityFrameworkCore;
using Ticketmaster.Objects;

namespace Ticketmaster.Data.DTOs
{
    public class RegisterUserDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
    public class RegisterVendorDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<Location> Locations { get; set; }
    }
    public class RegisterAdminDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string SshKey { get; set; }
    }
}
