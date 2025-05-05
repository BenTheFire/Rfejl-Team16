using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using System.Text.Json;
using TicketMaster.Data.Services.Interfaces;
using TicketMaster.Data.Services.StaticServiceMethods;
using TicketMaster.Extra;
using TicketMaster.Objects;

namespace TicketMaster.Data.Services.Implementations
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
        public async Task FetchMoviesDateAsync()
        {
            var noImageMovies = await _context.Movies.Where(o => o.ImageSource == null).ToListAsync();
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
            Console.WriteLine($"Updated dates for {noImageMovies.Count} entries.");
        }
        public async Task FetchMoviesImagesAsync()
        {
            var noImageMovies = await _context.Movies.Where(o => o.ImageSource == null).ToListAsync();
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
