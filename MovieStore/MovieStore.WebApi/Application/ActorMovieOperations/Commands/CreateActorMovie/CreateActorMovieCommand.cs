using AutoMapper;
using MovieStore.WebApi.DBOperations;
using MovieStore.WebApi.Entities;

namespace MovieStore.WebApi.Application.ActorMovieOperations.Commands.CreateActorMovie
{
    public class CreateActorMovieCommand
    {
        public CreateActorMovieModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateActorMovieCommand(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public void Handle()
        {
            var actorMovie = _dbContext.ActorsMovies.FirstOrDefault(a => a.ActorId == Model.ActorId && a.MovieId == Model.MovieId);
            
            if (actorMovie is not null)
            {
                throw new InvalidOperationException("There is an Actor-Movie Relationship.");
            }

            actorMovie = _mapper.Map<ActorMovie>(Model);

            _dbContext.ActorsMovies.Add(actorMovie);
            _dbContext.SaveChanges();
        }
    }
}
