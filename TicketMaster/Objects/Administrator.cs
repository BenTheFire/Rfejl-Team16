using TicketMaster.Authentication;

namespace TicketMaster.Objects
{
    public class Administrator : IAuthenticateUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
    }
}
