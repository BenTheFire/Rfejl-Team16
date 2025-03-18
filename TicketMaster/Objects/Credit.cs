namespace TicketMaster.Objects
{
    [Microsoft.EntityFrameworkCore.Keyless]
    public class Credit
    {
        public Movie OfMovie { get; set; }
        public string Role { get; set; }
        public Person WhoIs { get; set; }
    }
}
