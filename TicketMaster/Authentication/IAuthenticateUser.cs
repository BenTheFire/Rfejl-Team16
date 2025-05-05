namespace TicketMaster.Authentication
{
    public interface IAuthenticateUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
