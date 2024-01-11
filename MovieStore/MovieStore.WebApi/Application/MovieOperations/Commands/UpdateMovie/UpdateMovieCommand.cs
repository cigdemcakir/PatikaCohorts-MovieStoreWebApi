using MovieStore.WebApi.DBOperations;

namespace MovieStore.WebApi.Application.MovieOperations.Commands.UpdateMovie
{
    public class UpdateMovieCommand
    {
        public int MovieId { get; set; }
        public UpdateMovieModel Model { get; set; }
        private IMovieStoreDbContext _dbContext;

        public UpdateMovieCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var movie=_dbContext.Movies.FirstOrDefault(m=>m.Id==MovieId);
          
            if (movie is null)
            {
                throw new InvalidOperationException("Movie Not Found.");
            }

            var isGenreNull = _dbContext.Genres.Any(g => g.Id == Model.GenreId);
          
            if (!isGenreNull)
            {
                throw new InvalidOperationException("Genre Not Found.");
            }

            var isDirectorNull = _dbContext.Directors.Any(g => g.Id == Model.DirectorId);
           
            if (!isDirectorNull)
            {
                throw new InvalidOperationException("Director Not Found.");
            }

            var hasTitle = _dbContext.Movies.Any(m => m.Title.ToLower().Replace(" ", "") == Model.Title.ToLower().Replace(" ", "")
            && m.DirectorId == Model.DirectorId
            && m.Id != MovieId);
          
            if (hasTitle)
            {
                throw new InvalidOperationException("There is a movie with this name.");
            }

            movie.Title = !string.IsNullOrEmpty(Model.Title) ? Model.Title : movie.Title;
            movie.DirectorId= !int.IsNegative(Model.DirectorId) ? Model.DirectorId : movie.DirectorId;
            movie.GenreId = !int.IsNegative(Model.GenreId) ? Model.GenreId : movie.GenreId;
            movie.Price = decimal.IsPositive(Model.Price) ? Model.Price : movie.Price;
            movie.IsActive = Model.IsActive;

            _dbContext.SaveChanges();
        }

    }
}
