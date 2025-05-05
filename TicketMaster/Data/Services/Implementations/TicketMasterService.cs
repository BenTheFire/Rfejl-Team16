using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TicketMaster.Data.Services.Interfaces;
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

        public static string OmdbApi(int imdbID, string APIkey) => 
            $"http://www.omdbapi.com/?i=tt{imdbID:D7}&apikey={APIkey}&";

        public static async IAsyncEnumerable<string> ImageSrcLinkAsync(Movie movie)
        {
            for (int i = 0; i < 10; i++)
            {
                string apiResult;
                using var httpClient = new HttpClient();
                apiResult = await httpClient.GetStringAsync(OmdbApi(movie.ImdbId, "3a5dfbb5"));
                JsonDocument apiJson = JsonDocument.Parse(apiResult);
                if (apiJson.RootElement.TryGetProperty("Poster", out JsonElement result))
                {
                    yield return result.GetString();
                    yield break;
                }
                else
                {
                    Console.WriteLine("Poster key is missing");
                    yield return null;
                }
                await Task.Delay(500);
            }
            yield return "";
            yield break;
        }
    }
}
