using Microsoft.EntityFrameworkCore;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.WebApi.Application.MovieOperations.Commands.DeleteMovie
{
    public class DeleteMovieCommand
    {
        public int MovieId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public DeleteMovieCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var movies= _dbContext.Movies.OrderBy(p=>p.Id).ToList();
            
            var movie=_dbContext.Movies
                .Include(m=>m.Actors)
                .FirstOrDefault(m=>m.Id==MovieId);
          
            if (movie is null)
            {
                throw new InvalidOperationException("Movie Not Found.");
            }
           
            movie.IsActive = false;

            _dbContext.SaveChanges();
        }
    }
}
