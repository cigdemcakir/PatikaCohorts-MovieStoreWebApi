using AutoMapper;
using FluentAssertions;
using MovieStore.UnitTests.TestsSetup;
using MovieStore.WebApi.Application.ActorOperations.Commands.CreateActor;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.UnitTests.Application.ActorOperations.Commands.CreateActor
{
    public class CreateActorCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateActorCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenValidInputIsGiven_Actor_ShouldBeCreated()
        {
            //Arrange
            CreateActorCommand command = new CreateActorCommand(_dbContext, _mapper);
            
            command.Model = new CreateActorModel()
            {
                Name="test",
                Surname="test"
            };

            //Act 
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert 
            var actor = _dbContext.Actors.FirstOrDefault(a => a.Name == "test" && a.Surname == "test");
            
            actor.Should().NotBeNull();
        }
        [Fact]
        public void WhenAlreadyActorIsGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange
            CreateActorCommand command = new CreateActorCommand(_dbContext, _mapper);
            
            command.Model = new CreateActorModel()
            {
                Name = "Brad",
                Surname = "Pitt"
            };

            //Act & Assert 
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("An Actor Exists.");
        }
    }
}
