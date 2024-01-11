using FluentAssertions;
using MovieStore.WebApi.Application.MovieOperations.Commands.DeleteMovie;

namespace MovieStore.UnitTests.Application.MovieOperations.Commands.DeleteMovie
{
    public class DeleteMovieCommandValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(int movieId)
        {
            //Arrange
            DeleteMovieCommand command = new DeleteMovieCommand(null);
            
            command.MovieId = movieId;

            //Act 
            DeleteMovieCommandValidator validator = new DeleteMovieCommandValidator();
            
            var result = validator.Validate(command);

            //Assert 
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
