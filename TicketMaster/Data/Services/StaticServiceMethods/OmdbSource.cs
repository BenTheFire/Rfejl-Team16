using System.Text.Json;
using Ticketmaster.Objects;

namespace Ticketmaster.Data.Services.StaticServiceMethods
{
    public static class OmdbSource
    {
        private static string _key = "3a5dfbb5";
        public static string OmdbApi = $"http://www.omdbapi.com/?apikey={_key}";
        public static string OmdbApiImdbId(int imdbID) =>
            $"{OmdbApi}&i=tt{imdbID:D7}";

        public static async IAsyncEnumerable<string> ImageSrcLinkAsync(Movie movie)
        {
            for (int i = 0; i < 10; i++)
            {
                string apiResult;
                using var httpClient = new HttpClient();
                apiResult = await httpClient.GetStringAsync(OmdbApiImdbId(movie.ImdbId));
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
        public static async IAsyncEnumerable<string> DateSrcLinkAsync(Movie movie)
        {
            for (int i = 0; i < 10; i++)
            {
                string apiResult;
                using var httpClient = new HttpClient();
                apiResult = await httpClient.GetStringAsync(OmdbApiImdbId(movie.ImdbId));
                JsonDocument apiJson = JsonDocument.Parse(apiResult);
                if (apiJson.RootElement.TryGetProperty("Released", out JsonElement result))
                {
                    yield return result.GetString();
                    yield break;
                }
                else
                {
                    Console.WriteLine("Releasedate is missing");
                    yield return null;
                }
                await Task.Delay(500);
            }
            yield return "";
            yield break;
        }
        public static async IAsyncEnumerable<Movie> FetchMovieByImdbId(int imdbId)
        {
            string apiResult;
            using var httpClient = new HttpClient();
            apiResult = await httpClient.GetStringAsync(OmdbApiImdbId(imdbId));
            JsonDocument apiJson = JsonDocument.Parse(apiResult);
            Movie movie = new Movie();
            if (apiJson.RootElement.TryGetProperty("Title", out JsonElement result))
            {
                movie.Title = result.GetString();
                if (apiJson.RootElement.TryGetProperty("Plot", out JsonElement plot))
                {
                    movie.Description = plot.GetString();
                }
                if (apiJson.RootElement.TryGetProperty("Runtime", out JsonElement runtime))
                {
                    string[] time = runtime.GetString().Split(' ');
                    movie.LengthInSeconds = int.Parse(time[0]) * 60;
                }
                if (apiJson.RootElement.TryGetProperty("Poster", out JsonElement poster))
                {
                    movie.ImageSource = poster.GetString();
                }
                if (apiJson.RootElement.TryGetProperty("Year", out JsonElement released))
                {
                    movie.ReleaseDate = released.GetString();
                }
                if (apiJson.RootElement.TryGetProperty("imdbID", out JsonElement imdbIdElem))
                {
                    movie.ImdbId = int.Parse(imdbIdElem.GetString()[2..]);
                }
                yield return movie;
            }
            else
            {
                Console.WriteLine($"Movie with id {imdbId} is missing");
                yield return null;
            }
            yield return null;
        }
        public static async IAsyncEnumerable<Movie> FetchMovieByTitle(string title)
        {
            string apiResult;
            using var httpClient = new HttpClient();
            apiResult = await httpClient.GetStringAsync(OmdbApi + $"&t={title}");
            JsonDocument apiJson = JsonDocument.Parse(apiResult);
            Movie movie = new Movie();
            if (apiJson.RootElement.TryGetProperty("Title", out JsonElement result))
            {
                movie.Title = result.GetString();
                if (apiJson.RootElement.TryGetProperty("Plot", out JsonElement plot))
                {
                    movie.Description = plot.GetString();
                }
                if (apiJson.RootElement.TryGetProperty("Runtime", out JsonElement runtime))
                {
                    string[] time = runtime.GetString().Split(' ');
                    movie.LengthInSeconds = int.Parse(time[0]) * 60;
                }
                if (apiJson.RootElement.TryGetProperty("Poster", out JsonElement poster))
                {
                    movie.ImageSource = poster.GetString();
                }
                if (apiJson.RootElement.TryGetProperty("Year", out JsonElement released))
                {
                    movie.ReleaseDate = released.GetString();
                }
                if (apiJson.RootElement.TryGetProperty("imdbID", out JsonElement imdbId))
                {
                    movie.ImdbId = int.Parse(imdbId.GetString()[2..]);
                }
                yield return movie;
            }
            else
            {
                Console.WriteLine($"Movie with title {title} is missing");
                yield return null;
            }
            yield return null;
        }
    }
}
