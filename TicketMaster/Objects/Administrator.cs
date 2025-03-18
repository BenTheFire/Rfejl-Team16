namespace TicketMaster.Objects
{
    public class Administrator
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
    }
}
