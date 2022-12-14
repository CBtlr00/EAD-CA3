namespace CA3API.Movies
{
    public interface IMoviesService
    {
        Task<List<MoviesItem>> GetMovies(string searchText);
    }
}