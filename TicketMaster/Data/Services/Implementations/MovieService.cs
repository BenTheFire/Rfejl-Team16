using Microsoft.EntityFrameworkCore;
using Ticketmaster.Data.DTOs;
using Ticketmaster.Data.Services.Interfaces;
using Ticketmaster.Data.Services.StaticServiceMethods;
using Ticketmaster.Objects;

namespace Ticketmaster.Data.Services.Implementations
{

    public class MovieService : IMovieService
    {
        public MovieService(TicketmasterContext c)
        {
            _context = c;
        }
        private readonly TicketmasterContext _context;

        public async Task<MovieWithCast> FetchMovieDataByImdbIdAsync(string imdbId)
        {
            int imdbIdInt = Convert.ToInt32(imdbId);

            /*var result = await (
                from movie in _context.Movies
                join credit in _context.Credits on movie equals credit.OfMovie
                join person in _context.People on credit.WhoIs equals person
                where movie.ImdbId == imdbIdInt
                select new
                {
                    Movie = movie,
                    Person = person,
                    credit.Role
                }).ToListAsync();*/
            Movie movie = await _context.Movies.Where(o => o.ImdbId == imdbIdInt).FirstAsync();
            List<Credit> credits = await _context.Credits.Where(o => o.OfMovie.ImdbId == imdbIdInt).Include(o => o.WhoIs).ToListAsync();
            List<Person> people = new List<Person>();

            var cast = new List<(Person, string)>();

            foreach (Credit credit in credits)
            {
                cast.Add(new(credit.WhoIs, credit.Role));
            }

            MovieWithCast result = new MovieWithCast((movie, cast));

            return result;

        }

        public async Task<List<Screening>> FetchScreenings(string imdbId)
        {
            var guh = await FetchMovieDataByImdbIdAsync(imdbId);

            return await
                (from scr in _context.Screenings
                 where scr.OfMovie == guh.Movie
                 select scr)
                .Include(scr => scr.InLocation)
                .ToListAsync();
        }
        public async Task CreateMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            Console.WriteLine($"Movie created succesfully");
        }
        public async Task CreateMovie(MovieWithCast movie)
        {
            await _context.Movies.AddAsync(movie.Movie);

            foreach (Casting castMember in movie.Cast)
            {
                var existingPerson = await _context.People.FirstOrDefaultAsync(p => p.Name == castMember.Person.Name);

                if (existingPerson == null)
                {
                    await _context.People.AddAsync(castMember.Person);
                    Console.WriteLine($"Person ({castMember.Person.Name}) marked for creation.");
                }
                else
                {
                    movie.Cast.Where(o => o.Person == castMember.Person && o.Role == castMember.Role).First().Person = existingPerson;
                    Console.WriteLine($"Person ({existingPerson.Name}) already exists, linking existing entity.");
                }
            }
            foreach (var castMember in movie.Cast)
            {
                await _context.Credits.AddAsync(new Credit
                {
                    OfMovie = movie.Movie,
                    WhoIs = castMember.Person,
                    Role = castMember.Role
                });
                Console.WriteLine($"Credit ({castMember.Role}) for {castMember.Person.Name} marked for creation.");
            }

            await _context.SaveChangesAsync();
            Console.WriteLine($"Movie, people (if new), and credits created/linked successfully.");
        }
        /*public async Task CreateMovie(MovieWithCast movie)
        {
            await _context.Movies.AddAsync(movie.Movie);
            await _context.SaveChangesAsync();
            List<Person> people = movie.Cast.Select(o => o.Person).ToList();
            foreach (var person in people)
            {
                if (!await _context.People.AnyAsync(o => o.Name == person.Name))
                {
                    await _context.People.AddAsync(person);
                    await _context.SaveChangesAsync();
                    Console.WriteLine($"Person ({person.Name}) created succesfully");
                } else
                {
                    people.Where(o => o.Name == person.Name).First().Id = await _context.People.Where(o => o.Name == person.Name).Select(o => o.Id).FirstAsync();
                }
            }
            foreach (var cast in movie.Cast)
            {
                await _context.Credits.AddAsync(new Credit
                {
                    OfMovie = movie.Movie,
                    WhoIs = cast.Person,
                    Role = cast.Role
                });
                await _context.SaveChangesAsync();
                Console.WriteLine($"Credit ({cast.Role}) created succesfully");
            }
            await _context.SaveChangesAsync();
            Console.WriteLine($"Movie created with cast succesfully");
        }*/

