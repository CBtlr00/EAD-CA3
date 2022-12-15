using CA3API.Movies;
using Microsoft.AspNetCore.Components;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace CA3API.Movies
{
    public class MoviesService : IMoviesService
    {
        private readonly HttpClient _httpClient;
        const string _baseUrl = "https://advanced-movie-search.p.rapidapi.com/";
        const string _newsEndpoint = "search/movie?query=";
        const string _genreEndpoint = "discover/movie?with_genres=";
        const string _genreListEndpoint = "genre/movie/list";
        const string _host = "advanced-movie-search.p.rapidapi.com";
        const string _key = "4f949f8e15mshd96321a049eb4dcp165d00jsn9a8ee5e2827a";

        public MoviesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            ConfigureHttpClient();
        }
        public async Task<List<MoviesItem>> GetMovies(string searchText)
        {
            var response = await _httpClient.GetAsync(_newsEndpoint+searchText);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();

            var dto = await JsonSerializer.DeserializeAsync<MoviesData>(stream);
            return dto.results.Select(n => new MoviesItem
            {
                Headline = n.title,
                Body = (MarkupString)n.overview,
                Year = n.release_date,
                ImageUrl = n.poster_path,
                Language = n.original_language,
                Rating = n.popularity
            }).ToList();
        }

        public async Task<List<MoviesItem>> GetMoviesbyGenre(string Genre)
        {
            var response = await _httpClient.GetAsync(_genreEndpoint + Genre);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();

            var dto = await JsonSerializer.DeserializeAsync<MoviesData>(stream);
            return dto.results.Select(n => new MoviesItem
            {
                Headline = n.title,
                ID = n.id,
                Body = (MarkupString)n.overview,
                Year = n.release_date,
                ImageUrl = n.poster_path,
                Language = n.original_language,
                Rating = n.popularity
            }).ToList();
        }

        public async Task<List<GenreItem>> GenreList()
        {
            var response = await _httpClient.GetAsync(_genreListEndpoint);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();

            var dto = await JsonSerializer.DeserializeAsync<GenreData>(stream);
            return dto.genres.Select(n => new GenreItem
            {
                ID = n.id,
                Name = n.name
            }).ToList();
        }

        private void ConfigureHttpClient()
        {
            _httpClient.BaseAddress = new Uri(_baseUrl);
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", _host);
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", _key);
        }
    }
}
