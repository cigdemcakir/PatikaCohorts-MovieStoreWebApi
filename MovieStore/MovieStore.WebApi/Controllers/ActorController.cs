using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStore.WebApi.Application.ActorOperations.Commands.CreateActor;
using MovieStore.WebApi.Application.ActorOperations.Commands.DeleteActor;
using MovieStore.WebApi.Application.ActorOperations.Commands.UpdateActor;
using MovieStore.WebApi.Application.ActorOperations.Queries.GetActor;
using MovieStore.WebApi.Application.ActorOperations.Queries.GetActorDetail;
using MovieStore.WebApi.Application.ActorOperations.Queries.QueryViewModel;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.WebApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class ActorController : ControllerBase
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public ActorController(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        [HttpGet]
        public IActionResult GetActors()
        {
            GetActorsQuery query = new GetActorsQuery(_dbContext, _mapper);
            
            List<ActorViewModel> actors = query.Handle();
            
            return Ok(actors);
        }
        
        [HttpGet("{actorId}")]
        public IActionResult GetActorById(int actorId)
        {
            GetActorByIdQuery query = new GetActorByIdQuery(_dbContext, _mapper);
            
            query.ActorId=actorId;

            GetActorByIdQueryValidator validator= new GetActorByIdQueryValidator();
            
            validator.ValidateAndThrow(query);

            var actor = query.Handle();
           
            return Ok(actor);
        }
        
        [HttpPost]
        public IActionResult CreateActor([FromBody] CreateActorModel newActor)
        {
            CreateActorCommand command = new CreateActorCommand(_dbContext, _mapper);
            
            command.Model = newActor;

            CreateActorCommandValidator validator = new CreateActorCommandValidator();
            
            validator.ValidateAndThrow(command);

            command.Handle();
           
            return Ok();
        }
        
        [HttpPut("{actorId}")]
        public IActionResult UpdateActor(int actorId, [FromBody] UpdateActorModel updateActor)
        {
            UpdateActorCommand command=new UpdateActorCommand(_dbContext);
          
            command.ActorId=actorId;
            
            command.Model =updateActor;

            UpdateActorCommandValidator validator = new UpdateActorCommandValidator();
            
            validator.ValidateAndThrow(command);

            command.Handle();
            
            return Ok();
        }
        
        [HttpDelete]
        public IActionResult DeleteActor(int actorId)
        {
            DeleteActorCommand command = new DeleteActorCommand(_dbContext);
           
            command.ActorId = actorId;

            DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
            
            validator.ValidateAndThrow(command);

            command.Handle();
           
            return Ok();
        }

    }
}
