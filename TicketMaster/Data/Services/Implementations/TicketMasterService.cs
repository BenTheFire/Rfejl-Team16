using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using System.Text.Json;
using Ticketmaster.Data.Services.Interfaces;
using Ticketmaster.Data.Services.StaticServiceMethods;
using Ticketmaster.Extra;
using Ticketmaster.Objects;

namespace Ticketmaster.Data.Services.Implementations
{
    public class TicketMasterService : ITicketMasterService
    {
        private readonly TicketmasterContext _context;
        public TicketMasterService(TicketmasterContext context)
        {
            //needed constructor
            _context = context;
        }

        public async Task<List<Movie>> FetchMoviesBetweenAsync(int first, int last)
        {
            return await _context.Movies
                    .Where(movie => movie.Id < last && movie.Id >= first)
                    .ToListAsync();
        }
        public async Task UpdateImagesAndDateAsync()
        {
            var noDateMovies = await _context.Movies.Where(o => o.ReleaseDate == null || o.ReleaseDate == "").ToListAsync();
            var noImageMovies = await _context.Movies.Where(o => o.ImageSource == null || o.ImageSource == "").ToListAsync();
            if (noDateMovies.Count != 0)
            {
                foreach (var movie in noDateMovies)
                {
                    await foreach (var dateSrc in OmdbSource.DateSrcLinkAsync(movie))
                    {
                        if (dateSrc != null)
                        {
                            movie.ReleaseDate = dateSrc[^4..];
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                await _context.SaveChangesAsync();
            }
            Console.WriteLine($"Updated dates for {noDateMovies.Count} entries.");
            if (noImageMovies.Count != 0)
            {
                foreach (var movie in noImageMovies)
                {
                    await foreach (var imgSrc in OmdbSource.ImageSrcLinkAsync(movie))
                    {
                        if (imgSrc != null)
                        {
                            movie.ImageSource = imgSrc;
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                await _context.SaveChangesAsync();
            }
            Console.WriteLine($"Updated images for {noImageMovies.Count} entries.");
        }
    }
}
