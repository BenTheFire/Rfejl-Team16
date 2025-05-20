using Ticketmaster.Objects;

namespace Ticketmaster.Data.DTOs
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
        public MovieWithCast(Movie movie, List<Casting> cast)
        {
            Movie = movie;
            Cast = cast;
        }
        public Movie Movie { get; set; }
        public List<Casting> Cast { get; set; }
    }
    public class Casting
    {
        public Casting((Person person, string role) castingInput)
        {
            Person = castingInput.person;
            Role = castingInput.role;
        }
        public Person Person { get; set; }
        public string Role { get; set; }
    }
}
