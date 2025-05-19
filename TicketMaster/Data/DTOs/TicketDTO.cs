namespace Ticketmaster.Data.DTOs
{
    public class TicketDTO
    {
        public int? Id { get; set; }
        public int? OfScreeningId { get; set; }
        public int? CustomerId { get; set; }
        public float Price { get; set; }
        public string Seat { get; set; }
        public int? Status { get; set; }
        public int? ByVendorId { get; set; }
        public DateTime PurchaseTime { get; set; }
    }
}
