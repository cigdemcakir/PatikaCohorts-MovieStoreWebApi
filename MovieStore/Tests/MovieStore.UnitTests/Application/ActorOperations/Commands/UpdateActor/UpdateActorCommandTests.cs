using FluentAssertions;
using MovieStore.UnitTests.TestsSetup;
using MovieStore.WebApi.Application.ActorOperations.Commands.UpdateActor;
using MovieStore.WebApi.DBOperations;

namespace MovieStore.UnitTests.Application.ActorOperations.Commands.UpdateActor
{
    public class UpdateActorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IMovieStoreDbContext _dbContext;

        public UpdateActorCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
        }
        
        [Fact]
        public void WhenValidInputIsGiven_Actor_ShouldBeUpdated()
        {
            //Arrange
            UpdateActorCommand command = new UpdateActorCommand(_dbContext);
            
            command.ActorId = 1;
            
            command.Model = new UpdateActorModel()
            {
                Name = "actortobeupdated",
                Surname = "actortobeupdated"
            };

            //Act 
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //Assert
            var actor = _dbContext.Actors.FirstOrDefault(a => a.Name == "actortobeupdated" && a.Surname == "actortobeupdated");
            
            actor.Should().NotBeNull();
        }
        
        [Fact]
        public void WhenAlreadyActorIsGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange
            var registeredActor = _dbContext.Actors.Last();
            
            UpdateActorCommand command = new UpdateActorCommand(_dbContext);
            
            command.ActorId = 1;
            
            command.Model = new UpdateActorModel()
            {
                Name = registeredActor.Name,
                Surname = registeredActor.Surname
            };

            //Act & Assert 
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("There is a registered actor with the actor's name and surname.");
        }
        [Fact]
        public void WhenNonActorIdIsGiven_InvalidOperationException_ShouldBeError()
        {
            //Arrange
            UpdateActorCommand command = new UpdateActorCommand(_dbContext);
            
            command.ActorId = 999999999;
            
            command.Model = new UpdateActorModel()
            {
                Name = "",
                Surname = ""
            };

            //Act & Assert
            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Actor Not Found.");
        }
    }
}
