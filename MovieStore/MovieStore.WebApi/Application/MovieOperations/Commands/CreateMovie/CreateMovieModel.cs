namespace MovieStore.WebApi.Application.MovieOperations.Commands.CreateMovie
{
    public class CreateMovieModel
    {
        public string Title { get; set; }
        public DateTime YearOfMovie { get; set; }
        public int GenreId { get; set; }
        public int DirectorId { get; set; }
        public decimal Price { get; set; }
    }
}
