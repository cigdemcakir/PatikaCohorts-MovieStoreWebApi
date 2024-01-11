using FluentAssertions;
using MovieStore.WebApi.Application.ActorOperations.Commands.DeleteActor;

namespace MovieStore.UnitTests.Application.ActorOperations.Commands.DeleteActor
{
    public class DeleteActorCommandValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(int actorId)
        {
            //Arrange
            DeleteActorCommand command = new DeleteActorCommand(null);
            
            command.ActorId = actorId;

            //Act 
            DeleteActorCommandValidator validator = new DeleteActorCommandValidator();
            
            var result = validator.Validate(command);
            
            //Assert 
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
