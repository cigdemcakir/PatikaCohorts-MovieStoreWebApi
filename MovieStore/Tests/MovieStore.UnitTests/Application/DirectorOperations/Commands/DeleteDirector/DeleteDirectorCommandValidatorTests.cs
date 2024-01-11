using FluentAssertions;
using MovieStore.WebApi.Application.DirectorOperations.Commands.DeleteDirector;

namespace MovieStore.UnitTests.Application.DirectorOperations.Commands.DeleteDirector
{
    public class DeleteDirectorCommandValidatorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(int directorId)
        {
            //Arrange
            DeleteDirectorCommand command = new DeleteDirectorCommand(null);
            
            command.DirectorId = directorId;

            //Act 
            DeleteDirectorCommandValidator validator = new DeleteDirectorCommandValidator();
            
            var result = validator.Validate(command);

            //Assert 
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
