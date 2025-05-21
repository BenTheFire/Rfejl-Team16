using System.ComponentModel.DataAnnotations;

namespace Ticketmaster.Objects
{
    public class Credit
    {
        [Key]
        public int Id { get; set; }
        public Movie OfMovie { get; set; }
        public string Role { get; set; }
        public Person WhoIs { get; set; }
    }
}
