using Microsoft.AspNetCore.Components;

namespace CA3API.Movies
{
    public class MoviesItem
    {
        public string Headline { get; set; }
        public int ID { get; set; }
        public MarkupString Body { get; set; }
        public string Year { get; set; }
        public string ImageUrl { get; set; }
        public float Rating { get; set; }
        public string Language { get; set; }

    }
}
