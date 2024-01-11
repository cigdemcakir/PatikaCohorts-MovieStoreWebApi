using FluentAssertions;
using MovieStore.WebApi.Application.ActorMovieOperations.Queries.GetActorMovieDetail;

namespace MovieStore.UnitTests.Application.ActorMovieOperations.Queries.GetActorMovieDetail
{
    public class GetActorMovieByIdQueryValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(int actorMovieId)
        {
            //Arrange
            GetActorMovieByIdQuery command = new GetActorMovieByIdQuery(null, null);
            
            command.ActorMovieId = actorMovieId;

            //Act 
            GetActorMovieByIdQueryValidator validator = new GetActorMovieByIdQueryValidator();
            
            var result = validator.Validate(command);

            //Assert 
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
