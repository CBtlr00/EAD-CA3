namespace CA3API.Movies
{
    public interface IMoviesService
    {
        Task<List<MoviesItem>> GetMovies(string searchText);
        Task<List<MoviesItem>> GetMoviesbyGenre(string Genre);
        Task<List<GenreItem>> GenreList();
    }
}