namespace MovieStore.WebApi.Application.MovieOperations.Queries.QueryViewModel
{
    public class MovieViewModel
    {
        public string Title { get; set; }
        public string YearOfMovie { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public decimal Price { get; set; }
        public ICollection<string> Actors { get; set; }
    }
}
