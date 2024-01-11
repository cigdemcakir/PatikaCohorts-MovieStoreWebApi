using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.WebApi.Application.ActorMovieOperations.Queries.GetActorsMovies;
using MovieStore.WebApi.Application.ActorMovieOperations.Queries.QueryViewModel;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.WebApi.Application.ActorMovieOperations.Queries.GetActorMovieDetail
{
    public class GetActorMovieByIdQuery
    {

        public int ActorMovieId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetActorMovieByIdQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public ActorMovieViewModel Handle()
        {
            var actorMovie=_dbContext.ActorsMovies
                .Include(a=>a.Movie)
                .Include(a=>a.Actor)
                .FirstOrDefault(a=>a.Id==ActorMovieId);
            
            if (actorMovie is null)
            {
                throw new InvalidOperationException("No Actor-Film Relationship Found.");
            }

            ActorMovieViewModel viewModel = _mapper.Map<ActorMovieViewModel>(actorMovie);

            return viewModel;
        }
    }
}
