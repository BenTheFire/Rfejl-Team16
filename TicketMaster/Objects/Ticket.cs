using System.ComponentModel;

namespace Ticketmaster.Objects
{
    public class Ticket
    {
        public int Id { get; set; }
        public Screening OfScreening { get; set; }
        public CustomerData Customer { get; set; }
        public float Price { get; set; }
        public string Seat { get; set; }
        [DefaultValue(0)]
        public int Status { get; set; }
        public DateTime? PurchaseTime { get; set; }
    }
}
