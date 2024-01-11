using AutoMapper;
using MovieStore.WebApi.Application.GenreOperations.Queries.QueryViewModel;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.WebApi.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreByIdQuery
    {
        public int GenreId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetGenreByIdQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public GenreViewModel Handle()
        {
            var genre=_dbContext.Genres.FirstOrDefault(g=>g.IsActive && g.Id==GenreId);
         
            if (genre is null)
            {
                throw new InvalidOperationException("Genre Not Found.");
            }
          
            GenreViewModel viewModel=_mapper.Map<GenreViewModel>(genre);
           
            return viewModel;
        }
    }
}
