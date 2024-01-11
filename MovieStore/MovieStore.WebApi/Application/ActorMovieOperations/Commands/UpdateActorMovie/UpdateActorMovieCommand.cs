using MovieStore.WebApi.DBOperations;

namespace MovieStore.WebApi.Application.ActorMovieOperations.Commands.UpdateActorMovie
{
    public class UpdateActorMovieCommand
    {
        public int ActorMovieId { get; set; }
        public UpdateActorMovieModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public UpdateActorMovieCommand(IMovieStoreDbContext dbContext)
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

            var hasActor = _dbContext.Actors.Any(a => a.Id == Model.ActorId);
            
            if (!hasActor)
            {
                throw new InvalidOperationException("Actor Not Found.");
            }

            var hasMovie = _dbContext.Movies.Any(m => m.Id == Model.MovieId);
            
            if (!hasMovie)
            {
                throw new InvalidOperationException("Movie Not Found.");
            }

            var hasActorMovie = _dbContext.ActorsMovies.Any(a => a.MovieId == Model.MovieId && a.ActorId == Model.ActorId && a.Id != ActorMovieId);
            
            if (hasActorMovie)
            {
                throw new InvalidOperationException("There is already an actor-film relationship.");
            }

            actorMovie.ActorId = Model.ActorId;
            actorMovie.MovieId = Model.MovieId;

            _dbContext.SaveChanges();
        }

    }
}
