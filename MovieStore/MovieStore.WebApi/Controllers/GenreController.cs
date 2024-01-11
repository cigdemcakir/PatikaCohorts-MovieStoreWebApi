using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStore.WebApi.Application.GenreOperations.Commands.CreateGenre;
using MovieStore.WebApi.Application.GenreOperations.Commands.DeleteGenre;
using MovieStore.WebApi.Application.GenreOperations.Commands.UpdateGenre;
using MovieStore.WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using MovieStore.WebApi.Application.GenreOperations.Queries.GetGenres;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class GenreController : ControllerBase
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GenreController(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        [HttpGet]
        public IActionResult GetGenres()
        {
            GetGenresQuery query = new GetGenresQuery(_dbContext,_mapper);
            
            var genres = query.Handle();
            
            return Ok(genres);
        }
        
        [HttpGet("{genreId}")]
        public IActionResult GetGenreById(int genreId)
        {
            GetGenreByIdQuery query = new GetGenreByIdQuery(_dbContext, _mapper);
           
            query.GenreId=genreId;

            GetGenreByIdQueryValidator validator = new GetGenreByIdQueryValidator();
           
            validator.ValidateAndThrow(query);

            var genre = query.Handle();
           
            return Ok(genre);
        }
        
        [HttpPost]
        public IActionResult CreateGenre([FromBody] CreateGenreModel newGenre)
        {
            CreateGenreCommand command = new CreateGenreCommand(_dbContext, _mapper);
            
            command.Model=newGenre;

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            
            validator.ValidateAndThrow(command);

            command.Handle();
            
            return Ok();
        }
        
        [HttpPut("{genreId}")]
        public IActionResult UpdateGenre(int genreId, [FromBody] UpdateGenreModel updateGenre)
        {
            UpdateGenreCommand command=new UpdateGenreCommand(_dbContext);
           
            command.GenreId = genreId;
           
            command.Model=updateGenre;

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            
            validator.ValidateAndThrow(command);

            command.Handle();
            
            return Ok();
        }
        
        [HttpDelete("{genreId}")]
        public IActionResult DeleteGenre(int genreId)
        {
            DeleteGenreCommand command=new DeleteGenreCommand(_dbContext);
            
            command.GenreId = genreId;

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            
            validator.ValidateAndThrow(command);

            command.Handle();
            
            return Ok();
        }
    }
}
