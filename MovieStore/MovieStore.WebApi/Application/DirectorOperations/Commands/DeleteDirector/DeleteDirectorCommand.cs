using Microsoft.EntityFrameworkCore;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.WebApi.Application.DirectorOperations.Commands.DeleteDirector
{
    public class DeleteDirectorCommand
    {
        public int DirectorId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public DeleteDirectorCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var director=_dbContext.Directors
                .Include(d=>d.Movies)
                .FirstOrDefault(d=>d.Id==DirectorId);
            
            if (director is null)
            {
                throw new InvalidOperationException("Director Not Found.");
            }
            
            if (director.Movies.Count()>0)
            {
                throw new InvalidOperationException("The director could not be deleted as there are movies associated with the director.");
            }
            
            _dbContext.Directors.Remove(director);
            _dbContext.SaveChanges();
        }
    }
}
