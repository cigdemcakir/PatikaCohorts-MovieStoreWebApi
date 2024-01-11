using FluentAssertions;
using MovieStore.WebApi.Application.DirectorOperations.Queries.GetDirectorDetail;

namespace MovieStore.UnitTests.Application.DirectorOperations.Queries.GetDirectorDetail
{
    public class GetDirectorByIdQueryValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(int directorId)
        {
            //Arrange
            GetDirectorByIdQuery command = new GetDirectorByIdQuery(null, null);
            
            command.DirectorId = directorId;

            //Act 
            GetDirectorByIdQueryValidator validator = new GetDirectorByIdQueryValidator();
            
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
