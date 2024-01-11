using FluentAssertions;
using MovieStore.WebApi.Application.ActorOperations.Commands.CreateActor;

namespace MovieStore.UnitTests.Application.ActorOperations.Commands.CreateActor
{
    public class CreateActorCommandValidatorTests
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

        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(string actorName, string actorSurname)
        {
            //Arrange
            CreateActorCommand command = new CreateActorCommand(null, null);
            
            command.Model = new CreateActorModel()
            {
                Name=actorName,
                Surname=actorSurname
            };

            //Act
            CreateActorCommandValidator validator = new CreateActorCommandValidator();
            
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
