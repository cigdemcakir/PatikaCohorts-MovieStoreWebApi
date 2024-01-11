using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.WebApi.Application.MovieOperations.Queries.QueryViewModel;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.WebApi.Application.MovieOperations.Queries.GetMovieDetail
{
    public class GetMovieByIdQuery
    {
        public int MovieId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetMovieByIdQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public MovieViewModel Handle()
        {
            var movie = _dbContext.Movies
                .Include(m => m.Genre)
                .Include(m => m.Director)
                .Include(m => m.Actors).ThenInclude(arm => arm.Actor)
                .FirstOrDefault(m => m.Id == MovieId && m.IsActive);

            if (movie is null)
            {
                throw new InvalidOperationException("Movie Not Found.");
            }

            MovieViewModel movieViewModel=_mapper.Map<MovieViewModel>(movie);

            return movieViewModel;
        }

    }
}
