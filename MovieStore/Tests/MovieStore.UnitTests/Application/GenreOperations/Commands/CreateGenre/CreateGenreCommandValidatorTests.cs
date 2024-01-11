using FluentAssertions;
using MovieStore.WebApi.Application.GenreOperations.Commands.CreateGenre;

namespace MovieStore.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("nam")]

        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(string genreName)
        {
            //Arrange
            CreateGenreCommand command = new CreateGenreCommand(null, null);
            
            command.Model = new CreateGenreModel()
            {
                Name = genreName
            };

            //Act 
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            
            var result = validator.Validate(command);

            //Assert 
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
