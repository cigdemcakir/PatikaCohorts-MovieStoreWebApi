using FluentAssertions;
using MovieStore.WebApi.Application.DirectorOperations.Commands.CreateDirector;

namespace MovieStore.UnitTests.Application.DirectorOperations.Commands.CreateDirector
{
    public class CreateDirectorCommandValidatorTests
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("nam", "")]
        [InlineData("", "sur")]
        [InlineData("name", "")]
        [InlineData("", "surname")]
        [InlineData("name", "sur")]
        [InlineData("nam", "surname")]
        [InlineData("nam", "sur")]

        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(string directorName, string directorSurname)
        {
            //Arrange
            CreateDirectorCommand command = new CreateDirectorCommand(null, null);
            
            command.Model = new CreateDirectorModel()
            {
                Name = directorName,
                Surname = directorSurname
            };

            //Act 
            CreateDirectorCommandValidator validator = new CreateDirectorCommandValidator();
            
            var result = validator.Validate(command);

            //Assert 
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
