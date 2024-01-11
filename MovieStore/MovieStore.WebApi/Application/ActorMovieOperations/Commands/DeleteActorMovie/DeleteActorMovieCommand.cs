using MovieStore.WebApi.DBOperations;

namespace MovieStore.WebApi.Application.ActorMovieOperations.Commands.DeleteActorMovie
{
    public class DeleteActorMovieCommand
    {
        public int ActorMovieId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public DeleteActorMovieCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var actorMovie = _dbContext.ActorsMovies.FirstOrDefault(a => a.Id == ActorMovieId);
            
            if (actorMovie is null)
            {
                throw new InvalidOperationException("No Actor-Film Relationship Found.");
            }

            _dbContext.ActorsMovies.Remove(actorMovie);
            _dbContext.SaveChanges();
        }

    }
}
