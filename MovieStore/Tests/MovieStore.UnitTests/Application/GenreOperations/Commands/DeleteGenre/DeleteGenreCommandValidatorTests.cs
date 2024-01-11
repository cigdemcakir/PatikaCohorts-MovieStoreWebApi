using FluentAssertions;
using MovieStore.WebApi.Application.GenreOperations.Commands.DeleteGenre;

namespace MovieStore.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(int genreId)
        {
            //Arrange
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            
            command.GenreId = genreId;

            //Act 
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            
            var result = validator.Validate(command);
            
            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
