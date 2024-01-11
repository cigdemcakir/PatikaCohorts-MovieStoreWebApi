using AutoMapper;
using FluentAssertions;
using MovieStore.UnitTests.TestsSetup;
using MovieStore.WebApi.Application.ActorMovieOperations.Commands.CreateActorMovie;
using MovieStore.WebApi.Application.ActorOperations.Commands.CreateActor;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.UnitTests.Application.ActorMovieOperations.Commands.CreateActorMovie
{
    public class CreateActorMovieCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateActorMovieCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputIsGiven_ActorMovie_ShouldBeCreated()
        {
            //Arrange
            CreateActorCommand createActorCommand = new CreateActorCommand(_dbContext, _mapper);
            
            createActorCommand.Model = new CreateActorModel()
            {
                Name = "actor",
                Surname = "actor"
            };
            
            createActorCommand.Handle();
            
            var actorId = _dbContext.Actors.FirstOrDefault(a => a.Name == "actor" & a.Surname == "actor").Id;
            
            CreateActorMovieCommand command = new CreateActorMovieCommand(_dbContext, _mapper);
            
            command.Model = new CreateActorMovieModel()
            {
                ActorId = actorId,
                MovieId = 5
            };

            //Act 
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert 
            var actorMovie = _dbContext.ActorsMovies.FirstOrDefault(am => am.ActorId == actorId && am.MovieId == 5);
            actorMovie.Should().NotBeNull();
        }
        
        [Fact]
        public void WhenAlreadyActorMovieRelationshipIsGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange
            var actorMovie = _dbContext.ActorsMovies.FirstOrDefault();
            
            CreateActorMovieCommand command = new CreateActorMovieCommand(_dbContext, _mapper);
            
            command.Model = new CreateActorMovieModel()
            {
                ActorId = actorMovie.ActorId,
                MovieId = actorMovie.MovieId
            };
            
            //Act & Assert 
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("There is an Actor-Movie Relationship.");
        }
    }
}
