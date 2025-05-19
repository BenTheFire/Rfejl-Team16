using TicketMaster.Objects;

namespace TicketMaster.Data.DTOs
{
    public class MovieWithCast
    {
        public MovieWithCast((Movie movie, List<(Person person, string role)> cast) movieWithCastInput)
        {
            Movie = movieWithCastInput.movie;
            Cast = new();
            foreach (var item in movieWithCastInput.cast)
            {
                Cast.Add(new(item));
            }
        }
        public Movie Movie { get; init; }
        public List<Casting> Cast { get; private set; }
    }
    public class Casting
    {
        public Casting((Person person, string role) castingInput)
        {
            Person = castingInput.person;
            Role = castingInput.role;
        }
        public Person Person { get; init; }
        public string Role { get; init; }
    }
}
