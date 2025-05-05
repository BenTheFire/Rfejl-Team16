using TicketMaster.Objects;

namespace TicketMaster.Data.DTOs
{
    public class MovieWithCast
    {
        public MovieWithCast((Movie movie, List<(Person person, string role)> cast) thing)
        {
            Movie = thing.movie;
            Cast = new();
            foreach (var item in thing.cast)
            {
                Cast.Add(new(item));
            }
        }
        public Movie Movie { get; init; }
        public List<Casting> Cast { get; private set; }
    }
    public class Casting
    {
        public Casting((Person person, string role) thing)
        {
            Person = thing.person;
            Role = thing.role;
        }
        public Person Person { get; init; }
        public string Role { get; init; }
    }
}
