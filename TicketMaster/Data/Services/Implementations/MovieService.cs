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

        public async Task<MovieWithCast> FetchMovieDataAsync(string imdbId)
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
            Movie movie = await _context.Movies.Where(o => o.ImdbId == imdbIdInt).FirstOrDefaultAsync()
                ?? throw new MovieDbException();
            List<Credit> credits = await _context.Credits.Where(o => o.OfMovie.Id == imdbIdInt).ToListAsync();
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
            var guh = await FetchMovieDataAsync(imdbId);

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

        public async Task DeleteMovieByTitle(string title)
        {
            try
            {
                var toDelete = await _context.Movies.Where(o => o.Title == title).FirstAsync();
                if (toDelete != null)
                {
                    _context.Movies.Remove(toDelete);
                    await _context.SaveChangesAsync();
                    Console.WriteLine($"Movie ({title}) deleted succesfully");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Movie ({title}) not found");
            }
        }

        public async Task DeleteMovieByImdbId(string imdbId)
        {
            var toDelete = 
                await _context.Movies
                .Where(o => $"{o.ImdbId:D7}" == imdbId)
                .FirstOrDefaultAsync();

            if (toDelete != null)
            {
                _context.Movies.Remove(toDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateMovie(Movie movie)
        {
            try
            {
                var toUpdate = await _context.Movies.Where(o => o.Id == movie.Id).FirstAsync();
                if (toUpdate != null)
                {
                    toUpdate.Title = movie.Title;
                    toUpdate.ImageSource = movie.ImageSource;
                    toUpdate.ImdbId = movie.ImdbId;
                    toUpdate.ReleaseDate = movie.ReleaseDate;
                    toUpdate.Description = movie.Description;
                    toUpdate.LengthInSeconds = movie.LengthInSeconds;
                    await _context.SaveChangesAsync();
                    Console.WriteLine($"Movie ({movie.Id}) updated succesfully");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Movie ({movie.Id}) not found");
            }
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
            var movieToUpdate = await _context.Movies.Where(o => o.Title == title).FirstAsync();
            if (movieToUpdate == null)
            {
                Console.WriteLine($"Movie ({title}) not found");
                return;
            }
            else
            {
                await foreach (Movie item in OmdbSource.FetchMovieByTitle(title))
                {
                    movieToUpdate.Title = item.Title;
                    movieToUpdate.Description = item.Description;
                    movieToUpdate.LengthInSeconds = item.LengthInSeconds;
                    movieToUpdate.ImageSource = item.ImageSource;
                    movieToUpdate.ReleaseDate = item.ReleaseDate;
                    movieToUpdate.ImdbId = item.ImdbId;
                    _context.Movies.Update(movieToUpdate);
                    await _context.SaveChangesAsync();
                    Console.WriteLine($"Movie ({title}) updated succesfully");
                    return;
                }
            }
        }
    }
}
