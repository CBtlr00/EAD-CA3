namespace CA3API.Movies
{
    public class GenreData
    {
        public Genre[] genres { get; set; }
    }

    public class Genre
    {
        public int id { get; set; }
        public string name { get; set; }
    }

}
