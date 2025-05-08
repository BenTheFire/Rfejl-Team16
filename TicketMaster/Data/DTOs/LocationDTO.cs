namespace TicketMaster.Data.DTOs
{
    public class LocationDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int? Capacity { get; set; }
        public int? VendorId { get; set; }
    }
}
