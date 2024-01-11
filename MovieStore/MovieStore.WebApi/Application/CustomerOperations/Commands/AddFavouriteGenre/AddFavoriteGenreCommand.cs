using MovieStore.WebApi.DBOperations;

namespace MovieStore.WebApi.Application.CustomerOperations.Commands.AddFavouriteGenre
{
    public class AddFavoriteGenreCommand
    {
        public int CustomerId { get; set; }
        public int GenreId { get; set; }

        private readonly IMovieStoreDbContext _dbContext;

        public AddFavoriteGenreCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var customer = _dbContext.Customers.FirstOrDefault(c => c.Id == CustomerId);
           
            if (customer is null)
            {
                throw new InvalidOperationException("Customer Not Found.");
            }
            
            var genre = _dbContext.Genres.FirstOrDefault(c => c.Id == GenreId);
            
            if (genre is null)
            {
                throw new InvalidOperationException("Genre Not Found.");
            }

            customer.FavoriteGenre.Add(genre);

            _dbContext.SaveChanges();
        }
    }
}
