using FluentAssertions;
using MovieStore.WebApi.Application.MovieOperations.Commands.UpdateMovie;

namespace MovieStore.UnitTests.Application.MovieOperations.Commands.UpdateMovie
{
    public class UpdateMovieCommandValidatorTests
    {
        [Theory]
        [InlineData("", 0, 0, 0, 0, true)]
        [InlineData("", 1, 0, 0, 0, true)]
        [InlineData("", 0, 1, 0, 0, true)]
        [InlineData("", 0, 0, 1, 0, true)]
        [InlineData("", 0, 0, 0, 1, true)]
        [InlineData("", 1, 1, 0, 0, true)]
        [InlineData("", 1, 0, 1, 0, true)]
        [InlineData("", 1, 0, 0, 1, true)]
        [InlineData("", 0, 1, 1, 0, true)]
        [InlineData("", 0, 1, 0, 1, true)]
        [InlineData("", 0, 0, 1, 1, true)]
        [InlineData("", 1, 1, 1, 0, true)]
        [InlineData("", 1, 1, 0, 1, true)]
        [InlineData("", 1, 0, 1, 1, true)]
        [InlineData("", 0, 1, 1, 1, true)]
        [InlineData("", 1, 1, 1, 1, true)]
        [InlineData("tit", 0, 0, 0, 0, true)]
        [InlineData("tit", 1, 0, 0, 0, true)]
        [InlineData("tit", 0, 1, 0, 0, true)]
        [InlineData("tit", 0, 0, 1, 0, true)]
        [InlineData("tit", 0, 0, 0, 1, true)]
        [InlineData("tit", 1, 1, 0, 0, true)]
        [InlineData("tit", 1, 0, 1, 0, true)]
        [InlineData("tit", 1, 0, 0, 1, true)]
        [InlineData("tit", 0, 1, 1, 0, true)]
        [InlineData("tit", 0, 1, 0, 1, true)]
        [InlineData("tit", 0, 0, 1, 1, true)]
        [InlineData("tit", 1, 1, 1, 0, true)]
        [InlineData("tit", 1, 1, 0, 1, true)]
        [InlineData("tit", 1, 0, 1, 1, true)]
        [InlineData("tit", 0, 1, 1, 1, true)]
        [InlineData("tit", 1, 1, 1, 1, true)]
        [InlineData("tittle", 0, 0, 0, 0, true)]
        [InlineData("tittle", 1, 0, 0, 0, true)]
        [InlineData("tittle", 0, 1, 0, 0, true)]
        [InlineData("tittle", 0, 0, 1, 0, true)]
        [InlineData("tittle", 0, 0, 0, 1, true)]
        [InlineData("tittle", 1, 1, 0, 0, true)]
        [InlineData("tittle", 1, 0, 1, 0, true)]
        [InlineData("tittle", 1, 0, 0, 1, true)]
        [InlineData("tittle", 0, 1, 1, 0, true)]
        [InlineData("tittle", 0, 1, 0, 1, true)]
        [InlineData("tittle", 0, 0, 1, 1, true)]
        [InlineData("tittle", 1, 1, 1, 0, true)]
        [InlineData("tittle", 1, 1, 0, 1, true)]
        [InlineData("tittle", 1, 0, 1, 1, true)]
        [InlineData("tittle", 0, 1, 1, 1, true)]
        [InlineData("", 0, 0, 0, 0, false)]
        [InlineData("", 1, 0, 0, 0, false)]
        [InlineData("", 0, 1, 0, 0, false)]
        [InlineData("", 0, 0, 1, 0, false)]
        [InlineData("", 0, 0, 0, 1, false)]
        [InlineData("", 1, 1, 0, 0, false)]
        [InlineData("", 1, 0, 1, 0, false)]
        [InlineData("", 1, 0, 0, 1, false)]
        [InlineData("", 0, 1, 1, 0, false)]
        [InlineData("", 0, 1, 0, 1, false)]
        [InlineData("", 0, 0, 1, 1, false)]
        [InlineData("", 1, 1, 1, 0, false)]
        [InlineData("", 1, 1, 0, 1, false)]
        [InlineData("", 1, 0, 1, 1, false)]
        [InlineData("", 0, 1, 1, 1, false)]
        [InlineData("", 1, 1, 1, 1, false)]
        [InlineData("tit", 0, 0, 0, 0, false)]
        [InlineData("tit", 1, 0, 0, 0, false)]
        [InlineData("tit", 0, 1, 0, 0, false)]
        [InlineData("tit", 0, 0, 1, 0, false)]
        [InlineData("tit", 0, 0, 0, 1, false)]
        [InlineData("tit", 1, 1, 0, 0, false)]
        [InlineData("tit", 1, 0, 1, 0, false)]
        [InlineData("tit", 1, 0, 0, 1, false)]
        [InlineData("tit", 0, 1, 1, 0, false)]
        [InlineData("tit", 0, 1, 0, 1, false)]
        [InlineData("tit", 0, 0, 1, 1, false)]
        [InlineData("tit", 1, 1, 1, 0, false)]
        [InlineData("tit", 1, 1, 0, 1, false)]
        [InlineData("tit", 1, 0, 1, 1, false)]
        [InlineData("tit", 0, 1, 1, 1, false)]
        [InlineData("tit", 1, 1, 1, 1, false)]
        [InlineData("tittle", 0, 0, 0, 0, false)]
        [InlineData("tittle", 1, 0, 0, 0, false)]
        [InlineData("tittle", 0, 1, 0, 0, false)]
        [InlineData("tittle", 0, 0, 1, 0, false)]
        [InlineData("tittle", 0, 0, 0, 1, false)]
        [InlineData("tittle", 1, 1, 0, 0, false)]
        [InlineData("tittle", 1, 0, 1, 0, false)]
        [InlineData("tittle", 1, 0, 0, 1, false)]
        [InlineData("tittle", 0, 1, 1, 0, false)]
        [InlineData("tittle", 0, 1, 0, 1, false)]
        [InlineData("tittle", 0, 0, 1, 1, false)]
        [InlineData("tittle", 1, 1, 1, 0, false)]
        [InlineData("tittle", 1, 1, 0, 1, false)]
        [InlineData("tittle", 1, 0, 1, 1, false)]
        [InlineData("tittle", 0, 1, 1, 1, false)]
        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(string title, int movieId, int directorId, int genreId, decimal price, bool isActive)
        {
            //Arrange
            UpdateMovieCommand command = new UpdateMovieCommand(null);
            
            command.MovieId = movieId;
            
            command.Model = new UpdateMovieModel()
            {
                Title = title,
                DirectorId = directorId,
                GenreId = genreId,
                Price = price,
                IsActive = isActive 
            };

            //Act 
            UpdateMovieCommandValidator validator = new UpdateMovieCommandValidator();
            
            var result = validator.Validate(command);

            //Assert 
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
