using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStore.WebApi.Application.MovieOperations.Commands.CreateMovie;
using MovieStore.WebApi.Application.MovieOperations.Commands.DeleteMovie;
using MovieStore.WebApi.Application.MovieOperations.Commands.UpdateMovie;
using MovieStore.WebApi.Application.MovieOperations.Queries.GetMovieDetail;
using MovieStore.WebApi.Application.MovieOperations.Queries.GetMovies;
using MovieStore.WebApi.DbOperations;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public MovieController(MovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetMovies()
        {
            GetMoviesQuery query = new GetMoviesQuery(_dbContext, _mapper);
            
            var result= query.Handle();
           
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetMovie(int id)
        {
            GetMovieByIdQuery query = new GetMovieByIdQuery(_dbContext, _mapper);
        
            query.MovieId = id;

            GetMovieByIdQueryValidator validator=new GetMovieByIdQueryValidator();
           
            validator.ValidateAndThrow(query);

            var result = query.Handle();
            
            return Ok(result);
        }
        
        
        [HttpPost]
        public IActionResult CreateMovie([FromBody] CreateMovieModel newMovie)
        {
            CreateMovieCommand command = new CreateMovieCommand(_dbContext, _mapper);
            
            command.Model=newMovie;

            CreateMovieCommandValidator validator = new CreateMovieCommandValidator();
            
            validator.ValidateAndThrow(command);

            command.Handle();
            
            return Ok();
        }
        
        
        [HttpPut("{movieId}")]
        public IActionResult UpdateMovie(int movieId, [FromBody] UpdateMovieModel updateMovie)
        {
            UpdateMovieCommand command = new UpdateMovieCommand(_dbContext);
           
            command.MovieId= movieId;
           
            command.Model=updateMovie;

            UpdateMovieCommandValidator validator = new UpdateMovieCommandValidator();
            
            validator.ValidateAndThrow(command);

            command.Handle();
           
            return Ok();
        }
        
        [HttpDelete("{movieId}")]
        public IActionResult DeleteMovie(int movieId)
        {
            DeleteMovieCommand command = new DeleteMovieCommand(_dbContext);
           
            command.MovieId= movieId;

            DeleteMovieCommandValidator validator = new DeleteMovieCommandValidator();
            
            validator.ValidateAndThrow(command);

            command.Handle();
            
            return Ok();
        }
    }
}
