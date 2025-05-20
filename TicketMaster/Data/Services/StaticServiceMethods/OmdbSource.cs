using Microsoft.AspNetCore.Mvc.TagHelpers;
using System.Text.Json;
using Ticketmaster.Data.DTOs;
using Ticketmaster.Objects;

namespace Ticketmaster.Data.Services.StaticServiceMethods
{
    public static class OmdbSource
    {
        private static string _key = "3a5dfbb5";
        public static string OmdbApi = $"http://www.omdbapi.com/?apikey={_key}&plot=full";
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
        public static async IAsyncEnumerable<MovieWithCast> FetchMovieByImdbId(int imdbId)
        {
            string apiResult;
            using var httpClient = new HttpClient();
            apiResult = await httpClient.GetStringAsync(OmdbApiImdbId(imdbId));
            JsonDocument apiJson = JsonDocument.Parse(apiResult);
            Movie movie = ParseMovie(apiJson);
            List<Casting> cast = ParseCast(apiJson);
            if (movie != null)
            {
                MovieWithCast movieWithCast = new MovieWithCast(movie, cast);
                yield return movieWithCast;
            }
            else
            {
                Console.WriteLine($"Movie with Imdb ID {imdbId} is missing");
                yield return null;
            }
        }
        public static async IAsyncEnumerable<MovieWithCast> FetchMovieByTitle(string title)
        {
            string apiResult;
            using var httpClient = new HttpClient();
            apiResult = await httpClient.GetStringAsync(OmdbApi + $"&t={title}");
            JsonDocument apiJson = JsonDocument.Parse(apiResult);
            Movie movie = ParseMovie(apiJson);
            List<Casting> cast = ParseCast(apiJson);
            if (movie != null && cast != null)
            {
                MovieWithCast movieWithCast = new MovieWithCast(movie, cast);
                yield return movieWithCast;
            }
            else
            {
                Console.WriteLine($"Movie with title {title} is missing");
                yield return null;
            }
        }
        private static Movie ParseMovie(JsonDocument jsonDocument)
        {
            Movie movie = new Movie();
            if (jsonDocument.RootElement.TryGetProperty("Title", out JsonElement result))
            {
                movie.Title = result.GetString();
                if (jsonDocument.RootElement.TryGetProperty("Plot", out JsonElement plot))
                {
                    movie.Description = plot.GetString();
                }
                if (jsonDocument.RootElement.TryGetProperty("Runtime", out JsonElement runtime))
                {
                    string[] time = runtime.GetString().Split(' ');
                    movie.LengthInSeconds = int.Parse(time[0]) * 60;
                }
                if (jsonDocument.RootElement.TryGetProperty("Poster", out JsonElement poster))
                {
                    movie.ImageSource = poster.GetString();
                }
                if (jsonDocument.RootElement.TryGetProperty("Year", out JsonElement released))
                {
                    movie.ReleaseDate = released.GetString();
                }
                if (jsonDocument.RootElement.TryGetProperty("imdbID", out JsonElement imdbId))
                {
                    movie.ImdbId = int.Parse(imdbId.GetString()[2..]);
                }
                return movie;
            }
            else return null;
        }
        private static List<Casting> ParseCast(JsonDocument jsonDocument)
        {
            List<Casting> casting = new List<Casting>();
            if (jsonDocument.RootElement.TryGetProperty("Actors", out JsonElement result))
            {
                string[] actors = result.GetString().Split(',');
                foreach (string actor in actors)
                {
                    casting.Add(new Casting((new Person() { Name = actor.ToString() }, "Actor")));
                }
                if (jsonDocument.RootElement.TryGetProperty("Director", out JsonElement director))
                {
                    string[] directors = director.GetString().Split(',');
                    foreach (string actor in directors)
                    {
                        casting.Add(new Casting((new Person() { Name = actor.ToString() }, "Director")));
                    }
                }
                if(jsonDocument.RootElement.TryGetProperty("Writer", out JsonElement writer))
                {
                    string[] writers = writer.GetString().Split(',');
                    foreach (string actor in writers)
                    {
                        casting.Add(new Casting((new Person() { Name = actor.ToString() }, "Writer")));
                    }
                }
                return casting;
            }
            else return null;
        }
    }
}
