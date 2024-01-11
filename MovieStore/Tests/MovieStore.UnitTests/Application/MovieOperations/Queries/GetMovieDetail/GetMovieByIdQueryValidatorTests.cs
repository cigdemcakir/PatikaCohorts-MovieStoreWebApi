using FluentAssertions;
using MovieStore.WebApi.Application.MovieOperations.Queries.GetMovieDetail;

namespace MovieStore.UnitTests.Application.MovieOperations.Queries.GetMovieDetail
{
    public class GetMovieByIdQueryValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidMovieIdsAreGive_InvalidOperationException_ShouldBeReturned(int movieId)
        {
            //Assert 
            GetMovieByIdQuery query = new GetMovieByIdQuery(null, null);
            
            query.MovieId= movieId;

            //Act 
            GetMovieByIdQueryValidator validator = new GetMovieByIdQueryValidator();
            
            var result=validator.Validate(query);

            //Arrange 
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