        public async Task DeleteMovieByTitle(string title)
        {
            var toDelete = await _context.Movies.Where(o => o.Title == title).FirstOrDefaultAsync();
            if (toDelete != null)
            {
                _context.Movies.Remove(toDelete);
                await _context.SaveChangesAsync();
                Console.WriteLine($"Movie ({title}) deleted succesfully");
            }
            else
            {
                Console.WriteLine($"Movie ({title}) not found");
            }
            //try
            //{
            //    var toDelete = await _context.Movies.Where(o => o.Title == title).FirstAsync();
            //    if (toDelete != null)
            //    {
            //        _context.Movies.Remove(toDelete);
            //        await _context.SaveChangesAsync();
            //        Console.WriteLine($"Movie ({title}) deleted succesfully");
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine($"Movie ({title}) not found");
            //}
        }

        public async Task UpdateMovie(Movie movie)
        {
            if (!await _context.Movies.AnyAsync(o => o.Id == movie.Id))
            {
                Console.WriteLine($"Movie ({movie.Id}) not found");
                return;
            }

            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
            Console.WriteLine($"Movie ({movie.Id}) updated succesfully");
        }

        public async Task<bool> IsInDBByTitle(string title)
        {
            return await _context.Movies.AnyAsync(o => o.Title == title);
        }

        public async Task<bool> IsInDBByImdbId(string imdbId)
        {
            return await _context.Movies.AnyAsync(o => o.ImdbId == Convert.ToInt32(imdbId));
        }

        public async Task UpdateMovieFromOmdbByTitle(string title)
        {
            var movieToUpdate = await _context.Movies.Where(o => o.Title == title).FirstOrDefaultAsync();
            if (movieToUpdate == null)
            {
                Console.WriteLine($"Movie ({title}) not found");
                return;
            }
            else
            {
                await foreach (MovieWithCast item in OmdbSource.FetchMovieByTitle(title))
                {
                    movieToUpdate.Title = item.Movie.Title;
                    movieToUpdate.Description = item.Movie.Description;
                    movieToUpdate.LengthInSeconds = item.Movie.LengthInSeconds;
                    movieToUpdate.ImageSource = item.Movie.ImageSource;
                    movieToUpdate.ReleaseDate = item.Movie.ReleaseDate;
                    movieToUpdate.ImdbId = item.Movie.ImdbId;
                    _context.Movies.Update(movieToUpdate);
                    await _context.SaveChangesAsync();
                    Console.WriteLine($"Movie ({title}) updated succesfully");
                    return;
                }
            }
        }

        public async Task<List<Movie>> GetMoviesAsync()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<Movie> GetMovieByTitleAsync(string title)
        {
            var movie = await _context.Movies.Where(o => o.Title == title).FirstOrDefaultAsync();
            if (movie == null)
            {
                Console.WriteLine($"Movie ({title}) not found");
                return null;
            }
            else
            {
                return movie;
            }
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movie = await _context.Movies.Where(o => o.Id == id).FirstOrDefaultAsync();
            if (movie == null)
            {
                Console.WriteLine($"Movie ({id}) not found");
                return null;
            }
            else
            {
                return movie;
            }
        }

        public async Task<Movie> GetMovieByImdbIdAsync(string imdbId)
        {
            var movie = await _context.Movies.Where(o => o.ImdbId == Convert.ToInt32(imdbId)).FirstOrDefaultAsync();
            if (movie == null)
            {
                Console.WriteLine($"Movie ({imdbId}) not found");
                return null;
            }
            else
            {
                return movie;
            }
        }

        public Task DeleteMovieByImdbId(string imdbId)
        {
            throw new NotImplementedException();
        }
    }
}
