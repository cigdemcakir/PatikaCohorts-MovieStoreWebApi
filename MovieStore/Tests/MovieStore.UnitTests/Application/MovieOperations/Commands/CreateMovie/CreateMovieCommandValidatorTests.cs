using FluentAssertions;
using MovieStore.WebApi.Application.MovieOperations.Commands.CreateMovie;

namespace MovieStore.UnitTests.Application.MovieOperations.Commands.CreateMovie
{
    public class CreateMovieCommandValidatorTests
    {
        [Theory]
        [InlineData(0, 0, 0, "")]
        [InlineData(1, 0, 0, "")]
        [InlineData(0, 1, 0, "")]
        [InlineData(0, 0, 1, "")]
        [InlineData(1, 1, 0, "")]
        [InlineData(1, 0, 1, "")]
        [InlineData(0, 1, 1, "")]
        [InlineData(1, 1, 1, "")]
        [InlineData(0, 0, 0, "tit")]
        [InlineData(1, 0, 0, "tit")]
        [InlineData(0, 1, 0, "tit")]
        [InlineData(0, 0, 1, "tit")]
        [InlineData(1, 1, 0, "tit")]
        [InlineData(1, 0, 1, "tit")]
        [InlineData(0, 1, 1, "tit")]
        [InlineData(1, 1, 1, "tit")]
        [InlineData(0, 0, 0, "title")]
        [InlineData(1, 0, 0, "title")]
        [InlineData(0, 1, 0, "title")]
        [InlineData(0, 0, 1, "title")]
        [InlineData(1, 1, 0, "title")]
        [InlineData(1, 0, 1, "title")]
        [InlineData(0, 1, 1, "title")]

        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(int genreId,int directorId,decimal price,string title)
        {
            //Arrange
            CreateMovieCommand command = new CreateMovieCommand(null, null);
            
            command.Model = new CreateMovieModel()
            {
                Title = title,
                GenreId = genreId,
                DirectorId = directorId,
                Price = price,
                YearOfMovie = DateTime.Now.AddYears(-1)
            };

            //Act 
            CreateMovieCommandValidator validator = new CreateMovieCommandValidator();
            
            var result = validator.Validate(command);

            //Assert 
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenAnIncorrectDateIsEntered_InvalidOperationException_ShouldBeError()
        {
            //Arrange
            CreateMovieCommand command = new CreateMovieCommand(null, null);
            
            command.Model = new CreateMovieModel()
            {
                Title = "movie",
                GenreId = 1,
                DirectorId = 1,
                Price = 100,
                YearOfMovie = DateTime.Now
            };

            //Act 
            CreateMovieCommandValidator validator = new CreateMovieCommandValidator();
            
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
