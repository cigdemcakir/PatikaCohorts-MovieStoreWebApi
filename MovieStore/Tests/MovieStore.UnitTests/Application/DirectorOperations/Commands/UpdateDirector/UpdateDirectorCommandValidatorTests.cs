using FluentAssertions;
using MovieStore.WebApi.Application.DirectorOperations.Commands.UpdateDirector;

namespace MovieStore.UnitTests.Application.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateDirectorCommandValidatorTests
    {
        [Theory]
        [InlineData("", "", 1)]
        [InlineData("nam", "", 1)]
        [InlineData("", "sur", 1)]
        [InlineData("name", "", 1)]
        [InlineData("", "surname", 1)]
        [InlineData("name", "sur", 1)]
        [InlineData("nam", "surname", 1)]
        [InlineData("nam", "sur", 1)]
        [InlineData("", "", 0)]
        [InlineData("nam", "", 0)]
        [InlineData("", "sur", 0)]
        [InlineData("name", "", 0)]
        [InlineData("", "surname", 0)]
        [InlineData("name", "sur", 0)]
        [InlineData("nam", "surname", 0)]
        [InlineData("nam", "sur", 0)]
        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(string directorName, string directorSurname, int directorId)
        {
            //Arrange
            UpdateDirectorCommand command = new UpdateDirectorCommand(null);
            
            command.DirectorId = directorId;
            
            command.Model = new UpdateDirectorModel()
            {
                Name = directorName,
                Surname = directorSurname
            };

            //Act 
            UpdateDirectorCommandValidator validator = new UpdateDirectorCommandValidator();
            
            var result = validator.Validate(command);

            //Assert 
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
