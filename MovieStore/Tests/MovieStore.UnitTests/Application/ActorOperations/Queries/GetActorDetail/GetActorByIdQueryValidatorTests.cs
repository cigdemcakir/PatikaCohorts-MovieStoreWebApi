using FluentAssertions;
using MovieStore.WebApi.Application.ActorOperations.Queries.GetActorDetail;

namespace MovieStore.UnitTests.Application.ActorOperations.Queries.GetActorDetail
{
    public class GetActorByIdQueryValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(int actorId)
        {
            //Arrange
            GetActorByIdQuery command = new GetActorByIdQuery(null, null);
            
            command.ActorId = actorId;

            //Act 
            GetActorByIdQueryValidator validator = new GetActorByIdQueryValidator();
            
            var result = validator.Validate(command);
            
            //Assert 
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
