using FluentAssertions;
using MovieStore.WebApi.Application.CustomerOperations.Commands.CreateToken;

namespace MovieStore.UnitTests.Application.CustomerOperations.Commands.CreateToken
{
    public class CreateTokenCommandValidatorTests
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("email", "")]
        [InlineData("", "password")]
        [InlineData("email", "pas")]
        [InlineData("ema", "password")]
        public void WhenInvalidLoginInputsAreGiven_InvalidOperationException_ShouldBeErrors(string email, string password)
        {
            //Arrange
            CreateTokenCommand command = new CreateTokenCommand(null, null, null);
            
            command.Model = new CreateTokenModel()
            {
                Email = email,
                Password = password
            };

            //Act 
            CreateTokenCommandValidator validator = new CreateTokenCommandValidator();
            
            var result = validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
