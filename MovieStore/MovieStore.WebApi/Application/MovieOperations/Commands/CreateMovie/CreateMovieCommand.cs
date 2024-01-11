using AutoMapper;
using MovieStore.WebApi.DBOperations;
using MovieStore.WebApi.Entities;

namespace MovieStore.WebApi.Application.MovieOperations.Commands.CreateMovie
{
    public class CreateMovieCommand
    {
        public CreateMovieModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateMovieCommand(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var movie = _dbContext.Movies.FirstOrDefault(m => m.Title == Model.Title && m.DirectorId == Model.DirectorId);
         
            if(movie is not null)
            {
                throw new InvalidOperationException("There is a movie with this name and director.");
            }
           
            var hasGenre=_dbContext.Genres.Any(g=>g.Id == Model.GenreId);
            
            if (!hasGenre)
            {
                throw new InvalidOperationException("No Valid Movie Genre Found.");
            }

            movie = _mapper.Map<Movie>(Model);
            
            _dbContext.Movies.Add(movie);
            _dbContext.SaveChanges();
        }
    }
}
