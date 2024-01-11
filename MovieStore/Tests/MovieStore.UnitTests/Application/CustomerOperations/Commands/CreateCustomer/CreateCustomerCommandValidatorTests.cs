using FluentAssertions;
using MovieStore.WebApi.Application.CustomerOperations.Commands.CreateCustomer;

namespace MovieStore.UnitTests.Application.CustomerOperations.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidatorTests
    {
        [Theory]
        [InlineData("", "", "", "")]
        [InlineData("Name", "", "", "")]
        [InlineData("", "Surname", "", "")]
        [InlineData("", "", "Email", "")]
        [InlineData("", "", "", "Password")]
        [InlineData("Name", "Surname", "", "")]
        [InlineData("Name", "", "Email", "")]
        [InlineData("Name", "", "", "Password")]
        [InlineData("", "Surname", "Email", "")]
        [InlineData("", "Surname", "", "Password")]
        [InlineData("", "", "Email", "Password")]
        [InlineData("Name", "Surname", "Email", "")]
        [InlineData("Name", "Surname", "", "Password")]
        [InlineData("Name", "", "Email", "Password")]
        [InlineData("", "Surname", "Email", "Password")]
        public void WhenInvalidInputsAreGiven_InvalidOperationException_ShouldBeErrors(string name, string surname, string email, string password)
        {
            //Arrange
            CreateCustomerCommand command = new CreateCustomerCommand(null, null);
            
            command.Model = new CreateCustomerModel()
            {
                Name = name,
                Surname = surname,
                Email = email,
                Password = password
            };

            //Act 
            CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator();
            
            var result = validator.Validate(command);

            //Assert 
            result.Errors.Count.Should().BeGreaterThan(0);

        }
    }
}
