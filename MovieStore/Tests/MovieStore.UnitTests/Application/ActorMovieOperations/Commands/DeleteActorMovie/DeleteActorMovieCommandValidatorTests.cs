using FluentAssertions;
using MovieStore.WebApi.Application.ActorMovieOperations.Commands.DeleteActorMovie;

namespace MovieStore.UnitTests.Application.ActorMovieOperations.Commands.DeleteActorMovie
{
    public class DeleteActorMovieCommandValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(int actorMovieId)
        {
            //Arrange
            DeleteActorMovieCommand command = new DeleteActorMovieCommand(null);
            
            command.ActorMovieId = actorMovieId;

            //Act
            DeleteActorMovieCommandValidator validator = new DeleteActorMovieCommandValidator();
            
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
