namespace TicketMaster.Objects
{
    public class CustomerData
    {
        public int Id { get; set; }
        public User? OfUser { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
