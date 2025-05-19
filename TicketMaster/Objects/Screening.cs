namespace TicketMaster.Objects
{
    public class Screening
    {
        public int Id { get; set; }
        public Location InLocation { get; set; }
        public Movie OfMovie { get; set; }
        public DateTime Time { get; set; }
        public int SeatsTaken { get; set; }
    }
}
