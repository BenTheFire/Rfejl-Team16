namespace TicketMaster.Objects
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int LengthInSeconds { get; set; }
        public int ImdbId { get; set; }
    }
}
