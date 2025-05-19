namespace Ticketmaster.Data.DTOs
{
    public class ScreeningDTO
    {
        public int? Id { get; set; }
        public int? InLocationId { get; set; }
        public int? OfMovieId { get; set; }
        public DateTime Time { get; set; }
        public int? SeatsTaken { get; set; }
    }
}
