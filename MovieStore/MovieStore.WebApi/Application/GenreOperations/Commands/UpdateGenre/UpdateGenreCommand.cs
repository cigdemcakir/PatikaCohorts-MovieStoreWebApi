using MovieStore.WebApi.DBOperations;

namespace MovieStore.WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public int GenreId { get; set; }
        public UpdateGenreModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public UpdateGenreCommand(IMovieStoreDbContext dbContext)
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
           
            var hasGenre = _dbContext.Genres.Any( g => g.Name == Model.Name && g.Id!=GenreId);
          
            if (hasGenre)
            {
                throw new InvalidOperationException("The given genre name exists in another film genre.");
            }

            bool hasMovie=_dbContext.Movies.Any(m=>m.GenreId==GenreId);
          
            if (Model.IsActive == false && hasMovie)
            {
                throw new InvalidOperationException("The status cannot be set to inactive as there are movies associated with the genre.");
            }

            genre.Name = !string.IsNullOrEmpty(Model.Name) ? Model.Name : genre.Name;
            genre.IsActive = !bool.Equals(genre.IsActive, Model.IsActive) ? Model.IsActive : genre.IsActive;

            _dbContext.SaveChanges();
        }
    }
}
