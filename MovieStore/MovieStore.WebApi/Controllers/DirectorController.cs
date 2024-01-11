using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStore.WebApi.Application.DirectorOperations.Commands.CreateDirector;
using MovieStore.WebApi.Application.DirectorOperations.Commands.DeleteDirector;
using MovieStore.WebApi.Application.DirectorOperations.Commands.UpdateDirector;
using MovieStore.WebApi.Application.DirectorOperations.Queries.GetDirectorDetail;
using MovieStore.WebApi.Application.DirectorOperations.Queries.GetDirectors;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.WebApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class DirectorController : ControllerBase
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public DirectorController(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        [HttpGet]
        public IActionResult GetDirectors()
        {
            GetDirectorsQuery query=new GetDirectorsQuery(_dbContext, _mapper);
          
            var directors = query.Handle();
            
            return Ok(directors);
        }
        
        [HttpGet("{directorId}")]
        public IActionResult GetDirectorById(int directorId)
        {
            GetDirectorByIdQuery query=new GetDirectorByIdQuery(_dbContext,_mapper);
           
            query.DirectorId=directorId;

            GetDirectorByIdQueryValidator validator = new GetDirectorByIdQueryValidator();
           
            validator.ValidateAndThrow(query);

            var director = query.Handle();
           
            return Ok(director);
        }
        
        [HttpPost]
        public IActionResult CreateDirector([FromBody] CreateDirectorModel newDirector)
        {
            CreateDirectorCommand command = new CreateDirectorCommand(_dbContext,_mapper);
           
            command.Model=newDirector;

            CreateDirectorCommandValidator validator = new CreateDirectorCommandValidator();
           
            validator.ValidateAndThrow(command);

            command.Handle();
           
            return Ok();
        }
        
        [HttpPut("{directorId}")]
        public IActionResult UpdateDirector(int directorId, [FromBody] UpdateDirectorModel updateDirector)
        {
            UpdateDirectorCommand command = new UpdateDirectorCommand(_dbContext);
            
            command.DirectorId = directorId;
            
            command.Model = updateDirector;

            UpdateDirectorCommandValidator validator = new UpdateDirectorCommandValidator();
            
            validator.ValidateAndThrow(command);

            command.Handle();
            
            return Ok();
        }
        
        [HttpDelete("{directorId}")]
        public IActionResult DeleteDirector(int directorId)
        {
            DeleteDirectorCommand command = new DeleteDirectorCommand(_dbContext);
            
            command.DirectorId = directorId;

            DeleteDirectorCommandValidator validator = new DeleteDirectorCommandValidator();
            
            validator.ValidateAndThrow(command);

            command.Handle();
            
            return Ok();
        }
    }
}
