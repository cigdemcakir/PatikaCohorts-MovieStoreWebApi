using FluentAssertions;
using MovieStore.WebApi.Application.ActorMovieOperations.Commands.CreateActorMovie;

namespace MovieStore.UnitTests.Application.ActorMovieOperations.Commands.CreateActorMovie
{
    public class CreateActorMovieCommandValidatorTests
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(-1, -1)]
        [InlineData(1, -1)]
        [InlineData(-1, 1)]
        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(int actorId, int movieId)
        {
            //Arrange
            CreateActorMovieCommand command = new CreateActorMovieCommand(null, null);
            
            command.Model = new CreateActorMovieModel()
            {
                ActorId = actorId,
                MovieId = movieId
            };

            //Act 
            CreateActorMovieCommandValidator validator = new CreateActorMovieCommandValidator();
            
            var result=validator.Validate(command);

            //Assert 
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
