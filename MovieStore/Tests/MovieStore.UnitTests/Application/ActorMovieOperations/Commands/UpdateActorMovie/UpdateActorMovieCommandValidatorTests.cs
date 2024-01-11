using FluentAssertions;
using MovieStore.WebApi.Application.ActorMovieOperations.Commands.UpdateActorMovie;

namespace MovieStore.UnitTests.Application.ActorMovieOperations.Commands.UpdateActorMovie
{
    public class UpdateActorMovieCommandValidatorTests
    {
        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 0, 0)]
        [InlineData(0, 1, 0)]
        [InlineData(0, 0, 1)]
        [InlineData(1, 1, 0)]
        [InlineData(1, 0, 1)]
        [InlineData(0, 1, 1)]
        [InlineData(-1, 1, 1)]
        [InlineData(1, -1, 1)]
        [InlineData(1, 1, -1)]
        [InlineData(-1, -1, 1)]
        [InlineData(-1, 1, -1)]
        [InlineData(1, -1, -1)]
        [InlineData(-1, -1, -1)]
        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(int actorMovieId, int actorId, int movieId)
        {
            //Arrange
            UpdateActorMovieCommand command = new UpdateActorMovieCommand(null);
            
            command.ActorMovieId = actorMovieId;
            
            command.Model = new UpdateActorMovieModel()
            {
                ActorId = actorId,
                MovieId = movieId
            };

            //Act 
            UpdateActorMovieCommandValidator validator = new UpdateActorMovieCommandValidator();
            
            var result = validator.Validate(command);
            
            //Assert 
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
