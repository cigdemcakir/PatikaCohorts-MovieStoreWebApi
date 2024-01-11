using Microsoft.EntityFrameworkCore;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.WebApi.Application.ActorOperations.Commands.DeleteActor
{
    public class DeleteActorCommand
    {
        public int ActorId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public DeleteActorCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var actor=_dbContext.Actors
                .Include(a=>a.Movies)
                .FirstOrDefault(a=>a.Id==ActorId);
            
            if (actor is null)
            {
                throw new InvalidOperationException("Actor Not Found.");
            }
            
            if (actor.Movies.Count()>0)
            {
                throw new InvalidOperationException("The actor could not be deleted as there are movies in which the actor has appeared.");
            }

            _dbContext.Actors.Remove(actor);
            _dbContext.SaveChanges();
        }
    }
}
