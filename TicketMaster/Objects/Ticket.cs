namespace TicketMaster.Objects
{
    public class Ticket
    {
        public int Id { get; set; }
        public Screening OfScreening { get; set; }
        public CustomerData Customer { get; set; }
        public float Price { get; set; }
        public string Seat { get; set; }
        public Vendor ByVendor { get; set; }
    }
}
