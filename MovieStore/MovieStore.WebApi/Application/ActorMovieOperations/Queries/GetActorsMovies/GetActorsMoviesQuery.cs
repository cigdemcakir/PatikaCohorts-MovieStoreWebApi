using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.WebApi.Application.ActorMovieOperations.Queries.QueryViewModel;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.WebApi.Application.ActorMovieOperations.Queries.GetActorsMovies
{
    public class GetActorsMoviesQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetActorsMoviesQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<ActorMovieViewModel> Handle()
        {
            var actorsMovies = _dbContext.ActorsMovies
                .Include(a => a.Actor)
                .Include(a => a.Movie)
                .OrderBy(a=>a.Id).ToList();

            List<ActorMovieViewModel> viewModels =_mapper.Map<List<ActorMovieViewModel>>(actorsMovies);

            return viewModels;
        }
    }
}
