using TicketMaster.Authentication;

namespace TicketMaster.Objects
{
    public class User : IAuthenticateUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
