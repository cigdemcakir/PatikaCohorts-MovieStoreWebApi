using FluentAssertions;
using MovieStore.WebApi.Application.OrderOperations.Commands.CreateOrder;

namespace MovieStore.UnitTests.Application.OrderOperations.Commands.CreateOrder
{
    public class CreateOrderCommandValidatorTests
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        public void WhenInvalidAreGiven_InvalidOperationException_ShouldBeError(int movieId, int customerId)
        {
            //Arrange
            CreateOrderCommand command = new CreateOrderCommand(null, null);
            
            command.Model = new CreateOrderModel()
            {
                MovieId = movieId,
                CustomerId = customerId
            };

            //Act 
            CreateOrderCommandValidator validator = new CreateOrderCommandValidator(); 
            
            var result = validator.Validate(command);

            //Assert 
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
