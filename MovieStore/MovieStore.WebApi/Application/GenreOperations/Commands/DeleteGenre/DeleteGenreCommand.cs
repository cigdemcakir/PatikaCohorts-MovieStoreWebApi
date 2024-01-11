using MovieStore.WebApi.DBOperations;

namespace MovieStore.WebApi.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public DeleteGenreCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var genre=_dbContext.Genres.FirstOrDefault(g=>g.Id==GenreId);
           
            if (genre is null)
            {
                throw new InvalidOperationException("Genre Not Found.");
            }

            bool hasMovie=_dbContext.Movies.Any(m=>m.GenreId==GenreId);
          
            if (hasMovie)
            {
                throw new InvalidOperationException("The genre could not be deleted as there are movies associated with this genre.");
            }

            _dbContext.Genres.Remove(genre);
            _dbContext.SaveChanges();
        }

    }
}
