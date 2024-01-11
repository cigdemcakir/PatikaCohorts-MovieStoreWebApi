using FluentAssertions;
using MovieStore.WebApi.Application.GenreOperations.Commands.UpdateGenre;

namespace MovieStore.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTests
    {
        [Theory]
        [InlineData("", 0)]
        [InlineData("nam", 0)]
        [InlineData("",1)]
        [InlineData("nam",1)]
        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(string genreName, int genreId)
        {
            //Arrange
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            
            command.GenreId = genreId;
            
            command.Model = new UpdateGenreModel()
            {
                Name = genreName
            };

            //Act 
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            
            var result = validator.Validate(command);

            //Assert 
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
