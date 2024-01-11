using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.WebApi.Application.ActorOperations.Queries.QueryViewModel;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.WebApi.Application.ActorOperations.Queries.GetActorDetail
{
    public class GetActorByIdQuery
    {
        public int ActorId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetActorByIdQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public ActorViewModel Handle()
        {
            var actor=_dbContext.Actors
                .Include(a=>a.Movies).ThenInclude(m=>m.Movie)
                .FirstOrDefault(a=>a.Id==ActorId);
            
            if (actor is null)
            {
                throw new InvalidOperationException("Actor Not Found.");
            }

            ActorViewModel viewModel = _mapper.Map<ActorViewModel>(actor);
           
            return viewModel;
            
        }

    }
}
