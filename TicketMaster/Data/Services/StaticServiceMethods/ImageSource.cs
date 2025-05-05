using System.Text.Json;
using TicketMaster.Objects;

namespace TicketMaster.Data.Services.StaticServiceMethods
{
    public static class ImageSource
    {
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
