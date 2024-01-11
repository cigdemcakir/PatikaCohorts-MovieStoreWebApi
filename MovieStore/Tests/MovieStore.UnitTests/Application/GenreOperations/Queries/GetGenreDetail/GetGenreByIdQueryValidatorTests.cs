using FluentAssertions;
using MovieStore.WebApi.Application.GenreOperations.Queries.GetGenreDetail;

namespace MovieStore.UnitTests.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreByIdQueryValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(int genreId)
        {
            //Arrange
            GetGenreByIdQuery command = new GetGenreByIdQuery(null, null);
            
            command.GenreId = genreId;

            //Act 
            GetGenreByIdQueryValidator validator = new GetGenreByIdQueryValidator();
            
            var result = validator.Validate(command);

            //Assert 
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
