using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.WebApi.Application.ActorOperations.Queries.QueryViewModel;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.WebApi.Application.ActorOperations.Queries.GetActor
{
    public class GetActorsQuery
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetActorsQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<ActorViewModel> Handle()
        {
            var movies = _dbContext.Actors
                .Include(a => a.Movies).ThenInclude(m => m.Movie)
                .OrderBy(a => a.Id)
                .ToList();
            
            List<ActorViewModel> viewModel = _mapper.Map<List<ActorViewModel>>(movies);
           
            return viewModel;
        }
    }
}
